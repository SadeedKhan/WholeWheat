using OfficeOpenXml;
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
    public class ManageCustomerController : Controller
    {
        // GET: ManageCustomer
        public ActionResult Index()
        {
                return View();
        }

        public ActionResult AddCustomer(int CustomerID, string CustomerName, string Phone, string Email, string Description, string Address, int StatusID)
        {
            bool success = true;
            string errorMessage = "";
            var s = CustomerRepository.InsertUpdateCustomer(CustomerID, CustomerName, Phone, Email,  Description, Address, StatusID).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult CustomerDetail_PartialView(int StatusID)
        {
            List<ManageCustomer> MyList = CustomerRepository.GetCustomer(StatusID);
            return View("CustomerDetail_PartialView", MyList);
        }
    }
}