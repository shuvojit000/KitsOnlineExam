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
        #region Employee

        public ActionResult EmployeeRegisration() => View();

        private List<EmployeeViewModel> GetAllEmployee()
        {
            var itemSet = new List<EmployeeViewModel>();
            itemSet = onlineExamService.GetAllEmployee().Select(a => new EmployeeViewModel()
            {
                EmailID = a.EmailID,
                EmployeeCode = a.EmployeeCode,
                EmployeeID = a.EmployeeID,
                EmployeeName = a.EmployeeName,
                LoginID = a.LoginID,
                MobileNo = a.MobileNo,
                Password = a.Password,
                UserName = a.UserName,
                UserType = a.UserType,
                ModifiedOn = a.ModifiedOn?.Date,
                Status = a.Status,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }

        private EmployeeViewModel SelectEmployee(string employeeId)
        {
            var model = new EmployeeViewModel();
            var item = onlineExamService.SelectEmployee(new OnlineExam.Request.EmployeeRequestDTO
            {
                EmployeeID = Convert.ToInt32(employeeId),


            });
            model.EmployeeID = item.EmployeeID;
            model.EmployeeName = item.EmployeeName;
            model.EmailID = item.EmailID;
            model.EmployeeCode = item.EmployeeCode;
            model.LoginID = item.LoginID;
            model.MobileNo = item.MobileNo;
            model.Password = item.Password;
            model.Status = item.Status;
            model.UserName = item.UserName;
            model.UserType = item.UserType;
            model.ModifiedOn = item.ModifiedOn?.Date;
            model.Status = item.Status;
            model.CreatedBy = item.CreatedBy;
            model.CreatedOn = item.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(item.ModifiedBy);
            return model;

        }

        public PartialViewResult AddEmployee(string id)
        {
            var model = new EmployeeViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectEmployee(id);

            }
            return PartialView("_addEmployee", model);
        }

        public PartialViewResult EmployeeView(string id)
        {
            return PartialView("_viewEmployee", SelectEmployee(id));
        }

        public PartialViewResult EmployeeList()
        {
            return PartialView("_listEmployee", GetAllEmployee());
        }

        [HttpPost]
        public JsonResult SaveEmployee(EmployeeViewModel model)
        {
            var type = "INSERT";
            if (model.EmployeeID > 0)
            {
                type = "UPDATE";
            }

            var result = onlineExamService.SaveEmployee(new OnlineExam.Request.EmployeeRequestDTO()
            {

                CreatedBy = User.UserId,
                Active = model.Active,
                EmailID = model.EmailID,
                EmployeeCode = model.EmployeeCode,
                EmployeeID = model.EmployeeID,
                EmployeeName = model.EmployeeName,
                LoginID = User.UserId,
                MobileNo = model.MobileNo,
                Password = model.Password,
                UserName = model.UserName

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteEmployee(EmployeeViewModel model)
        {

            var result = onlineExamService.SaveEmployee(new OnlineExam.Request.EmployeeRequestDTO()
            {

                CreatedBy = User.UserId,
                EmployeeID = model.EmployeeID


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Subject Allocation

        public ActionResult SubjectAllocation() => View();

        private List<SubjectAllocationViewModel> GetAllSubjectAllocation()
        {

            var itemSet = new List<SubjectAllocationViewModel>();
            itemSet = onlineExamService.GetAllSubjectAllocation().Select(a => new SubjectAllocationViewModel()
            {
                SubjectAllocationID = a.SubjectAllocationID,
                AcademicID = a.AcademicID,
                AcademicName = a.AcademicName,
                ProgramName = a.ProgramName,
                CountryName = a.CountryName,
                EmployeeName = a.EmployeeName,
                ProgrammeVersioningID = a.ProgrammeVersioningID,
                Version = a.Version,
                FacultyCode = a.FacultyCode,
                FacultyName = a.FacultyName,
                AcademicYearCode = a.AcademicYearCode,
                YearName = a.YearName,
                Status = a.Active,// by suvendu
                ModifiedOn = a.ModifiedOn?.Date,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn,
                ModifiedBy = Convert.ToInt32(a.ModifiedBy),
            }).ToList();

            return itemSet;
        }

        private SubjectAllocationViewModel SelectSubjectAllocation(string SubjectAllocationId)
        {
            var itemSet = onlineExamService.SelectSubjectAllocation(new OnlineExam.Request.SubjectAllocationRequestDTO
            {
                SubjectAllocationID = Convert.ToInt32(SubjectAllocationId)

            });

            SubjectAllocationViewModel model = new SubjectAllocationViewModel();
            model.SubjectAllocationID = Convert.ToInt32(itemSet.SubjectAllocationID);
            model.AcademicID = itemSet.AcademicID;
            model.AcademicName = itemSet.AcademicName;
            model.ProgrammeVersioningID = Convert.ToInt32(itemSet.ProgrammeVersioningID);
            model.CountryCode = Convert.ToInt32(itemSet.CountryCode);
            model.CountryName = (itemSet.CountryCode == 1) ? "India" : (itemSet.CountryCode == 2) ? "Malaysia" : "United States";
            model.FacultyCode = Convert.ToInt32(itemSet.FacultyCode);
            model.AcademicYearCode = Convert.ToInt32(itemSet.AcademicYearCode);
            model.ProgramName = itemSet.ProgramName;
            model.Version = itemSet.Version;
            model.FacultyName = itemSet.FacultyName;
            model.EmployeeID = itemSet.EmployeeID;
            model.EmployeeName = itemSet.EmployeeName;
            model.YearName = itemSet.YearName;
            model.ModifiedOn = itemSet.ModifiedOn?.Date;
            model.CreatedBy = Convert.ToInt32(itemSet.CreatedBy);
            model.CreatedOn = itemSet.CreatedOn;
            model.ModifiedBy = Convert.ToInt32(itemSet.ModifiedBy);
            model.SubAllocationList = new List<SubjectAllocationList>();
            model.SubAllocationDetailsList = new List<SubjectAllocationDetailsList>();
            model.AllocationList = new SubjectAllocationList();
            model.SubAllocationList = (from objChildtbl1 in itemSet.SubAllocationList
                                       select new SubjectAllocationList
                                       {
                                           SemisterID = objChildtbl1.SemisterID,
                                           SemisterName = objChildtbl1.SemisterName,

                                       }).ToList();

            model.AllocationList.SubAllocationDetailsList = (from objChildtbl1 in itemSet.AllocationList.SubAllocationDetailsList
                                                             select new SubjectAllocationDetailsList
                                                             {
                                                                 CourseID = objChildtbl1.CourseID,
                                                                 CourseName = objChildtbl1.CourseName,
                                                                 ProgrammeSemesterID = objChildtbl1.ProgrammeSemesterID,
                                                                 Allocation = objChildtbl1.Status.ToString()
                                                             }).ToList();


            return model;
        }

        public PartialViewResult AddSubjectAllocation(string id)
        {
            var model = new SubjectAllocationViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = SelectSubjectAllocation(id);
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
                model.SyllabusVersionList = onlineExamService.GetAllProgramVersioning().Where(a => a.ProgramVersioningID == Convert.ToInt32(model.ProgrammeVersioningID)
                && a.Status == "A")
                       .Select(a => new SelectListItem
                       {
                           Text = a.Version,
                           Value = a.ProgramVersioningID.ToString()

                       }).ToList();


                var itemList = onlineExamService.GetAllProgrammeSemester().Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgrammeVersioningID)
                && a.Status == "A").ToList();
                model.CountryList = itemList?.Select(a => new SelectListItem() { Text = (a.CountryID == 1) ? "India" : (a.CountryID == 2) ? "Malaysia" : "United States", Value = a.CountryID.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                          .Select(g => g.FirstOrDefault())
                                          .ToList();

                model.AcademicYearList = itemList?.Where(a => a.ProgrammeVersioningID == Convert.ToInt32(model.ProgrammeVersioningID) &&
                a.CountryID == model.CountryCode).Select(a => new SelectListItem() { Text = a.ProgrammeYear.ToString(), Value = a.ProgrammeYear.ToString() }).ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();
                var EmpList = onlineExamService.GetAllEmployee().Where(a => a.Status == "A").ToList();
                model.EmployeeList = EmpList?.Select(a => new SelectListItem() { Text = a.EmployeeName.ToString(), Value = a.EmployeeID.ToString() })
                    .ToList().GroupBy(n => new { n.Text, n.Value })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();

            }
            else
            {
                model.FacultyList = new List<SelectListItem>();
                model.ProgramList = new List<SelectListItem>();
                model.SyllabusVersionList = new List<SelectListItem>();
                model.CountryList = new List<SelectListItem>
                            {
                                new SelectListItem{ Text="India", Value = "1" },
                                new SelectListItem{ Text="Malaysia", Value = "2" },
                                new SelectListItem{ Text="United States", Value = "3" },
                             };
                model.EmployeeList = onlineExamService.GetAllEmployee().Where(a => a.Status == "A").Select(a => new SelectListItem() { Text = a.EmployeeName.ToString(), Value = a.EmployeeID.ToString() }).ToList();

                model.AcademicYearList = new List<SelectListItem>();
                model.SubAllocationList = new List<SubjectAllocationList>();
                model.AllocationList = new SubjectAllocationList();
                model.AllocationList.SubAllocationDetailsList = new List<SubjectAllocationDetailsList>();
                model.SubjectAllocationID = 0;
            }
            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();

            return PartialView("_addSubjectAllocation", model);
        }

        public PartialViewResult SubjectAllocationView(string id)
        {
            return PartialView("_viewSubjectAllocation", SelectSubjectAllocation(id));
        }

        public PartialViewResult SubjectAllocationList()
        {
            return PartialView("_listSubjectAllocation", GetAllSubjectAllocation());
        }

        [HttpPost]
        public JsonResult SaveSubjectAllocation(SubjectAllocationViewModel model)
        {
            var type = "INSERT";
            if (model.SubjectAllocationID > 0)
            {
                type = "UPDATE";
            }
            XElement xEle = null;
            if (model.SubAllocationDetailsList.Any())
            {
                xEle = new XElement("Allocations",
                        from emp in model.SubAllocationDetailsList
                        select new XElement("Allocation",
                                     new XElement("ProgrammeSemesterID", emp.ProgrammeSemesterID),
                                       new XElement("CourseID", emp.CourseID),
                                       new XElement("Status", emp.Allocation)
                                   ));
            }

            var result = onlineExamService.SaveSubjectAllocation(new OnlineExam.Request.SubjectAllocationRequestDTO()
            {
                SubjectAllocationID = Convert.ToInt32(model.SubjectAllocationID),
                CreatedBy = User.UserId,
                DepartmentID = Convert.ToInt32(model.FacultyCode),
                ProgrammeVersioningID = Convert.ToInt32(model.ProgrammeVersioningID),
                CountryID = Convert.ToInt32(model.CountryCode),
                EmployeeID = Convert.ToInt32(model.EmployeeID),
                AcademicID = Convert.ToInt32(model.AcademicYearCode),
                TabAllocationDetails = xEle.ToString().Trim(),
                Active = model.Active

            }, type);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSubjectAllocation(SubjectAllocationViewModel model)
        {

            var result = onlineExamService.SaveSubjectAllocation(new OnlineExam.Request.SubjectAllocationRequestDTO()
            {

                CreatedBy = User.UserId,
                SubjectAllocationID = Convert.ToInt32(model.SubjectAllocationID)


            }, "DELETE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult AllocationDetails(SubjectAllocationViewModel model)
        {
            model.SubAllocationList = new List<SubjectAllocationList>();
            model.AllocationList = new SubjectAllocationList();
            model.AllocationList.SubAllocationDetailsList = new List<SubjectAllocationDetailsList>();
            model.SubAllocationList = onlineExamService.GetAllProgrammeSemester().Where(a => a.DepartmentID == model.FacultyCode &&
            a.ProgrammeVersioningID == Convert.ToInt32(model.ProgrammeVersioningID)
                                                   && a.CountryID == Convert.ToInt32(model.CountryCode) &&
                                                   a.ProgrammeYear == Convert.ToInt32(model.AcademicYearCode) &&
                                                   a.Status == "A"
                                                   ).Select(a => new SubjectAllocationList
                                                   {
                                                       SemisterID = a.ProgrammeSemesterID,
                                                       SemisterName = a.ProgrammeSemester.ToString()

                                                   }).ToList();



            model.AllocationList.SubAllocationDetailsList = onlineExamService.GetAllCourse().Where(a => a.ProgramVersioningID == Convert.ToInt32(model.ProgrammeVersioningID)
            && a.CountryId == Convert.ToInt32(model.CountryCode) &&
                                                  a.ProgrammeYear == Convert.ToInt32(model.AcademicYearCode) &&
                                                  a.Status == "A")
            .Select(a => new SubjectAllocationDetailsList
            {
                CourseID = a.CourseID,
                CourseName = a.CourseName.ToString(),
                ProgrammeSemesterID = a.ProgrammeSemesterID

            }).ToList();


            return PartialView("_AllocationDetails", model);
        }

        #endregion
    }
}