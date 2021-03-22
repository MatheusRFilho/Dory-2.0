using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Dory2.Models
{
    public class Funcoes
    {
        /// Gera Hash SHA1, SHA256, SHA512
        public static string HashTexto(string texto, string nomeHash)
        {
            HashAlgorithm algoritmo = HashAlgorithm.Create(nomeHash);
            if (algoritmo == null)
            {
                throw new ArgumentException("Nome de hash incorreto", "nomeHash");
            }
            byte[] hash = algoritmo.ComputeHash(Encoding.UTF8.GetBytes(texto));
            return Convert.ToBase64String(hash);
        }
    }
}