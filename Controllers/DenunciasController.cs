using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dory2.Models;

namespace Dory2.Controllers
{
    public class DenunciasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Denuncias
        public ActionResult Index()
        {
            return View(db.Denuncias.ToList());
        }

        // GET: Denuncias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Denuncias denuncias = db.Denuncias.Find(id);
            if (denuncias == null)
            {
                return HttpNotFound();
            }
            return View(denuncias);
        }

        // GET: Denuncias/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Denuncias/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao,DataDenuncia,TipoDenuncia,Status,RepostaAdm")] Denuncias denuncias)
        {
            if (ModelState.IsValid)
            {
                db.Denuncias.Add(denuncias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(denuncias);
        }

        // GET: Denuncias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Denuncias denuncias = db.Denuncias.Find(id);
            if (denuncias == null)
            {
                return HttpNotFound();
            }
            return View(denuncias);
        }

        // POST: Denuncias/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao,DataDenuncia,TipoDenuncia,Status,RepostaAdm")] Denuncias denuncias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(denuncias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(denuncias);
        }

        // GET: Denuncias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Denuncias denuncias = db.Denuncias.Find(id);
            if (denuncias == null)
            {
                return HttpNotFound();
            }
            return View(denuncias);
        }

        // POST: Denuncias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Denuncias denuncias = db.Denuncias.Find(id);
            db.Denuncias.Remove(denuncias);
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
