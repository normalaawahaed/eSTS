using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.License
{
    public partial class STSOperatorLicList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
            {
                Response.Redirect("~//SignIn.aspx", true);
            }
        }

        protected void dsLicComp_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            if (Convert.ToBoolean(Session["IsSTSOperator"]))
            {
                e.DataSource.WhereParameters.Clear();
                e.DataSource.Where = "it.[CompID]=@pCompID";
                e.DataSource.WhereParameters.Add("pCompID", DbType.String, Session["CompID"].ToString());
            }
        }

        protected void lilView_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;
            if (Convert.ToBoolean(Session["IsSTSOperator"]))
            {
                contentUrl = string.Format("STSOperatorLic.aspx?mode=e&sno={0}", DataBinder.Eval(container.DataItem, "LicCompanyID"));
                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                                              //link.Text = "<i class='fa fa-eye fa-lg text-success' aria-hidden='true'>" + string.Format("{0}", "") + "</i>";
                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-pencil'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "Edit Application";
            }
            else
            {
                contentUrl = string.Format("STSOperatorLic.aspx?mode=v&sno={0}", DataBinder.Eval(container.DataItem, "LicCompanyID"));
                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                                              //link.Text = "<i class='fa fa-eye fa-lg text-success' aria-hidden='true'>" + string.Format("{0}", "") + "</i>";
                link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-eye'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "View Application";
            }
        }
    }
}