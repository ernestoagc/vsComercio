using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessEntity;
using DataAccess;


namespace UIWEB.Controllers
{
    public class BancoController : Controller
    {
        BancoDA oBancoDA = new BancoDA();
        SucursalDA oSucursalDA = new SucursalDA();

        // GET: Banco
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetBancos()
        {
            List<BancoBE> listaBancos = oBancoDA.Get();
            return Json(listaBancos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBancoNuevo()
        {
            BancoBE oBancoNuevoBE = new BancoBE();
            return Json(oBancoNuevoBE, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Insert(BancoBE pBancoBE)
        {
            BancoBE oBancoBE = oBancoDA.Insert(pBancoBE);
            return Json(new { success = true, message = "OK", obj = pBancoBE });
        }

        [HttpPost]
        public JsonResult Delete(BancoBE pBancoBE)
        {
            SucursalBE.Criterio oCriterio= new SucursalBE.Criterio();
            oCriterio.BancoId=pBancoBE.Id;
            var ltsResultado= oSucursalDA.Get(oCriterio);

            if (ltsResultado.Count>0)
                return Json(new { success = false, message = "No se puede eliminar la banca porque tiene sucursales asociadas." });

            oBancoDA.Delete(pBancoBE);
            return Json(new { success = true, message = "OK" });


        }
    }
}