﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class SubjectAssessmentRequestDTO
    {
        public int SubjectAssessmentID { get; set; }
        public int ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public int FacultyCode { get; set; }
        public string FacultyName { get; set; }
        public int SyllabusVersionCode { get; set; }
        public string SyllabusVersionName { get; set; }
        public int CountryCode { get; set; }
        public string CountryName { get; set; }
        public int AcademicYearCode { get; set; }
        public string YearName { get; set; }
        public int SemisterCode { get; set; }
        public string SemisterName { get; set; }
        public int CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<SubjectAssessmentDetails> SubAssessmentDetails { get; set; }
    }
    public class SubjectAssessmentDetails
    {
        public int SlNo { get; set; }
        public string AssessmentName { get; set; }
        public string Assessment { get; set; }
        public string AssessmentType { get; set; }
        public string FullMarks { get; set; }
        public bool OpenClose { get; set; }
    }
}