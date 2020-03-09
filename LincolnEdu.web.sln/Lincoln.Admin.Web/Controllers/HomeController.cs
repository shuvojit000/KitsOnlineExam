using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Lincoln.Admin.Web.Models;
using Lincoln.OnlineExam;
using Newtonsoft.Json;
using System.IO;

namespace Lincoln.Admin.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IOnlineExam onlineExamService;

        public HomeController(IOnlineExam onlineExamService)
        {
            this.onlineExamService = onlineExamService;
        }

        public ActionResult LogIn()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(LogInViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = onlineExamService.ValidateUser(new OnlineExam.Request.LogInRequestDTO()
                {
                    EmailID = model.EmailID,
                    MobileNo = model.MobileNo,
                    Password = model.Password,
                    UserName = model.UserName,
                    UserType = model.UserType

                }, "LOGIN");
                if (user.LoginID > 0)
                {
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.UserId = user.LoginID;
                    serializeModel.FirstName = "";
                    serializeModel.LastName = "";
                    serializeModel.Email = user.EmailID;
                    serializeModel.UserType = user.UserType;
                    serializeModel.UserName = user.UserName;
                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    user.EmailID,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(15),
                    false, //pass here true, if you want to implement remember me functionality
                    userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else if (String.Equals(user.UserType, "ADMIN", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Dashboard", "Admin", new { area = "Admin" });
                    }
                    else if (String.Equals(user.UserType, "FACULTY", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Dashboard", "Faculty", new { area = "Faculty" });
                    }
                    else if (String.Equals(user.UserType, "STUDENT", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Dashboard", "Student", new { area = "Student" });
                    }
                }

                ModelState.AddModelError("UserName", "Incorrect Username and/or Password");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home", new { area=""});
        }

        public ActionResult uploadPartial()
        {
            var appData = Server.MapPath("~/UploadImage");
            var images = Directory.GetFiles(appData).Select(x => new imagesviewmodel
            {
                Url = Url.Content("/UploadImage/" + Path.GetFileName(x))
            });
            return View(images);
        }
        public void uploadnow(HttpPostedFileWrapper upload)
        {
            if (upload != null)
            {
                string ImageName = upload.FileName;
                string path = System.IO.Path.Combine(Server.MapPath("~/UploadImage"), ImageName);
                upload.SaveAs(path);
            }

        }
    }
}