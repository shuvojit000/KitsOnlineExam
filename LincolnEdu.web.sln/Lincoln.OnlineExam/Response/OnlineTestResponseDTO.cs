﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class OnlineTestResponseDTO
    {
        public int SLNo { get; set; }
        public int? LoginID { get; set; }
        public string StudentName { get; set; }
        public string ExaminationName { get; set; }
        public int? CourseID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Status { get; set; }
        public string ExamAttendance { get; set; }
        public int? ExaminationDuration { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public decimal? TotalMarks { get; set; }
        public decimal? MarksObtained { get; set; }
        public int? IsCalculator { get; set; }
        public int? ExaminationID { get; set; }
        public string TimerTime { get; set; }
    }
}
