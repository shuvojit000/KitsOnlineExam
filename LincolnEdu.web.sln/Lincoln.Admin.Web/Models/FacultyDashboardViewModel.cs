using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class FacultyDashboardViewModel
    {

        public int NoOfQuestion { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
    }
}