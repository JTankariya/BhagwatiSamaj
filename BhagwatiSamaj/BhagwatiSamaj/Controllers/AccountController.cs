using BhagwatiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult SignIn()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult SignIn(string UserName, string Password)
        {
            ResponseMsg response = new ResponseMsg();
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    User lst = clsBusinessLogic.GetUserByUserNamePassword(UserName, Password).FirstOrDefault();
                    if (lst != null)
                    {
                        response.IsSuccess = true;
                        response.ResponseValue = lst;
                        Session["CurrentUser"] = lst;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ResponseValue = "User with given credentials not found, Please enter correct username and password.";
                    }

                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.ResponseValue = ex.Message;
                }
            }
            return Json(response);
        }

        public ActionResult PrintMember()
        {
            string fileName = Convert.ToString(ConfigurationManager.AppSettings["ReportFolderPath"]).Replace("~", "") + clsBusinessLogic.PrintMemberReport();
            return Json(new ResponseMsg { IsSuccess = true, ResponseValue = fileName }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ForgetPassword()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult ForgetPassword(string EmailId)
        {
            ResponseMsg response = new ResponseMsg();
            if (!string.IsNullOrEmpty(EmailId))
            {
                var obj = clsBusinessLogic.GetUserByUserNamePassword(EmailId, "").FirstOrDefault();
                if (obj != null && obj.Id > 0)
                {
                    bool result = clsBusinessLogic.SendEmail(EmailId, "Forget Password : Bhagwati Samaj", "Your current Password Is : " + obj.Password, obj);
                    if (result)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ResponseValue = "Error while sending email, Please try after sometime";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseValue = "User not found with provided email, Please enter emailid which is registerd on our site.";
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseValue = "Email id cannot be empty, Please enter Email Id.";
            }
            return Json(response);
        }

        public ActionResult Register()
        {
            return View(new User());
        }


        [HttpPost]
        public ActionResult Register(User obj)
        {
            ResponseMsg response = new ResponseMsg();
            if (obj != null)
            {
                var userId = clsBusinessLogic.CheckUserDuplicate(obj.EmailId);
                if (userId == 0)
                {
                    int result = clsBusinessLogic.SaveUser(obj);
                    if (result > 0)
                    {
                        response.IsSuccess = true;
                        response.ResponseValue = obj;
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseValue = "User with same email id id exist, Please enter another email id or use forget password.";
                }
            }
            return Json(response);
        }

        public ActionResult LogOut()
        {
            Session["CurrentUser"] = null;
            return View("SignIn", new User());
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string NewPassword)
        {
            ResponseMsg response = new ResponseMsg();
            try
            {
                clsBusinessLogic.ChangePassword(NewPassword, ((User)Session["CurrentUser"]).Id.ToString());
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResponseValue = "Error : " + ex.Message;
            }
            return Json(response);
        }

        public ActionResult UpdateProfile()
        {
            return View((User)Session["CurrentUser"]);
        }
    }
}
