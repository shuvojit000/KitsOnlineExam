using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Utility;
using Lincoln.OnlineExam.Response;

namespace Lincoln.OnlineExam.Repository
{
    public class QuestionRepository : IQuestionRepository
    {

        public int SavePaper(PaperRequestDTO recordAttributer, string Operation)
        {
            SqlParameter paperID = new SqlParameter("@PaperID", SqlDbType.Int);
            paperID.Value = recordAttributer.PaperID;

            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = recordAttributer.CourseID;

            SqlParameter examinationSectionID = new SqlParameter("@ExaminationSectionID", SqlDbType.Int);
            examinationSectionID.Value = recordAttributer.ExaminationSectionID;

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

            SqlServerHelper.ExecuteNonQueryProc("[ln.Faculty].[upSavePaper]", paperID, loginID, courseID, examinationSectionID, sectionName,
                active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }


        public PaperResponseDTO SelectPaper(PaperRequestDTO recordAttributer)
        {
            var item = new PaperResponseDTO();


            SqlParameter paperID = new SqlParameter("@PaperID", SqlDbType.Int);
            paperID.Value = recordAttributer.PaperID;

            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = recordAttributer.CourseID;
            SqlParameter examinationSectionID = new SqlParameter("@ExaminationSectionID", SqlDbType.Int);
            examinationSectionID.Value = recordAttributer.ExaminationSectionID;

            SqlParameter sectionName = new SqlParameter("@SectionName", SqlDbType.VarChar);
            sectionName.Value = recordAttributer.SectionName;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";

            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetPaper]", paperID, loginID,
                courseID, examinationSectionID, sectionName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.LoginID = Convert.ToInt32(dr["LoginID"]);
                        item.CourseID = Convert.ToInt32(dr["CourseID"]);
                        item.PaperID = Convert.ToInt32(dr["PaperID"]);
                        item.QuestionNo = Convert.ToInt32(dr["QuestionNo"]);
                        item.ExaminationSectionID = Convert.ToInt32(dr["ExaminationSectionID"]);
                        item.SectionName = object.ReferenceEquals(dr["SectionName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SectionName"]);
                        item.SectionMarks = Convert.ToDecimal(dr["SectionMarks"]);
                        item.RemainingMarks = Convert.ToDecimal(dr["RemainingMarks"]);
                    }
                }
            }
            return item;

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

            SqlParameter sectionMarks = new SqlParameter("@SectionMarks", SqlDbType.Decimal);
            sectionMarks.Value = recordAttributer.SectionMarks;

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
                paperDetailsID, paperID, questionType, questionNo, questionText, textOrImageQuestion, audioOrVideoQuestion, questionMarks, sectionMarks, optionANo,
                optionAText, optionBNo, optionBText, optionCNo, optionCText, optionDNo, optionDText, optionENo, optionEText, answerNo, answerText,
                active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }


        public List<PaperDetailsResponseDTO> GetAllPaperDetails(PaperDetailsRequestDTO recordAttributer)
        {
            var itemSet = new List<PaperDetailsResponseDTO>();


            SqlParameter paperDetailsID = new SqlParameter("@PaperDetailsID", SqlDbType.Int);
            paperDetailsID.Value = recordAttributer.PaperDetailsID;

            SqlParameter paperID = new SqlParameter("@PaperID", SqlDbType.Int);
            paperID.Value = recordAttributer.PaperID;

            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetPaperDetails]", paperDetailsID, paperID, loginID, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new PaperDetailsResponseDTO()
                        {

                            PaperDetailsID = Convert.ToInt32(dr["PaperDetailsID"]),
                            ExaminationSectionID = Convert.ToInt32(dr["ExaminationSectionID"]),                           
                            AnswerNo = Convert.ToInt32(dr["AnswerNo"]),
                            AnswerText = object.ReferenceEquals(dr["AnswerText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AnswerText"]),
                            SectionMarks = Convert.ToDecimal(dr["SectionMarks"]),
                            AudioOrVideoQuestion = object.ReferenceEquals(dr["AudioOrVideoQuestion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AudioOrVideoQuestion"]),
                            OptionANo = Convert.ToInt32(dr["OptionANo"]),
                            OptionAText = object.ReferenceEquals(dr["OptionAText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionAText"]),
                            OptionBNo = Convert.ToInt32(dr["OptionBNo"]),
                            OptionBText = object.ReferenceEquals(dr["OptionBText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionBText"]),
                            OptionCNo = Convert.ToInt32(dr["OptionCNo"]),
                            OptionCText = object.ReferenceEquals(dr["OptionCText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionCText"]),
                            OptionDNo = Convert.ToInt32(dr["OptionDNo"]),
                            OptionDText = object.ReferenceEquals(dr["OptionDText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionDText"]),
                            OptionENo = Convert.ToInt32(dr["OptionENo"]),
                            OptionEText = object.ReferenceEquals(dr["OptionEText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionEText"]),
                            PaperID = Convert.ToInt32(dr["PaperID"]),
                            QuestionMarks = Convert.ToDecimal(dr["QuestionMarks"]),
                            RemainingMarks = Convert.ToDecimal(dr["RemainingMarks"]),
                            QuestionNo = Convert.ToInt32(dr["QuestionNo"]),
                            QuestionText = object.ReferenceEquals(dr["QuestionText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionText"]),
                            QuestionType = object.ReferenceEquals(dr["QuestionType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionType"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            TextOrImageQuestion = object.ReferenceEquals(dr["TextOrImageQuestion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["TextOrImageQuestion"]),



                        });

                    }
                }
            }
            return itemSet;

        }

        public PaperDetailsResponseDTO SelectAllPaperDetails(PaperDetailsRequestDTO recordAttributer)
        {
            var itemSet = new PaperDetailsResponseDTO();


            SqlParameter paperDetailsID = new SqlParameter("@PaperDetailsID", SqlDbType.Int);
            paperDetailsID.Value = recordAttributer.PaperDetailsID;

            SqlParameter paperID = new SqlParameter("@PaperID", SqlDbType.Int);
            paperID.Value = recordAttributer.PaperID;

            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetPaperDetails]", paperDetailsID, paperID,loginID, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        itemSet.PaperDetailsID = Convert.ToInt32(dr["PaperDetailsID"]);
                        itemSet.ExaminationSectionID = Convert.ToInt32(dr["ExaminationSectionID"]);
                        itemSet.AnswerNo = Convert.ToInt32(dr["AnswerNo"]);
                        itemSet.AnswerText = object.ReferenceEquals(dr["AnswerText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AnswerText"]);
                        itemSet.SectionMarks = Convert.ToDecimal(dr["SectionMarks"]);
                        itemSet.AudioOrVideoQuestion = object.ReferenceEquals(dr["AudioOrVideoQuestion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AudioOrVideoQuestion"]);
                        itemSet.OptionANo = Convert.ToInt32(dr["OptionANo"]);
                        itemSet.OptionAText = object.ReferenceEquals(dr["OptionAText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionAText"]);
                        itemSet.OptionBNo = Convert.ToInt32(dr["OptionBNo"]);
                        itemSet.OptionBText = object.ReferenceEquals(dr["OptionBText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionBText"]);
                        itemSet.OptionCNo = Convert.ToInt32(dr["OptionCNo"]);
                        itemSet.OptionCText = object.ReferenceEquals(dr["OptionCText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionCText"]);
                        itemSet.OptionDNo = Convert.ToInt32(dr["OptionDNo"]);
                        itemSet.OptionDText = object.ReferenceEquals(dr["OptionDText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionDText"]);
                        itemSet.OptionENo = Convert.ToInt32(dr["OptionENo"]);
                        itemSet.OptionEText = object.ReferenceEquals(dr["OptionEText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["OptionEText"]);
                        itemSet.PaperID = Convert.ToInt32(dr["PaperID"]);
                        itemSet.QuestionMarks = Convert.ToDecimal(dr["QuestionMarks"]);
                        itemSet.RemainingMarks = Convert.ToDecimal(dr["RemainingMarks"]);
                        itemSet.QuestionNo = Convert.ToInt32(dr["QuestionNo"]);
                        itemSet.QuestionText = object.ReferenceEquals(dr["QuestionText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionText"]);
                        itemSet.QuestionType = object.ReferenceEquals(dr["QuestionType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionType"]);
                        itemSet.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        itemSet.TextOrImageQuestion = object.ReferenceEquals(dr["TextOrImageQuestion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["TextOrImageQuestion"]);





                    }
                }
            }
            return itemSet;

        }

    }
}
