using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BusinessEntity;
using DataAccess;

namespace ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "BancoService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione BancoService.svc o BancoService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class BancoService : IBancoService
    {

        OrdenPagoDA ordenPagoDA = new OrdenPagoDA();
        SucursalDA sucursalDA = new SucursalDA();

        public List<OrdenPagoBE> GetOrdenesPago(string pTipoMoneda)
        {
            OrdenPagoBE.Criterio Criterio = new OrdenPagoBE.Criterio();
            Criterio.CodigoMoneda = pTipoMoneda;
            List<OrdenPagoBE> ltsResultado = ordenPagoDA.Get(Criterio);
            return ltsResultado;
        }

        public List<SucursalBE> GetSucursales(string pBancoNombre)
        {
            SucursalBE.Criterio criterio = new SucursalBE.Criterio();
            criterio.BancoNombre = pBancoNombre;
            List<SucursalBE> ltsResultado = sucursalDA.Get(criterio);
            return ltsResultado;
        }
    }
}
