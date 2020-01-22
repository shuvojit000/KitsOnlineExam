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

        #region Change Status
        public int UpdateStatus(UpdateStatusReuestDTO recordAttributer)
        {
            SqlParameter iD = new SqlParameter("@ID", SqlDbType.VarChar);
            iD.Value = recordAttributer.ID;
            SqlParameter table = new SqlParameter("@Table", SqlDbType.VarChar);
            table.Value = recordAttributer.Table;
            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;
            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upUpdateStatus]", iD, table, active, createdBy, status);

            return Convert.ToInt32(status.Value);
        }
        #endregion

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


        #region AcademicLevel


        public int SaveAcademicLevel(AcademicLevelRequestDTO recordAttributer, string Operation)
        {
            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = recordAttributer.AcademicID;

            SqlParameter academicCode = new SqlParameter("@AcademicCode", SqlDbType.VarChar);
            academicCode.Value = recordAttributer.AcademicCode;

            SqlParameter academicName = new SqlParameter("@AcademicName", SqlDbType.VarChar);
            academicName.Value = recordAttributer.AcademicName;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveAcademic]", academicID, academicCode, academicName, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<AcademicLevelResponseDTO> GetAllAcademicLevel()
        {
            var itemSet = new List<AcademicLevelResponseDTO>();


            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = DBNull.Value;
            SqlParameter academicCode = new SqlParameter("@AcademicCode", SqlDbType.VarChar);
            academicCode.Value = DBNull.Value;
            SqlParameter academicName = new SqlParameter("@AcademicName", SqlDbType.VarChar);
            academicName.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetAcademic]", academicID, academicCode, academicName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new AcademicLevelResponseDTO()
                        {
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),

                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            AcademicCode = object.ReferenceEquals(dr["AcademicCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicCode"]),
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

        public AcademicLevelResponseDTO SelectAcademicLevel(AcademicLevelRequestDTO recordAttributer)
        {
            var item = new AcademicLevelResponseDTO();


            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = recordAttributer.AcademicID;

            SqlParameter academicCode = new SqlParameter("@AcademicCode", SqlDbType.VarChar);
            academicCode.Value = recordAttributer.AcademicCode;
            SqlParameter academicName = new SqlParameter("@AcademicName", SqlDbType.VarChar);
            academicName.Value = recordAttributer.AcademicName;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetAcademic]", academicID, academicCode,
                academicName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        item.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
                        item.AcademicCode = object.ReferenceEquals(dr["AcademicCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicCode"]);
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

        #region Department

        public int SaveDepartment(DepartmentRequestDTO recordAttributer, string Operation)
        {
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = recordAttributer.DepartmentID;

            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = recordAttributer.AcademicID;

            SqlParameter departmentCode = new SqlParameter("@DepartmentCode", SqlDbType.VarChar);
            departmentCode.Value = recordAttributer.DepartmentCode;

            SqlParameter departmentName = new SqlParameter("@DepartmentName", SqlDbType.VarChar);
            departmentName.Value = recordAttributer.DepartmentName;
            SqlParameter hODName = new SqlParameter("@HODName", SqlDbType.VarChar);
            hODName.Value = recordAttributer.HODName;
            SqlParameter hODEmail = new SqlParameter("@HODEmail", SqlDbType.VarChar);
            hODEmail.Value = recordAttributer.HODEmail;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveDepartment]", departmentID, academicID, departmentCode, departmentName,
                hODName, hODEmail, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<DepartmentResponseDTO> GetAllDepartment()
        {
            var itemSet = new List<DepartmentResponseDTO>();

            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = DBNull.Value;
            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = DBNull.Value;
            SqlParameter departmentCode = new SqlParameter("@DepartmentCode", SqlDbType.VarChar);
            departmentCode.Value = DBNull.Value;
            SqlParameter departmentName = new SqlParameter("@DepartmentName", SqlDbType.VarChar);
            departmentName.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetDepartment]", departmentID, academicID, departmentCode, departmentName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new DepartmentResponseDTO()
                        {
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            DepartmentCode = object.ReferenceEquals(dr["DepartmentCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentCode"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            HODName = object.ReferenceEquals(dr["HODName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["HODName"]),
                            HODEmail = object.ReferenceEquals(dr["HODEmail"], DBNull.Value) ? string.Empty : Convert.ToString(dr["HODEmail"]),
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

        public DepartmentResponseDTO SelectDepartment(DepartmentRequestDTO recordAttributer)
        {
            var item = new DepartmentResponseDTO();


            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = recordAttributer.DepartmentID;
            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = recordAttributer.AcademicID;
            SqlParameter departmentCode = new SqlParameter("@DepartmentCode", SqlDbType.VarChar);
            departmentCode.Value = recordAttributer.DepartmentCode;
            SqlParameter departmentName = new SqlParameter("@DepartmentName", SqlDbType.VarChar);
            departmentName.Value = recordAttributer.DepartmentName;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetDepartment]", departmentID, academicID, departmentCode, departmentName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        item.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        item.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
                        item.DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.DepartmentCode = object.ReferenceEquals(dr["DepartmentCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentCode"]);
                        item.HODName = object.ReferenceEquals(dr["HODName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["HODName"]);
                        item.HODEmail = object.ReferenceEquals(dr["HODEmail"], DBNull.Value) ? string.Empty : Convert.ToString(dr["HODEmail"]);
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


        #region Programme

        public int SaveProgramme(ProgrammeRequestDTO recordAttributer, string Operation)
        {
            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = recordAttributer.ProgrammeID;

            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = recordAttributer.AcademicID;

            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = recordAttributer.DepartmentID;



            SqlParameter programmeCode = new SqlParameter("@ProgrammeCode", SqlDbType.VarChar);
            programmeCode.Value = recordAttributer.ProgrammeCode;

            SqlParameter programmeName = new SqlParameter("@ProgrammeName", SqlDbType.VarChar);
            programmeName.Value = recordAttributer.ProgrammeName;
            SqlParameter approvalNo = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
            approvalNo.Value = recordAttributer.ApprovalNo;

            SqlParameter credit = new SqlParameter("@Credit", SqlDbType.VarChar);
            credit.Value = recordAttributer.Credit;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveProgramme]", programmeID, academicID, departmentID, programmeCode, programmeName,
                approvalNo, credit, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<ProgrammeResponseDTO> GetAllProgramme()
        {
            var itemSet = new List<ProgrammeResponseDTO>();

            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = DBNull.Value;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = DBNull.Value;
            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = DBNull.Value;
            SqlParameter programmeCode = new SqlParameter("@ProgrammeCode", SqlDbType.VarChar);
            programmeCode.Value = DBNull.Value;
            SqlParameter programmeName = new SqlParameter("@ProgrammeName", SqlDbType.VarChar);
            programmeName.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgramme]", programmeID, academicID, departmentID, programmeCode,
                programmeName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new ProgrammeResponseDTO()
                        {
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            ProgrammeCode = object.ReferenceEquals(dr["ProgrammeCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeCode"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ApprovalNo = object.ReferenceEquals(dr["ApprovalNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ApprovalNo"]),
                            Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]),
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

        public ProgrammeResponseDTO SelectProgramme(ProgrammeRequestDTO recordAttributer)
        {
            var item = new ProgrammeResponseDTO();

            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = recordAttributer.ProgrammeID;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = recordAttributer.DepartmentID;
            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = recordAttributer.AcademicID;
            SqlParameter programmeCode = new SqlParameter("@ProgrammeCode", SqlDbType.VarChar);
            programmeCode.Value = recordAttributer.ProgrammeCode;
            SqlParameter programmeName = new SqlParameter("@ProgrammeName", SqlDbType.VarChar);
            programmeName.Value = recordAttributer.ProgrammeName;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgramme]", programmeID, academicID, departmentID, programmeCode, programmeName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]);
                        item.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        item.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        item.ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
                        item.DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.ProgrammeCode = object.ReferenceEquals(dr["ProgrammeCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeCode"]);
                        item.ApprovalNo = object.ReferenceEquals(dr["ApprovalNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ApprovalNo"]);
                        item.Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]);
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

        #region ProgramVersioning

        public int SaveProgramVersioning(ProgramVersioningRequestDTO recordAttributer, string Operation)
        {
            SqlParameter programVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programVersioningID.Value = recordAttributer.ProgramVersioningID;

            SqlParameter programCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programCode.Value = recordAttributer.ProgramCode;

            SqlParameter departmentCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentCode.Value = recordAttributer.DepartmentCode;

            SqlParameter version = new SqlParameter("@Version", SqlDbType.VarChar);
            version.Value = recordAttributer.Version;
            SqlParameter placeHolder = new SqlParameter("@SyllabusVersion", SqlDbType.VarChar);
            placeHolder.Value = recordAttributer.PlaceHolder;
            SqlParameter credit = new SqlParameter("@Credit", SqlDbType.VarChar);
            credit.Value = recordAttributer.Credit;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveProgrammeVersioning]", programVersioningID, programCode, departmentCode, version,
                placeHolder, credit, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<ProgramVersioningResponseDTO> GetAllProgramVersioning()
        {
            var itemSet = new List<ProgramVersioningResponseDTO>();

            SqlParameter ProgrammeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            ProgrammeVersioningID.Value = DBNull.Value;
            SqlParameter departmentCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentCode.Value = DBNull.Value;
            SqlParameter programCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programCode.Value = DBNull.Value;
            SqlParameter version = new SqlParameter("@Version", SqlDbType.VarChar);
            version.Value = DBNull.Value;
            SqlParameter placeHolder = new SqlParameter("@SyllabusVersion", SqlDbType.VarChar);
            placeHolder.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgrammeVersioning]", ProgrammeVersioningID, departmentCode, programCode, version, placeHolder, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new ProgramVersioningResponseDTO()
                        {
                            ProgramVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]),
                            DepartmentCode = Convert.ToInt32(dr["DepartmentID"]),
                            ProgramCode = Convert.ToInt32(dr["ProgrammeID"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            PlaceHolder = object.ReferenceEquals(dr["SyllabusVersion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SyllabusVersion"]),
                            Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        });

                    }
                }
            }
            return itemSet;

        }

        public ProgramVersioningResponseDTO SelectProgramVersioning(ProgramVersioningRequestDTO recordAttributer)
        {
            var item = new ProgramVersioningResponseDTO();


            SqlParameter programVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programVersioningID.Value = DBNull.Value;
            SqlParameter departmentCode = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            departmentCode.Value = DBNull.Value;
            SqlParameter programCode = new SqlParameter("@ProgrammeID", SqlDbType.VarChar);
            programCode.Value = DBNull.Value;
            SqlParameter version = new SqlParameter("@Version", SqlDbType.VarChar);
            version.Value = DBNull.Value;
            SqlParameter placeHolder = new SqlParameter("@SyllabusVersion", SqlDbType.VarChar);
            placeHolder.Value = DBNull.Value;
            //SqlParameter credit = new SqlParameter("@Credit", SqlDbType.VarChar);
            //credit.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgrammeVersioning]", programVersioningID, programCode, departmentCode, version, placeHolder, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.ProgramVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        item.ProgramCode = Convert.ToInt32(dr["ProgrammeID"]);
                        item.Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        item.PlaceHolder = object.ReferenceEquals(dr["SyllabusVersion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SyllabusVersion"]);
                        item.DepartmentCode = Convert.ToInt32(dr["DepartmentID"]);
                        item.Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        item.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                    }
                }
            }
            return item;

        }
        #endregion


        #region Programme Semester

        public int SaveProgrammeSemester(ProgrammeSemesterRequestDTO recordAttributer, string Operation)
        {
            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = recordAttributer.ProgrammeSemesterID;

            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = recordAttributer.DepartmentID;

            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = recordAttributer.ProgrammeID;

            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = recordAttributer.CountryID;

            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = recordAttributer.ProgrammeYear;

            SqlParameter programmeSemester = new SqlParameter("@ProgrammeSemester", SqlDbType.VarChar);
            programmeSemester.Value = recordAttributer.ProgrammeSemester;

            SqlParameter semesterType = new SqlParameter("@SemesterType", SqlDbType.VarChar);
            semesterType.Value = recordAttributer.SemesterType;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveProgrammeSemester]", programmeSemesterID, departmentID, programmeID, countryID,
                programmeYear, programmeSemester, semesterType,
                active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<ProgrammeSemesterResponseDTO> GetAllProgrammeSemester()
        {
            var itemSet = new List<ProgrammeSemesterResponseDTO>();

            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = DBNull.Value;
            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = DBNull.Value;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = DBNull.Value;

            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = DBNull.Value;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = DBNull.Value;
            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = DBNull.Value;
            SqlParameter programmeSemester = new SqlParameter("@ProgrammeSemester", SqlDbType.VarChar);
            programmeSemester.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgrammeSemester]", programmeSemesterID, academicID, departmentID, programmeID,
                countryID, programmeYear,
                programmeSemester, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new ProgrammeSemesterResponseDTO()
                        {
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            SemesterType = object.ReferenceEquals(dr["SemesterType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SemesterType"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ProgrammeSemesterID = Convert.ToInt32(dr["ProgrammeSemesterID"]),
                            ProgrammeSemester = Convert.ToInt32(dr["ProgrammeSemester"]),
                            ProgrammeYear = Convert.ToInt32(dr["ProgrammeYear"]),
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

        public ProgrammeSemesterResponseDTO SelectProgrammeSemester(ProgrammeSemesterRequestDTO recordAttributer)
        {
            var item = new ProgrammeSemesterResponseDTO();

            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = DBNull.Value;
            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = DBNull.Value;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = DBNull.Value;

            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = DBNull.Value;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = DBNull.Value;
            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = DBNull.Value;
            SqlParameter programmeSemester = new SqlParameter("@ProgrammeSemester", SqlDbType.VarChar);
            programmeSemester.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgrammeSemester]", programmeSemesterID, academicID, departmentID, programmeID,
               countryID, programmeYear,
               programmeSemester, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]);
                        item.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        item.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        item.ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
                        item.SemesterType = object.ReferenceEquals(dr["SemesterType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SemesterType"]);
                        item.DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.ProgrammeSemesterID = Convert.ToInt32(dr["ProgrammeSemesterID"]);
                        item.ProgrammeSemester = Convert.ToInt32(dr["ProgrammeSemester"]);
                        item.ProgrammeYear = Convert.ToInt32(dr["ProgrammeYear"]);
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
