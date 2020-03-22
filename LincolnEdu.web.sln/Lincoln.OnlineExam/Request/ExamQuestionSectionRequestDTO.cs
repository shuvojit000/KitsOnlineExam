using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class ExamQuestionSectionRequestDTO
    {
        public int CourseID { get; set; }
        public int? QuestionNo { get; set; }
        public string SectionName { get; set; }

    }
}
