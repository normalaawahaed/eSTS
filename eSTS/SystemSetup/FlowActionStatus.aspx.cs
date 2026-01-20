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
    public partial class FlowActionStatus : System.Web.UI.Page
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
                    eSTS.Database.FlowActionStatu newObj = new eSTS.Database.FlowActionStatu();

                    newObj.FlowActionStatusID = Guid.NewGuid();
                    newObj.ActionStatusSeq = Convert.ToInt32(e.NewValues["ActionStatusSeq"].ToString());
                    newObj.ActionStatus = e.NewValues["ActionStatus"].ToString();
                    newObj.LabelColor = e.NewValues["LabelColor"].ToString();
                    newObj.IsHideFlow = Convert.ToBoolean(e.NewValues["IsHideFlow"]);
                    newObj.IsActive = Convert.ToBoolean(e.NewValues["IsActive"]);

                    newObj.CreatedDate = DateTime.Now;
                    newObj.CreatedBy = Session["UserID"].ToString();

                    dbContext.FlowActionStatus.Add(newObj);
                    dbContext.SaveChanges();// new Guid(Session["AccessID"].ToString()), "FlowModuleLayerID");

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
                    var obj = dbContext.FlowActionStatus.Find(new Guid(e.Keys["FlowModuleLayerID"].ToString()));

                    obj.ActionStatusSeq = Convert.ToInt32(e.NewValues["ApprovalSeq"].ToString());
                    obj.ActionStatus = e.NewValues["ActionStatus"].ToString();
                    obj.LabelColor = e.NewValues["LabelColor"].ToString();
                    obj.IsHideFlow = Convert.ToBoolean(e.NewValues["IsHideFlow"]);
                    obj.IsActive = Convert.ToBoolean(e.NewValues["IsActive"]);

                    obj.UpdatedDate = DateTime.Now;
                    obj.UpdatedBy = Session["UserID"].ToString();

                    dbContext.SaveChanges();// new Guid(Session["AccessID"].ToString()), "FlowModuleLayerID");
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