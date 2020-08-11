using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class AdminAnswerReviewViewModels
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
        public decimal? MarksObtained { get; set; }
        public int? ResultApproved { get; set; }
    }
}