using Dory2.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                tut.IsDeleted = false;
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

                TempData["MSG"] = "success|Cadastro efetuado com sucesso, o desaparecido já está visível no Dory e pode ser visualizado na sessão de Meus Desaparecidos";
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

                Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().LastOrDefault();
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

                switch (infos.TipoSanguineo)
                {
                    case "APositivo":
                        ViewBag.Sangue = "A+";
                        break;

                    case "ANegativo":
                        ViewBag.Sangue = "A-";
                        break;

                    case "ABPositivo":
                        ViewBag.Sangue = "AB+";
                        break;

                    case "ABNegativo":
                        ViewBag.Sangue = "AB-";
                        break;

                    case "OPositivo":
                        ViewBag.Sangue = "O+";
                        break;

                    case "ONegativo":
                        ViewBag.Sangue = "O-";
                        break;

                    case "BPositivo":
                        ViewBag.Sangue = "B+";
                        break;

                    case "BNegativo":
                        ViewBag.Sangue = "B-";
                        break;

                    default:
                        break;
                }

                ViewBag.Mental = infos.DeficienciaMental;
                ViewBag.Fisico = infos.DeficienciaFisica;
                ViewBag.Doencas = infos.Doencas;
                ViewBag.Comidas = infos.RestricaoAlimentar;
                ViewBag.Medicamentos = infos.RestricaoMedicamentos;
                ViewBag.DesaparecidoId = des.Id;
                
                ViewBag.CorCabelo = infos.Cabelo;
                ViewBag.CorOlhos = infos.Olhos;
                ViewBag.Altura = infos.Altura;
                ViewBag.Peso = infos.Peso;
                ViewBag.Descricao = infos.Descricao;

                List<Casos> cas = db.Casos.Where(x => x.DesaparecidoId == des.Id).ToList();
                ViewBag.Historico = cas;

                return View(tut);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ListDesaparecidoTest()
        {
            List<Tutorias> infos = db.Tutorias.ToList();
            List<Galeria> gal = db.Galeria.ToList();
            ViewBag.FotosPerfil = gal.ToArray();
            return View(infos);
        }

        public ActionResult ListMeusDesaparecidos()
        {
            int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
            List<Tutorias> infos = db.Tutorias.Where(x => x.ResponsavelId == resId && x.Ativo == true).ToList();
            List<Galeria> gal = db.Galeria.ToList();
            ViewBag.FotosPerfil = gal.ToArray();
            return View(infos);
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
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse desaparecido";
                    return RedirectToAction("Index", "Home");
                }

                return View();
            }

            TempData["MSG"] = "warning|Logue antes de tentar editar esse desaparecido";
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
            Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().LastOrDefault();
            string valor = "";
            upl.PessoaId = des.PessoaId;

            if (ModelState.IsValid)
            {
                var resFoto = db.Galeria.Where(x => x.PessoaId == des.PessoaId).ToList().FirstOrDefault();
                if (arq != null)
                {
                    Upload.CriarDiretorio();
                    string nomearq = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(arq.FileName);
                    valor = Upload.UploadArquivo(arq, nomearq);
                    if (valor == "sucesso")
                    {

                        Galeria gal = new Galeria();
                        gal.Foto = nomearq;
                        gal.PessoaId = des.PessoaId;
                        db.Galeria.Add(gal);
                        if (resFoto != null)
                        {
                            Upload.ExcluirArquivo(Request.PhysicalApplicationPath + "Uploads\\" + resFoto.Foto);
                            db.Galeria.Remove(resFoto);
                        }
                        db.SaveChanges();
                        return RedirectToAction("ListOneDesaparecido/" + tut.Id, "Desaparecido");
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

        public ActionResult EditarDadosPessoais(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias desTut = db.Tutorias.Find(id);
                Tutorias validation = db.Tutorias.Where(x=> x.ResponsavelId == resId && x.PessoaId == desTut.PessoaId).ToList().FirstOrDefault();
                if (validation == null)
                {
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse desaparecido";
                    return RedirectToAction("Index", "Home");
                }    

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                EditarInformacoesPessoais edt = new EditarInformacoesPessoais();
                Tutorias tut = db.Tutorias.Find(id);
                Pessoa pes = db.Pessoa.Find(tut.PessoaId);
                Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().LastOrDefault();
                Mais_infos min = db.Mais_Infos.Where(x => x.DesaparecidoId == des.Id).ToList().FirstOrDefault();

                edt.Altura = Convert.ToString(min.Altura);
                edt.CorCabelo = min.Cabelo;
                edt.CorOlhos = min.Olhos;
                edt.Cpf = pes.Cpf;

                switch (pes.Cutis)
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
            TempData["MSG"] = "warning|Logue antes de tentar editar esse desaparecido";
            return RedirectToAction("Index", "Home");
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
                    Tutorias tut = db.Tutorias.Where(x => x.PessoaId == pes.Id).ToList().FirstOrDefault();

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

                    return RedirectToAction("ListOneDesaparecido", "Desaparecido", new { id = tut.Id });
                }
            }
            TempData["MSG"] = "warning|Preencha todos os campos";
            return View(edt);
        }

        public ActionResult EditarMaisInfos(int? id)
        {

            if (User.Identity.IsAuthenticated)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias desTut = db.Tutorias.Find(id);
                Tutorias validation = db.Tutorias.Where(x => x.ResponsavelId == resId && x.PessoaId == desTut.PessoaId).ToList().FirstOrDefault();
                if (validation == null)
                {
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse desaparecido";
                    return RedirectToAction("Index", "Home");
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                FinalRegisterDesaparecido edt = new FinalRegisterDesaparecido();
                Tutorias tut = db.Tutorias.Find(id);
                Pessoa pes = db.Pessoa.Find(tut.PessoaId);
                Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().LastOrDefault();
                Mais_infos min = db.Mais_Infos.Where(x => x.DesaparecidoId == des.Id).ToList().FirstOrDefault();

                edt.codigo = des.Id;
                edt.deficienciaFisicaText = min.DeficienciaFisica;
                edt.deficienciaMentalText = min.DeficienciaMental;
                edt.doencaText = min.Doencas;
                edt.restricaoAlimentarText = min.RestricaoAlimentar;
                edt.restricaoMedicamentosText = min.RestricaoMedicamentos;

                if (min.DeficienciaFisica != "Não tem ou não foi informado")
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
            TempData["MSG"] = "warning|Logue antes de tentar editar esse desaparecido";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarMaisInfos(FinalRegisterDesaparecido edt)
        {
            if (ModelState.IsValid)
            {
                Mais_infos inf = db.Mais_Infos.Where(x => x.DesaparecidoId == edt.codigo).ToList().FirstOrDefault();
                Desaparecido des = db.Desaparecido.Find(edt.codigo);
                Tutorias tut = db.Tutorias.Where(x => x.PessoaId == des.PessoaId).ToList().FirstOrDefault();

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

                return RedirectToAction("ListOneDesaparecido", "Desaparecido", new { id = tut.Id });

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
            Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().LastOrDefault();

            vi.DesaparecidoId = des.Id;
            vi.DataVisto = DateTime.Now;

            if (Request.Cookies.Get("userId") != null)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Responsavel res = db.Responsavel.Find(resId);
                Pessoa pesR = db.Pessoa.Find(res.PessoaId);

                if(res != null)
                {
                    vi.Email = res.Email;
                    vi.Nome = pesR.Nome;
                    vi.Contato = res.Celular;
                }
            }

            return View(vi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViEstaPessoa(ViEssaPessoa vi)
        {
            if (ModelState.IsValid)
            {
                Casos cas = new Casos();

                cas.EmailQuemViu = vi.Email;
                cas.ContatoQuemViu = vi.Contato;
                cas.NomeQuemViu = vi.Nome;
                cas.UltimoHorarioVisto = vi.DataVisto;
                cas.DesaparecidoId = vi.DesaparecidoId;

                if(vi.CidadeVisto != null)
                {
                    cas.UltimaLocalizacao = vi.CidadeVisto;
                } else
                {
                    cas.UltimaLocalizacao = "Não informado";
                }

                if (vi.RoupaVisto != null)
                {
                    cas.UltimaRoupa = vi.RoupaVisto;
                }
                else
                {
                    cas.UltimaRoupa = "Não informado";
                }

                if (vi.LocalVisto != null)
                {
                    cas.UltimoLugarVisto = vi.LocalVisto;
                }
                else
                {
                    cas.UltimoLugarVisto = "Não informado";
                }

                if (vi.DescricaoVisto != null)
                {
                    cas.MaisInformacoes = vi.DescricaoVisto;
                }
                else
                {
                    cas.MaisInformacoes = "Não informado";
                }

                Desaparecido des = db.Desaparecido.Find(vi.DesaparecidoId);
                Tutorias tut = db.Tutorias.Where(x => x.PessoaId == des.PessoaId).ToList().FirstOrDefault();

                db.Casos.Add(cas);
                db.SaveChanges();
                return RedirectToAction("ListOneDesaparecido", "Desaparecido", new { id = tut.Id });
            }
            return View(vi);
        }

        public ActionResult DesaparecidoEncontrado(int? id)
        {
            if(User.Identity.IsAuthenticated)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias tut = db.Tutorias.Find(id);
                Tutorias validation = db.Tutorias.Where(x => x.ResponsavelId == resId && x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
                if (validation == null)
                {
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse desaparecido";
                    return RedirectToAction("Index", "Home");
                }

                Desaparecido des = db.Desaparecido.Where(x => x.PessoaId == tut.PessoaId).ToList().LastOrDefault();

                des.Encontrado = DateTime.Now;
                tut.Ativo = false;
                db.SaveChanges();

                return RedirectToAction("ListDesaparecidoTest", "Desaparecido");
            }
            TempData["MSG"] = "warning|Logue antes de tentar alterar esse desaparecido";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ExcluirDesaparecido(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias tut = db.Tutorias.Find(id);
                Tutorias validation = db.Tutorias.Where(x => x.ResponsavelId == resId && x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
                if (validation == null)
                {
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse desaparecido";
                    return RedirectToAction("Index", "Home");
                }

                tut.IsDeleted = true;
                db.SaveChanges();

                return RedirectToAction("ListMeusDesaparecidos", "Desaparecido");
            }
            TempData["MSG"] = "warning|Logue antes de tentar alterar esse desaparecido";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ReativarDesaparecido(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int resId = Convert.ToInt32(Request.Cookies.Get("userId").Value);
                Tutorias tut = db.Tutorias.Find(id);
                Tutorias validation = db.Tutorias.Where(x => x.ResponsavelId == resId && x.PessoaId == tut.PessoaId).ToList().FirstOrDefault();
                if (validation == null)
                {
                    TempData["MSG"] = "warning|Não foi você quem cadastrou esse desaparecido";
                    return RedirectToAction("Index", "Home");
                }

                tut.IsDeleted = false;
                db.SaveChanges();

                return RedirectToAction("ListMeusDesaparecidos", "Desaparecido");
            }
            TempData["MSG"] = "warning|Logue antes de tentar alterar esse desaparecido";
            return RedirectToAction("Index", "Home");
        }
    }

}