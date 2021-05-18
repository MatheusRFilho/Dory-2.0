using Dory2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if (ModelState.IsValid)
            {
                string name = cad.Nome + " " + cad.Sobrenome;
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias tut = new Tutorias();
                tut.Ativo = true;
                tut.Cadastro = DateTime.Now;
                tut.Responsavel = db.Responsavel.Find(resId);
                tut.Pessoa = new Pessoa();

                tut.Pessoa.Nome = name;
                tut.Pessoa.DataNascimento = cad.DataNascimento;
                tut.Pessoa.Sexo = Convert.ToString(cad.Sexo);
                tut.Pessoa.Cutis = Convert.ToString(cad.Cutis);

                db.Tutorias.Add(tut);
                db.SaveChanges();

                Desaparecido des = new Desaparecido();
                des.Pessoa = tut.Pessoa;
                //des.VulneravelId = 0; 

                db.Desaparecido.Add(des);
                db.SaveChanges();

                Mais_infos infos = new Mais_infos();
                infos.Olhos = cad.CorOlhos;
                infos.Cabelo = cad.CorCabelo;
                infos.Altura = Convert.ToDecimal(cad.Altura);
                infos.Peso = Convert.ToDecimal(cad.Peso);
                infos.TipoSanguineo = Convert.ToString(cad.TipoSanguineo);
                infos.Descricao = cad.Descricao;
                infos.Desaparecido = des;
                infos.VulneravelId = null;

                db.Mais_Infos.Add(infos);
                db.SaveChanges();

                return RedirectToAction("FinalRegisterDesaparecido", "Desaparecido", new { id = infos.Id });
            }
            return View();
        }


        public ActionResult FinalRegisterDesaparecido(int id)
        {
            FinalRegisterDesaparecido register = new FinalRegisterDesaparecido();
            register.codigo = id;
            return View(register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalRegisterDesaparecido(FinalRegisterDesaparecido cad)
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
                } else
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

                return RedirectToAction("ConfirmationRegisterDesaparecido", "Desaparecido", new { id = inf.Id });

            }
            return View();
        }

        public ActionResult ConfirmationRegisterDesaparecido(int id)
        {
            ConfirmationRegisterDesaparecido register = new ConfirmationRegisterDesaparecido();
            int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
            Responsavel res = db.Responsavel.Find(resId);
            Pessoa pes = db.Pessoa.Find(res.Id);

            register.SeuCPF = pes.Cpf;
            register.SeuRG = pes.Rg;
            register.codigo = id;
            return View(register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmationRegisterDesaparecido(ConfirmationRegisterDesaparecido cad)
        {

            if (ModelState.IsValid)
            {
                Mais_infos infos = db.Mais_Infos.Find(cad.codigo);
                Desaparecido des = db.Desaparecido.Find(infos.DesaparecidoId);
                Pessoa pes = db.Pessoa.Find(des.PessoaId);

                pes.Cpf = cad.CpfDesaparecido;
                pes.Rg = cad.RgDesaparecido;

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ListOneDesaparecido(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorias tut = db.Tutorias.Find(id);
            if (tut.Ativo)
            {
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

                Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
                Mais_infos infos = db.Mais_Infos.Find(des.Id);

                ViewBag.IsResponsavel = false;

                if(Request.Cookies.Get("userId") != null)
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
                ViewBag.DesaparecidoId = des.Id;


                return View(tut);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ListDesaparecidoTest()
        {
            List<Tutorias> infos = db.Tutorias.ToList();
            return View(infos);
        }

        public ActionResult ListMeusDesaparecidos()
        {
            int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
            List<Tutorias> infos = db.Tutorias.Where(x => x.ResponsavelId == resId).ToList();
            return View(infos);
        }

        public ActionResult EditarDadosPessoais(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditarInformacoesPessoais edt = new EditarInformacoesPessoais();
            Desaparecido des = db.Desaparecido.Find(id);
            Pessoa pes = db.Pessoa.Find(des.PessoaId);
            Mais_infos min = db.Mais_Infos.Where(x => x.DesaparecidoId == des.Id).ToList().FirstOrDefault();

            edt.Altura = min.Altura;
            edt.CorCabelo = min.Cabelo;
            edt.CorOlhos = min.Olhos;
            edt.Cpf = pes.Cpf;
            //edt.Cutis = new SelectList(EditarInformacoesPessoais.Etinias);
            //edt.TipoSanguineo = new SelectList(EditarInformacoesPessoais.TipoSanguineos);
            //edt.Sexo 
            edt.DataNascimento = pes.DataNascimento;
            edt.Descricao = min.Descricao;
            edt.Nome = pes.Nome;
            edt.Peso = min.Peso;
            edt.Rg = pes.Rg;
            return View(edt);
        }
    }
}