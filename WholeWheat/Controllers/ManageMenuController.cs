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
    public class ManageMenuController : Controller
    {
        // GET: ManageMenu
        public ActionResult Index()
        {
                return View();
        }
        [HttpPost]
        public JsonResult AddMenu(int Menuid, string Menuname, int StatusId)
        {
            bool success = true;
            string errorMessage = "";
            var s = MenuRepository.InsertUpdateMenu(Menuid, Menuname, StatusId).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult MenuDetail_PartialView(int StatusID)
        {
            List<WholeWheatRepository.Models.ManageMenu> MyList = MenuRepository.GetMenu(StatusID);
                return View("MenuDetail_PartialView", MyList);
        }
    }
}