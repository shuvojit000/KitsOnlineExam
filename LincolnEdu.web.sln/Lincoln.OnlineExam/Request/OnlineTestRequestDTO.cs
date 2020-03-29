using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Request
{
    public class OnlineTestRequestDTO
    {
        public int LoginID { get; set; }
        public int? IsAnswer { get; set; }
        public int? QuestionNo { get; set; }

    }
}
