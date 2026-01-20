using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Apps.Auth
{
    public class DALLevelModule
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion

        public LevelModule _BL = new LevelModule();
        const string TableName = "access_level_module";

        public DALLevelModule()
        { sqlCon.ConnectionString = connectionstring; }

        public bool Load()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT access_level_id, access_module_id FROM access_level_module";

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
                querystring = "SELECT access_level_id, access_module_id FROM access_level_module WHERE access_level_id='" + _BL.AccessLevelId + "'";

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

        public bool Save()
        {
            try
            {
                if (CheckExists())
                    return Update();
                else
                    return Insert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckExists()
        {
            object obj = null;
            bool lPass = false;
            int iResult = 0;

            try
            {
                string querystring = null;
                querystring = "SELECT COUNT(*) FROM access_level_module " +
                    "WHERE access_level_id = @access_level_id AND access_module_id = @access_module_id";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@access_level_id", _BL.AccessLevelId);
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

        public bool Insert()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "INSERT INTO access_level_module (access_level_id, access_module_id, access_view, access_add, access_edit, access_delete) " +
                    "VALUES (@access_level_id, @access_module_id, @access_view, @access_add, @access_edit, @access_delete)";

                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@access_level_id", _BL.AccessLevelId);
                cmd.Parameters.AddWithValue("@access_module_id", _BL.AccessModuleId);
                cmd.Parameters.AddWithValue("@access_view", _BL.AccessView);
                cmd.Parameters.AddWithValue("@access_add", _BL.AccessAdd);
                cmd.Parameters.AddWithValue("@access_edit", _BL.AccessEdit);
                cmd.Parameters.AddWithValue("@access_delete", _BL.AccessDelete);

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
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "UPDATE access_level_module SET access_view=@access_view, access_add=@access_add, access_edit=@access_edit, access_delete=@access_delete " +
                    "WHERE access_level_id=@access_level_id AND access_module_id=@access_module_id";

                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@access_level_id", _BL.AccessLevelId);
                cmd.Parameters.AddWithValue("@access_module_id", _BL.AccessModuleId);
                cmd.Parameters.AddWithValue("@access_view", _BL.AccessView);
                cmd.Parameters.AddWithValue("@access_add", _BL.AccessAdd);
                cmd.Parameters.AddWithValue("@access_edit", _BL.AccessEdit);
                cmd.Parameters.AddWithValue("@access_delete", _BL.AccessDelete);

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

        public bool Delete()
        {
            object obj = null;
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "DELETE FROM access_level_module WHERE access_level_id = '" + _BL.AccessLevelId + "'";

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
                querystring = "DELETE FROM access_level_module WHERE access_level_id = '" + _BL.AccessLevelId + "' AND access_module_id='" + AccessModuleID + "'";

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

        public bool GetAccessLevel(string AccessLevelId, string PageFileName)
        {
            bool lPass = false;

            try
            {
                string querystring = null;

                querystring = "SELECT ISNULL(access_view,0) AS access_view, ISNULL(access_add,0) AS access_add, ISNULL(access_edit,0) AS access_edit, " +
                    "ISNULL(access_delete,0) AS access_delete " +
                    "FROM access_module A " +
                    "LEFT OUTER JOIN access_level_module B ON A.access_module_id=B.access_module_id AND B.access_level_id='" + AccessLevelId + "' " +
                    "WHERE A.module_link LIKE '%" + PageFileName + "' AND is_active=1";

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


                    SetAccessLevel();
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

        private void SetAccessLevel()
        {
            _BL.AccessView = false;
            _BL.AccessAdd = false;
            _BL.AccessEdit = false;
            _BL.AccessDelete = false;

            if (ds.Tables[0].Rows.Count > 0)
            {
                _BL.AccessView = Convert.ToBoolean(ds.Tables[0].Rows[0]["access_view"]);
                _BL.AccessAdd = Convert.ToBoolean(ds.Tables[0].Rows[0]["access_add"]);
                _BL.AccessEdit = Convert.ToBoolean(ds.Tables[0].Rows[0]["access_edit"]);
                _BL.AccessDelete = Convert.ToBoolean(ds.Tables[0].Rows[0]["access_delete"]);
            }
        }
    }
}