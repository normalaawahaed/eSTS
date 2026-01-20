using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace eSTS.DAL
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

       
        const string TableName = "users";

        public DALUsers()
        { sqlCon.ConnectionString = connectionstring; }

        public int CheckExistByUserIDPass1(string OrgzID,string userID, string userPass)
        {
            int result = 0;
            try
            {
                string querystring = null;
                querystring = " Select COUNT(userid) as ttl from v_AdminUsers where ROCNO='" + OrgzID + "' and UserID = '" + userID + "' and UserPass='" + userPass + "'";

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
        public int CheckExistByUserIDPass2(string OrgzID,string userID, string userPass)
        {
            int result = 0;
            try
            {
                string querystring = null;
                querystring = " Select COUNT(userid) as ttl from v_UsersSASO where ROCNO='" + OrgzID + "' and UserID = '" + userID + "' and UserPass='" + userPass + "'";

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


        public int CheckExistByUserIDPass(string userID, string userPass)
        {
            int result = 0;
            try
            {
                string querystring = null;
                querystring = " Select COUNT(userid) as ttl from v_MMSUsers where UserID = '" + userID + "' and UserPass='" + userPass + "'";

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

        public int CheckExistByUserID(string userID)
        {
            int result = 0;
            try
            {
                string querystring = null;
                querystring = " Select COUNT(userid) as ttl from v_users where UserID = '" + userID + "'";

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
        public bool LoadTBLUAMUSER(string UserID)
        {
            bool lPass = false;

            try
            {
                //user_pass = security.Encrypt(user_pass);

                string querystring = null;

                querystring = "SELECT * FROM SRV_NAME4.LPJCONSOLE.dbo.TBLUAMUSER WHERE DELETED = 0 AND ACCESSSTATUS = 0 AND TBLUAMUSER.LOGINID = '" + UserID+"'";
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
        public bool LoadLoginGOV(string UserID)
        {
            bool lPass = false;

            try
            {
                //user_pass = security.Encrypt(user_pass);

                string querystring = null;

                querystring = "Select * from v_AdminUsers where UserID = '" + UserID + "'";
                //querystring = "Select * from v_users where UserID = '" + UserID + "'";
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

                    ad.Fill(ds, "Users");
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
        public bool LoadLoginNGOV(string UserID)
        {
            bool lPass = false;

            try
            {
                //user_pass = security.Encrypt(user_pass);

                string querystring = null;

                querystring = "Select * from v_UsersSASO where UserID = '" + UserID + "'";

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

                    ad.Fill(ds, "Users");
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
        public bool LoadLoginByUserID(string ROCNo,string UserID)
        {
            bool lPass = false;

            try
            {
                

                string querystring = null;

                querystring = "Select * from v_users where ROCNo='" + ROCNo + "' and UserID = '" + UserID + "'";

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

                    ad.Fill(ds, "Users");
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
        public bool LoadPermitIssuerID(string AccessGroupID)
        {
            bool lPass = false;

            try
            {


                string querystring = null;

                querystring = "select * from AccessGroup where AccessGroupID='"+AccessGroupID+"'";

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

                    ad.Fill(ds, "AccessGroup");
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
        public bool LoadUserVTMS(string location)
        {
            bool lPass = false;

            try
            {
                //user_pass = security.Encrypt(user_pass);

                string querystring = null;

                querystring = @"select * from dbo.v_AdminUsers 
                            where UserID in ('VTMSPG','VTMSPTP') and PortLoc = "+location;

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

                    ad.Fill(ds, "Users");
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