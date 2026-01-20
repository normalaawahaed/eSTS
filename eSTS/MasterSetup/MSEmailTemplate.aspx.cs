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
    public partial class MSEmailTemplate : System.Web.UI.Page
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
                    hfEmailTempID.Value = Request.QueryString["sno"].ToString();
               
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
                    Guid moduleID = new Guid(hfEmailTempID.Value.ToString());
                    Database.EmailTemplate obj = dbContext.EmailTemplates.Where(w => w.EmailTempID == moduleID).FirstOrDefault<Database.EmailTemplate>();
                    txtCode.Text = obj.TemplateCode;
                    txtSubject.Text = obj.TempSubject;
                    EmailEditor.Html = obj.TempBody;
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
                        Database.EmailTemplate newObj = new Database.EmailTemplate();

                        newObj.EmailTempID = Guid.NewGuid();
                        newObj.TemplateCode = txtCode.Text;
                        newObj.TempSubject = txtSubject.Text;
                        newObj.TempBody = EmailEditor.Html;

                        newObj.CreatedDate = DateTime.Now;
                        newObj.CreatedBy = Session["UserID"].ToString();

                        dbContext.EmailTemplates.Add(newObj);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        Guid moduleID = new Guid(hfEmailTempID.Value.ToString());

                        Database.EmailTemplate obj = dbContext.EmailTemplates.Where(w => w.EmailTempID == moduleID).FirstOrDefault<Database.EmailTemplate>();


                        obj.TemplateCode = txtCode.Text;
                        obj.TempSubject = txtSubject.Text;
                        obj.TempBody = EmailEditor.Html;

                        obj.UpdateDate = DateTime.Now;
                        obj.UpdateBy = Session["UserID"].ToString();

                        dbContext.SaveChanges();
                    }
                      
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}