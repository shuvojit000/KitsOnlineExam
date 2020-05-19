using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class OnlineExamAppResponseDTO
    {
        public int? StudentID { get; set; }
        public int? CourseID { get; set; }
        public string CourseName { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string StudentName { get; set; }
        public string Status { get; set;}
        public string PaymentStatus { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
