using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class DepartmentResponseDTO
    {
        public int DepartmentID { get; set; }
        public int AcademicID { get; set; }
        public string AcademicName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string HODName { get; set; }
        public string HODEmail { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
