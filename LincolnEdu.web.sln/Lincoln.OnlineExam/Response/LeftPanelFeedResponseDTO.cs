using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class LeftPanelFeedResponseDTO
    {
        public int? StudentID { get; set; }
        public int? QuestionTotal { get; set; }
        public int? AnswerTotal { get; set; }
        public int? FlagTotal { get; set; }
        public int? QuestionNo { get; set; }
    }
}
