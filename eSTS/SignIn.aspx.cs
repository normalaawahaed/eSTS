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
    public partial class SignIn : System.Web.UI.Page
    {
        string UserType = null;
        string OrgzID = null;
        string UserID = null;
        string UserPass = null;
        string redirectPage = null;

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
                txtUserID.Focus();
                lblVersion.Text = "Version "+ WebConfigurationManager.AppSettings["STSVersion"].ToString();
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserType = rbUserType.Value.ToString();
                OrgzID = Request["txtOrgzID"].ToString();
                UserID = Request["txtUserID"].ToString();
                UserPass = Request["txtPassword"].ToString();

                if (UserType == "")
                {
                    lblErrorMsg.Text = "Please select agency.";
                    return;
                }
                if (OrgzID == "")
                {
                    lblErrorMsg.Text = "Please enter ROC/ROB/Org.No.";
                    return;
                }
                if (UserID == "")
                {
                    lblErrorMsg.Text = "Please enter User ID";
                    return;
                }
                if (UserPass == "")
                {
                    lblErrorMsg.Text = "Please enter Password";
                    return;
                }
          
                var hashedpwd = security.Encrypt(UserPass);
              
                eSTS.DAL.DALUsers objMain = new eSTS.DAL.DALUsers();
                //var hashedpwd2 = CommonSaltedHash.ComputeHash(hashedpwd, null);

                if (UserType=="GOV")
                {
                    if (objMain.CheckExistByUserIDPass1(OrgzID, UserID, hashedpwd) == 0)
                    {
                        loadAccess(objMain.ds);
                        ////if (!objMain.LoadTBLUAMUSER(UserID))
                        ////{
                        //if (!objMain.LoadLoginByUserID(OrgzID, UserID))
                        //{
                        //    lblErrorMsg.Text = "Failed to retrieve user info!";
                        //    return;
                        //}
                        //if (objMain.ds.Tables[0].Rows.Count > 0)
                        //{
                        //    var hashedpwd2 = CommonSaltedHash.VerifyHash(hashedpwd, objMain.ds.Tables[0].Rows[0]["UserPass2"].ToString());

                        //    if (hashedpwd2 == true)
                        //    {
                        //        //if (objMain.CheckExistByUserID(UserID) == 1)
                        //        //{
                        //        loadAccess(objMain.ds);
                        //        //}
                        //    }
                        //    else
                        //    {
                        //        //lblErrorMsg.Text = "Invalid User ID  or Password";
                        //        loadAccess(objMain.ds);
                        //    }
                        //}
                    }
                    else
                    {
                        if (!objMain.LoadLoginByUserID(OrgzID, UserID))
                        {
                            lblErrorMsg.Text = "Failed to retrieve user info!";
                            return;
                        }
                        else
                            loadAccess(objMain.ds);
                    }
                }
                else
                {
                    if (objMain.CheckExistByUserIDPass2(OrgzID, UserID, hashedpwd) == 0)
                    {
                        //if (!objMain.LoadTBLUAMUSER(UserID))
                        //{
                        if (!objMain.LoadLoginByUserID(OrgzID, UserID))
                        {
                            lblErrorMsg.Text = "Failed to retrieve user info!";
                            return;
                        }
                        if (objMain.ds.Tables[0].Rows.Count > 0)
                        {
                            var hashedpwd2 = CommonSaltedHash.VerifyHash(hashedpwd, objMain.ds.Tables[0].Rows[0]["UserPass2"].ToString());

                            if (hashedpwd2 == true)
                            {
                                loadAccess(objMain.ds);
                            }
                            else
                            {
                                lblErrorMsg.Text = "Invalid User ID  or Password";
                            }
                        }
                    }
                    else
                    {
                        if (!objMain.LoadLoginByUserID(OrgzID, UserID))
                        {
                            lblErrorMsg.Text = "Failed to retrieve user info!";
                            return;
                        }
                        else
                            loadAccess(objMain.ds);
                    }
                }
               
             
                   
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, System.IO.Path.GetFileName(Request.PhysicalPath), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private bool loadAccess(DataSet ds)
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

                        if (drAG["AccessGroupID"].ToString() == WebConfigurationManager.AppSettings["BO"].ToString())
                            Session["IsSTSOperator"] = true;
                        if (drAG["AccessGroupID"].ToString() == WebConfigurationManager.AppSettings["BA"].ToString())
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