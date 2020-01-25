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
        public int UpdateStatus(UpdateStatusReuestDTO recordAttributer)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.UpdateStatus(recordAttributer);
            }
        }
        public List<DropdownResponseDTO> GetDropdownData(string type)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetDropdownData(type);
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

       
        public int SaveStudent(StudentRequestDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.SaveStudent(recordAttributer, Operation);
            }
        }
        public List<StudentResponseDTO> GetAllStudent()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.GetAllStudent();
            }
        }
        public StudentResponseDTO SelectStudent(StudentRequestDTO recordAttributer)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.SelectStudent(recordAttributer);
            }
        }
      


        public int SaveAcademicLevel(AcademicLevelRequestDTO recordAttributer, string Operation) {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveAcademicLevel(recordAttributer, Operation);
            }
        }
        public List<AcademicLevelResponseDTO> GetAllAcademicLevel() {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllAcademicLevel();
            }
        }
        public AcademicLevelResponseDTO SelectAcademicLevel(AcademicLevelRequestDTO recordAttributer) {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectAcademicLevel(recordAttributer);
            }
        }


        public int SaveDepartment(DepartmentRequestDTO recordAttributer, string Operation) {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveDepartment(recordAttributer, Operation);
            }
        }
        public List<DepartmentResponseDTO> GetAllDepartment() {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllDepartment();
            }
        }
        public DepartmentResponseDTO SelectDepartment(DepartmentRequestDTO recordAttributer) {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectDepartment(recordAttributer);
            }
        }


        public int SaveProgramme(ProgrammeRequestDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveProgramme(recordAttributer, Operation);
            }
        }
        public List<ProgrammeResponseDTO> GetAllProgramme()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllProgramme();
            }
        }
        public ProgrammeResponseDTO SelectProgramme(ProgrammeRequestDTO recordAttributer)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectProgramme(recordAttributer);
            }
        }



        public int SaveProgramVersioning(ProgramVersioningRequestDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveProgramVersioning(recordAttributer, Operation);
            }
        }
        public List<ProgramVersioningResponseDTO> GetAllProgramVersioning()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllProgramVersioning();
            }
        }
        public ProgramVersioningResponseDTO SelectProgramVersioning(ProgramVersioningRequestDTO recordAttributer)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectProgramVersioning(recordAttributer);
            }
        }



        public int SaveProgrammeSemester(ProgrammeSemesterRequestDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveProgrammeSemester(recordAttributer, Operation);
            }
        }





       public  List<ProgrammeSemesterResponseDTO> GetAllProgrammeSemester()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllProgrammeSemester();
            }
        }
        public  ProgrammeSemesterResponseDTO SelectProgrammeSemester(ProgrammeSemesterRequestDTO recordAttributer)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectProgrammeSemester(recordAttributer);
            }
        }
    }
}
