using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lincoln.Admin.Web.Controllers;
using Lincoln.Admin.Web.Models;
using Lincoln.OnlineExam;
using Lincoln.OnlineExam.Response;
using System.Xml.Linq;

namespace Lincoln.Admin.Web.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IOnlineExam onlineExamService;

        // GET: Admin/Admin
        public AdminController(IOnlineExam onlineExamService)
        {
            this.onlineExamService = onlineExamService;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ChangeAllStatus(UpdateStatusViewModel model)
        {

            return Json(onlineExamService.UpdateStatus(new OnlineExam.Request.UpdateStatusReuestDTO
            {

                Active = model.Active,
                CreatedBy = User.UserId,
                ID = model.ID,
                Table = model.Table,

            }), JsonRequestBehavior.AllowGet);
        }




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
                //PlaceHolder = a.PlaceHolder,
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
            //model.PlaceHolder = item.PlaceHolder;
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
            }
            model.ProgramList = onlineExamService.GetDropdownData("Programme").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
            model.DepartmentList = onlineExamService.GetDropdownData("Department").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
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
                //PlaceHolder = model.PlaceHolder,
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
            return model;

        }

        public PartialViewResult AddProgrammeSemester(string id)
        {
            var model = new ProgrammeSemesterViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectProgrammeSemester(id);

                model.ProgrammeList = onlineExamService.GetAllProgramme().Where(a => a.DepartmentID == Convert.ToInt32(model.DepartmentID) && a.Status == "A")
                    .Select(a => new SelectListItem
                    {
                        Text = a.ProgrammeName + "(" + a.ProgrammeCode + ")",
                        Value = a.ProgrammeID.ToString()

                    }).ToList();
            }
            else
            {
                model.ProgrammeList = new List<SelectListItem>();
            }
            model.CountryList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="India", Value = "1" },
                                new SelectListItem{ Text="Malaysia", Value = "2" },
                                new SelectListItem{ Text="United States", Value = "3" },
                             };
            model.ProgYearList = Enumerable.Range(1, 10).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();
            model.ProgSEMList = Enumerable.Range(1, 10).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList();
            model.DepartmentList = onlineExamService.GetDropdownData("Department").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
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
                ProgrammeID = model.ProgrammeID,
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
                ProgrammeSemester = a.ProgrammeSemester,
                ProgrammeYear = a.ProgrammeYear,
                SemesterType = a.SemesterType,
                CountryID = a.CountryId,
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
            model.ProgrammeSemester = item.ProgrammeSemester;
            model.ProgrammeYear = item.ProgrammeYear;
            model.SemesterType = item.SemesterType;
            model.CountryID = item.CountryId;
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
                model.ProgrammeList = onlineExamService.GetAllProgramme().Where(a => a.DepartmentID == Convert.ToInt32(model.DepartmentID) && a.Status == "A")
                   .Select(a => new SelectListItem
                   {
                       Text = a.ProgrammeName + "(" + a.ProgrammeCode + ")",
                       Value = a.ProgrammeID.ToString()

                   }).ToList();

                var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeID == Convert.ToInt32(model.ProgrammeID) && a.Status == "A").ToList();
                model.CountryList = itemList?.Select(a => new SelectListItem() { Text = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : "United States", Value = a.CountryID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
                model.ProgSEMList = itemList?.Where(a => a.ProgrammeID == Convert.ToInt32(model.ProgrammeID) && a.CountryID == model.CountryID && a.ProgrammeYear == model.ProgrammeYear).Select(a => new SelectListItem() { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemester.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
                model.ProgYearList = itemList?.Where(a => a.ProgrammeID == Convert.ToInt32(model.ProgrammeID) && a.CountryID == model.CountryID).Select(a => new SelectListItem() { Text = a.ProgrammeYear.ToString(), Value = a.ProgrammeYear.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
            }
            else
            {

            }

            model.DepartmentList = onlineExamService.GetDropdownData("Department").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();

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
                ProgrammeSemester = model.ProgrammeSemester,
                ProgrammeID = model.ProgrammeID,
                DepartmentID = model.DepartmentID,
                ApprovalNo = model.ApprovalNo,
                CountryID = model.CountryID,
                Credit = model.Credit,
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
        public JsonResult ChangeProgramSelection(string programmeID)
        {
            var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeID == Convert.ToInt32(programmeID) && a.Status == "A").ToList(); ;
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
        public JsonResult ChangeCountrySelection(string programmeID, string countryId)
        {
            var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeID == Convert.ToInt32(programmeID)
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
        public JsonResult ChangeProgramYearSelection(string programmeID, string countryId, string programYear)
        {
            var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeID == Convert.ToInt32(programmeID)
                                                   && a.CountryID == Convert.ToInt32(countryId) && a.ProgrammeYear == Convert.ToInt32(programYear) && a.Status == "A"
                                                   ).ToList().OrderBy(a => a.ProgrammeSemester);
            var model = new CourseViewModel();
            model.ProgSEMList = new List<SelectListItem>();
            if (itemList != null && itemList.Count() > 0)
            {

                model.ProgSEMList = itemList.Select(a => new SelectListItem() { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemester.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();

            }


            return Json(model, JsonRequestBehavior.AllowGet);

        }
        #endregion Course


        #region Dropdown Code
        [HttpPost]
        public JsonResult GetProgDrop(string departmentID)
        {

            return Json(onlineExamService.GetAllProgramme().Where(a => a.DepartmentID == Convert.ToInt32(departmentID) && a.Status == "A")
                    .Select(a => new SelectListItem
                    {
                        Text = a.ProgrammeName + "(" + a.ProgrammeCode + ")",
                        Value = a.ProgrammeID.ToString()

                    }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDepartmentDrop(string academicID)
        {

            return Json(onlineExamService.GetAllDepartment().Where(a => a.AcademicID == Convert.ToInt32(academicID) && a.Status == "A")
                    .Select(a => new SelectListItem
                    {
                        Text = a.DepartmentName + "(" + a.DepartmentCode + ")",
                        Value = a.DepartmentID.ToString()

                    }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProgrammeVersionDDL(string ProgramVID)
        {
            return Json(onlineExamService.GetAllProgramVersioning().Where(a => a.ProgramCode == Convert.ToInt32(ProgramVID) && a.Status == "A")
                      .Select(a => new SelectListItem
                      {
                          Text = a.Version,
                          Value = a.ProgramVersioningID.ToString()

                      }).ToList(), JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetCourseDDL(string SemisterID)
        {

            return Json(onlineExamService.GetAllCourse().Where(a => a.CourseID == Convert.ToInt32(SemisterID))
                    .Select(a => new SelectListItem
                    {
                        Text = a.CourseName,
                        Value = a.CourseID.ToString()

                    }).ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion


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
                StartDate = a.StartDate.Value.ToShortDateString(),
                EndDate = a.EndDate.Value.ToShortDateString(),
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
            model.StartDate = item.StartDate.Value.ToShortDateString();
            model.EndDate = item.EndDate.Value.ToShortDateString();
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
                FacultyCode = a.FacultyCode,
                FacultyName = a.FacultyName,
                SyllabusVersion = a.SyllabusVersionCode,
                SyllabusVersionName = a.SyllabusVersionName,
                ProgramCode = a.ProgramCode,
                ProgramName = a.ProgramName,
                AssessmentName = a.AssessmentName,
                AssessmentType = a.AssessmentType,
                Active = a.Status,
                ModifiedOn = a.ModifiedOn?.Date,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
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
            model.ProgramCode = item.ProgramCode;
            model.FacultyCode = item.FacultyCode;
            model.SyllabusVersion = item.SyllabusVersionCode;
            model.ProgramName = item.ProgramName;
            model.FacultyName = item.FacultyName;
            model.SyllabusVersionName = item.SyllabusVersionName;
            model.AssessmentType = item.AssessmentType;
            model.AssessmentName = item.AssessmentName;
            model.ModifiedOn = item.ModifiedOn?.Date;
            //model.Status = item.Status;
            //model.AcademicID = item.AcademicID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            return model;

        }

        public PartialViewResult AddAssessment(string id)
        {
            var model = new AssessmentViewModel();
            model.FacultyList = onlineExamService.GetAllDepartment().Select(a => new SelectListItem { Text = a.DepartmentName, Value = a.DepartmentID.ToString() }).ToList();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectAssessment(id);

                model.FacultyList = onlineExamService.GetAllDepartment().Where(a => a.DepartmentID == Convert.ToInt32(model.FacultyCode) && a.Status == "A")
                        .Select(a => new SelectListItem
                        {
                            Text = a.DepartmentName,
                            Value = a.DepartmentID.ToString()

                        }).ToList();

                model.ProgramList = onlineExamService.GetAllProgramme().Where(a => a.DepartmentID == Convert.ToInt32(model.FacultyCode) && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.ProgrammeName + "(" + a.ProgrammeCode + ")",
                           Value = a.ProgrammeID.ToString()

                       }).ToList();
                model.SyllabusVersionList = onlineExamService.GetAllProgramVersioning().Where(a => a.ProgramVersioningID == Convert.ToInt32(model.SyllabusVersion) && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.Version,
                           Value = a.ProgramVersioningID.ToString()

                       }).ToList();
            }
            else
            {
                model.ProgramList = new List<SelectListItem>();
                model.SyllabusVersionList = new List<SelectListItem>();
            }
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
                FacultyCode = model.FacultyCode,
                ProgramCode = model.ProgramCode,
                AssessmentType = model.AssessmentType,
                AssessmentName = model.AssessmentName,
                SyllabusVersion = model.SyllabusVersion,
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
                SyllabusVersionCode = a.SyllabusVersionCode,
                SyllabusVersionName = a.SyllabusVersionName,
                CourseName = a.CourseName,
                FacultyCode = a.FacultyCode,
                FacultyName = a.FacultyName,
                SectionName = a.SectionName,
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

            model.ProgramName = item.ProgramName;
            model.CourseName = item.CourseName;
            model.SemisterName = item.SemisterName;
            model.SyllabusVersionName = item.SyllabusVersionName;
            model.CountryName = item.CountryName;
            model.FacultyName = item.FacultyName;
            model.YearName = item.YearName;
            model.SectionName = item.SectionName;
            model.ModifiedOn = item.ModifiedOn?.Date;
            //model.Status = item.Status;
            //model.AcademicID = item.AcademicID;
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

                model.FacultyList = onlineExamService.GetAllDepartment().Where(a => a.DepartmentID == Convert.ToInt32(model.FacultyCode) && a.Status == "A")
                     .Select(a => new SelectListItem
                     {
                         Text = a.DepartmentName,
                         Value = a.DepartmentID.ToString()

                     }).ToList();

                model.ProgramList = onlineExamService.GetAllProgramme().Where(a => a.DepartmentID == Convert.ToInt32(model.FacultyCode) && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.ProgrammeName + "(" + a.ProgrammeCode + ")",
                           Value = a.ProgrammeID.ToString()

                       }).ToList();
                model.SyllabusVersionList = onlineExamService.GetAllProgramVersioning().Where(a => a.ProgramCode == Convert.ToInt32(model.ProgramCode) && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.Version,
                           Value = a.ProgramVersioningID.ToString()

                       }).ToList();

                model.SemisterList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeSemesterID == Convert.ToInt32(model.ProgramCode) && a.Status == "A")
                      .Select(a => new SelectListItem
                      {
                          Text = a.SemesterType,
                          Value = a.ProgrammeSemesterID.ToString()

                      }).ToList();
                model.CourseList = onlineExamService.GetAllCourse().Where(a => a.ProgrammeID == Convert.ToInt32(model.ProgramCode)
                && a.CountryId == model.CountryCode
                && a.ProgrammeYear == model.AcademicYearCode && a.Status == "A" && a.ProgrammeSemester == model.SemisterCode)
                      .Select(a => new SelectListItem
                      {
                          Text = a.CourseName,
                          Value = a.CourseID.ToString()

                      }).ToList();
                var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeID == Convert.ToInt32(model.ProgramCode) && a.Status == "A").ToList();
                model.CountryList = itemList?.Select(a => new SelectListItem() { Text = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : "United States", Value = a.CountryID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                          .Select(g => g.FirstOrDefault())
                                          .ToList();
                model.SemisterList = itemList?.Where(a => a.ProgrammeID == Convert.ToInt32(model.ProgramCode) && a.CountryID == model.CountryCode
                && a.ProgrammeYear == model.AcademicYearCode).Select(a => new SelectListItem() { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemester.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
                model.AcademicYearList = itemList?.Where(a => a.ProgrammeID == Convert.ToInt32(model.ProgramCode) &&
                a.CountryID == model.CountryCode).Select(a => new SelectListItem() { Text = a.ProgrammeYear.ToString(), Value = a.ProgrammeYear.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();



                model.SectionList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="Section A", Value = "1" },
                                new SelectListItem{ Text="Section B", Value = "2" },
                                new SelectListItem{ Text="Section C", Value = "3" },
                                 new SelectListItem{ Text="Section D", Value = "4" },
                                  new SelectListItem{ Text="Section E", Value = "5" },
                             };

            }
            else
            {
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
                                 new SelectListItem{ Text="Section A", Value = "1" },
                                new SelectListItem{ Text="Section B", Value = "2" },
                                new SelectListItem{ Text="Section C", Value = "3" },
                                 new SelectListItem{ Text="Section D", Value = "4" },
                                  new SelectListItem{ Text="Section E", Value = "5" },
                             };
                model.AcademicYearList = new List<SelectListItem>();
            }
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
                SyllabusVersionCode = model.SyllabusVersionCode,
                CountryCode = model.CountryCode,
                FacultyCode = model.FacultyCode,
                AcademicYearCode = model.AcademicYearCode,
                SectionName = model.SectionName,
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
        public JsonResult GetCourseExam(string programmeId, string countryId, string programmeYear, string programmeSem)
        {

            return Json(onlineExamService.GetAllCourse().Where(a => a.ProgrammeID == Convert.ToInt32(programmeId)
                                                                    && a.CountryId == Convert.ToInt32(countryId)
                                                                    && a.ProgrammeYear == Convert.ToInt32(programmeYear)
                                                                     && a.ProgrammeSemester == Convert.ToInt32(programmeSem))
                    .Select(a => new SelectListItem
                    {
                        Text = a.CourseName,
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
                ProgramCode = a.ProgramCode,
                ProgramName = a.ProgramName,
                CourseCode = a.CourseCode,
                CountryName = a.CountryName,
                SemisterCode = a.SemisterCode,
                SemisterName = a.SemisterName,
                SyllabusVersionCode = a.SyllabusVersionCode,
                SyllabusVersionName = a.SyllabusVersionName,
                CourseName = a.CourseName,
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
                ProgramName = a.ProgramName,
                CourseCode = a.CourseCode,
                CountryName = a.CountryName,
                SemisterCode = a.SemisterCode,
                SemisterName = a.SemisterName,
                SyllabusVersionCode = a.SyllabusVersionCode,
                SyllabusVersionName = a.SyllabusVersionName,
                CourseName = a.CourseName,
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

            model.SubAssessmentDetails = item.SubAssessmentDetails;
            model.SubjectAssessmentID = Convert.ToInt32(itemSet.ElementAt(0).SubjectAssessmentID);
            model.ProgramCode = Convert.ToInt32(itemSet.ElementAt(0).ProgramCode);
            model.CourseCode = Convert.ToInt32(itemSet.ElementAt(0).CourseCode);
            model.SemisterCode = Convert.ToInt32(itemSet.ElementAt(0).SemisterCode);
            model.SyllabusVersionCode = Convert.ToInt32(itemSet.ElementAt(0).SyllabusVersionCode);
            model.CountryCode = Convert.ToInt32(itemSet.ElementAt(0).CountryCode);
            model.FacultyCode = Convert.ToInt32(itemSet.ElementAt(0).FacultyCode);
            model.AcademicYearCode = Convert.ToInt32(itemSet.ElementAt(0).AcademicYearCode);

            model.ProgramName = itemSet.ElementAt(0).ProgramName;
            model.CourseName = itemSet.ElementAt(0).CourseName;
            model.SemisterName = itemSet.ElementAt(0).SemisterName;
            model.SyllabusVersionName = itemSet.ElementAt(0).SyllabusVersionName;
            model.CountryName = itemSet.ElementAt(0).CountryName;
            model.FacultyName = itemSet.ElementAt(0).FacultyName;
            model.YearName = itemSet.ElementAt(0).YearName;
            model.ModifiedOn = itemSet.ElementAt(0).ModifiedOn?.Date;
            //model.Status = item.Status;
            //model.AcademicID = item.AcademicID;
            model.CreatedBy = Convert.ToInt32(itemSet.ElementAt(0).CreatedBy);
            model.CreatedOn = itemSet.ElementAt(0).CreatedOn;
            model.ModifiedBy = Convert.ToInt32(itemSet.ElementAt(0).ModifiedBy);
            return model;

        }

        public PartialViewResult AddSubjectAssessment(string id)
        {
            var model = new SubjectAssessmentViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                // model = SelectSubjectAssessment(id);
                model.FacultyList = onlineExamService.GetAllDepartment().Where(a => a.DepartmentID == Convert.ToInt32(model.FacultyCode) && a.Status == "A")
                         .Select(a => new SelectListItem
                         {
                             Text = a.DepartmentName,
                             Value = a.DepartmentID.ToString()

                         }).ToList();

                model.ProgramList = onlineExamService.GetAllProgramme().Where(a => a.DepartmentID == Convert.ToInt32(model.FacultyCode) && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.ProgrammeName + "(" + a.ProgrammeCode + ")",
                           Value = a.ProgrammeID.ToString()

                       }).ToList();
                model.SyllabusVersionList = onlineExamService.GetAllProgramVersioning().Where(a => a.ProgramVersioningID == Convert.ToInt32(model.ProgramCode) && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.Version + "(" + a.ProgramVersioningID + ")",
                           Value = a.ProgramVersioningID.ToString()

                       }).ToList();

                model.SemisterList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeSemesterID == Convert.ToInt32(model.ProgramCode) && a.Status == "A")
                      .Select(a => new SelectListItem
                      {
                          Text = a.SemesterType,
                          Value = a.ProgrammeSemesterID.ToString()

                      }).ToList();
                model.CourseList = onlineExamService.GetAllCourse().Where(a => a.CourseID == Convert.ToInt32(model.CourseID) && a.Status == "A")
                      .Select(a => new SelectListItem
                      {
                          Text = a.CourseName,
                          Value = a.CourseID.ToString()

                      }).ToList();

                model.CountryList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="India", Value = "1" },
                                new SelectListItem{ Text="Malaysia", Value = "2" },
                                new SelectListItem{ Text="United States", Value = "3" },
                             };
                //var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeID == Convert.ToInt32(model.ProgramCode)).ToList();
                //model.CountryList = itemList?.Select(a => new SelectListItem() { Text = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : "United States", Value = a.CountryID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                //                           .Select(g => g.FirstOrDefault())
                //                           .ToList();
                //model.SemisterList = itemList?.Select(a => new SelectListItem() { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemester.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                //                           .Select(g => g.FirstOrDefault())
                //                           .ToList();
                //model.AcademicYearList = itemList?.Select(a => new SelectListItem() { Text = a.ProgrammeYear.ToString(), Value = a.ProgrammeYear.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                //                           .Select(g => g.FirstOrDefault())
                //                           .ToList();
                model.AcademicYearList = Enumerable.Range((DateTime.Now.Year - 9), 10).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();
            }
            else
            {
                model.FacultyList = onlineExamService.GetAllDepartment().Select(a => new SelectListItem { Text = a.DepartmentName, Value = a.DepartmentID.ToString() }).ToList();
                model.ProgramList = new List<SelectListItem>();
                model.SyllabusVersionList = new List<SelectListItem>();
                model.SemisterList = onlineExamService.GetAllProgrammeSemester().Select(a => new SelectListItem { Text = a.ProgrammeSemester.ToString(), Value = a.ProgrammeSemesterID.ToString() }).ToList();
                model.CourseList = new List<SelectListItem>();
                model.CountryList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="India", Value = "1" },
                                new SelectListItem{ Text="Malaysia", Value = "2" },
                                new SelectListItem{ Text="United States", Value = "3" },
                             };
                model.AcademicYearList = Enumerable.Range((DateTime.Now.Year - 9), 10).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();
            }
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
                SyllabusVersionCode = Convert.ToInt32(model.SyllabusVersionCode),
                CountryCode = Convert.ToInt32(model.CountryCode),
                FacultyCode = Convert.ToInt32(model.FacultyCode),
                AcademicYearCode = Convert.ToInt32(model.AcademicYearCode),
                TabAssessmentDetails = xEle.ToString().Trim(),
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteSubjectAssesssment(SubjectAssessmentViewModel model)
        {

            var result = onlineExamService.SaveSubjectAssessment(new OnlineExam.Request.SubjectAssessmentRequestDTO()
            {

                CreatedBy = User.UserId,
                SubjectAssessmentID = Convert.ToInt32(model.SubjectAssessmentID)


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
