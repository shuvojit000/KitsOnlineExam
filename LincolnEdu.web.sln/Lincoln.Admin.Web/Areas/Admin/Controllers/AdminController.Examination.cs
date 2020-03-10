using Lincoln.Admin.Web.Models;
using Lincoln.OnlineExam.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Lincoln.Admin.Web.Areas.Admin.Controllers
{
    public partial class AdminController
    {
        #region Examination Name 

        public ActionResult ExaminationName() => View();

        private List<ExaminationNameViewModel> GetAllExaminationName()
        {
            string d = DateTime.Now.Date.ToShortDateString();
            var itemSet = new List<ExaminationNameViewModel>();
            itemSet = onlineExamService.GetAllExaminationName().Select(a => new ExaminationNameViewModel()
            {
                ExaminationNameID = a.ExaminationNameID,
                ExaminationName = a.ExaminationName,
                StartDate = a.StartDate != null ? Convert.ToDateTime(a.StartDate).ToString("dd/MM/yyyy") : string.Empty,
                EndDate = a.EndDate != null ? Convert.ToDateTime(a.EndDate).ToString("dd/MM/yyyy") : string.Empty,
                Active = a.Status,
                ModifiedOn = a.ModifiedOn?.Date,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }
        private ExaminationNameViewModel SelectExaminationName(string ExaminationNameId)
        {
            var model = new ExaminationNameViewModel();
            var item = onlineExamService.SelectExaminationName(new OnlineExam.Request.ExaminationNameRequestDTO
            {
                ExaminationNameID = Convert.ToInt32(ExaminationNameId)

            });
            model.ExaminationNameID = item.ExaminationNameID;
            model.ExaminationName = item.ExaminationName;
            model.StartDate = item.StartDate != null ? Convert.ToDateTime(item.StartDate).ToString("dd/MM/yyyy") : string.Empty;
            model.EndDate = item.EndDate != null ? Convert.ToDateTime(item.EndDate).ToString("dd/MM/yyyy") : string.Empty;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            //model.AcademicID = item.AcademicID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            return model;

        }

        public PartialViewResult AddExaminationName(string id)
        {
            var model = new ExaminationNameViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectExaminationName(id);
            }
            return PartialView("_addExaminationName", model);
        }
        public PartialViewResult ExaminationNameView(string id)
        {
            return PartialView("_viewExaminationName", SelectExaminationName(id));
        }
        public PartialViewResult ExaminationNameList()
        {
            return PartialView("_listExaminationName", GetAllExaminationName());
        }
        [HttpPost]
        public JsonResult SaveExaminationName(ExaminationNameViewModel model)
        {
            var type = "INSERT";
            if (model.ExaminationNameID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveExaminationName(new OnlineExam.Request.ExaminationNameRequestDTO()
            {
                ExaminationNameID = model.ExaminationNameID,
                ExaminationName = model.ExaminationName,
                CreatedBy = User.UserId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteExaminationName(ExaminationNameViewModel model)
        {

            var result = onlineExamService.SaveExaminationName(new OnlineExam.Request.ExaminationNameRequestDTO()
            {

                CreatedBy = User.UserId,
                ExaminationNameID = model.ExaminationNameID


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Assesssment 

        public ActionResult Assessment() => View();

        private List<AssessmentViewModel> GetAllAssessment()
        {
            var itemSet = new List<AssessmentViewModel>();
            itemSet = onlineExamService.GetAllAssessment().Select(a => new AssessmentViewModel()
            {

                AssessmentID = a.AssessmentID,
                AcademicName = a.AcademicName,
                AcademicID = a.AcademicID,
                DepartmentID = a.DepartmentID,
                DepartmentName = a.DepartmentName,
                ProgramVersioningID = a.ProgrammeVersioningID,
                Version = a.Version,
                ProgrammeID = a.ProgrammeID,
                ProgramName = a.ProgramName,
                AssessmentName = a.AssessmentName,
                AssessmentType = a.AssessmentType,
                Active = a.Status,
                ModifiedOn = a.ModifiedOn?.Date,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                CountryID = a.CountryID,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
                CountryName = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : a.CountryID == 3 ? "United States" : string.Empty,
            }).ToList();

            return itemSet;
        }
        private AssessmentViewModel SelectAssessment(string AssesssmentId)
        {
            var model = new AssessmentViewModel();
            var item = onlineExamService.SelectAssessment(new OnlineExam.Request.AssessmentRequestDTO
            {
                AssessmentID = Convert.ToInt32(AssesssmentId)

            });
            model.AssessmentID = item.AssessmentID;
            model.AcademicID = item.AcademicID;
            model.AcademicName = item.AcademicName;
            model.ProgrammeID = item.ProgrammeID;
            model.DepartmentID = item.DepartmentID;
            model.ProgramVersioningID = item.ProgrammeVersioningID;
            model.ProgramName = item.ProgramName;
            model.DepartmentName = item.DepartmentName;
            model.Version = item.Version;
            model.AssessmentType = item.AssessmentType;
            model.AssessmentName = item.AssessmentName;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            model.CountryID = item.CountryID;
            model.CountryName = (item.CountryID == 1) ? "India" : (item.CountryID == 2) ? "Malaysia" : item.CountryID == 3 ? "United States" : string.Empty;
            return model;

        }

        public PartialViewResult AddAssessment(string id)
        {
            var model = new AssessmentViewModel();

            if (!string.IsNullOrEmpty(id))
            {
                model = SelectAssessment(id);

                model.DepartmentList = onlineExamService.GetAllDepartment().Where(a => a.AcademicID == model.AcademicID && a.Status == "A")
                     .Select(a => new SelectListItem
                     {
                         Text = a.DepartmentName + "(" + a.DepartmentCode + ")",
                         Value = a.DepartmentID.ToString()

                     }).ToList();

                model.ProgramList = onlineExamService.GetAllProgrammeWithVersion().Where(a => a.DepartmentID == Convert.ToInt32(model.DepartmentID) && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.ProgrammeName + "(" + a.Version + ")",
                           Value = a.ProgramVersioningID.ToString()

                       }).ToList();

            }
            else
            {
                model.ProgramList = new List<SelectListItem>();
                model.SyllabusVersionList = new List<SelectListItem>();
                model.DepartmentList = new List<SelectListItem>();
            }
            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();

            model.CountryList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="India", Value = "1" },
                                new SelectListItem{ Text="Malaysia", Value = "2" },
                                new SelectListItem{ Text="United States", Value = "3" },
                             };
            return PartialView("_addAssessment", model);
        }
        public PartialViewResult AssessmentView(string id)
        {
            return PartialView("_viewAssessment", SelectAssessment(id));
        }
        public PartialViewResult AssessmentList()
        {
            return PartialView("_listAssessment", GetAllAssessment());
        }
        [HttpPost]
        public JsonResult SaveAssessment(AssessmentViewModel model)
        {
            var type = "INSERT";
            if (model.AssessmentID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveAssessment(new OnlineExam.Request.AssessmentRequestDTO()
            {
                AssessmentID = model.AssessmentID,
                CreatedBy = User.UserId,
                DepartmentID = model.DepartmentID,
                ProgrammeID = model.ProgrammeID,
                AssessmentType = model.AssessmentType,
                AssessmentName = model.AssessmentName,
                ProgrammeVersioningID = model.ProgramVersioningID,
                CountryID = model.CountryID,
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteAssessment(AssessmentViewModel model)
        {

            var result = onlineExamService.SaveAssessment(new OnlineExam.Request.AssessmentRequestDTO()
            {

                CreatedBy = User.UserId,
                AssessmentID = model.AssessmentID


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Examination Section 

        public ActionResult ExaminationSection() => View();

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
                MaximumMarks = a.MaximumMarks,
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
            model.AcademicID = item.AcademicID;
            model.AcademicName = item.AcademicName;
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
            model.MaximumMarks = item.MaximumMarks;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            return model;

        }

        public PartialViewResult AddExaminationSection(string id)
        {
            var model = new ExaminationSectionViewModel();

            if (!string.IsNullOrEmpty(id))
            {
                model = SelectExaminationSection(id);


                model.FacultyList = onlineExamService.GetAllDepartment().Where(a => a.AcademicID == model.AcademicID && a.Status == "A")
                                   .Select(a => new SelectListItem
                                   {
                                       Text = a.DepartmentName + "(" + a.DepartmentCode + ")",
                                       Value = a.DepartmentID.ToString()

                                   }).ToList();

                model.ProgramList = onlineExamService.GetAllProgrammeWithVersion().Where(a => a.DepartmentID == Convert.ToInt32(model.FacultyCode) && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.ProgrammeName + "(" + a.Version + ")",
                           Value = a.ProgramVersioningID.ToString()

                       }).ToList();


                model.SemisterList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeSemesterID == Convert.ToInt32(model.SemisterCode)
                && a.Status == "A" && a.ProgrammeVersioningID == model.ProgrammeVersioningID)
                      .Select(a => new SelectListItem
                      {
                          Text = a.ProgrammeSemester.ToString(),
                          Value = a.ProgrammeSemesterID.ToString()

                      }).ToList();
                model.CourseList = onlineExamService.GetAllCourse().Where(a => a.ProgramVersioningID == Convert.ToInt32(model.ProgrammeVersioningID)
                && a.CountryId == model.CountryCode
                && a.ProgrammeYear == model.AcademicYearCode && a.Status == "A" && a.ProgrammeSemesterID == model.SemisterCode)
                      .Select(a => new SelectListItem
                      {
                          Text = a.CourseName + "(" + a.CourseCode + ")",
                          Value = a.CourseID.ToString()

                      }).ToList();
                var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgrammeVersioningID)
                && a.Status == "A").ToList();
                model.CountryList = itemList?.Select(a => new SelectListItem() { Text = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : "United States", Value = a.CountryID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                          .Select(g => g.FirstOrDefault())
                                          .ToList();
                model.SemisterList = itemList?.Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgrammeVersioningID)
                && a.CountryID == model.CountryCode
                && a.ProgrammeYear == model.AcademicYearCode).Select(a => new SelectListItem() { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemesterID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
                model.AcademicYearList = itemList?.Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgrammeVersioningID) &&
                a.CountryID == model.CountryCode).Select(a => new SelectListItem() { Text = a.ProgrammeYear.ToString(), Value = a.ProgrammeYear.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();



                model.SectionList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="A", Value = "A" },
                                new SelectListItem{ Text="B", Value = "B" },
                                new SelectListItem{ Text="C", Value = "C" },
                                 new SelectListItem{ Text="D", Value = "D" },
                                  new SelectListItem{ Text="E", Value = "E" },
                             };

            }
            else
            {
                model.FacultyList = new List<SelectListItem>();
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
                model.AcademicYearList = new List<SelectListItem>();
            }
            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();

            return PartialView("_addExaminationSection", model);
        }
        public PartialViewResult ExaminationSectionView(string id)
        {
            return PartialView("_viewExaminationSection", SelectExaminationSection(id));
        }
        public PartialViewResult ExaminationSectionList()
        {
            return PartialView("_listExaminationSection", GetAllExaminationSection());
        }
        [HttpPost]
        public JsonResult SaveExaminationSection(ExaminationSectionViewModel model)
        {
            var type = "INSERT";
            if (model.ExaminationSectionID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveExaminationSection(new OnlineExam.Request.ExaminationSectionRequestDTO()
            {
                ExaminationSectionID = model.ExaminationSectionID,
                CreatedBy = User.UserId,
                ProgramCode = model.ProgramCode,
                CourseCode = model.CourseCode,
                SemisterCode = model.SemisterCode,
                ProgrammeVersioningID = model.ProgrammeVersioningID,
                CountryCode = model.CountryCode,
                FacultyCode = model.FacultyCode,
                AcademicYearCode = model.AcademicYearCode,
                SectionName = model.SectionName,
                QuestionType = model.QuestionType,
                MaximumMarks = model.MaximumMarks,
                Active = model.Active
            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteExaminationSection(ExaminationSectionViewModel model)
        {

            var result = onlineExamService.SaveExaminationSection(new OnlineExam.Request.ExaminationSectionRequestDTO()
            {

                CreatedBy = User.UserId,
                ExaminationSectionID = model.ExaminationSectionID


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCourseExam(string programVersioningID, string countryId, string programmeYear, string programmeSem)
        {

            return Json(onlineExamService.GetAllCourse().Where(a => a.ProgramVersioningID == Convert.ToInt32(programVersioningID)
                                                                    && a.CountryId == Convert.ToInt32(countryId)
                                                                    && a.ProgrammeYear == Convert.ToInt32(programmeYear)
                                                                     && a.ProgrammeSemesterID == Convert.ToInt32(programmeSem) && a.Status == "A")
                    .Select(a => new SelectListItem
                    {
                        Text = a.CourseName + "(" + a.CourseCode + ")",
                        Value = a.CourseID.ToString()

                    }).ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Subject Assesssment 

        public ActionResult SubjectAssessment() => View();

        private List<SubjectAssessmentViewModel> GetAllSubjectAssessment()
        {
            var itemSet = new List<SubjectAssessmentViewModel>();
            itemSet = onlineExamService.GetAllSubjectAssessment().Select(a => new SubjectAssessmentViewModel()
            {
                SubjectAssessmentID = a.SubjectAssessmentID,
                AcademicID = a.AcademicID,
                AcademicName = a.AcademicName,
                ProgramCode = a.ProgramCode,
                ProgramName = a.ProgramName,
                CourseCode = a.CourseCode,
                CourseName = onlineExamService.SelectCourse(new OnlineExam.Request.CourseRequestDTO() { CourseID = a.CourseID }).CourseName,
                CountryName = a.CountryName,
                SemisterCode = a.SemisterCode,
                SemisterName = a.SemisterName,
                ProgrammeVersioningID = a.ProgrammeVersioningID,
                Version = a.Version,
                FacultyCode = a.FacultyCode,
                FacultyName = a.FacultyName,
                AcademicYearCode = a.AcademicYearCode,
                YearName = a.YearName,
                Active = a.Status,
                ModifiedOn = a.ModifiedOn?.Date,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }
        private SubjectAssessmentResponseDTO SelectSubjectAssessment(string SubjectAssessmentId)
        {
            var model = new SubjectAssessmentResponseDTO();
            model.SubAssessmentDetails = new List<OnlineExam.Response.SubjectAssessmentDetails>();
            var item = onlineExamService.SelectSubjectAssessment(new OnlineExam.Request.SubjectAssessmentRequestDTO
            {
                SubjectAssessmentID = Convert.ToInt32(SubjectAssessmentId)

            });
            var itemSet = new List<SubjectAssessmentViewModel>();
            itemSet = onlineExamService.GetAllSubjectAssessment().Select(a => new SubjectAssessmentViewModel()
            {
                SubjectAssessmentID = a.SubjectAssessmentID,
                ProgramCode = a.ProgramCode,
                AcademicID = a.AcademicID,
                AcademicName = a.AcademicName,
                ProgramName = a.ProgramName,
                CourseID = a.CourseID,
                CourseCode = a.CourseID,
                CountryName = (a.CountryCode == 1) ? "India" : (a.CountryCode == 2) ? "Malaysia" : a.CountryCode == 3 ? "United States" : string.Empty,
                CountryCode = a.CountryCode,
                SemisterCode = a.SemisterCode,
                SemisterName = onlineExamService.GetAllProgrammeSemester().Where(x => x.ProgrammeSemesterID == Convert.ToInt32(a.ProgramCode) && a.Status == "A").Select(x => x.SemesterType).SingleOrDefault(),
                ProgrammeVersioningID = a.ProgrammeVersioningID,
                Version = a.Version,
                CourseName = onlineExamService.GetAllCourse().Where(x => x.CourseID == Convert.ToInt32(a.CourseID) && a.Status == "A").Select(x => x.CourseName).SingleOrDefault(),
                FacultyCode = a.FacultyCode,
                FacultyName = a.FacultyName,
                AcademicYearCode = a.AcademicYearCode,
                YearName = a.YearName,
                Active = a.Status,
                ModifiedOn = a.ModifiedOn?.Date,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).Where(x => x.SubjectAssessmentID == Convert.ToInt32(SubjectAssessmentId)).ToList();
            model.AcademicID = Convert.ToInt32(itemSet.ElementAt(0).AcademicID);
            model.SubAssessmentDetails = item.SubAssessmentDetails;
            model.SubjectAssessmentID = Convert.ToInt32(itemSet.ElementAt(0).SubjectAssessmentID);
            model.ProgramCode = Convert.ToInt32(itemSet.ElementAt(0).ProgramCode);
            model.CourseCode = Convert.ToInt32(itemSet.ElementAt(0).CourseCode);
            model.CourseID = item.CourseID;
            model.SemisterCode = Convert.ToInt32(itemSet.ElementAt(0).SemisterCode);
            model.ProgrammeVersioningID = Convert.ToInt32(itemSet.ElementAt(0).ProgrammeVersioningID);
            model.CountryCode = Convert.ToInt32(itemSet.ElementAt(0).CountryCode);
            model.FacultyCode = Convert.ToInt32(itemSet.ElementAt(0).FacultyCode);
            model.AcademicYearCode = Convert.ToInt32(itemSet.ElementAt(0).AcademicYearCode);
            model.AcademicName = itemSet.ElementAt(0).AcademicName;

            model.ProgramName = itemSet.ElementAt(0).ProgramName;
            model.CourseName = itemSet.ElementAt(0).CourseName;
            model.SemisterName = itemSet.ElementAt(0).SemisterCode.ToString();
            model.Version = itemSet.ElementAt(0).Version;
            model.CountryName = itemSet.ElementAt(0).CountryName;
            model.FacultyName = itemSet.ElementAt(0).FacultyName;
            model.YearName = itemSet.ElementAt(0).YearName;
            model.ModifiedOn = itemSet.ElementAt(0).ModifiedOn?.Date;


            model.CreatedBy = Convert.ToInt32(itemSet.ElementAt(0).CreatedBy);
            model.CreatedOn = itemSet.ElementAt(0).CreatedOn;
            model.ModifiedBy = Convert.ToInt32(itemSet.ElementAt(0).ModifiedBy);
            return model;

        }

        public PartialViewResult AddSubjectAssessment(string id)
        {
            var model = new SubjectAssessmentResponseDTO();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectSubjectAssessment(id);


                model.FacultyList = onlineExamService.GetAllDepartment().Where(a => a.AcademicID == model.AcademicID && a.Status == "A")
                                   .Select(a => new SelectListItem
                                   {
                                       Text = a.DepartmentName + "(" + a.DepartmentCode + ")",
                                       Value = a.DepartmentID.ToString()

                                   }).ToList();
                model.ProgramList = onlineExamService.GetAllProgrammeWithVersion().Where(a => a.DepartmentID == Convert.ToInt32(model.FacultyCode) && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.ProgrammeName + "(" + a.Version + ")",
                           Value = a.ProgramVersioningID.ToString()

                       }).ToList();

                model.CourseList = onlineExamService.GetAllCourse().Where(a => a.ProgramVersioningID == Convert.ToInt32(model.ProgrammeVersioningID)
                && a.CountryId == model.CountryCode
                && a.ProgrammeYear == model.AcademicYearCode && a.Status == "A" && a.ProgrammeSemesterID == model.SemisterCode)
                      .Select(a => new SelectListItem
                      {
                          Text = a.CourseName + "(" + a.CourseCode + ")",
                          Value = a.CourseID.ToString()

                      }).ToList();
                var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgrammeVersioningID)
                && a.ProgrammeYear == model.AcademicYearCode
                && a.Status == "A").ToList();
                model.CountryList = itemList?.Select(a => new SelectListItem() { Text = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : "United States", Value = a.CountryID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                          .Select(g => g.FirstOrDefault())
                                          .ToList();
                model.SemisterList = itemList?.Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgrammeVersioningID) && a.CountryID == model.CountryCode
                && a.ProgrammeYear == model.AcademicYearCode).Select(a => new SelectListItem() { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemesterID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
                model.AcademicYearList = itemList?.Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgrammeVersioningID) &&
                a.CountryID == model.CountryCode).Select(a => new SelectListItem() { Text = a.ProgrammeYear.ToString(), Value = a.ProgrammeYear.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
            }
            else
            {
                model.FacultyList = new List<SelectListItem>();
                model.ProgramList = new List<SelectListItem>();
                model.SyllabusVersionList = new List<SelectListItem>();
                model.SemisterList = new List<SelectListItem>();// onlineExamService.GetAllProgrammeSemester().Select(a => new SelectListItem { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemesterID.ToString() }).ToList();
                model.CourseList = new List<SelectListItem>();
                model.CountryList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="India", Value = "1" },
                                new SelectListItem{ Text="Malaysia", Value = "2" },
                                new SelectListItem{ Text="United States", Value = "3" },
                             };
                model.AcademicYearList = new List<SelectListItem>();// Enumerable.Range((DateTime.Now.Year - 9), 10).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();
            }
            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();

            return PartialView("_addSubjectAssessment", model);
        }
        public PartialViewResult SubjectAssessmentView(string id)
        {
            return PartialView("_viewSubjectAssessment", SelectSubjectAssessment(id));
        }
        public PartialViewResult SubjectAssessmentList()
        {
            return PartialView("_listSubjectAssessment", GetAllSubjectAssessment());
        }
        [HttpPost]
        public JsonResult SaveSubjectAssessment(SubjectAssessmentViewModel model)
        {
            var type = "INSERT";
            if (model.SubjectAssessmentID > 0)
            {
                type = "UPDATE";
            }
            XElement xEle = null;
            if (model.SubAssessmentDetails.Any())
            {
                xEle = new XElement("Assessments",
                        from emp in model.SubAssessmentDetails
                        select new XElement("Assessment",
                                     new XElement("AssessmentName", emp.AssessmentName),
                                       new XElement("AssessmentPercentage", emp.AssessmentPercentage),
                                       new XElement("FullMarks", emp.FullMarks),
                                       new XElement("AssessmentType", emp.AssessmentType),
                                       new XElement("OpenClose", emp.OpenClose)
                                   ));
            }

            var result = onlineExamService.SaveSubjectAssessment(new OnlineExam.Request.SubjectAssessmentRequestDTO()
            {
                SubjectAssessmentID = Convert.ToInt32(model.SubjectAssessmentID),
                CreatedBy = User.UserId,
                ProgramCode = Convert.ToInt32(model.ProgramCode),
                CourseCode = Convert.ToInt32(model.CourseCode),
                SemisterCode = Convert.ToInt32(model.SemisterCode),
                ProgrammeVersioningID = Convert.ToInt32(model.ProgrammeVersioningID),
                CountryCode = Convert.ToInt32(model.CountryCode),
                FacultyCode = Convert.ToInt32(model.FacultyCode),
                AcademicYearCode = Convert.ToInt32(model.AcademicYearCode),
                TabAssessmentDetails = xEle.ToString().Trim(),
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteSubjectAssessment(SubjectAssessmentViewModel model)
        {

            var result = onlineExamService.SaveSubjectAssessment(new OnlineExam.Request.SubjectAssessmentRequestDTO()
            {

                CreatedBy = User.UserId,
                SubjectAssessmentID = Convert.ToInt32(model.SubjectAssessmentID)


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetAssessmentById(string departmentId, string programmeVersioningId)
        {

            return Json(GetAllAssessment().Where(a => a.ProgramVersioningID == Convert.ToInt32(programmeVersioningId)
            && a.DepartmentID == Convert.ToInt32(departmentId)
            ), JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}