using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Models
{
    public class CourseViewModel
    {
        public int? CourseID { get; set; }
        public int? AcademicID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ProgrammeID { get; set; }
        public int? CountryID { get; set; }
        public string DepartmentName { get; set; }
        public string ProgrammeName { get; set; }
        public int? ProgrammeYear { get; set; }
        public int? ProgrammeSemester { get; set; }
        public string SemesterType { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string ApprovalNo { get; set; }
        public string Credit { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Active { get; set; }

        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> ProgrammeList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> ProgYearList { get; set; }
        public List<SelectListItem> ProgSEMList { get; set; }

    }
}