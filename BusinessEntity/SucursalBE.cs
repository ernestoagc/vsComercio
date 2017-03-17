using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class SucursalBE
    {
        public SucursalBE(){
            FechaRegistro = DateTime.Now;
            FechaRegistroString = DateTime.Now.ToShortDateString();
            Banco = new BancoBE();
            }

        public int Id { get; set; }
        public BancoBE Banco { get; set; }
        public string Direccion { get; set; }
        public string NombreCombo { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroString { get; set; }
        public List<BancoBE> CfgBancos { get; set; }

        public class Criterio {
            public string BancoNombre { get; set; }
            public int BancoId { get; set; }
        }
    }
}
