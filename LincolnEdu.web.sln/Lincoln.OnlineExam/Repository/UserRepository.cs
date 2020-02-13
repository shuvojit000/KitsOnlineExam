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


        #region Employee 


        public int SaveEmployee(EmployeeRequestDTO recordAttributer, string Operation)
        {

            SqlParameter employeeID = new SqlParameter("@EmployeeID", SqlDbType.Int);
            employeeID.Value = recordAttributer.EmployeeID;
            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;
            SqlParameter userName = new SqlParameter("@UserName", SqlDbType.VarChar);
            userName.Value = recordAttributer.UserName;
            SqlParameter employeeName = new SqlParameter("@EmployeeName", SqlDbType.VarChar);
            employeeName.Value = recordAttributer.EmployeeName;
            SqlParameter employeeCode = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
            employeeCode.Value = recordAttributer.EmployeeCode;
            SqlParameter password = new SqlParameter("@Password", SqlDbType.VarChar);
            password.Value = recordAttributer.Password;
            SqlParameter userType = new SqlParameter("@UserType", SqlDbType.VarChar);
            userType.Value = recordAttributer.UserType;
            SqlParameter emailID = new SqlParameter("@EmailID", SqlDbType.VarChar);
            emailID.Value = recordAttributer.EmailID;
            SqlParameter mobileNo = new SqlParameter("@MobileNo", SqlDbType.VarChar);
            mobileNo.Value = recordAttributer.MobileNo;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;
            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = recordAttributer.Status;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Faculty].[upSaveEmployee]", employeeID, loginID, userName, employeeName, employeeCode,
               password, userType, emailID, mobileNo,
                active, createdBy, type, status);

            recordAttributer.Status = Convert.ToInt32(status.Value);
            return recordAttributer.Status;
        }

        public List<EmployeeResponseDTO> GetAllEmployee()
        {
            var itemSet = new List<EmployeeResponseDTO>();


            SqlParameter employeeID = new SqlParameter("@EmployeeID", SqlDbType.Int);
            employeeID.Value = DBNull.Value;
            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = DBNull.Value;
            SqlParameter employeeCode = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
            employeeCode.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetEmployee]", employeeID, loginID, employeeCode, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new EmployeeResponseDTO()
                        {
                            EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                            LoginID = Convert.ToInt32(dr["LoginID"]),
                            EmployeeName = object.ReferenceEquals(dr["EmployeeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmployeeName"]),
                            EmployeeCode = object.ReferenceEquals(dr["EmployeeCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmployeeCode"]),
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

        public EmployeeResponseDTO SelectEmployee(EmployeeRequestDTO recordAttributer)
        {
            var item = new EmployeeResponseDTO();

            SqlParameter employeeID = new SqlParameter("@EmployeeID", SqlDbType.Int);
            employeeID.Value = recordAttributer.EmployeeID;
            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;
            SqlParameter employeeCode = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
            employeeCode.Value = recordAttributer.EmployeeCode;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";

            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetEmployee]", employeeID, loginID, employeeCode, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                        item.LoginID = Convert.ToInt32(dr["LoginID"]);
                        item.EmailID = object.ReferenceEquals(dr["EmailID"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmailID"]);
                        item.EmployeeCode = object.ReferenceEquals(dr["EmployeeCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmployeeCode"]);
                        item.Password = object.ReferenceEquals(dr["Password"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Password"]);
                        item.MobileNo = object.ReferenceEquals(dr["MobileNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["MobileNo"]);
                        item.EmployeeName = object.ReferenceEquals(dr["EmployeeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmployeeName"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        item.UserName = object.ReferenceEquals(dr["UserName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["UserName"]);
                        // item.UserType = object.ReferenceEquals(dr["UserType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["UserType"]);
                        // item.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                        //item.CreatedOn = object.ReferenceEquals(dr["CreatedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["CreatedOn"]);
                        //item.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
                        // item.ModifiedOn = object.ReferenceEquals(dr["ModifiedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["ModifiedOn"]);

                    }
                }
            }
            return item;

        }


        #endregion



    }
}
