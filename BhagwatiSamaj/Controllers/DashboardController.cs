using BhagwatiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            if (Session["LastPage"] != null && Session["LastPage"].ToString().ToUpper() == "ADDCANDIDATE")
            {
                Session["LastPage"] = null;
                return RedirectToAction("AddFamily", "Family");
            }
            if (Session["LastPage"] != null && Session["LastPage"].ToString().ToUpper() == "ADDMATRIMONIAL")
            {
                Session["LastPage"] = null;
                return RedirectToAction("AddMatrimonial", "Matrimonials", new { Id = "0" });
            }
            return View();
        }
        public ActionResult Gyativad()
        {
            return View();
        }
        public ActionResult Events()
        {
            var events = clsBusinessLogic.GetAllEvents().ToList();
            foreach (var item in events)
            {
                item.EventUploads = clsBusinessLogic.GetEventsUpload(item.Id);
            }
            return View(events);
        }

        public ActionResult EventDetail(string EventId)
        {
            var evnt = clsBusinessLogic.GetAllEvents().FirstOrDefault(x => x.Id == Convert.ToInt32(EventId));
            evnt.EventUploads = clsBusinessLogic.GetEventsUpload(evnt.Id);
            return View(evnt);
        }

        public ActionResult ContactUs()
        {
            return View();
        }
    }
}
