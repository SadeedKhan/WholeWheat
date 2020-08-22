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
    public class POSController : Controller
    {
        // GET: POS
        public ActionResult Index()
        {
            return View(new DropDowns());
        }
        public ActionResult PrintBill()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaleDetail_PartialView(int SaleID)
        {
            List<ManageSaleDetail> MyList = POSRepository.GetSalesDetails(SaleID);
            return View("SaleDetail_PartialView", MyList);
        }
        public ActionResult SubMenuList_PartialView(int MenuID)
        {
            List<ManageSubMenu> MyList = SubMenuRepository.GetAllSubMenuList(MenuID);
            return View("SubMenuList_PartialView", MyList);
        }
        public ActionResult MenuList_PartialView(int StatusID)
        {
            List<ManageMenu> MyList = MenuRepository.GetMenu(StatusID);
            return View("MenuList_PartialView", MyList);
        }
        public ActionResult PendingSaleDetail_PartialView()
        {
            List<ManageSale> MyList = POSRepository.GetPendingSales();
            return View("PendingSaleDetail_PartialView", MyList);
        }
        public ActionResult TodayPendingSaleDetail_PartialView()
        {
            List<ManageSale> MyList = POSRepository.GetTodayPendingSales();
            return View("PendingSaleDetail_PartialView", MyList);
        }
        public ActionResult PendingDineInSaleDetail_PartialView()
        {
            List<ManageSale> MyList = POSRepository.GetDineInPendingSales();
            return View("PendingDineInSaleDetail_PartialView", MyList);
        }
        [HttpPost]
        public JsonResult AddSale(int SaleID, string TotalItems,decimal SubTotal, decimal Total, decimal DiscountAmount, decimal DeliveryCharges, decimal PaidAmount, decimal Change, int CustomerID, int DeliveryBoyID, string TableNo, string TakeAwayCustomerName, int SaleTypeID, string Tax, decimal TaxAmount,int OrderStatusID, int SaleStatusID, int PaymentModeID, List<ManageSaleDetail> SaleDetailList)
        {
            bool success = true;
            string errorMessage = "";
            var s = POSRepository.SaleInsertUpdate(SaleID,TotalItems,SubTotal,Total,DiscountAmount, DeliveryCharges, PaidAmount,Change,CustomerID,DeliveryBoyID,TableNo, TakeAwayCustomerName,SaleTypeID, Tax,TaxAmount, OrderStatusID,SaleStatusID, PaymentModeID,SaleDetailList).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult AddSaleWithPayment(int SaleID,decimal PaidAmount,decimal Change,int SaleStatusID, int PaymentModeID)
        {
            bool success = true;
            string errorMessage = "";
            string TableNo = "";
            var s = POSRepository.SalePaymentUpdate(SaleID, PaidAmount, Change,TableNo, SaleStatusID, PaymentModeID).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult CancelSaleDetailRow(int SaleDetailID)
        {
            bool success = true;
            string errorMessage = "";
            var s = POSRepository.DeleteSaleDetailRow(SaleDetailID).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult OrderDeliveredStatus(int SaleID)
        {
            bool success = true;
            string errorMessage = "";
            var s = POSRepository.OrderDeliveredStatus(SaleID).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult CheckUserExist(string Phone)
       {
            bool success = true;
            var MyList = CustomerRepository.CheckUserExist(Phone).ToJson();
            return Json(new { Success = success, Response = MyList }, JsonRequestBehavior.DenyGet);
        }
    }
}