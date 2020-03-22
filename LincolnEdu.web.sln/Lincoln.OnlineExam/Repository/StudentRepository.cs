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
            loginID.Value = DBNull.Value;
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
        #endregion
    }
}
