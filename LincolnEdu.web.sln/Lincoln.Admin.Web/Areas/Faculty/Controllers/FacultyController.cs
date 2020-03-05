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
            var modelList = new List<FacultyDashboardViewModel>();
            var itemSet = onlineExamService.GetAllFacultyCourse(new OnlineExam.Request.FacultyDashboardRequestDTO
            {
                EmployeeID = Convert.ToInt32(User.UserId),
            });
            foreach (var item in itemSet)
            {
                var model = new FacultyDashboardViewModel();
                model.EmployeeID = item.EmployeeID;
                model.NoOfQuestion = item.NoOfQuestion;
                model.EmployeeName = item.EmployeeName;
                model.CourseID = item.CourseID;
                model.CourseName = item.CourseName;
                model.CourseCode = item.CourseCode;
                modelList.Add(model);
            };
            return View(modelList);
        }
        public ActionResult QuestionSetUp(string id)
        {
            var model = new QuestionSetUpViewModel();

            model.CourseID = Convert.ToInt32(id);
            model.ExamSectionList = GetAllExaminationSection().Where(a => a.CourseID == Convert.ToInt32(id) && a.Active == "A").Select(c => new SelectListItem
            {
                Text = c.SectionName,
                Value = c.ExaminationSectionID.ToString()
            }).ToList();

            TempData["CourseID"] = model.CourseID;
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
                ProgrammeVersioningID = a.ProgrammeVersioningID,
                Version = a.Version,
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
                MaximumMarks=a.MaximumMarks,
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
            model.ProgrammeVersioningID = item.ProgrammeVersioningID;
            model.CountryCode = item.CountryCode;
            model.FacultyCode = item.FacultyCode;
            model.AcademicYearCode = item.AcademicYearCode;
            model.QuestionType = item.QuestionType;
            model.ProgramName = item.ProgramName;
            model.CourseName = item.CourseName;
            model.SemisterName = item.SemisterName;
            model.Version = item.Version;
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
        public PartialViewResult AddQuestion(string examSectionID, string paperDetailsID)
        {
            var model = new QuestionSetUpViewModel();
            if (!string.IsNullOrEmpty(paperDetailsID))
            {
                model = SelectPaperDetails(paperDetailsID);
            }
            else
            {

                if (!string.IsNullOrEmpty(examSectionID))
                {
                    model.QuestionType = SelectExaminationSection(examSectionID)?.QuestionType;
                    model.QuestionNo = onlineExamService.SelectPaper(new OnlineExam.Request.PaperRequestDTO()
                    {
                        CourseID = Convert.ToInt32(TempData.Peek("CourseID")),
                        LoginID = User.UserId,
                        ExaminationSectionID = Convert.ToInt32(examSectionID),
                        CreatedBy = User.UserId,

                    })?.QuestionNo ?? 1;
                    model.SectionMarks = GetAllExaminationSection().Where(a => a.ExaminationSectionID == Convert.ToInt32(examSectionID)).
                        FirstOrDefault().MaximumMarks ?? 0;
                    model.RemainingMarks = onlineExamService.SelectPaper(new OnlineExam.Request.PaperRequestDTO()
                    {
                        CourseID = Convert.ToInt32(TempData.Peek("CourseID")),
                        LoginID = User.UserId,
                        ExaminationSectionID = Convert.ToInt32(examSectionID),
                        CreatedBy = User.UserId,

                    })?.RemainingMarks ?? model.SectionMarks;
                }
               
            }

            return PartialView("_AddQuestion", model);
        }

        public PartialViewResult QuestionList()
        {
            return PartialView("_listQuestion", GetPaperDetails());
        }

        public PartialViewResult QuestionView(string id)
        {
            return PartialView("_QuestionView", SelectPaperDetails(id));
        }
        [HttpPost]
        public JsonResult SavePaperDetails(QuestionSetUpViewModel model)
        {

            model.PaperID = onlineExamService.SelectPaper(new OnlineExam.Request.PaperRequestDTO()
            {
                CourseID = model.CourseID,
                LoginID = User.UserId,
                ExaminationSectionID = model.ExaminationSectionID,
                CreatedBy = User.UserId,

            })?.PaperID;
            var type = "INSERT";
            if (model.PaperID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SavePaper(new OnlineExam.Request.PaperRequestDTO()
            {

                CreatedBy = User.UserId,
                Active = model.Active,
                CourseID = model.CourseID,
                PaperID = model.PaperID,
                SectionName = model.SectionName,
                ExaminationSectionID = model.ExaminationSectionID,
                LoginID = User.UserId

            }, type);
            model.PaperID = onlineExamService.SelectPaper(new OnlineExam.Request.PaperRequestDTO()
            {
                CourseID = model.CourseID,
                LoginID = User.UserId,
                ExaminationSectionID = model.ExaminationSectionID,
                CreatedBy = User.UserId,

            }).PaperID;
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
                SectionMarks = model.SectionMarks,
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
                QuestionNo = model.QuestionNo == 0 ? 1 : model.QuestionNo,
                QuestionText = model.QuestionText,
                QuestionType = model.QuestionType,
                TextOrImageQuestion = model.TextOrImageQuestion
            }, Detailstype);
            return Json(resultDetails, JsonRequestBehavior.AllowGet);
        }



        private List<QuestionSetUpViewModel> GetPaperDetails()
        {
            var itemSet = new List<QuestionSetUpViewModel>();


            itemSet = onlineExamService.GetAllPaperDetails(new OnlineExam.Request.PaperDetailsRequestDTO() { LoginID = User.UserId }).Select(a => new QuestionSetUpViewModel()
            {
                ExaminationSectionID = a.ExaminationSectionID,
                Active = a.Status,
                AnswerNo = a.AnswerNo,
                AnswerText = a.AnswerText,               
                AudioOrVideoQuestion = a.AudioOrVideoQuestion,
                OptionANo = a.OptionANo,
                OptionAText = a.OptionAText,
                OptionBNo = a.OptionBNo,
                OptionBText = a.OptionBText,
                OptionCNo = a.OptionCNo,
                OptionCText = a.OptionCText,
                OptionDNo = a.OptionDNo,
                OptionDText = a.OptionDText,
                OptionEText = a.OptionEText,
                OptionENo = a.OptionENo,
                PaperID = a.PaperID,
                PaperDetailsID = a.PaperDetailsID,
                QuestionMarks = a.QuestionMarks,
                QuestionNo = Convert.ToInt32(a.QuestionNo),
                QuestionType = a.QuestionType,
                SectionMarks = a.SectionMarks,
                TextOrImageQuestion = a.TextOrImageQuestion,
                QuestionText = HttpUtility.HtmlDecode(a.QuestionText),
                RemainingMarks=a.RemainingMarks

            }).ToList();


            return itemSet;
        }

        private QuestionSetUpViewModel SelectPaperDetails(string paperDetailsID)
        {
            var model = new QuestionSetUpViewModel();


            var itemSet = onlineExamService.SelectAllPaperDetails(new OnlineExam.Request.PaperDetailsRequestDTO() { PaperDetailsID = Convert.ToInt32(paperDetailsID) });
            model.Active = itemSet.Status;
            model.AnswerNo = itemSet.AnswerNo;
            model.AnswerText = itemSet.AnswerText;
            model.AudioOrVideoQuestion = itemSet.AudioOrVideoQuestion;
            model.OptionANo = itemSet.OptionANo;
            model.OptionAText = itemSet.OptionAText;
            model.OptionBNo = itemSet.OptionBNo;
            model.OptionBText = itemSet.OptionBText;
            model.OptionCNo = itemSet.OptionCNo;
            model.OptionCText = itemSet.OptionCText;
            model.OptionDNo = itemSet.OptionDNo;
            model.OptionDText = itemSet.OptionDText;
            model.OptionEText = itemSet.OptionEText;
            model.OptionENo = itemSet.OptionENo;
            model.PaperID = itemSet.PaperID;
            model.PaperDetailsID = itemSet.PaperDetailsID;
            model.QuestionMarks = itemSet.QuestionMarks;
            model.QuestionNo = Convert.ToInt32(itemSet.QuestionNo);
            model.QuestionType = itemSet.QuestionType;
            model.SectionMarks = itemSet.SectionMarks;
            model.TextOrImageQuestion = itemSet.TextOrImageQuestion;
            model.QuestionText = itemSet.QuestionText;
            model.RemainingMarks = itemSet.RemainingMarks;

            return model;
        }


        [HttpPost]
        public JsonResult DeletePaperDetails(QuestionSetUpViewModel model)
        {

            var result = onlineExamService.SavePaperDetails(new OnlineExam.Request.PaperDetailsRequestDTO()
            {

                CreatedBy = User.UserId,
                PaperDetailsID = model.PaperDetailsID


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}