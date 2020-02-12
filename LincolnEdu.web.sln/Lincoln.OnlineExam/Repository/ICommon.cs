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
        
        
        int SaveSubjectAllocation(SubjectAllocationRequestDTO recordAttributer, string Operation);
        List<SubjectAllocationResponseDTO> GetAllSubjectAllocation();
        SubjectAllocationResponseDTO SelectSubjectAllocation(SubjectAllocationRequestDTO recordAttributer);
    }
}
