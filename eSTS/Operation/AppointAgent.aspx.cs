using Apps.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eSTS.DAL;
using System.Data;
using eSTS.Database;
using System.IO;

namespace eSTS.Operation
{
    public partial class AppointAgent : System.Web.UI.Page
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
                    hfOpAppointAgentID.Value = Request.QueryString["sno"].ToString();
                    Session["mode"] = Request.QueryString["mode"].ToString();
                }
                else
                {
                    Session["mode"] = "n";
                }
                if (!Page.IsPostBack)
                {
                    FormControl(Session["mode"].ToString());
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void FormControl(string mode)
        {
            if (mode == "n")
            {
                //LoadForm(mode);
                btnSaveBO.Enabled = true;
                uploadFile.Enabled = false;
            }
            else if (mode == "e")
            {
                LoadForm(mode);
                cbSO.Enabled = false;
                btnSaveBO.Enabled = true;
                uploadFile.Enabled = true;
            }
        }
        private void LoadForm(string mode)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid OpAppointAgentID = new Guid(hfOpAppointAgentID.Value.ToString());
                    //if (mode == "n")
                    //{
                    //    v_AppointAgentNew item = dbContext.v_AppointAgentNew.Where(w => w.OpAppointAgentID == OpAppointAgentID).FirstOrDefault<v_AppointAgentNew>();
                    //}
                    //else
                    //{
                    v_AppointAgentNew item = dbContext.v_AppointAgentNew.Where(w => w.OpAppointAgentID == OpAppointAgentID).FirstOrDefault<v_AppointAgentNew>();

                    //}
                    cbSO.Value = item.SOCompID.ToString();
                    dtFromDate.Value = DateTime.Parse(item.AppointStartDate.Value.ToString());
                    dtToDate.Value = DateTime.Parse(item.AppointEndDate.Value.ToString());//  String.Format("{0:dd/MM/yyyy}", item.AppointEndDate);

                    var sb = new System.Text.StringBuilder();
                    //Upload File
                    if (item.AppointAttachLink != "" && item.AppointAttachLink != null)
                    {
                        sb.AppendLine("<a href='" + item.AppointAttachLink + "' target='_blank'><span class='corner'></span><div class='icon'><i class='fa fa-file text-info'></i>" +
                                        "</div><div class='file-name text-center'> Download Letter Of Appoinment </div></a>");

                        lilFile.Text = sb.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
      

        protected void btnSaveBO_Click(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    if (Session["mode"].ToString() == "e")
                    {
                        var item = dbContext.OpAppointAgents.Find(new Guid(hfOpAppointAgentID.Value.ToString()));

                        item.AppointStartDate = Convert.ToDateTime(dtFromDate.Value);
                        item.AppointEndDate = Convert.ToDateTime(dtToDate.Value);

                        item.UpdatedBy = Session["UserID"].ToString();
                        item.UpdatedDate = DateTime.Now;

                        dbContext.SaveChanges(Session["UserID"].ToString(), "OpAppointAgentID", item.OpAppointAgentID);
                    }
                    else
                    {
                        if (hfOpAppointAgentID.Value == null || hfOpAppointAgentID.Value.ToString()=="")
                        {
                            //Guid licCompanyID = new Guid(cbSO.Value.ToString());
                            //string SOCompID = dbContext.v_LicCompany.Where(w => w.LicCompanyID == licCompanyID).FirstOrDefault<v_LicCompany>().CompID; 
                            OpAppointAgent item = new OpAppointAgent();

                            item.OpAppointAgentID = Guid.NewGuid();
                            item.SACompID = Session["CompID"].ToString();
                            //item.SOLicenseID = licCompanyID;
                            item.SOCompID = cbSO.Value.ToString();
                            item.AppointStartDate = Convert.ToDateTime(dtFromDate.Value);
                            item.AppointEndDate = Convert.ToDateTime(dtToDate.Value);

                            item.CreatedBy = Session["UserID"].ToString();
                            item.CreatedDate = DateTime.Now;

                            dbContext.OpAppointAgents.Add(item);
                            dbContext.SaveChanges(Session["UserID"].ToString(), "OpAppointAgentID", item.OpAppointAgentID);


                            hfOpAppointAgentID.Value = item.OpAppointAgentID.ToString();

                            FormControl("e");
                        }
                        else
                        {
                            OpAppointAgent item = dbContext.OpAppointAgents.Find(new Guid(hfOpAppointAgentID.Value.ToString()));

                            item.AppointStartDate = Convert.ToDateTime(dtFromDate.Value);
                            item.AppointEndDate = Convert.ToDateTime(dtToDate.Value);

                            dbContext.SaveChanges(Session["UserID"].ToString(), "OpAppointAgentID", item.OpAppointAgentID);
                        }
                    }
                        dbContext.Dispose();
                }
                //System.Threading.Thread.Sleep(5000);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
            }
        }

        protected void btnSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (uploadFile.UploadedFiles.Count() == 0)
                {
                    lblErrMsg.Text = "Please select Letter of Appointment";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    uploadFile.Focus();
                    return;
                }
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    if (hfOpAppointAgentID.Value == null)
                    {
                        OpAppointAgent item = new OpAppointAgent();

                        item.OpAppointAgentID = Guid.NewGuid();
                        item.SACompID = Session["CompID"].ToString();
                        item.SOCompID = cbSO.Value.ToString();

                        item.AppointAttachLink = SaveAttach();

                        //Upload File
                        if (item.AppointAttachLink != "" && item.AppointAttachLink != null)
                        {
                            var sb1 = new System.Text.StringBuilder();
                            sb1.AppendLine("<a href='" + item.AppointAttachLink + "' target='_blank'><span class='corner'></span><div class='icon'><i class='fa fa-file text-info'></i>" +
                                            "</div><div class='file-name text-center'> Download Letter Of Appointment </div></a>");

                            lilFile.Text = sb1.ToString();

                        }

                        item.CreatedBy = Session["UserID"].ToString();
                        item.CreatedDate = DateTime.Now;

                        dbContext.OpAppointAgents.Add(item);
                        dbContext.SaveChanges(Session["UserID"].ToString(), "OpAppointAgentID", item.OpAppointAgentID);


                        hfOpAppointAgentID.Value = item.OpAppointAgentID.ToString();

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);
                    }
                    else
                    {
                        OpAppointAgent item = dbContext.OpAppointAgents.Find(new Guid(hfOpAppointAgentID.Value.ToString()));

                        item.AppointAttachLink = SaveAttach();

                        //Upload File
                        if (item.AppointAttachLink != "" && item.AppointAttachLink != null)
                        {
                            var sb1 = new System.Text.StringBuilder();
                            sb1.AppendLine("<a href='" + item.AppointAttachLink + "' target='_blank'><span class='corner'></span><div class='icon'><i class='fa fa-file text-info'></i>" +
                                            "</div><div class='file-name text-center'> Download Letter Of Appointment </div></a>");

                            lilFile.Text = sb1.ToString();

                        }


                        dbContext.SaveChanges(Session["UserID"].ToString(), "OpAppointAgentID", item.OpAppointAgentID);
                        dbContext.Dispose();

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);
                    }
                }
                var sb = new System.Text.StringBuilder();

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
            }
        }

        protected string SaveAttach()
        {
            string fileName = "";
            string fullfileDirectory = "";
            string extension = "";
            string OriginalFileName = "";
            bool folderExists;

            string UploadDirectory = "Upload/" + Session["CompID"].ToString() + "/AppointAgent";

            try
            {

                if (uploadFile.UploadedFiles.Count() > 0)
                {
                    //Upload File 
                    extension = uploadFile.UploadedFiles[0].FileName.Trim().Substring(uploadFile.UploadedFiles[0].FileName.Trim().LastIndexOf("."));
                    OriginalFileName = "AppointLetter" + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + extension;


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
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return fileName.Replace("~", "");
        }

   
    }
}