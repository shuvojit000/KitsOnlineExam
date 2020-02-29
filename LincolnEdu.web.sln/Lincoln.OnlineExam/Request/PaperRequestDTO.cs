using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class PaperRequestDTO
    {
        public int? PaperID { get; set; }
        public int? LoginID { get; set; }
        public int? CourseID { get; set; }  
        public int? ExaminationSectionID { get; set; }
        public string SectionName { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
