﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class StudentExamViewModel
    {
        public string ExaminationName { get; set; }
        public int? CourseID { get; set; }
    }
    public class ExamQuestionSectionViewModel
    {
        public string SectionName { get; set; }
        public int? ExaminationSectionID { get; set; }
        public int? MaxQuestionNo { get; set; }
        public int? MinQuestionNo { get; set; }
    }
    public class ExamHeaderButtonViewModel
    {
        public int CourseID { get; set; }
    }
    public class ExamLeftPanelViewModel
    {
        public int? StudentID { get; set; }
        public int? QuestionTotal { get; set; }
        public int? AnswerTotal { get; set; }
        public int? FlagTotal { get; set; }
        public int? QuestionNo { get; set; }
        public int? CoureseID { get; set; }
        public List<int> NotAnsweredList { get; set; }
        public List<int> CompletedList { get; set; }
        public List<int> FlagedList { get; set; }
    }
   
}