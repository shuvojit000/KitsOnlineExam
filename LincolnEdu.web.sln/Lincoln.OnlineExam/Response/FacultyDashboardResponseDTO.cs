using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
   public class FacultyDashboardResponseDTO
    {
        public int NoOfQuestion { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
    }
}
