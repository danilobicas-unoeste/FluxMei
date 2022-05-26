using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LivroCaixa.Filters;
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
        private readonly FirestoreProvider _firestoreProvider;

        public MeiController(FirestoreProvider firestoreProvider)
        {
            _firestoreProvider = firestoreProvider;
        }

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
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            //Mei mei = db.Meis.Find(id);
            Mei mei = await _firestoreProvider.Get<Mei>(id.ToString(), token);
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
        [ValidateAntiModelInjection("IdMei")]
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
                return RedirectToAction("IndexLogado");
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
            int meilogado = int.Parse(Session["mei"].ToString());
            if ((mei == null) || (mei.IdMei != meilogado))
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
            int meilogado = int.Parse(Session["mei"].ToString());
            if (meilogado == mei.IdMei)
            {
                var usuarios = db.Users.Where(u => u.IdMei == meilogado).ToList();
                foreach(var u in usuarios)
                {
                    db.Users.Remove(u);
                }
                var tiposmov = db.TipoMovimentoes.Where(t => t.IdMei == meilogado).ToList();
                foreach (var t in tiposmov) 
                {
                    db.TipoMovimentoes.Remove(t);
                }
                var movs = db.Movimentoes.Where(t => t.IdMei == meilogado).ToList();
                foreach(var m in movs)
                {
                    db.Movimentoes.Remove(m);
                }
                db.Meis.Remove(mei);
                db.SaveChanges();
            }
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
