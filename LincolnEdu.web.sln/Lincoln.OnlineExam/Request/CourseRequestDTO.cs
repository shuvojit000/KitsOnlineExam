using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class CourseRequestDTO
    {
        public int? CourseID { get; set; }
        public int? AcademicID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ProgrammeID { get; set; }
        public int? CountryID { get; set; }
        public int? ProgrammeYear { get; set; }
        public int? ProgrammeSemester { get; set; }
        public int? CountryId { get; set; }
        public string SemesterType { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string ApprovalNo { get; set; }
        public string Credit { get; set; }
        public int Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Active { get; set; }
    }
}
