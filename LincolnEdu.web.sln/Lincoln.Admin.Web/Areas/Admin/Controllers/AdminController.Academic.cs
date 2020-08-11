using Lincoln.Admin.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Areas.Admin.Controllers
{
    public partial class AdminController
    {
        #region Academic Level
        public ActionResult AcademicLevel() => View();

        private List<AcademicViewModel> GetAllAcademicLevel()
        {
            var itemSet = new List<AcademicViewModel>();
            itemSet = onlineExamService.GetAllAcademicLevel().Select(a => new AcademicViewModel()
            {
                AcademicName = a.AcademicName,
                AcademicCode = a.AcademicCode,
                ModifiedOn = a.ModifiedOn?.Date,
                Status = a.Status,
                AcademicID = a.AcademicID,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }

        private AcademicViewModel SelectAcademicLevel(string academicId)
        {
            var model = new AcademicViewModel();
            var item = onlineExamService.SelectAcademicLevel(new OnlineExam.Request.AcademicLevelRequestDTO
            {
                AcademicID = Convert.ToInt32(academicId)

            });
            model.AcademicName = item.AcademicName;
            model.AcademicCode = item.AcademicCode;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            model.AcademicID = item.AcademicID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);

            return model;
        }

        public PartialViewResult AddAcademicLevel(string id)
        {
            var model = new AcademicViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectAcademicLevel(id);
            }
            return PartialView("_addAcademicLevel", model);
        }

        public PartialViewResult AcademicLevelView(string id)
        {
            return PartialView("_viewAcademicLevel", SelectAcademicLevel(id));
        }

        public PartialViewResult AcademicLevelList()
        {
            return PartialView("_listAcademicLevel", GetAllAcademicLevel());
        }

        [HttpPost]
        public JsonResult SaveAcademicLevel(AcademicViewModel model)
        {
            var type = "INSERT";
            if (model.AcademicID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveAcademicLevel(new OnlineExam.Request.AcademicLevelRequestDTO()
            {
                AcademicID = model.AcademicID,
                CreatedBy = User.UserId,
                AcademicName = model.AcademicName,
                AcademicCode = model.AcademicCode,
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAcademicLevel(AcademicViewModel model)
        {
            var result = onlineExamService.SaveAcademicLevel(new OnlineExam.Request.AcademicLevelRequestDTO()
            {
                AcademicID = model.AcademicID,
                CreatedBy = User.UserId,
                AcademicName = model.AcademicName,
                AcademicCode = model.AcademicCode,

            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Department
        public ActionResult Department() => View();

        private List<DepartmentViewModel> GetAllDepartment()
        {
            var itemSet = new List<DepartmentViewModel>();
            itemSet = onlineExamService.GetAllDepartment().Select(a => new DepartmentViewModel()
            {
                AcademicName = a.AcademicName,
                DepartmentCode = a.DepartmentCode,
                DepartmentName = a.DepartmentName,
                DepartmentID = a.DepartmentID,
                HODEmail = a.HODEmail,
                HODName = a.HODName,
                ModifiedOn = a.ModifiedOn?.Date,
                Status = a.Status,
                AcademicID = a.AcademicID,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }

        private DepartmentViewModel SelectDepartment(string departmentId)
        {
            var model = new DepartmentViewModel();
            var item = onlineExamService.SelectDepartment(new OnlineExam.Request.DepartmentRequestDTO
            {
                DepartmentID = Convert.ToInt32(departmentId),
                AcademicID = null

            });
            model.AcademicName = item.AcademicName;
            model.DepartmentID = item.DepartmentID;
            model.DepartmentName = item.DepartmentName;
            model.DepartmentCode = item.DepartmentCode;
            model.HODName = item.HODName;
            model.HODEmail = item.HODEmail;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            model.AcademicID = item.AcademicID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);

            return model;
        }

        public PartialViewResult AddDepartment(string id)
        {
            var model = new DepartmentViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectDepartment(id);
            }
            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
            return PartialView("_addDepartment", model);
        }

        public PartialViewResult DepartmentView(string id)
        {
            return PartialView("_viewDepartment", SelectDepartment(id));
        }

        public PartialViewResult DepartmentList()
        {
            return PartialView("_listDepartment", GetAllDepartment());
        }

        [HttpPost]
        public JsonResult SaveDepartment(DepartmentViewModel model)
        {
            var type = "INSERT";
            if (model.DepartmentID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveDepartment(new OnlineExam.Request.DepartmentRequestDTO()
            {
                AcademicID = model.AcademicID,
                CreatedBy = User.UserId,
                DepartmentCode = model.DepartmentCode,
                HODEmail = model.HODEmail,
                HODName = model.HODName,
                DepartmentName = model.DepartmentName,
                DepartmentID = model.DepartmentID,
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteDepartment(DepartmentViewModel model)
        {

            var result = onlineExamService.SaveDepartment(new OnlineExam.Request.DepartmentRequestDTO()
            {

                CreatedBy = User.UserId,
                DepartmentID = model.DepartmentID


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Programme

        public ActionResult Programme() => View();

        private List<ProgrammeViewModel> GetAllProgramme()
        {
            var itemSet = new List<ProgrammeViewModel>();
            itemSet = onlineExamService.GetAllProgramme().Select(a => new ProgrammeViewModel()
            {
                ProgrammeID = a.ProgrammeID,
                ProgrammeName = a.ProgrammeName,
                AcademicName = a.AcademicName,
                ProgrammeCode = a.ProgrammeCode,
                DepartmentName = a.DepartmentName,
                DepartmentID = a.DepartmentID,
                ApprovalNo = a.ApprovalNo,
                Credit = a.Credit,
                ModifiedOn = a.ModifiedOn?.Date,
                Status = a.Status,
                AcademicID = a.AcademicID,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }

        private ProgrammeViewModel SelectProgramme(string programmeId)
        {
            var model = new ProgrammeViewModel();
            var item = onlineExamService.SelectProgramme(new OnlineExam.Request.ProgrammeRequestDTO
            {
                ProgrammeID = Convert.ToInt32(programmeId),
            });
            model.ProgrammeID = item.ProgrammeID;
            model.ProgrammeName = item.ProgrammeName;
            model.AcademicName = item.AcademicName;
            model.DepartmentID = item.DepartmentID;
            model.DepartmentName = item.DepartmentName;
            model.ProgrammeCode = item.ProgrammeCode;
            model.ApprovalNo = item.ApprovalNo;
            model.Credit = item.Credit;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            model.AcademicID = item.AcademicID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);

            return model;
        }

        public PartialViewResult AddProgramme(string id)
        {
            var model = new ProgrammeViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectProgramme(id);
                model.DepartmentList = onlineExamService.GetAllDepartment().Where(a => a.AcademicID == model.AcademicID && a.Status == "A")
                    .Select(a => new SelectListItem
                    {
                        Text = a.DepartmentName + "(" + a.DepartmentCode + ")",
                        Value = a.DepartmentID.ToString()

                    }).ToList();
            }
            else
            {
                model.DepartmentList = new List<SelectListItem>();
            }
            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();

            return PartialView("_addProgramme", model);
        }

        public PartialViewResult ProgrammeView(string id)
        {
            return PartialView("_viewProgramme", SelectProgramme(id));
        }

        public PartialViewResult ProgrammeList()
        {
            return PartialView("_listProgramme", GetAllProgramme());
        }

        [HttpPost]
        public JsonResult SaveProgramme(ProgrammeViewModel model)
        {
            var type = "INSERT";
            if (model.ProgrammeID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveProgramme(new OnlineExam.Request.ProgrammeRequestDTO()
            {
                AcademicID = model.AcademicID,
                CreatedBy = User.UserId,
                ProgrammeCode = model.ProgrammeCode,
                ApprovalNo = model.ApprovalNo,
                Credit = model.Credit,
                ProgrammeName = model.ProgrammeName,
                ProgrammeID = model.ProgrammeID,
                DepartmentID = model.DepartmentID,
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteProgramme(ProgrammeViewModel model)
        {

            var result = onlineExamService.SaveProgramme(new OnlineExam.Request.ProgrammeRequestDTO()
            {

                CreatedBy = User.UserId,
                ProgrammeID = model.ProgrammeID


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Programme Versioning

        public ActionResult ProgrammeVersioning() => View();

        private List<ProgramVersioningViewModel> GetAllProgramVersioning()
        {
            var itemSet = new List<ProgramVersioningViewModel>();
            itemSet = onlineExamService.GetAllProgramVersioning().Select(a => new ProgramVersioningViewModel()
            {
                ProgramVersioningID = a.ProgramVersioningID,
                DepartmentCode = a.DepartmentCode,
                DepartmentName = a.DepartmentName,
                Version = a.Version,
                ProgramCode = a.ProgramCode,
                ProgramName = a.ProgramName,
                AcademicID = a.AcademicID,
                AcademicName = a.AcademicName,
                Credit = a.Credit,
                Active = a.Status,
                ModifiedOn = a.ModifiedOn?.Date,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }

        private ProgramVersioningViewModel SelectProgramVersioning(string programVersioningId)
        {
            var model = new ProgramVersioningViewModel();
            var item = onlineExamService.SelectProgramVersioning(new OnlineExam.Request.ProgramVersioningRequestDTO
            {
                ProgramVersioningID = Convert.ToInt32(programVersioningId)
            });
            model.ProgramVersioningID = item.ProgramVersioningID;
            model.ProgramCode = item.ProgramCode;
            model.DepartmentCode = item.DepartmentCode;
            model.Version = item.Version;
            model.AcademicID = item.AcademicID;
            model.AcademicName = item.AcademicName;
            model.Credit = item.Credit;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.ProgramName = item.ProgramName;
            model.DepartmentName = item.DepartmentName;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);

            return model;

        }

        public PartialViewResult AddProgramVersioning(string id)
        {
            var model = new ProgramVersioningViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectProgramVersioning(id);
                model.ProgramList = onlineExamService.GetAllProgramme().Where(a => a.DepartmentID == Convert.ToInt32(model.DepartmentCode) && a.Status == "A")
                    .Select(a => new SelectListItem
                    {
                        Text = a.ProgrammeName + "(" + a.ProgrammeCode + ")",
                        Value = a.ProgrammeID.ToString()

                    }).ToList();
                model.DepartmentList = onlineExamService.GetAllDepartment().Where(a => a.AcademicID == model.AcademicID && a.Status == "A")
                   .Select(a => new SelectListItem
                   {
                       Text = a.DepartmentName + "(" + a.DepartmentCode + ")",
                       Value = a.DepartmentID.ToString()

                   }).ToList();
            }
            else
            {
                model.ProgramList = new List<SelectListItem>();
                model.DepartmentList = new List<SelectListItem>();
            }
            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
            return PartialView("_addProgramVersioning", model);
        }

        public PartialViewResult ProgramVersioningView(string id)
        {
            return PartialView("_viewProgramVersioning", SelectProgramVersioning(id));
        }

        public PartialViewResult ProgramVersioningList()
        {
            return PartialView("_listProgramVersioning", GetAllProgramVersioning());
        }

        [HttpPost]
        public JsonResult SaveProgramVersioning(ProgramVersioningViewModel model)
        {
            var type = "INSERT";
            if (model.ProgramVersioningID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveProgramVersioning(new OnlineExam.Request.ProgramVersioningRequestDTO()
            {
                ProgramVersioningID = model.ProgramVersioningID,
                CreatedBy = User.UserId,
                DepartmentCode = model.DepartmentCode,
                Credit = model.Credit,
                Version = model.Version,
                ProgramCode = model.ProgramCode,
                Active = model.Active
            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteProgramVersioning(ProgramVersioningViewModel model)
        {

            var result = onlineExamService.SaveProgramVersioning(new OnlineExam.Request.ProgramVersioningRequestDTO()
            {

                CreatedBy = User.UserId,
                ProgramVersioningID = model.ProgramVersioningID


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Programme Semester

        public ActionResult ProgrammeSemester() => View();

        private List<ProgrammeSemesterViewModel> GetAllProgrammeSemester()
        {
            var itemSet = new List<ProgrammeSemesterViewModel>();
            itemSet = onlineExamService.GetAllProgrammeSemester().Select(a => new ProgrammeSemesterViewModel()
            {

                ProgrammeID = a.ProgrammeID,
                ProgrammeCode = a.ProgrammeCode,
                ProgrammeName = a.ProgrammeName,
                AcademicName = a.AcademicName,
                DepartmentName = a.DepartmentName,
                DepartmentID = a.DepartmentID,
                ModifiedOn = a.ModifiedOn?.Date,
                ProgrammeSemester = a.ProgrammeSemester,
                ProgrammeSemesterID = a.ProgrammeSemesterID,
                ProgrammeYear = a.ProgrammeYear,
                SemesterType = a.SemesterType,
                Status = a.Status,
                AcademicID = a.AcademicID,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                CountryID = a.CountryID,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
                ProgramVersioningID = a.ProgrammeVersioningID,
                Version = a.Version,
                CountryName = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : a.CountryID == 3 ? "United States" : string.Empty,
            }).ToList();

            return itemSet;
        }

        private ProgrammeSemesterViewModel SelectProgrammeSemester(string programmeSemesterId)
        {
            var model = new ProgrammeSemesterViewModel();
            var item = onlineExamService.SelectProgrammeSemester(new OnlineExam.Request.ProgrammeSemesterRequestDTO
            {
                ProgrammeSemesterID = Convert.ToInt32(programmeSemesterId),
            });
            model.ProgrammeID = item.ProgrammeID;
            model.ProgrammeName = item.ProgrammeName;
            model.ProgrammeCode = item.ProgrammeCode;
            model.AcademicName = item.AcademicName;
            model.DepartmentName = item.DepartmentName;
            model.DepartmentID = item.DepartmentID;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.ProgrammeSemester = item.ProgrammeSemester;
            model.ProgrammeSemesterID = item.ProgrammeSemesterID;
            model.ProgrammeYear = item.ProgrammeYear;
            model.SemesterType = item.SemesterType;
            model.Status = item.Status;
            model.AcademicID = item.AcademicID;
            model.CountryID = item.CountryID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            model.Version = item.Version;
            model.ProgramVersioningID = item.ProgrammeVersioningID;
            model.CountryName = (item.CountryID == 1) ? "India" : (item.CountryID == 2) ? "Malaysia" : item.CountryID == 3 ? "United States" : string.Empty;

            return model;
        }

        public PartialViewResult AddProgrammeSemester(string id)
        {
            var model = new ProgrammeSemesterViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectProgrammeSemester(id);
                model.DepartmentList = onlineExamService.GetAllDepartment().Where(a => a.AcademicID == model.AcademicID && a.Status == "A")
                   .Select(a => new SelectListItem
                   {
                       Text = a.DepartmentName + "(" + a.DepartmentCode + ")",
                       Value = a.DepartmentID.ToString()

                   }).ToList();

                model.ProgrammeList = onlineExamService.GetAllProgrammeWithVersion().Where(a => a.DepartmentID == Convert.ToInt32(model.DepartmentID) && a.Status == "A"
                                        && !string.IsNullOrEmpty(a.Version))
                    .Select(a => new SelectListItem
                    {
                        Text = a.ProgrammeName + "(" + a.Version + ")",
                        Value = a.ProgramVersioningID.ToString()

                    }).ToList();
            }
            else
            {
                model.ProgrammeList = new List<SelectListItem>();
                model.DepartmentList = new List<SelectListItem>();
            }
            model.CountryList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="India", Value = "1" },
                                new SelectListItem{ Text="Malaysia", Value = "2" },
                                new SelectListItem{ Text="United States", Value = "3" },
                             };
            model.ProgYearList = Enumerable.Range(1, 10).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();
            model.ProgSEMList = Enumerable.Range(1, 10).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList();
            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();

            return PartialView("_addProgrammeSemester", model);
        }

        public PartialViewResult ProgrammeSemesterView(string id)
        {
            return PartialView("_viewProgrammeSemester", SelectProgrammeSemester(id));
        }

        public PartialViewResult ProgrammeSemesterList()
        {
            return PartialView("_listProgrammeSemester", GetAllProgrammeSemester());
        }

        [HttpPost]
        public JsonResult SaveProgrammeSemester(ProgrammeSemesterViewModel model)
        {
            var type = "INSERT";
            if (model.ProgrammeSemesterID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveProgrammeSemester(new OnlineExam.Request.ProgrammeSemesterRequestDTO()
            {
                ProgrammeSemesterID = model.ProgrammeSemesterID,
                CreatedBy = User.UserId,
                DepartmentID = model.DepartmentID,
                ProgramVersioningID = model.ProgramVersioningID,
                CountryID = model.CountryID,
                ProgrammeYear = model.ProgrammeYear,
                ProgrammeSemester = model.ProgrammeSemester,
                SemesterType = model.SemesterType,
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteProgrammeSemester(ProgrammeSemesterViewModel model)
        {
            var result = onlineExamService.SaveProgrammeSemester(new OnlineExam.Request.ProgrammeSemesterRequestDTO()
            {
                CreatedBy = User.UserId,
                ProgrammeSemesterID = model.ProgrammeSemesterID

            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Course

        public ActionResult Course() => View();

        private List<CourseViewModel> GetAllCourse()
        {
            var itemSet = new List<CourseViewModel>();
            itemSet = onlineExamService.GetAllCourse().Select(a => new CourseViewModel()
            {
                CourseName = a.CourseName,
                AcademicName = a.AcademicName,
                CourseCode = a.CourseCode,
                ModifiedOn = a.ModifiedOn?.Date,
                Status = a.Status,
                CourseID = a.CourseID,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
                AcademicID = a.AcademicID,
                ApprovalNo = a.ApprovalNo,
                Credit = a.Credit,
                DepartmentID = a.DepartmentID,
                DepartmentName = a.DepartmentName,
                ProgrammeID = a.ProgrammeID,
                ProgrammeName = a.ProgrammeName,
                ProgrammeSemesterID = a.ProgrammeSemesterID,
                ProgrammeSemester = a.ProgrammeSemester,
                ProgrammeYear = a.ProgrammeYear,
                SemesterType = a.SemesterType,
                CountryID = a.CountryId,
                CourseType = a.CourseType,
                ProgramVersioningID = a.ProgramVersioningID,
                Version = a.Version,
                CountryName = (a.CountryId == 1) ? "India" : (a.CountryId == 2) ? "Malaysia" : a.CountryId == 3 ? "United States" : string.Empty
            }).ToList();

            return itemSet;
        }

        private CourseViewModel SelectCourse(string courseId)
        {
            var model = new CourseViewModel();
            var item = onlineExamService.SelectCourse(new OnlineExam.Request.CourseRequestDTO
            {
                CourseID = Convert.ToInt32(courseId)
            });
            model.CourseName = item.CourseName;
            model.CourseCode = item.CourseCode;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            model.CourseID = item.CourseID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            model.AcademicID = item.AcademicID;
            model.ApprovalNo = item.ApprovalNo;
            model.Credit = item.Credit;
            model.DepartmentID = item.DepartmentID;
            model.DepartmentName = item.DepartmentName;
            model.ProgrammeID = item.ProgrammeID;
            model.ProgrammeName = item.ProgrammeName;
            model.ProgrammeSemesterID = item.ProgrammeSemesterID;
            model.ProgrammeSemester = item.ProgrammeSemester;
            model.ProgrammeYear = item.ProgrammeYear;
            model.SemesterType = item.SemesterType;
            model.AcademicName = item.AcademicName;
            model.CountryID = item.CountryId;
            model.CountryName = (item.CountryId == 1) ? "India" : (item.CountryId == 2) ? "Malaysia" : item.CountryId == 3 ? "United States" : string.Empty;
            model.CourseType = item.CourseType;
            model.ProgramVersioningID = item.ProgramVersioningID;
            model.Version = item.Version;

            return model;

        }

        public PartialViewResult AddCourse(string id)
        {
            var model = new CourseViewModel();
            model.ProgrammeList = new List<SelectListItem>();
            model.CountryList = new List<SelectListItem>();
            model.ProgYearList = new List<SelectListItem>();
            model.ProgSEMList = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectCourse(id);
                model.ProgrammeList = onlineExamService.GetAllProgrammeWithVersion().Where(a => a.DepartmentID == Convert.ToInt32(model.DepartmentID) && a.Status == "A"
                                        && !string.IsNullOrEmpty(a.Version))
                   .Select(a => new SelectListItem
                   {
                       Text = a.ProgrammeName + "(" + a.Version + ")",
                       Value = a.ProgramVersioningID.ToString()

                   }).ToList();
                model.DepartmentList = onlineExamService.GetAllDepartment().Where(a => a.AcademicID == model.AcademicID && a.Status == "A")
                   .Select(a => new SelectListItem
                   {
                       Text = a.DepartmentName + "(" + a.DepartmentCode + ")",
                       Value = a.DepartmentID.ToString()

                   }).ToList();
                var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgramVersioningID) && a.Status == "A").ToList();
                model.CountryList = itemList?.Select(a => new SelectListItem() { Text = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : "United States", Value = a.CountryID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
                model.ProgSEMList = itemList?.Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgramVersioningID) && a.CountryID == model.CountryID && a.ProgrammeYear == model.ProgrammeYear).Select(a => new SelectListItem() { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemesterID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
                model.ProgYearList = itemList?.Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgramVersioningID) && a.CountryID == model.CountryID).Select(a => new SelectListItem() { Text = a.ProgrammeYear.ToString(), Value = a.ProgrammeYear.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
            }
            else
            {
                model.DepartmentList = new List<SelectListItem>();
            }

            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();

            return PartialView("_addCourse", model);
        }

        public PartialViewResult CourseView(string id)
        {
            return PartialView("_viewCourse", SelectCourse(id));
        }

        public PartialViewResult CourseList()
        {
            return PartialView("_listCourse", GetAllCourse());
        }

        [HttpPost]
        public JsonResult SaveCourse(CourseViewModel model)
        {
            var type = "INSERT";
            if (model.CourseID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveCourse(new OnlineExam.Request.CourseRequestDTO()
            {
                CourseID = model.CourseID,
                CreatedBy = User.UserId,
                CourseCode = model.CourseCode,
                CourseName = model.CourseName,
                SemesterType = model.SemesterType,
                ProgrammeYear = model.ProgrammeYear,
                ProgrammeSemesterID = model.ProgrammeSemesterID,
                ProgrammeID = model.ProgrammeID,
                DepartmentID = model.DepartmentID,
                ApprovalNo = model.ApprovalNo,
                CountryID = model.CountryID,
                CourseType = model.CourseType,
                Credit = model.Credit,
                ProgramVersioningID = model.ProgramVersioningID,
                Active = model.Active
            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCourse(CourseViewModel model)
        {
            var result = onlineExamService.SaveCourse(new OnlineExam.Request.CourseRequestDTO()
            {
                CourseID = model.CourseID,
                CreatedBy = User.UserId,
            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeProgramSelection(string programmeVersionID)
        {
            var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeVersioningID == Convert.ToInt32(programmeVersionID) && a.Status == "A").ToList();
            var model = new CourseViewModel();
            model.CountryList = new List<SelectListItem>();
            if (itemList != null && itemList.Count() > 0)
            {
                model.CountryList = itemList.Select(a => new SelectListItem() { Text = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : "United States", Value = a.CountryID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeCountrySelection(string programmeVersionID, string countryId)
        {
            var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeVersioningID == Convert.ToInt32(programmeVersionID)
                                                    && a.CountryID == Convert.ToInt32(countryId) && a.Status == "A"
                                                    ).ToList().OrderBy(a => a.ProgrammeYear);
            var model = new CourseViewModel();
            model.ProgYearList = new List<SelectListItem>();

            if (itemList != null && itemList.Count() > 0)
            {
                model.ProgYearList = itemList.Select(a => new SelectListItem() { Text = a.ProgrammeYear.ToString(), Value = a.ProgrammeYear.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                            .Select(g => g.FirstOrDefault())
                                            .ToList();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeProgramYearSelection(string programmeVersionID, string countryId, string programYear)
        {
            var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeVersioningID == Convert.ToInt32(programmeVersionID ?? "0")
                                                   && a.CountryID == Convert.ToInt32(countryId)
                                                   && a.ProgrammeYear == Convert.ToInt32(programYear) && a.Status == "A"
                                                   ).ToList().OrderBy(a => a.ProgrammeSemester);
            var model = new CourseViewModel();
            model.ProgSEMList = new List<SelectListItem>();
            if (itemList != null && itemList.Count() > 0)
            {
                model.ProgSEMList = itemList.Select(a => new SelectListItem() { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemesterID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeProgramSemSelection(string progSemId, string countryID, string programmeID, string programYear)
        {
            return Json(onlineExamService.SelectProgrammeSemester(new OnlineExam.Request.ProgrammeSemesterRequestDTO
            {
                ProgrammeSemesterID = Convert.ToInt32(progSemId),
            }).SemesterType, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}