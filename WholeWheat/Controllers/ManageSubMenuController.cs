using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WholeWheat.Helpers;
using WholeWheatRepository.Models;
using WholeWheatRepository.Repository;

namespace WholeWheat.Controllers
{
    [BaseController]
    public class ManageSubMenuController : Controller
    {
        // GET: ManageSubMenu
        public ActionResult Index()
        {
                return View(new DropDowns());
        }
        [HttpPost]
        public JsonResult AddSubMenu(ManageSubMenu obj)
        {
            bool success = true;
            string errorMessage = "";
            if(obj.File != null)
            {
                var filename = Path.GetFileName(obj.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Image/MenuImages/"), filename);
                obj.File.SaveAs(path);
            }
            try
            {
                var s = SubMenuRepository.InsertUpdateSubMenu(obj.SubMenuID, obj.SubMenuName, obj.MenuID, obj.Code,Convert.ToDouble(obj.Price), obj.FileName, obj.Color, obj.Description, obj.StatusID).ToJson();
                return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception e)
            {
                var a = "File Upload Failed";
                return Json(a);
            }
            
        }
        public ActionResult SubMenuDetail_PartialView(int StatusID)
        {
            List<ManageSubMenu> MyList = SubMenuRepository.GetSubMenu(StatusID);
            return View("SubMenuDetail_PartialView", MyList);
        }
    }
}  