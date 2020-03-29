using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class StudentExaminationTestResponseDTO
    {
        public int? ExaminationTestID { get; set; }
        public int? StudentID { get; set; }
        public int? QuestionNo { get; set; }
        public int? AnswerNo { get; set; }
        public string AnswerText { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
