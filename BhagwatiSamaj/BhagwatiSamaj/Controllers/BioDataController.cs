using BhagwatiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Controllers
{
    public class BioDataController : Controller
    {
        //
        // GET: /BioData/

        public ActionResult List()
        {
            var candidates = clsBusinessLogic.GetAllFamiliesForAdminListing().Where(x => x.IsPublished == true).ToList();
            return View(candidates);
        }

        public ActionResult AddCandidate()
        {
            //if (Session["CurrentUser"] != null)
            //{
            ViewBag.CountryList = clsBusinessLogic.GetCountryList();
            ViewBag.StateList = new List<State>();
            ViewBag.CityList = new List<City>();
            var candidate = new FamilyViewModel();
            candidate.FamilyMembers.Add(new FamilyMember());
            return View(candidate);
            //}
            //else
            //{
            //    Session["LastPage"] = "AddCandidate";
            //    return RedirectToAction("SignIn", "Account");
            //}
        }

        public ActionResult AddFamilyMember()
        {
            return PartialView("_FamilyMemberRow", new FamilyMember());
        }

        public ActionResult CandidateDetail(string CandidateId)
        {
            var candidate = clsBusinessLogic.GetAllFamiliesForAdminListing().FirstOrDefault(x => x.Id == Convert.ToInt32(CandidateId));
            return View(candidate);
        }

        [HttpPost]
        public ActionResult SaveMember(FamilyViewModel model, HttpPostedFileBase fPic)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                int candidateId = clsBusinessLogic.SaveFamily(model);
                if (candidateId > 0)
                {
                    if (fPic != null)
                    {
                        fPic.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["CandidatePhotoPath"]) + candidateId + System.IO.Path.GetExtension(fPic.FileName));
                        clsBusinessLogic.UpdateFamilyPhoto(candidateId, candidateId + System.IO.Path.GetExtension(fPic.FileName));
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
                response.ResponseValue = "Error : " + ex.Message;
            }
            return Json(response);
        }
    }
}

