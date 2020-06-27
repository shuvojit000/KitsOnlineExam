using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Response;

namespace Lincoln.OnlineExam.Repository
{
   public interface IStudent
    {       
        int SaveStudent(StudentRequestDTO recordAttributer, string Operation);
        List<StudentResponseDTO> GetAllStudent();
        StudentResponseDTO SelectStudent(StudentRequestDTO recordAttributer);

        List<OnlineTestResponseDTO> GetStudentExamination(OnlineTestRequestDTO request);
        List<ExamQuestionSectionResponseDTO> GetExamQuestionSection(ExamQuestionSectionRequestDTO request);
        List<PaperDetailsResponseDTO> GetQuestionPaper(ExamQuestionSectionRequestDTO request);

        int SaveExaminationTest(ExaminationTestRequestDTO recordAttributer, string Operation);
        List<StudentExaminationTestResponseDTO> GetExaminationTest(ExaminationTestRequestDTO request);
        int SaveExaminationSheet(StudentExaminationSheetResponseDTO recordAttributer, string Operation);
        List<LeftPanelFeedResponseDTO> GetAttemptQuestion(ExaminationTestRequestDTO request, string Operation);
        int SaveTimerTime(ExaminationTestRequestDTO recordAttributer);

    }
}
