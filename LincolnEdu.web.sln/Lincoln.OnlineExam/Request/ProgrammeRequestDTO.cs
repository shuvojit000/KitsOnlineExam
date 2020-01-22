using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class ProgrammeRequestDTO
    {
        public int? ProgrammeID { get; set; }
        public int? AcademicID { get; set; }
        public int? DepartmentID { get; set; }
        public string ProgrammeCode { get; set; }
        public string ProgrammeName { get; set; }
        public string ApprovalNo { get; set; }
        public string Credit { get; set; }
        public string Active { get; set; }
        public int? CreatedBy { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }

    }
}
