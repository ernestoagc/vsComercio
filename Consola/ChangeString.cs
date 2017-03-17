using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consola
{
    public static class ChangeString
    {

        public static string build(string pEntrada)
        {
            //-----solucion 1
            string Entrada = pEntrada;
            //string Entrada2 = "**Casa 52";
            string abc = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            abc = abc + abc.ToUpper();


            char[] carac = Entrada.ToCharArray();

            for (int i = 0; i < carac.Length; i++)
            {
                if (abc.Contains(carac[i]))
                    carac[i] += (char)1;
            }
            string final = new string(carac);
            return final;
        }
    }
}
