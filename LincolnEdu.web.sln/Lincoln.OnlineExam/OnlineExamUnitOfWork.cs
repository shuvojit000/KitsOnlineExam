using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincoln.OnlineExam.Repository;

namespace Lincoln.OnlineExam
{
    public class OnlineExamUnitOfWork : IDisposable
    {
        
        private IUserRepository userRepository;


        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {

                    this.userRepository = new UserRepository();
                }

                return this.userRepository;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
