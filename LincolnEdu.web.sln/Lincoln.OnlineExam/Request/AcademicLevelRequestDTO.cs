using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class AcademicLevelRequestDTO
    {
        
        public int AcademicID { get; set; }
        public string AcademicCode { get; set; }
        public string AcademicName { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
