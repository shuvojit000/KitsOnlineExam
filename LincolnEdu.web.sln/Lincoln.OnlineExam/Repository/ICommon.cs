using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Response;

namespace Lincoln.OnlineExam.Repository
{
    public interface ICommon
    {
        int UpdateStatus(UpdateStatusReuestDTO recordAttributer);
        List<DropdownResponseDTO> GetDropdownData(string Type);
        int SaveSubject(SubjectRequestDTO recordAttributer, string Operation);
        List<SubjectResponseDTO> GetAllSubject();
        SubjectResponseDTO SelectSubject(SubjectRequestDTO recordAttributer);

        int SaveCourse(CourseRequestDTO recordAttributer, string Operation);
        List<CourseResponseDTO> GetAllCourse();
        CourseResponseDTO SelectCourse(CourseRequestDTO recordAttributer);

        int SaveBatche(BatchRequestDTO recordAttributer, string Operation);
        List<BatchResponseDTO> GetAllBatch();
        BatchResponseDTO SelectBatch(BatchRequestDTO recordAttributer);
        int SaveQuestionSection(QuestionSectionRequestDTO recordAttributer, string Operation);
        List<QuestionSectionResponseDTO> GetAllQuestionSection();
        QuestionSectionResponseDTO SelectQuestionSection(QuestionSectionRequestDTO recordAttributer);


        int SaveAcademicLevel(AcademicLevelRequestDTO recordAttributer, string Operation);
        List<AcademicLevelResponseDTO> GetAllAcademicLevel();
        AcademicLevelResponseDTO SelectAcademicLevel(AcademicLevelRequestDTO recordAttributer);

        int SaveDepartment(DepartmentRequestDTO recordAttributer, string Operation);
        List<DepartmentResponseDTO> GetAllDepartment();
        DepartmentResponseDTO SelectDepartment(DepartmentRequestDTO recordAttributer);

        int SaveProgramme(ProgrammeRequestDTO recordAttributer, string Operation);
        List<ProgrammeResponseDTO> GetAllProgramme();
        ProgrammeResponseDTO SelectProgramme(ProgrammeRequestDTO recordAttributer);
    }
}
