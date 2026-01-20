using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace eSTS.DAL
{
    public class DALAccess
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion


        public DataSet GetAccessGroupModule()
        {
            try
            {
                string querystring = null;
                querystring = "Select * from v_AccessGroupModule  Order By lvl0seq  ";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "AccessGroupModule");
                    cmd.Connection.Close();
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

            return ds;
        }
        public int CheckExist(string access_group_id, string access_module_id)
        {
            int result = 0;
            try
            {
                string querystring = null;
                querystring = " Select COUNT(accessgroupid)as ttl from accessgroupmodule where accessgroupid='" + access_group_id + "' and moduleid = '" + access_module_id + "'";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Connection.Close();
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

            return result;
        }
    }
  
}