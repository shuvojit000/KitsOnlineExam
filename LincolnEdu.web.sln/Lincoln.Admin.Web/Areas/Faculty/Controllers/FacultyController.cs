using Lincoln.Admin.Web.Controllers;
using Lincoln.Admin.Web.Models;
using Lincoln.OnlineExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Areas.Faculty.Controllers
{
    public class FacultyController : BaseController
    {
        private readonly IOnlineExam onlineExamService;

        public FacultyController(IOnlineExam onlineExamService)
        {
            this.onlineExamService = onlineExamService;
        }

        // GET: Faculty/Faculty
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult QuestionSetUp()
        {
            var model = new QuestionSetUpViewModel();
            model.FacultyList = onlineExamService.GetAllDepartment().Select(a => new SelectListItem { Text = a.DepartmentName, Value = a.DepartmentID.ToString() }).ToList();
            model.ProgramList = new List<SelectListItem>();
            model.SyllabusVersionList = new List<SelectListItem>();
            model.SemisterList = new List<SelectListItem>();
            model.CourseList = new List<SelectListItem>();
            model.CountryList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="India", Value = "1" },
                                new SelectListItem{ Text="Malaysia", Value = "2" },
                                new SelectListItem{ Text="United States", Value = "3" },
                             };

            model.SectionList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="A", Value = "A" },
                                new SelectListItem{ Text="B", Value = "B" },
                                new SelectListItem{ Text="C", Value = "C" },
                                 new SelectListItem{ Text="D", Value = "D" },
                                  new SelectListItem{ Text="E", Value = "E" },
                             };
            model.ProgrammeYearList = new List<SelectListItem>();
            return View(model);
        }

    }
}