using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dory2.Models;

namespace Dory2.Controllers
{
    public class VulneravelsController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Vulneravels
        public ActionResult Index()
        {
            var vulneravel = db.Vulneravel.Include(v => v.Pessoa);
            return View(vulneravel.ToList());
        }

        // GET: Vulneravels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vulneravel vulneravel = db.Vulneravel.Find(id);
            if (vulneravel == null)
            {
                return HttpNotFound();
            }
            return View(vulneravel);
        }

        // GET: Vulneravels/Create
        public ActionResult Create()
        {
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Cpf");
            return View();
        }

        // POST: Vulneravels/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Observacoes,Status,PessoaId")] Vulneravel vulneravel)
        {
            if (ModelState.IsValid)
            {
                db.Vulneravel.Add(vulneravel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Cpf", vulneravel.PessoaId);
            return View(vulneravel);
        }

        // GET: Vulneravels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vulneravel vulneravel = db.Vulneravel.Find(id);
            if (vulneravel == null)
            {
                return HttpNotFound();
            }
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Cpf", vulneravel.PessoaId);
            return View(vulneravel);
        }

        // POST: Vulneravels/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Observacoes,Status,PessoaId")] Vulneravel vulneravel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vulneravel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Cpf", vulneravel.PessoaId);
            return View(vulneravel);
        }

        // GET: Vulneravels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vulneravel vulneravel = db.Vulneravel.Find(id);
            if (vulneravel == null)
            {
                return HttpNotFound();
            }
            return View(vulneravel);
        }

        // POST: Vulneravels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vulneravel vulneravel = db.Vulneravel.Find(id);
            db.Vulneravel.Remove(vulneravel);
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


        public ActionResult InitialRegisterVulneravel()
        {
            int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
            Responsavel res = db.Responsavel.Where(x => x.Id == resId).ToList().FirstOrDefault();
            if (res.Pessoa.Cpf == null)
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
        public ActionResult InitialRegisterVulneravel(InitialRegisterVulneravel cad)
        {
            if (ModelState.IsValid)
            {
                string name = cad.Nome + " " + cad.Sobrenome;
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias tut = new Tutorias();
                tut.Ativo = false;
                tut.IsVulneravel = true;

                tut.Cadastro = DateTime.Now;
                tut.Responsavel = db.Responsavel.Find(resId);
                tut.Pessoa = new Pessoa();

                tut.Pessoa.Nome = name;
                tut.Pessoa.DataNascimento = cad.DataNascimento;
                tut.Pessoa.Sexo = Convert.ToString(cad.Sexo);
                tut.Pessoa.Cutis = Convert.ToString(cad.Cutis);

                db.Tutorias.Add(tut);
                db.SaveChanges();

                Vulneravel vul = new Vulneravel();
                vul.Pessoa = tut.Pessoa;
                vul.Status = false;
                vul.Observacoes = "Nenhuma";

                //des.VulneravelId = 0; 

                db.Vulneravel.Add(vul);
                db.SaveChanges();

                Mais_infos infos = new Mais_infos();
                infos.Olhos = cad.CorOlhos;
                infos.Cabelo = cad.CorCabelo;
                infos.Altura = Convert.ToDecimal(cad.Altura);
                infos.Peso = Convert.ToDecimal(cad.Peso);
                infos.TipoSanguineo = Convert.ToString(cad.TipoSanguineo);
                infos.Descricao = cad.Descricao;
                infos.Vulneravel = vul;
                infos.DesaparecidoId = null;

                db.Mais_Infos.Add(infos);
                db.SaveChanges();

                return RedirectToAction("FinalRegisterVulneravel", "Vulneravels", new { id = infos.Id });
            }
            return View();
        }


        public ActionResult FinalRegisterVulneravel(int id)
        {
            FinalRegisterVulneravel register = new FinalRegisterVulneravel();
            register.codigo = id;
            return View(register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalRegisterVulneravel(FinalRegisterVulneravel cad)
        {
            if (ModelState.IsValid)
            {
                Mais_infos inf = db.Mais_Infos.Find(cad.codigo);
                if (cad.deficienciaFisicaRadio == "yes")
                {
                    if (cad.deficienciaFisicaText != null)
                    {
                        inf.DeficienciaFisica = cad.deficienciaFisicaText;
                    }
                    else
                    {
                        inf.DeficienciaFisica = "Tem porem não foi informado";
                    }
                }
                else
                {
                    inf.DeficienciaFisica = "Não tem ou não foi informado";
                }


                if (cad.deficienciaMentalRadio == "yes")
                {
                    if (cad.deficienciaMentalText != null)
                    {
                        inf.DeficienciaMental = cad.deficienciaMentalText;
                    }
                    else
                    {
                        inf.DeficienciaMental = "Tem porem não foi informado";
                    }
                }
                else
                {
                    inf.DeficienciaMental = "Não tem ou não foi informado";
                }

                if (cad.doencaRadio == "yes")
                {
                    if (cad.doencaText != null)
                    {
                        inf.Doencas = cad.doencaText;
                    }
                    else
                    {
                        inf.Doencas = "Tem porem não foi informado";
                    }
                }
                else
                {
                    inf.Doencas = "Não tem ou não foi informado";
                }

                if (cad.restricaoAlimentarRadio == "yes")
                {
                    if (cad.restricaoAlimentarText != null)
                    {
                        inf.RestricaoAlimentar = cad.restricaoAlimentarText;
                    }
                    else
                    {
                        inf.RestricaoAlimentar = "Tem porem não foi informado";
                    }
                }
                else
                {
                    inf.RestricaoAlimentar = "Não tem ou não foi informado";
                }

                if (cad.restricaoMedicamentosRadio == "yes")
                {
                    if (cad.restricaoMedicamentosText != null)
                    {
                        inf.RestricaoMedicamentos = cad.restricaoMedicamentosText;
                    }
                    else
                    {
                        inf.RestricaoMedicamentos = "Tem porem não foi informado";
                    }
                }
                else
                {
                    inf.RestricaoMedicamentos = "Não tem ou não foi informado";
                }

                db.SaveChanges();

                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        public ActionResult ListMeusVulneraveis()
        {
            int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
            List<Tutorias> infos = db.Tutorias.Where(x => x.ResponsavelId == resId && x.IsVulneravel == true).ToList();
            List<Galeria> gal = db.Galeria.ToList();
            ViewBag.FotosPerfil = gal.ToArray();
            return View(infos);
        }

        public ActionResult ListOneVulneravel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorias tut = db.Tutorias.Find(id);

            if (User.Identity.IsAuthenticated)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias desTut = db.Tutorias.Find(id);
                Tutorias validation = db.Tutorias.Where(x => x.ResponsavelId == resId && x.PessoaId == desTut.PessoaId).ToList().FirstOrDefault();
                if (validation == null)
                {
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse Vulneravel";
                    return RedirectToAction("Index", "Home");
                }
            }

            // calculo da idade do responsavel
            DateTime dataInicial = tut.Pessoa.DataNascimento;
            DateTime dataFinal = DateTime.Now;
            int ano = dataFinal.Year;
            int anoInicial = dataInicial.Year;
            int idade = ano - anoInicial;
            ViewBag.Idade = idade;

            Galeria galeria = db.Galeria.Where(x => x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
            if (galeria != null)
            {
                ViewBag.FotoPerfil = galeria.Foto;
            }

            Vulneravel vul = db.Vulneravel.Where(x => x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
            Mais_infos infos = db.Mais_Infos.Where(x => x.VulneravelId == vul.Id).ToList().FirstOrDefault();

            ViewBag.IsResponsavel = false;

            if (Request.Cookies.Get("userId") != null)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                if (tut.ResponsavelId == resId)
                {
                    ViewBag.IsResponsavel = true;
                }
            }

            ViewBag.Mental = infos.DeficienciaMental;
            ViewBag.Fisico = infos.DeficienciaFisica;
            ViewBag.Doencas = infos.Doencas;
            ViewBag.Comidas = infos.RestricaoAlimentar;
            ViewBag.Medicamentos = infos.RestricaoMedicamentos;
            ViewBag.VulneravelId = vul.Id;
            ViewBag.Sangue = infos.TipoSanguineo;
            ViewBag.CorCabelo = infos.Cabelo;
            ViewBag.CorOlhos = infos.Olhos;
            ViewBag.Altura = infos.Altura;
            ViewBag.Peso = infos.Peso;
            ViewBag.Descricao = infos.Descricao;

            return View(tut);
        }

        public ActionResult UploadFotoPerfil(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias desTut = db.Tutorias.Find(id);
                Tutorias validation = db.Tutorias.Where(x => x.ResponsavelId == resId && x.PessoaId == desTut.PessoaId).ToList().FirstOrDefault();
                if (validation == null)
                {
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse vulnerável";
                    return RedirectToAction("Index", "Home");
                }

                return View();
            }

            TempData["MSG"] = "warning|Logue antes de tentar editar esse vulnerável";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFotoPerfil(UploadFoto upl, HttpPostedFileBase arq, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tutorias tut = db.Tutorias.Find(id);
            Vulneravel vul = db.Vulneravel.Where(x => x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
            string valor = "";
            upl.PessoaId = vul.PessoaId;

            if (ModelState.IsValid)
            {
                var resFoto = db.Galeria.Where(x => x.PessoaId == vul.PessoaId).ToList().FirstOrDefault();
                if (arq != null)
                {
                    Upload.CriarDiretorio();
                    string nomearq = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(arq.FileName);
                    valor = Upload.UploadArquivo(arq, nomearq);
                    if (valor == "sucesso")
                    {

                        Galeria gal = new Galeria();
                        gal.Foto = nomearq;
                        gal.PessoaId = vul.PessoaId;
                        db.Galeria.Add(gal);
                        if (resFoto != null)
                        {
                            Upload.ExcluirArquivo(Request.PhysicalApplicationPath + "Uploads\\" + resFoto.Foto);
                            db.Galeria.Remove(resFoto);
                        }
                        db.SaveChanges();
                        return RedirectToAction("ListOneVulneravel/" + tut.Id, "Vulneravels");
                    }
                    else
                    {
                        ModelState.AddModelError("", valor);
                        TempData["MSG"] = "warning|Ops! Algo deu errado";
                        return View();
                    }
                }
                else
                {
                    TempData["MSG"] = "warning|Selecione uma imagem para seu perfil";
                    return View();
                }

            }
            TempData["MSG"] = "warning|Preencha todos os campos";
            return View();
        }

        public ActionResult DesaparecimentoVulneravel(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {

                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias tut = db.Tutorias.Find(id);
                Tutorias validation = db.Tutorias.Where(x => x.ResponsavelId == resId && x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
                if (validation == null)
                {
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse vulnerável";
                    return RedirectToAction("Index", "Home");
                }

                tut.Ativo = true;

                Desaparecido des = new Desaparecido();
                des.Pessoa = tut.Pessoa;
                db.Desaparecido.Add(des);

                Vulneravel vul = db.Vulneravel.Where(x => x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
                vul.Status = true;

                Mais_infos min = db.Mais_Infos.Where(x => x.VulneravelId == vul.Id).ToList().FirstOrDefault();
                min.Desaparecido = des;

                db.SaveChanges();

                return RedirectToAction("ListMeusVulneraveis", "Vulneravels");
            }
            TempData["MSG"] = "warning|Logue antes de tentar alterar esse Vulneravel";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult VulneravelEncontrado(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias tut = db.Tutorias.Find(id);
                Tutorias validation = db.Tutorias.Where(x => x.ResponsavelId == resId && x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
                if (validation == null)
                {
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse vulnerável";
                    return RedirectToAction("Index", "Home");
                }

                Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().LastOrDefault();

                des.Encontrado = DateTime.Now;
                tut.Ativo = false;

                Mais_infos min = db.Mais_Infos.Where(x => x.DesaparecidoId == des.Id).ToList().FirstOrDefault();
                min.DesaparecidoId = null;

                Vulneravel vul = db.Vulneravel.Where(x => x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
                vul.Status = false;

                db.SaveChanges();

                return RedirectToAction("ListMeusVulneraveis", "Vulneravels");
            }
            TempData["MSG"] = "warning|Logue antes de tentar alterar esse vulnerável";
            return RedirectToAction("Index", "Home");
        }
    }
}
