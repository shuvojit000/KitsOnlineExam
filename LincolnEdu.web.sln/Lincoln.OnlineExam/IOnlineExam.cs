using System.Collections.Generic;
using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Response;

namespace Lincoln.OnlineExam
{
    public interface IOnlineExam
    {
        List<DropdownResponseDTO> GetDropdownData(string Type);
        List<FacultyDashboardResponseDTO> GetAllFacultyCourse(FacultyDashboardRequestDTO recordAttributer);
        LogInResponseDTO ValidateUser(LogInRequestDTO request, string Operation);
        int UpdateStatus(UpdateStatusReuestDTO recordAttributer);

        int SaveStudent(StudentRequestDTO recordAttributer, string Operation);
        List<StudentResponseDTO> GetAllStudent();
        StudentResponseDTO SelectStudent(StudentRequestDTO recordAttributer);

       

        int SaveCourse(CourseRequestDTO recordAttributer, string Operation);

        List<CourseResponseDTO> GetAllCourse();

        CourseResponseDTO SelectCourse(CourseRequestDTO recordAttributer);

       
     


        int SaveAcademicLevel(AcademicLevelRequestDTO recordAttributer, string Operation);
        List<AcademicLevelResponseDTO> GetAllAcademicLevel();
        AcademicLevelResponseDTO SelectAcademicLevel(AcademicLevelRequestDTO recordAttributer);

        int SaveDepartment(DepartmentRequestDTO recordAttributer, string Operation);
        List<DepartmentResponseDTO> GetAllDepartment();
        DepartmentResponseDTO SelectDepartment(DepartmentRequestDTO recordAttributer);

        int SaveProgramme(ProgrammeRequestDTO recordAttributer, string Operation);
        List<ProgrammeResponseDTO> GetAllProgramme();
        List<ProgrammeResponseDTO> GetAllProgrammeWithVersion();
        ProgrammeResponseDTO SelectProgramme(ProgrammeRequestDTO recordAttributer);

        int SaveProgramVersioning(ProgramVersioningRequestDTO recordAttributer, string Operation);
        List<ProgramVersioningResponseDTO> GetAllProgramVersioning();
        ProgramVersioningResponseDTO SelectProgramVersioning(ProgramVersioningRequestDTO recordAttributer);

        int SaveProgrammeSemester(ProgrammeSemesterRequestDTO recordAttributer, string Operation);
        List<ProgrammeSemesterResponseDTO> GetAllProgrammeSemester();
        ProgrammeSemesterResponseDTO SelectProgrammeSemester(ProgrammeSemesterRequestDTO recordAttributer);

        int SaveSubjectAssessment(SubjectAssessmentRequestDTO recordAttributer, string Operation);
        List<SubjectAssessmentResponseDTO> GetAllSubjectAssessment();
        SubjectAssessmentResponseDTO SelectSubjectAssessment(SubjectAssessmentRequestDTO recordAttributer);

        int SaveAssessment(AssessmentRequestDTO recordAttributer, string Operation);
        List<AssessmentResponseDTO> GetAllAssessment();
        AssessmentResponseDTO SelectAssessment(AssessmentRequestDTO recordAttributer);


        int SaveExaminationName(ExaminationNameRequestDTO recordAttributer, string Operation);
        List<ExaminationNameResponseDTO> GetAllExaminationName();
        ExaminationNameResponseDTO SelectExaminationName(ExaminationNameRequestDTO recordAttributer);

        int SaveExaminationSection(ExaminationSectionRequestDTO recordAttributer, string Operation);
        List<ExaminationSectionResponseDTO> GetAllExaminationSection();
        ExaminationSectionResponseDTO SelectExaminationSection(ExaminationSectionRequestDTO recordAttributer);

        int SaveEmployee(EmployeeRequestDTO recordAttributer, string Operation);
        List<EmployeeResponseDTO> GetAllEmployee();
        EmployeeResponseDTO SelectEmployee(EmployeeRequestDTO recordAttributer);
        
         int SaveSubjectAllocation(SubjectAllocationRequestDTO recordAttributer, string Operation);
        List<SubjectAllocationResponseDTO> GetAllSubjectAllocation();
        SubjectAllocationResponseDTO SelectSubjectAllocation(SubjectAllocationRequestDTO recordAttributer);

        int SavePaper(PaperRequestDTO recordAttributer, string Operation);
        PaperResponseDTO SelectPaper(PaperRequestDTO recordAttributer);
        int SavePaperDetails(PaperDetailsRequestDTO recordAttributer, string Operation);
        List<PaperDetailsResponseDTO> GetAllPaperDetails(PaperDetailsRequestDTO recordAttributer);
        PaperDetailsResponseDTO SelectAllPaperDetails(PaperDetailsRequestDTO recordAttributer);
        List<OnlineTestResponseDTO> GetStudentExamination(OnlineTestRequestDTO request);

        List<ExamQuestionSectionResponseDTO> GetExamQuestionSection(ExamQuestionSectionRequestDTO request);
        List<PaperDetailsResponseDTO> GetQuestionPaper(ExamQuestionSectionRequestDTO request);
        int SaveExaminationTest(ExaminationTestRequestDTO recordAttributer, string Operation);

        List<StudentExaminationTestResponseDTO> GetExaminationTest(ExaminationTestRequestDTO request);
        int SaveExaminationSheet(StudentExaminationSheetResponseDTO recordAttributer, string Operation);
        List<LeftPanelFeedResponseDTO> GetAttemptQuestion(ExaminationTestRequestDTO request, string Operation);
    }
}
