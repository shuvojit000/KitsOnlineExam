﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class PaperDetailsRequestDTO
    {
        public int? PaperDetailsID { get; set; }
        public int? LoginID { get; set; }
        public int? PaperID { get; set; }
        public int? CourseID { get; set; }
        public string QuestionType { get; set; }
        public int? QuestionNo { get; set; }
        public string QuestionText { get; set; }
        public string TextOrImageQuestion { get; set; }
        public string AudioOrVideoQuestion { get; set; }
        public decimal? QuestionMarks { get; set; }
        public decimal? SectionMarks { get; set; }
        public int? OptionANo { get; set; }
        public string OptionAText { get; set; }
        public int? OptionBNo { get; set; }
        public string OptionBText { get; set; }
        public int? OptionCNo { get; set; }
        public string OptionCText { get; set; }
        public int? OptionDNo { get; set; }
        public string OptionDText { get; set; }
        public int? OptionENo { get; set; }
        public string OptionEText { get; set; }
        public int? AnswerNo { get; set; }
        public string AnswerText { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
