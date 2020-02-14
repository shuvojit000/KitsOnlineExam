using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lincoln.OnlineExam.Response
{
   public class SubjectAllocationResponseDTO
    {
    
        public int? SubjectAllocationID { get; set; }
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
         public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        // public int? CourseCode { get; set; }

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

        public List<SelectListItem> EmployeeList { get; set; }
        public SubjectAllocationListR AllocationList { get; set; }
        public List<SubjectAllocationListR> SubAllocationList { get; set; }
        public string TabAllocationDetails { get; set; }
        public List<SubjectAllocationDetailsListR> SubAllocationDetailsList { get; set; }
    }

    public class SubjectAllocationListR
    {
        public int SlNo { get; set; }
        public int SubjectAllocationID { get; set; }
        public int SemisterID { get; set; }
        public string SemisterName { get; set; }
        public SubjectAllocationDetailsListR AllocationDetails { get; set; }
        public List<SubjectAllocationDetailsListR> SubAllocationDetailsList { get; set; }
    }


    public class SubjectAllocationDetailsListR
    {
        public int SlNo { get; set; }
        public string SubjectAllocationDetailsID { get; set; }
        public int CourseID { get; set; }
        public int ProgrammeSemesterID { get; set; }
        public string CourseName { get; set; }
        public int Allocation { get; set; }
    }
}
