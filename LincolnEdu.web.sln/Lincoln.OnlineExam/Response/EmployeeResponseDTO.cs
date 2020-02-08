using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class EmployeeResponseDTO
    {
        public int EmployeeID { get; set; }
        public int LoginID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
