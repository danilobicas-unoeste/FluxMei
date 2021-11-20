using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LivroCaixa.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LivroCaixa.Controllers
{
    [Authorize]
    public class MeiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Mei
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            ApplicationUser usuario = db.Users.Find(userid);
            return View(db.Meis.Where(u=>u.IdMei == usuario.IdMei).ToList());
        }

        // GET: Mei/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mei mei = db.Meis.Find(id);
            if (mei == null)
            {
                return HttpNotFound();
            }
            return View(mei);
        }

        // GET: Mei/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mei/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMei,Login,Senha,NomeEmpresa,Logradouto,Cnpj,NomeProprietario,Telefone")] Mei mei)
        {
            if (ModelState.IsValid)
            {
                db.Meis.Add(mei);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mei);
        }

        // GET: Mei/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mei mei = db.Meis.Find(id);
            if (mei == null)
            {
                return HttpNotFound();
            }
            return View(mei);
        }

        // POST: Mei/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMei,Login,Senha,NomeEmpresa,Logradouto,Cnpj,NomeProprietario,Telefone")] Mei mei)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mei).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mei);
        }

        // GET: Mei/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mei mei = db.Meis.Find(id);
            if (mei == null)
            {
                return HttpNotFound();
            }
            return View(mei);
        }

        // POST: Mei/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mei mei = db.Meis.Find(id);
            db.Meis.Remove(mei);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
