using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WholeWheat.Controllers
{
    public class BaseController : AuthorizeAttribute
    {
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (Session["UserId"] == null)
        //        Response.Redirect("/Login/Login");
        //    // base.OnActionExecuting(filterContext);
        //}

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            FormsAuthentication.SignOut();
            filterContext.Result = new RedirectResult("~/Login/Login");
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

    }
}