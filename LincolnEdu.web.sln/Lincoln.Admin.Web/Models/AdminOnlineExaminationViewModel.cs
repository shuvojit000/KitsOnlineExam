using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Models
{
    public class AdminOnlineExaminationViewModel
    {
        public int? AcademicID { get; set; }
        public int? CourseID { get; set; }
        public string strCourseID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ProgrammeID { get; set; }
        public int? ProgrammeVersioningID { get; set; }
        public int? ProgrammeSemesterID { get; set; }
        public int? CountryID { get; set; }
        public int? EmployeeID { get; set; }
        public string IntakeID { get; set; }
        public string strEmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ? ProgrammeYear { get; set; }
        public int? ExaminationID { get; set; }
        public string EnrollmentNo { get; set; }
        public List<SelectListItem> AcademicList { get; set; }
        public List<SelectListItem> ProgramList { get; set; }
        public List<SelectListItem> CourseList { get; set; }
        public List<SelectListItem> SemisterList { get; set; }
        public List<SelectListItem> ProgYearList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> ExaminationList { get; set; }
        public List<SelectListItem> IntakeList { get; set; }
        public string EmailID { get; set; }
        public int? StudentID { get; set; }
        public string strStudentID { get; set; }
        public string StudentName { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public string ExaminationTime { get; set; }
        public int? ExaminationDuration { get; set; }
        public string ReviewStatus { get; set; }
        public decimal? MarksObtained { get; set; }
        public decimal? MaximumMarks { get; set; }
        public string CourseName { get; set; }
        public int? PaperDetailsID { get; set; }
        public int? QuestionNo { get; set; }
        public string PaymentStatus { get; set; }
        public List<AdminOnlineExaminationViewModel> CustomList { get; set; }
        public int? IsCalculator { get; set; }
    }
}