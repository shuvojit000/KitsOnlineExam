using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Response;

namespace Lincoln.OnlineExam.Repository
{
    public interface IQuestionRepository
    {
        int SavePaper(PaperRequestDTO recordAttributer, string Operation);
        PaperResponseDTO SelectPaper(PaperRequestDTO recordAttributer);
        int SavePaperDetails(PaperDetailsRequestDTO recordAttributer, string Operation);
        List<PaperDetailsResponseDTO> GetAllPaperDetails(PaperDetailsRequestDTO recordAttributer);
        PaperDetailsResponseDTO SelectAllPaperDetails(PaperDetailsRequestDTO recordAttributer);
    }
}
