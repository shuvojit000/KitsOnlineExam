﻿using System;
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
        public int? ProgrammeVersioningID { get; set; }
        public string Version { get; set; }
        public int CountryCode { get; set; }
        public string CountryName { get; set; }
        public int AcademicYearCode { get; set; }
        public string YearName { get; set; }
        public int SemisterCode { get; set; }
        public string SemisterName { get; set; }
        public string SectionName { get; set; }
        public string SectionID { get; set; }
        public string CourseCode { get; set; }
        public int CourseID { get; set; }
        public string QuestionType { get; set; }
        public string CourseName { get; set; }
        public decimal? MaximumMarks { get; set; }
        public int? AcademicID { get; set; }
        public string AcademicName { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<SelectListItem> AcademicList { get; set; }
        public List<SelectListItem> ProgramList { get; set; }
        public List<SelectListItem> CourseList { get; set; }
        public List<SelectListItem> SemisterList { get; set; }
        public List<SelectListItem> AcademicYearList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> SyllabusVersionList { get; set; }
        public List<SelectListItem> FacultyList { get; set; }

        public List<SelectListItem> SectionList { get; set; }
    }
}