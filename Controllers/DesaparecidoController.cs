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
            int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
            Responsavel res = db.Responsavel.Where(x => x.Id == resId).ToList().FirstOrDefault();
            if(res.Pessoa.Cpf == null)
            {
                return RedirectToAction("TerminarRegistro", "Responsavels");
            }
            else
            {
                return View();
            }
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

        public ActionResult ConfirmationRegisterDesaparecido()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmationRegisterDesaparecido(ConfirmationRegisterDesaparecido cad)
        {
            return View();
        }

        public ActionResult ListDesaparecido()
        {
            return View();
        }


        public ActionResult ListOneDesaparecido()
        {
            return View();
        }

    }
}