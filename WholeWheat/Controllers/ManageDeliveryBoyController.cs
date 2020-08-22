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
    public class ManageDeliveryBoyController : Controller
    {
        // GET: ManageDeliveryBoy
        public ActionResult Index()
        {
                return View();
        }

        public ActionResult AddDeliveryBoy(int DeliveryBoyID, string DeliveryBoyName, string Phone, string Email,string Cnic, string Description, string Address, int StatusID)
        {
            bool success = true;
            string errorMessage = "";
            var s = DeliveryBoyRepository.InsertUpdateCustomer(DeliveryBoyID, DeliveryBoyName, Phone, Email, Cnic, Description, Address, StatusID).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult DeliveryBoyDetail_PartialView(int StatusID)
        {
            List<ManageDeliveryBoy> MyList = DeliveryBoyRepository.GetDeliveryBoy(StatusID);
            return View("DeliveryBoyDetail_PartialView", MyList);
        }

    }
}
