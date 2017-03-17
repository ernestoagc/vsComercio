using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    [Serializable]
    public class OrdenPagoBE
    {
        public OrdenPagoBE() {
            FechaPago = DateTime.Now;
            FechaPagoString = DateTime.Now.ToShortDateString();
        }

        public int Id { get; set; }
        public SucursalBE Sucursal { get; set; }
        public MonedaBE Moneda { get; set; }
        public EstadoBE Estado { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string FechaPagoString { get; set; }


        public List<MonedaBE> CfgMonedas { get; set; }
        public List<EstadoBE> CfgEstados { get; set; }
        public List<SucursalBE> CfgSucursals { get; set; }

        public class Criterio {
            public string CodigoMoneda { get; set; }
            public int SucursalId { get; set; }
        }
    }
}
