using BhagwatiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MemberList()
        {
            var admincandidates = clsBusinessLogic.GetAllFamiliesForAdminListing();
            return View(admincandidates);
        }

        public ActionResult GetCandidateInfo(string candidateId)
        {
            var candidate = clsBusinessLogic.GetAllFamiliesForAdminListing().FirstOrDefault(x => x.Id == Convert.ToInt32(candidateId));
            return View(candidate);
        }

        public ActionResult ActivateProfile(string candidateId)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                clsBusinessLogic.ActivateFamily(candidateId);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResponseValue = "Error : " + ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeactivateProfile(string candidateId)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                clsBusinessLogic.DeactivateFamily(candidateId);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResponseValue = "Error : " + ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventList()
        {
            var events = clsBusinessLogic.GetAllEvents();
            return View(events);
        }

        public ActionResult EditEvent(string Id)
        {
            if (Id == "0")
            {
                var envt = new Event();
                return View(envt);
            }
            else
            {
                var evnt = clsBusinessLogic.GetAllEvents().FirstOrDefault(x => x.Id == Convert.ToInt32(Id));
                evnt.EventUploads = clsBusinessLogic.GetEventsUpload(evnt.Id);
                return View(evnt);
            }

        }

        public ActionResult DeleteEventPhoto(string EventUploadId)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                clsBusinessLogic.DeleteEventPhoto(EventUploadId);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResponseValue = "Error : " + ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveEvent(Event evnt, List<HttpPostedFileBase> uploads)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                int EventId = clsBusinessLogic.SaveEvent(evnt);
                if (uploads != null)
                {
                    if (EventId > 0)
                    {
                        foreach (var item in uploads)
                        {
                            if (item != null)
                            {
                                var uploadId = clsBusinessLogic.InsertEventUpload(EventId, EventId + System.IO.Path.GetExtension(item.FileName));
                                item.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["EventPhotoPath"]) + uploadId + "_" + EventId + System.IO.Path.GetExtension(item.FileName));
                                clsBusinessLogic.UpdateEventUpload(uploadId, uploadId + "_" + EventId + System.IO.Path.GetExtension(item.FileName));
                            }
                        }
                    }
                }
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResponseValue = "Error : " + ex.Message;
            }
            return Json(response);
        }

        public ActionResult DeleteEvent(string eventId)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                var uploads = clsBusinessLogic.GetEventsUpload(Convert.ToInt32(eventId));

                if (uploads != null)
                {
                    foreach (var upload in uploads)
                    {
                        clsBusinessLogic.DeleteEventPhoto(upload.Id.ToString());
                        if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["EventPhotoPath"]) + upload.UploadPath))
                        {
                            System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["EventPhotoPath"]) + upload.UploadPath);
                        }
                    }
                }
                clsBusinessLogic.DeleteEvent(eventId);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResponseValue = "Error : " + ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAdvertize(string advertizeId)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                var advertize = clsBusinessLogic.GetAllAdverize().FirstOrDefault(x => x.Id == Convert.ToInt32(advertizeId));
                if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["EventPhotoPath"]) + advertize.AdvertizePath))
                {
                    System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["EventPhotoPath"]) + advertize.AdvertizePath);
                }
                clsBusinessLogic.DeleteAdvertize(advertizeId);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResponseValue = "Error : " + ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdvertizeList()
        {
            var advertize = clsBusinessLogic.GetAllAdverize();
            return View(advertize);
        }

        public ActionResult EditAdvertize(string Id)
        {
            if (Id == "0")
            {
                var envt = new Advertize();
                return View(envt);
            }
            else
            {
                var evnt = clsBusinessLogic.GetAllAdverize().FirstOrDefault(x => x.Id == Convert.ToInt32(Id));
                return View(evnt);
            }

        }

        public ActionResult SaveAdvertize(Advertize advertize, HttpPostedFileBase fPic)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                int AdvertizeId = clsBusinessLogic.SaveAdvertize(advertize);
                if (fPic != null)
                {
                    if (AdvertizeId > 0)
                    {
                        fPic.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["AdvertizePhotoPath"] + AdvertizeId + System.IO.Path.GetExtension(fPic.FileName)));
                        clsBusinessLogic.UpdateAdvertize(AdvertizeId, AdvertizeId + System.IO.Path.GetExtension(fPic.FileName));
                    }
                }
                response.IsSuccess = true;
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
