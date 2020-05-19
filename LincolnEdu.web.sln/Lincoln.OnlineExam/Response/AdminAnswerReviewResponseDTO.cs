using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class AdminAnswerReviewResponseDTO
    {
        public int? PaperDetailsID { get; set; }
        public int? PaperID { get; set; }
        public int? QuestionNo { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public decimal? QuestionMarks { get; set; }
        public string AnswerNo { get; set; }
        public string AnswerText { get; set; }
        public string AnswerNoByStudent { get; set; }
        public string AnswerTextByStudent { get; set; }
        public decimal? QuestionMarksObtain { get; set; }
    }
}
