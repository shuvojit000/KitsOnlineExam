using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class StudentRequestDTO
    {
        public int StudentID { get; set; }
        public int LoginID { get; set; }
        public int BatchID { get; set; }
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string Active { get; set; }
        public string Password { get; set; }
        public int CreatedBy { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
    }
}
