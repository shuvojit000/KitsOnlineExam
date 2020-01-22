using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lincoln.Admin.Web.Controllers;
using Lincoln.Admin.Web.Models;
using Lincoln.OnlineExam;

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

        #region Batch

        public ActionResult Batch() => View();

        private List<BatchViewModel> GetAllBatch()
        {
            var itemSet = new List<BatchViewModel>();
            itemSet = onlineExamService.GetAllBatch().Select(a => new BatchViewModel()
            {
                BatchName = a.BatchName,
                BatchDesc = a.BatchDesc,
                ModifiedOn = a.ModifiedOn?.Date,
                Status = a.Status,
                BatchID = a.BatchID,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }
        private BatchViewModel SelectBatch(string batchId)
        {
            var model = new BatchViewModel();
            var item = onlineExamService.SelectBatch(new OnlineExam.Request.BatchRequestDTO
            {
                BatchID = Convert.ToInt32(batchId)

            });
            model.BatchName = item.BatchName;
            model.BatchDesc = item.BatchDesc;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            model.BatchID = item.BatchID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            return model;

        }

        public PartialViewResult AddBatch(string id)
        {
            var model = new BatchViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectBatch(id);
            }
            return PartialView("_addBatch", model);
        }
        public PartialViewResult BatchView(string id)
        {
            return PartialView("_viewBatch", SelectBatch(id));
        }
        public PartialViewResult BatchList()
        {
            return PartialView("_listBatch", GetAllBatch());
        }
        [HttpPost]
        public JsonResult SaveBatch(BatchViewModel model)
        {
            var type = "INSERT";
            if (model.BatchID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveBatche(new OnlineExam.Request.BatchRequestDTO()
            {
                BatchID = model.BatchID,
                CreatedBy = User.UserId,
                BatchName = model.BatchName,
                BatchDesc = model.BatchDesc,
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteBatch(BatchViewModel model)
        {

            var result = onlineExamService.SaveBatche(new OnlineExam.Request.BatchRequestDTO()
            {
                BatchID = model.BatchID,
                CreatedBy = User.UserId,
                BatchName = model.BatchName,
                BatchDesc = model.BatchDesc,

            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion Batch

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
            return model;

        }

        public PartialViewResult AddCourse(string id)
        {
            var model = new CourseViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectCourse(id);
            }
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
                CourseCode = model.CourseCode,
                CourseName = model.CourseName,

            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion Course



        #region Subject

        public ActionResult Subject()
        {
            return View(GetAllSubject());
        }

        private List<SubjectViewModel> GetAllSubject()
        {
            var itemSet = new List<SubjectViewModel>();
            itemSet = onlineExamService.GetAllSubject().Select(a => new SubjectViewModel()
            {
                SubjectName = a.SubjectName,
                CourseID = a.CourseID,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = a.ModifiedBy,
                ModifiedOn = a.ModifiedOn,
                Status = a.Status,
                SubjectCode = a.SubjectCode,
                SubjectID = a.SubjectID,
            }).ToList();

            return itemSet;
        }

        private SubjectViewModel SelectSubject(string subjectId)
        {
            var model = new SubjectViewModel();
            var item = onlineExamService.SelectSubject(new OnlineExam.Request.SubjectRequestDTO()
            {
                SubjectID = Convert.ToInt32(subjectId)
            });

            model.CourseID = item.CourseID;
            model.SubjectID = item.SubjectID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = item.ModifiedBy;
            model.ModifiedOn = item.ModifiedOn;
            model.Status = item.Status;
            model.SubjectCode = item.SubjectCode;
            model.SubjectID = item.SubjectID;
            model.SubjectName = item.SubjectName;
            return model;
        }

        public PartialViewResult SubjectView(string id)
        {
            return PartialView("_SubjectView", SelectSubject(id));
        }

        public PartialViewResult AddSubject(string id)
        {
            var model = new SubjectViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectSubject(id);
            }
            model.CourseList = onlineExamService.GetDropdownData("COURSE").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
            return PartialView("_AddSubject", model);
        }

        public PartialViewResult ListSubject()
        {

            return PartialView("_listSubject", GetAllSubject());
        }

        [HttpPost]
        public JsonResult SaveSubject(SubjectViewModel model)
        {
            var type = "INSERT";
            if (model.SubjectID > 0)
            {
                type = "UPDATE";
            }
            var result = onlineExamService.SaveSubject(new OnlineExam.Request.SubjectRequestDTO()
            {
                CourseID = model.CourseID,
                CreatedBy = User.UserId,
                SubjectCode = model.SubjectCode,
                SubjectID = model.SubjectID,
                SubjectName = model.SubjectName,
                Active = model.Active
            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteSubject(SubjectViewModel model)
        {

            var result = onlineExamService.SaveSubject(new OnlineExam.Request.SubjectRequestDTO()
            {
                CourseID = model.CourseID,
                CreatedBy = User.UserId,
                SubjectCode = model.SubjectCode,
                SubjectID = model.SubjectID,
                SubjectName = model.SubjectName
            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion Subject

        #region Faculty registration

        public ActionResult FacultyRegistration() => View();

        private List<FacultyViewModel> GetAllFaculty()
        {
            var itemSet = new List<FacultyViewModel>();
            itemSet = onlineExamService.GetAllFaculty().Select(a => new FacultyViewModel()
            {
                EmailID = a.EmailID,
                LoginID = a.LoginID,
                MobileNo = a.MobileNo,
                EmployeeCode = a.EmployeeCode,
                FacultyID = a.FacultyID,
                EmployeeName = a.EmployeeName,
                ModifiedOn = a.ModifiedOn?.Date,
                Status = a.Status,
                UserName = a.UserName,
                UserType = a.UserType,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }
        private FacultyViewModel SelectFaculty(string facultyId)
        {
            var model = new FacultyViewModel();
            var item = onlineExamService.SelectFaculty(new OnlineExam.Request.FacultyRequestDTO
            {
                FacultyID = Convert.ToInt32(facultyId)

            });
            model.FacultyID = item.FacultyID;
            model.EmployeeName = item.EmployeeName;
            model.EmailID = item.EmailID;
            model.LoginID = item.LoginID;
            model.MobileNo = item.MobileNo;
            model.Password = item.Password;
            model.EmployeeCode = item.EmployeeCode;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            model.UserName = item.UserName;
            model.Password = item.Password;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            return model;

        }

        public PartialViewResult AddFaculty(string id)
        {
            var model = new FacultyViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectFaculty(id);
            }
            //model.BatchList = onlineExamService.GetDropdownData("BATCH").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList(); ;
            return PartialView("_addFaculty", model);
        }
        public PartialViewResult FacultyView(string id)
        {
            return PartialView("_viewFaculty", SelectFaculty(id));
        }
        public PartialViewResult FacultyList()
        {
            return PartialView("_listFaculty", GetAllFaculty());
        }
        [HttpPost]
        public JsonResult SaveFaculty(FacultyViewModel model)
        {
            var type = "INSERT";
            if (model.FacultyID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveFaculty(new OnlineExam.Request.FacultyRequestDTO()
            {
                FacultyID = model.FacultyID,
                CreatedBy = User.UserId,
                EmailID = model.EmailID,
                EmployeeCode = model.EmployeeCode,
                MobileNo = model.MobileNo,
                EmployeeName = model.EmployeeName,
                UserName = model.EmployeeCode,
                Password = model.Password,
                Active = model.Active,
                UserType = "FACULTY"
            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteFaculty(FacultyViewModel model)
        {

            var result = onlineExamService.SaveFaculty(new OnlineExam.Request.FacultyRequestDTO()
            {

                CreatedBy = User.UserId,
                FacultyID = model.FacultyID

            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion Faculty registration

        #region Question Section

        public ActionResult QuestionSection() => View();
        private List<QuestionSectionViewModel> GetAllQuestionSection()
        {
            var itemSet = new List<QuestionSectionViewModel>();
            itemSet = onlineExamService.GetAllQuestionSection().Select(a => new QuestionSectionViewModel()
            {
                QuestionSectionName = a.QuestionSectionName,
                QuestionSectionID = a.QuestionSectionID,
                ModifiedOn = a.ModifiedOn?.Date,
                Status = a.Status,
                QuestionSectionDesc = a.QuestionSectionDesc,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }
        private QuestionSectionViewModel SelectQuestionSection(string questionSectionId)
        {
            var model = new QuestionSectionViewModel();
            var item = onlineExamService.SelectQuestionSection(new OnlineExam.Request.QuestionSectionRequestDTO
            {
                QuestionSectionID = Convert.ToInt32(questionSectionId)

            });
            model.QuestionSectionID = item.QuestionSectionID;
            model.QuestionSectionName = item.QuestionSectionName;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            model.QuestionSectionDesc = item.QuestionSectionDesc;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            return model;

        }

        public PartialViewResult AddQuestionSection(string id)
        {
            var model = new QuestionSectionViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectQuestionSection(id);
            }
            return PartialView("_addQuestionSection", model);
        }
        public PartialViewResult QuestionSectionView(string id)
        {
            return PartialView("_viewQuestionSection", SelectQuestionSection(id));
        }
        public PartialViewResult QuestionSectionList()
        {
            return PartialView("_listQuestionSection", GetAllQuestionSection());
        }
        [HttpPost]
        public JsonResult SaveQuestionSection(QuestionSectionViewModel model)
        {
            var type = "INSERT";
            if (model.QuestionSectionID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveQuestionSection(new OnlineExam.Request.QuestionSectionRequestDTO()
            {
                QuestionSectionID = model.QuestionSectionID,
                CreatedBy = User.UserId,
                QuestionSectionName = model.QuestionSectionName,
                QuestionSectionDesc = model.QuestionSectionDesc,
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteQuestionSection(QuestionSectionViewModel model)
        {

            var result = onlineExamService.SaveQuestionSection(new OnlineExam.Request.QuestionSectionRequestDTO()
            {
                QuestionSectionID = model.QuestionSectionID,
                CreatedBy = User.UserId,
                QuestionSectionName = model.QuestionSectionName,
                QuestionSectionDesc = model.QuestionSectionDesc,

            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Student

        public ActionResult Student() => View();

        private List<StudentViewModel> GetAllStudent()
        {
            var itemSet = new List<StudentViewModel>();
            itemSet = onlineExamService.GetAllStudent().Select(a => new StudentViewModel()
            {
                EmailID = a.EmailID,
                LoginID = a.LoginID,
                MobileNo = a.MobileNo,
                RollNo = a.RollNo,
                StudentID = a.StudentID,
                StudentName = a.StudentName,
                ModifiedOn = a.ModifiedOn?.Date,
                Status = a.Status,
                BatchID = a.BatchID,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }
        private StudentViewModel SelectStudent(string studenId)
        {
            var model = new StudentViewModel();
            var item = onlineExamService.SelectStudent(new OnlineExam.Request.StudentRequestDTO
            {
                StudentID = Convert.ToInt32(studenId)

            });
            model.StudentID = item.StudentID;
            model.StudentName = item.StudentName;
            model.EmailID = item.EmailID;
            model.LoginID = item.LoginID;
            model.MobileNo = item.MobileNo;
            model.Password = item.Password;
            model.RollNo = item.RollNo;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            model.BatchID = item.BatchID;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            return model;

        }

        public PartialViewResult AddStudent(string id)
        {
            var model = new StudentViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectStudent(id);
            }
            model.BatchList = onlineExamService.GetDropdownData("BATCH").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList(); ;
            return PartialView("_addStudent", model);
        }
        public PartialViewResult StudentView(string id)
        {
            return PartialView("_viewStudent", SelectStudent(id));
        }
        public PartialViewResult StudentList()
        {
            return PartialView("_listStudent", GetAllStudent());
        }
        [HttpPost]
        public JsonResult SaveStudent(StudentViewModel model)
        {
            var type = "INSERT";
            if (model.StudentID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveStudent(new OnlineExam.Request.StudentRequestDTO()
            {
                BatchID = model.BatchID,
                CreatedBy = User.UserId,
                EmailID = model.EmailID,
                RollNo = model.RollNo,
                MobileNo = model.MobileNo,
                StudentID = model.StudentID,
                StudentName = model.StudentName,
                Password = model.Password,
                Active = model.Active,
                UserName = model.EmailID,
                UserType = "STUDENT"
            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteStudent(StudentViewModel model)
        {

            var result = onlineExamService.SaveStudent(new OnlineExam.Request.StudentRequestDTO()
            {

                CreatedBy = User.UserId,
                StudentID = model.StudentID

            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        #endregion


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
                Version = a.Version,
                ProgramCode = a.ProgramCode,
                PlaceHolder = a.PlaceHolder,
                Credit = a.Credit,
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
            model.PlaceHolder = item.PlaceHolder;
            model.Credit = item.Credit;
            model.ModifiedOn = item.ModifiedOn?.Date;
            //model.Status = item.Status;
            //model.AcademicID = item.AcademicID;
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
                PlaceHolder = model.PlaceHolder,
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
                                new SelectListItem{ Text="Option 1", Value = "1" },
                                new SelectListItem{ Text="Option 2", Value = "2" },
                                new SelectListItem{ Text="Option 3", Value = "3" },
                             };
            model.ProgYearList = Enumerable.Range((DateTime.Now.Year - 9), 10).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();
            model.ProgSEMList = Enumerable.Range(1, 10).Select(x => new SelectListItem { Value = x.ToString(), Text = "SEM" + x.ToString() }).ToList();
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

        #endregion

    }
}