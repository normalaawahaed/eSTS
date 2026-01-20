using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Apps.Common
{
    public class DALLogDB
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion

        public LogDB _BL = new LogDB();
        const string TableName = "log";

        public DALLogDB()
        { sqlCon.ConnectionString = connectionstring; }

        public bool Load()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT log_seq, log_activity_type, log_activity, log_datetime, log_remark, logger FROM [log]";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, TableName);
                    cmd.Connection.Close();
                    lPass = true;
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return lPass;
        }

        public bool LoadByActivityType(StandardDefinition.LogType ActivityType)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT log_seq, log_activity_type, log_activity, log_datetime, log_remark, logger FROM [log] " +
                    "WHERE log_activity_type='" + ActivityType.ToString() + "' " +
                    "ORDER BY log_seq";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, TableName);
                    cmd.Connection.Close();
                    lPass = true;
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return lPass;
        }

        public bool Insert()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "INSERT INTO [log] (log_activity_type, log_activity, log_datetime, log_remark, logger) " +
                    "VALUES ('" + _BL.LogActivityType + "', '" + _BL.LogActivity + "', '" + _BL.LogDatetime.ToString("yyyy/MM/dd HH:mm:ss") + "', " +
                    "'" + _BL.LogRemark + "', '" + _BL.Logger + "')";

                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.CommandText = querystring;
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();

                int affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
                cmd.Connection.Close();
                if (affectedRows > 0)
                    lPass = true;
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return lPass;
        }
    }
}
