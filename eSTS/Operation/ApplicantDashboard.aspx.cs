using Apps.Common;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eSTS.DAL;
using System.Data;
using System.Web.Configuration;

namespace eSTS.Operation
{
    public partial class ApplicantDashboard : System.Web.UI.Page
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
               
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
     
        //UnComplete Approval List
        protected void grid_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        {
            try
            {
                //if (e.DataColumn.FieldName == "DeliveryLocation")
                //{
                //    BootstrapGridView gv = (BootstrapGridView)sender;
                //    string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();
                //    e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>";

                //}
                if (e.DataColumn.FieldName == "VSName")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string vsName = gv.GetRowValues(e.VisibleIndex, "VSName").ToString();
                    string vsIMO = gv.GetRowValues(e.VisibleIndex, "VSIMONo").ToString();
                    string vsPOR = gv.GetRowValues(e.VisibleIndex, "VSPortReg").ToString();

                    e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vsName + "</span><br>" +
                        "<span><strong>IMO NO. :</strong> " + vsIMO + "</span><br>" +
                    "<span><strong>POR :</strong> " + vsPOR + "</span><br>";
                }
                if (e.DataColumn.FieldName == "VRName")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string vrName = gv.GetRowValues(e.VisibleIndex, "VRName").ToString();
                    string vrIMO = gv.GetRowValues(e.VisibleIndex, "VRIMONo").ToString();
                    string vrPOR = gv.GetRowValues(e.VisibleIndex, "VRPortReg").ToString();
                    string latDegree = gv.GetRowValues(e.VisibleIndex, "VRLatDegree").ToString();
                    string latMin = gv.GetRowValues(e.VisibleIndex, "VRLatMin").ToString();
                    string longDegree = gv.GetRowValues(e.VisibleIndex, "VRLongDegree").ToString();
                    string longMin = gv.GetRowValues(e.VisibleIndex, "VRLongMin").ToString();
                    string latitude = gv.GetRowValues(e.VisibleIndex, "VRLatitude").ToString();
                    string longitude = gv.GetRowValues(e.VisibleIndex, "VRLongitude").ToString();
                    string soCompanyName = gv.GetRowValues(e.VisibleIndex, "SOCompanyName").ToString();
                    e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vrName + "</span><br>" +
                        "<span><strong>IMO NO. :</strong> " + vrIMO + "</span><br>" +
                    "<span><strong>POR :</strong> " + vrPOR + "</span><br>" +
                                      "<span><strong>Lat (DMS) :</strong> " + latDegree + "° " + latMin + "'N<br>" +
                                      "<span><strong>Long (DMS):</strong> " + longDegree + "° " + longMin + "'E </span><br>" +
                                          "<a href='javascript:showMap(" + latitude + "," + longitude + ");'>Show Map <i class='fa fa-map-marker text-info' aria-hidden='true'></i></a>"+
                                           "<br><br><span><strong>Operator Name :</strong> " + soCompanyName + "</span></p>";
                }
                if (e.DataColumn.FieldName == "UOMCode")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();
                    string operationDate = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("dd/MM/yyyy");
                    string operationTime = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("HH:mm");
                    string oilType = gv.GetRowValues(e.VisibleIndex, "OilTypeDesc").ToString();
                    string uom = gv.GetRowValues(e.VisibleIndex, "UOMCode").ToString();
                    string oilAmt = gv.GetRowValues(e.VisibleIndex, "EstOilMT").ToString();

                    e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>" +
                                "<p><span><strong>Operation Date :</strong> " + operationDate + "</span><br>" +
                        "<span><strong>Operation Time :</strong> " + operationTime + "</span><br>" +
                        "<span><strong>Oil Type :</strong> " + oilType + "</span><br>" +
                    "<span><strong>Est Oil Amt :</strong> " + oilAmt + "(" + uom + ")</span><br>";
                }
                if (e.DataColumn.FieldName == "ActionStatus")
                {

                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string action = gv.GetRowValues(e.VisibleIndex, "ActionStatus").ToString();
                    string labelColor = gv.GetRowValues(e.VisibleIndex, "LabelColor").ToString();
                    if (action.Length > 30)
                    {
                        e.Cell.Text = "<p><span class='label label-" + labelColor + "'>" + action.Substring(0, 22) + "<br>" +
                            action.Substring(22, action.Length - 22) + "</span></P>";
                    }
                    else
                        e.Cell.Text = "<p><span class='label label-" + labelColor + "'>" + action + " </span></P>";

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void gridActive_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        {
            try
            {
                //if (e.DataColumn.FieldName == "DeliveryLocation")
                //{
                //    BootstrapGridView gv = (BootstrapGridView)sender;
                //    string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();
                //    e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>";

                //}
                if (e.DataColumn.FieldName == "VSName")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string vsName = gv.GetRowValues(e.VisibleIndex, "VSName").ToString();
                    string vsIMO = gv.GetRowValues(e.VisibleIndex, "VSIMONo").ToString();
                    string vsPOR = gv.GetRowValues(e.VisibleIndex, "VSPortReg").ToString();

                    e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vsName + "</span><br>" +
                        "<span><strong>IMO NO. :</strong> " + vsIMO + "</span><br>" +
                    "<span><strong>POR :</strong> " + vsPOR + "</span><br>";
                }
                if (e.DataColumn.FieldName == "VRName")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string vrName = gv.GetRowValues(e.VisibleIndex, "VRName").ToString();
                    string vrIMO = gv.GetRowValues(e.VisibleIndex, "VRIMONo").ToString();
                    string vrPOR = gv.GetRowValues(e.VisibleIndex, "VRPortReg").ToString();
                    string latDegree = gv.GetRowValues(e.VisibleIndex, "VRLatDegree").ToString();
                    string latMin = gv.GetRowValues(e.VisibleIndex, "VRLatMin").ToString();
                    string longDegree = gv.GetRowValues(e.VisibleIndex, "VRLongDegree").ToString();
                    string longMin = gv.GetRowValues(e.VisibleIndex, "VRLongMin").ToString();
                    string latitude = gv.GetRowValues(e.VisibleIndex, "VRLatitude").ToString();
                    string longitude = gv.GetRowValues(e.VisibleIndex, "VRLongitude").ToString();
                    string soCompanyName = gv.GetRowValues(e.VisibleIndex, "SOCompanyName").ToString();
                    e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vrName + "</span><br>" +
                        "<span><strong>IMO NO. :</strong> " + vrIMO + "</span><br>" +
                    "<span><strong>POR :</strong> " + vrPOR + "</span><br>" +
                                      "<span><strong>Lat (DMS) :</strong> " + latDegree + "° " + latMin + "'N<br>" +
                                      "<span><strong>Long (DMS):</strong> " + longDegree + "° " + longMin + "'E </span><br>" +
                                          "<a href='javascript:showMap(" + latitude + "," + longitude + ");'>Show Map <i class='fa fa-map-marker text-info' aria-hidden='true'></i></a>" +
                                            "<br><br><span><strong>Operator Name :</strong> " + soCompanyName + "</span></p>";
                }
                if (e.DataColumn.FieldName == "UOMCode")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();
                    string operationDate = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("dd/MM/yyyy");
                    string operationTime = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("HH:mm");
                    string oilType = gv.GetRowValues(e.VisibleIndex, "OilTypeDesc").ToString();
                    string uom = gv.GetRowValues(e.VisibleIndex, "UOMCode").ToString();
                    string oilAmt = gv.GetRowValues(e.VisibleIndex, "EstOilMT").ToString();
                    string validPermit = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "ValidPermit").ToString()).ToString("dd/MM/yyyy HH:mm");
                    bool isPayment = Convert.ToBoolean(gv.GetRowValues(e.VisibleIndex, "IsPayment").ToString());

                    e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>" +
                                "<p><span><strong>Operation Date :</strong> " + operationDate + "</span><br>" +
                        "<span><strong>Operation Time :</strong> " + operationTime + "</span><br>" +
                        "<span><strong>Oil Type :</strong> " + oilType + "</span><br>" +
                    "<span><strong>Est Oil Amt :</strong> " + oilAmt + "(" + uom + ")</span><br><br>";
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

                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string action = gv.GetRowValues(e.VisibleIndex, "ActionStatus").ToString();
                    string labelColor = gv.GetRowValues(e.VisibleIndex, "LabelColor").ToString();
                    if (action.Length > 30)
                    {
                        e.Cell.Text = "<p><span class='label label-"+ labelColor + "'>" + action.Substring(0, 22) + "<br>" +
                            action.Substring(22, action.Length-22) + "</span></P>";
                    }
                    else
                        e.Cell.Text = "<p><span class='label label-" + labelColor + "'>" + action + " </span></P>";

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        //protected void gridPendingBDN_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.DataColumn.FieldName == "Status")
        //        {
        //            string permitDocLink = "";
        //            BootstrapGridView gv = (BootstrapGridView)sender;
        //            string status = gv.GetRowValues(e.VisibleIndex, "Status").ToString();
        //            string id = gv.GetRowValues(e.VisibleIndex, "OperationAppID").ToString();
        //            if (gv.GetRowValues(e.VisibleIndex, "PermitDocLink") != null)
        //                permitDocLink = gv.GetRowValues(e.VisibleIndex, "PermitDocLink").ToString();
        //            bool isPayment = Convert.ToBoolean(gv.GetRowValues(e.VisibleIndex, "IsPayment"));
        //            bool isCompleted = Convert.ToBoolean(gv.GetRowValues(e.VisibleIndex, "IsAppCompleted"));
        //            bool isSubmitBDN = Convert.ToBoolean(gv.GetRowValues(e.VisibleIndex, "IsSubmitBDN"));


        //            DALOperation objOperation = new DALOperation();

        //            DataSet ds = objOperation.GetApplicationFlowStatus(id);
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                if (isSubmitBDN == false && isCompleted == true)
        //                {
        //                   // e.Cell.Text = "<p><span class='label label-" + dr["LabelColor"].ToString() + "'>" + dr["FlowActionStatus"] + "</span></P>";
        //                    e.Cell.Text = e.Cell.Text + "<p><span class='label label-warning'> Pending BDN </span></P>";
        //                }
        //                else
        //                    e.Cell.Text = "<span class='label label-" + dr["LabelColor"].ToString() + "'>" + dr["FlowActionStatus"] + "</span>";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
        //    }
        //}

        ////protected void gridComplete_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        ////{
        ////    try
        ////    {
        ////        BootstrapGridView gv = (BootstrapGridView)sender;
        ////        string status = gv.GetRowValues(e.VisibleIndex, "Status").ToString();
        ////        string id = grid.GetRowValues(e.VisibleIndex, "OperationAppID").ToString();
        ////        //bool isDraft = Convert.ToBoolean(grid.GetRowValues(e.VisibleIndex, "IsDraft"));
        ////        //bool isRejected = Convert.ToBoolean(grid.GetRowValues(e.VisibleIndex, "IsRejected"));
        ////        //bool isCompleted = Convert.ToBoolean(grid.GetRowValues(e.VisibleIndex, "IsAppCompleted"));

        ////        if (e.DataColumn.FieldName == "Status")
        ////        {
        ////            DALOperation objOperation = new DALOperation();

        ////            DataSet ds = objOperation.GetApplicationFlowStatus(id);
        ////            foreach (DataRow dr in ds.Tables[0].Rows)
        ////            {
        ////                e.Cell.Text = "<span class='label label-" + dr["LabelColor"].ToString() + "'>" + dr["FlowActionStatus"] + "</span>";
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
        ////    }
        ////}

        protected void lilView_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;
            
            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;
            if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() != "ba50b535-0bfc-412a-b8c3-30d22867f531")
            {
                contentUrl = string.Format("OpAppA.aspx?mode=e&appr=0&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-pencil'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "Edit Application";
            }
            else
            {
                contentUrl = string.Format("OpAppA.aspx?mode=v&appr=0&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-eye'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "View Application";
            }
        }
        protected void lilView2_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("OpAppA.aspx?mode=v&appr=0&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
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


        protected void lilCancel_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;
            if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() != "ba50b535-0bfc-412a-b8c3-30d22867f531")
            {
                link.EncodeHtml = false;
                // link.Text = "<i class='fa fa-times fa-lg text-danger' aria-hidden='true'>" + string.Format("{0}", "") + "</i>";
                link.Text = " <button class='btn btn-danger btn-circle btn-outline' type='button'><i class='fa fa-times'></i></button>" + string.Format("{0}", "") + "</i>";
                link.NavigateUrl = "javascript:popUpCancel('" + DataBinder.Eval(container.DataItem, "OperationAppID") + "');";
                link.ToolTip = "Cancel";
            }
            
        }
        protected void lilCancel2_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;
            if (Convert.ToInt32(DataBinder.Eval(container.DataItem, "ActionStatusSeq")) < 10)
            {
                // link.Text = "<i class='fa fa-times fa-lg text-danger' aria-hidden='true'>" + string.Format("{0}", "") + "</i>";
                link.Text = " <button class='btn btn-danger btn-circle btn-outline' type='button'><i class='fa fa-times'></i></button>" + string.Format("{0}", "") + "</i>";
                link.NavigateUrl = "javascript:popUpCancel('" + DataBinder.Eval(container.DataItem, "OperationAppID") + "');";
                link.ToolTip = "Cancel";
            }
        }
        protected void lilPermit_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "333a69e2-104f-4d87-acc9-8ce0ee87204b" || DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString()== "27a4c669-06db-4363-9ae4-e6b7066c2df5")
            {
                contentUrl = string.Format("{0}", DataBinder.Eval(container.DataItem, "PermitDocLink"));

                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = " <button class='btn btn-primary btn-circle btn-outline' type='button'><i class='fa fa-file-pdf-o'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "blank";
                link.ToolTip = "Permit";
            }
        }
        //protected void lilNotis_Init(object sender, EventArgs e)
        //{
        //    string contentUrl = "";
        //    ASPxHyperLink link = (ASPxHyperLink)sender;

        //    GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

        //    link.EncodeHtml = false;

        //    if (DataBinder.Eval(container.DataItem, "NPDocLink").ToString() != "")
        //    {
        //        contentUrl = string.Format("{0}", DataBinder.Eval(container.DataItem, "NPDocLink"));

        //        link.EncodeHtml = false;
        //        link.NavigateUrl = contentUrl;// "javascript:void(0);";
        //        link.Text = " <button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-file-pdf-o'></i></button>" + string.Format("{0}", "") + "</i>";
        //        link.Target = "blank";
        //        link.ToolTip = "STS Notice";
        //    }
        //}

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

            contentUrl = string.Format("UploadCMBL.aspx?mode=e&m=a&appr=0&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "333a69e2-104f-4d87-acc9-8ce0ee87204b" || DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "27a4c669-06db-4363-9ae4-e6b7066c2df5" || DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "b153a781-b0d2-4921-8cc1-30e1ea3093a3")
            {
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-upload'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "Upload Cargo Manifest / Bill Of Lading";
            }
        }
        protected void lilAmend_Init(object sender, EventArgs e)
        {
            /* CR : EB/CR/2022/01/001
            Added by : Normala
            Date : 25/01/2022
            Reason/Purpose : Allow for Amendments 
            */
           
            bool isPayment = false, isAmend = false, isAmendApprove = false;
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;
            if (DataBinder.Eval(container.DataItem, "IsPayment").ToString() == "")
                isPayment = false;
            else if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsPayment").ToString()) == true)
                isPayment = true;

            if (DataBinder.Eval(container.DataItem, "IsAmend").ToString() == "")
                isAmend = false;
            else if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsAmend").ToString()) == true)
                isAmend = true;

            if (DataBinder.Eval(container.DataItem, "IsAmendApprove").ToString() == "")
                isAmendApprove = false;
            else if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsAmendApprove").ToString()) == true)
                isAmendApprove = true;

            if (isPayment == true && isAmend == false && isAmendApprove == false)
            {
                if (DateTime.Now <= Convert.ToDateTime(DataBinder.Eval(container.DataItem, "ValidPermit").ToString()))
                {
                    contentUrl = string.Format("OpAppA.aspx?mode=a&appr=0&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

                    link.EncodeHtml = false;
                    link.NavigateUrl = contentUrl;// "javascript:void(0);";
                    link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-retweet'></i></button>" + string.Format("{0}", "") + "</i>";
                    link.Target = "_self";
                    link.ToolTip = "Amend Application";
                }
            }
        }

        protected void pcCancel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid operationAppID = new Guid(hfApplicationID.Value.ToString());

                    OperationApp appInfo = dbContext.OperationApps.Find(operationAppID);

                    appInfo.IsCancel = true;
                    appInfo.CancelDate = DateTime.Now;
                    appInfo.CancelRemark = txtCancelReason.Text;
                    appInfo.CancelBy = Session["UserID"].ToString();
                    
                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", appInfo.OperationAppID);

                    OperationAppFlow appFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == appInfo.OperationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();

                    appFlow.ActionBy = Session["UserID"].ToString();
                    appFlow.ActionDate = DateTime.Now;
                    appFlow.IsActive = false;

                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", appInfo.OperationAppID);

                    //create new flow for cancel
                    OperationAppFlow cancelAppFlow = new OperationAppFlow();
                    cancelAppFlow.OperationAppFlowID = Guid.NewGuid();
                    cancelAppFlow.OperationAppID = appInfo.OperationAppID;
                    cancelAppFlow.FlowActionStatusID = sysParam.FlowCancel;
                    cancelAppFlow.IsActive = true;
                    cancelAppFlow.Remark = txtCancelReason.Text;
                    cancelAppFlow.ActionBy = Session["UserID"].ToString();
                    cancelAppFlow.ActionDate = DateTime.Now.AddSeconds(1);
                    cancelAppFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                    cancelAppFlow.CreatedBy = Session["UserID"].ToString();

                    dbContext.OperationAppFlows.Add(cancelAppFlow);
                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppFlowID", appInfo.OperationAppID);
                    DALOperation.PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowCancel, false);

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void dsOperationApp_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
            {
                e.DataSource.WhereParameters.Clear();
                e.DataSource.Where = "it.[SupplyMethodID]=@pSupplyMethodID and (it.[IsAppCompleted]=false and it.[CompID]=@pCompID and (it.[IsCancel]=false or it.[IsRejected]=true) and (it.[FlowActionStatusID]=@pFlowDraft or it.[FlowActionStatusID]=@pFlowReject or it.[FlowActionStatusID]=@pflowPendingApproval) or (it.[FlowActionStatusID] = @pflowAmendPending)) ";
                e.DataSource.WhereParameters.Add("pCompID",DbType.String, Session["CompID"].ToString());
                e.DataSource.WhereParameters.Add("pSupplyMethodID", DbType.Guid, dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodA.ToString());
                e.DataSource.WhereParameters.Add("pFlowDraft", DbType.Guid, dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowDraft.ToString());
                e.DataSource.WhereParameters.Add("pFlowReject", DbType.Guid, dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowReject.ToString());
                e.DataSource.WhereParameters.Add("pflowPendingApproval", DbType.Guid, dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingApproval.ToString());
                e.DataSource.WhereParameters.Add("pflowAmendPending", DbType.Guid, dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowAmendPending.ToString());
            }
        }
        protected void dsActiveApp_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {

            try
            {
                using (eSTS_StagEntities dbContext = new Database.eSTS_StagEntities())
                {
                    string flowPermitIssued = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPermitIssued.ToString();
                    string flowPendingCM = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingCM.ToString();
                    string flowPendingPayment = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingPayment.ToString();
                    string flowProcessInv = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowProcessInvoice.ToString();
                    string flowPendingApprovedDec = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingApproveDec.ToString();
                    string flowRejectDec = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowRejectDec.ToString();
                    string supplyMethodID = dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodA.ToString();

                    e.DataSource.WhereParameters.Clear();
                    if (Convert.ToInt32(Session["PortLocation"]) == 0)
                        e.DataSource.Where = "it.[CompID]=@pCompID and it.[SupplyMethodID] = @pSupplyMethodID and (it.[FlowActionStatusID] = @pPermitIssued or it.[FlowActionStatusID] = @pPendingCM or it.[FlowActionStatusID] = @pPendingPayment or it.[FlowActionStatusID] = @pflowPendingApprovedDec or it.[FlowActionStatusID] = @pflowRejectDec or it.[FlowActionStatusID] = @pflowProcessInv)";
                    else
                    {
                        if (Session["PermitIssuerID"].ToString() != "")
                            e.DataSource.Where = "it.[CompID]=@pCompID and it.[SupplyMethodID]=@pSupplyMethodID and it.[Location]=@pLocation and it.[PermitIssuerID]=@pPermitIssuerID and (it.[FlowActionStatusID] = @pPermitIssued or it.[FlowActionStatusID] = @pPendingCM or it.[FlowActionStatusID] = @pPendingPayment or it.[FlowActionStatusID] = @pflowPendingApprovedDec or it.[FlowActionStatusID] = @pflowRejectDec or it.[FlowActionStatusID] = @pflowProcessInv)";
                        else
                            e.DataSource.Where = "it.[CompID]=@pCompID and it.[SupplyMethodID]=@pSupplyMethodID and it.[Location]=@pLocation and (it.[FlowActionStatusID] = @pPermitIssued or it.[FlowActionStatusID] = @pPendingCM or it.[FlowActionStatusID] = @pPendingPayment or it.[FlowActionStatusID] = @pflowPendingApprovedDec or it.[FlowActionStatusID] = @pflowRejectDec or it.[FlowActionStatusID] = @pflowProcessInv)";
                    }
                    e.DataSource.WhereParameters.Add("pCompID", DbType.String, Session["CompID"].ToString());
                    e.DataSource.WhereParameters.Add("pLocation", TypeCode.Int32, Session["PortLocation"].ToString());
                    e.DataSource.WhereParameters.Add("pSupplyMethodID", DbType.Guid, supplyMethodID);
                    e.DataSource.WhereParameters.Add("pPermitIssuerID", DbType.Guid, Session["PermitIssuerID"].ToString());
                    e.DataSource.WhereParameters.Add("pPermitIssued", DbType.Guid, flowPermitIssued);
                    e.DataSource.WhereParameters.Add("pPendingPayment", DbType.Guid, flowPendingPayment);
                    e.DataSource.WhereParameters.Add("pPendingCM", DbType.Guid, flowPendingCM);
                    e.DataSource.WhereParameters.Add("pflowProcessInv", DbType.Guid, flowProcessInv);
                    e.DataSource.WhereParameters.Add("pflowPendingApprovedDec", DbType.Guid, flowPendingApprovedDec);
                    e.DataSource.WhereParameters.Add("pflowRejectDec", DbType.Guid, flowRejectDec);
                    dbContext.Dispose();
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        //protected void dsPendingBDN_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        //{
        //    try
        //    {
        //        e.DataSource.Where = "it.[SupplyMethodID]=@pSupplyMethodID and it.[CompID]=@pCompID and it.[IsAppCompleted]=true  and it.[IsSubmitBDN]=false and it.[IsCancel]=false and it.[BDNCutOffHrs] >= 96";

        //        e.DataSource.WhereParameters["pCompID"].DefaultValue = Session["CompID"].ToString();
        //        e.DataSource.WhereParameters["pSupplyMethodID"].DefaultValue = WebConfigurationManager.AppSettings["STS"].ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
        //    }

        //}
    }
}