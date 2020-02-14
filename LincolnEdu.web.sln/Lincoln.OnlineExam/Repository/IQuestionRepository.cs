using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincoln.OnlineExam.Request;

namespace Lincoln.OnlineExam.Repository
{
    public interface IQuestionRepository
    {
        int SavePaper(PaperRequestDTO recordAttributer, string Operation);
        int SavePaperDetails(PaperDetailsRequestDTO recordAttributer, string Operation);
    }
}
