using Apps.Common;
using COMMON.Lib.BizLogic;
using COMMON.Lib.BizLogic.Manager;
using eSTS.Common;
using eSTS.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Apps.Auth;
using COMMON.Lib.BizLogic.Classes;
using System.Data;
using System.Web.Configuration;

namespace eSTS
{
    public partial class insts : System.Web.UI.Page
    {
        string UserID = null;
        string UserPass = null;
        string redirectPage = null;
        string UserType = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMsg.Text = "";
            //int isLock = Convert.ToInt32(ConfigurationManager.AppSettings["SetLock"]);

            //if (isLock == 1)
            //{
            //    Response.Redirect("~//TemporarilyClose.aspx");
            //}
            if (Request.QueryString.Count > 0)
            {
                if ((Request.QueryString["staffno"] != null || Request.QueryString["userid"] != null))
                {
                    UserID = Request.QueryString["userid"].ToString();
                    UserPass = "";
                   // loadAccess(UserID);
                }
                else
                {
                    if (Request.QueryString["rd"] != null)
                    {
                        redirectPage = Request.QueryString["rd"].ToString();
                    }

                }
            }
            //if (Request.QueryString["LogStatus"].ToString().Trim().ToUpper().Equals("OUT"))
            //               {
            if (Session["UserID"] != null)
            {
                string UserID = Session["UserID"].ToString();
                string UserGroup = Session["UserGroup"].ToString();

                Log.WriteUserAccessLog(UserID, UserGroup, StandardDefinition.AccessType.Logout);
            }
            else
            {
                //Session.Clear();
                //Response.Redirect("~//Login.aspx");
            }
            //}




            if (!IsPostBack)
            { 
                //txtUserID.Focus();
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserID = Request["txtUserID"].ToString();
                UserPass = Request["txtPassword"].ToString();

                //COMMON_UserAuthenticate userAuthenticate = new COMMON_UserAuthenticate("MSSQL");
                var hashedpwd = security.Encrypt(UserPass);
                eSTS.DAL.DALUsers objMain = new eSTS.DAL.DALUsers();


                //FOR TESTING
                Session["CompID"] = "";
                Session["UserID"] = "";
                Session["UserGroup"] = "";
                Session["IsSTSOperator"] = "";
                //if (objMain.CheckExistByUserIDPass(UserID, hashedpwd) == 0)
                //{
                //    if (!objMain.LoadTBLUAMUSER(UserID))
                //    {
                //        lblErrorMsg.Text = "Failed to retrieve user info!";
                //        return;
                //    }
                //    if (objMain.ds.Tables[0].Rows.Count > 0)
                //    {
                //        var hashedpwd2 = CommonSaltedHash.VerifyHash(hashedpwd, objMain.ds.Tables[0].Rows[0]["Password"].ToString());

                //        if (hashedpwd2 == true)
                //        {
                //            if (objMain.CheckExistByUserID(UserID) == 1)
                //            {
                //                loadAccess(UserID);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        loadAccess(UserID);
                //    }

                //}
                //else
                loadAccess(UserID);
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, System.IO.Path.GetFileName(Request.PhysicalPath), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private bool loadAccess(string UserID)
        {
            eSTS.DAL.DALUsers objMain = new eSTS.DAL.DALUsers();
            try
            {
                UserType = rbUserType.Value.ToString();
                if (UserType == "GOV")
                {
                    if (!objMain.LoadLoginGOV(UserID))
                    {
                        lblErrorMsg.Text = "Failed to retrieve user info!";
                        return false;
                    }
                }
                else     
                {
                    if (!objMain.LoadLoginNGOV(UserID))
                    {
                        lblErrorMsg.Text = "Failed to retrieve user info!";
                        return false;
                    }
                }
                if (objMain.ds.Tables["Users"].Rows.Count > 0)
                {
                    DataRow dr = objMain.ds.Tables["Users"].Rows[0];

                    //Session["AccessID"] = "A522B496-09CD-4A04-9401-F6F12BAFFB8E";
                    if (dr["AccessGroupID"] != null)
                        Session["UserGroup"] = dr["AccessGroupID"].ToString();

                    
                    Session["UserGroupDesc"] = dr["AccessGroupDesc"].ToString();
                    Session["CompID"] = dr["ROCNo"].ToString();
                    Session["UserID"] = dr["UserID"].ToString();
                    Session["FullName"] = dr["FullName"].ToString();
                    Session["PortLocation"] = dr["PortLoc"].ToString();
                    Session["CompanyName"] = dr["CompanyName"].ToString();
                    Session["IsSTSOperator"] = false;
                    Session["IsSTSAgent"] = false;
                    //checkPermitIssuerID
                    objMain.LoadPermitIssuerID(dr["AccessGroupID"].ToString());
                    if (objMain.ds.Tables["AccessGroup"].Rows.Count > 0)
                    {
                        DataRow drAG = objMain.ds.Tables["AccessGroup"].Rows[0];
                        Session["PermitIssuerID"] = drAG["PermitIssuerID"];
                        if (new Guid(drAG["AccessGroupID"].ToString()) == new Guid(WebConfigurationManager.AppSettings["BO"].ToString()))
                            Session["IsSTSOperator"] = true;
                        if (new Guid(drAG["AccessGroupID"].ToString()) == new Guid(WebConfigurationManager.AppSettings["BA"].ToString()))
                            Session["IsSTSAgent"] = true;
                    }
                    else
                    {
                        Session["PermitIssuerID"] = "";
                    }
                        proceedLogin(dr["ModuleLink"].ToString());
                    Log.WriteUserAccessLog(Session["UserID"].ToString(), Session["UserGroup"].ToString(), StandardDefinition.AccessType.Login);
                }
                else
                {
                    lblErrorMsg.Text = "You're not authorized to access the system";
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, System.IO.Path.GetFileName(Request.PhysicalPath), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return true;
        }
        private void proceedLogin(string link)
        {
            try
            {
                #region New Dashboard 20170927
                //string UserGroup = Session["UserGroup"].ToString().Trim();
                //if (isAdmin && isNDA == false)
                //    Response.Redirect("~//NDA.aspx", false);
                //else
                //{
                //    if (redirectPage != null)
                //        Response.Redirect("~//Leave/LeaveDashboard.aspx", false);
                //    else
                //        Response.Redirect("~//PersonalDashboard.aspx", false);
                //}
                if(link!="")
                    Response.Redirect("~/"+link, false);
                else
                    Response.Redirect("~/License/BunkerOperatorLic.aspx", false);
                #endregion
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, System.IO.Path.GetFileName(Request.PhysicalPath), System.Reflection.MethodBase.GetCurrentMethod().Name);
                General.SetLabelMessage(ref lblErrorMsg, "Login Failed", General.MsgType.Warning);
            }
        }
    }
}