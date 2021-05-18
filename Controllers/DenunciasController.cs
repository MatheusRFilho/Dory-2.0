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
            var denuncias = db.Denuncias.Include(d => d.Desaparecido);
            return View(denuncias.ToList());
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
            Denuncias den = new Denuncias();
            den.DesaparecidoId = Convert.ToInt32(id);
            return View(den);
        }

        // POST: Denuncias/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Descricao,DataDenuncia,DesaparecidoId")] Denuncias denuncias)
        {
            denuncias.DataDenuncia = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Denuncias.Add(denuncias);
                db.SaveChanges();
                TempData["MSG"] = "success|Em breve um administrador estará revisando sua denúncia e tomará as decisões cabíveis";
                return RedirectToAction("ListOneDesaparecido", "Desaparecido", new { id = denuncias.DesaparecidoId });
            }

            ViewBag.DesaparecidoId = new SelectList(db.Desaparecido, "Id", "Id", denuncias.DesaparecidoId);
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
            ViewBag.DesaparecidoId = new SelectList(db.Desaparecido, "Id", "Id", denuncias.DesaparecidoId);
            return View(denuncias);
        }

        // POST: Denuncias/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Descricao,DataDenuncia,DesaparecidoId")] Denuncias denuncias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(denuncias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesaparecidoId = new SelectList(db.Desaparecido, "Id", "Id", denuncias.DesaparecidoId);
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
