using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessEntity;
using DataAccess;
using System.Net;
using System.ServiceModel.Web;

namespace UIWEB.Controllers
{
    public class SucursalController : Controller
    {
        SucursalDA oSucursalDA = new SucursalDA();
        BancoDA oBancoDA = new BancoDA();
        OrdenPagoDA oOrdenPagoDA = new OrdenPagoDA();
        private SucursalBE SucursalNuevo()
        {
            SucursalBE oSucursalBE = new SucursalBE();
            BancoBE oBancoBE = new BancoBE();
            oBancoBE.Nombre = "Seleccione";
            oBancoBE.Id = 0;

            oSucursalBE.CfgBancos = oBancoDA.Get();
            oSucursalBE.CfgBancos.Insert(0, oBancoBE);
        
            return oSucursalBE;
        }


       
        // GET: Sucursal
        public ActionResult Index()
        {
            return View();
        }
       

        public JsonResult GetSucursals()
        {
            List<SucursalBE> listaSucursales = oSucursalDA.Get(null);
            return Json(listaSucursales, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSucursalNuevo()
        {
           
            return Json(SucursalNuevo(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insert(SucursalBE pSucursalBE)
        {
            if (pSucursalBE.Banco.Id == 0) {
                return Json(new { success = false, message = "Seleccionar un Banco" },JsonRequestBehavior.AllowGet);
            }

            SucursalBE oSucursalBE = oSucursalDA.Insert(pSucursalBE);
            return Json(new { success = true, message = "OK", obj = oSucursalBE });

        }

        [HttpPost]
        public JsonResult Delete(SucursalBE pSucursalBE)
        {
            OrdenPagoBE.Criterio oCriterio = new OrdenPagoBE.Criterio();
            oCriterio.SucursalId = pSucursalBE.Id;
            var ltsResultado = oOrdenPagoDA.Get(oCriterio);

            if (ltsResultado.Count > 0)
                return Json(new { success = false, message = "No se puede eliminar la sucursal porque tiene ordenes de pagos asociadas." });

            oSucursalDA.Delete(pSucursalBE);
            return Json(new { success = true, message = "OK" });

        }
    }
}