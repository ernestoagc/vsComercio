using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessEntity;
namespace Consola
{
    class Program
    {


        static void Main(string[] args)
        {

          
          
                Console.WriteLine("Que problema desea resolver: ");
                Console.WriteLine("1. Problema (ChangeString.cs) ");
                Console.WriteLine("2. Problema (CompleteRange.cs) ");
                Console.WriteLine("3. Problema (MoneyParts.cs) ");
                Console.Write("ingresar opcion: ");
                string opcion= Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("------PROBLEMA 1------");
                        Console.Write("Ingresar cadena de entrada: ");
                        string Entrada1 = Console.ReadLine();
                        string Salida1 = ChangeString.build(Entrada1);
                        Console.WriteLine("Cadena modificada es:  " + Salida1);
                        break;
                    case "2":
                         Console.Clear();
                        Console.WriteLine("------PROBLEMA 2------");
                        Console.Write("Ingresar arreglo de entrada (ejemplo: 1,2,3,4,5 ): ");
                        string Entrada2 = Console.ReadLine();
                         var ArregloString = Entrada2.Split(',');
                        int[] Arreglo = Array.ConvertAll(ArregloString, o => int.Parse(o));


                        int[] ArregloSalida = CompleteRange.build(Arreglo);
                        Console.Write("El arreglo ordenado es:  ");

                        for (int i = 0; i < ArregloSalida.Length; i++)
                        {
                            Console.Write(ArregloSalida[i]+", ");
                        }
                            break;

                    case "3":
                            Console.Clear();
                         Console.WriteLine("------PROBLEMA 3------");
                         Console.Write("Ingrese moneda a cambiar (valor numerico): ");
                       string entrada3=  Console.ReadLine();
                       decimal[] denominaciones = new decimal[] { 0.05m, 0.1m, 0.2m, 0.5m, 1, 2, 5, 10, 20, 50, 100, 200 };
                            List<List<Nodo>> soluciones = new List<List<Nodo>>();
                            decimal monto = Convert.ToDecimal(entrada3);
                            Nodo nodo = new Nodo();

                            MoneyParts oMonedaCalculo = new MoneyParts();
                            oMonedaCalculo.calcularMonto(denominaciones, monto, denominaciones.Count() - 1, nodo);
                            oMonedaCalculo.imprimir(nodo, "  ", soluciones);
                            break;

                }

            Console.ReadLine();



        }
    }
}
