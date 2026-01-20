using Apps.Common;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using eSTS.DAL;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eSTS.Common;
using System.Web.Configuration;
using DevExpress.XtraPrinting;
using DevExpress.Export;

namespace eSTS.Operation
{
    public partial class OperationListPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Check Session
                if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
                {
                    Response.Redirect("~//SignIn.aspx", true);
                }
                if (!Page.IsPostBack){
                    dtOperationDateFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                    dtOperationDateTo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 0, 0, 0);
                    LoadFSU();
                }
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void LoadFSU()
        {
            try
            {
                DALOperation obj = new DALOperation();
                DataSet dsFSU = obj.GetFSU();

                cbFSU.DataSource = dsFSU;
                cbFSU.TextField = "FSU";
                cbFSU.ValueField = "FSU";
                cbFSU.DataBind();
                cbFSU.Value = "ALL";
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void gridComplete_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        {
            try
            {
                BootstrapGridView gv = (BootstrapGridView)sender;
                string methodCode = gv.GetRowValues(e.VisibleIndex, "MethodCode").ToString();
                if (methodCode == "A")
                {
                    if (e.DataColumn.FieldName == "VSName")
                    {
                        string Name = "";
                        string IMO = "";
                        string POR = "";
                        string latDegree = "";
                        string latMin = "";
                        string longDegree = "";
                        string longMin = "";
                        string latitude = "";
                        string longitude = "";
                        string soCompanyName = "";
                         Name = gv.GetRowValues(e.VisibleIndex, "VRName").ToString();
                         IMO = gv.GetRowValues(e.VisibleIndex, "VRIMONo").ToString();
                         POR = gv.GetRowValues(e.VisibleIndex, "VRPortReg").ToString();
                         latDegree = gv.GetRowValues(e.VisibleIndex, "VRLatDegree").ToString();
                         latMin = gv.GetRowValues(e.VisibleIndex, "VRLatMin").ToString();
                         longDegree = gv.GetRowValues(e.VisibleIndex, "VRLongDegree").ToString();
                         longMin = gv.GetRowValues(e.VisibleIndex, "VRLongMin").ToString();
                        latitude = gv.GetRowValues(e.VisibleIndex, "VRLatitude").ToString();
                        longitude = gv.GetRowValues(e.VisibleIndex, "VRLongitude").ToString();
                        soCompanyName= gv.GetRowValues(e.VisibleIndex, "SOCompanyName").ToString();
                        e.Cell.Text = @"<p><span><strong>FSU Name :</strong> " + Name + "</span><br>" +
                            "<span><strong> FSU IMO NO. :</strong> " + IMO + "</span><br>" +
                        "<span><strong>POR :</strong> " + POR + "</span><br>" +
                                          "<span><strong>Lat (DMS) :</strong> " + latDegree + "° " + latMin + "'N<br>" +
                                          "<span><strong>Long (DMS):</strong> " + longDegree + "° " + longMin + "'E</span><br>" +
                                          "<a href='javascript:showMap("+ latitude + ","+ longitude + ");'>Show Map <i class='fa fa-map-marker text-info' aria-hidden='true'></i></a>"+
                                           "<br><br><span><strong>Operator Name :</strong> " + soCompanyName + "</span></p>";
                    }
                 
                    if (e.DataColumn.FieldName == "VRName")
                    {
                        string vrName = gv.GetRowValues(e.VisibleIndex, "VSName").ToString();
                        string vrIMO = gv.GetRowValues(e.VisibleIndex, "VSIMONo").ToString();
                        string vrPOR = gv.GetRowValues(e.VisibleIndex, "VSPortReg").ToString();
                       

                        e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vrName + "</span><br>" +
                            "<span><strong>IMO NO. :</strong> " + vrIMO + "</span><br>" +
                        "<span><strong>POR :</strong> " + vrPOR + "</span><br>";
                    }
                    if (e.DataColumn.FieldName == "UOMCode")
                    {
                        string operationDate = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("dd/MM/yyyy");
                        string operationTime = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("HH:mm");
                        string oilType = gv.GetRowValues(e.VisibleIndex, "OilTypeDesc").ToString();
                        string uom = gv.GetRowValues(e.VisibleIndex, "UOMCode").ToString();
                        string oilAmt = gv.GetRowValues(e.VisibleIndex, "EstOilMT").ToString();
                        string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();
                        string validPermit = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "ValidPermit").ToString()).ToString("dd/MM/yyyy HH:mm");
                        bool isPayment = Convert.ToBoolean(gv.GetRowValues(e.VisibleIndex, "IsPayment").ToString());

                        e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>" +
                                     "<p><span><strong>Operation Date :</strong> " + operationDate + "</span><br>" +
                             "<span><strong>Operation Time :</strong> " + operationTime + "</span><br>" +
                            "<span><strong>Oil Type :</strong> " + oilType + "</span><br>" +
                        "<span><strong>Est Oil Amt :</strong> " + oilAmt + "(" + uom + ")</span><br>";

                        if (isPayment == true)
                        {
                            if (DateTime.Now <= Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "ValidPermit").ToString()))
                                e.Cell.Text = e.Cell.Text + "<span><strong>Permit Validity :</strong><font color='blue'>" + validPermit + "</font></span><br>";
                            else
                                e.Cell.Text = e.Cell.Text + "<span><strong>Permit Validity :</strong><font color='red'>" + validPermit + "</font></span><br>";
                        }
                    }
                    if (e.DataColumn.FieldName == "ActionStatus")
                    {

                        string action = gv.GetRowValues(e.VisibleIndex, "ActionStatus").ToString();
                        string labelColor = gv.GetRowValues(e.VisibleIndex, "LabelColor").ToString();
                        e.Cell.Text = "<p><span class='label label-" + labelColor + "'>" + action + " </span></P>";

                    }
                }
                else
                {
                   
                        if (e.DataColumn.FieldName == "VSName")
                        {
                        string Name = "";
                        string IMO = "";
                        string POR = "";
                        string latDegree = "";
                        string latMin = "";
                        string longDegree = "";
                        string longMin = "";
                        string latitude = "";
                        string longitude = "";
                        string soCompanyName = "";
                        Name = gv.GetRowValues(e.VisibleIndex, "VSName").ToString();
                        IMO = gv.GetRowValues(e.VisibleIndex, "VSIMONo").ToString();
                        POR = gv.GetRowValues(e.VisibleIndex, "VSPortReg").ToString();
                        latDegree = gv.GetRowValues(e.VisibleIndex, "VSLatDegree").ToString();
                        latMin = gv.GetRowValues(e.VisibleIndex, "VSLatMin").ToString();
                        longDegree = gv.GetRowValues(e.VisibleIndex, "VSLongDegree").ToString();
                        longMin = gv.GetRowValues(e.VisibleIndex, "VSLongMin").ToString();
                        latitude = gv.GetRowValues(e.VisibleIndex, "VSLatitude").ToString();
                        longitude = gv.GetRowValues(e.VisibleIndex, "VSLongitude").ToString();
                        soCompanyName = gv.GetRowValues(e.VisibleIndex, "SOCompanyName").ToString();
                        e.Cell.Text = @"<p><span><strong>FSU Name :</strong> " + Name + "</span><br>" +
                            "<span><strong> FSU IMO NO. :</strong> " + IMO + "</span><br>" +
                        "<span><strong>POR :</strong> " + POR + "</span><br>" +
                                          "<span><strong>Lat (DMS) :</strong> " + latDegree + "° " + latMin + "'N<br>" +
                                          "<span><strong>Long (DMS):</strong> " + longDegree + "° " + longMin + "'E</span><br>" +
                                          "<a href='javascript:showMap('" + latitude + "'" + longitude + "');'>Show Map <i class='fa fa-map-marker text-info' aria-hidden='true'></i></a>" +
                                           "<br><br><span><strong>Operator Name :</strong> " + soCompanyName + "</span></p>";
                    }
                        if (e.DataColumn.FieldName == "VRName")
                        {
                        string vrName = gv.GetRowValues(e.VisibleIndex, "VRName").ToString();
                        string vrIMO = gv.GetRowValues(e.VisibleIndex, "VRIMONo").ToString();
                        string vrPOR = gv.GetRowValues(e.VisibleIndex, "VRPortReg").ToString();


                        e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vrName + "</span><br>" +
                            "<span><strong>IMO NO. :</strong> " + vrIMO + "</span><br>" +
                        "<span><strong>POR :</strong> " + vrPOR + "</span><br>";
                    }
                    if (e.DataColumn.FieldName == "UOMCode")
                    {
                        string operationDate = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("dd/MM/yyyy");
                        string operationTime = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("HH:mm");
                        string oilType = gv.GetRowValues(e.VisibleIndex, "OilTypeDesc").ToString();
                        string uom = gv.GetRowValues(e.VisibleIndex, "UOMCode").ToString();
                        string oilAmt = gv.GetRowValues(e.VisibleIndex, "EstOilMT").ToString();
                        string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();
                        string validPermit = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "ValidPermit").ToString()).ToString("dd/MM/yyyy HH:mm");
                        bool isPayment = Convert.ToBoolean(gv.GetRowValues(e.VisibleIndex, "IsPayment").ToString());

                        e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>" +
                                     "<p><span><strong>Operation Date :</strong> " + operationDate + "</span><br>" +
                             "<span><strong>Operation Time :</strong> " + operationTime + "</span><br>" +
                            "<span><strong>Oil Type :</strong> " + oilType + "</span><br>" +
                        "<span><strong>Est Oil Amt :</strong> " + oilAmt + "(" + uom + ")</span><br>";

                        if (isPayment == true)
                        {
                            if (DateTime.Now <= Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "ValidPermit").ToString()))
                                e.Cell.Text = e.Cell.Text + "<span><strong>Permit Validity :</strong><font color='blue'>" + validPermit + "</font></span><br>";
                            else
                                e.Cell.Text = e.Cell.Text + "<span><strong>Permit Validity :</strong><font color='red'>" + validPermit + "</font></span><br>";
                        }
                    }
                        if (e.DataColumn.FieldName == "ActionStatus")
                        {
                            string action = gv.GetRowValues(e.VisibleIndex, "ActionStatus").ToString();
                            string labelColor = gv.GetRowValues(e.VisibleIndex, "LabelColor").ToString();
                            e.Cell.Text = "<p><span class='label label-" + labelColor + "'>" + action + " </span></P>";
                        }
                    }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }


        #region Literal Init

        protected void lilView_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;
            if (DataBinder.Eval(container.DataItem, "MethodCode").ToString() == "A")
            {
                contentUrl = string.Format("OpAppA.aspx?mode=v&appr=0&adm=1&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));
            }
            else
            {
                contentUrl = string.Format("OpAppB.aspx?mode=v&appr=0&adm=1&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));
            }
            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            //link.Text = "<i class='fa fa-eye fa-lg text-success' aria-hidden='true'>" + string.Format("{0}", "") + "</i>";
            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-eye'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "_self";
            link.ToolTip = "View Application";
        }
        protected void lilPayment_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsPayment")) == true)
            {
                string dtPayYear = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentDate")).Year.ToString();
                string dtPayMth = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentDate")).Month.ToString();
                string dtPayDay = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentDate")).Day.ToString();
                string timePayHour = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentTime")).Hour.ToString();
                string timePayMin = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentTime")).Minute.ToString();
                string paymentAmt = Convert.ToDouble(DataBinder.Eval(container.DataItem, "PaymentAmount")).ToString();
                string receiptNo = DataBinder.Eval(container.DataItem, "ReceiptNo").ToString();
                string permitRef = DataBinder.Eval(container.DataItem, "PaymentRefID").ToString();

                link.Text = " <button class='btn btn-warning btn-circle btn-outline' type='button'><i class='fa fa-dollar'></i></button>" + string.Format("{0}", "") + "</i>";
                //link.Text = "<a href='javascript:void(0);' onclick='OnMoreInfoClick(this, '')'>More Info...</a>";
                link.NavigateUrl = "javascript:popUpPayment('" + DataBinder.Eval(container.DataItem, "OperationAppID") + "','" + dtPayYear + "','" + dtPayMth + "','" + dtPayDay + "','" + timePayHour + "','" + timePayMin + "','" + paymentAmt + "','" + receiptNo + "','" + permitRef + "');";
                link.ToolTip = "Payment Details";
            }
        }

        protected void lilPermit_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "333a69e2-104f-4d87-acc9-8ce0ee87204b" || DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "7f99dadd-6895-4091-a5f3-c2803acfff1f")
            {
                contentUrl = string.Format("{0}", DataBinder.Eval(container.DataItem, "PermitDocLink"));

                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = " <button class='btn btn-primary btn-circle btn-outline' type='button'><i class='fa fa-file-pdf-o'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "blank";
                link.ToolTip = "Permit";
            }
        }

        protected void lilNotis_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            if (DataBinder.Eval(container.DataItem, "NPDocLink").ToString() != "")
            {
                contentUrl = string.Format("{0}", DataBinder.Eval(container.DataItem, "NPDocLink"));

                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = " <button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-file-pdf-o'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "blank";
                link.ToolTip = "STS Notice";
            }
        }

        protected void LilLampiran_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            if (DataBinder.Eval(container.DataItem, "Lampiran1DocLink").ToString() != "")
            {
                contentUrl = string.Format("{0}", DataBinder.Eval(container.DataItem, "Lampiran1DocLink"));

                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-info btn-circle btn-outline' type='button'><i class='fa fa-file-pdf-o'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "blank";
                link.ToolTip = "Lampiran 1 491B";
            }
        }
        protected void lilCM_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("UploadCMBL.aspx?mode=v&m=a&appr=0&adm=1&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));
            bool isSubmitCM = false;
            bool isSubmitBL = false;

            if (DataBinder.Eval(container.DataItem, "IsSubmitCM") != null)
            {
                if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsSubmitCM")) == true)
                    isSubmitCM = true;
            }
            if (DataBinder.Eval(container.DataItem, "IsSubmitBL") != null)
            {
                if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "isSubmitBL")) == true)
                    isSubmitBL = true;
            }
            link.EncodeHtml = false;
            //if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "333a69e2-104f-4d87-acc9-8ce0ee87204b" || isSubmitCM==true ||isSubmitBL==true)
            if (isSubmitCM == true || isSubmitBL == true)
            {
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-clipboard'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "View Cargo Manifest/ Survey Report / Bill Of Lading";
            }
        }

        #endregion
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void BindGrid()
        {
            try
            {
                DALOperation obj = new DALOperation();
                DataSet ds = obj.GetOperation(Convert.ToDateTime(dtOperationDateFrom.Value), Convert.ToDateTime(dtOperationDateTo.Value), cbFSU.Value.ToString());

                gridComplete.DataSource = ds;
                gridComplete.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void ButtonXLS1_Click(object sender, EventArgs e)
        {
            gridComplete.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }
        protected void ButtonXLSX1_Click(object sender, EventArgs e)
        {
            gridComplete.ExportXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }
    }
}