using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessEntity;
using DataAccess;

namespace UIWEB.Controllers
{
    public class OrdenPagoController : Controller
    {
        BancoDA oBancoDA = new BancoDA();
        SucursalDA oSucursalDA = new SucursalDA();
        MonedaDA oMonedaDA = new MonedaDA();
        EstadoDA oEstadoDA = new EstadoDA();
        OrdenPagoDA oOrdenPagoDA = new OrdenPagoDA();
        // GET: OrdenPago
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetOrdenPagos()
        {
            List<OrdenPagoBE> listaOrdenPagoes = oOrdenPagoDA.Get(null);
            return Json(listaOrdenPagoes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrdenPagoNuevo()
        {

            return Json(OrdenPagoNuevo(), JsonRequestBehavior.AllowGet);
        }

        private OrdenPagoBE OrdenPagoNuevo()
        {
            OrdenPagoBE oOrdenPagoBE = new OrdenPagoBE();
            MonedaBE oMonedaBE = new MonedaBE();
            SucursalBE oSucursalBE = new SucursalBE();
            EstadoBE oEstadoBE = new EstadoBE();

            oMonedaBE.Nombre = "Seleccione";
            oMonedaBE.Id = 0;

            oSucursalBE.NombreCombo = "Seleccione";
            oSucursalBE.Id = 0;

            oEstadoBE.Nombre = "Seleccione";
            oEstadoBE.Id = 0;

            oOrdenPagoBE.CfgMonedas = oMonedaDA.Get();
            oOrdenPagoBE.CfgSucursals = oSucursalDA.Get(null);
            oOrdenPagoBE.CfgEstados = oEstadoDA.Get();

            oOrdenPagoBE.CfgSucursals.Insert(0, oSucursalBE);
            oOrdenPagoBE.CfgMonedas.Insert(0, oMonedaBE);
            oOrdenPagoBE.CfgEstados.Insert(0, oEstadoBE);
            return oOrdenPagoBE;
        }


        [HttpPost]
        public JsonResult Delete(OrdenPagoBE pOrdenPagoBE)
        {
            oOrdenPagoDA.Delete(pOrdenPagoBE);
            return Json("OK");

        }

        [HttpPost]
        public JsonResult Insert(OrdenPagoBE pOrdenPagoBE)
        {
            if (pOrdenPagoBE.Sucursal.Id == 0)
            {
                return Json(new { success = false, message = "Seleccionar una sucursal" }, JsonRequestBehavior.AllowGet);
            }

            if (pOrdenPagoBE.Moneda.Id == 0)
            {
                return Json(new { success = false, message = "Seleccionar un tipo de moneda" }, JsonRequestBehavior.AllowGet);
            }

            if (pOrdenPagoBE.Estado.Id == 0)
            {
                return Json(new { success = false, message = "Seleccionar un estado" }, JsonRequestBehavior.AllowGet);
            }

           
            if (pOrdenPagoBE.Estado.Id == 0)
            {
                return Json(new { success = false, message = "Seleccionar un estado" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                pOrdenPagoBE.FechaPago = Convert.ToDateTime(pOrdenPagoBE.FechaPagoString);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "La fecha de pago no tiene el formato correcto" }, JsonRequestBehavior.AllowGet);

            }

            OrdenPagoBE oOrdenPagoBE = oOrdenPagoDA.Insert(pOrdenPagoBE);
            return Json(new { success = true, message = "OK", obj = oOrdenPagoBE });

        }
    }
}