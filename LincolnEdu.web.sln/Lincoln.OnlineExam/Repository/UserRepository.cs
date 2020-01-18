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

        public int SaveFaculty(FacultyRequestDTO recordAttributer, string Operation)
        {

            SqlParameter facultyID = new SqlParameter("@FacultyID", SqlDbType.Int);
            facultyID.Value = recordAttributer.FacultyID;

            SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
            loginID.Value = recordAttributer.LoginID;

            SqlParameter employeeCode = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
            employeeCode.Value = recordAttributer.EmployeeCode;

            SqlParameter employeeName = new SqlParameter("@EmployeeName", SqlDbType.VarChar);
            employeeName.Value = recordAttributer.EmployeeName;
           
            SqlParameter mobileNo = new SqlParameter("@MobileNo", SqlDbType.Int);
            mobileNo.Value = recordAttributer.MobileNo;
            SqlParameter emailID = new SqlParameter("@EmailID", SqlDbType.Int);
            emailID.Value = recordAttributer.EmailID;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Int);
            active.Value = recordAttributer.Active;

            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Int);
            type.Value = Operation;

            SqlParameter status = new SqlParameter("@Status", SqlDbType.BigInt);
            status.Value = recordAttributer.Status;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Faculty].[upSaveFaculty]", facultyID, loginID, employeeCode, employeeName, mobileNo,
                                       emailID, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);
             
        }

        //public List<FacultyResponseDTO> GetAllFaculty()
        //{
        //    var itemSet = new List<FacultyResponseDTO>();

        //    SqlParameter facultyID = new SqlParameter("@FacultyID", SqlDbType.Int);
        //    facultyID.Value = DBNull.Value;
        //    SqlParameter loginID = new SqlParameter("@LoginID", SqlDbType.Int);
        //    loginID.Value = DBNull.Value;

        //    SqlParameter employeeCode = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
        //    employeeCode.Value = DBNull.Value;
        //    SqlParameter employeeName = new SqlParameter("@EmployeeName", SqlDbType.VarChar);
        //    employeeName.Value = DBNull.Value;

        //    SqlParameter emailID = new SqlParameter("@EmailID", SqlDbType.VarChar);
        //    employeeName.Value = DBNull.Value;


        //    SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
        //    type.Value = "GET";
        //    //using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetSubject]", courseID, subjectID, subjectCode, subjectName, type))
        //    //{
        //    //    if (dr != null && dr.HasRows)
        //    //    {
        //    //        while (dr.Read())
        //    //        {
        //    //            itemSet.Add(new SubjectResponseDTO()
        //    //            {
        //    //                CourseID = Convert.ToInt32(dr["CourseID"]),
        //    //                SubjectID = Convert.ToInt32(dr["SubjectID"]),
        //    //                SubjectCode = object.ReferenceEquals(dr["SubjectCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SubjectCode"]),
        //    //                SubjectName = object.ReferenceEquals(dr["SubjectName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SubjectName"]),
        //    //                Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
        //    //                CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
        //    //                //CreatedOn = object.ReferenceEquals(dr["CreatedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["CreatedOn"]),
        //    //                // ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]),
        //    //                // ModifiedOn = object.ReferenceEquals(dr["ModifiedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["ModifiedOn"])

        //    //            });

        //    //        }
        //    //    }
        //    }
        //    return itemSet;

        //}
        //public SubjectResponseDTO SelectSubject(SubjectRequestDTO recordAttributer)
        //{
        //    var item = new SubjectResponseDTO();

        //    SqlParameter subjectID = new SqlParameter("@SubjectID", SqlDbType.Int);
        //    subjectID.Value = recordAttributer.SubjectID;
        //    SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
        //    courseID.Value = DBNull.Value;

        //    SqlParameter subjectCode = new SqlParameter("@SubjectCode", SqlDbType.VarChar);
        //    subjectCode.Value = recordAttributer.SubjectCode;
        //    SqlParameter subjectName = new SqlParameter("@SubjectName", SqlDbType.VarChar);
        //    subjectName.Value = recordAttributer.SubjectName;

        //    SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
        //    type.Value = "GET";
        //    using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetSubject]", courseID, subjectID, subjectCode, subjectName, type))
        //    {
        //        if (dr != null && dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {

        //                item.CourseID = Convert.ToInt32(dr["CourseID"]);
        //                item.SubjectID = Convert.ToInt32(dr["SubjectID"]);
        //                item.SubjectCode = object.ReferenceEquals(dr["SubjectCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SubjectCode"]);
        //                item.SubjectName = object.ReferenceEquals(dr["SubjectName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SubjectName"]);
        //                item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
        //                item.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
        //                //item.CreatedOn = object.ReferenceEquals(dr["CreatedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["CreatedOn"]);
        //                //item.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
        //                // item.ModifiedOn = object.ReferenceEquals(dr["ModifiedOn"], DBNull.Value) ? default(DateTime) : Convert.ToDateTime(dr["ModifiedOn"]);

        //            }
        //        }
        //    }
        //    return item;

        //}


    }
}
