using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Models
{
    public class AuthorizeWebFormAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var loginUrl = "~/Account/SignIn";
            if (HttpContext.Current.Session.Count == 0 || HttpContext.Current.Session["CurrentUser"] == null)
            {
                filterContext.Result = new RedirectResult(loginUrl);
            }
            //else
            //{
            //    if (ActiveMachines.lstActiveMachines.Any(x => x.MachineName == SessionManager.Instance.MachineName && x.LastUpdate.AddMinutes(1) < DateTime.Now))
            //        filterContext.Result = new RedirectResult(loginUrl);
            //}
        }
    }
}