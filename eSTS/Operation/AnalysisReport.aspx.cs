using Apps.Common;
using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.Operation
{
    public partial class AnalysisReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
            {
                string defaultPage = System.Configuration.ConfigurationManager.AppSettings["DefaultPage"].ToString();
                Session.Abandon();
                Response.Redirect(defaultPage, true);
            }

            if (!Page.IsPostBack)
            {
                hfCompID.Value = Session["CompID"].ToString();
                LoadForm();
            }
        }
        private void LoadForm()
        {
            try
            {
                int startYear = 2022;
                int loop = (DateTime.Today.Year - startYear);// + 1;

                for (int i = 0; i <= loop; i++)
                {
                    BootstrapListEditItem item = new BootstrapListEditItem();
                    item.Text = (startYear + i).ToString();
                    item.Value = (startYear + i).ToString();
                    cbYear.Items.Add(item);
                    cbYear.Value = DateTime.Today.Year;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlDataSource.DataBind();
            SqlDataSource2.DataBind();
            SqlDataSource3.DataBind();

        }
    }
}