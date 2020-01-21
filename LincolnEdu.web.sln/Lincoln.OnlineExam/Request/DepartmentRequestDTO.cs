using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class DepartmentRequestDTO
    {
        public int DepartmentID { get; set; }
        public int AcademicID { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string HODName { get; set; }
        public string HODEmail { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
