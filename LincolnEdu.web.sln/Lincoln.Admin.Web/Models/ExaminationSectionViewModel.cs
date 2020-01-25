using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Models
{
    public class ExaminationSectionViewModel
    {
        public int ExaminationSectionID { get; set; }
        public int ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public int FacultyCode { get; set; }
        public string FacultyName { get; set; }
        public int SyllabusVersionCode { get; set; }
        public string SyllabusVersionName { get; set; }
        public int CountryCode { get; set; }
        public string CountryName { get; set; }
        public int AcademicYearCode { get; set; }
        public string YearName { get; set; }
        public int SemisterCode { get; set; }
        public string SemisterName { get; set; }
        public int CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public List<SelectListItem> ProgramList { get; set; }
        public List<SelectListItem> CourseList { get; set; }
        public List<SelectListItem> SemisterList { get; set; }
        public List<SelectListItem> AcademicYearList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> SyllabusVersionList { get; set; }
        public List<SelectListItem> FacultyList { get; set; }
    }
}