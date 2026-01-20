using Apps.Common;
using DevExpress.Web;
using eSTS.DAL;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.License
{
    public partial class STSOperatorLic : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string mode = "";
                //Check Session
                if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
                {
                    Response.Redirect("~//SignIn.aspx", true);
                }
                if (Request.QueryString.Count > 0)
                {
                    hfLicCompID.Value = Request.QueryString["sno"].ToString();
                    mode = Request.QueryString["mode"].ToString();
                }
                else
                {
                    mode = "n";
                }
                if (!Page.IsPostBack)
                {
                    LoadForm();
                    FormControl(mode);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void FormControl(string mode)
        {
            if(mode=="n")
            {
                btnAddShip.Enabled = false;
                cbAttachType.Enabled = false;
                uploadFile.Enabled = false;
                btnSaveAttachDoc.Enabled = false;
            }
            else if (mode == "e")
            {
                btnAddShip.Enabled = true;
                cbAttachType.Enabled = true;
                uploadFile.Enabled = true;
                btnSaveAttachDoc.Enabled = true;
            }
        }
        private void LoadForm()
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid licCompID = new Guid(hfLicCompID.Value.ToString());
                    v_LicCompany obj = dbContext.v_LicCompany.Where(w => w.LicCompanyID == licCompID).FirstOrDefault<v_LicCompany>();

                    lblCompanyName.Text = obj.CompanyName;
                    lblLocation.Text = obj.PortName;
                 

                    gridShip.DataBind();
                    gridAttach.DataBind();
                   
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

    
    
       #region Attachment
        protected void btnSaveAttachDoc_Click(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    eSTS.Database.LicCompanyAttach item = new eSTS.Database.LicCompanyAttach();
                    Guid id = new Guid(cbAttachType.Value.ToString());
                    Database.v_SuppDoc docType = dbContext.v_SuppDoc.Where(w => w.MSDocTypeID == id).FirstOrDefault<Database.v_SuppDoc>();

                    item.AttachID = Guid.NewGuid();
                    item.LicCompanyID = new Guid(hfLicCompID.Value.ToString());
                    item.AttachTypeID = new Guid(cbAttachType.Value.ToString());
                    item.RefNo = txtRefNo.Text;
                    item.ValidFrom = Convert.ToDateTime(dtValidFrom.Value);
                    item.ValidTo = Convert.ToDateTime(dtValidTo.Value);
                    item.Path = SaveAttach(docType.DocCode,item.LicCompanyID.ToString());

                    item.CreatedBy =  Session["UserID"].ToString();
                    item.CreatedDate = DateTime.Now;

                    dbContext.LicCompanyAttaches.Add(item);
                    dbContext.SaveChanges();// new Guid(Session["AccessID"].ToString()), "AttachID");

                    dsAttach.DataBind();
                    gridAttach.DataBind();

                    txtRefNo.Text = "";
                    dtValidFrom.Text = "";
                    dtValidTo.Text = "";
                }
                
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected string SaveAttach(string docCode, string id)
        {
            string fileName = "";
            string fullfileDirectory = "";
            string extension = "";
            string OriginalFileName = "";
            bool folderExists;
            string UploadDirectory = "Upload/" + Session["CompID"].ToString() + "/License";

            try
            {

                if (uploadFile.UploadedFiles.Count() > 0)
                {
                    
                    //Upload File 
                    extension = uploadFile.UploadedFiles[0].FileName.Trim().Substring(uploadFile.UploadedFiles[0].FileName.Trim().LastIndexOf("."));
                    OriginalFileName = docCode +"_"+id+ DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + extension;// uploadFile.UploadedFiles[0].FileName.Trim();



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
                fileName = "~/License/" + fileName;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return fileName;//.Replace("~", "");
        }

        protected void dsAttach_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                if (hfLicCompID.Value != "")
                    e.DataSource.WhereParameters["pLicCompanyID"].DefaultValue = hfLicCompID.Value.ToString();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void dsAttachMMS_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                if (hfLicCompID.Value != "")
                    e.DataSource.WhereParameters["pLicenseID"].DefaultValue = hfLicCompID.Value.ToString();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void gridAttach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    var licCompAttachID = dbContext.LicCompanyAttaches.Find(new Guid(e.Keys[0].ToString()));

                    dbContext.LicCompanyAttaches.Remove(licCompAttachID);

                    dbContext.SaveChanges();

                    gridAttach.DataBind();
                    gridAttach.CancelEdit();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #endregion
        protected void gridAttach_CommandButtonInitialize(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewCommandButtonEventArgs e)
        {
            if (Convert.ToBoolean(Session["IsSTSOperator"]))
            {
                if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
                    e.Visible = false;
            }
        }
       protected void dsBunkerOperator_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            if (Convert.ToBoolean(Session["IsSTSOperator"]))
            {
                e.DataSource.WhereParameters.Clear();
                e.DataSource.Where = "it.[CompID]=@pCompID";
                e.DataSource.WhereParameters.Add("pCompID", DbType.String, Session["CompID"].ToString());
            }

        }

        protected void gridShip_CommandButtonInitialize(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewCommandButtonEventArgs e)
        {
            if (Convert.ToBoolean(Session["IsSTSOperator"]))
            {
                if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
                    e.Visible = false;
            }
        }

        protected void lilView_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("ShipLicenseInfo.aspx?m=v&lno={0}&sid={1}", DataBinder.Eval(container.DataItem, "LicCompanyID"), DataBinder.Eval(container.DataItem, "LicCompanyVesselID"));

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-eye'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "blank";
            link.ToolTip = "View Ship Details";
        }
        protected void lilEdit_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-pencil'></i></button>" + string.Format("{0}", "") + "</i>";
            link.NavigateUrl = string.Format("ShipLicenseInfo.aspx?m=e&lno={0}&sid={1}", DataBinder.Eval(container.DataItem, "LicCompanyID"), DataBinder.Eval(container.DataItem, "LicCompanyVesselID"));
            link.ToolTip = "Edit Ship Details";
            link.Target = "blank";
        }

        
    }
}