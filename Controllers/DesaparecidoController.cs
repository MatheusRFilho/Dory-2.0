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
                Mais_infos infos = db.Mais_Infos.Where(x => x.DesaparecidoId == des.Id).ToList().FirstOrDefault();

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
            List<Tutorias> infos = db.Tutorias.Where(x => x.ResponsavelId == resId && x.Ativo == true).ToList();
            return View(infos);
        }

        public ActionResult EditarDadosPessoais(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditarInformacoesPessoais edt = new EditarInformacoesPessoais();
            Tutorias tut = db.Tutorias.Find(id);
            Pessoa pes = db.Pessoa.Find(tut.PessoaId);
            Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
            Mais_infos min = db.Mais_Infos.Where(x => x.DesaparecidoId == des.Id).ToList().FirstOrDefault();

            edt.Altura = Convert.ToString(min.Altura);
            edt.CorCabelo = min.Cabelo;
            edt.CorOlhos = min.Olhos;
            edt.Cpf = pes.Cpf;

            switch(pes.Cutis)
            {
                case "Amarela":
                    edt.Cutis = EditarInformacoesPessoais.Etinias.Amarela;
                    break;

                case "Branca":
                    edt.Cutis = EditarInformacoesPessoais.Etinias.Branca;
                    break;

                case "Indigena":
                    edt.Cutis = EditarInformacoesPessoais.Etinias.Indígena;
                    break;

                case "Negra":
                    edt.Cutis = EditarInformacoesPessoais.Etinias.Negra;
                    break;

                case "Parda":
                    edt.Cutis = EditarInformacoesPessoais.Etinias.Parda;
                    break;

                default:
                    break;
            }

            switch (min.TipoSanguineo)
            {
                case "APositivo":
                    edt.TipoSanguineo = EditarInformacoesPessoais.TipoSanguineos.APositivo;
                    break;

                case "ANegativo":
                    edt.TipoSanguineo = EditarInformacoesPessoais.TipoSanguineos.ANegativo;
                    break;

                case "ABPositivo":
                    edt.TipoSanguineo = EditarInformacoesPessoais.TipoSanguineos.ABPositivo;
                    break;

                case "ABNegativo":
                    edt.TipoSanguineo = EditarInformacoesPessoais.TipoSanguineos.ABNegativo;
                    break;

                case "OPositivo":
                    edt.TipoSanguineo = EditarInformacoesPessoais.TipoSanguineos.OPositivo;
                    break;

                case "ONegativo":
                    edt.TipoSanguineo = EditarInformacoesPessoais.TipoSanguineos.ONegativo;
                    break;

                case "BPositivo":
                    edt.TipoSanguineo = EditarInformacoesPessoais.TipoSanguineos.BPositivo;
                    break;

                case "BNegativo":
                    edt.TipoSanguineo = EditarInformacoesPessoais.TipoSanguineos.BNegativo;
                    break;

                default:
                    break;
            }

            switch (pes.Sexo)
            {
                case "Masculino":
                    edt.Sexo = EditarInformacoesPessoais.Sexos.Masculino;
                    break;

                case "Feminino":
                    edt.Sexo = EditarInformacoesPessoais.Sexos.Feminino;
                    break;

                case "Outro":
                    edt.Sexo = EditarInformacoesPessoais.Sexos.Outro;
                    break;

                default:
                    break;
            }

            edt.DataNascimento = pes.DataNascimento;
            edt.Descricao = min.Descricao;
            edt.Nome = pes.Nome;
            edt.Peso = Convert.ToString(min.Peso);
            edt.Rg = pes.Rg;
            edt.Codigo = des.Id;
            return View(edt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDadosPessoais(EditarInformacoesPessoais edt)
        {
            if (ModelState.IsValid)
            {
                Desaparecido des = db.Desaparecido.Find(edt.Codigo);
                if(des != null)
                {
                    Pessoa pes = db.Pessoa.Find(des.PessoaId);

                    string auxRg = pes.Rg;
                    string auxCpf = pes.Cpf;
                    pes.Rg = "";
                    pes.Cpf = "";

                    db.SaveChanges();

                    if (db.Pessoa.Where(x => x.Cpf == edt.Cpf).ToList().Count > 0)
                    {
                        ModelState.AddModelError("Cpf", "CPF já cadastrado");
                        pes.Rg = auxRg;
                        pes.Cpf = auxCpf;
                        db.SaveChanges();
                        return View(edt);
                    }

                    if (db.Pessoa.Where(x => x.Rg == edt.Rg).ToList().Count > 0)
                    {
                        ModelState.AddModelError("Rg", "RG já cadastrado");
                        pes.Rg = auxRg;
                        pes.Cpf = auxCpf;
                        db.SaveChanges();
                        return View(edt);
                    }

                    pes.Nome = edt.Nome;
                    pes.Rg = edt.Rg;
                    pes.Cpf = edt.Cpf;
                    pes.DataNascimento = edt.DataNascimento;
                    pes.Sexo = Convert.ToString(edt.Sexo);
                    pes.Cutis = Convert.ToString(edt.Cutis);
                    db.SaveChanges();

                    Mais_infos min = db.Mais_Infos.Where(x => x.DesaparecidoId == des.Id).ToList().FirstOrDefault();
                    min.Altura = Convert.ToDecimal(edt.Altura);
                    min.Cabelo = edt.CorCabelo;
                    min.Descricao = edt.Descricao;
                    min.Olhos = edt.CorOlhos;
                    min.Peso = Convert.ToDecimal(edt.Peso);
                    min.TipoSanguineo = Convert.ToString(edt.TipoSanguineo);
                    db.SaveChanges();

                    return RedirectToAction("ListOneDesaparecido", "Desaparecido", new { id = des.Id });
                }
            }
            TempData["MSG"] = "warning|Preencha todos os campos";
            return View(edt);
        }

        public ActionResult EditarMaisInfos(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FinalRegisterDesaparecido edt = new FinalRegisterDesaparecido();
            Tutorias tut = db.Tutorias.Find(id);
            Pessoa pes = db.Pessoa.Find(tut.PessoaId);
            Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
            Mais_infos min = db.Mais_Infos.Where(x => x.DesaparecidoId == des.Id).ToList().FirstOrDefault();

            edt.codigo = des.Id;
            edt.deficienciaFisicaText = min.DeficienciaFisica;
            edt.deficienciaMentalText = min.DeficienciaMental;
            edt.doencaText = min.Doencas;
            edt.restricaoAlimentarText = min.RestricaoAlimentar;
            edt.restricaoMedicamentosText = min.RestricaoMedicamentos;
            
            if(min.DeficienciaFisica != "Não tem ou não foi informado")
            {
                edt.deficienciaFisicaRadio = "yes";
            }

            if (min.DeficienciaMental != "Não tem ou não foi informado")
            {
                edt.deficienciaMentalRadio = "yes";
            }

            if (min.Doencas != "Não tem ou não foi informado")
            {
                edt.doencaRadio = "yes";
            }

            if (min.RestricaoAlimentar != "Não tem ou não foi informado")
            {
                edt.restricaoAlimentarRadio = "yes";
            }

            if (min.RestricaoMedicamentos != "Não tem ou não foi informado")
            {
                edt.restricaoMedicamentosRadio = "yes";
            }

            return View(edt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarMaisInfos(FinalRegisterDesaparecido edt)
        {
            if (ModelState.IsValid)
            {
                Mais_infos inf = db.Mais_Infos.Where(x => x.DesaparecidoId == edt.codigo).ToList().FirstOrDefault();
                if (edt.deficienciaFisicaRadio == "yes")
                {
                    if (edt.deficienciaFisicaText != null)
                    {
                        inf.DeficienciaFisica = edt.deficienciaFisicaText;
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


                if (edt.deficienciaMentalRadio == "yes")
                {
                    if (edt.deficienciaMentalText != null)
                    {
                        inf.DeficienciaMental = edt.deficienciaMentalText;
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

                if (edt.doencaRadio == "yes")
                {
                    if (edt.doencaText != null)
                    {
                        inf.Doencas = edt.doencaText;
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

                if (edt.restricaoAlimentarRadio == "yes")
                {
                    if (edt.restricaoAlimentarText != null)
                    {
                        inf.RestricaoAlimentar = edt.restricaoAlimentarText;
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

                if (edt.restricaoMedicamentosRadio == "yes")
                {
                    if (edt.restricaoMedicamentosText != null)
                    {
                        inf.RestricaoMedicamentos = edt.restricaoMedicamentosText;
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

                return RedirectToAction("ListOneDesaparecido", "Desaparecido", new { id = edt.codigo });

            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViEstaPessoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViEssaPessoa vi = new ViEssaPessoa();
            Tutorias tut = db.Tutorias.Find(id);
            Pessoa pes = db.Pessoa.Find(tut.PessoaId);
            Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();

            if (Request.Cookies.Get("userId") != null)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Responsavel res = db.Responsavel.Find(resId);
                Pessoa pesR = db.Pessoa.Find(res.PessoaId);
                vi.DesaparecidoId = des.Id;

                if(res != null)
                {
                    vi.Email = res.Email;
                    vi.Nome = pesR.Nome;
                }
            }


            return View(vi);
        }
    }

}