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
        private ICommon commonRepository;
        private IQuestionRepository questionRepository;
        private IStudent studentRepository;
        public IStudent StudentRepository
        {
            get
            {
                if (this.studentRepository == null)
                {

                    this.studentRepository = new StudentRepository();
                }

                return this.studentRepository;
            }
        }
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
        public ICommon CommonRepository
        {
            get
            {
                if (this.commonRepository == null)
                {

                    this.commonRepository = new CommonRepository();
                }

                return this.commonRepository;
            }
        }
        public IQuestionRepository QuestionRepository
        {
            get
            {
                if (this.questionRepository == null)
                {

                    this.questionRepository = new QuestionRepository();
                }

                return this.questionRepository;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
