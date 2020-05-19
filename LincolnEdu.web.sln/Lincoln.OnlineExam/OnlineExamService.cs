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
        public List<FacultyDashboardResponseDTO> GetAllFacultyCourse(FacultyDashboardRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllFacultyCourse(request);
            }
        }
        public LogInResponseDTO ValidateUser(LogInRequestDTO request, string Operation)
        {
            var result = new LogInResponseDTO();
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                result = unitOfWork.UserRepository.ValidateUser(request, Operation);
            }
            return result;
        }
        public int UpdateStatus(UpdateStatusReuestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.UpdateStatus(request);
            }
        }
        public List<DropdownResponseDTO> GetDropdownData(string type)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetDropdownData(type);
            }

        }


        public int SaveCourse(CourseRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveCourse(request, Operation);
            }
        }
        public List<CourseResponseDTO> GetAllCourse()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllCourse();
            }
        }
        public CourseResponseDTO SelectCourse(CourseRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectCourse(request);
            }
        }


        public int SaveStudent(StudentRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.SaveStudent(request, Operation);
            }
        }
        public List<StudentResponseDTO> GetAllStudent()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.GetAllStudent();
            }
        }
        public StudentResponseDTO SelectStudent(StudentRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.SelectStudent(request);
            }
        }



        public int SaveAcademicLevel(AcademicLevelRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveAcademicLevel(request, Operation);
            }
        }
        public List<AcademicLevelResponseDTO> GetAllAcademicLevel()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllAcademicLevel();
            }
        }
        public AcademicLevelResponseDTO SelectAcademicLevel(AcademicLevelRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectAcademicLevel(request);
            }
        }


        public int SaveDepartment(DepartmentRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveDepartment(request, Operation);
            }
        }
        public List<DepartmentResponseDTO> GetAllDepartment()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllDepartment();
            }
        }
        public DepartmentResponseDTO SelectDepartment(DepartmentRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectDepartment(request);
            }
        }


        public int SaveProgramme(ProgrammeRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveProgramme(request, Operation);
            }
        }
        public List<ProgrammeResponseDTO> GetAllProgramme()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllProgramme();
            }
        }
        public List<ProgrammeResponseDTO> GetAllProgrammeWithVersion()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllProgrammeWithVersion();
            }
        }
        public ProgrammeResponseDTO SelectProgramme(ProgrammeRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectProgramme(request);
            }
        }



        public int SaveProgramVersioning(ProgramVersioningRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveProgramVersioning(request, Operation);
            }
        }
        public List<ProgramVersioningResponseDTO> GetAllProgramVersioning()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllProgramVersioning();
            }
        }
        public ProgramVersioningResponseDTO SelectProgramVersioning(ProgramVersioningRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectProgramVersioning(request);
            }
        }



        public int SaveProgrammeSemester(ProgrammeSemesterRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveProgrammeSemester(request, Operation);
            }
        }





        public List<ProgrammeSemesterResponseDTO> GetAllProgrammeSemester()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllProgrammeSemester();
            }
        }
        public ProgrammeSemesterResponseDTO SelectProgrammeSemester(ProgrammeSemesterRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectProgrammeSemester(request);
            }
        }


        public int SaveExaminationName(ExaminationNameRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveExaminationName(request, Operation);
            }
        }
        public List<ExaminationNameResponseDTO> GetAllExaminationName()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllExaminationName();
            }
        }
        public ExaminationNameResponseDTO SelectExaminationName(ExaminationNameRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectExaminationName(request);
            }
        }


        public int SaveExaminationSection(ExaminationSectionRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveExaminationSection(request, Operation);
            }
        }
        public List<ExaminationSectionResponseDTO> GetAllExaminationSection()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllExaminationSection();
            }
        }
        public ExaminationSectionResponseDTO SelectExaminationSection(ExaminationSectionRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectExaminationSection(request);
            }
        }

        public int SaveAssessment(AssessmentRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveAssessment(request, Operation);
            }
        }
        public List<AssessmentResponseDTO> GetAllAssessment()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllAssessment();
            }
        }
        public AssessmentResponseDTO SelectAssessment(AssessmentRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectAssessment(request);
            }
        }
        public int SaveSubjectAssessment(SubjectAssessmentRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveSubjectAssessment(request, Operation);
            }
        }
        public List<SubjectAssessmentResponseDTO> GetAllSubjectAssessment()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllSubjectAssessment();
            }
        }
        public SubjectAssessmentResponseDTO SelectSubjectAssessment(SubjectAssessmentRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectSubjectAssessment(request);
            }
        }

        public int SaveEmployee(EmployeeRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.SaveEmployee(request, Operation);
            }
        }
        public List<EmployeeResponseDTO> GetAllEmployee()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.GetAllEmployee();
            }
        }
        public EmployeeResponseDTO SelectEmployee(EmployeeRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.UserRepository.SelectEmployee(request);
            }
        }

        public int SaveSubjectAllocation(SubjectAllocationRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveSubjectAllocation(request, Operation);
            }
        }
        public List<SubjectAllocationResponseDTO> GetAllSubjectAllocation()
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllSubjectAllocation();
            }
        }
        public SubjectAllocationResponseDTO SelectSubjectAllocation(SubjectAllocationRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SelectSubjectAllocation(request);
            }
        }

        public int SavePaper(PaperRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.QuestionRepository.SavePaper(request, Operation);
            }
        }
        public PaperResponseDTO SelectPaper(PaperRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.QuestionRepository.SelectPaper(request);
            }
        }
        public int SavePaperDetails(PaperDetailsRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.QuestionRepository.SavePaperDetails(request, Operation);
            }
        }
        public List<PaperDetailsResponseDTO> GetAllPaperDetails(PaperDetailsRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.QuestionRepository.GetAllPaperDetails(request);
            }
        }
        public PaperDetailsResponseDTO SelectAllPaperDetails(PaperDetailsRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.QuestionRepository.SelectAllPaperDetails(request);
            }
        }
        public List<OnlineTestResponseDTO> GetStudentExamination(OnlineTestRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.StudentRepository.GetStudentExamination(request);
            }
        }
        public List<ExamQuestionSectionResponseDTO> GetExamQuestionSection(ExamQuestionSectionRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.StudentRepository.GetExamQuestionSection(request);
            }
        }
        public List<PaperDetailsResponseDTO> GetQuestionPaper(ExamQuestionSectionRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.StudentRepository.GetQuestionPaper(request);
            }
        }
        public int SaveExaminationTest(ExaminationTestRequestDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.StudentRepository.SaveExaminationTest(recordAttributer,Operation);
            }
        }
        public List<StudentExaminationTestResponseDTO> GetExaminationTest(ExaminationTestRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.StudentRepository.GetExaminationTest(request);
            }

        }
        public int SaveExaminationSheet(StudentExaminationSheetResponseDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.StudentRepository.SaveExaminationSheet(recordAttributer, Operation);
            }
        }
        public List<LeftPanelFeedResponseDTO> GetAttemptQuestion(ExaminationTestRequestDTO request, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.StudentRepository.GetAttemptQuestion(request, Operation);
            }
        }




        public List<OnlineExamAppResponseDTO> GetAllOnlineExamApp(AdminOnlineExamRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllOnlineExamApp(request);
            }
        }
        public List<OnlineExamAppResponseDTO> GetAllOnlineExamEvaluation(AdminOnlineExamRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllOnlineExamEvaluation(request);
            }
        }
        public List<OnlineExamAppResponseDTO> GetAllOnlineExamSchedule(AdminOnlineExamRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllOnlineExamSchedule(request);
            }
        }
        public List<OnlineExamAppResponseDTO> GetAllOnlineExamResult(AdminOnlineExamRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAllOnlineExamResult(request);
            }
        }
        public List<AdminAnswerReviewResponseDTO> GetAnserReview(AdminOnlineExamRequestDTO request)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.GetAnserReview(request);
            }
        }
        public int SaveExaminationConfiguration(AdminOnlineExamRequestDTO recordAttributer, string Operation)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveExaminationConfiguration(recordAttributer, Operation);
            }
        }

        public int SaveResultApproval(AdminOnlineExamRequestDTO recordAttributer)
        {
            using (var unitOfWork = new OnlineExamUnitOfWork())
            {
                return unitOfWork.CommonRepository.SaveResultApproval(recordAttributer);
            }
        }

    }
}
