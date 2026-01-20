using Apps.Common;
using eSTS.DAL;
using System;
using eSTS.Database;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.Sync
{
    public partial class SyncUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                DALSync sync = new DALSync();
                if (sync.SyncUsers())
                {
                    gridUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void gridUsers_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                using (MMSSyncEntities dbContext = new MMSSyncEntities())
                {

                    var item = dbContext.Users.Find(new Guid(e.Keys[0].ToString()));

                   // item.AccessGroupID = new Guid(e.NewValues["AccessGroupID"].ToString());
                    item.STSAccessGroupID = new Guid(e.NewValues["STSAccessGroupID"].ToString());
                   

                    item.UpdatedBy = Session["UserID"].ToString();
                    item.UpdatedDate = DateTime.Now;

                    dbContext.SaveChanges();
                    dsUsers.DataBind();

                    gridUsers.CancelEdit();
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