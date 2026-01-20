using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Apps.Auth
{
    public class DALAccessModule
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion

        public AccessModule _BL = new AccessModule();
        const string TableName = "access_module";

        public DALAccessModule()
        { sqlCon.ConnectionString = connectionstring; }

        public bool Load()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = @"SELECT AccessModule.ModuleID,AccessModule.ModuleDesc,AccessModule.ParentID,Parent.ModuleDesc as ParentName,                               AccessModule.ModuleLevel,AccessModule.ModuleSeq,AccessModule.ModuleLink,AccessModule.Icon,AccessModule.IsActive,AccessModule.IsSetting
                FROM  AccessModule
                left outer join
                (select ModuleDesc, ModuleID from AccessModule where ParentID is null) as Parent on Parent.ModuleID = AccessModule.ModuleID
                ORDER BY AccessModule.ModuleLevel, AccessModule.ParentID, AccessModule.ModuleSeq";

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
                querystring = @"SELECT AccessModule.ModuleID,AccessModule.ModuleDesc,AccessModule.ParentID,Parent.ModuleDesc as ParentName,
AccessModule.ModuleLevel,AccessModule.ModuleSeq,AccessModule.ModuleLink,AccessModule.Icon,AccessModule.IsActive,AccessModule.IsSetting
FROM  AccessModule
left outer join
(select ModuleDesc, ModuleID from AccessModule where ParentID is null) as Parent on Parent.ModuleID = AccessModule.ModuleID
Where IsActive=1
ORDER BY AccessModule.ModuleLevel, AccessModule.ParentID, AccessModule.ModuleSeq";

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

        public bool LoadActive(string AccessGroupId)
        {
            bool lPass = false;

            try
            {
                string querystring = null;

                //querystring = "SELECT A.access_module_id, description, parent_id, module_level, module_seq, module_link, is_active " +
                //    ", ISNULL(access_add,0) AS access_add, ISNULL(access_edit,0) AS access_edit, ISNULL(access_delete,0) AS access_delete " +
                //   "FROM access_module A "+
                //   "LEFT OUTER JOIN access_group_module B ON A.access_module_id=B.access_module_id AND B.access_group_id='" + AccessGroupId + "' " +
                //   "WHERE is_active=1 " +
                //   "ORDER BY module_level, parent_id, module_seq";
                querystring = "SELECT A.access_module_id, description, parent_id, module_level, module_seq, module_link, is_active " +
                   "FROM access_module A " +
                   "LEFT OUTER JOIN access_group_module B ON A.access_module_id=B.access_module_id AND B.access_group_id='" + AccessGroupId + "' " +
                   "WHERE is_active=1 " +
                   "ORDER BY module_level, parent_id, module_seq";

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

        public bool LoadLevelActive(string AccessLevelId)
        {
            bool lPass = false;

            try
            {
                string querystring = null;

                querystring = "SELECT A.access_module_id, description, parent_id, module_level, module_seq, module_link, is_active " +
                    ", ISNULL(access_view,1) AS access_view, ISNULL(access_add,1) AS access_add, ISNULL(access_edit,1) AS access_edit, ISNULL(access_delete,1) AS access_delete " +
                   "FROM access_module A " +
                   "LEFT OUTER JOIN access_level_module B ON A.access_module_id=B.access_module_id AND B.access_level_id='" + AccessLevelId + "' " +
                   "WHERE is_active=1 " +
                   "ORDER BY module_level, parent_id, module_seq";

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

        public bool LoadByGroup(string AccessGroupId)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = @"SELECT AccessModule.ModuleID,AccessModule.ModuleDesc,AccessModule.ParentID,Parent.ModuleDesc as ParentName,
                                AccessModule.ModuleLevel,AccessModule.ModuleSeq,AccessModule.ModuleLink,AccessModule.Icon,AccessModule.IsActive,AccessModule.IsSetting
                                FROM  AccessModule
                                left outer join
                                (select ModuleDesc, ModuleID from AccessModule where ParentID is null) as Parent on Parent.ModuleID = AccessModule.ModuleID
                                Where IsActive = 1 and AccessModule.ModuleID In (select ModuleID from AccessGroupModule where AccessGroupID = @accessGroupID)
                                ORDER BY AccessModule.ModuleLevel, AccessModule.ParentID, AccessModule.ModuleSeq";
                 

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@accessGroupID", AccessGroupId);
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
                querystring = "INSERT INTO access_module (access_module_id, description, parent_id, module_level, module_seq, module_link, is_active) " +
                    "VALUES ('" + _BL.AccessModuleId + "', '" + _BL.Description + "', '" + _BL.ParentId + "', " +
                    _BL.ModuleLevel + ", " + _BL.ModuleSeq + ", '" + _BL.ModuleLink + "', '" + _BL.IsActive + "')";

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
                querystring = "UPDATE access_module " +
                    "SET description = '" + _BL.Description + "', parent_id = '" + _BL.ParentId + "', module_level = " + _BL.ModuleLevel +
                    ", module_seq = '" + _BL.ModuleSeq + "', module_link = '" + _BL.ModuleLink + "',  is_active = '" + _BL.IsActive + "' " +
                    "WHERE access_module_id = '" + _BL.AccessModuleId + "'";

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

        public void GetModuleDesc_ByModuleLink(ref string pageName,ref string pagelink, ref string ParentName, ref string ParentLink,string ModuleLink)
        {

            try
            {
                string querystring = null;
                querystring = @"SELECT AccessModule.ModuleID,AccessModule.ModuleDesc,AccessModule.ModuleTitle,AccessModule.ParentID,Parent.ModuleTitle as ParentName,Parent.ModuleLink as ParentLink, 
AccessModule.ModuleLevel,AccessModule.ModuleSeq,AccessModule.ModuleLink,AccessModule.Icon,AccessModule.IsActive,AccessModule.IsSetting
FROM  AccessModule
left outer join
(select ModuleDesc,ModuleTitle, ModuleID, ModuleLink from AccessModule where ParentID is null) as Parent on Parent.ModuleID = AccessModule.ModuleID
Where  AccessModule.ModuleLink like  '%" + ModuleLink+"%'";
             
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("module_link", ModuleLink);
                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, TableName);
                    cmd.Connection.Close();
                    
                    if (ds.Tables[TableName].Rows.Count > 0)
                    {
                        pageName = ds.Tables[TableName].Rows[0]["ModuleTitle"].ToString();
                        if (ds.Tables[TableName].Rows[0]["ParentName"] !=null)
                            ParentName = ds.Tables[TableName].Rows[0]["ParentName"].ToString();
                        if (ds.Tables[TableName].Rows[0]["ModuleLink"] != null)
                            pagelink = ds.Tables[TableName].Rows[0]["ModuleLink"].ToString();
                        if (ds.Tables[TableName].Rows[0]["ParentLink"] != null)
                            ParentLink = ds.Tables[TableName].Rows[0]["ParentLink"].ToString();
                    }
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

        }

        public bool CheckAccessLink(string accessGroupID, string ModuleLink)
        {

            try
            {
                string querystring = null;
                querystring = @"Select x.ModuleID,x.ModuleCode,x.ModuleDesc,x.ParentID,x.ModuleLink " +
                              "  ,y.ModuleCode as yCode,y.ModuleDesc as yDesc,y.ModuleLink as yLink from  " +
                               " (  " +
                               " SELECT    am.*  " +
                               " FROM         dbo.AccessGroupModule AS agm  " +
                                "INNER JOIN                       dbo.AccessModule AS am ON agm.ModuleID = am.ModuleID  " +
                               " WHERE     agm.AccessGroupID = '" + accessGroupID + "'" +
                                ") x  " +
                               " left outer join   " +
                               " (" +
                               " select * from AccessModule where ParentID is not null and ModuleLevel is null  " +
                               " ) y on x.ModuleID=y.ParentID  " +
                               " where x.ModuleLink like  '%" + ModuleLink + "% ' or y.ModuleLink like  '%" + ModuleLink + "%'  " +
                               " order by x.ModuleCode  ";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                  //  cmd.Parameters.AddWithValue("pAccessGroupID", accessGroupID);
                  //  cmd.Parameters.AddWithValue("pModuleLink", ModuleLink);
                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    int recordExist = (int)cmd.ExecuteScalar();

                    if (recordExist == 1) //anything different from 1 should be wrong
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }
            return false;
        }
        public string GetUserManual(string AccessGroupID)
        {
            object result = null;
            try
            {
                string querystring = null;
                querystring = "select UserManual from AccessGroup where AccessGroupID ='" + AccessGroupID + "'";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    result = cmd.ExecuteScalar();
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

            return Convert.ToString(result);
        }
    }
}