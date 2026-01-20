using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Apps.Auth
{
    public class DALGroupModule
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion

        public GroupModule _BL = new GroupModule();
        const string TableName = "access_group_module";

        public DALGroupModule()
        { sqlCon.ConnectionString = connectionstring; }

        public bool Load()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT access_group_id, access_module_id FROM access_group_module";

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

        public bool LoadByGroup()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT access_group_id, access_module_id FROM access_group_module WHERE access_group_id='" + _BL.AccessGroupId + "'";

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

        //public bool Save()
        //{
        //    try
        //    {
        //        if (CheckExists())
        //            return Update();
        //        else
        //            return Insert();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public bool CheckExists()
        {
            object obj = null;
            bool lPass = false;
            int iResult = 0;

            try
            {
                string querystring = null;
                querystring = "SELECT COUNT(*) FROM access_group_module " +
                    "WHERE access_group_id = @access_group_id AND access_module_id = @access_module_id";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@access_group_id", _BL.AccessGroupId);
                    cmd.Parameters.AddWithValue("@access_module_id", _BL.AccessModuleId);

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

        //public bool Insert()
        //{
        //    bool lPass = false;

        //    try
        //    {
        //        string querystring = null;
        //        querystring = "INSERT INTO access_group_module (access_group_id, access_module_id, access_add, access_edit, access_delete) " +
        //            "VALUES (@access_group_id, @access_module_id, @access_add, @access_edit, @access_delete)";

        //        cmd.Connection = sqlCon;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@access_group_id", _BL.AccessGroupId);
        //        cmd.Parameters.AddWithValue("@access_module_id", _BL.AccessModuleId);
        //        cmd.Parameters.AddWithValue("@access_add", _BL.AccessAdd);
        //        cmd.Parameters.AddWithValue("@access_edit", _BL.AccessEdit);
        //        cmd.Parameters.AddWithValue("@access_delete", _BL.AccessDelete);

        //        cmd.CommandText = querystring;
        //        cmd.CommandTimeout = 0;
        //        cmd.Connection.Open();

        //        int affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
        //        cmd.Connection.Close();
        //        if (affectedRows > 0)
        //            lPass = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        if ((cmd.Connection.State == ConnectionState.Open))
        //        {
        //            cmd.Connection.Close();
        //        }
        //        throw;
        //    }

        //    return lPass;
        //}

        public bool Insert()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "INSERT INTO access_group_module (access_group_id, access_module_id) " +
                    "VALUES (@access_group_id, @access_module_id)";

                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@access_group_id", _BL.AccessGroupId);
                cmd.Parameters.AddWithValue("@access_module_id", _BL.AccessModuleId);

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

        //public bool Update()
        //{
        //    bool lPass = false;

        //    try
        //    {
        //        string querystring = null;
        //        querystring = "UPDATE access_group_module SET access_add=@access_add, access_edit=@access_edit, access_delete=@access_delete " +
        //            "WHERE access_group_id=@access_group_id AND access_module_id=@access_module_id";

        //        cmd.Connection = sqlCon;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@access_group_id", _BL.AccessGroupId);
        //        cmd.Parameters.AddWithValue("@access_module_id", _BL.AccessModuleId);
        //        cmd.Parameters.AddWithValue("@access_add", _BL.AccessAdd);
        //        cmd.Parameters.AddWithValue("@access_edit", _BL.AccessEdit);
        //        cmd.Parameters.AddWithValue("@access_delete", _BL.AccessDelete);

        //        cmd.CommandText = querystring;
        //        cmd.CommandTimeout = 0;
        //        cmd.Connection.Open();

        //        int affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
        //        cmd.Connection.Close();
        //        if (affectedRows > 0)
        //            lPass = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        if ((cmd.Connection.State == ConnectionState.Open))
        //        {
        //            cmd.Connection.Close();
        //        }
        //        throw;
        //    }

        //    return lPass;
        //}

        public bool Delete()
        {
            object obj = null;
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "DELETE FROM access_group_module WHERE access_group_id = '" + _BL.AccessGroupId + "'";

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

        public bool Delete(string AccessModuleID)
        {
            object obj = null;
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "DELETE FROM access_group_module WHERE access_group_id = '" + _BL.AccessGroupId + "' AND access_module_id='" + AccessModuleID + "'";

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