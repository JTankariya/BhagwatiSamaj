using BhagwatiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Controllers
{
    public class FamilyController : Controller
    {
        //
        // GET: /Family/

        public ActionResult List()
        {
            //ViewBag.CityList = clsBusinessLogic.GetCityList().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            //ViewBag.EducationList = clsBusinessLogic.GetEducationFromFamily("");
            //ViewBag.BusinessTypeList = clsBusinessLogic.GetBusinessTypeFromFamily("");
            ViewBag.NativePlaceList = clsBusinessLogic.GetNativePlaceFromFamilies("");
            var families = clsBusinessLogic.GetAllFamiliesForAdminListing().Where(x => x.IsPublished == true).ToList();
            return View(families);
        }

        public ActionResult GetSearchList(string Name, string NativePlace)
        {
            var model = clsBusinessLogic.GetAllFamiliesForAdminListing();
            if (!string.IsNullOrEmpty(Name))
            {
                model = model.Where(x => (x.FirstName + " " + x.MiddleName + " " + x.LastName).ToUpper().Contains(Name.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(NativePlace))
            {
                model = model.Where(x => x.NativePlace.ToUpper() == NativePlace).ToList();
            }

            model = model.OrderByDescending(x => x.Id).ToList();

            return PartialView("FamilyRow", model);
        }

        public ActionResult AddFamily()
        {
            if (Session["CurrentUser"] != null)
            {
                ViewBag.CountryList = clsBusinessLogic.GetCountryList();
                ViewBag.StateList = new List<State>();
                ViewBag.CityList = new List<City>();
                var candidate = new FamilyViewModel();
                candidate.FamilyMembers.Add(new FamilyMember());
                return View(candidate);
            }
            else
            {
                Session["LastPage"] = "AddCandidate";
                return RedirectToAction("SignIn", "Account");
            }
        }

        public ActionResult AddFamilyMember()
        {
            return PartialView("_FamilyMemberRow", new FamilyMember());
        }

        public ActionResult FamilyDetail(string FamilyId)
        {
            var family = clsBusinessLogic.GetFamilyById(FamilyId);

            return View(family);
        }

        [HttpPost]
        public ActionResult SaveFamily(FamilyViewModel model, HttpPostedFileBase fPicMainMember, HttpPostedFileBase fPicFather, HttpPostedFileBase fPicMother, List<HttpPostedFileBase> fPicMembers)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                var newId = Guid.NewGuid();
                if (fPicMainMember != null)
                {
                    newId = Guid.NewGuid();
                    fPicMainMember.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["FamilyPhotoPath"]) + newId + System.IO.Path.GetExtension(fPicMainMember.FileName));
                    model.family.PhotoPath = newId + System.IO.Path.GetExtension(fPicMainMember.FileName);
                }
                if (fPicFather != null)
                {
                    newId = Guid.NewGuid();
                    fPicFather.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["FamilyPhotoPath"]) + newId + System.IO.Path.GetExtension(fPicFather.FileName));
                    model.family.FatherImagePath = newId + System.IO.Path.GetExtension(fPicFather.FileName);
                }
                if (fPicMother != null)
                {
                    newId = Guid.NewGuid();
                    fPicMother.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["FamilyPhotoPath"]) + newId + System.IO.Path.GetExtension(fPicMother.FileName));
                    model.family.MotherImagePath = newId + System.IO.Path.GetExtension(fPicMother.FileName);
                }
                if (fPicMembers != null && fPicMembers.Count > 0)
                {
                    for (var i = 0; i < fPicMembers.Count; i++)
                    {
                        if (fPicMembers[i] != null)
                        {
                            newId = Guid.NewGuid();
                            fPicMembers[i].SaveAs(Server.MapPath(ConfigurationManager.AppSettings["FamilyPhotoPath"]) + newId + System.IO.Path.GetExtension(fPicMembers[i].FileName));
                            model.FamilyMembers[i].PhotoPath = newId + System.IO.Path.GetExtension(fPicMembers[i].FileName);
                        }

                    }
                }
                int candidateId = clsBusinessLogic.SaveFamily(model);
                if (!(candidateId > 0))
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
                response.ResponseValue = "Error : " + ex.Message;
            }
            return Json(response);
        }

    }
}
