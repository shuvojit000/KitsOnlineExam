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

        public List<ProgrammeResponseDTO> GetAllProgrammeWithVersion()
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
            type.Value = "GETSEM";
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
                            ProgramVersioningID = Convert.ToInt32(dr["ProgramVersioningID"]),
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            ProgrammeCode = object.ReferenceEquals(dr["ProgrammeCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeCode"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ApprovalNo = object.ReferenceEquals(dr["ApprovalNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ApprovalNo"]),
                            Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
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
            //SqlParameter placeHolder = new SqlParameter("@SyllabusVersion", SqlDbType.VarChar);
            //placeHolder.Value = recordAttributer.PlaceHolder;
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
                 credit, active, createdBy, type, status);

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
            SqlParameter placeHolder = new SqlParameter("@AcademicID", SqlDbType.Int);
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
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            DepartmentCode = Convert.ToInt32(dr["DepartmentID"]),
                            ProgramCode = Convert.ToInt32(dr["ProgrammeID"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
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
            programVersioningID.Value = recordAttributer.ProgramVersioningID;
            SqlParameter departmentCode = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            departmentCode.Value = DBNull.Value;
            SqlParameter programCode = new SqlParameter("@ProgrammeID", SqlDbType.VarChar);
            programCode.Value = DBNull.Value;
            SqlParameter version = new SqlParameter("@Version", SqlDbType.VarChar);
            version.Value = DBNull.Value;
            

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgrammeVersioning]", programVersioningID, programCode, departmentCode, version, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.ProgramVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        item.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        item.ProgramCode = Convert.ToInt32(dr["ProgrammeID"]);
                        item.Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        item.ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.DepartmentCode = Convert.ToInt32(dr["DepartmentID"]);
                        item.DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]);
                        item.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
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

            SqlParameter programVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programVersioningID.Value = recordAttributer.ProgramVersioningID;

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

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveProgrammeSemester]", programmeSemesterID, departmentID, programVersioningID, countryID,
                programmeYear, programmeSemester, semesterType,
                active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<ProgrammeSemesterResponseDTO> GetAllProgrammeSemester()
        {
            var itemSet = new List<ProgrammeSemesterResponseDTO>();

            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = DBNull.Value;
           
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = DBNull.Value;

           
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = DBNull.Value;
            SqlParameter programmeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programmeVersioningID.Value = DBNull.Value;

            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = DBNull.Value;
            SqlParameter programmeSemester = new SqlParameter("@ProgrammeSemester", SqlDbType.VarChar);
            programmeSemester.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgrammeSemester]", programmeSemesterID, departmentID,
                countryID, programmeVersioningID, programmeYear,
                programmeSemester, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new ProgrammeSemesterResponseDTO()
                        {
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            CountryID = Convert.ToInt32(dr["CountryID"]),
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            ProgrammeVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]),
                            ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            ProgrammeCode = object.ReferenceEquals(dr["ProgrammeCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeCode"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            SemesterType = object.ReferenceEquals(dr["SemesterType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SemesterType"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ProgrammeSemesterID = Convert.ToInt32(dr["ProgrammeSemesterID"]),
                            ProgrammeSemester = Convert.ToInt32(dr["ProgrammeSemester"]),
                            ProgrammeYear = Convert.ToInt32(dr["ProgrammeYear"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"])

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
            programmeSemesterID.Value = recordAttributer.ProgrammeSemesterID;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = recordAttributer.DepartmentID;

            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = DBNull.Value;
            SqlParameter programmeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programmeVersioningID.Value = DBNull.Value;
            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = DBNull.Value;
            SqlParameter programmeSemester = new SqlParameter("@ProgrammeSemester", SqlDbType.VarChar);
            programmeSemester.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgrammeSemester]", programmeSemesterID, departmentID,
               countryID, programmeVersioningID, programmeYear,
               programmeSemester, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]);
                        item.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        item.ProgrammeVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        item.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        item.CountryID = Convert.ToInt32(dr["CountryID"]);
                        item.ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
                        item.SemesterType = object.ReferenceEquals(dr["SemesterType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SemesterType"]);
                        item.DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.ProgrammeSemesterID = Convert.ToInt32(dr["ProgrammeSemesterID"]);
                        item.ProgrammeSemester = Convert.ToInt32(dr["ProgrammeSemester"]);
                        item.ProgrammeYear = Convert.ToInt32(dr["ProgrammeYear"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        item.Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        item.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                        item.ProgrammeCode = object.ReferenceEquals(dr["ProgrammeCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeCode"]);

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
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = recordAttributer.DepartmentID;
            SqlParameter programVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programVersioningID.Value = recordAttributer.ProgramVersioningID;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = recordAttributer.CountryID;
            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = recordAttributer.ProgrammeYear;
            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = recordAttributer.ProgrammeSemesterID;
            SqlParameter semesterType = new SqlParameter("@SemesterType", SqlDbType.VarChar);
            semesterType.Value = recordAttributer.SemesterType;

            SqlParameter courseCode = new SqlParameter("@CourseCode", SqlDbType.VarChar);
            courseCode.Value = recordAttributer.CourseCode;
            SqlParameter courseName = new SqlParameter("@CourseName", SqlDbType.VarChar);
            courseName.Value = recordAttributer.CourseName;
            SqlParameter approvalNo = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
            approvalNo.Value = recordAttributer.ApprovalNo;
            SqlParameter courseType = new SqlParameter("@CourseType", SqlDbType.VarChar);
            courseType.Value = recordAttributer.CourseType;
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

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveCourse]", courseID, departmentID, programVersioningID, countryID, programmeYear,
                 programmeSemesterID, semesterType, courseCode, courseName, approvalNo,courseType, credit, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<CourseResponseDTO> GetAllCourse()
        {
            var itemSet = new List<CourseResponseDTO>();


            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = DBNull.Value;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = DBNull.Value;
            SqlParameter programmeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programmeVersioningID.Value = DBNull.Value;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = DBNull.Value;
            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = DBNull.Value;
            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = DBNull.Value;

            SqlParameter courseCode = new SqlParameter("@CourseCode", SqlDbType.VarChar);
            courseCode.Value = DBNull.Value;
            SqlParameter courseName = new SqlParameter("@CourseName", SqlDbType.VarChar);
            courseName.Value = DBNull.Value;
            SqlParameter approvalNo = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
            approvalNo.Value = DBNull.Value;
            SqlParameter credit = new SqlParameter("@Credit", SqlDbType.VarChar);
            credit.Value = DBNull.Value;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetCourse]", courseID, departmentID, programmeVersioningID,
                countryID, programmeYear, programmeSemesterID, courseCode, courseName, approvalNo, credit, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new CourseResponseDTO()
                        {
                            CourseID = Convert.ToInt32(dr["CourseID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            ApprovalNo = object.ReferenceEquals(dr["ApprovalNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ApprovalNo"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]),
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            ProgramVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]),
                            ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            ProgrammeSemesterID = Convert.ToInt32(dr["ProgrammeSemesterID"]),
                            ProgrammeSemester = object.ReferenceEquals(dr["ProgrammeSemester"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeSemester"]),
                            ProgrammeYear = Convert.ToInt32(dr["ProgrammeYear"]),
                            SemesterType = object.ReferenceEquals(dr["SemesterType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SemesterType"]),
                            CourseCode = object.ReferenceEquals(dr["CourseCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseCode"]),
                            CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            CourseType = object.ReferenceEquals(dr["CourseType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseType"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"])

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
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = DBNull.Value;
            SqlParameter programmeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programmeVersioningID.Value = DBNull.Value;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = DBNull.Value;
            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = DBNull.Value;
            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = DBNull.Value;

            SqlParameter courseCode = new SqlParameter("@CourseCode", SqlDbType.VarChar);
            courseCode.Value = DBNull.Value;
            SqlParameter courseName = new SqlParameter("@CourseName", SqlDbType.VarChar);
            courseName.Value = DBNull.Value;
            SqlParameter approvalNo = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
            approvalNo.Value = DBNull.Value;
            SqlParameter credit = new SqlParameter("@Credit", SqlDbType.VarChar);
            credit.Value = DBNull.Value;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetCourse]", courseID, departmentID, programmeVersioningID,
                countryID, programmeYear, programmeSemesterID, courseCode, courseName, approvalNo, credit, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.CourseID = Convert.ToInt32(dr["CourseID"]);
                        item.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        item.CountryId = Convert.ToInt32(dr["CountryId"]);
                        item.ApprovalNo = object.ReferenceEquals(dr["ApprovalNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ApprovalNo"]);
                        item.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
                        item.Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]);
                        item.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        item.DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]);
                        item.ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.ProgramVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        item.Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        item.ProgrammeSemesterID = Convert.ToInt32(dr["ProgrammeSemesterID"]);
                        item.ProgrammeYear = Convert.ToInt32(dr["ProgrammeYear"]);
                        item.SemesterType = object.ReferenceEquals(dr["SemesterType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SemesterType"]);
                        item.ProgrammeSemester = object.ReferenceEquals(dr["ProgrammeSemester"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeSemester"]);
                        item.CourseCode = object.ReferenceEquals(dr["CourseCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseCode"]);
                        item.CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        item.CourseType = object.ReferenceEquals(dr["CourseType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseType"]);
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

        #region Examination Name

        public int SaveExaminationName(ExaminationNameRequestDTO recordAttributer, string Operation)
        {
            CultureInfo culture = new CultureInfo("en-IN");
            SqlParameter ExaminationID = new SqlParameter("@ExaminationID", SqlDbType.Int);
            ExaminationID.Value = recordAttributer.ExaminationNameID;
            SqlParameter ExaminationName = new SqlParameter("@ExaminationName", SqlDbType.VarChar);
            ExaminationName.Value = recordAttributer.ExaminationName;
            SqlParameter StartDate = new SqlParameter("@StartDate", SqlDbType.Date);
            StartDate.Value = Convert.ToDateTime(recordAttributer.StartDate, culture);
            SqlParameter EndDate = new SqlParameter("@EndDate", SqlDbType.Date);
            EndDate.Value = Convert.ToDateTime(recordAttributer.EndDate, culture);
            //SqlParameter StartTime = new SqlParameter("@StartTime", SqlDbType.VarChar);
            //StartTime.Value = recordAttributer.StartTime;
            //SqlParameter EndTime = new SqlParameter("@EndTime", SqlDbType.VarChar);
            //EndTime.Value = recordAttributer.EndTime;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;
            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Examination].[upSaveExamination]", ExaminationID, ExaminationName, StartDate, EndDate, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<ExaminationNameResponseDTO> GetAllExaminationName()
        {
            var itemSet = new List<ExaminationNameResponseDTO>();

            SqlParameter ExaminationID = new SqlParameter("@ExaminationID", SqlDbType.Int);
            ExaminationID.Value = DBNull.Value;
            SqlParameter ExaminationName = new SqlParameter("@ExaminationName", SqlDbType.VarChar);
            ExaminationName.Value = DBNull.Value;
            SqlParameter StartDate = new SqlParameter("@StartDate", SqlDbType.Date);
            StartDate.Value = DBNull.Value;
            SqlParameter EndDate = new SqlParameter("@EndDate", SqlDbType.Date);
            EndDate.Value = DBNull.Value;
            //SqlParameter StartTime = new SqlParameter("@StartTime", SqlDbType.VarChar);
            //StartTime.Value = DBNull.Value;
            //SqlParameter EndTime = new SqlParameter("@EndTime", SqlDbType.VarChar);
            //EndTime.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetExamination]", ExaminationID, ExaminationName, StartDate, EndDate, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new ExaminationNameResponseDTO()
                        {
                            ExaminationNameID = Convert.ToInt32(dr["ExaminationID"]),
                            ExaminationName = object.ReferenceEquals(dr["ExaminationName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ExaminationName"]),
                            StartDate = Convert.ToDateTime(dr["StartDate"]),
                            EndDate = Convert.ToDateTime(dr["EndDate"]),
                            //StartDate = object.ReferenceEquals(dr["StartDate"], DBNull.Value) ? string.Empty : Convert.ToString(dr["StartDate"]),
                            // EndDate = object.ReferenceEquals(dr["EndDate"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EndDate"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        });

                    }
                }
            }
            return itemSet;

        }

        public ExaminationNameResponseDTO SelectExaminationName(ExaminationNameRequestDTO recordAttributer)
        {
            var item = new ExaminationNameResponseDTO();


            SqlParameter ExaminationNameID = new SqlParameter("@ExaminationID", SqlDbType.Int);
            ExaminationNameID.Value = recordAttributer.ExaminationNameID;
            SqlParameter ExaminationName = new SqlParameter("@ExaminationName", SqlDbType.VarChar);
            ExaminationName.Value = DBNull.Value;
            SqlParameter StartDate = new SqlParameter("@StartDate", SqlDbType.Date);
            StartDate.Value = DBNull.Value;
            SqlParameter EndDate = new SqlParameter("@EndDate", SqlDbType.Date);
            EndDate.Value = DBNull.Value;
            //SqlParameter StartTime = new SqlParameter("@StartTime", SqlDbType.VarChar);
            //StartTime.Value = DBNull.Value;
            //SqlParameter EndTime = new SqlParameter("@EndTime", SqlDbType.VarChar);
            //EndTime.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetExamination]", ExaminationNameID, ExaminationName, StartDate, EndDate, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.ExaminationNameID = Convert.ToInt32(dr["ExaminationID"]);
                        item.ExaminationName = object.ReferenceEquals(dr["ExaminationName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ExaminationName"]);
                        //item.StartDate = object.ReferenceEquals(dr["StartDate"], DBNull.Value) ? string.Empty : Convert.ToString(dr["StartDate"]);
                        //item.EndDate = object.ReferenceEquals(dr["EndDate"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EndDate"]);
                        item.StartDate = Convert.ToDateTime(dr["StartDate"]);
                        item.EndDate = Convert.ToDateTime(dr["EndDate"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        item.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                    }
                }
            }
            return item;

        }
        #endregion

        #region Assessment

        public int SaveAssessment(AssessmentRequestDTO recordAttributer, string Operation)
        {
            SqlParameter AssessmentID = new SqlParameter("@AssessmentID", SqlDbType.Int);
            AssessmentID.Value = recordAttributer.AssessmentID;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = recordAttributer.CountryID;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.DepartmentID;
            SqlParameter AssessmentName = new SqlParameter("@AssessmentName", SqlDbType.VarChar);
            AssessmentName.Value = recordAttributer.AssessmentName;
            SqlParameter SyllabusVersion = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            SyllabusVersion.Value = recordAttributer.ProgrammeVersioningID;
            SqlParameter AssessmentType = new SqlParameter("@AssessmentType", SqlDbType.VarChar);
            AssessmentType.Value = recordAttributer.AssessmentType;
            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;
            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;
            SqlServerHelper.ExecuteNonQueryProc("[ln.Examination].[upSaveAssessment]", AssessmentID, countryID, FacultyCode, SyllabusVersion,
                AssessmentName, AssessmentType, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<AssessmentResponseDTO> GetAllAssessment()
        {
            var itemSet = new List<AssessmentResponseDTO>();

            SqlParameter AssessmentID = new SqlParameter("@AssessmentID", SqlDbType.Int);
            AssessmentID.Value = DBNull.Value;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = DBNull.Value;
            SqlParameter AssessmentName = new SqlParameter("@AssessmentName", SqlDbType.VarChar);
            AssessmentName.Value = DBNull.Value;
            SqlParameter SyllabusVersion = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            SyllabusVersion.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetAssessment]", AssessmentID, FacultyCode, SyllabusVersion, AssessmentName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new AssessmentResponseDTO()
                        {
                            AssessmentID = Convert.ToInt32(dr["AssessmentID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            CountryID = Convert.ToInt32(dr["CountryID"]),
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ProgrammeVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]),                           
                            AssessmentName = object.ReferenceEquals(dr["AssessmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentName"]),
                            AssessmentType = object.ReferenceEquals(dr["AssessmentType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentType"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"])
                        });

                    }
                }
            }
            return itemSet;

        }

        public AssessmentResponseDTO SelectAssessment(AssessmentRequestDTO recordAttributer)
        {
            var item = new AssessmentResponseDTO();

            SqlParameter AssessmentID = new SqlParameter("@AssessmentID", SqlDbType.Int);
            AssessmentID.Value = recordAttributer.AssessmentID;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = DBNull.Value;
            SqlParameter AssessmentName = new SqlParameter("@AssessmentName", SqlDbType.VarChar);
            AssessmentName.Value = DBNull.Value;
            SqlParameter SyllabusVersion = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            SyllabusVersion.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetAssessment]", AssessmentID, FacultyCode, SyllabusVersion, AssessmentName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.AssessmentID = Convert.ToInt32(dr["AssessmentID"]);
                        item.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        item.ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]);
                        item.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        item.CountryID = Convert.ToInt32(dr["CountryID"]);
                        item.ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
                        item.DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.ProgrammeVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        item.AssessmentName = object.ReferenceEquals(dr["AssessmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentName"]);
                        item.AssessmentType = object.ReferenceEquals(dr["AssessmentType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentType"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        item.Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        item.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);


                    };
                }
            }
            return item;

        }
        #endregion

        #region Examination Section

        public int SaveExaminationSection(ExaminationSectionRequestDTO recordAttributer, string Operation)
        {
            SqlParameter ExaminationSectionID = new SqlParameter("@ExaminationSectionID", SqlDbType.Int);
            ExaminationSectionID.Value = recordAttributer.ExaminationSectionID;
            SqlParameter ProgramCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            ProgramCode.Value = recordAttributer.ProgrammeVersioningID;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.FacultyCode;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = recordAttributer.CountryCode;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = recordAttributer.AcademicYearCode;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            SemisterCode.Value = recordAttributer.SemisterCode;
           
            SqlParameter CourseCode = new SqlParameter("@CourseID", SqlDbType.Int);
            CourseCode.Value = recordAttributer.CourseCode;
            SqlParameter SectionName = new SqlParameter("@SectionName", SqlDbType.VarChar);
            SectionName.Value = recordAttributer.SectionName;
            SqlParameter questionType = new SqlParameter("@QuestionType", SqlDbType.VarChar);
            questionType.Value = recordAttributer.QuestionType;
            SqlParameter maximumMarks = new SqlParameter("@MaximumMarks", SqlDbType.Decimal);
            maximumMarks.Value = recordAttributer.MaximumMarks;

            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;
            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Examination].[upSaveExaminationSection]",
                ExaminationSectionID, FacultyCode, ProgramCode,
                CountryCode, AcademicYearCode, SemisterCode,  CourseCode, SectionName,questionType,maximumMarks, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<ExaminationSectionResponseDTO> GetAllExaminationSection()
        {
            var itemSet = new List<ExaminationSectionResponseDTO>();

            SqlParameter ExaminationSectionID = new SqlParameter("@ExaminationSectionID", SqlDbType.Int);
            ExaminationSectionID.Value = DBNull.Value;
            SqlParameter ProgramCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            ProgramCode.Value = DBNull.Value;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = DBNull.Value;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = DBNull.Value;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = DBNull.Value;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            SemisterCode.Value = DBNull.Value;
            SqlParameter CourseCode = new SqlParameter("@CourseID", SqlDbType.Int);
            CourseCode.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetExaminationSection]", ExaminationSectionID, ProgramCode, FacultyCode, CountryCode, AcademicYearCode, SemisterCode, CourseCode, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new ExaminationSectionResponseDTO()
                        {
                            ExaminationSectionID = Convert.ToInt32(dr["ExaminationSectionID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            ProgramCode = Convert.ToInt32(dr["ProgrammeID"]),
                            FacultyCode = Convert.ToInt32(dr["DepartmentID"]),
                            ProgrammeVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]),
                            CountryCode = Convert.ToInt32(dr["CountryID"]),
                            CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            SectionName = object.ReferenceEquals(dr["SectionName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SectionName"]),
                            AcademicYearCode = Convert.ToInt32(dr["ProgrammeYear"]),
                            SemisterCode = Convert.ToInt32(dr["ProgrammeSemesterID"]),
                            SemisterName= object.ReferenceEquals(dr["ProgrammeSemester"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeSemester"]),
                            CourseID = Convert.ToInt32(dr["CourseID"]),
                            QuestionType = object.ReferenceEquals(dr["QuestionType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionType"]),
                            MaximumMarks = Convert.ToDecimal(dr["MaximumMarks"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        });

                    }
                }
            }
            return itemSet;

        }

        public ExaminationSectionResponseDTO SelectExaminationSection(ExaminationSectionRequestDTO recordAttributer)
        {
            var item = new ExaminationSectionResponseDTO();


            SqlParameter ExaminationSectionID = new SqlParameter("@ExaminationSectionID", SqlDbType.Int);
            ExaminationSectionID.Value = recordAttributer.ExaminationSectionID;
            SqlParameter ProgramCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            ProgramCode.Value = recordAttributer.ProgrammeVersioningID;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.FacultyCode;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = recordAttributer.CountryCode;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = recordAttributer.AcademicYearCode;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            SemisterCode.Value = recordAttributer.SemisterCode;
            SqlParameter CourseCode = new SqlParameter("@CourseID", SqlDbType.Int);
            CourseCode.Value = recordAttributer.CourseCode;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetExaminationSection]", ExaminationSectionID, ProgramCode, FacultyCode, CountryCode, AcademicYearCode, SemisterCode, CourseCode, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.ExaminationSectionID = Convert.ToInt32(dr["ExaminationSectionID"]);
                        item.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        item.ProgramCode = Convert.ToInt32(dr["ProgrammeID"]);
                        item.FacultyCode = Convert.ToInt32(dr["DepartmentID"]);
                        item.ProgrammeVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        item.CountryCode = Convert.ToInt32(dr["CountryID"]);
                        item.AcademicYearCode = Convert.ToInt32(dr["ProgrammeYear"]);
                        item.SemisterCode = Convert.ToInt32(dr["ProgrammeSemesterID"]);
                        item.SectionName = object.ReferenceEquals(dr["SectionName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SectionName"]);
                        item.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
                        item.Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        item.ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]);
                        item.SemisterName = object.ReferenceEquals(dr["ProgrammeSemester"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]);
                        item.CourseID = Convert.ToInt32(dr["CourseID"]);
                        item.MaximumMarks = Convert.ToDecimal(dr["MaximumMarks"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        item.QuestionType= object.ReferenceEquals(dr["QuestionType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["QuestionType"]);
                        item.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                    }
                }
            }
            return item;

        }
        #endregion

        #region Subject Assessment

        public int SaveSubjectAssessment(SubjectAssessmentRequestDTO recordAttributer, string Operation)
        {
            SqlParameter SubjectAssessmentID = new SqlParameter("@AssessmentConfigurationID", SqlDbType.Int);
            SubjectAssessmentID.Value = recordAttributer.SubjectAssessmentID;

           
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.FacultyCode;
            SqlParameter SyllabusVersionCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            SyllabusVersionCode.Value = recordAttributer.ProgrammeVersioningID;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = recordAttributer.CountryCode;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = recordAttributer.AcademicYearCode;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            SemisterCode.Value = recordAttributer.SemisterCode;
            SqlParameter CourseCode = new SqlParameter("@CourseID", SqlDbType.Int);
            CourseCode.Value = recordAttributer.CourseCode;
            SqlParameter AssessmentXML = new SqlParameter("@AssessmentXML", SqlDbType.Xml);
            AssessmentXML.Value = recordAttributer.TabAssessmentDetails;
            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Examination].[upSaveAssessmentConfiguration]", SubjectAssessmentID, FacultyCode, SyllabusVersionCode,
                CountryCode, AcademicYearCode, SemisterCode, CourseCode, AssessmentXML, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<SubjectAssessmentResponseDTO> GetAllSubjectAssessment()
        {
            var itemSet = new List<SubjectAssessmentResponseDTO>();

            SqlParameter SubjectAssessmentID = new SqlParameter("@AssessmentConfigurationID", SqlDbType.Int);
            SubjectAssessmentID.Value = DBNull.Value;

            SqlParameter ProgramCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            ProgramCode.Value = DBNull.Value;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = DBNull.Value;
           
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = DBNull.Value;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = DBNull.Value;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            SemisterCode.Value = DBNull.Value;
            SqlParameter CourseCode = new SqlParameter("@CourseID", SqlDbType.Int);
            CourseCode.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetAssessmentConfiguration]", SubjectAssessmentID, ProgramCode, FacultyCode,
                CountryCode, AcademicYearCode, SemisterCode, CourseCode, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new SubjectAssessmentResponseDTO()
                        {
                            SubjectAssessmentID = Convert.ToInt32(dr["AssessmentConfigurationID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            ProgramCode = Convert.ToInt32(dr["ProgrammeID"]),
                            FacultyCode = Convert.ToInt32(dr["DepartmentID"]),
                            ProgrammeVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            YearName = object.ReferenceEquals(dr["ProgrammeYear"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeYear"]),
                            SemisterName = object.ReferenceEquals(dr["ProgrammeSemester"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeSemester"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            CountryCode = Convert.ToInt32(dr["CountryID"]),
                            AcademicYearCode = Convert.ToInt32(dr["ProgrammeYear"]),
                            SemisterCode = Convert.ToInt32(dr["ProgrammeSemesterID"]),
                            CourseID = Convert.ToInt32(dr["CourseID"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        });

                    }
                }
            }
            return itemSet;

        }

        public SubjectAssessmentResponseDTO SelectSubjectAssessment(SubjectAssessmentRequestDTO recordAttributer)
        {
            var item = new SubjectAssessmentResponseDTO();
            SqlParameter AssessmentConfigurationDetailsID = new SqlParameter("@AssessmentConfigurationDetailsID", SqlDbType.Int);
            AssessmentConfigurationDetailsID.Value = DBNull.Value; 
            SqlParameter SubjectAssessmentID = new SqlParameter("@AssessmentConfigurationID", SqlDbType.Int);
            SubjectAssessmentID.Value = recordAttributer.SubjectAssessmentID;
            SqlParameter AssessmentName = new SqlParameter("@AssessmentName", SqlDbType.VarChar);
            AssessmentName.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            if (SubjectAssessmentID != null)
            {
                type.Value = "GET";
            }

            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetAssessmentConfigurationDetails]", AssessmentConfigurationDetailsID, SubjectAssessmentID, AssessmentName, type))
            {
                item.SubAssessmentDetails = new List<Response.SubjectAssessmentDetails>();
                
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.TabulalConfigurationDetails = new Response.SubjectAssessmentDetails();
                        item.TabulalConfigurationDetails.AssessmentName = object.ReferenceEquals(dr["AssessmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentName"]); ;
                        item.TabulalConfigurationDetails.AssessmentType = object.ReferenceEquals(dr["AssessmentType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentType"]); ;
                        item.TabulalConfigurationDetails.FullMarks = Convert.ToDecimal(dr["FullMarks"]);
                        item.TabulalConfigurationDetails.AssessmentPercentage = Convert.ToDecimal(dr["AssessmentPercentage"]);
                        item.TabulalConfigurationDetails.OpenClose = Convert.ToInt32(dr["OpenClose"]);
                        item.SubAssessmentDetails.Add(item.TabulalConfigurationDetails);
                    }
                }
            }
            return item;

        }
        #endregion

        #region Subject Allocation

        public int SaveSubjectAllocation(SubjectAllocationRequestDTO recordAttributer, string Operation)
        {
            SqlParameter SubjectAllocationID = new SqlParameter("@SubjectAllocationID", SqlDbType.Int);
            SubjectAllocationID.Value = recordAttributer.SubjectAllocationID;

            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.DepartmentID;

            SqlParameter ProgramCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            ProgramCode.Value = recordAttributer.ProgrammeVersioningID;

            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = recordAttributer.CountryID;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = recordAttributer.AcademicID;

            SqlParameter EmployeeID = new SqlParameter("@EmployeeID", SqlDbType.Int);
            EmployeeID.Value = recordAttributer.EmployeeID;
           
            SqlParameter allocationxml = new SqlParameter("@AllocationXML", SqlDbType.Xml);
            allocationxml.Value = recordAttributer.TabAllocationDetails;
            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;

            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;


            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Faculty].[upSaveSubjectAllocation]", SubjectAllocationID, EmployeeID, ProgramCode, FacultyCode,
                CountryCode, AcademicYearCode, allocationxml, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<SubjectAllocationResponseDTO> GetAllSubjectAllocation()
        {
            var itemSet = new List<SubjectAllocationResponseDTO>();

            SqlParameter SubjectAllocationID = new SqlParameter("@SubjectAllocationID", SqlDbType.Int);
            SubjectAllocationID.Value = DBNull.Value;
            SqlParameter DepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            DepartmentID.Value = DBNull.Value;
            SqlParameter ProgramCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            ProgramCode.Value = DBNull.Value;
            SqlParameter FacultyCode = new SqlParameter("@EmployeeID", SqlDbType.Int);
            FacultyCode.Value = DBNull.Value;
           
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = DBNull.Value;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetSubjectAllocation]", SubjectAllocationID, DepartmentID, ProgramCode,
                FacultyCode,
                CountryCode, AcademicYearCode, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new SubjectAllocationResponseDTO()
                        {
                            SubjectAllocationID = Convert.ToInt32(dr["SubjectAllocationID"]),
                            ProgrammeVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]),
                            FacultyCode = Convert.ToInt32(dr["DepartmentID"]),
                            EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                            AcademicID = Convert.ToInt32(dr["AcademicID"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]),
                            FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            YearName = object.ReferenceEquals(dr["ProgrammeYear"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeYear"]),
                            CountryCode = Convert.ToInt32(dr["CountryID"]),
                            AcademicYearCode = Convert.ToInt32(dr["ProgrammeYear"]),
                            Active = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),//by suvendu
                            EmployeeName = object.ReferenceEquals(dr["EmployeeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmployeeName"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        });

                    }
                }
            }
            return itemSet;

        }

        public SubjectAllocationResponseDTO SelectSubjectAllocation(SubjectAllocationRequestDTO recordAttributer)
        {
            var itemSet = new SubjectAllocationResponseDTO();

            SqlParameter SubjectAllocationID = new SqlParameter("@SubjectAllocationID", SqlDbType.Int);
            SubjectAllocationID.Value = recordAttributer.SubjectAllocationID;
            SqlParameter DepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            DepartmentID.Value = recordAttributer.DepartmentID;
            SqlParameter ProgramCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            ProgramCode.Value = recordAttributer.ProgrammeVersioningID;
            SqlParameter FacultyCode = new SqlParameter("@EmployeeID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.EmployeeID;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = recordAttributer.CountryID;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = recordAttributer.AcademicID;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";

            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetSubjectAllocation]", SubjectAllocationID, DepartmentID, ProgramCode, 
                FacultyCode, 
                CountryCode, AcademicYearCode, type))
            {
                itemSet.SubAllocationList = new List<Response.SubjectAllocationListR>();

                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        itemSet.ProgrammeVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        itemSet.AcademicID = Convert.ToInt32(dr["AcademicID"]);
                        itemSet.AcademicName = object.ReferenceEquals(dr["AcademicName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AcademicName"]);
                        itemSet.FacultyCode = Convert.ToInt32(dr["DepartmentID"]);
                        itemSet.FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        itemSet.AcademicYearCode = Convert.ToInt32(dr["ProgrammeYear"]);
                        itemSet.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                        itemSet.EmployeeName = object.ReferenceEquals(dr["EmployeeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmployeeName"]);
                        itemSet.CountryCode = Convert.ToInt32(dr["CountryID"]);
                        itemSet.SubjectAllocationID = Convert.ToInt32(dr["SubjectAllocationID"]);
                        itemSet.ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        itemSet.Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        itemSet.YearName = object.ReferenceEquals(dr["ProgrammeYear"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeYear"]);
                        itemSet.Active = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);//By suvendu

                    }
                }
            }

            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetSubjectAllocationDetails]", SubjectAllocationID))
            {


                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.AllocationList = new Response.SubjectAllocationListR();
                        itemSet.AllocationList.SemisterName = "";
                        var gfy = itemSet.SubAllocationList.Any(x => x.SemisterID == Convert.ToInt32(dr["ProgrammeSemesterID"]));
                        if (gfy == false)
                        {
                            var gdt = Convert.ToInt32(dr["ProgrammeSemesterID"]);
                            itemSet.AllocationList.SemisterID = gdt;
                            itemSet.AllocationList.SemisterName = object.ReferenceEquals(dr["ProgrammeSemester"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeSemester"]); ;
                            itemSet.SubAllocationList.Add(itemSet.AllocationList);
                        }
                    }
                }
            }
            itemSet.AllocationList.SubAllocationDetailsList = new List<SubjectAllocationDetailsListR>();
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetSubjectAllocationDetails]", SubjectAllocationID))
            {

                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.AllocationList.AllocationDetails = new Response.SubjectAllocationDetailsListR();
                        itemSet.AllocationList.AllocationDetails.CourseName = "";
                        itemSet.AllocationList.AllocationDetails.ProgrammeSemesterID = Convert.ToInt32(dr["ProgrammeSemesterID"]);
                        itemSet.AllocationList.AllocationDetails.CourseID = Convert.ToInt32(dr["CourseID"]);
                        itemSet.AllocationList.AllocationDetails.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
                        itemSet.AllocationList.AllocationDetails.CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]); ;
                        itemSet.AllocationList.SubAllocationDetailsList.Add(itemSet.AllocationList.AllocationDetails);
                    }
                }


            }
            return itemSet;

        }
        #endregion

        #region Tracher Courses

        public List<FacultyDashboardResponseDTO> GetAllFacultyCourse(FacultyDashboardRequestDTO recordAttributer)
        {
            var itemSet = new List<FacultyDashboardResponseDTO>();
            SqlParameter EmployeeID = new SqlParameter("@EmployeeID", SqlDbType.Int);
            EmployeeID.Value = recordAttributer.EmployeeID;
            SqlParameter CourseID = new SqlParameter("@CourseID", SqlDbType.Int);
            CourseID.Value = recordAttributer.CourseID;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Faculty].[upGetQuestionCourse]", CourseID, EmployeeID, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new FacultyDashboardResponseDTO()
                        {
                            EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                            EmployeeName = object.ReferenceEquals(dr["EmployeeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmployeeName"]),
                            NoOfQuestion = Convert.ToInt32(dr["NoOfQuestion"]),
                            CourseID = Convert.ToInt32(dr["CourseID"]),
                            CourseCode = object.ReferenceEquals(dr["CourseCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseCode"]),
                            CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]),
                            SectionInQuestion= object.ReferenceEquals(dr["SectionInQuestion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SectionInQuestion"]),
                            //SectionName = object.ReferenceEquals(dr["SectionName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SectionName"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            SubjectAllocationDetailsID = Convert.ToInt32(dr["SubjectAllocationDetailsID"]),
                            SubjectAllocationID = Convert.ToInt32(dr["SubjectAllocationID"]),

                        });

                    }
                }
            }
            return itemSet;

        }

        #endregion


        #region Online Exam Application 

        public List<OnlineExamAppResponseDTO> GetAllOnlineExamApp(AdminOnlineExamRequestDTO request)
        {
            var itemSet = new List<OnlineExamAppResponseDTO>();

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = request.CourseID;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = request.DepartmentID;
            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = request.ProgrammeID;
            SqlParameter programmeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programmeVersioningID.Value = request.ProgrammeVersioningID;
            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = request.ProgrammeSemesterID;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = request.CountryID;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Admin].[upGetExamination]", courseID, departmentID, programmeID, programmeVersioningID,
                programmeSemesterID, countryID, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new OnlineExamAppResponseDTO()
                        {
                            StudentID = Convert.ToInt32(dr["StudentID"]),
                            StudentName = object.ReferenceEquals(dr["StudentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["StudentName"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        });

                    }
                }
            }
            return itemSet;
        }

        #endregion


        #region Online Exam Evaluation 

        public List<OnlineExamAppResponseDTO> GetAllOnlineExamEvaluation(AdminOnlineExamRequestDTO request)
        {
            var itemSet = new List<OnlineExamAppResponseDTO>();

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = request.CourseID;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = request.DepartmentID;
            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = request.ProgrammeID;
            SqlParameter programmeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programmeVersioningID.Value = request.ProgrammeVersioningID;
            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = request.ProgrammeSemesterID;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = request.CountryID;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Admin].[upGetEvaluation]",courseID,departmentID,programmeID,programmeVersioningID,
                programmeSemesterID,countryID, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new OnlineExamAppResponseDTO()
                        {
                            StudentID = Convert.ToInt32(dr["StudentID"]),                            
                            StudentName = object.ReferenceEquals(dr["StudentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["StudentName"]),
                            EmployeeName = object.ReferenceEquals(dr["EmployeeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["EmployeeName"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        });

                    }
                }
            }
            return itemSet;
        }

        #endregion

        #region Online Exam Schedule

        public List<OnlineExamAppResponseDTO> GetAllOnlineExamSchedule(AdminOnlineExamRequestDTO request)
        {
            var itemSet = new List<OnlineExamAppResponseDTO>();

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = request.CourseID;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = request.DepartmentID;
            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = request.ProgrammeID;
            SqlParameter programmeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programmeVersioningID.Value = request.ProgrammeVersioningID;
            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = request.ProgrammeSemesterID;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = request.CountryID;
            SqlParameter employeeID = new SqlParameter("@EmployeeID", SqlDbType.Int);
            employeeID.Value = request.EmployeeID;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Admin].[upGetScheduling]", courseID, departmentID, programmeID, programmeVersioningID,
                programmeSemesterID, countryID,employeeID, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new OnlineExamAppResponseDTO()
                        {
                            StudentID = Convert.ToInt32(dr["StudentID"]),
                            StudentName = object.ReferenceEquals(dr["StudentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["StudentName"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        });

                    }
                }
            }
            return itemSet;
        }

        #endregion

        #region Online Exam Result

        public List<OnlineExamAppResponseDTO> GetAllOnlineExamResult(AdminOnlineExamRequestDTO request)
        {
            var itemSet = new List<OnlineExamAppResponseDTO>();

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = request.CourseID;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = request.DepartmentID;
            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = request.ProgrammeID;
            SqlParameter programmeVersioningID = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            programmeVersioningID.Value = request.ProgrammeVersioningID;
            SqlParameter programmeSemesterID = new SqlParameter("@ProgrammeSemesterID", SqlDbType.Int);
            programmeSemesterID.Value = request.ProgrammeSemesterID;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = request.CountryID;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Admin].[upGetResult]", courseID, departmentID, programmeID, programmeVersioningID,
                programmeSemesterID, countryID, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new OnlineExamAppResponseDTO()
                        {
                            StudentID = Convert.ToInt32(dr["StudentID"]),
                            StudentName = object.ReferenceEquals(dr["StudentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["StudentName"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        });

                    }
                }
            }
            return itemSet;
        }

        #endregion

        #region Save All online Examination  Application 

        public int SaveExaminationConfiguration(AdminOnlineExamRequestDTO recordAttributer, string Operation)
        {

            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = recordAttributer.CourseID;

            SqlParameter examinationXML = new SqlParameter("@ExaminationXML", SqlDbType.Xml);
            examinationXML.Value = recordAttributer.ExaminationXML;

            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
           
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;

            SqlParameter dBStatus = new SqlParameter("@DBStatus", SqlDbType.Char);
            dBStatus.Value = string.Empty;

            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Admin].[upSaveExaminationConfiguration]", courseID, examinationXML, createdBy, type, dBStatus, status);

            return Convert.ToInt32(status.Value);

        }

        #endregion

    }
}
