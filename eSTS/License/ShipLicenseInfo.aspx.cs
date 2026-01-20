using Apps.Common;
using DMSLatLongConverter;
using eSTS.DAL;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.License
{
    public partial class ShipLicenseInfo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string mode = "";
                //Check Session
                if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
                {
                    Response.Redirect("~//SignIn.aspx", true);
                }
                if (Request.QueryString.Count > 0)
                {
                    hfLicCompID.Value = Request.QueryString["lno"].ToString();
                    hfShipID.Value = Request.QueryString["sid"].ToString();
                    mode = Request.QueryString["m"].ToString();
                    //if (mode != "n")
                    //{
                    //    hfShipID.Value = Request.QueryString["cid"].ToString();
                    //}
                }
                if (!Page.IsPostBack)
                {
                    if (mode != "n")
                        LoadForm();
                    FormControl(mode);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void FormControl(string mode)
        {
            if (mode == "e")
            {
                btnPopupSearch.Enabled = false;
            }
            else if (mode == "n")
            {
                btnSaveAttachDoc.Enabled = false;
                btnPopupSearch.Enabled = true;
                cbAttachType.Enabled = true;
                uploadFile.Enabled = true;
                btnSaveAttachDoc.Enabled = true;
            }
            else if (mode == "v")
            {
                btnSaveBO.Visible = false;
                btnSaveAttachDoc.Visible = false;
                btnPopupSearch.Visible = false;
                cbAttachType.Enabled = false;
                uploadFile.Enabled = false;
                btnSaveAttachDoc.Visible = false;

                txtNRT.Enabled = false;
                txtGRT.Enabled = false;
                txtOffNo.Enabled = false;
                txtOffNo2.Enabled = false;
                txtLOA.Enabled = false;
                txtMMSINo.Enabled = false;
                txtLatDegree.Enabled = false;
                txtLatMin.Enabled = false;
                txtLongDegree.Enabled = false;
                txtLongMin.Enabled = false;
                txtCallSign.Enabled = false;
                txtLicenseNo.Enabled = false;
                dtValidFrom.Enabled = false;
                dtValidTo.Enabled = false;

                divAttachForm.Visible = false;
            }
        }
        private void LoadForm()
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid licVesselID = new Guid(hfShipID.Value.ToString());
                    v_OpLicCompanyVessel item = dbContext.v_OpLicCompanyVessel.Where(w => w.LicCompanyVesselID ==licVesselID).FirstOrDefault<v_OpLicCompanyVessel>();

                    lblExpDate.Text = Convert.ToDateTime(item.DtLicExp).ToString("dd/MM/yyyy");
                    lblLocation.Text = item.PortName;
                    lblCompanyName.Text = item.CompanyName;
                   // var item = dbContext.LicCompanyVessels.Find(new Guid(hfLicCompID.Value.ToString()));

                    txtShipName.Text = item.ShipName;
                    txtIMONo2.Text = item.IMONo;
                    txtOffNo2.Text = item.OffNo;
                    txtNRT.Text = item.NRT.ToString();
                    txtGRT.Text = item.GRT.ToString();
                    txtLOA.Text = item.LOA.ToString();
                    txtMMSINo.Text = item.MMSINo;
                    txtCallSign.Text = item.CallSign;
                    txtLatDegree.Text = item.LatDegree.ToString();
                    txtLatMin.Text = item.LatMin.ToString();
                    txtLongDegree.Text = item.LongDegree.ToString();
                    txtLongMin.Text = item.LongMin.ToString();
                    btnPopupSearch.Visible = false;


                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void pcSearchShip_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                string filter = "";

                if (txtSearchShipName.Text != "")
                    filter += " And ShipName like '%" + txtSearchShipName.Text + "%'";

                if (txtIMONo.Text != "")
                    filter += " And (IMONo like '%" + txtIMONo.Text + "%' or OffNo like '%" + txtIMONo.Text + "%')";

                if (txtOffNo.Text != "")
                    filter += " And OffNo like '%" + txtOffNo.Text + "%'";

                if (filter == "")
                    filter += "Where 0=1";
                else
                    filter = "Where 1=1 " + filter;

                DALMMS objMMS = new DALMMS();

                DataSet ds = objMMS.GetShipDetails(filter);

                gridSearchShip.DataSource = ds;
                gridSearchShip.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void gridShip_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                //Find ShipDetails
                string filter = "Where ShipID='" + hfShipRecID.Value.ToString() + "'";
                DALMMS objMMS = new DALMMS();

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #region Attachment
        protected void btnSaveAttachDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbAttachType.Value == null)
                {
                    lblErrMsg.Text = "Please select Document Type";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    cbAttachType.Focus();
                    return;
                }
                if (dtValidFrom.Value == null || dtValidFrom.Value.ToString() == "")
                {
                    lblErrMsg.Text = "Please select Valid From Date";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    dtValidFrom.Focus();
                    return;
                }
                if (dtValidTo.Value == null || dtValidTo.Value.ToString() == "")
                {
                    lblErrMsg.Text = "Please select Valid To Date";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    dtValidTo.Focus();
                    return;
                }

                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    eSTS.Database.LicCompanyVesselAttach item = new eSTS.Database.LicCompanyVesselAttach();
                    //v_SuppDoc docType = dbContext.v_SuppDoc.Find(cbAttachType.Value);
                    Guid id = new Guid(cbAttachType.Value.ToString());
                    Database.v_SuppDoc docType = dbContext.v_SuppDoc.Where(w => w.MSDocTypeID == id).FirstOrDefault<Database.v_SuppDoc>();

                    item.AttachID = Guid.NewGuid();
                    item.LicCompanyVesselID = new Guid(hfLicCompID.Value.ToString());
                    item.AttchTypeID = new Guid(cbAttachType.Value.ToString());
                    // item.DocTitle = txtAttachDocDesc.Text;
                    item.LicenseNo = txtLicenseNo.Text;
                    item.ValidFrom = Convert.ToDateTime(dtValidFrom.Value);
                    item.ValidTo = Convert.ToDateTime(dtValidTo.Value);
                    item.Path = SaveAttach(docType.DocCode, item.LicCompanyVesselID.ToString());

                    item.CreatedBy = Session["UserID"].ToString();
                    item.CreatedDate = DateTime.Now;

                    dbContext.LicCompanyVesselAttaches.Add(item);
                    dbContext.SaveChanges();// new Guid(Session["AccessID"].ToString()), "AttachID");

                    dsAttach.DataBind();
                    gridAttach.DataBind();

                    txtLicenseNo.Text = "";
                    dtValidFrom.Text = "";
                    dtValidTo.Text = "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected string SaveAttach(string docCode,string id)
        {
            string fileName = "";
            string fullfileDirectory = "";
            string extension = "";
            string OriginalFileName = "";
            bool folderExists;
            string UploadDirectory = "Upload/" + Session["CompID"].ToString() + "/License";

            try
            {

                if (uploadFile.UploadedFiles.Count() > 0)
                {
                    //Upload File 
                    extension = uploadFile.UploadedFiles[0].FileName.Trim().Substring(uploadFile.UploadedFiles[0].FileName.Trim().LastIndexOf("."));
                    OriginalFileName = docCode + "_"+id + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()+DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + extension;// uploadFile.UploadedFiles[0].FileName.Trim();


                    fileName = UploadDirectory + "/" + OriginalFileName;
                    fullfileDirectory = Server.MapPath(UploadDirectory + "/" + OriginalFileName);

                    //-------------------------------------------------------------
                    // Save File to server directory
                    //-------------------------------------------------------------
                    folderExists = Directory.Exists(Server.MapPath(UploadDirectory));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath(UploadDirectory));

                    uploadFile.UploadedFiles[0].SaveAs(fullfileDirectory);
                    //------------------------------------------------------------

                }
                fileName = "~/License/" + fileName;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return fileName;
        }

        protected void dsAttach_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                if (hfLicCompID.Value != "")
                    e.DataSource.WhereParameters["pLicCompanyVesselID"].DefaultValue = hfLicCompID.Value.ToString();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void gridAttach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    var licCompAttachID = dbContext.LicCompanyVesselAttaches.Find(new Guid(e.Keys[0].ToString()));

                    dbContext.LicCompanyVesselAttaches.Remove(licCompAttachID);

                    dbContext.SaveChanges();

                    gridAttach.DataBind();
                    gridAttach.CancelEdit();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #endregion
        protected void gridAttach_CommandButtonInitialize(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewCommandButtonEventArgs e)
        {
            if (Convert.ToBoolean(Session["IsSTSOperator"]))
            {
                if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
                    e.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    if (txtGRT.Text == "")
                    {
                        lblErrMsg.Text = "Please Enter Vessel GRT";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtGRT.Focus();
                        return;
                    }
                    if (txtNRT.Text == "")
                    {
                        lblErrMsg.Text = "Please Enter Vessel NRT";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtNRT.Focus();
                        return;
                    }
                    if (txtLOA.Text == "")
                    {
                        lblErrMsg.Text = "Please Enter Vessel LOA";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtLOA.Focus();
                        return;
                    }
                    if (txtMMSINo.Text == "")
                    {
                        lblErrMsg.Text = "Please Enter MMSI No.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtMMSINo.Focus();
                        return;
                    }
                    if (txtCallSign.Text == "")
                    {
                        lblErrMsg.Text = "Please Enter Vessel Call Sign";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtCallSign.Focus();
                        return;
                    }
                    if (txtLatDegree.Text == "" || txtLatDegree.Text == "0")
                    {
                        lblErrMsg.Text = "Please Enter Latitude Degree";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtLatDegree.Focus();
                        return;
                    }
                    if (txtLatMin.Text == "" || txtLatMin.Text == "0")
                    {
                        lblErrMsg.Text = "Please Enter Latitude Min";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtLatMin.Focus();
                        return;
                    }
                    if (txtLongDegree.Text == "" || txtLongDegree.Text == "0")
                    {
                        lblErrMsg.Text = "Please Enter Longitude Degree";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtLongDegree.Focus();
                        return;
                    }
                    if (txtLongMin.Text == "" || txtLongMin.Text == "0")
                    {
                        lblErrMsg.Text = "Please Enter Longitude Min";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtLongMin.Focus();
                        return;
                    }
                    if (hfLicCompID.Value.ToString() == "")
                    {
                        string filter = "Where ShipID='" + hfShipRecID.Value.ToString() + "'";

                        //Get Ship Details
                        DALMMS objMMS = new DALMMS();

                        DataSet dsShip = objMMS.GetShipDetails(filter);

                        eSTS.Database.LicCompanyVessel vesselInfo = new eSTS.Database.LicCompanyVessel();

                        vesselInfo.LicCompanyVesselID = Guid.NewGuid();
                        vesselInfo.GRT = Convert.ToDecimal(txtGRT.Text);
                        vesselInfo.NRT = Convert.ToDecimal(txtNRT.Text);
                        vesselInfo.LOA = Convert.ToDecimal(txtLOA.Text);
                        vesselInfo.MMSINo = txtMMSINo.Text;
                        vesselInfo.CallSign = txtCallSign.Text;
                        vesselInfo.LatDegree = Convert.ToInt32(txtLatDegree.Text);
                        vesselInfo.LatMin = Convert.ToDecimal(txtLatMin.Text);
                        vesselInfo.LongDegree = Convert.ToInt32(txtLongDegree.Text);
                        vesselInfo.LongMin = Convert.ToDecimal(txtLongMin.Text);
                        vesselInfo.Latitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLatDegree.Text), Convert.ToDouble(txtLatMin.Text)).ToStringD();
                        vesselInfo.Longitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLongDegree.Text), Convert.ToDouble(txtLongMin.Text)).ToStringD();
                        vesselInfo.CreatedDate = DateTime.Now;
                        vesselInfo.CreatedBy = Session["UserID"].ToString();
                        vesselInfo.LicCompanyID = new Guid(hfLicCompID.Value.ToString());
                        if (dsShip.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = dsShip.Tables[0].Rows[0];
                            vesselInfo.OffNo = dr["OffNo"].ToString();
                            vesselInfo.ShipName = dr["ShipName"].ToString();
                            vesselInfo.PortReg = dr["PortReg"].ToString();
                           
                            vesselInfo.IMONo = dr["IMONo"].ToString();
                            vesselInfo.Status = dr["Status"].ToString();
                            vesselInfo.OwnerName = dr["OwnerName"].ToString();
                            vesselInfo.ShipFlag = dr["ShipFlag"].ToString();
                            vesselInfo.ShipType = dr["ShipTypeId"].ToString();
                            vesselInfo.VoyageType = dr["VoyageType"].ToString();
                            vesselInfo.ShipID = new Guid(dr["ShipID"].ToString());
                            if (dr["YearReg"].ToString() != "")
                                vesselInfo.YearReg = Convert.ToInt32(dr["YearReg"].ToString());

                            if (dr["YearBuilt"].ToString() != "")
                                vesselInfo.YearBuilt = Convert.ToInt32(dr["YearBuilt"].ToString());



                            if (dr["DWT"].ToString() != "")
                                vesselInfo.DWT = Convert.ToDecimal(dr["DWT"].ToString());

                            if (dr["LOA"].ToString() != "")
                                vesselInfo.LOA = Convert.ToDecimal(dr["LOA"].ToString());

                            if (dr["Breadth"].ToString() != "")
                                vesselInfo.Breadth = Convert.ToDecimal(dr["Breadth"].ToString());

                            if (dr["Depth"].ToString() != "")
                                vesselInfo.Depth = Convert.ToDecimal(dr["Depth"].ToString());

                            if (dr["STDDraft"].ToString() != "")
                                vesselInfo.STDDraft = Convert.ToDecimal(dr["STDDraft"].ToString());

                            if (dr["ShipCapacity"].ToString() != "")
                                vesselInfo.ShipCapacity = Convert.ToDecimal(dr["ShipCapacity"].ToString());

                            if (dr["ShipBeam"].ToString() != "")
                                vesselInfo.ShipBeam = Convert.ToDecimal(dr["ShipBeam"].ToString());

                            if (dr["DispmtWeight"].ToString() != "")
                                vesselInfo.DispmtWeight = Convert.ToDecimal(dr["DispmtWeight"].ToString());
                        }

                        dbContext.LicCompanyVessels.Add(vesselInfo);
                        dbContext.SaveChanges();
                        hfShipID.Value = vesselInfo.LicCompanyVesselID.ToString();
                        btnSaveAttachDoc.Enabled = true;
                    }
                    else
                    {
                        Guid shipID = new Guid(hfShipID.Value);
                        eSTS.Database.LicCompanyVessel vesselInfo = dbContext.LicCompanyVessels.Find(shipID);

                        vesselInfo.LatDegree = Convert.ToInt32(txtLatDegree.Text);
                        vesselInfo.LatMin = Convert.ToDecimal(txtLatMin.Text);
                        vesselInfo.LongDegree = Convert.ToInt32(txtLongDegree.Text);
                        vesselInfo.LongMin = Convert.ToDecimal(txtLongMin.Text);
                        vesselInfo.Latitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLatDegree.Text), Convert.ToDouble(txtLatMin.Text)).ToStringD();
                        vesselInfo.Longitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLongDegree.Text), Convert.ToDouble(txtLongMin.Text)).ToStringD();
                        vesselInfo.CreatedDate = DateTime.Now;
                        vesselInfo.MMSINo = txtMMSINo.Text;
                        vesselInfo.CallSign = txtCallSign.Text;
                        dbContext.SaveChanges(Session["UserID"].ToString(), "LicCompanyVesselID", vesselInfo.LicCompanyVesselID);
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}