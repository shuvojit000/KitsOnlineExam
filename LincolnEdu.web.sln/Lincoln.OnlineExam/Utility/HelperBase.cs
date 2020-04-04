﻿using System;
using System.Data;
using System.Data.Common;

namespace Lincoln.OnlineExam.Utility
{
    public  class HelperBase : IDisposable
    {
        internal bool isOpen = false;
        DbConnection conn;
        DbCommand cmd;

        public virtual DbConnection Connection { get { return conn; } set { conn = value; } }
        public DbCommand Command { get { return cmd; } set { cmd = value; } }

        string connection_str;

        public string ConnectionString
        {
            get
            {
                return connection_str;
            }
            set
            {
                connection_str = value;
            }
        }


        public int ExecuteStoredProcedure(string StoredProcedureName)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoredProcedureName;
            return cmd.ExecuteNonQuery();
        }

        public int ExecuteNoneQuery()
        {
            return cmd.ExecuteNonQuery();
        }

        public string ExecuteScalarString()
        {
            return cmd.ExecuteScalar().ToString();
        }

        public int ExecuteScalarInt()
        {
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable ReadTable(DbCommand cmd)
        {
            DataTable dt = new DataTable();
            DbDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                int fieldc = reader.FieldCount;
                for (int i = 0; i < fieldc; i++)
                {
                    DataColumn dc = new DataColumn(reader.GetName(i), reader.GetFieldType(i));
                    dt.Columns.Add(dc);
                }
                while (reader.Read())
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < fieldc; i++)
                    {
                        dr[i] = reader[i];
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        public DataTable ReadTable()
        {
            return HelperBase.ReadTable(cmd);
            /*
                DataTable dt=new DataTable();
            DbDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                int fieldc=reader.FieldCount;
                for (int i = 0; i < fieldc; i++)
                {
                    DataColumn dc = new DataColumn(reader.GetName(i), reader.GetFieldType(i));
                    dt.Columns.Add(dc);
                }
                while (reader.Read())
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < fieldc; i++)
                    {
                        dr[i] = reader[i];
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            finally
            {
                if (reader != null) reader.Close();
            }*/
        }

        public virtual void Open()
        {
            conn.ConnectionString = ConnectionString;
            conn.Open();
            isOpen = true;
        }

        public virtual void Close()
        {
            if (isOpen && conn != null)
            {
                conn.Close();
            }
        }
        public static bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
        public void Dispose()
        {
            Close();
        }
    }
}
