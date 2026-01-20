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
    public partial class AccessModule : System.Web.UI.Page
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
                    eSTS.Database.AccessModule item = new eSTS.Database.AccessModule();

                    item.ModuleID = Guid.NewGuid();
                    item.ModuleCode = e.NewValues["ModuleCode"].ToString();
                    item.ModuleDesc = e.NewValues["ModuleDesc"].ToString();
                    if (e.NewValues["ParentID"] != null)
                        item.ParentID = new Guid(e.NewValues["ParentID"].ToString());
                    if (e.NewValues["ModuleLevel"] != null)
                        item.ModuleLevel = Convert.ToInt32(e.NewValues["ModuleLevel"].ToString());
                    if (e.NewValues["ModuleSeq"] != null)
                        item.ModuleSeq = Convert.ToInt32(e.NewValues["ModuleSeq"].ToString());
                    item.ModuleTitle = e.NewValues["ModuleTitle"].ToString();
                    if (e.NewValues["ModuleLink"] != null)
                        item.ModuleLink = e.NewValues["ModuleLink"].ToString();
                    if (e.NewValues["Icon"] != null)
                        item.Icon = e.NewValues["Icon"].ToString();

                    item.IsActive = Convert.ToBoolean(e.NewValues["IsActive"]);
                    item.IsSetting = Convert.ToBoolean(e.NewValues["IsSetting"]);

                    //item.CreatedBy = new Guid(Session["AccessID"].ToString());
                    item.CreatedDate = DateTime.Now;

                    dbContext.AccessModules.Add(item);
                    //dbContext.SaveChanges(new Guid(Session["AccessID"].ToString()), "AccessGroupID");
                    dbContext.SaveChanges();
                    dsAccessModule.DataBind();

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

                    var item = dbContext.AccessModules.Find(new Guid(e.Keys[0].ToString()));

                    item.ModuleCode = e.NewValues["ModuleCode"].ToString();
                    item.ModuleDesc = e.NewValues["ModuleDesc"].ToString();
                    item.ParentID = new Guid(e.NewValues["ParentID"].ToString());
                    item.ModuleLevel = Convert.ToInt32(e.NewValues["ModuleLevel"].ToString());
                    item.ModuleSeq = Convert.ToInt32(e.NewValues["ModuleSeq"].ToString());
                    item.ModuleLink = e.NewValues["ModuleLink"].ToString();
                    item.Icon = e.NewValues["Icon"].ToString();
                    item.ModuleTitle = e.NewValues["ModuleTitle"].ToString();

                    item.IsActive = Convert.ToBoolean(e.NewValues["IsActive"]);
                    item.IsSetting = Convert.ToBoolean(e.NewValues["IsSetting"]);

                    item.UpdatedBy = Session["UserID"].ToString();
                    item.UpdatedDate = DateTime.Now;

                    dbContext.SaveChanges();
                    dsAccessModule.DataBind();

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