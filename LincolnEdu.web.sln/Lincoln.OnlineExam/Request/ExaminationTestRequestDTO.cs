using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class ExaminationTestRequestDTO
    {
        public int? StudentID { get; set; }
        public int? QuestionNo { get; set; }
        public int? AnswerNo { get; set; }
        public string AnswerText { get; set; }
        public int? IsAnswer { get; set; }
        public int Status { get; set; }
        public int? LoginID { get; set; }
        public int? PaperID { get; set; }
        public int? CourseID { get; set; }
    }
}
