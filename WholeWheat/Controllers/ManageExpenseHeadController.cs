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
    public class ManageExpenseHeadController : Controller
    {
        // GET: ManageExpenseHead
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult AddExpenseHead(int ExpenseHeadID, string ExpenseHeadName, int StatusId)
        {
            bool success = true;
            string errorMessage = "";
            var s = ExpenseHeadRepository.InsertUpdateExpenseHead(ExpenseHeadID, ExpenseHeadName, StatusId).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }
        public ActionResult ExpenseHeadDetail_PartialView(int StatusID)
        {
            List<ManageExpenseHead> MyList = ExpenseHeadRepository.GetExpenseHead(StatusID);
            return View("ExpenseHeadDetail_PartialView", MyList);
        }
    }
}