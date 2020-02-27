using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class ProgrammeSemesterRequestDTO
    {
        public int? ProgrammeSemesterID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ProgramVersioningID { get; set; }
        public int? ProgrammeID { get; set; }
        public int? CountryID { get; set; }
        public int? ProgrammeYear { get; set; }
        public int? ProgrammeSemester { get; set; }
        public string SemesterType { get; set; }
        public string Active { get; set; }
        public int? CreatedBy { get; set; }
        public int Status { get; set; }

    }
}
