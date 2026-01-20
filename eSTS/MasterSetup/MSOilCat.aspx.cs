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
    public partial class MSOilCat : System.Web.UI.Page
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
                    eSTS.Database.MSOilCategory item = new eSTS.Database.MSOilCategory();

                    item.OilCategoryID = Guid.NewGuid();
                    item.OilCategory = e.NewValues["OilCategory"].ToString();
                    
                    item.IsActive = Convert.ToBoolean(e.NewValues["IsActive"]);
                    item.CreatedBy = Session["UserID"].ToString();
                    item.CreatedDate = DateTime.Now;

                    dbContext.MSOilCategories.Add(item);
                    dbContext.SaveChanges(Session["UserID"].ToString(), "OilCategoryID", item.OilCategoryID);
                    dsOilCat.DataBind();

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

                    var item = dbContext.MSOilCategories.Find(new Guid(e.Keys[0].ToString()));

                    item.OilCategory = e.NewValues["OilCategory"].ToString();

                    item.IsActive = Convert.ToBoolean(e.NewValues["IsActive"]);

                    item.UpdatedBy = Session["UserID"].ToString();
                    item.UpdatedDate = DateTime.Now;

                    dbContext.SaveChanges();
                    dsOilCat.DataBind();

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