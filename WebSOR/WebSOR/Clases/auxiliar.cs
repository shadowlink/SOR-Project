using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebSOR
{
    class Auxiliar
    {
        public string CalculateSha1(string text, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSha1 =
            new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(
                cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }
    }
}
