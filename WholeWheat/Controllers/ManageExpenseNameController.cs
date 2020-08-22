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
    public class ManageExpenseNameController : Controller
    {
        // GET: ManageExpenseName
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult AddExpenseName(int ExpenseNameID, string ExpenseName, int StatusId)
        {
            bool success = true;
            string errorMessage = "";
            var s = ExpenseNameRepository.InsertUpdateExpenseName(ExpenseNameID, ExpenseName, StatusId).ToJson();
            return Json(new { Success = success, ErrorMessage = errorMessage, Response = s }, JsonRequestBehavior.DenyGet);
        }
        public ActionResult ExpenseNameDetail_PartialView()
        {
            List<ManageExpenseName> MyList = ExpenseNameRepository.GetExpenseName();
            return View("ExpenseNameDetail_PartialView", MyList);
        }
    }
}