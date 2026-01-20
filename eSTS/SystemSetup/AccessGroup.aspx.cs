using Apps.Common;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.SystemSetup
{
    public partial class AccessGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
            {
                Session.Abandon();
                Response.Redirect("~//SignIn.aspx", true);
            }
        }

        protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    eSTS.Database.AccessGroup item = new eSTS.Database.AccessGroup();

                    item.AccessGroupID = Guid.NewGuid();
                    item.AccessGroupDesc = e.NewValues["AccessGroupDesc"].ToString();
                    item.AccessGroupName = e.NewValues["AccessGroupName"].ToString();
                    item.MainPageId = new Guid(e.NewValues["MainPageId"].ToString());

                    if (e.NewValues["PortLoc"] != null)
                        item.PortLoc = Convert.ToInt32(e.NewValues["PortLoc"].ToString());
                    if(e.NewValues["PermitIssuerID"]!=null)
                        item.PermitIssuerID= new Guid(e.NewValues["PermitIssuerID"].ToString());
                    if (e.NewValues["IsEmailGroup"] != null)
                        item.IsEmailGroup = Convert.ToBoolean(e.NewValues["IsEmailGroup"]);
                    if (e.NewValues["EmailGroup"] != null)
                        item.EmailGroup = e.NewValues["EmailGroup"].ToString();

                    item.IsActive = Convert.ToBoolean(e.NewValues["IsActive"]);
                    item.CreatedBy = Session["UserID"].ToString();
                    item.CreatedDate = DateTime.Now;

                    dbContext.AccessGroups.Add(item);
                    //dbContext.SaveChanges(new Guid(Session["AccessID"].ToString()), "AccessGroupID");
                    dbContext.SaveChanges();
                    dsAccessGroup.DataBind();

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

                    var item = dbContext.AccessGroups.Find(new Guid(e.Keys[0].ToString()));

                    item.AccessGroupDesc = e.NewValues["AccessGroupDesc"].ToString();
                    item.AccessGroupName = e.NewValues["AccessGroupName"].ToString();
                    item.MainPageId = new Guid(e.NewValues["MainPageId"].ToString());
                    if (e.NewValues["PortLoc"] != null)
                        item.PortLoc = Convert.ToInt32(e.NewValues["PortLoc"].ToString());
                    item.PermitIssuerID = new Guid(e.NewValues["PermitIssuerID"].ToString());
                    item.IsEmailGroup = Convert.ToBoolean(e.NewValues["IsEmailGroup"]);
                    item.EmailGroup = e.NewValues["EmailGroup"].ToString();
                    item.IsActive = Convert.ToBoolean(e.NewValues["IsActive"]);

                    item.UpdatedBy = Session["UserID"].ToString();
                    item.UpdatedDate = DateTime.Now;

                    dbContext.SaveChanges();
                    dsAccessGroup.DataBind();

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