using BhagwatiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Controllers
{
    public class CityController : Controller
    {
        //
        // GET: /City/

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Add(string StateId)
        {
            ViewBag.StateId = StateId;
            ViewBag.StateList = clsBusinessLogic.GetStateByCountry("0");
            return View(new City());
        }

        public ActionResult GetCityByState(string StateId)
        {
            ResponseMsg response = new ResponseMsg();
            response.IsSuccess = true;
            response.ResponseValue = clsBusinessLogic.GetCityByState(StateId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveCity(string CityName, string StateId)
        {
            ResponseMsg response = new ResponseMsg();
            response.ResponseValue = clsBusinessLogic.InsertCity(CityName, StateId);
            response.IsSuccess = true;
            return Json(response);
        }
    }
}
