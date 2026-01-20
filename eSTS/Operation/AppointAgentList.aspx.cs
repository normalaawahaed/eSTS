using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.Operation
{
    public partial class AppointAgentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
            {
                Response.Redirect("~//SignIn.aspx", true);
            }
        }
        protected void lilEdit_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;
                contentUrl = string.Format("AppointAgent.aspx?mode=e&sno={0}", DataBinder.Eval(container.DataItem, "OpAppointAgentID"));
                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                                              //link.Text = "<i class='fa fa-eye fa-lg text-success' aria-hidden='true'>" + string.Format("{0}", "") + "</i>";
                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-pencil'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "Edit Application";
         
        }
    }
}