using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Lincoln.Application.Web.Models;
using Lincoln.OnlineExam;
using Newtonsoft.Json;
using static Lincoln.Application.Web.FilterConfig;

namespace Lincoln.Application.Web.Controllers
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
            onlineExamService.test();
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = "";
                if (user != null)
                {
                   

                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.UserId = 0;
                    serializeModel.FirstName = "";
                    serializeModel.LastName = "";

                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    "user.Email",
                    DateTime.Now,
                    DateTime.Now.AddMinutes(15),
                    false, //pass here true, if you want to implement remember me functionality
                    userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

    }
}