using Lincoln.Admin.Web.Controllers;
using Lincoln.Admin.Web.Models;
using Lincoln.OnlineExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Areas.Student.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IOnlineExam onlineExamService;

        public StudentController(IOnlineExam onlineExamService)
        {
            this.onlineExamService = onlineExamService;
        }

        // GET: Faculty/Faculty
        public ActionResult Dashboard()
        {
            List<StudentDashboardViewModel> DVMList = new List<StudentDashboardViewModel>();
            StudentDashboardViewModel DVM = new StudentDashboardViewModel();
            DVM.CourseCode = "C001";
            DVM.CourseID = 22;
            DVM.CourseName = "C++";
            DVMList.Add(DVM);

            StudentDashboardViewModel DVM1 = new StudentDashboardViewModel();
            DVM1.CourseCode = "C002";
            DVM1.CourseID = 23;
            DVM1.CourseName = "C#";
            DVMList.Add(DVM1);
            return View(DVMList);
        }
        public ActionResult AnswerSetup(string ID)
        {
            AnswerSetUpdViewModel AVM = new AnswerSetUpdViewModel();
            AVM.CourseCode = "C001";
            AVM.CourseName = "C++";
            AVM.CourseID = 22;
            AVM.QuestionText = "What Is your Age?";
            AVM.OptionANo = 1;
            AVM.OptionAText = "One";
            AVM.OptionBNo = 2;
            AVM.OptionBText = "Two";
            AVM.OptionCNo = 3;
            AVM.OptionCText = "Three";
            AVM.OptionDNo = 4;
            AVM.OptionDText = "Four";
            AVM.OptionENo = 5;
            AVM.OptionEText = "Five";



            if (ID == "22")
            {
                AVM.CourseID = 22;
                AVM.QuestionType = "S";
            }
            else
            {
                AVM.CourseCode = "C002";
                AVM.CourseName = "C#";
                AVM.CourseID = 23;
                AVM.QuestionType = "O";
            }

            return View(AVM);
        }

        public PartialViewResult AnswerPaper(string id)
        {
            AnswerSetUpdViewModel AVM = new AnswerSetUpdViewModel();
           
            AVM.QuestionText = "What Is your Age?";
            AVM.OptionANo = 1;
            AVM.OptionAText = "One";
            AVM.OptionBNo = 2;
            AVM.OptionBText = "Two";
            AVM.OptionCNo = 3;
            AVM.OptionCText = "Three";
            AVM.OptionDNo = 4;
            AVM.OptionDText = "Four";
            AVM.OptionENo = 5;
            AVM.OptionEText = "Five";

            if (id == "22")
            {
                AVM.CourseID = 22;
                AVM.QuestionType = "S";
            }
            else
            {
                AVM.CourseID = 23;
                AVM.QuestionType = "O";
            }
            return PartialView("_AnswerPaper", AVM);
        }
    }
}