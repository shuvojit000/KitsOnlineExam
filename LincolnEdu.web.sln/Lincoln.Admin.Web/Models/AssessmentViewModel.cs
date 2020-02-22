using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Models
{
    public class AssessmentViewModel
    {
        public int AssessmentID { get; set; }
        public int? ProgrammeID { get; set; }
        public int? DepartmentID { get; set; }
        public int SyllabusVersion { get; set; }

        public string ProgramName { get; set; }
        public string DepartmentName { get; set; }
        public string SyllabusVersionName { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public List<SelectListItem> ProgramList { get; set; }
        public List<SelectListItem> SyllabusVersionList { get; set; }
        public List<SelectListItem> FacultyList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }

    }
}