using Lincoln.Admin.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Areas.Admin.Controllers
{
    public partial class AdminController
    {
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
    }
}