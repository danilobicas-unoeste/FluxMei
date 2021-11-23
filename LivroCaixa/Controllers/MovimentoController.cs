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
    public class MovimentoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movimento
        public ActionResult Index()
        {
            int mei = int.Parse(Session["mei"].ToString());
            var movimentoes = db.Movimentoes.Where(m=>m.IdMei == mei).Include(m => m.Mei).Include(m => m.TipoMovimento);
            return View(movimentoes.OrderByDescending(m=>m.Data).Take(50).ToList());
        }

        // GET: Movimento/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimento movimento = db.Movimentoes.Find(id);
            if (movimento == null)
            {
                return HttpNotFound();
            }
            return View(movimento);
        }

        // GET: Movimento/Create
        public ActionResult Create()
        {
            //ViewBag.IdMei = new SelectList(db.Meis, "IdMei", "Login");
            int mei = int.Parse(Session["mei"].ToString());
            ViewBag.TipoMovimentoId = new SelectList(db.TipoMovimentoes, "tipoid", "descricao");
            Movimento movimento = new Movimento();
            movimento.IdMei = mei;
            return View(movimento);
        }

        // POST: Movimento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMovimento,Descicao,Total,Data,Valor,TipoMovimentoId,IdMei,userName")] Movimento movimento)
        {
            int mei = int.Parse(Session["mei"].ToString());
            movimento.userName = User.Identity.Name;
            movimento.IdMei = mei;
            if (ModelState.IsValid)
            {
                db.Movimentoes.Add(movimento);
                db.SaveChanges();
                return RedirectToAction("Index", "Movimento");
            }

            ViewBag.IdMei = new SelectList(db.Meis, "IdMei", "Login", movimento.IdMei);
            ViewBag.TipoMovimentoId = new SelectList(db.TipoMovimentoes, "tipoid", "descricao", movimento.TipoMovimentoId);
            return View(movimento);
        }

        // GET: Movimento/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimento movimento = db.Movimentoes.Find(id);
            if (movimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMei = new SelectList(db.Meis, "IdMei", "Login", movimento.IdMei);
            ViewBag.TipoMovimentoId = new SelectList(db.TipoMovimentoes, "tipoid", "descricao", movimento.TipoMovimentoId);
            return View(movimento);
        }

        // POST: Movimento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMovimento,Descicao,Total,Data,Valor,TipoMovimentoId,IdMei,userName")] Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMei = new SelectList(db.Meis, "IdMei", "Login", movimento.IdMei);
            ViewBag.TipoMovimentoId = new SelectList(db.TipoMovimentoes, "tipoid", "descricao", movimento.TipoMovimentoId);
            return View(movimento);
        }

        // GET: Movimento/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimento movimento = db.Movimentoes.Find(id);
            if (movimento == null)
            {
                return HttpNotFound();
            }
            return View(movimento);
        }

        // POST: Movimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Movimento movimento = db.Movimentoes.Find(id);
            db.Movimentoes.Remove(movimento);
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
