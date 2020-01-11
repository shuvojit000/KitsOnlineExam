using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Response;
using Lincoln.OnlineExam.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Repository
{
    public class UserRepository : IUserRepository
    {
        public LogInResponseDTO ValidateUser(LogInRequestDTO request, string Operation)
        {
            var result = new LogInResponseDTO();
            SqlParameter userType = new SqlParameter("@UserType", SqlDbType.VarChar);
            userType.Value = request.UserType;

            SqlParameter userName = new SqlParameter("@UserName", SqlDbType.VarChar);
            userName.Value = request.UserName;

            SqlParameter emailID = new SqlParameter("@EmailID", SqlDbType.VarChar);
            emailID.Value = request.EmailID;

            SqlParameter mobileNo = new SqlParameter("@MobileNo", SqlDbType.VarChar);
            mobileNo.Value = request.MobileNo;

            SqlParameter password = new SqlParameter("@Password", SqlDbType.VarChar);
            password.Value = request.Password;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.VarChar);
            type.Value = Operation;

            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.User].[upGetLogin]", userType, userName, emailID, mobileNo, password, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result.LoginID = Convert.ToInt32(dr["LoginID"]);
                        result.UserName = object.ReferenceEquals(dr["UserName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["UserName"]);
                        result.MobileNO = object.ReferenceEquals(dr["MobileNO"], DBNull.Value) ? string.Empty : Convert.ToString(dr["MobileNO"]);
                        result.Password = object.ReferenceEquals(dr["Password"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Password"]);
                        result.EmailID = object.ReferenceEquals(dr["EmailID"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmailID"]);
                        result.UserType = object.ReferenceEquals(dr["UserType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["UserType"]);
                    }
                }
            }
            return result;
        }

        public int SaveStudent(StudentRequestDTO recordAttributer, string Operation)
        {

            SqlParameter studentID = new SqlParameter("@StudentID", SqlDbType.Int);
            studentID.Value = recordAttributer.StudentID;

            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;
            SqlParameter batchID = new SqlParameter("@BatchID", SqlDbType.Int);
            batchID.Value = recordAttributer.BatchID;
            SqlParameter studentName = new SqlParameter("@StudentName", SqlDbType.VarChar);
            studentName.Value = recordAttributer.StudentName;
            SqlParameter rollNo = new SqlParameter("@RollNo", SqlDbType.VarChar);
            rollNo.Value = recordAttributer.RollNo;
            SqlParameter mobileNo = new SqlParameter("@MobileNo", SqlDbType.Int);
            mobileNo.Value = recordAttributer.MobileNo;
            SqlParameter emailID = new SqlParameter("@EmailID", SqlDbType.Int);
            emailID.Value = recordAttributer.EmailID;
            SqlParameter active = new SqlParameter("@Active", SqlDbType.Int);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Int);
            type.Value = recordAttributer.Type;

            SqlParameter status = new SqlParameter("@Status", SqlDbType.BigInt);
            status.Value = recordAttributer.Status;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Student].[upSaveStudent]",studentID,loginID,batchID,studentName,rollNo,mobileNo,
                                       emailID,active,createdBy,type,status );

            recordAttributer.Status = Convert.ToInt32(status.Value);
            return recordAttributer.Status;
        }



    }
}
