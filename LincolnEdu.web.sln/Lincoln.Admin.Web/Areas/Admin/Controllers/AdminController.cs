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

        #region Student Registration

        public ActionResult StudentRegistration() => View();

        #endregion Student Registration

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
            if (model.SubjectID>0)
            {
                type = "UPDATE";
            }
            var result = onlineExamService.SaveSubject(new OnlineExam.Request.SubjectRequestDTO()
            {
                CourseID = model.CourseID,
                CreatedBy = User.UserId,
                SubjectCode = model.SubjectCode,
                SubjectID = model.SubjectID,
                SubjectName = model.SubjectName
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

        #endregion Faculty registration
    }
}