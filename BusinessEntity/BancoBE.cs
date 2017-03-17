using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    [Serializable]
    public class BancoBE
    {
        public BancoBE() {
            FechaRegistro = DateTime.Now;
            FechaRegistroString = FechaRegistro.ToShortDateString();
        }
        public int Id {get;set;}
        public string Nombre {get;set;}
        public string  Direccion {get;set;}
        public DateTime FechaRegistro { get; set; }

        public string FechaRegistroString { get; set; }
    }
}
