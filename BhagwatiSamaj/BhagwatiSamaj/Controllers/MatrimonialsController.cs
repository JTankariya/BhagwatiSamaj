using BhagwatiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Controllers
{
    public class MatrimonialsController : Controller
    {
        //
        // GET: /Matrimonials/

        public ActionResult List()
        {
            //ViewBag.CityList = clsBusinessLogic.GetCityList().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            //ViewBag.EducationList = clsBusinessLogic.GetEducationFromMatrimonials("");
            ViewBag.NativePlaceList = clsBusinessLogic.GetNativePlaceFromMatrimonials("");
            var matrimonials = clsBusinessLogic.GetAllMatrimonials(0).OrderByDescending(x => x.Id).AsEnumerable();
            return View(matrimonials);
        }

        public ActionResult AddMatrimonial(string Id)
        {
            ViewBag.CountryList = clsBusinessLogic.GetCountryList();
            ViewBag.StateList = new List<State>();
            ViewBag.CityList = new List<City>();
            if (Id == "0")
            {
                if (Session["CurrentUser"] != null)
                {
                    var model = new Matrimonial();
                    model.IsActive = true;
                    return View(model);
                }
                else
                {
                    Session["LastPage"] = "AddMatrimonial";
                    return RedirectToAction("SignIn", "Account");
                }
            }
            else
            {
                return View(clsBusinessLogic.GetMatrimonialById(Id));
            }

        }

        public ActionResult GetSearchList(string Name, string NativePlace)
        {
            var model = clsBusinessLogic.GetAllMatrimonials(0);
            if (!string.IsNullOrEmpty(Name))
                model = model.Where(x => x.FullName.ToUpper().Contains(Name.ToUpper()));
            if (!string.IsNullOrEmpty(NativePlace))
                model = model.Where(x => x.NativePlace != null && x.NativePlace.ToUpper() == NativePlace.ToUpper());
            model = model.OrderByDescending(x => x.Id).AsEnumerable();
            return PartialView("_MatrimonialRow", model);
        }
        public ActionResult Detail(string PersonId)
        {
            var person = clsBusinessLogic.GetAllMatrimonials(0).FirstOrDefault(x => x.Id == Convert.ToInt32(PersonId));
            return View(person);
        }

        public ActionResult SaveMatrimonial(Matrimonial model, HttpPostedFileBase fPic)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                int matrimonialId = clsBusinessLogic.SaveMatrimonial(model, ((BhagwatiSamaj.Models.User)Session["CurrentUser"]).Id.ToString());
                if (matrimonialId > 0)
                {
                    if (fPic != null)
                    {
                        fPic.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["MatrimonialPhotoPath"]) + matrimonialId + System.IO.Path.GetExtension(fPic.FileName));
                        clsBusinessLogic.UpdateMatrimonialPhoto(matrimonialId, matrimonialId + System.IO.Path.GetExtension(fPic.FileName));
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseValue = "Error while saving candidate, Please try after sometime.";
                    return Json(response);
                }

                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResponseValue = ex.Message;
            }
            return Json(response);
        }

    }
}
