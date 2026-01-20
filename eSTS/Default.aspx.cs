using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;
 

namespace eSTS
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string json = wc.DownloadString("http://bulk.blaster-pro.com/sendAPI.php?apikey=33b3bc47db5e274d1f09541386adb49f&number=60177373394&message=Please be informed you test is success");
                    DataSet ds = JObject.Parse(json)["root"].ToObject<DataSet>();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


}