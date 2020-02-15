using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Utility;

namespace Lincoln.OnlineExam.Repository
{
    public class QuestionRepository : IQuestionRepository
    {

        public int SavePaper(PaperRequestDTO recordAttributer, string Operation)
        {
            SqlParameter paperID = new SqlParameter("@PaperID", SqlDbType.Int);
            paperID.Value = recordAttributer.PaperID;

            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = recordAttributer.ProgrammeID;

            SqlParameter programmeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programmeVersioningID.Value = recordAttributer.ProgrammeVersioningID;

            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = recordAttributer.CountryID;

            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = recordAttributer.ProgrammeYear;

            SqlParameter programmeSemesterid = new SqlParameter("@ProgrammeSemester", SqlDbType.Int);
            programmeSemesterid.Value = recordAttributer.ProgrammeSemester;

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = recordAttributer.CourseID;

            SqlParameter sectionName = new SqlParameter("@SectionName", SqlDbType.VarChar);
            sectionName.Value = recordAttributer.SectionName;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Faculty].[upSavePaper]", paperID, programmeID, programmeVersioningID, countryID, programmeYear,
                programmeSemesterid, courseID, sectionName,
                active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }



        public int SavePaperDetails(PaperDetailsRequestDTO recordAttributer, string Operation)
        {
            SqlParameter paperDetailsID = new SqlParameter("@PaperDetailsID", SqlDbType.Int);
            paperDetailsID.Value = recordAttributer.PaperDetailsID;

            SqlParameter paperID = new SqlParameter("@PaperID", SqlDbType.Int);
            paperID.Value = recordAttributer.PaperID;

            SqlParameter questionType = new SqlParameter("@QuestionType", SqlDbType.VarChar);
            questionType.Value = recordAttributer.QuestionType;

            SqlParameter questionNo = new SqlParameter("@QuestionNo", SqlDbType.Int);
            questionNo.Value = recordAttributer.QuestionNo;

            SqlParameter questionText = new SqlParameter("@QuestionText", SqlDbType.VarChar);
            questionText.Value = recordAttributer.QuestionText;

            SqlParameter textOrImageQuestion = new SqlParameter("@TextOrImageQuestion", SqlDbType.VarChar);
            textOrImageQuestion.Value = recordAttributer.TextOrImageQuestion;

            SqlParameter audioOrVideoQuestion = new SqlParameter("@AudioOrVideoQuestion", SqlDbType.VarChar);
            audioOrVideoQuestion.Value = recordAttributer.AudioOrVideoQuestion;

            SqlParameter questionMarks = new SqlParameter("@QuestionMarks", SqlDbType.Decimal);
            questionMarks.Value = recordAttributer.QuestionMarks;

            SqlParameter optionANo = new SqlParameter("@OptionANo", SqlDbType.Int);
            optionANo.Value = recordAttributer.OptionANo;

            SqlParameter optionAText = new SqlParameter("@OptionAText", SqlDbType.VarChar);
            optionAText.Value = recordAttributer.OptionAText;

            SqlParameter optionBNo = new SqlParameter("@OptionBNo", SqlDbType.Int);
            optionBNo.Value = recordAttributer.OptionBNo;

            SqlParameter optionBText = new SqlParameter("@OptionBText", SqlDbType.VarChar);
            optionBText.Value = recordAttributer.OptionBText;

            SqlParameter optionCNo = new SqlParameter("@OptionCNo", SqlDbType.Int);
            optionCNo.Value = recordAttributer.OptionCNo;

            SqlParameter optionCText = new SqlParameter("@OptionCText", SqlDbType.VarChar);
            optionCText.Value = recordAttributer.OptionCText;

            SqlParameter optionDNo = new SqlParameter("@OptionDNo", SqlDbType.Int);
            optionDNo.Value = recordAttributer.OptionDNo;

            SqlParameter optionDText = new SqlParameter("@OptionDText", SqlDbType.VarChar);
            optionDText.Value = recordAttributer.OptionDText;

            SqlParameter optionENo = new SqlParameter("@OptionENo", SqlDbType.Int);
            optionENo.Value = recordAttributer.OptionENo;

            SqlParameter optionEText = new SqlParameter("@OptionEText", SqlDbType.VarChar);
            optionEText.Value = recordAttributer.OptionEText;

            SqlParameter answerNo = new SqlParameter("@AnswerNo", SqlDbType.Int);
            answerNo.Value = recordAttributer.AnswerNo;

            SqlParameter answerText = new SqlParameter("@AnswerText", SqlDbType.VarChar);
            answerText.Value = recordAttributer.AnswerText;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;

            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Faculty].[upSavePaperDetails]",
                paperDetailsID,paperID,questionType,questionNo,questionText,textOrImageQuestion,audioOrVideoQuestion,questionMarks,optionANo,
                optionAText,optionBNo,optionBText,optionCNo,optionCText,optionDNo,optionDText,optionENo,optionEText,answerNo,answerText,
                active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }

    }
}
