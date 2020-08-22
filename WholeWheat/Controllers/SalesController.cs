using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholeWheat.Helpers;
using WholeWheatRepository.Models;
using WholeWheatRepository.Repository;

namespace WholeWheat.Controllers
{
    [BaseController]
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaleDetail_PartialView(int SaleID)
        {
            var s = POSRepository.GetSalesDetails(SaleID).ToJson();
            return Json(new { Response = s }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Sales_PartialView()
        {
            List<ManageSale> MyList = SalesRepository.GetAllSales();
            return View("Sales_PartialView", MyList);
        }
        public ActionResult DeleteRecord(int SaleID)
        {
            bool success = true;
            string errorMessage = "";
            var s = SalesRepository.DeleteSale(SaleID).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }

    }
}