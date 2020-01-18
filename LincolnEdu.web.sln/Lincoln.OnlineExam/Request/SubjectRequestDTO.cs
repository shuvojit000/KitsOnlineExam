using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class SubjectRequestDTO
    {
        public int SubjectID { get; set; }
        public int CourseID { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Status { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
       


    }
}
