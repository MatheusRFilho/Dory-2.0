﻿using Dory2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dory2.Controllers
{
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            List<Tutorias> infos = db.Tutorias.ToList();
            List<Galeria> gal = db.Galeria.ToList();
            ViewBag.FotosPerfil = gal.ToArray();

            DateTime padrao = new DateTime();
            int des = db.Desaparecido.Where(x => x.Encontrado > padrao).Count();
            ViewBag.PessoasEncontradas = des;

            return View(infos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}