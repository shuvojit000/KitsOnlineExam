using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class ExaminationConfigurationRequestDTO
    {
        public int? ExaminationID { get; set; }
        public int? CourseID { get; set; }
        public int? StudentID { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public string ExaminationTime { get; set; }
        public int? ExaminationDuration { get; set; }
        public int? EmployeeID { get; set; }
        public string ReviewStatus { get; set; }
        public decimal? MarksObtained { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
