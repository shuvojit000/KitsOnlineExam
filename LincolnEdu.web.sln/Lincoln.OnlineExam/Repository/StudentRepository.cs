using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Response;
using Lincoln.OnlineExam.Utility;
using System.Globalization;

namespace Lincoln.OnlineExam.Repository
{
    public class StudentRepository : IStudent
    {
        #region Student

        public int SaveStudent(StudentRequestDTO recordAttributer, string Operation)
        {

            SqlParameter studentID = new SqlParameter("@StudentID", SqlDbType.Int);
            studentID.Value = recordAttributer.StudentID;

            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;
            SqlParameter batchID = new SqlParameter("@BatchID", SqlDbType.Int);
            batchID.Value = recordAttributer.BatchID;
            SqlParameter userName = new SqlParameter("@UserName", SqlDbType.VarChar);
            userName.Value = recordAttributer.UserName;

            SqlParameter studentName = new SqlParameter("@StudentName", SqlDbType.VarChar);
            studentName.Value = recordAttributer.StudentName;
            SqlParameter password = new SqlParameter("@Password", SqlDbType.VarChar);
            password.Value = recordAttributer.Password;
            SqlParameter rollNo = new SqlParameter("@RollNo", SqlDbType.VarChar);
            rollNo.Value = recordAttributer.RollNo;
            SqlParameter mobileNo = new SqlParameter("@MobileNo", SqlDbType.VarChar);
            mobileNo.Value = recordAttributer.MobileNo;
            SqlParameter emailID = new SqlParameter("@EmailID", SqlDbType.VarChar);
            emailID.Value = recordAttributer.EmailID;
            SqlParameter userType = new SqlParameter("@UserType", SqlDbType.VarChar);
            userType.Value = recordAttributer.UserType;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;

            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = recordAttributer.Status;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Student].[upSaveStudent]", studentID, loginID, batchID, userName, studentName, rollNo, mobileNo,
                                       emailID, password, userType, active, createdBy, type, status);

            recordAttributer.Status = Convert.ToInt32(status.Value);
            return recordAttributer.Status;
        }

        public List<StudentResponseDTO> GetAllStudent()
        {
            var itemSet = new List<StudentResponseDTO>();


            SqlParameter studentID = new SqlParameter("@StudentID", SqlDbType.Int);
            studentID.Value = DBNull.Value;
            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = DBNull.Value;

            SqlParameter emailID = new SqlParameter("@EmailID", SqlDbType.VarChar);
            emailID.Value = DBNull.Value;
            SqlParameter mobileNo = new SqlParameter("@MobileNo", SqlDbType.VarChar);
            mobileNo.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Student].[upGetStudent]", studentID, loginID, emailID, mobileNo, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new StudentResponseDTO()
                        {
                            StudentID = Convert.ToInt32(dr["StudentID"]),
                            BatchID = Convert.ToInt32(dr["BatchID"]),
                            LoginID = Convert.ToInt32(dr["LoginID"]),
                            StudentName = object.ReferenceEquals(dr["StudentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["StudentName"]),
                            RollNo = object.ReferenceEquals(dr["RollNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["RollNo"]),
                            MobileNo = object.ReferenceEquals(dr["MobileNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["MobileNo"]),
                            EmailID = object.ReferenceEquals(dr["EmailID"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmailID"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            UserName = object.ReferenceEquals(dr["UserName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["UserName"]),
                            UserType = object.ReferenceEquals(dr["UserType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["UserType"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                            //CreatedOn = object.ReferenceEquals(dr["CreatedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["CreatedOn"]),
                            // ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]),
                            // ModifiedOn = object.ReferenceEquals(dr["ModifiedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["ModifiedOn"])

                        });

                    }
                }
            }
            return itemSet;

        }

        public StudentResponseDTO SelectStudent(StudentRequestDTO recordAttributer)
        {
            var item = new StudentResponseDTO();


            SqlParameter studentID = new SqlParameter("@StudentID", SqlDbType.Int);
            studentID.Value = recordAttributer.StudentID;
            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = DBNull.Value;

            SqlParameter emailID = new SqlParameter("@EmailID", SqlDbType.VarChar);
            emailID.Value = DBNull.Value;
            SqlParameter mobileNo = new SqlParameter("@MobileNo", SqlDbType.VarChar);
            mobileNo.Value = DBNull.Value;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Student].[upGetStudent]", studentID, loginID, emailID, mobileNo, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.StudentID = Convert.ToInt32(dr["StudentID"]);
                        item.LoginID = Convert.ToInt32(dr["LoginID"]);
                        item.BatchID = Convert.ToInt32(dr["BatchID"]);
                        item.EmailID = object.ReferenceEquals(dr["EmailID"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmailID"]);
                        item.RollNo = object.ReferenceEquals(dr["RollNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["RollNo"]);
                        item.Password = object.ReferenceEquals(dr["Password"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Password"]);
                        item.MobileNo = object.ReferenceEquals(dr["MobileNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["MobileNo"]);
                        item.StudentName = object.ReferenceEquals(dr["StudentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["StudentName"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        item.UserName = object.ReferenceEquals(dr["UserName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["UserName"]);
                        item.UserType = object.ReferenceEquals(dr["UserType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["UserType"]);
                        item.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                        //item.CreatedOn = object.ReferenceEquals(dr["CreatedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["CreatedOn"]);
                        //item.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
                        // item.ModifiedOn = object.ReferenceEquals(dr["ModifiedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["ModifiedOn"]);

                    }
                }
            }
            return item;

        }


        #endregion

        #region Examination Test

        public List<OnlineTestResponseDTO> GetStudentExamination(OnlineTestRequestDTO request)
        {

            var itemSet = new List<OnlineTestResponseDTO>();

            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = request.LoginID;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";

            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Student].[upGetOnlineTest]", loginID, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new OnlineTestResponseDTO()
                        {
                            LoginID = Convert.ToInt32(dr["LoginID"]),
                            CourseID = Convert.ToInt32(dr["CourseID"]),
                            StudentName = object.ReferenceEquals(dr["StudentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["StudentName"]),
                            CourseCode= object.ReferenceEquals(dr["CourseCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseCode"]),
                            CourseName= object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]),
                            EndDate= Convert.ToDateTime(dr["EndDate"]),
                            ExamAttendance= object.ReferenceEquals(dr["ExamAttendance"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ExamAttendance"]),
                            ExaminationName= object.ReferenceEquals(dr["ExaminationName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ExaminationName"]),
                            SLNo= Convert.ToInt32(dr["SLNo"]),
                            StartDate= Convert.ToDateTime(dr["StartDate"]),
                            Status= object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                        });
                    }
                }

            }
            return itemSet;
        }
        public List<ExamQuestionSectionResponseDTO> GetExamQuestionSection(ExamQuestionSectionRequestDTO request)
        {

            var itemSet = new List<ExamQuestionSectionResponseDTO>();

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = request.CourseID;
            SqlParameter questionNo = new SqlParameter("@QuestionNo", SqlDbType.Int);
            questionNo.Value = request.QuestionNo;
            SqlParameter sectionName = new SqlParameter("@SectionName", SqlDbType.VarChar);
            sectionName.Value = request.SectionName;

            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Student].[upGetQuestionSection]", courseID, questionNo, sectionName))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new ExamQuestionSectionResponseDTO()
                        {
                            MaxQuestionNo = Convert.ToInt32(dr["MaxQuestionNo"]),
                            MinQuestionNo = Convert.ToInt32(dr["MinQuestionNo"]),
                            ExaminationSectionID = Convert.ToInt32(dr["ExaminationSectionID"]),
                            SectionName = object.ReferenceEquals(dr["SectionName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SectionName"]),
                        });
                    }
                }

            }
            return itemSet;
        }


        public List<PaperDetailsResponseDTO> GetQuestionPaper(ExamQuestionSectionRequestDTO request)
        {
            var itemSet = new List<PaperDetailsResponseDTO>();

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = request.CourseID;
            SqlParameter questionNo = new SqlParameter("@QuestionNo", SqlDbType.Int);
            questionNo.Value = request.QuestionNo;
            SqlParameter sectionName = new SqlParameter("@SectionName", SqlDbType.VarChar);
            sectionName.Value = request.SectionName;
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Student].[upGetQuestionPaper]", courseID, questionNo, sectionName))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new PaperDetailsResponseDTO()
                        {
                            PaperID= Convert.ToInt32(dr["PaperID"]),
                            CourseID= Convert.ToInt32(dr["CourseID"]),
                            ExaminationSectionID = Convert.ToInt32(dr["ExaminationSectionID"]),
                            PaperDetailsID = Convert.ToInt32(dr["PaperDetailsID"]),
                            QuestionType = object.ReferenceEquals(dr["QuestionType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionType"]),
                            QuestionNo = Convert.ToInt32(dr["QuestionNo"]),
                            QuestionText = object.ReferenceEquals(dr["QuestionText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionText"]),
                            TextOrImageQuestion = object.ReferenceEquals(dr["TextOrImageQuestion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["TextOrImageQuestion"]),
                            AudioOrVideoQuestion = object.ReferenceEquals(dr["AudioOrVideoQuestion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AudioOrVideoQuestion"]),
                            QuestionMarks = Convert.ToDecimal(dr["QuestionMarks"]),
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
                            AnswerNo = Convert.ToInt32(dr["AnswerNo"]),
                            AnswerText = object.ReferenceEquals(dr["AnswerText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AnswerText"]),
                            SectionMarks = Convert.ToDecimal(dr["SectionMarks"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"])
                        });
                    }
                }

            }
            return itemSet;
        }

        public List<StudentExaminationTestResponseDTO> GetExaminationTest(ExaminationTestRequestDTO request)
        {
            var itemSet = new List<StudentExaminationTestResponseDTO>();

            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = request.LoginID;
            SqlParameter isAnswer = new SqlParameter("@IsAnswer", SqlDbType.Int);
            isAnswer.Value = request.IsAnswer;
            SqlParameter questionNo = new SqlParameter("@QuestionNo", SqlDbType.Int);
            questionNo.Value = request.QuestionNo;
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Student].[upGetExaminationTest]", loginID, isAnswer,questionNo))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new StudentExaminationTestResponseDTO()
                        {
                            ExaminationTestID= Convert.ToInt32(dr["ExaminationTestID"]),
                            AnswerNo= Convert.ToInt32(dr["AnswerNo"]),
                            AnswerText= object.ReferenceEquals(dr["AnswerText"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AnswerText"]),
                            QuestionNo= Convert.ToInt32(dr["QuestionNo"]),
                            StudentID= Convert.ToInt32(dr["StudentID"])
                        });
                    }
                }

            }
            return itemSet;
        }


        public int SaveExaminationTest(ExaminationTestRequestDTO recordAttributer, string Operation)
        {
            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;

            SqlParameter questionNo = new SqlParameter("@QuestionNo", SqlDbType.Int);
            questionNo.Value = recordAttributer.QuestionNo;
            SqlParameter answerNo = new SqlParameter("@AnswerNo", SqlDbType.Int);
            answerNo.Value = recordAttributer.AnswerNo;
            SqlParameter answerText = new SqlParameter("@AnswerText", SqlDbType.VarChar);
            answerText.Value = recordAttributer.AnswerText;
            SqlParameter isAnswer = new SqlParameter("@IsAnswer", SqlDbType.VarChar);
            isAnswer.Value = recordAttributer.IsAnswer;
            
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;

            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = recordAttributer.Status;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Student].[upSaveExaminationTest]", loginID, questionNo,answerNo,answerText,isAnswer, type, status);

            recordAttributer.Status = Convert.ToInt32(status.Value);
            return recordAttributer.Status;

        }

        public int SaveExaminationSheet(StudentExaminationSheetResponseDTO recordAttributer, string Operation)
        {
            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;

            SqlParameter paperID = new SqlParameter("@PaperID", SqlDbType.Int);
            paperID.Value = recordAttributer.PaperID;

            SqlParameter paperDetailsID = new SqlParameter("@PaperDetailsID", SqlDbType.Int);
            paperDetailsID.Value = recordAttributer.PaperDetailsID;

            SqlParameter totalTime = new SqlParameter("@TotalTime", SqlDbType.Int);
            totalTime.Value = recordAttributer.TotalTime;

            SqlParameter examinationDuration = new SqlParameter("@ExaminationDuration", SqlDbType.VarChar);
            examinationDuration.Value = recordAttributer.ExaminationDuration;

            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;

            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = recordAttributer.Status;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Student].[upSaveExaminationSheet]", loginID, paperID, paperDetailsID, totalTime, examinationDuration, createdBy,type, status);

            recordAttributer.Status = Convert.ToInt32(status.Value);
            return recordAttributer.Status;

        }


        #endregion
    }
}
