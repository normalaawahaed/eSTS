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
using eSTS.Database;

namespace eSTS
{
    public partial class SiteInspinia : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SetPageTitle();
           // LoadMenu();
            BuildMenu();
            cmdLogOut.ServerClick += new EventHandler(cmdLogOut_Click);
        }
        private void SetPageTitle()
        {
            try
            {
                DALAccessModule objMain = new DALAccessModule();

               
                string PageFileName = "";
               // Response.Write("Before PageFileName" + Request.Path);
                string[] path = Request.Path.Split('/');
                PageFileName = path[path.Length - 1];
                //for(int i=0;i <= path.Length;i++)
                //{
                //    Response.Write(i + ")"+path[i]);
                //}
                //PageFileName = Request.Path.Replace(@"/", @"\").Replace(@"eSTS", "").Substring(1);// Path.GetFileName(Request.PhysicalPath);
                //Response.Write(" page file name" + PageFileName);
                string pageTitle = ""; string pageLink = ""; string ParentName = ""; string parentLink = "";
               
                objMain.GetModuleDesc_ByModuleLink(ref pageTitle, ref pageLink, ref ParentName, ref parentLink, PageFileName);

                // /*
                //* CHECKING ACCESS RIGHT PAGE
                //*/

                //if (!objMain.CheckAccessLink(Session["UserGroup"].ToString(), PageFileName + ".aspx"))
                //{
                //    Response.Redirect("~/NotAuthorize.html");
                //}

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
                    if (ParentName == pageTitle)
                    {
                        title = @" <h2> " + ParentName + " </h2><ol class='breadcrumb'> " +
                            " <li class='breadcrumb-item'><a href = '" + ResolveUrl("~/"+ pageLink + "") + "' ><i class='fa fa-dashboard'></i> Home</a></li> " +
                            "<li class='breadcrumb-item active'><strong>" + pageTitle + "</strong></li></ol>";
                    }
                    else
                    {
                        title = @" <h2> " + ParentName + " </h2><ol class='breadcrumb'> " +
                            " <li class='breadcrumb-item'><a href = '" + ResolveUrl("~/" + pageLink +"") + "' ><i class='fa fa-dashboard'></i> Home</a></li> " +
                            "<li class='breadcrumb-item'><a href =  '" + ResolveUrl("~/" + parentLink) + "'> " + ParentName + " </a ></li > " +
                            "<li class='breadcrumb-item active'><strong>" + pageTitle + "</strong></li></ol>";
                    }
                }
                else
                {
                    title = "<h2>" + pageTitle + "</h2><ol class='breadcrumb'> " +
                        " <li class='breadcrumb-item'><a href = '" + ResolveUrl("~/" + pageLink + "") + "' ><i class='fa fa-dashboard'></i> Home</a></li> " +
                        "<li class='breadcrumb-item active'><strong>" + pageTitle + "</strong></li></ol>";
                }

                this.lilBreadcrumb.Text = title;
                ViewState["ParentName"] = "System Setup";
                this.LoginUserFullName.Text = Session["FullName"].ToString();
                this.lblUserGroupDesc.Text = Session["CompanyName"].ToString();// Session["UserGroupDesc"].ToString();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private void LoadMenu()
        {
            if (Session["UserGroup"] == null || Session["UserGroup"].ToString() == "")
                return;
            var sb = new StringBuilder();
            Guid accessGroupID = new Guid(Session["UserGroup"].ToString());
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/') + "/";
            string UserManualLink = baseUrl + "DisplayUserManual.aspx"; //UserManualLink.Replace("\\", " /");
            string userManualLine = String.Format(@"<li class='landing_link'><a target='_blank' href='{0}'><i class='fa fa-question-circle'></i> <span class='nav-label'>User Manual</span> <span class='label label-warning float-right'>HELP?</span></a></li>", UserManualLink);

           
            using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
            {
                eSTS.Database.AccessGroup ag = dbContext.AccessGroups.Where(w => w.AccessGroupID == accessGroupID).FirstOrDefault<eSTS.Database.AccessGroup>();
                sb.Append(ag.Menu);
               // sb.Append(userManualLine);
                this.lilMenu.Text = sb.ToString();

            }

        }

        private void BuildMenu()
        {
            DALAccessModule objMain = new DALAccessModule();

            if (Session["UserGroup"] == null || Session["UserGroup"].ToString()=="")
                return;

            if (!objMain.LoadByGroup(Session["UserGroup"].ToString()))
                return;

            string UserManualLink = objMain.GetUserManual(Session["UserGroup"].ToString());

            DataTable dt = objMain.ds.Tables["access_module"];
            DataRow[] parentMenus = dt.Select("ParentID is NULL");

            var sb = new StringBuilder();
            string unorderedList = "";
            if (Session["unorderedList"] == null)
            {
                unorderedList = GenerateUL(parentMenus, dt, sb, UserManualLink);
                this.lilMenu.Text = unorderedList;
                Session["unorderedList"] = unorderedList;
            }
            else
                this.lilMenu.Text = Session["unorderedList"].ToString();

        }

        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb, string UserManualLink)
        {
            //sb.AppendLine("<li>);
            //sb.AppendLine("<li class='header'>MAIN NAVIGATION</li>");

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
                        if (menuText == ViewState["ParentName"].ToString()) //Expand true
                            line = String.Format(@"<li><a href='{0}' aria-expanded='true'><i class='{2}'></i> <span class='nav-label'>{1}</span><span class='fa arrow'></span></a>", handler, menuText, icon);
                        else
                            line = String.Format(@"<li><a href='{0}' aria-expanded='true'><i class='{2}'></i> <span class='nav-label'>{1}</span><span class='fa arrow'></span></a>", handler, menuText, icon);
                        sb.Append(line);

                        var subMenuBuilder = new StringBuilder();
                        sb.Append(GenerateULSecondLevel(subMenu, table, subMenuBuilder));

                        sb.Append("</li>");
                    }
                    else
                    {

                        string line = String.Format(@"<li><a href='{0}'><i class='{2}'></i> <span class='nav-label'>{1}</span></a></li>", handler, menuText, icon);
                        sb.Append(line);
                    }
                    sb.Append("</li>");
                }
            }

            //sb.Append("<li><a href='#'><i class='fa fa-book'></i> <span>Documentation</span></a></li>");
            if (UserManualLink != "")
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/') + "/";
                UserManualLink = baseUrl + "DisplayUserManual.aspx"; //UserManualLink.Replace("\\", " /");
                string userManualLine = String.Format(@"<li class='landing_link'><a target='_blank' href='{0}'><i class='fa fa-question-circle'></i> <span class='nav-label'>User Manual</span> <span class='label label-warning float-right'>HELP?</span></a></li>", UserManualLink);
               
                sb.Append(userManualLine);
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
        
        private string GenerateULSecondLevel(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            sb.AppendLine("<ul class='nav nav-second-level collapse' aria-expanded='true'>");

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
                        //string line = "";

                        //if (menuText == ViewState["ParentName"].ToString())
                        //    line = String.Format(@"<li class='treeview active menu-open'><a href='{0}'><i class={2}></i> <span>{1}</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>", handler, menuText, icon);
                        //else
                        //    line = String.Format(@"<li class='treeview'><a href='{0}'><i class={2}></i> <span>{1}</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>", handler, menuText, icon);
                        ////string line = String.Format(@"<li><a href='{0}'>{1}</a>", handler, menuText);
                        ////string line = String.Format(@"<li class='active'><a href='{0}'><i class='fa fa-circle-o'></i> {1}</a></li>", handler, menuText);

                        //sb.Append(line);

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

        protected void cmdLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    string UserID = Session["UserID"].ToString();
                    string UserGroup = Session["UserGroup"].ToString();

                    Log.WriteUserAccessLog(UserID, UserGroup, StandardDefinition.AccessType.Logout);
                }

                Session.Clear();
                Response.Redirect("~//SignIn.aspx");
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, "Site.Master", "checkSession()");
            }
        }
    }
}