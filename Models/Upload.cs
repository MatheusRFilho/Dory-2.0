using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Upload
    {
        public static bool CriarDiretorio()
        {
            string dir = HttpContext.Current.Request.PhysicalApplicationPath + "Uploads\\";
            if (!Directory.Exists(dir))
            {
                //Caso não exista devermos criar
                Directory.CreateDirectory(dir);
                return true;
            }
            else
                return false;
        }
        public static bool ExcluirArquivo(string arq)
        {
            if (File.Exists(arq))
            {
                File.Delete(arq);
                return true;
            }
            else
                return false;
        }
        public static string UploadArquivo(HttpPostedFileBase flpUpload, string nome)
        {
            try
            {
                double permitido = 900;
                if (flpUpload != null)
                {
                    string arq = Path.GetFileName(flpUpload.FileName);
                    double tamanho = Convert.ToDouble(flpUpload.ContentLength) / 1024;
                    string extensao = Path.GetExtension(flpUpload.FileName).ToLower();
                    string diretorio = HttpContext.Current.Request.PhysicalApplicationPath + "Uploads\\" + nome;
                    if (tamanho > permitido)
                        return "Tamanho Máximo permitido é de " + permitido + " kb!";
                    else if ((extensao != ".png" && extensao != ".jpg"))
                        return "Extensão inválida, só são permitidas .png e .jpg!";
                    else
                    {
                        if (!File.Exists(diretorio))
                        {
                            flpUpload.SaveAs(diretorio);
                            return "sucesso";
                        }
                        else
                            return "Já existe um arquivo com esse nome!";
                    }
                }
                else
                    return "Erro no Upload!";
            }
            catch { return "Erro no Upload"; }
        }
    }
}