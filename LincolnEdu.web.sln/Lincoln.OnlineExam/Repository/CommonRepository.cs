﻿using System;
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
                            DepartmentCode = Convert.ToInt32(dr["DepartmentID"]),
                            ProgramCode = Convert.ToInt32(dr["ProgrammeID"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            //PlaceHolder = object.ReferenceEquals(dr["SyllabusVersion"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SyllabusVersion"]),
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
            //SqlParameter placeHolder = new SqlParameter("@SyllabusVersion", SqlDbType.VarChar);
            //placeHolder.Value = DBNull.Value;
            //SqlParameter credit = new SqlParameter("@Credit", SqlDbType.VarChar);
            //credit.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetProgrammeVersioning]", programVersioningID, programCode, departmentCode, version, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.ProgramVersioningID = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        item.ProgramCode = Convert.ToInt32(dr["ProgrammeID"]);
                        item.Version = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        item.ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.DepartmentCode = Convert.ToInt32(dr["DepartmentID"]);
                        item.DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
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
                            CountryID = Convert.ToInt32(dr["CountryID"]),
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
            programmeSemesterID.Value = recordAttributer.ProgrammeSemesterID;
            SqlParameter academicID = new SqlParameter("@AcademicID", SqlDbType.Int);
            academicID.Value = DBNull.Value;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = recordAttributer.DepartmentID;

            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = recordAttributer.ProgrammeID;
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
                        item.CountryID = Convert.ToInt32(dr["CountryID"]);
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


        #region Course


        public int SaveCourse(CourseRequestDTO recordAttributer, string Operation)
        {
            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = recordAttributer.CourseID;
            SqlParameter departmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            departmentID.Value = recordAttributer.DepartmentID;
            SqlParameter programmeID = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programmeID.Value = recordAttributer.ProgrammeID;
            SqlParameter countryID = new SqlParameter("@CountryID", SqlDbType.Int);
            countryID.Value = recordAttributer.CountryID;
            SqlParameter programmeYear = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            programmeYear.Value = recordAttributer.ProgrammeYear;
            SqlParameter programmeSemester = new SqlParameter("@ProgrammeSemester", SqlDbType.Int);
            programmeSemester.Value = recordAttributer.ProgrammeSemester;
            SqlParameter semesterType = new SqlParameter("@SemesterType", SqlDbType.VarChar);
            semesterType.Value = recordAttributer.SemesterType;
            SqlParameter courseCode = new SqlParameter("@CourseCode", SqlDbType.VarChar);
            courseCode.Value = recordAttributer.CourseCode;
            SqlParameter courseName = new SqlParameter("@CourseName", SqlDbType.VarChar);
            courseName.Value = recordAttributer.CourseName;
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

            SqlServerHelper.ExecuteNonQueryProc("[ln.Master].[upSaveCourse]", courseID, departmentID, programmeID, countryID, programmeYear,
                programmeSemester, semesterType, courseCode, courseName, approvalNo, credit, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<CourseResponseDTO> GetAllCourse()
        {
            var itemSet = new List<CourseResponseDTO>();


            SqlParameter courseID = new SqlParameter("@CourseID", SqlDbType.Int);
            courseID.Value = DBNull.Value;
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
            SqlParameter programmeSemester = new SqlParameter("@ProgrammeSemester", SqlDbType.Int);
            programmeSemester.Value = DBNull.Value;

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
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetCourse]", courseID, academicID, departmentID, programmeID,
                countryID, programmeYear, programmeSemester, courseCode, courseName, approvalNo, credit, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new CourseResponseDTO()
                        {
                            CourseID = Convert.ToInt32(dr["CourseID"]),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            ApprovalNo = object.ReferenceEquals(dr["ApprovalNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ApprovalNo"]),
                            Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]),
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            ProgrammeSemester = Convert.ToInt32(dr["ProgrammeSemester"]),
                            ProgrammeYear = Convert.ToInt32(dr["ProgrammeYear"]),
                            SemesterType = object.ReferenceEquals(dr["SemesterType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SemesterType"]),
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
            courseID.Value = DBNull.Value;
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
            SqlParameter programmeSemester = new SqlParameter("@ProgrammeSemester", SqlDbType.Int);
            programmeSemester.Value = DBNull.Value;

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
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Master].[upGetCourse]", courseID, academicID, departmentID, programmeID,
                countryID, programmeYear, programmeSemester, courseCode, courseName, approvalNo, credit, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        item.CourseID = Convert.ToInt32(dr["CourseID"]);
                        item.CountryId = Convert.ToInt32(dr["CountryId"]);
                        item.ApprovalNo = object.ReferenceEquals(dr["ApprovalNo"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ApprovalNo"]);
                        item.Credit = object.ReferenceEquals(dr["Credit"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Credit"]);
                        item.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        item.DepartmentName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]);
                        item.ProgrammeName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.ProgrammeSemester = Convert.ToInt32(dr["ProgrammeSemester"]);
                        item.ProgrammeYear = Convert.ToInt32(dr["ProgrammeYear"]);
                        item.SemesterType = object.ReferenceEquals(dr["SemesterType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SemesterType"]);
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

        #region Examination Name

        public int SaveExaminationName(ExaminationNameRequestDTO recordAttributer, string Operation)
        {
            CultureInfo culture = new CultureInfo("en-US");
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
            SqlParameter programCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programCode.Value = recordAttributer.ProgramCode;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.FacultyCode;
            SqlParameter AssessmentName = new SqlParameter("@AssessmentName", SqlDbType.VarChar);
            AssessmentName.Value = recordAttributer.AssessmentName;
            SqlParameter SyllabusVersion = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            SyllabusVersion.Value = recordAttributer.SyllabusVersion;
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
            SqlServerHelper.ExecuteNonQueryProc("[ln.Examination].[upSaveAssessment]", AssessmentID, programCode, FacultyCode, SyllabusVersion,
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
            SqlParameter programCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programCode.Value = DBNull.Value;
            SqlParameter AssessmentName = new SqlParameter("@AssessmentName", SqlDbType.VarChar);
            AssessmentName.Value = DBNull.Value;
            //SqlParameter AssessmentType = new SqlParameter("@AssessmentType", SqlDbType.VarChar);
            //AssessmentType.Value = DBNull.Value;
            SqlParameter SyllabusVersion = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            SyllabusVersion.Value = DBNull.Value;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetAssessment]", AssessmentID, FacultyCode, programCode, SyllabusVersion, AssessmentName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemSet.Add(new AssessmentResponseDTO()
                        {
                            AssessmentID = Convert.ToInt32(dr["AssessmentID"]),
                            ProgramCode = Convert.ToInt32(dr["ProgrammeID"]),
                            FacultyCode = Convert.ToInt32(dr["DepartmentID"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            SyllabusVersionCode = Convert.ToInt32(dr["ProgrammeVersioningID"]),
                            SyllabusVersionName = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            AssessmentName = object.ReferenceEquals(dr["AssessmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentName"]),
                            AssessmentType = object.ReferenceEquals(dr["AssessmentType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentType"]),
                            Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]),
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
            SqlParameter programCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            programCode.Value = DBNull.Value;
            SqlParameter AssessmentName = new SqlParameter("@AssessmentName", SqlDbType.VarChar);
            AssessmentName.Value = DBNull.Value;
            //SqlParameter AssessmentType = new SqlParameter("@AssessmentType", SqlDbType.VarChar);
            //AssessmentType.Value = DBNull.Value;
            SqlParameter SyllabusVersion = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            SyllabusVersion.Value = DBNull.Value;


            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = "GET";
            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("[ln.Examination].[upGetAssessment]", AssessmentID, programCode, FacultyCode, SyllabusVersion, AssessmentName, type))
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item.AssessmentID = Convert.ToInt32(dr["AssessmentID"]);
                        item.ProgramCode = Convert.ToInt32(dr["ProgrammeID"]);
                        item.FacultyCode = Convert.ToInt32(dr["DepartmentID"]);
                        item.ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.SyllabusVersionCode = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        item.SyllabusVersionName = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        item.AssessmentName = object.ReferenceEquals(dr["AssessmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentName"]);
                        item.AssessmentType = object.ReferenceEquals(dr["AssessmentType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["AssessmentType"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
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
            SqlParameter ProgramCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            ProgramCode.Value = recordAttributer.ProgramCode;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.FacultyCode;
            SqlParameter SyllabusVersionCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            SyllabusVersionCode.Value = recordAttributer.SyllabusVersionCode;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = recordAttributer.CountryCode;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = recordAttributer.AcademicYearCode;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemester", SqlDbType.Int);
            SemisterCode.Value = recordAttributer.SemisterCode;
            SqlParameter SectionName = new SqlParameter("@SectionName", SqlDbType.VarChar);
            SectionName.Value = recordAttributer.SectionName;
            SqlParameter CourseCode = new SqlParameter("@CourseID", SqlDbType.Int);
            CourseCode.Value = recordAttributer.CourseCode;
            SqlParameter active = new SqlParameter("@Active", SqlDbType.Char);
            active.Value = recordAttributer.Active;
            SqlParameter createdBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            createdBy.Value = recordAttributer.CreatedBy;
            SqlParameter type = new SqlParameter("@Type", SqlDbType.Char);
            type.Value = Operation;
            SqlParameter status = new SqlParameter("@Status", SqlDbType.Int);
            status.Value = 0;
            status.Direction = ParameterDirection.InputOutput;

            SqlServerHelper.ExecuteNonQueryProc("[ln.Examination].[upSaveExaminationSection]", ExaminationSectionID, ProgramCode, FacultyCode, SyllabusVersionCode,
                CountryCode, AcademicYearCode, SemisterCode, SectionName, CourseCode, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<ExaminationSectionResponseDTO> GetAllExaminationSection()
        {
            var itemSet = new List<ExaminationSectionResponseDTO>();

            SqlParameter ExaminationSectionID = new SqlParameter("@ExaminationSectionID", SqlDbType.Int);
            ExaminationSectionID.Value = DBNull.Value;
            SqlParameter ProgramCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            ProgramCode.Value = DBNull.Value;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = DBNull.Value;
            //SqlParameter SyllabusVersionCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            //SyllabusVersionCode.Value = DBNull.Value;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = DBNull.Value;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = DBNull.Value;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemester", SqlDbType.Int);
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
                            ProgramCode = Convert.ToInt32(dr["ProgrammeID"]),
                            FacultyCode = Convert.ToInt32(dr["DepartmentID"]),
                            SyllabusVersionCode = Convert.ToInt32(dr["ProgrammeVersioningID"]),
                            CountryCode = Convert.ToInt32(dr["CountryID"]),
                            CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            SyllabusVersionName = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            //CountryName = object.ReferenceEquals(dr["CountryID"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseCode"]),
                            SectionName = object.ReferenceEquals(dr["SectionName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SectionName"]),
                            AcademicYearCode = Convert.ToInt32(dr["ProgrammeYear"]),
                            SemisterCode = Convert.ToInt32(dr["ProgrammeSemester"]),
                            //SemisterName= object.ReferenceEquals(dr["SemesterType"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SemesterType"]),
                            CourseID = Convert.ToInt32(dr["CourseID"]),
                            //CourseCode = object.ReferenceEquals(dr["CourseCode"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseCode"]),                          
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
            SqlParameter ProgramCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            ProgramCode.Value = recordAttributer.ProgramCode;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.FacultyCode;
            //SqlParameter SyllabusVersionCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            //SyllabusVersionCode.Value = DBNull.Value;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = recordAttributer.CountryCode;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = recordAttributer.AcademicYearCode;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemester", SqlDbType.Int);
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
                        item.ProgramCode = Convert.ToInt32(dr["ProgrammeID"]);
                        item.FacultyCode = Convert.ToInt32(dr["DepartmentID"]);
                        item.SyllabusVersionCode = Convert.ToInt32(dr["ProgrammeVersioningID"]);
                        item.CountryCode = Convert.ToInt32(dr["CountryID"]);
                        item.AcademicYearCode = Convert.ToInt32(dr["ProgrammeYear"]);
                        item.SemisterCode = Convert.ToInt32(dr["ProgrammeSemester"]);
                        item.SectionName = object.ReferenceEquals(dr["SectionName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["SectionName"]);
                        item.SyllabusVersionName = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]);
                        item.ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]);
                        item.FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]);
                        item.CourseName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]);
                        item.SemisterName = object.ReferenceEquals(dr["CourseName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["CourseName"]);
                        item.CourseID = Convert.ToInt32(dr["CourseID"]);
                        item.Status = object.ReferenceEquals(dr["Status"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Status"]);
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

            SqlParameter ProgramCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            ProgramCode.Value = recordAttributer.ProgramCode;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = recordAttributer.FacultyCode;
            SqlParameter SyllabusVersionCode = new SqlParameter("@ProgrammeVersioningID", SqlDbType.Int);
            SyllabusVersionCode.Value = recordAttributer.SyllabusVersionCode;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = recordAttributer.CountryCode;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = recordAttributer.AcademicYearCode;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemester", SqlDbType.Int);
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

            SqlServerHelper.ExecuteNonQueryProc("[ln.Examination].[upSaveAssessmentConfiguration]", SubjectAssessmentID, ProgramCode, FacultyCode, SyllabusVersionCode,
                CountryCode, AcademicYearCode, SemisterCode, CourseCode, AssessmentXML, active, createdBy, type, status);

            return Convert.ToInt32(status.Value);

        }
        public List<SubjectAssessmentResponseDTO> GetAllSubjectAssessment()
        {
            var itemSet = new List<SubjectAssessmentResponseDTO>();

            SqlParameter SubjectAssessmentID = new SqlParameter("@AssessmentConfigurationID", SqlDbType.Int);
            SubjectAssessmentID.Value = DBNull.Value;

            SqlParameter ProgramCode = new SqlParameter("@ProgrammeID", SqlDbType.Int);
            ProgramCode.Value = DBNull.Value;
            SqlParameter FacultyCode = new SqlParameter("@DepartmentID", SqlDbType.Int);
            FacultyCode.Value = DBNull.Value;
            //SqlParameter AssessmentName = new SqlParameter("@AssessmentName", SqlDbType.VarChar);
            //AssessmentName.Value = DBNull.Value;
            SqlParameter CountryCode = new SqlParameter("@CountryID", SqlDbType.Int);
            CountryCode.Value = DBNull.Value;
            SqlParameter AcademicYearCode = new SqlParameter("@ProgrammeYear", SqlDbType.Int);
            AcademicYearCode.Value = DBNull.Value;
            SqlParameter SemisterCode = new SqlParameter("@ProgrammeSemester", SqlDbType.Int);
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
                            ProgramCode = Convert.ToInt32(dr["ProgrammeID"]),
                            FacultyCode = Convert.ToInt32(dr["DepartmentID"]),
                            SyllabusVersionCode = Convert.ToInt32(dr["ProgrammeVersioningID"]),
                            ProgramName = object.ReferenceEquals(dr["ProgrammeName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeName"]),
                            FacultyName = object.ReferenceEquals(dr["DepartmentName"], DBNull.Value) ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                            //SyllabusVersionName = object.ReferenceEquals(dr["Version"], DBNull.Value) ? string.Empty : Convert.ToString(dr["Version"]),
                            YearName = object.ReferenceEquals(dr["ProgrammeYear"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeYear"]),
                            SemisterName = object.ReferenceEquals(dr["ProgrammeSemester"], DBNull.Value) ? string.Empty : Convert.ToString(dr["ProgrammeSemester"]),
                            CountryCode = Convert.ToInt32(dr["CountryID"]),
                            AcademicYearCode = Convert.ToInt32(dr["ProgrammeYear"]),
                            SemisterCode = Convert.ToInt32(dr["ProgrammeSemester"]),
                            CourseCode = Convert.ToInt32(dr["CourseID"]),
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
            AssessmentConfigurationDetailsID.Value = DBNull.Value; //recordAttributer.SubAssessmentDetails.ElementAt(0).AssessmentConfigurationDetailsID;
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
                        //item.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                        item.SubAssessmentDetails.Add(item.TabulalConfigurationDetails);
                    }
                }
            }
            return item;

        }
        #endregion
    }
}
