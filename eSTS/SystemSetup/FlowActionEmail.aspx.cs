using Apps.Common;
using DevExpress.Web.Bootstrap;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.SystemSetup
{
    public partial class FlowActionEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
            {
                Response.Redirect("~//SignIn.aspx", true);
            }
        }

        protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    eSTS.Database.FlowActionEmail item = new eSTS.Database.FlowActionEmail();

                    item.FlowActionEmailID = Guid.NewGuid();
                    item.FlowActionStatusID = new Guid(e.NewValues["FlowActionStatusID"].ToString());
                    item.EmailTempID = new Guid(e.NewValues["EmailTempID"].ToString());
                    item.IsApplicant = Convert.ToBoolean(e.NewValues["IsApplicant"].ToString());

                    if (e.NewValues["IsOperator"] != null)
                        item.IsOperator = Convert.ToBoolean(e.NewValues["IsOperator"].ToString());

                    if (e.NewValues["ReceiptAGID"] != null)
                        item.ReceiptAGID = new Guid(e.NewValues["ReceiptAGID"].ToString());


                    dbContext.FlowActionEmails.Add(item);
                    dbContext.SaveChanges();
                    dsFlowActionEmail.DataBind();

                    grid.CancelEdit();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    var item = dbContext.FlowActionEmails.Find(new Guid(e.Keys[0].ToString()));
                    item.FlowActionStatusID = new Guid(e.NewValues["FlowActionStatusID"].ToString());
                    item.EmailTempID = new Guid(e.NewValues["EmailTempID"].ToString());
                    item.IsApplicant = Convert.ToBoolean(e.NewValues["IsApplicant"].ToString());
                    if (e.NewValues["IsOperator"] != null)
                        item.IsOperator = Convert.ToBoolean(e.NewValues["IsOperator"].ToString());
                    if (e.NewValues["ReceiptAGID"] != null)
                        item.ReceiptAGID = new Guid(e.NewValues["ReceiptAGID"].ToString());


                    dbContext.SaveChanges();
                    dsFlowActionEmail.DataBind();

                    grid.CancelEdit();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

    }
}