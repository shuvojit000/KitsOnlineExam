using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam
{
    public interface IOnlineExam
    {
        LogInResponseDTO ValidateUser(LogInRequestDTO request, string Operation);
        int SaveStudent(StudentRequestDTO recordAttributer, string Operation);
    }
}
