using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS
{
    public partial class ResetAD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetADList();
        }

        public bool GetADList()
        {
            bool lPass = false;

            try
            {
                string ADHost = ConfigurationManager.AppSettings["ActiveDirectoryHost"].ToString();
                string ADUserName = ConfigurationManager.AppSettings["ADUserName"].ToString();
                string ADPassword = ConfigurationManager.AppSettings["ADPassword"].ToString();
                string ADGroup = ConfigurationManager.AppSettings["ActiveDirectoryGroup"].ToString();


                PrincipalContext context = new PrincipalContext(ContextType.Domain, ADHost, ADUserName, ADPassword);
                
                GroupPrincipal grp = GroupPrincipal.FindByIdentity(context, IdentityType.SamAccountName, ADGroup);

                if (grp != null)
                {
                    foreach (Principal p in grp.GetMembers(false))
                    {
                        UserPrincipal up = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, p.SamAccountName);
                        if (up == null)
                            continue;
                        if (up.EmailAddress != null)
                            cbUID.Items.Add(new DevExpress.Web.Bootstrap.BootstrapListEditItem(up.SamAccountName.ToString().ToLower(), up.SamAccountName.ToString().ToLower()));
                    }
                  
                    grp.Dispose();
                    context.Dispose();
                }
                else
                {
                    lblErrMsg.Text = "Group AD not found!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    lblErrMsg.Focus();
                }
                lPass = true;
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                lblErrMsg.Focus();
            }
            return lPass;
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                string ADHost = ConfigurationManager.AppSettings["ActiveDirectoryHost"].ToString();
                string ADUserName =  ConfigurationManager.AppSettings["ADUserName"].ToString();
                string ADPassword = ConfigurationManager.AppSettings["ADPassword"].ToString();

                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, ADHost, ADUserName, ADPassword))
                {
                    using (var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, cbUID.Value.ToString()))
                    {
                        if (user != null && user.IsAccountLockedOut())
                        {
                            user.UnlockAccount();
                        }
                        user.SetPassword(txtPwd.Value.ToString());
                        user.Save();
                        lblErrMsg.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);

                    }
                }

            }
            catch (Exception ex)
            {
                lblErrMsg.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                lblErrMsg.Focus();
                
            }
        }
    }
}