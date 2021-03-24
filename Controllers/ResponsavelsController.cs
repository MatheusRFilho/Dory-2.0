﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dory2.Models;

namespace Dory2.Controllers
{
    public class ResponsavelsController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Responsavels
        public ActionResult Index()
        {
            var responsavel = db.Responsavel.Include(r => r.Pessoa);
            return View(responsavel.ToList());
        }

        // GET: Responsavels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsavel responsavel = db.Responsavel.Find(id);
            if (responsavel == null)
            {
                return HttpNotFound();
            }
            return View(responsavel);
        }

        // GET: Responsavels/Create
        public ActionResult Create()
        {
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Cpf");
            return View();
        }

        // POST: Responsavels/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Senha,Celular,PessoaId")] Responsavel responsavel)
        {
            if (ModelState.IsValid)
            {
                db.Responsavel.Add(responsavel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Cpf", responsavel.PessoaId);
            return View(responsavel);
        }

        // GET: Responsavels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsavel responsavel = db.Responsavel.Find(id);
            if (responsavel == null)
            {
                return HttpNotFound();
            }
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Cpf", responsavel.PessoaId);
            return View(responsavel);
        }

        // POST: Responsavels/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Senha,Celular,PessoaId")] Responsavel responsavel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(responsavel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Cpf", responsavel.PessoaId);
            return View(responsavel);
        }

        // GET: Responsavels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsavel responsavel = db.Responsavel.Find(id);
            if (responsavel == null)
            {
                return HttpNotFound();
            }
            return View(responsavel);
        }

        // POST: Responsavels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Responsavel responsavel = db.Responsavel.Find(id);
            db.Responsavel.Remove(responsavel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Acesso()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Acesso(Acesso ace)
        {
            if (ModelState.IsValid)
            {
                string senhacrip = Funcoes.HashTexto(ace.Senha, "SHA512");
                Responsavel res = db.Responsavel.Where(x => x.Email == ace.Email && x.Senha == senhacrip).ToList().FirstOrDefault();

                if (res == null)
                {
                    ModelState.AddModelError("", "Email ou senha incorretos");
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(res.Email, false);

                    //string permissoes = "";
                    //foreach (UsuarioPerfil p in usu.UsuarioPerfil)
                    //    permissoes += p.Perfil.Descricao + ",";

                    //permissoes = permissoes.Substring(0, permissoes.Length - 1);
                    //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usu.Nome, DateTime.Now, DateTime.Now.AddMinutes(30), false, permissoes);
                    //string hash = FormsAuthentication.Encrypt(ticket);
                    //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    //Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
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
