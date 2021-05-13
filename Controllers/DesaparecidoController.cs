using Dory2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dory2.Controllers
{
    public class DesaparecidoController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Desaparecido
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InitialRegisterDesaparecido()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InitialRegisterDesaparecido(InitialRegisterDesaparecido cad)
        {
            return RedirectToAction("FinalRegisterDesaparecido");
        }


        public ActionResult FinalRegisterDesaparecido()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalRegisterDesaparecido(FinalRegisterDesaparecido cad)
        {
            return View();
        }
    }
}