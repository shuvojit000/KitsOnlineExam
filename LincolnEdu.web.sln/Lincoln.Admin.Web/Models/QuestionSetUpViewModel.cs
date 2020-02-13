using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Models
{
    public class QuestionSetUpViewModel
    {
        public int? AcademicID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ProgrammeID { get; set; }
        public int? CountryID { get; set; }
        public int? ProgrammeYear { get; set; }
        public int? ProgrammeSemester { get; set; }
        public int? CourseID { get; set; }
        public string SectionName { get; set; }
        public int? SyllabusVersionID { get; set; }
        public string QuestionType { get; set; }
        public string QuestionStatement { get; set; }
        public int QuestionMarks { get; set; }

        public string AnswerNo { get; set; }
        public string AnswerStatement { get; set; }

        public List<SelectListItem> ProgramList { get; set; }
        public List<SelectListItem> ProgrammeYearList { get; set; }
        public List<SelectListItem> CourseList { get; set; }
        public List<SelectListItem> SemisterList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> SyllabusVersionList { get; set; }
        public List<SelectListItem> FacultyList { get; set; }
        public List<SelectListItem> SectionList { get; set; }

    }
}