using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Apps.Auth
{
    public class DALAuditTrail
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion

        //public AccessGroup _BL = new AccessGroup();
        const string TableName = "log";

        public DALAuditTrail()
        { sqlCon.ConnectionString = connectionstring; }

        public bool Load(string Query)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT [log_activity_type], [log_activity], [log_datetime], [log_remark], " +
                              "(user_id + ' - ' + full_name) as logger " +
                              "FROM [log] LEFT OUTER JOIN users ON logger=user_id " + Query;

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
    }
}