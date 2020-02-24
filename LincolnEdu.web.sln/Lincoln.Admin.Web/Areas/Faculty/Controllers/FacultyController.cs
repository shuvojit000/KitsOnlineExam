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


            model.ExamSectionList = GetAllExaminationSection().Where(a => a.Active == "A").Select(c => new SelectListItem
            {
                Text = c.SectionName,
                Value = c.ExaminationSectionID.ToString()
            }).ToList();


            return View(model);
        }
        private List<ExaminationSectionViewModel> GetAllExaminationSection()
        {
            var itemSet = new List<ExaminationSectionViewModel>();
            itemSet = onlineExamService.GetAllExaminationSection().Select(a => new ExaminationSectionViewModel()
            {
                ExaminationSectionID = a.ExaminationSectionID,
                ProgramCode = a.ProgramCode,
                ProgramName = a.ProgramName,
                CourseCode = a.CourseCode,
                CourseID = a.CourseID,
                CountryName = a.CountryName,
                SemisterCode = a.SemisterCode,
                SemisterName = a.SemisterName,
                SyllabusVersionCode = a.SyllabusVersionCode,
                SyllabusVersionName = a.SyllabusVersionName,
                CourseName = a.CourseName,
                FacultyCode = a.FacultyCode,
                FacultyName = a.FacultyName,
                SectionName = a.SectionName,
                AcademicYearCode = a.AcademicYearCode,
                YearName = a.YearName,
                QuestionType = a.QuestionType,
                Active = a.Status,
                ModifiedOn = a.ModifiedOn?.Date,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }

        private ExaminationSectionViewModel SelectExaminationSection(string ExaminationSectionId)
        {
            var model = new ExaminationSectionViewModel();
            var item = onlineExamService.SelectExaminationSection(new OnlineExam.Request.ExaminationSectionRequestDTO
            {
                ExaminationSectionID = Convert.ToInt32(ExaminationSectionId)

            });
            model.ExaminationSectionID = item.ExaminationSectionID;
            model.ProgramCode = item.ProgramCode;
            model.CourseCode = item.CourseCode;
            model.CourseID = item.CourseID;
            model.SemisterCode = item.SemisterCode;
            model.SyllabusVersionCode = item.SyllabusVersionCode;
            model.CountryCode = item.CountryCode;
            model.FacultyCode = item.FacultyCode;
            model.AcademicYearCode = item.AcademicYearCode;
            model.QuestionType = item.QuestionType;
            model.ProgramName = item.ProgramName;
            model.CourseName = item.CourseName;
            model.SemisterName = item.SemisterName;
            model.SyllabusVersionName = item.SyllabusVersionName;
            model.CountryName = item.CountryName;
            model.FacultyName = item.FacultyName;
            model.YearName = item.YearName;
            model.SectionName = item.SectionName;
            model.SectionID = item.SectionName;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            return model;

        }
        public PartialViewResult AddQuestion(string id)
        {
            var model = new QuestionSetUpViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model.QuestionType = SelectExaminationSection(id)?.QuestionType;
            }
            return PartialView("_AddQuestion", model);
        }
        [HttpPost]
        public JsonResult SavePaperDetails(QuestionSetUpViewModel model)
        {

            var type = "INSERT";
            if (model.PaperID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SavePaper(new OnlineExam.Request.PaperRequestDTO()
            {

                CreatedBy = User.UserId,
                Active = model.Active,
                CountryID = model.CountryID,
                CourseID = model.CourseID,
                PaperID = model.PaperID,
                ProgrammeID = model.ProgrammeID,
                ProgrammeSemester = model.ProgrammeSemester,
                ProgrammeVersioningID = model.SyllabusVersionID,
                ProgrammeYear = model.ProgrammeYear,
                SectionName = model.SectionName

            }, type);

            var Detailstype = "INSERT";
            if (model.PaperDetailsID > 0)
            {
                Detailstype = "UPDATE";
            }
            var resultDetails = onlineExamService.SavePaperDetails(new OnlineExam.Request.PaperDetailsRequestDTO()
            {

                CreatedBy = User.UserId,
                Active = model.Active,
                PaperID = model.PaperID,
                AnswerNo = model.AnswerNo,
                AnswerText = model.AnswerText,
                PaperDetailsID = model.PaperDetailsID,
                AudioOrVideoQuestion = model.AudioOrVideoQuestion,
                OptionANo = model.OptionANo,
                OptionAText = model.OptionAText,
                OptionBNo = model.OptionBNo,
                OptionBText = model.OptionBText,
                OptionCNo = model.OptionCNo,
                OptionCText = model.OptionCText,
                OptionDNo = model.OptionDNo,
                OptionDText = model.OptionDText,
                OptionENo = model.OptionENo,
                OptionEText = model.OptionEText,
                QuestionMarks = model.QuestionMarks,
                QuestionNo = model.QuestionNo,
                QuestionText = model.QuestionText,
                QuestionType = model.QuestionType,
                TextOrImageQuestion = model.TextOrImageQuestion
            }, Detailstype);
            return Json(resultDetails, JsonRequestBehavior.AllowGet);
        }
    }
}