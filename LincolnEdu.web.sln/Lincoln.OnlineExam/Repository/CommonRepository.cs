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

namespace Lincoln.OnlineExam.Repository
{
    public class CommonRepository : ICommon
    {

        #region Dropdown Code

        public List<DropdownResponseDTO> GetDropdownData(string Type)
        {
            var itemSet = new List<DropdownResponseDTO>();
            SqlParameter type = new SqlParameter("@Type", SqlDbType.VarChar);
            type.Value = Type;
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetDropDown]", type))
            {
                while (dr.Read())
                {

                    itemSet.Add(new DropdownResponseDTO()
                    {

                        CodeID = Convert.ToString(dr["CodeID"]),
                        CodeDesc = object.ReferenceEquals(dr["CodeDesc"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CodeDesc"]),

                    });
                }
            }
            return itemSet;
        }
        #endregion

        #region Subject

        public int SaveSubject(SubjectRequestDTO recordAttributer, string Operation)
        {
            SqlParameter subjectID = new SqlParameter("@SubjectID", SqlDbType.Int);
            subjectID.Value = recordAttributer.SubjectID;
            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = recordAttributer.CourseID;

            SqlParameter subjectCode = new SqlParameter("@SubjectCode", SqlDbType.VarChar);
            subjectCode.Value = recordAttributer.SubjectCode;
            SqlParameter subjectName = new SqlParameter("@SubjectName", SqlDbType.VarChar);
            subjectName.Value = recordAttributer.SubjectName;
            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;
            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveSubject]", subjectID, courseID, subjectCode, subjectName, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }

        public List<SubjectResponseDTO> GetAllSubject()
        {
            var itemSet = new List<SubjectResponseDTO>();

            SqlParameter subjectID = new SqlParameter("@SubjectID", SqlDbType.Int);
            subjectID.Value = DBNull.Value;
            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = DBNull.Value;

            SqlParameter subjectCode = new SqlParameter("@SubjectCode", SqlDbType.VarChar);
            subjectCode.Value = DBNull.Value;
            SqlParameter subjectName = new SqlParameter("@SubjectName", SqlDbType.VarChar);
            subjectName.Value = DBNull.Value;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetSubject]", courseID, subjectID, subjectCode, subjectName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new SubjectResponseDTO()
                        {
                            CourseID = Convert.ToInt32(dr["CourseID"]),
                            SubjectID = Convert.ToInt32(dr["SubjectID"]),
                            SubjectCode = object.ReferenceEquals(dr["SubjectCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SubjectCode"]),
                            SubjectName = object.ReferenceEquals(dr["SubjectName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SubjectName"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
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
        public SubjectResponseDTO SelectSubject(SubjectRequestDTO recordAttributer)
        {
            var item = new SubjectResponseDTO();

            SqlParameter subjectID = new SqlParameter("@SubjectID", SqlDbType.Int);
            subjectID.Value = recordAttributer.SubjectID;
            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = DBNull.Value;

            SqlParameter subjectCode = new SqlParameter("@SubjectCode", SqlDbType.VarChar);
            subjectCode.Value = recordAttributer.SubjectCode;
            SqlParameter subjectName = new SqlParameter("@SubjectName", SqlDbType.VarChar);
            subjectName.Value = recordAttributer.SubjectName;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetSubject]", courseID, subjectID, subjectCode, subjectName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.CourseID = Convert.ToInt32(dr["CourseID"]);
                        item.SubjectID = Convert.ToInt32(dr["SubjectID"]);
                        item.SubjectCode = object.ReferenceEquals(dr["SubjectCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SubjectCode"]);
                        item.SubjectName = object.ReferenceEquals(dr["SubjectName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SubjectName"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
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

        #region Course


        public int SaveCourse(CourseRequestDTO recordAttributer, string Operation)
        {
            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = recordAttributer.CourseID;

            SqlParameter courseCode = new SqlParameter("@CourseCode", SqlDbType.VarChar);
            courseCode.Value = recordAttributer.CourseCode;
            SqlParameter courseName = new SqlParameter("@CourseName", SqlDbType.VarChar);
            courseName.Value = recordAttributer.CourseName;
            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;
            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveCourse]", courseID, courseCode, courseName, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<CourseResponseDTO> GetAllCourse()
        {
            var itemSet = new List<CourseResponseDTO>();


            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = DBNull.Value;

            SqlParameter courseCode = new SqlParameter("@CourseCode", SqlDbType.VarChar);
            courseCode.Value = DBNull.Value;
            SqlParameter courseName = new SqlParameter("@CourseName", SqlDbType.VarChar);
            courseName.Value = DBNull.Value;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetCourse]", courseID, courseCode, courseName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new CourseResponseDTO()
                        {
                            CourseID = Convert.ToInt32(dr["CourseID"]),

                            CourseCode = object.ReferenceEquals(dr["CourseCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseCode"]),
                            CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
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

        public CourseResponseDTO SelectCourse(CourseRequestDTO recordAttributer)
        {
            var item = new CourseResponseDTO();


            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = recordAttributer.CourseID;

            SqlParameter courseCode = new SqlParameter("@CourseCode", SqlDbType.VarChar);
            courseCode.Value = recordAttributer.CourseCode;
            SqlParameter courseName = new SqlParameter("@CourseName", SqlDbType.VarChar);
            courseName.Value = recordAttributer.CourseName;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetCourse]", courseID, courseCode, courseName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.CourseID = Convert.ToInt32(dr["CourseID"]);
                        item.CourseCode = object.ReferenceEquals(dr["CourseCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseCode"]);
                        item.CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
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

        #region Batch


        public int SaveBatche(BatchRequestDTO recordAttributer, string Operation)
        {
            SqlParameter batchID = new SqlParameter("@BatchID", SqlDbType.Int);
            batchID.Value = recordAttributer.BatchID;

            SqlParameter batchName = new SqlParameter("@BatchName", SqlDbType.VarChar);
            batchName.Value = recordAttributer.BatchName;
            SqlParameter batchDesc = new SqlParameter("@BatchDesc", SqlDbType.VarChar);
            batchDesc.Value = recordAttributer.BatchDesc;
            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;
            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Student].[upSaveBatch]", batchID, batchName, batchDesc, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<BatchResponseDTO> GetAllBatch()
        {
            var itemSet = new List<BatchResponseDTO>();


            SqlParameter batchID = new SqlParameter("@BatchID", SqlDbType.Int);
            batchID.Value = DBNull.Value;

            SqlParameter batchName = new SqlParameter("@BatchName", SqlDbType.VarChar);
            batchName.Value = DBNull.Value;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Student].[upGetBatch]", batchID, batchName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new BatchResponseDTO()
                        {
                            BatchID = Convert.ToInt32(dr["BatchID"]),

                            BatchName = object.ReferenceEquals(dr["BatchName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["BatchName"]),
                            BatchDesc = object.ReferenceEquals(dr["BatchDesc"], DBNull.Value) ? string.Empty : Convert.ToString(dr["BatchDesc"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
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

        public BatchResponseDTO SelectBatch(BatchRequestDTO recordAttributer)
        {
            var item = new BatchResponseDTO();


            SqlParameter batchID = new SqlParameter("@BatchID", SqlDbType.Int);
            batchID.Value = recordAttributer.BatchID;

            SqlParameter batchName = new SqlParameter("@BatchName", SqlDbType.VarChar);
            batchName.Value = recordAttributer.BatchName;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Student].[upGetBatch]", batchID, batchName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.BatchID = Convert.ToInt32(dr["BatchID"]);
                        item.BatchName = object.ReferenceEquals(dr["BatchName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["BatchName"]);
                        item.BatchDesc = object.ReferenceEquals(dr["BatchDesc"], DBNull.Value) ? string.Empty : Convert.ToString(dr["BatchDesc"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
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

        #region Question Section

        public int SaveQuestionSection(QuestionSectionRequestDTO recordAttributer, string Operation)
        {
            SqlParameter questionSectionID = new SqlParameter("@QuestionSectionID", SqlDbType.Int);
            questionSectionID.Value = recordAttributer.QuestionSectionID;

            SqlParameter questionSectionName = new SqlParameter("@QuestionSectionName", SqlDbType.VarChar);
            questionSectionName.Value = recordAttributer.QuestionSectionName;

            SqlParameter qestionSectionDesc = new SqlParameter("@QuestionSectionDesc", SqlDbType.VarChar);
            qestionSectionDesc.Value = recordAttributer.QuestionSectionDesc;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            
            SqlParameter section = new SqlParameter("@Section", SqlDbType.Char);
            section.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveQuestionSection]", questionSectionID, questionSectionName, qestionSectionDesc, active, createdBy, section, status);

            return Convert.ToInt32(status.Value);

        }
        public List<QuestionSectionResponseDTO> GetAllQuestionSection()
        {
            var itemSet = new List<QuestionSectionResponseDTO>();


            SqlParameter questionSectionID = new SqlParameter("@QuestionSectionID", SqlDbType.Int);
            questionSectionID.Value = DBNull.Value;
            SqlParameter subjectID = new SqlParameter("@SubjectID", SqlDbType.Int);
            subjectID.Value = DBNull.Value;
            SqlParameter questionSectionName = new SqlParameter("@QuestionSectionName", SqlDbType.VarChar);
            questionSectionName.Value = DBNull.Value;
            SqlParameter questionSectionDesc = new SqlParameter("@QuestionSectionDesc", SqlDbType.VarChar);
            questionSectionDesc.Value = DBNull.Value;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetQuestionSection]", questionSectionID, subjectID, questionSectionName, questionSectionDesc, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new QuestionSectionResponseDTO()
                        {
                            QuestionSectionID = Convert.ToInt32(dr["QuestionSectionID"]),

                            QuestionSectionName = object.ReferenceEquals(dr["QuestionSectionName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionSectionName"]),
                            QuestionSectionDesc = object.ReferenceEquals(dr["QuestionSectionDesc"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionSectionDesc"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
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

        public QuestionSectionResponseDTO SelectQuestionSection(QuestionSectionRequestDTO recordAttributer)
        {
            var item = new QuestionSectionResponseDTO();


            SqlParameter questionSectionID = new SqlParameter("@QuestionSectionID", SqlDbType.Int);
            questionSectionID.Value = recordAttributer.QuestionSectionID;
            SqlParameter subjectID = new SqlParameter("@SubjectID", SqlDbType.Int);
            subjectID.Value = recordAttributer.SubjectID;

            SqlParameter questionSectionName = new SqlParameter("@QuestionSectionName", SqlDbType.VarChar);
            questionSectionName.Value = recordAttributer.QuestionSectionName;
            SqlParameter questionSectionDesc = new SqlParameter("@QuestionSectionDesc", SqlDbType.VarChar);
            questionSectionDesc.Value = recordAttributer.QuestionSectionDesc;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetQuestionSection]", questionSectionID, subjectID,
                questionSectionName, questionSectionDesc, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.QuestionSectionID = Convert.ToInt32(dr["QuestionSectionID"]);
                        item.QuestionSectionName = object.ReferenceEquals(dr["QuestionSectionName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionSectionName"]);
                        item.QuestionSectionDesc = object.ReferenceEquals(dr["QuestionSectionDesc"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionSectionDesc"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
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
    }
}
