using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class StudentExaminationSheetResponseDTO
    {
        public int? LoginID { get; set; }
        public int? PaperID { get; set; }
        public int? PaperDetailsID { get; set; }
        public int? TotalTime { get; set; }
        public int? ExaminationDuration { get; set; }
        public int? CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
