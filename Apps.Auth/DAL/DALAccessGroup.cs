using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Apps.Auth
{
    public class DALAccessGroup
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion

        public AccessGroup _BL = new AccessGroup();
        const string TableName = "access_group";

        public DALAccessGroup()
        { sqlCon.ConnectionString = connectionstring; }

        public bool Load()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT access_group_id, description, is_admin, is_active,is_wibs,is_wcms,is_khairat FROM access_group";

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

        public bool LoadActive()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT access_group_id, description,is_wibs,is_wcms,is_khairat, is_admin, is_active FROM access_group WHERE is_active=1";

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

        public bool LoadGroup(string group_id)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT access_group_id, description,is_wibs,is_wcms,is_khairat, is_admin, is_active FROM access_group WHERE access_group_id='"+group_id+"'";

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
        public bool CheckIsAdminUser(string AccessGroupId)
        {
            object obj = null;
            int iResult = 0;
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT COUNT(*) FROM access_group " +
                    "WHERE is_active=1 AND is_admin=1 AND access_group_id='" + AccessGroupId + "'";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    obj = cmd.ExecuteScalar();
                    cmd.Connection.Close();
                    if ((obj != null) && !obj.GetType().ToString().Equals("System.DBNull"))
                        iResult = Convert.ToInt32(obj);

                    if (iResult > 0)
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
                querystring = "INSERT INTO access_group (access_group_id, description,is_wibs,is_wcms,is_khairat, is_admin, is_active) " +
                    "VALUES ('" + _BL.AccessGroupId + "', '" + _BL.Description + "', '" + _BL.IsWIBS + "', '" + _BL.IsWCMS + "', '" + _BL.IsKhairat + "', '" + _BL.IsAdmin + "', '" + _BL.IsActive + "')";

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

        public bool Update()
        {
            object obj = null;
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "UPDATE access_group " +
                    "SET description = '" + _BL.Description + "', is_wibs = '" + _BL.IsWIBS + "', is_wcms = '" + _BL.IsWCMS + "', is_khairat = '" + _BL.IsKhairat + "', is_admin = '" + _BL.IsAdmin + "',  is_active = '" + _BL.IsActive + "' " +
                    "WHERE access_group_id = '" + _BL.AccessGroupId + "'";

                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.CommandText = querystring;
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();

                obj = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
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