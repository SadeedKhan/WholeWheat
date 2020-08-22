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
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            Reports MyList = ReportRepository.GetReportDetail();
            ViewBag.TotalCustomer = MyList.TotalCustomer;
            ViewBag.TotalMenu = MyList.TotalMenu;
            ViewBag.TotalSubMenu = MyList.TotalSubMenu;
            ViewBag.DailySale = MyList.DailySale;
            return View(new DropDowns());
        }
        [HttpPost]
        public JsonResult PieChartDetail()
        {
            var s = ReportRepository.GetPieChartDetail().ToJson();
            return Json(new { Response = s }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult MonthlyGraphDetail()
        {
            var s = ReportRepository.MonthlyGraphDetail().ToJson();
            return Json(new { Response = s }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult ProductDetail_PartialView(int ProductID,DateTime FromDate, DateTime EndDate)
        {
            List<ManageSaleDetail> MyList = ReportRepository.GetProductByDate( ProductID, FromDate, EndDate);
            return View("ProductDetail_PartialView", MyList);
        }
        [HttpPost]
        public ActionResult SaleTypeDetail_PartialView(int SaleTypeID, DateTime FromDate, DateTime EndDate)
        {
            List<ManageSale> MyList = ReportRepository.GetSaleTypeByDate(SaleTypeID, FromDate, EndDate);
            return View("SaleTypeDetail_PartialView", MyList);
        }
    }
}