using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Models
{
    public class QuestionSetUpViewModel
    {      
        
        public int? CourseID { get; set; }
        public int? ExaminationSectionID { get; set; }
        public string SectionName { get; set; }       
        public int? PaperDetailsID { get; set; }
        public int? PaperID { get; set; }
        public string QuestionType { get; set; }
        public int? QuestionNo { get; set; }
        public string QuestionText { get; set; }
        public string TextOrImageQuestion { get; set; }
        public string AudioOrVideoQuestion { get; set; }
        public decimal? QuestionMarks { get; set; }
        public decimal? SectionMarks { get; set; }
        public decimal? RemainingMarks { get; set; }
        public int? OptionANo { get; set; }
        public string OptionAText { get; set; }
        public int? OptionBNo { get; set; }
        public string OptionBText { get; set; }
        public int? OptionCNo { get; set; }
        public string OptionCText { get; set; }
        public int? OptionDNo { get; set; }
        public string OptionDText { get; set; }
        public int? OptionENo { get; set; }
        public string OptionEText { get; set; }
        public int? AnswerNo { get; set; }
        public string AnswerText { get; set; }
        public string Active { get; set; }
        public int IsAnswer { get; set; }
        public List<SelectListItem> ProgramList { get; set; }
        public List<SelectListItem> ProgrammeYearList { get; set; }
        public List<SelectListItem> CourseList { get; set; }
        public List<SelectListItem> SemisterList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> SyllabusVersionList { get; set; }
        public List<SelectListItem> FacultyList { get; set; }
        public List<SelectListItem> ExamSectionList { get; set; }


        public int? ExaminationDuration { get; set; }
        public int? TotalTime { get; set; }
    }
}