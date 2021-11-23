using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LivroCaixa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult IndexLogado()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Descrição do Projeto.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Descrição dos Participantes.";

            return View();
        }
    }
}