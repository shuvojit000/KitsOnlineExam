using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class ExamQuestionSectionResponseDTO
    {
        public string SectionName { get; set; }
        public int? ExaminationSectionID { get; set; }
        public int? QuestionNo { get; set; }
    }
}
