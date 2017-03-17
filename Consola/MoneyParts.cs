using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consola
{
   public  class MoneyParts
    {


        public void imprimir(Nodo nodo, String sangria, List<List<Nodo>> soluciones)
        {
            if (nodo.getHijos().Count == 0)
            {
                Console.WriteLine("     Solucion");
            }

            foreach (Nodo hijo in nodo.getHijos())
            {
                Console.WriteLine("");
                Console.Write(sangria + hijo);
                imprimir(hijo, sangria + "  ", soluciones);
            }
        }

        public void calcularMonto(decimal[] denominaciones, decimal monto, int idDenominacionActual, Nodo padre)
        {
            if (idDenominacionActual < 0)
            {
                return;
            }
            if (monto - denominaciones[idDenominacionActual] == 0)
            {
                Nodo hijo = new Nodo();
                hijo.setMonto(monto);
                hijo.setResta(monto - denominaciones[idDenominacionActual]);
                padre.getHijos().Add(hijo);
                hijo.setDenominacion(denominaciones[idDenominacionActual]);
            }
            if (monto - denominaciones[idDenominacionActual] > 0)
            {
                Nodo hijo = new Nodo();
                hijo.setMonto(monto);
                hijo.setResta(monto - denominaciones[idDenominacionActual]);
                hijo.setDenominacion(denominaciones[idDenominacionActual]);
                padre.getHijos().Add(hijo);
                calcularMonto(denominaciones, monto - denominaciones[idDenominacionActual], idDenominacionActual, hijo);
            }
            calcularMonto(denominaciones, monto, idDenominacionActual - 1, padre);
        }

    }


   public class Nodo
   {
       decimal denominacion;
       private List<Nodo> hijos;
       decimal monto;
       private decimal resta;

       public Nodo()
       {
           setHijos(new List<Nodo>());
       }
       public decimal getDenominacion()
       {
           return denominacion;
       }
       public void setDenominacion(decimal denominacion)
       {
           this.denominacion = denominacion;
       }

       public decimal getMonto()
       {
           return monto;
       }
       public void setMonto(decimal monto)
       {
           this.monto = monto;
       }
       public List<Nodo> getHijos()
       {
           return hijos;
       }
       public void setHijos(List<Nodo> hijos)
       {
           this.hijos = hijos;
       }

       public decimal getResta()
       {
           return resta;
       }
       public void setResta(decimal resta)
       {
           this.resta = resta;
       }

       public override string ToString()
       {
           return "[moneda=" + denominacion + "]";
       }

   }


}
