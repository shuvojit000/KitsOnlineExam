using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class PaperResponseDTO
    {
        public int? LoginID { get; set; }
        public int? PaperID { get; set; }
        public int? QuestionNo { get; set; }
        public int? CourseID { get; set; }
        public int? ExaminationSectionID { get; set; }
        public string SectionName { get; set; }
        public decimal? SectionMarks { get; set; }
    }
}
