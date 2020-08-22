using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholeWheat.Helpers;
using WholeWheatRepository.Models;
using WholeWheatRepository.Repository;

namespace WholeWheat.Controllers
{
    [BaseController]
    public class ManageExpenseController : Controller
    {
        // GET: ManageExpense
        public ActionResult Index()
        {
            return View(new DropDowns());
        }
        public JsonResult AddExpense(ManageExpense obj)
        {
            bool success = true;
            string errorMessage = "";
            if (obj.File != null)
            {
                var filename = Path.GetFileName(obj.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Image/ExpenseImages/"), filename);
                obj.File.SaveAs(path);
            }
            try
            {
                var s = ExpenseRepository.InsertUpdateExpense(obj.ExpenseID, obj.ExpenseNameID,Convert.ToDateTime(obj.ExpenseDate), obj.ReferenceNo,obj.ExpenseHeadID, Convert.ToDecimal(obj.ExpenseAmount), obj.FileName, obj.Notes, obj.StatusID).ToJson();
                return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception e)
            {
                var a = "File Upload Failed";
                return Json(a);
            }

        }
        public ActionResult ExpenseDetail_PartialView(int StatusID)
        {
            List<ManageExpense> MyList = ExpenseRepository.GetExpense(StatusID);
            return View("ExpenseDetail_PartialView", MyList);
        }
    }
}