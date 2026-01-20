using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Apps.Auth;
using Apps.Common;


namespace eSTS
{
    public partial class SiteAdminLTE : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle();
            BuildMenu();

        }
        private void SetPageTitle()
        {
            try
            {
                DALAccessModule objMain = new DALAccessModule();
                string PageFileName = "";
                //Response.Write("Before PageFileName" + Request.Path);
                PageFileName = Request.Path.Replace(@"/", @"\").Replace(@"\hrmis\", "\\").Replace(@"\hrms\", "\\").Replace(@"\HRMIS_TEST\", "\\").Replace(@"\HRMS_TEST2\", "\\").Substring(1);// Path.GetFileName(Request.PhysicalPath);
                //Response.Write("page file name" + PageFileName);
                string pageTitle = ""; string pageLink = ""; string ParentName = ""; string parentLink = "";

                objMain.GetModuleDesc_ByModuleLink(ref pageTitle, ref pageLink, ref ParentName, ref parentLink, PageFileName + ".aspx");
                //lblPageTitle.Text = "&nbsp;";
                //if (ParentName != "")
                //    lblPageTitle.Text = ParentName + " - " + pageTitle;
                //else
                //    lblPageTitle.Text = pageTitle;
                //lblPageTitle2.Text = pageTitle;
                string title = "";
                if (ParentName != "")
                {
                    if (parentLink == "")
                        parentLink = pageLink;
                    title = "<ol class='breadcrumb'> " +
                        " <li><a href = '" + ResolveUrl("~/PersonalDashboard.aspx") + "' ><i class='fa fa-dashboard'></i> Home</a></li> " +
                        "<li><a href =  '" + ResolveUrl("~/" + parentLink) + "'> " + ParentName + " </a ></li > " +
                        "<li class='active'>" + pageTitle + "</li></ol>";
                }
                else
                {
                    title = "<ol class='breadcrumb'> " +
                        " <li><a href = '" + ResolveUrl("~/Default.aspx") + "' ><i class='fa fa-dashboard'></i> Home</a></li> " +
                        "<li class='active'>" + pageTitle + "</li></ol>";
                }

                this.lilTitle.Text = title;
                ViewState["ParentName"] = "System Setup";
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }
        private void BuildMenu()
        {
            DALAccessModule objMain = new DALAccessModule();

            if (Session["UserGroup"] == null)
                return;

            if (!objMain.LoadByGroup(Session["UserGroup"].ToString()))
                return;

            DataTable dt = objMain.ds.Tables["access_module"];
            DataRow[] parentMenus = dt.Select("ParentID is NULL");

            var sb = new StringBuilder();
            string unorderedList = GenerateUL(parentMenus, dt, sb);
            this.lilMenu.Text = unorderedList;

        }
        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            sb.AppendLine("<ul class='sidebar-menu' data-widget='tree'>");
            sb.AppendLine("<li class='header'>MAIN NAVIGATION</li>");


            if (menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    string handler = "";
                    handler = dr["ModuleLink"].ToString();
                    string menuText = "";
                    menuText = dr["ModuleDesc"].ToString();
                    string icon = "";
                    icon = dr["icon"].ToString();

                    if (handler.Trim() == "")
                        handler = "#";
                    else
                    {
                        string baseUrl = "";

                        #region Live
                        baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/') + "/";
                        #endregion
                        handler = baseUrl + handler.Replace("\\", "/");
                    }

                    string pid = dr["ModuleID"].ToString();
                    string parentId = dr["ParentID"].ToString();

                    DataRow[] subMenu = table.Select(String.Format("ParentID = '{0}'", pid));


                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {

                        //string line = String.Format(@"<li><a href='{0}' class='firstlevel' style='color:#000000'>{1}<span class='fa arrow'></span></a>", handler, menuText);

                        string line = "";
                        if (menuText == ViewState["ParentName"].ToString())
                            line = String.Format(@"<li class='treeview active menu-open'><a href='{0}'><i class='{2}'></i> <span>{1}</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>", handler, menuText, icon);
                        else
                            line = String.Format(@"<li class='treeview'><a href='{0}'><i class='{2}'></i> <span>{1}</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>", handler, menuText, icon);
                        sb.Append(line);

                        var subMenuBuilder = new StringBuilder();
                        sb.Append(GenerateULSecondLevel(subMenu, table, subMenuBuilder));

                        sb.Append("</li>");
                    }
                    else
                    {

                        string line = String.Format(@"<li><a href='{0}'><i class='{2}'></i> <span>{1}</span></a></li>", handler, menuText, icon);
                        sb.Append(line);
                    }
                    sb.Append("</li>");
                }
            }

            //sb.Append("<li><a href='#'><i class='fa fa-book'></i> <span>Documentation</span></a></li>");
            sb.Append("</ul>");
            return sb.ToString();
        }
        private string GenerateULSecondLevel(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            sb.AppendLine("<ul class='treeview-menu'>");
            if (menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    string handler = "";
                    handler = dr["ModuleLink"].ToString();
                    string menuText = "";
                    menuText = dr["ModuleDesc"].ToString();
                    string icon = "";
                    icon = dr["icon"].ToString();

                    //if (cookie.Value == "my")
                    //    transLib.MenuBM(ref menuText);

                    if (handler.Trim() == "")
                        handler = "#";
                    else
                    {
                        string baseUrl = "";

                        #region Live
                        baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/') + "/";
                        #endregion

                        handler = baseUrl + handler.Replace("\\", "/");
                    }

                    string pid = dr["ModuleID"].ToString();
                    string parentId = dr["ParentID"].ToString();

                    DataRow[] subMenu = table.Select(String.Format("ParentID = '{0}'", pid));
                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {
                        string line = "";

                        if (menuText == ViewState["ParentName"].ToString())
                            line = String.Format(@"<li class='treeview active menu-open'><a href='{0}'><i class={2}></i> <span>{1}</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>", handler, menuText, icon);
                        else
                            line = String.Format(@"<li class='treeview'><a href='{0}'><i class={2}></i> <span>{1}</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>", handler, menuText, icon);
                       
                        sb.Append(line);

                        //var subMenuBuilder = new StringBuilder();
                        //sb.Append(GenerateULThirdLevel(subMenu, table, subMenuBuilder));
                        //sb.Append("</li>");
                    }
                    else
                    {
                        string line = "";
                        line = String.Format(@"<li><a href='{0}'><i class='fa fa-caret-right'></i> {1}</a></li>", handler, menuText);
                        sb.Append(line);
                    }
                    //sb.Append("</li>");
                }
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}