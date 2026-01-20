using Apps.Common;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.MasterSetup
{
    public partial class MSDocDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
            {
                Response.Redirect("~//SignIn.aspx", true);
            }

            if (Request.QueryString.Count > 0)
            {
                hfMethod.Value = Request.QueryString["mode"].ToString();
                if (Request.QueryString["mode"].ToString() == "e")
                {
                    hfMSDocTypeID.Value = Request.QueryString["did"].ToString();
                    hfModuleAttachID.Value = Request.QueryString["mid"].ToString();
                }
            }
            if (!Page.IsPostBack)
            {
                if (hfMethod.Value == "e")
                {
                    LoadForm();
                }
            }
        }
        private void LoadForm()
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid id = new Guid(hfModuleAttachID.Value.ToString());
                    Database.v_SuppDoc obj = dbContext.v_SuppDoc.Where(w => w.ModuleAttachID == id).FirstOrDefault<Database.v_SuppDoc>();
                    txtCode.Text = obj.DocCode;
                    txtDesc.Text = obj.DocDesc;
                    cbModule.Value  = obj.ModuleID;
                    if (obj.DocStatus == 1)
                        chkStatus.Checked = true;
                    else
                        chkStatus.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
            
      
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    if (hfMethod.Value == "n")
                    {
                        eSTS.Database.MSDocType item = new eSTS.Database.MSDocType();

                        item.MSDocTypeID = Guid.NewGuid();
                        item.DocCode = txtCode.Text;
                        item.DocDesc = txtDesc.Text;
                        item.DocType = "Supporting Document";
                        if (chkStatus.Checked)
                            item.DocStatus = 1;
                        else
                            item.DocStatus = 0;

                        item.CreatedBy = Session["UserID"].ToString();
                        item.CreatedDate = DateTime.Now;

                        dbContext.MSDocTypes.Add(item);
                        dbContext.SaveChanges();

                        eSTS.Database.ModuleAttachDoc moduleAttachDoc = new eSTS.Database.ModuleAttachDoc();
                        moduleAttachDoc.ModuleAttachID = Guid.NewGuid();
                        moduleAttachDoc.ModuleID = cbModule.Value.ToString();
                        moduleAttachDoc.ModuleDesc = cbModule.Text;
                        moduleAttachDoc.AttachTypeID = item.MSDocTypeID;
                     
                        dbContext.ModuleAttachDocs.Add(moduleAttachDoc);
                        dbContext.SaveChanges();

                    }
                    else
                    {
                        Guid MSDocTypeID = new Guid(hfMSDocTypeID.Value.ToString());
                        var docType = dbContext.MSDocTypes.Find(MSDocTypeID);

                        docType.DocCode = txtCode.Text;
                        docType.DocDesc = txtDesc.Text;
                        if (chkStatus.Checked)
                            docType.DocStatus = 1;
                        else
                            docType.DocStatus = 0;
                        docType.UpdatedBy = Session["UserID"].ToString();
                        docType.UpdatedDate = DateTime.Now;

                        dbContext.SaveChanges();

                        Guid ModuleAttachID = new Guid(hfModuleAttachID.Value.ToString());
                        var module = dbContext.ModuleAttachDocs.Find(ModuleAttachID);

                        module.ModuleID = cbModule.Value.ToString();
                        module.ModuleDesc = cbModule.Text;
                        module.AttachTypeID = MSDocTypeID;
                        dbContext.SaveChanges();

                    }
                    Response.Redirect("~//MasterSetup/MSDoc.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = "Failed to Save";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);

                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}