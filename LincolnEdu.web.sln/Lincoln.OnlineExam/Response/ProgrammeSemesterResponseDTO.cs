using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class ProgrammeSemesterResponseDTO
    {
        public int? ProgrammeSemesterID { get; set; }
        public int? AcademicID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ProgrammeID { get; set; }
        public int? CountryID { get; set; }
        public string AcademicName { get; set; }
        public string DepartmentName { get; set; }
        public string ProgrammeName { get; set; }
        public int? ProgrammeYear { get; set; }
        public int? ProgrammeSemester { get; set; }
        public string SemesterType { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
