using Lincoln.Admin.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Areas.Admin.Controllers
{
    public partial class AdminController
    {
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
    }
}