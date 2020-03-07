using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class StudentDashboardViewModel
    {
        public int ProgrammeSemesterID { get; set; }
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public int StudentID { get; set; }
        public int LoginID { get; set; }
        public string CourseName { get; set; }
        public string StudentName { get; set; }
        public string UserName { get; set; }
        public string RollNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string UserType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class AnswerSetUpdViewModel
    {

        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int? DepartmentID { get; set; }
        public int? ProgrammeSemesterID { get; set; }
        public int? CourseID { get; set; }
        public int? PaperDetailsID { get; set; }
        public int? PaperID { get; set; }
        public string QuestionType { get; set; }
        public int QuestionNo { get; set; }
        public string QuestionText { get; set; }
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
        public string AnswerText { get; set; }
        public string Active { get; set; }
    }
}