using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Sena
{
    class Crypto
    {
        public string newHash(TextBox Tsenha)
        {
            //METODO UTILIZADO PARA GERAR O HASH

            byte[] hashSenha;

            string senha = Tsenha.Text;

            UnicodeEncoding ue = new UnicodeEncoding(); //utilizado na conversão de uma string em unicode byte

            //dados convertidos em byte
            //Estes dois exemplos corresponde ao mesmo resultado
            //byte[] senhaByte = Encoding.Unicode.GetBytes(senha);

            byte[] senhaByte = ue.GetBytes(senha);

            SHA512Managed sha512 = new SHA512Managed(); //classe utilzada para gerar o hash

            //hash gerado conforme dados convertidos em byte.

            hashSenha = sha512.ComputeHash(senhaByte);

            string resultSenha = "";

            foreach (byte bytesenha in hashSenha)
            {
                resultSenha += bytesenha.ToString("X2");
            }

            return resultSenha;


            /*
            Tipos de Hash : sha1, md5
            Tipode de Encoding : Unicode, UTF8, UTF7, UTF32
            X2 faz conversão de modo que fique em hex.
            */

        }
    }
}
