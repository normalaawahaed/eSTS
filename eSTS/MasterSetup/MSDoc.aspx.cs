using Apps.Common;
using DevExpress.Web;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.MasterSetup
{
    public partial class MSDoc : System.Web.UI.Page
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
               
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void lilNew_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("MSDocDetails.aspx?mode=n");

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-plus'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "_self";
            link.ToolTip = "New Document";
        }
        protected void lilEdit_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("MSDocDetails.aspx?mode=e&did={0}&mid={1}", DataBinder.Eval(container.DataItem, "MSDocTypeID"),DataBinder.Eval(container.DataItem, "ModuleAttachID"));

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-pencil'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "_self";
            link.ToolTip = "Edit Document";
        }
    }
}