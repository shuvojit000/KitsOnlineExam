using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class AssessmentResponseDTO
    {
        public int AssessmentID { get; set; }
        public int? ProgrammeID { get; set; }
        public int? DepartmentID { get; set; }
        public int SyllabusVersionCode { get; set; }
        public string ProgramName { get; set; }
        public string DepartmentName { get; set; }
        public string SyllabusVersionName { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
