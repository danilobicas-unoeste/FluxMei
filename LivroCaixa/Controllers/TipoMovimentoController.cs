using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LivroCaixa.Models;

namespace LivroCaixa.Controllers
{
    [Authorize]
    public class TipoMovimentoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoMovimento
        public ActionResult Index()
        {
            int mei = int.Parse(Session["mei"].ToString());
            return View(db.TipoMovimentoes.Where(t=>t.IdMei == mei).ToList());
        }

        // GET: TipoMovimento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMovimento tipoMovimento = db.TipoMovimentoes.Find(id);
            if (tipoMovimento == null)
            {
                return HttpNotFound();
            }
            return View(tipoMovimento);
        }

        // GET: TipoMovimento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoMovimento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipoid,descricao,receitadespesa")] TipoMovimento tipoMovimento)
        {
            tipoMovimento.IdMei = int.Parse(Session["mei"].ToString());
            if (ModelState.IsValid)
            {
                
                db.TipoMovimentoes.Add(tipoMovimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoMovimento);
        }

        // GET: TipoMovimento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMovimento tipoMovimento = db.TipoMovimentoes.Find(id);
            if (tipoMovimento == null)
            {
                return HttpNotFound();
            }
            return View(tipoMovimento);
        }

        // POST: TipoMovimento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tipoid,descricao,receitadespesa")] TipoMovimento tipoMovimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoMovimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoMovimento);
        }

        // GET: TipoMovimento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMovimento tipoMovimento = db.TipoMovimentoes.Find(id);
            if (tipoMovimento == null)
            {
                return HttpNotFound();
            }
            return View(tipoMovimento);
        }

        // POST: TipoMovimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoMovimento tipoMovimento = db.TipoMovimentoes.Find(id);
            db.TipoMovimentoes.Remove(tipoMovimento);
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
