using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
   public class FacultyDashboardResponseDTO
    {
       public int? SubjectAllocationID { get; set; }
        public int? SubjectAllocationDetailsID { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public int? CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string SectionName { get; set; }
        public int NoOfQuestion { get; set; }
        public string SectionInQuestion { get; set; }
    }
}
