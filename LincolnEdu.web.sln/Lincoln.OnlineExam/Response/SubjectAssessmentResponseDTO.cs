using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lincoln.OnlineExam.Response
{
   public class SubjectAssessmentResponseDTO
    {
    //    public int SubjectAssessmentID { get; set; }
    //    public int ProgramCode { get; set; }
    //    public string ProgramName { get; set; }
    //    public int FacultyCode { get; set; }
    //    public string FacultyName { get; set; }
    //    public int SyllabusVersionCode { get; set; }
    //    public string SyllabusVersionName { get; set; }
    //    public int CountryCode { get; set; }
    //    public string CountryName { get; set; }
    //    public int AcademicYearCode { get; set; }
    //    public string YearName { get; set; }
    //    public int SemisterCode { get; set; }
    //    public string SemisterName { get; set; }
    //    public int CourseCode { get; set; }
    //    public string CourseName { get; set; }

    //    public int CreatedBy { get; set; }
    //    public string Status { get; set; }
    //    public DateTime? CreatedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime? ModifiedOn { get; set; }
    //    public  SubjectAssessmentDetails TabularDetails { get; set; }
    //    public List<SubjectAssessmentDetails> SubAssessmentDetails { get; set; }
    //}
    //public class SubjectAssessmentDetails
    //{
    //    public int AssessmentConfigurationDetailsID { get; set; }
    //    public int SlNo { get; set; }
    //    public string AssessmentName { get; set; }
    //    public decimal AssessmentPercentage { get; set; }
    //    public string AssessmentType { get; set; }
    //    public decimal FullMarks { get; set; }
    //    public string OpenClose { get; set; }
    //}

    //public class SubjectAssessmentViewModel
    //{
        public int? SubjectAssessmentID { get; set; }
        public int? ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public int? FacultyCode { get; set; }
        public string FacultyName { get; set; }
        public int? SyllabusVersionCode { get; set; }
        public string SyllabusVersionName { get; set; }
        public int? CountryCode { get; set; }
        public string CountryName { get; set; }
        public int? AcademicYearCode { get; set; }
        public string YearName { get; set; }
        public int? SemisterCode { get; set; }
        public string SemisterName { get; set; }
        public int? CourseCode { get; set; }

        public int? CourseID { get; set; }
        public string CourseName { get; set; }
        public int? CreatedBy { get; set; }
        public string Status { get; set; }

        public string Active { get; set; }
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
        public SubjectAssessmentDetails TabulalConfigurationDetails { get; set; }
        public List<SubjectAssessmentDetails> SubAssessmentDetails { get; set; }
    }

    public class SubjectAssessmentDetails
    {
        public int AssessmentConfigurationDetailsID { get; set; }
        public int SlNo { get; set; }
        public string AssessmentName { get; set; }
        public decimal AssessmentPercentage { get; set; }
        public string AssessmentType { get; set; }
        public decimal FullMarks { get; set; }
        public string OpenClose { get; set; }
    }
}
