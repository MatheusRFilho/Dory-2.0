using System;
using System.Collections.Generic;
using System.Configuration;
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
                    HttpCookie cookie = new HttpCookie("loginData", res.Pessoa.Nome);
                    Response.Cookies.Add(cookie);

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
            // Apagar cookie
            HttpCookie cookie = Request.Cookies["loginData"];
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(Cadastro cad)
        {
            if (ModelState.IsValid)
            {
                if (db.Responsavel.Where( x=> x.Email == cad.Email).ToList().Count > 0 )
                {
                    ModelState.AddModelError("Email", "Email já cadastrado");
                    return View();
                } else if (db.Responsavel.Where(x => x.Celular == cad.Celular).ToList().Count > 0)
                {
                    ModelState.AddModelError("Celular", "Celular já cadastrado");
                    return View();
                } else
                {
                    string NomeCompleto = cad.Nome + " " + cad.Sobrenome;

                    Responsavel res = new Responsavel();
                    res.Email = cad.Email;
                    res.Celular = cad.Celular;
                    res.Senha = Funcoes.HashTexto(cad.Senha, "SHA512");
                    res.Pessoa = new Pessoa();
                    res.Pessoa.Nome = NomeCompleto;
                    res.Pessoa.DataNascimento = cad.DataNascimento;
                    res.Pessoa.Tipo = "Comum";

                    db.Responsavel.Add(res);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult EsqueceuSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EsqueceuSenha(EsqueceuSenha esq)
        {
            if (ModelState.IsValid)
            {
                var res = db.Responsavel.Where(x => x.Email == esq.Email).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Hash = Funcoes.Codifica(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                    db.Entry(res).State = EntityState.Modified;
                    db.SaveChanges();
                    string msg = "<h3>Redefinir Senha</h3>";
                    msg += "Para alterar sua senha <a href='" + ConfigurationManager.AppSettings["URLSite"] + "/Responsavels/Redefinir/" + res.Hash + "' target='_blank'>clique aqui</a>";
                    Funcoes.EnviarEmail(res.Email, "Redefinição de senha", msg);
                    TempData["MSG"] = "success|Email enviado com sucesso!";
                    return RedirectToAction("Acesso");
                }
                TempData["MSG"] = "error|E-mail não encontrado";
                return View();
            }
            TempData["MSG"] = "warning|Preencha todos os campos";
            return View();
        }

        public ActionResult Redefinir(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var res = db.Responsavel.Where(x => x.Hash == id).ToList().FirstOrDefault();
                if (res != null)
                {
                    try
                    {
                        DateTime dt = Convert.ToDateTime(Funcoes.Decodifica(res.Hash));
                        if (dt > DateTime.Now)
                        {
                            RedefinirSenha red = new RedefinirSenha();
                            red.Hash = res.Hash;
                            red.Email = res.Email;
                            return View(red);
                        }
                        TempData["MSG"] = "warning|Esse link já expirou!";
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        TempData["MSG"] = "error|Hash inválida!";
                        return RedirectToAction("Index");
                    }
                }
                TempData["MSG"] = "error|Hash inválida!";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "error|Acesso inválido!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Redefinir(RedefinirSenha red)
        {
            if (ModelState.IsValid)
            {
                var res = db.Responsavel.Where(x => x.Hash == red.Hash).ToList().FirstOrDefault();
                if (res != null)
                {
                    res.Hash = null;
                    res.Senha = Funcoes.HashTexto(red.Senha, "SHA512");
                    db.Entry(res).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Senha redefinida com sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MSG"] = "error|E-mail não encontrado";
                return View(red);
            }
            TempData["MSG"] = "warning|Preencha todos os campos";
            return View(red);
        }
    }
}
