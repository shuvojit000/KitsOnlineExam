﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lincoln.OnlineExam.Request
{
    public class SubjectAllocationRequestDTO
    {
        public int? SubjectAllocationID { get; set; }
        public int? ProgrammeVersioningID { get; set; }
        public string ProgramName { get; set; }
        public int? EmployeeID { get; set; }
        public int? DepartmentID { get; set; }
        public string FacultyName { get; set; }
        public string Version { get; set; }
        public string SyllabusVersionName { get; set; }
        public int? CountryID { get; set; }
        public string CountryName { get; set; }
        public int? AcademicID { get; set; }
        public string YearName { get; set; }
        public int? CreatedBy { get; set; }
        public string Status { get; set; }
        public string Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<SelectListItem> ProgramList { get; set; }
        public List<SelectListItem> AcademicYearList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> SyllabusVersionList { get; set; }
        public List<SelectListItem> FacultyList { get; set; }
        public SubjectAllocationList AllocationList { get; set; }
        public List<SubjectAllocationList> SubAllocationList { get; set; }
        public string TabAllocationDetails { get; set; }
        public List<SubjectAllocationDetailsList> SubAllocationDetailsList { get; set; }
    }

    public class SubjectAllocationList
    {
        public int SlNo { get; set; }
        public int SubjectAllocationID { get; set; }
        public int? SemisterID { get; set; }
        public string SemisterName { get; set; }
        public SubjectAllocationDetailsList AllocationDetails { get; set; }
        public List<SubjectAllocationDetailsList> SubAllocationDetailsList { get; set; }
    }


    public class SubjectAllocationDetailsList
    {
        public int SlNo { get; set; }
        public string SubjectAllocationDetailsID { get; set; }
        public int? CourseID { get; set; }
        public int? ProgrammeSemesterID { get; set; }
        public string CourseName { get; set; }
        public int Allocation { get; set; }
    }
}
