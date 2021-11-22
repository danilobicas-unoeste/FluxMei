using LivroCaixa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace LivroCaixa.Controllers
{
    public class RelatoriosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Relatorios
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RelatorioPDF([Bind(Include = "DataInicial,DataFinal")] PeriodoViewModel periodo)
        {
            int mei = int.Parse(Session["mei"].ToString());
            RelatorioViewModel relatorio = new RelatorioViewModel();
            var movimentos = db.Movimentoes.Include(m => m.TipoMovimento)
                                           .Include(m => m.Mei)
                                           .Where(m => m.IdMei == mei)
                                           .Where(m => (m.Data >= periodo.DataInicial) && (m.Data <= periodo.DataFinal)).ToList();
            decimal saldo = 0;
            foreach (var item in movimentos)
            {
                if (item.TipoMovimento.receitadespesa == "R")
                {
                    saldo += item.Valor;
                }
                if (item.TipoMovimento.receitadespesa == "D")
                {
                    saldo -= item.Valor;
                }
            }
            relatorio.saldo = saldo;
            relatorio.movimentos = movimentos;
            return View(movimentos);
        }
    }
}