using Lincoln.OnlineExam.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Repository
{
    public interface IUserRepository
    {
        int ValidateUser(LogInRequestDTO request);
    }
}
