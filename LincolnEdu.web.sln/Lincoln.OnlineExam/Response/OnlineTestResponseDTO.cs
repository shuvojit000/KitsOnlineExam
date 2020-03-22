using System;
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Status { get; set; }
        public string ExamAttendance { get; set; }
    }
}
