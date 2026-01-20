using Apps.Auth;
using Apps.Common;
using DevExpress.Web.Bootstrap;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.SystemSetup
{
    public partial class AccessGroupModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
            {
                Response.Redirect("~//SignIn.aspx", true);
            }
            if (cbAccessGroup.Value != null)
            {
                eSTS.DAL.DALAccess obj = new eSTS.DAL.DALAccess();
                DataSet ds = obj.GetAccessGroupModule();

                grid.DataSource = ds;
                grid.DataBind();
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
                    item.ModuleDesc = e.NewValues["ModuleDesc"].ToString();
                    if (e.NewValues["ParentID"] != null)
                        item.ParentID = new Guid(e.NewValues["ParentID"].ToString());
                    item.ModuleLevel = Convert.ToInt32(e.NewValues["ModuleLevel"].ToString());
                    item.ModuleSeq = Convert.ToInt32(e.NewValues["ModuleSeq"].ToString());
                    item.ModuleLink = e.NewValues["ModuleLink"].ToString();
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

                    item.ModuleDesc = e.NewValues["ModuleDesc"].ToString();
                    item.ParentID = new Guid(e.NewValues["ParentID"].ToString());
                    item.ModuleLevel = Convert.ToInt32(e.NewValues["ModuleLevel"].ToString());
                    item.ModuleSeq = Convert.ToInt32(e.NewValues["ModuleSeq"].ToString());
                    item.ModuleLink = e.NewValues["ModuleLink"].ToString();
                    item.Icon = e.NewValues["Icon"].ToString();

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

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                eSTS.DAL.DALAccess obj = new eSTS.DAL.DALAccess();
                DataSet ds = obj.GetAccessGroupModule();

                grid.DataSource = ds;
                grid.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void grid_DataBound(object sender, EventArgs e)
        {
            try
            {
                string accessGroup = cbAccessGroup.Value.ToString();

                grid.ExpandAll();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    //int id = Convert.ToInt32(grid.GetRowValues(i, "UniqueId"));

                    //if (id == 22)
                    //    id = 0;
                    object keyValue;
                    keyValue = grid.GetRowValues(i, "lvl2id");
                    if (keyValue.ToString() == "")
                    {
                        keyValue = grid.GetRowValues(i, "lvl1id");

                        if (keyValue.ToString() == "")
                        {
                            keyValue = grid.GetRowValues(i, "lvl0id");
                        }
                    }
                    eSTS.DAL.DALAccess obj = new eSTS.DAL.DALAccess();
                    int result = obj.CheckExist(accessGroup, keyValue.ToString());


                    if (result > 0)
                    {
                        grid.Selection.SelectRow(i);
                    }
                    else
                    {
                        grid.Selection.UnselectRow(i);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, System.IO.Path.GetFileName(Request.PhysicalPath), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void gridGroupModule_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                BootstrapGridView grid = (BootstrapGridView)sender;
                int key = Convert.ToInt32(e.Parameters.Split('|')[0]);
                bool isSelected = Convert.ToBoolean(e.Parameters.Split('|')[1]);

                string accessGroup = "";
                if (cbAccessGroup.Value != null)
                {
                    accessGroup = cbAccessGroup.Value.ToString();
                }
                else
                    cbAccessGroup.ValidationSettings.RequiredField.IsRequired = true;

                eSTS.DAL.DALAccess obj = new eSTS.DAL.DALAccess();

                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    v_AccessModule accessModule = dbContext.v_AccessModule.Where(w => w.UniqueId == key).FirstOrDefault<v_AccessModule>();
                    if (accessModule.lvl2ID != null)
                    {
                        string accessModuleID = accessModule.lvl2ID.ToString();

                        AddRemoveAccessGroupModule(accessModuleID, accessGroup, isSelected);
                    }
                    if ( accessModule.lvl1ID != null)
                    {
                        string accessModuleID = accessModule.lvl1ID.ToString();

                        AddRemoveAccessGroupModule(accessModuleID, accessGroup, isSelected);
                    }
                    if (  accessModule.lvl0ID != null)
                    {
                        string accessModuleID = accessModule.lvl0ID.ToString();

                        AddRemoveAccessGroupModule(accessModuleID, accessGroup, isSelected);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                //General.SetLabelMessage(ref lblMsg, " Access module for group " + cbAccessGroup.Text, General.MsgType.SaveErr);
            }
        }

        private void AddRemoveAccessGroupModule(string accessModuleID, string accessGroup, bool isSelected)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid? accessGroupID = new Guid(accessGroup);
                    Guid? moduleID = new Guid(accessModuleID);

                    eSTS.Database.AccessGroupModule accessGroupModule = dbContext.AccessGroupModules.Where(w => w.AccessGroupID == accessGroupID && w.ModuleID== moduleID).FirstOrDefault<eSTS.Database.AccessGroupModule>();

                    if (accessGroupModule == null) //Not Exist
                    {
                        if (isSelected) //if Select
                        {
                            //Insert
                            eSTS.Database.AccessGroupModule agm = new eSTS.Database.AccessGroupModule();

                            agm.AccessGroupModuleID = Guid.NewGuid();
                            agm.AccessGroupID = accessGroupID;
                            agm.ModuleID = moduleID;
                            dbContext.AccessGroupModules.Add(agm);
                            dbContext.SaveChanges();//new Guid(Session["AccessID"].ToString()), "access_group_id");
                        }
                    }
                    else //Exist
                    {
                        if (!isSelected) //if Select
                        {
                            //Remove

                            dbContext.AccessGroupModules.Remove(accessGroupModule);
                            dbContext.SaveChanges();// new Guid(Session["AccessID"].ToString()), "access_group_id");
                        }
                    }
                    BuildMenu(accessGroupID);
                }
               
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void BuildMenu(Guid? accessGroupID)
        {
            DALAccessModule objMain = new DALAccessModule();

            if (!objMain.LoadByGroup(accessGroupID.ToString()))
                return;

            string UserManualLink = objMain.GetUserManual(accessGroupID.ToString());

            DataTable dt = objMain.ds.Tables["access_module"];
            DataRow[] parentMenus = dt.Select("ParentID is NULL");

            var sb = new StringBuilder();
            string unorderedList = "";
            unorderedList = GenerateUL(parentMenus, dt, sb, UserManualLink);

            using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
            {
                eSTS.Database.AccessGroup ag = dbContext.AccessGroups.Where(w => w.AccessGroupID == accessGroupID).FirstOrDefault<eSTS.Database.AccessGroup>();

                ag.Menu = unorderedList;
                dbContext.SaveChanges();
            }

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
                        //if (menuText == ViewState["ParentName"].ToString()) //Expand true
                        //    line = String.Format(@"<li><a href='{0}' aria-expanded='true'><i class='{2}'></i> <span class='nav-label'>{1}</span><span class='fa arrow'></span></a>", handler, menuText, icon);
                        //else
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
                UserManualLink = baseUrl + UserManualLink.Replace("\\", "/");
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

    }
}