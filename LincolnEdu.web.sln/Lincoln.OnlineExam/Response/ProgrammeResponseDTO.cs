using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class ProgrammeResponseDTO
    {
        public int ProgrammeID { get; set; }
        public int? AcademicID { get; set; }
        public int? DepartmentID { get; set; }
        public string AcademicName { get; set; }
        public string DepartmentName { get; set; }
        public string ProgrammeCode { get; set; }
        public string ProgrammeName { get; set; }
        public string ApprovalNo { get; set; }
        public string Credit { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
