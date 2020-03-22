using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class StudentExamViewModel
    {

    }
    public class ExamQuestionSectionViewModel
    {
        public string SectionName { get; set; }
        public int? ExaminationSectionID { get; set; }
        public int? QuestionNo { get; set; }
    }
}