using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WholeWheat.Helpers;
using WholeWheat.Models.Admin;
using WholeWheatRepository.Models;
using WholeWheatRepository.Repository;

namespace WholeWheat.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        #region Login Management
        [AllowAnonymous]
        public ActionResult Login()
        {
            AdminLoginModel model = new AdminLoginModel();
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                string code = "WWT-3355306";
                string encryptedPassword = PasswordHelper.EncryptPassword(model.UserPassword);
                AdminUser user = AdminRepository.AuthenticateUser(model.UserName, encryptedPassword);
                if (user != null)
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        var authTicket = new FormsAuthenticationTicket(720, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(1440), false, code);
                        var v = FormsAuthentication.DefaultUrl;
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        HttpContext.Response.Cookies.Add(authCookie);
                        {
                            Session["UserId"] = user.Id;
                            Session["UserName"] = user.UserName;
                            return RedirectToAction("Index", "POS");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid UserName Or Password";
                    }
            }

            return View();
        }
        public JsonResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return null;
        }
        #endregion
    }
}