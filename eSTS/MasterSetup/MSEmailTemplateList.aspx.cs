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
    public partial class MSEmailTemplateList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
            {
                Response.Redirect("~//SignIn.aspx", true);
            }
        }

        protected void lilNew_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("MSEmailTemplate.aspx?mode=n");

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-plus'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "_self";
            link.ToolTip = "New Email Template";
        }
        protected void lilEdit_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("MSEmailTemplate.aspx?mode=e&sno={0}", DataBinder.Eval(container.DataItem, "EmailTempID"));

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-pencil'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "_self";
            link.ToolTip = "Edit Email Template";
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~//MasterSetup/MSemailtemplate.aspx?mode=n", false);
        }
    }
}