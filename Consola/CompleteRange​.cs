using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consola
{
    public static class CompleteRange​
    {
        public static int[] build(int[] pArreglo)
        {
            //-----solucion 2
            int[] ArregloNum = { 4, 15, 7, 2 };
            int maximo = pArreglo.Max(t => t);
            int[] devolver = new int[maximo];

            for (int i = 0; i < devolver.Length; i++)
            {
                devolver[i] = i + 1;
            }

            return devolver;
        }
    }
}
