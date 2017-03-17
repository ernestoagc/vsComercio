using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessEntity;
using System.ServiceModel.Web;

namespace ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IBancoService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IBancoService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "OrdenPago/{pTipoMoneda}", ResponseFormat = WebMessageFormat.Json)]
        List<OrdenPagoBE> GetOrdenesPago(string pTipoMoneda);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Sucursal/{pBancoNombre}", ResponseFormat = WebMessageFormat.Xml)]
        List<SucursalBE> GetSucursales(string pBancoNombre);
    }
}
