using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class SubjectAllocationRequestDTO
    {
        public int SubjectAllocationID { get; set; }
        public int ProgrammeID { get; set; }
       // public string ProgramName { get; set; }
        public int EmployeeID { get; set; }
       // public string FacultyName { get; set; }
       // public int Version { get; set; }
        public string Version { get; set; }
        public int CountryID { get; set; }
       // public string CountryName { get; set; }
        public int AcademicID { get; set; }
       // public string Version { get; set; }
       // public int SemisterCode { get; set; }
      //  public string SemisterName { get; set; }
       // public int CourseCode { get; set; }
       // public string CourseName { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string AllocationDetails { get; set; }
        public List<SubjectAllocationtDetails> SubAllocationDetails { get; set; }
    }
    public class SubjectAllocationtDetails
    {
        public int SubjectAllocationDetailsID { get; set; }
        public int SlNo { get; set; }
        public string AssessmentName { get; set; }
        public decimal Assessment { get; set; }
        public string AssessmentType { get; set; }
        public decimal FullMarks { get; set; }
        public int OpenClose { get; set; }
    }
}
