using Apps.Common;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.Web.Bootstrap;
using eSTS.DAL;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.Operation
{
    public partial class UploadCMBL_B : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Check Session
                if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
                {
                    Response.Redirect("~//SignIn.aspx", true);
                }
                if (Request.QueryString.Count > 0)
                {
                    hfApplicationID.Value = Request.QueryString["sno"].ToString();
                    Session["mode"] = Request.QueryString["mode"].ToString();
                    hfMethod.Value = Request.QueryString["m"].ToString();
                }
                else
                {
                    Session["mode"] = "n";
                }
                if (!Page.IsPostBack)
                {
                    if (Session["mode"].ToString() != "n")
                    {
                        LoadForm();
                        if (Session["mode"].ToString() == "v")
                            DisableControl();
                    }


                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        

        private void LoadForm()
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid ApplicationID = new Guid(hfApplicationID.Value.ToString());
                    OperationApp item = dbContext.OperationApps.Where(w => w.OperationAppID == ApplicationID).FirstOrDefault<OperationApp>();
                   
                    txtBLNo.Text = item.BLNo;
                    txtCMNo.Text = item.CMNo;

                    if (item.ETA != null)
                        dtETA.Value = item.ETA;

                    if (item.ETD != null)
                        dtETD.Value = item.ETD;

                    if (item.ActOperationDateTime != null)
                        dtOperationDate.Value = item.ActOperationDateTime;

                    if (item.ActOperationTime != null)
                        operationTime.Value = item.ActOperationTime;

                    //-----------------------------------------------
                    //PRODUCT SUPPLIED
                    //-----------------------------------------------

                    txtMT.Text = item.ActOilMT.ToString();
                    cbUOM.Value = item.UOMID;

                    var sb = new System.Text.StringBuilder();
                    //Upload File
                    if (item.CMAttachLink != "" && item.CMAttachLink != null)
                    {
                        sb.AppendLine("<a href='" + item.CMAttachLink + "' target='_blank'><span class='corner'></span><div class='icon'><i class='fa fa-file'></i>" +
                                        "</div><div class='file-name text-center'> Download Cargo Manifest </div></a>");

                        lilFileCM.Text = sb.ToString();

                    }
                    if (item.BLAttachLink != "" && item.BLAttachLink != null)
                    {
                        sb.Clear();
                        sb.AppendLine("<a href='" + item.BLAttachLink + "' target='_blank'><span class='corner'></span><div class='icon'><i class='fa fa-file'></i>" +
                                        "</div><div class='file-name text-center'> Download Bill of Lading</div></a>");

                        lilFileBL.Text = sb.ToString();

                    }
                    //Acknowledment
                    if (Convert.ToBoolean(item.IsAcknowledgeBL))
                        chkAck.Checked = true;
                    else
                        chkAck.Checked = false;

                    if (Convert.ToBoolean(item.IsIntegrityBL))
                        chkIntegrity.Checked = true;
                    else
                        chkIntegrity.Checked = false;


                    dbContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void DisableControl()
        {
            dtETA.Enabled = false;
            dtETD.Enabled = false;
            cbUOM.Enabled = false;
            txtBLNo.Enabled = false;
            dtOperationDate.Enabled = false;
            operationTime.Enabled = false;
            txtMT.Enabled = false;
            chkAck.Enabled = false;
            chkIntegrity.Enabled = false;
            btnSave.Visible = false;
            btnSaveFileBL.Visible = false;
            btnSaveFileCM.Visible = false;
            divAttachBL.Visible = false;
            divAttachCM.Visible = false;
        }
        
        #region Attachment

        protected string SaveAttach(BootstrapUploadControl uploadFile, string type)
        {
            string fileName = "";
            string fullfileDirectory = "";
            string extension = "";
            string OriginalFileName = "";
            bool folderExists;
            string UploadDirectory = "Upload/" + Session["CompID"].ToString() + "/Operation";

            try
            {

                if (uploadFile.UploadedFiles.Count() > 0)
                {
                    //Upload File 
                    extension = uploadFile.UploadedFiles[0].FileName.Trim().Substring(uploadFile.UploadedFiles[0].FileName.Trim().LastIndexOf("."));
                    // OriginalFileName = uploadFile.UploadedFiles[0].FileName.Trim();
                    OriginalFileName = type + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + extension;


                    fileName = UploadDirectory + "/" + OriginalFileName;
                    fullfileDirectory = Server.MapPath(UploadDirectory + "/" + OriginalFileName);

                    //-------------------------------------------------------------
                    // Save File to server directory
                    //-------------------------------------------------------------
                    folderExists = Directory.Exists(Server.MapPath(UploadDirectory));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath(UploadDirectory));

                    uploadFile.UploadedFiles[0].SaveAs(fullfileDirectory);
                    //------------------------------------------------------------

                }
                fileName = "../Operation/" + fileName;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return fileName.Replace("~", "");
        }


        #endregion

        protected void btnSaveFileCM_Click(object sender, EventArgs e)
        {
            try
            {
                if (uploadFileCM.UploadedFiles.Count() == 0)
                {
                    lblErrMsg.Text = "Please select Cargo Manifest";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    uploadFileCM.Focus();
                    return;
                }
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    OperationApp item = dbContext.OperationApps.Find(new Guid(hfApplicationID.Value.ToString()));

                    item.IsSubmitCM = true;
                    item.CMNo = txtCMNo.Text;
                    item.CMAttachLink = SaveAttach(uploadFileCM, "CM");
                    item.SubmitCMDate = DateTime.Now;

                    //Upload File
                    if (item.CMAttachLink != "" && item.CMAttachLink != null)
                    {
                        var sb1 = new System.Text.StringBuilder();
                        sb1.AppendLine("<a href='" + item.CMAttachLink + "' target='_blank'><span class='corner'></span><div class='icon'><i class='fa fa-file'></i>" +
                                        "</div><div class='file-name text-center'> Download Ullage Report </div></a>");

                        lilFileCM.Text = sb1.ToString();

                    }


                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", item.OperationAppID);
                    dbContext.Dispose();
                }
                var sb = new System.Text.StringBuilder();

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
            }
        }

        protected void btnSaveFileBL_Click(object sender, EventArgs e)
        {
            try
            {
                if (uploadFileBL.UploadedFiles.Count() == 0)
                {
                    lblErrMsg.Text = "Please select Bill of lading file";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    uploadFileBL.Focus();
                    return;
                }
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    OperationApp item = dbContext.OperationApps.Find(new Guid(hfApplicationID.Value.ToString()));

                    item.IsSubmitBL = true;
                    item.BLNo = txtBLNo.Text;
                    item.BLAttachLink = SaveAttach(uploadFileBL, "BL");
                    item.SubmitBLDate = DateTime.Now;

                    
                    if (item.BLAttachLink != "" && item.BLAttachLink != null)
                    {
                        var sb = new System.Text.StringBuilder();
                        sb.AppendLine("<a href='" + item.BLAttachLink + "' target='_blank'><span class='corner'></span><div class='icon'><i class='fa fa-file'></i>" +
                                        "</div><div class='file-name text-center'> Download Bill of Lading</div></a>");

                        lilFileBL.Text = sb.ToString();

                    }

                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", item.OperationAppID);
                    dbContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();

                    //string supplyMethodA = sysParam.SupplyMethodA.ToString();
                    OperationApp item = dbContext.OperationApps.Find(new Guid(hfApplicationID.Value.ToString()));

                    if (isValidate(item))
                    {
                        item.ETA = Convert.ToDateTime(dtETA.Value);
                        item.ETD = Convert.ToDateTime(dtETD.Value);
                        item.ActOperationDateTime = Convert.ToDateTime(dtOperationDate.Value);
                        item.ActOperationTime = new DateTime(item.ActOperationDateTime.Value.Year, item.ActOperationDateTime.Value.Month, item.ActOperationDateTime.Value.Day, Convert.ToDateTime(operationTime.Value).Hour, Convert.ToDateTime(operationTime.Value).Minute, 0);
                        item.ActOilMT = Convert.ToDouble(txtMT.Text);
                        item.UOMID = new Guid(cbUOM.Value.ToString());
                        item.IsSubmitBL = true;
                        if (chkAck.Checked)
                            item.IsAcknowledgeBL = true;

                        if (chkIntegrity.Checked)
                            item.IsIntegrityBL = true;


                        item.IntegrityClauseENBL = @"<p>I/Company or our servants hereby declare that I/company or our servants will not offer a bribe to Johor Port Authority&rsquo;s servants or other individual which involved direct or indirect business practice to get the approved license.</p>
<p>If I/Company or servants is found to have violated or involved in violation of the integrity pact of any corrupt business practice, then I/Company or servants shall be entitled to:</p>
<p>Termination of the license or<br />Blacklisted and<br />Disciplinary action following by Malaysian government procurement regulations<br />If I/Company or our servants receive an offer/ a bribe from Johor Port Authority&rsquo;s servants or other individual which involved direct or indirect to give the approved license, I/Company or our servants promises that I/Company or our servants will report to Malaysian Anti-Corruption Commission (MACC) or police station immediately.</p>";
                        item.IntegrityClauseMYBL = @"<p>Saya/ Syarikat dengan ini mengisytiharkan bahawa saya atau mana-mana individu dalam yang mewakili syarikat ini tidak akan menawar atau memberi rasuah kepada mana-mana individu dalam Lembaga Pelabuhan Johor atau mana-mana individu lain, sebagai ganjaran mendapatkan kelulusan lesen seperti di atas.</p>
<p>Sekiranya saya atau mana-mana individu yang mewakili syarikat ini di dapati bersalah menawar atau memberi rasuah kepada mana-mana individu dalam Lembaga Pelabuhan Johor atau mana-mana individu lain sebagai ganjaran mendapatkan kelulusan lesen seperti di atas, maka saya sebagai wakil syarikat bersetuju tindakan-tindakan berikut diambil :</p>
<p>Penarikan balik lesen aktiviti pelabuhan; dan<br />Disenarai hitam untuk mohon lesen aktiviti pelabuhan; atau<br />Lain-lain tindakan tatatertib mengikut peraturan Perolehan Kerajaan.<br />Sekiranya terdapat mana-mana individu cuba meminta rasuah daripada saya atau mana-mana individu yang berkaitan dengan syarikat ini sebagai ganjaran mendapatkan sebut harga seperti di atas, maka saya berjanji akan dengan segera melaporkan perbuatan tersebut kepada pejabat Suruhanjaya Rasuah Malaysia(SPRM) atau balai polis yang berhampiran.</p>";

                        item.UpdatedBy = Session["UserID"].ToString();
                        item.UpdatedDate = DateTime.Now;
                        dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", item.OperationAppID);


                        OperationAppFlow appFlowPendingBL = dbContext.OperationAppFlows.Where(w => w.OperationAppID == item.OperationAppID && w.FlowActionStatusID == sysParam.FlowPendingBL && w.IsActive==true).FirstOrDefault<OperationAppFlow>();

                        if (appFlowPendingBL.IsActive == true)
                        {
                            appFlowPendingBL.ActionBy = Session["UserID"].ToString();
                            appFlowPendingBL.ActionDate = DateTime.Now;
                            appFlowPendingBL.IsActive = false;

                            dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppFlowID", item.OperationAppID);

                            //Create Submit Declaration
                            OperationAppFlow appFlowSubmitDec = new OperationAppFlow();
                            appFlowSubmitDec.OperationAppFlowID = Guid.NewGuid();
                            appFlowSubmitDec.OperationAppID = item.OperationAppID;
                            appFlowSubmitDec.FlowActionStatusID = sysParam.FlowSubmitDec;
                            appFlowSubmitDec.IsActive = false;
                            appFlowSubmitDec.CreatedBy = Session["UserID"].ToString();
                            appFlowSubmitDec.CreatedDate = DateTime.Now.AddSeconds(1);
                            appFlowSubmitDec.ActionBy = Session["UserID"].ToString();
                            appFlowSubmitDec.ActionDate = DateTime.Now.AddSeconds(1);

                            dbContext.OperationAppFlows.Add(appFlowSubmitDec);
                            dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppFlowID", item.OperationAppID);


                            OperationAppFlow appFlowComplete = new OperationAppFlow();
                            appFlowComplete.OperationAppFlowID = Guid.NewGuid();
                            appFlowComplete.OperationAppID = item.OperationAppID;
                            appFlowComplete.FlowActionStatusID = sysParam.FlowComplete;
                            appFlowComplete.IsActive = true;
                            appFlowComplete.CreatedBy = Session["UserID"].ToString();
                            appFlowComplete.CreatedDate = DateTime.Now;
                            appFlowComplete.ActionBy = Session["UserID"].ToString();
                            appFlowComplete.ActionDate = DateTime.Now.AddSeconds(2);
                            dbContext.OperationAppFlows.Add(appFlowComplete);
                            dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppFlowID", item.OperationAppID);

                            Response.Redirect("~//Operation/ApplicantDashboardB.aspx", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
            }
        }

        private bool isValidate(OperationApp item)
        {
            try
            {
                if (dtETA.Value == null)
                {
                    lblErrMsg.Text = "Please enter ETA Date";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    dtETA.Focus();
                    return false;
                }
                if (dtETD.Value == null)
                {
                    lblErrMsg.Text = "Please enter ETD Date";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    dtETD.Focus();
                    return false;
                }
                if (dtOperationDate.Value == null)
                {
                    lblErrMsg.Text = "Please enter Operation Date";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    dtOperationDate.Focus();
                    return false;
                }
                if (operationTime.Value == null)
                {
                    lblErrMsg.Text = "Please enter Operation Time";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    dtOperationDate.Focus();
                    return false;
                }
                if (txtMT.Text == "")
                {
                    lblErrMsg.Text = "Please enter Amount Delivered";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtMT.Focus();
                    return false;
                }
                else if (Convert.ToDecimal(txtMT.Text) <= 0)
                {
                    lblErrMsg.Text = "Please enter valid value of Amount Delivered";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtMT.Focus();
                    return false;
                }
                if ((item.CMAttachLink == "" || item.CMAttachLink == null) && (item.BLAttachLink == "" || item.BLAttachLink == null))
                {
                    lblErrMsg.Text = "Please attach Cargo Manifest/Bill Of Lading";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtCMNo.Focus();
                    return false;

                    //if (txtBLNo.Text.ToString() == "")
                    //{
                    //    lblErrMsg.Text = "Please enter Bill of lading";
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    //    txtBLNo.Focus();
                    //    return false;
                    //}
                }
                //if (divUploadCM.Visible == true)
                //{
                //    if (txtCMNo.Text.ToString() == "")
                //    {
                //        lblErrMsg.Text = "Please enter Cargo Manifest";
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                //        txtCMNo.Focus();
                //        return false;
                //    }

                //}
                if (!chkAck.Checked)
                {
                    lblErrMsg.Text = "Please check Company Acknowledgement & Integrity Clause before you submit.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    chkAck.Focus();
                    return false;
                }
                if (!chkIntegrity.Checked)
                {
                    lblErrMsg.Text = "Please check Company Acknowledgement & Integrity Clause before you submit.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    chkIntegrity.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
            }
            return true;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~//Operation/ApplicantDashboardB.aspx", false);
        }

        public static bool PendingEmailSTS(Guid? operationAppID, Guid? FlowActionStatusID, bool isReject)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            bool result;
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    PendingEmail emailLog = new PendingEmail();
                    emailLog.PendingMailID = Guid.NewGuid();
                    emailLog.RefID = operationAppID;
                    emailLog.RefFlowID = FlowActionStatusID;
                    emailLog.IsReject = isReject;
                    emailLog.LogDate = new DateTime?(DateTime.Now);
                    emailLog.IsSend = new bool?(false);
                    dbContext.PendingEmails.Add(emailLog);
                    dbContext.SaveChanges();
                }

                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri("");

                //    HttpContent content = new StringContent(
                //       JsonConvert.SerializeObject(emailLog),
                //       Encoding.UTF8,
                //       "application/json"
                //   );
                //    var responseTask = client.PostAsync();

                //    responseTask.Wait();

                //    var res = responseTask.Result;

                //    HttpClient httpClient = new HttpClient();

                //    HttpResponseMessage response =await httpClient.PostAsync("http://172.16.8.10:8181/api/email/e812eef2-42fe-4da0-95ae-0b45c6df2829", content);
                //    string statusCode = response.StatusCode.ToString();
                //}
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, "DALOperation", MethodBase.GetCurrentMethod().Name.ToString());
                result = false;
                return result;
            }
            result = true;
            return result;
        }

    }
}