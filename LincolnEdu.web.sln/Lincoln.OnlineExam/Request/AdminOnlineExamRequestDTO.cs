using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class AdminOnlineExamRequestDTO
    {
        public int? CourseID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ProgrammeID { get; set; }
        public int ? ProgrammeVersioningID { get; set; }
        public int? ProgrammeSemesterID { get; set; }
        public int? CountryID { get; set; }
        public int? EmployeeID { get; set; }
        public string ExaminationXML { get; set; }
        public int  CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
