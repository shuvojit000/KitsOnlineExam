using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Models
{
    public class ProgrammeSemesterViewModel
    {
        public int? ProgrammeSemesterID { get; set; }
        public int? AcademicID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ProgrammeID { get; set; }
        public int? CountryID { get; set; }
        public string CountryName { get; set; }
        public string AcademicName { get; set; }
        public string DepartmentName { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; }
        public int? ProgrammeYear { get; set; }
        public int? ProgrammeSemester { get; set; }
        public string SemesterType { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Version { get; set; }
        public int? ProgramVersioningID { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> ProgrammeList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> ProgYearList { get; set; }
        public List<SelectListItem> ProgSEMList { get; set; }
        public string Active { get; set; }
       
        public List<SelectListItem> AcademicList { get; set; }
    }
}