using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class ExaminationSectionRequestDTO
    {
        public int? ExaminationSectionID { get; set; }
        public int? ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public int? FacultyCode { get; set; }
        public string FacultyName { get; set; }
        public int? ProgrammeVersioningID { get; set; }
        public string Version { get; set; }
        public int? CountryCode { get; set; }
        public string CountryName { get; set; }
        public int? AcademicYearCode { get; set; }
        public string YearName { get; set; }
        public int? SemisterCode { get; set; }
        public string SemisterName { get; set; }
        public int? SectionID { get; set; }
        public string SectionName { get; set; }
        public string CourseCode { get; set; }
        public int? CourseID { get; set; }
        public string QuestionType { get; set; }
        public string CourseName { get; set; }
        public string Active { get; set; }
        public int? CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
