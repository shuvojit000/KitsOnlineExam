using Lincoln.OnlineExam.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam
{
    public interface IOnlineExam
    {
        int ValidateUser(LogInRequestDTO request);
    }
}
