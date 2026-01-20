using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Apps.Auth
{
    public class DALUsers
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion

        public Users _BL = new Users();
        const string TableName = "users";

        public DALUsers()
        { sqlCon.ConnectionString = connectionstring; }

        public bool Load()
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT  user_id, user_pass, full_name, id_no, gender, address1, address2, city, state, country, postcode, phone, email_address, access_group_id, access_level_id, unit_id, " +
                      "designation, remark, is_active, created_on, created_by, modified_on, modified_by FROM users";

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

        public bool LoadLogin(string UserID, string user_pass)
        {
            bool lPass = false;

            try
            {
                user_pass = security.Encrypt(user_pass);

                string querystring = null;
                //querystring = "SELECT user_id, user_pass, full_name, id_no, email_address, designation, remark, access_group_id, access_level_id, users.unit_id, users.is_active, unit.hou_user_id " +
                //            "FROM users inner join unit on users.unit_id=unit.unit_id "+        
                //            "where users.is_active=1 AND user_id='" + UserID + "' AND user_pass='" + user_pass + "'";
                //querystring = "SELECT a.LoginID,a.accessid, a.Password, b.StaffName,b.ICNumber,d.EmailAddress, b.designation, remark, access_group_id, access_level_id, b.BranchID, e.is_active " +
                //               "FROM lpjportal.dbo.TblUAMUser a " +
                //               "INNER JOIN lpjportal.dbo.TblUAMUserProfile b on a.AccessID = b.AccessID " +
                //               "INNER JOIN lpjportal.dbo.SetupDept c on c.DeptCode = b.BranchID " +
                //               "Left outer join lpjportal.dbo.TblSMMEmailList d on d.EmailID = b.EmailID " +
                //               "Inner join users e on e.user_id collate SQL_Latin1_General_CP1_CI_AS = a.LoginID " +
                //               "where a.Deleted = 0 AND a.LoginID = 'normala.a'";

                querystring = "select loginid, accessid,piid,password,staffnumber,staffname,ICNew,EmailAdd,UnitID,designation,access_group_id, access_level_id,Branchid,is_active,IsNDA,IsAdmin,IsSSM " +
                               "from v_staff where LoginID = '"+UserID+"'";

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

        public bool Load_ByUserId(string userid)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                //querystring = "SELECT user_id, user_pass, full_name, id_no, email_address, remark, access_group_id, a.unit_id, a.is_active "+
                //              "FROM users a INNER JOIN unit b on a.unit_id = b.unit_id WHERE user_id='" + userid + "'";
                querystring = "SELECT user_id, user_pass, full_name, id_no, gender, address1, address2, city, state, country, postcode, phone, email_address, access_group_id, unit_id, " +
                      "designation, remark, is_active, created_on, created_by, modified_on, modified_by " +
                              "FROM users WHERE user_id='" + userid + "'";

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
        
        public bool Load_ByGroup(string AccessGroupID)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT user_id, user_pass, full_name, id_no, email_address, remark, access_group_id, unit_id, is_active, created_on, created_by, modified_on, modified_by " +
                    "FROM users " +
                    "WHERE access_group_id='" + AccessGroupID + "'";

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

        public bool Load_ByEmail(string EmailAddress)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT TOP 1 user_id, user_pass, full_name, id_no, email_address, remark, access_group_id, unit_id, is_active " +
                    "FROM users " +
                    "WHERE email_address='" + EmailAddress + "'";

                using (SqlConnection sqlCon = new SqlConnection(connectionstring))
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

        public bool Load_ByUserID_Email(string UserID, string EmailAddress)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT TOP 1 user_id, user_pass, full_name, id_no, email_address, remark, access_group_id, unit_id, is_active " +
                    "FROM users " +
                    "WHERE user_id='" + UserID + "' AND email_address='" + EmailAddress + "'";

                using (SqlConnection sqlCon = new SqlConnection(connectionstring))
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
                _BL.UserPass = security.Encrypt(_BL.UserPass);

                string querystring = null;
                querystring = "INSERT INTO users (user_id, user_pass, full_name, id_no, " +
                    "gender, address1, address2, city, state, country, postcode, phone, " +
                    "email_address, access_group_id, access_level_id, unit_id, designation, remark, is_active, " +
                   "created_on, created_by, modified_on, modified_by) " +
                    "VALUES (@user_id, @user_pass, @full_name, @id_no, " +
                    "@gender, @address1, @address2, @city, @state, @country, @postcode, @phone, " +
                    "@email_address, @access_group_id, @access_level_id, @unit_id, @designation, @remark, @is_active, " +
                   "GETDATE(), @created_by, GETDATE(), @modified_by)";

                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@user_id", _BL.UserId);
                cmd.Parameters.AddWithValue("@user_pass", _BL.UserPass);
                cmd.Parameters.AddWithValue("@full_name", _BL.FullName);
                cmd.Parameters.AddWithValue("@id_no", _BL.IdNo);

                cmd.Parameters.AddWithValue("@gender", _BL.Gender);
                cmd.Parameters.AddWithValue("@address1", _BL.Address1);
                cmd.Parameters.AddWithValue("@address2", _BL.Address2);
                cmd.Parameters.AddWithValue("@city", _BL.City);
                cmd.Parameters.AddWithValue("@state", _BL.State);
                cmd.Parameters.AddWithValue("@country", _BL.Country);
                cmd.Parameters.AddWithValue("@postcode", _BL.Postcode);
                cmd.Parameters.AddWithValue("@phone", _BL.Phone);
                
                cmd.Parameters.AddWithValue("@email_address", _BL.EmailAddress);
                cmd.Parameters.AddWithValue("@access_group_id", _BL.AccessGroupId);
                cmd.Parameters.AddWithValue("@access_level_id", _BL.AccessLevelId);
                cmd.Parameters.AddWithValue("@unit_id", _BL.UnitId);
                cmd.Parameters.AddWithValue("@designation", _BL.Designation);
                cmd.Parameters.AddWithValue("@remark", _BL.Remark);
                cmd.Parameters.AddWithValue("@is_active", _BL.IsActive);
                cmd.Parameters.AddWithValue("@created_by", _BL.CreatedBy);
                cmd.Parameters.AddWithValue("@modified_by", _BL.ModifiedBy);
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
                querystring = "UPDATE users " +
                    "SET full_name=@full_name, id_no=@id_no, "+
                    "gender=@gender, address1=@address1, address2=@address2, city=@city, state=@state, country=@country, postcode=@postcode, phone=@phone, " +
                    "email_address=@email_address, access_group_id=@access_group_id, access_level_id=@access_level_id, unit_id=@unit_id, designation=@designation, remark=@remark, " +
                    "is_active=@is_active, modified_on=GETDATE(), modified_by=@modified_by " +
                    "WHERE user_id=@user_id";

                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@full_name", _BL.FullName);
                cmd.Parameters.AddWithValue("@id_no", _BL.IdNo);

                cmd.Parameters.AddWithValue("@gender", _BL.Gender);
                cmd.Parameters.AddWithValue("@address1", _BL.Address1);
                cmd.Parameters.AddWithValue("@address2", _BL.Address2);
                cmd.Parameters.AddWithValue("@city", _BL.City);
                cmd.Parameters.AddWithValue("@state", _BL.State);
                cmd.Parameters.AddWithValue("@country", _BL.Country);
                cmd.Parameters.AddWithValue("@postcode", _BL.Postcode);
                cmd.Parameters.AddWithValue("@phone", _BL.Phone);

                cmd.Parameters.AddWithValue("@email_address", _BL.EmailAddress);
                cmd.Parameters.AddWithValue("@access_group_id", _BL.AccessGroupId);
                cmd.Parameters.AddWithValue("@access_level_id", _BL.AccessLevelId);
                cmd.Parameters.AddWithValue("@unit_id", _BL.UnitId);
                cmd.Parameters.AddWithValue("@designation", _BL.Designation);
                cmd.Parameters.AddWithValue("@remark", _BL.Remark);
                cmd.Parameters.AddWithValue("@is_active", _BL.IsActive);
                cmd.Parameters.AddWithValue("@modified_by", _BL.ModifiedBy);
                cmd.Parameters.AddWithValue("@user_id", _BL.UserId);
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

        public bool UpdatePassword()
        {
            object obj = null;
            bool lPass = false;

            try
            {
                _BL.UserPass = security.Encrypt(_BL.UserPass);

                string querystring = null;
                querystring = "UPDATE users " +
                    "SET user_pass=@user_pass " +
                    "WHERE user_id=@user_id";

                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@user_pass", _BL.UserPass);
                cmd.Parameters.AddWithValue("@user_id", _BL.UserId);
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
        
        public bool CheckUserExist(string UserID)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT user_id " +
                    "FROM users WHERE user_id='" + UserID + "'";

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

                    if (ds.Tables[TableName].Rows.Count > 0)
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

        public string GetUserFullName(string UserID)
        {
            object obj = null;
            string iResult = "";

            try
            {
                string querystring = null;
                querystring = "SELECT full_name " +
                    "FROM users WHERE user_id='" + UserID + "'";

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
                        iResult = Convert.ToString(obj);

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

            return iResult;
        }

        #region Khairat
        public bool CheckUserExist_K(string UserID)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT user_id " +
                    "FROM k_user WHERE user_id='" + UserID + "'";

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

                    if (ds.Tables[TableName].Rows.Count > 0)
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

        public bool Load_ByUserID_Email_K(string UserID, string EmailAddress)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT TOP 1 user_id, user_pass, full_name, id_no, email_address, remark, access_group_id, comp_code, is_active " +
                    "FROM k_user " +
                    "WHERE user_id='" + UserID + "' AND email_address='" + EmailAddress + "'";

                using (SqlConnection sqlCon = new SqlConnection(connectionstring))
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

        public bool UpdatePassword_K()
        {
            object obj = null;
            bool lPass = false;

            try
            {
                _BL.UserPass = security.Encrypt(_BL.UserPass);

                string querystring = null;
                querystring = "UPDATE k_user " +
                    "SET user_pass=@user_pass " +
                    "WHERE user_id=@user_id";

                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@user_pass", _BL.UserPass);
                cmd.Parameters.AddWithValue("@user_id", _BL.UserId);
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

        public bool Load_Khairat(string UserID)
        {
            bool lPass = false;

            try
            {
                string querystring = null;
                querystring = "SELECT  A.user_id, A.user_pass, A.full_name, A.id_no, A.gender, A.address1, A.address2, "+
                                "A.city, state,A.country, A.postcode, "+
                                "A.phone, A.email_address, A.access_group_id, A.unit_id, "+
                                "A.designation, A.remark, A.is_active, A.created_on, A.created_by, A.modified_on, A.modified_by, "+
                                "C.doc_code "+ 
                                "FROM users A "+
                                "INNER JOIN unit B on A.unit_id=B.unit_id "+
                                "INNER JOIN setup_company_info C on B.comp_id=C.comp_id "+
                                "WHERE A.is_active='1' AND A.user_id='"+UserID+"' ";

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
        #endregion
    }
}