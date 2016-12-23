using BhagwatiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Controllers
{
    public class CountryController : Controller
    {
        //
        // GET: /Country/

        public ActionResult List()
        {
            var countryList = clsBusinessLogic.GetCountryList();
            return View();
        }


        public ActionResult Add()
        {
            return View(new Country());
        }

        [HttpPost]
        public ActionResult SaveCountry(string CountryName) {
            ResponseMsg response = new ResponseMsg();
            response.ResponseValue = clsBusinessLogic.InsertCountry(CountryName);
            response.IsSuccess = true;
            return Json(response);
        }
    }
}
