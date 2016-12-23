using BhagwatiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Controllers
{
    public class StateController : Controller
    {
        //
        // GET: /State/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStateByCountry(string CountryId)
        {
            ResponseMsg response = new ResponseMsg();
            response.IsSuccess = true;
            response.ResponseValue = clsBusinessLogic.GetStateByCountry(CountryId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(string CountryId)
        { 
            ViewBag.CountryId= CountryId;
            ViewBag.CountryList = clsBusinessLogic.GetCountryList();
            return View(new State());
        }

        [HttpPost]
        public ActionResult SaveState(string StateName, string CountryId)
        {
            ResponseMsg response = new ResponseMsg();
            response.ResponseValue = clsBusinessLogic.InsertState(StateName, CountryId);
            response.IsSuccess = true;
            return Json(response);
        }
    }
}
