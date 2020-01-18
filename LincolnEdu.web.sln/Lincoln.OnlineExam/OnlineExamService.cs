using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam
{
    public class OnlineExamService : IOnlineExam
    {

        public LogInResponseDTO ValidateUser(LogInRequestDTO request, string Operation)
        {
            var result = new LogInResponseDTO();
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                result = unitOfWork.UserRepository.ValidateUser(request, Operation);
            }
            return result;
        }
        public int SaveStudent(StudentRequestDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.SaveStudent(recordAttributer, Operation);
            }

        }
        public List<DropdownResponseDTO> GetDropdownData(string Type)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetDropdownData(Type);
            }

        }
        public int SaveSubject(SubjectRequestDTO recordAttributer, string Operation)
        {

            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveSubject(recordAttributer, Operation);
            }
        }
        public List<SubjectResponseDTO> GetAllSubject()
        {

            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllSubject();
            }
        }
        public SubjectResponseDTO SelectSubject(SubjectRequestDTO recordAttributer)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectSubject(recordAttributer);
            }
        }

        public int SaveCourse(CourseRequestDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveCourse(recordAttributer, Operation);
            }
        }
        public List<CourseResponseDTO> GetAllCourse()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllCourse();
            }
        }
        public CourseResponseDTO SelectCourse(CourseRequestDTO recordAttributer)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectCourse(recordAttributer);
            }
        }

        public int SaveBatche(BatchRequestDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveBatche(recordAttributer, Operation);
            }
        }
        public List<BatchResponseDTO> GetAllBatch()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllBatch();
            }
        }
        public BatchResponseDTO SelectBatch(BatchRequestDTO recordAttributer)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectBatch(recordAttributer);
            }
        }


    }
}
