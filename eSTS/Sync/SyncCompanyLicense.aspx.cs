using Apps.Common;
using eSTS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.Sync
{
    public partial class SyncCompanyLicense : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                DALSync sync = new DALSync();
                if (sync.SyncLicense())
                {
                    gridLicCompany.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}