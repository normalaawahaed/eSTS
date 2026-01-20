<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="ApplicantDashboard.aspx.cs" Inherits="eSTS.Operation.ApplicantDashboard" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <dx:BootstrapPopupControl ID="popupReminder" HeaderText="Reminder: Pending BDN" CssClasses-ModalBackground="" runat="server">
        
        <ContentCollection>
<dx:ContentControl runat="server">
    <asp:Label ID="lblPopupReminder" runat="server"></asp:Label>
            </dx:ContentControl>
</ContentCollection>
        
    </dx:BootstrapPopupControl>
        <div class="modal inmodal" id="myModal4" tabindex="-1" role="dialog"  aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content animated fadeIn">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                            <h4 class="modal-title"><i class="fa fa-warning text-warning"></i>  ATTENTION</h4>
                                        </div>
                                        <div class="modal-body">
                                          
                                        </div>
                                    </div>
                                </div>
                            </div>

    <asp:HiddenField runat="server" ID="hfApplicationID" ClientIDMode="Static" />
    
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-warning">
                <div class="panel-heading">
                    <%--<i class="fa fa-warning"></i>--%> Pending Supply Vessel To FSU Application
                </div>
                <div class="panel-body">
                    <div class="col-lg-12">
                        <dx:BootstrapPopupControl ID="pcBDN" runat="server" Width="430px" CloseAction="CloseButton" CloseOnEscape="True" Modal="false"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcPayment"
                            HeaderText="Payment Information"  >
                            <ClientSideEvents EndCallback="function(s, e) {
	grid.Refresh();
}" />
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Payment Date</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapDateEdit ID="dtPaymentDate" ClientInstanceName="dtPaymentDate" runat="server" ReadOnly="true" DisplayFormatString="dd/MM/yyyy"  EditFormatString="dd/MM/yyyy" EditFormat="Custom"></dx:BootstrapDateEdit>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Payment Time</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTimeEdit ID="paymentTime" ClientInstanceName="paymentTime" runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm" ReadOnly="true"></dx:BootstrapTimeEdit>

                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Payment Amount</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTextBox ID="txtPaymentAmt" ClientInstanceName="txtPaymentAmt" runat="server" class="form-control required" ReadOnly="true">
                                                <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..99&gt;" />
                                            </dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Receipt No</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTextBox ID="txtReceiptNo" ClientInstanceName="txtReceiptNo" runat="server" MaxLength="50" class="form-control required" ReadOnly="true"></dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Permit Reference</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTextBox ID="txtPermitRef" ClientInstanceName="txtPermitRef" runat="server" MaxLength="50" class="form-control required" ReadOnly="true"></dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-9" style="align-items: center">
                                        </div>
                                        <div class="col-lg-3" style="padding-top: 5px">
                                            <dx:BootstrapButton ID="btnUpdatePayment" runat="server" AutoPostBack="False" Text="Submit" EnableTheming="True" Visible="False">
                                                <CssClasses Icon="fa fa-save" />
                                                <SettingsBootstrap RenderOption="Success" />
                                                <ClientSideEvents Click="function(s, e) {
	pcPayment.PerformCallback();
	pcPayment.Hide();
}" />
                                            </dx:BootstrapButton>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>

                        </dx:BootstrapPopupControl>
                       <dx:BootstrapPopupControl ID="pcCancel" runat="server" Width="600px" CloseAction="CloseButton" CloseOnEscape="True"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcCancel"
                            HeaderText="Cancel Application" OnCallback="pcCancel_Callback">
                            <ClientSideEvents EndCallback="function(s, e) {
console.log(&quot;endcallback&quot;);
	grid.Refresh();
gridActive.Refresh();
}
" />
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Remark / Reason</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapMemo ID="txtCancelReason" runat="server" ClientInstanceName="txtCancelReason">
                                                <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="btnCancel">
<RequiredField IsRequired="True"></RequiredField>
                                                </ValidationSettings>
                            </dx:BootstrapMemo>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-9" style="align-items: center">
                                        </div>
                                        <div class="col-lg-3" style="padding-top: 5px">
                                            <dx:BootstrapButton ID="btnCancel" runat="server" AutoPostBack="False" Text="Cancel" EnableTheming="True" >
                                                <CssClasses Icon="fa fa-times fa-lg text-success" />
                                                <SettingsBootstrap RenderOption="Danger" />
                                                <ClientSideEvents Click="function(s, e) {
	pcCancel.PerformCallback();
	pcCancel.Hide();
}" />
                                            </dx:BootstrapButton>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>

                        </dx:BootstrapPopupControl>
                             <dx:BootstrapPopupControl ID="pcRejectReason" runat="server" Width="600px" CloseAction="CloseButton" CloseOnEscape="True"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcRejectReason"
                            HeaderText="Reject Reason / Remark" >
                                  <ClientSideEvents EndCallback="function(s, e) {
	grid.Refresh();
}
" />
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Remark / Reason</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapMemo ID="txtRejectReason" runat="server" ClientInstanceName="txtRejectReason" ReadOnly="true">
                            </dx:BootstrapMemo>
                                        </div>
                                    </div>
                                    </dx:ContentControl>
                            </ContentCollection>

                        </dx:BootstrapPopupControl>
                        <asp:EntityDataSource ID="dsOperationApp" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_OperationApp" OnSelecting="dsOperationApp_Selecting" OrderBy="it.[EstOperationTime] desc"  Select="it.[OperationAppID], it.[SupplyMethodID], it.[CaseNum], it.[DeliveryLocation], it.[VSIMONo], it.[VSName], it.[VSFlag], it.[VSPortReg], it.[VSLOA], it.[VSGRT], it.[VSNRT], it.[VSLatDegree], it.[VSLatMin], it.[VSLongDegree], it.[VSLongMin], it.[VRLOA], it.[VRGRT], it.[VRNRT], it.[VRMMSINo], it.[VRLatDegree], it.[VRLatMin], it.[VRLongDegree], it.[VRLongMin], it.[UOMCode], it.[EstOilMT], it.[EstOperationDateTime], it.[EstOperationTime], it.[OilTypeDesc], it.[ActionStatus], it.[LabelColor], it.[VRIMONo], it.[VRName], it.[VRFlag], it.[VRPortReg], it.[IsDraft],it.[FlowActionStatusID], it.[Lampiran1DocLink],it.[VSLatitude],it.[VSLongitude],it.[VRLatitude],it.[VRLongitude],it.[SOCompanyName]">
                           
                        </asp:EntityDataSource>
                        <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsOperationApp" EnableTheming="False" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared" ClientInstanceName="grid">
                            <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsPager PageSize="5">
                            </SettingsPager>
                            <SettingsCommandButton>
                                <EditButton IconCssClass="fa fa-edit text-success" Text=" " />
                                <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                            </SettingsCommandButton>
                            <Columns>
                               <dx:BootstrapGridViewTextColumn FieldName="OperationAppID" Caption="#" VisibleIndex="0">
                                    <CssClasses DataCell="text-navy" />
                                    <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="lilView_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                         <dx:ASPxHyperLink ID="LilLampiran" runat="server" OnInit="LilLampiran_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                           <dx:ASPxHyperLink ID="lilCancel" runat="server" OnInit="lilCancel_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CaseNum" Caption="Case Num" VisibleIndex="1" ReadOnly="True">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="DeliveryLocation" Caption="Delivery Location" visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="VSName"  Caption="Vessel Supplier"  VisibleIndex="3" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="VRName"  Caption="Vessel Receiver (FSU)"  VisibleIndex="4" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="UOMCode" Caption="Product Supply" ReadOnly="True" VisibleIndex="5">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="ActionStatus" Caption="Status" VisibleIndex="6" ReadOnly="True">
                                </dx:BootstrapGridViewTextColumn>
                            </Columns>
                            <SettingsSearchPanel Visible="True" />
                        </dx:BootstrapGridView>
                    </div>
                </div>
                <%--<div class="ibox ">
                <div class="ibox-content">
              
                </div>
            </div>--%>
            </div>
        </div>
    </div>
   <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <%--<i class="fa fa-warning"></i>--%> Approved  Supply Vessel To FSU Application
                </div>
                <div class="panel-body">
                    <div class="col-lg-12">
                        <asp:EntityDataSource ID="dsActive" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_OperationApp" OnSelecting="dsActiveApp_Selecting" OrderBy="it.[EstOperationTime]  desc" Select="it.[OperationAppID], it.[SupplyMethodID], it.[CaseNum], it.[DeliveryLocation], it.[VSLatDegree], it.[VSLatMin], it.[VSLongDegree], it.[VSLongMin], it.[VRLatDegree], it.[VRLatMin], it.[VRLongDegree], it.[VRLongMin], it.[VSIMONo], it.[VSName], it.[VSFlag], it.[VSPortReg], it.[VSLOA], it.[VSGRT], it.[VSNRT], it.[VRLOA], it.[VRGRT], it.[VRNRT], it.[VRMMSINo], it.[UOMCode], it.[EstOilMT], it.[EstOperationDateTime], it.[EstOperationTime], it.[OilTypeDesc], it.[ActionStatus], it.[LabelColor], it.[VRIMONo], it.[VRName], it.[VRFlag], it.[VRPortReg], it.[CompanyName], it.[IsPayment], it.[PermitDocLink], it.[PaymentDate], it.[PaymentDate], it.[PaymentTime], it.[PaymentAmount], it.[ReceiptNo], it.[ReceiptNo], it.[PaymentRefID], it.[NPDocLink], it.[Lampiran1DocLink], it.[FlowActionStatusID], it.[IsSubmitCM],it.[VSLatitude],it.[VSLongitude],it.[VRLatitude],it.[VRLongitude],it.[ActionStatusSeq],it.[SOCompanyName],it.[ValidPermit],it.[IsAmend],it.[IsAmendApprove]" >
                        </asp:EntityDataSource>
                        <dx:BootstrapGridView ID="gridActive" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridActive" DataSourceID="dsActive" KeyFieldName="OperationAppID" EnableTheming="False" OnHtmlDataCellPrepared="gridActive_HtmlDataCellPrepared">
                            <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsCommandButton>
                                <EditButton IconCssClass="fa fa-edit text-success" Text=" " />
                                <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                            </SettingsCommandButton>
                            <Columns>
                                <dx:BootstrapGridViewTextColumn FieldName="OperationAppID" Caption="#" VisibleIndex="0">
                                    <CssClasses DataCell="text-navy" />
                                    <DataItemTemplate>
                                             
                                        <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="lilView2_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                         <dx:ASPxHyperLink ID="lilCancel" runat="server" OnInit="lilCancel2_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="linkPayment" runat="server" OnInit="lilPayment_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <%--  <dx:ASPxHyperLink ID="lilNotis" runat="server" OnInit="lilNotis_Init" Target="_self">
                                        </dx:ASPxHyperLink>--%>
                                          <dx:ASPxHyperLink ID="LilLampiran" runat="server" OnInit="LilLampiran_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="lilPermit" runat="server" OnInit="lilPermit_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <!--  CR : EB/CR/2022/01/001
                                              Added by : Normala
                                              Date : 25/01/2022
                                                Reason/Purpose : Allow for Amendments 
                                            -->
                                          <dx:ASPxHyperLink ID="lilAmend" runat="server" OnInit="lilAmend_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                           <dx:ASPxHyperLink ID="lilCM" runat="server" OnInit="lilCM_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="CompID" VisibleIndex="1" Caption="ROC No." Visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CaseNum" VisibleIndex="2" Caption="Case Num">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="3" Caption="Company Name">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="DeliveryLocation" Caption="Delivery Location" VisibleIndex="2" visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="VSName"  Caption="Vessel Supplier"  VisibleIndex="3" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="VRName"  Caption="Vessel Receiver (FSU)"  VisibleIndex="4" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="UOMCode" Caption="Product Supply" ReadOnly="True" VisibleIndex="5">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="ActionStatus" Caption="Status" VisibleIndex="6" ReadOnly="True">
                                </dx:BootstrapGridViewTextColumn>
                            </Columns>
                            <SettingsSearchPanel Visible="True" />
                        </dx:BootstrapGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
                       
    <script type="text/javascript">
        
        function popUpPayment(id,  dtPayYear, dtPayMth, dtPayDay, timePayHour, timePayMin, paymentAmt, receiptNo, permitRef) {
            // alert(id);
            console.log("dtPayYear >" + dtPayYear);
            console.log("dtPayMth >" + dtPayMth);
            console.log("dtPayDay > " + dtPayDay);
            console.log("timePayHour >" + timePayHour);
            console.log("timePayMin >" + timePayMin);
            console.log("paymentAmt >" + paymentAmt);
            console.log("receiptNo >" + receiptNo);
            console.log("permitRef >" + permitRef);

            $("#hfApplicationID").val(id);

            var sPayDate = new Date(dtPayYear, parseInt(dtPayMth)-1, parseInt(dtPayDay), 00, 00, 00);
            console.log(sPayDate);
            dtPaymentDate.SetValue(sPayDate);

            var sPayTime = new Date(dtPayYear, parseInt(dtPayMth), parseInt(dtPayDay), timePayHour, timePayMin, 00);
            paymentTime.SetValue(sPayTime);

            txtPaymentAmt.SetValue(paymentAmt);
            txtReceiptNo.SetValue(receiptNo);
            txtPermitRef.SetValue(permitRef);
            pcPayment.ShowWindow(pcPayment.GetWindow(0));

        }
        function showMap(lat, long) {
            if (lat != "" || long != "")
                window.open("http://maps.google.com/maps?q=" + lat + "," + long + "&z=12");
            else {
                var msg = document.getElementById("lblErrMsg");
                if (lat == "")
                    msg = "Please enter Latitude";

                if (long == "")
                    msg = "Please enter Longitude";

                var x = document.getElementById("error_alert");
                x.style.display = "block";
            }
        }
        function popUpCancel(id) {
            // alert(id);
            $("#hfApplicationID").val(id);

            pcCancel.ShowWindow();

        }
        function showRejectReason(reason) {
            txtRejectReason.SetValue(reason);
            pcRejectReason.ShowWindow();

        }
        function ShowPopup(title, body) {
            //    var x = "<a href='#' class='btn btn-danger  dim btn-large-dim'>" + body + "</a><h4>Pending Bunker Delivery Note</h4>";
            //var x = "<h4>eB system found <span class='label label-danger'>" + body + "</span> BDN still pending.  Please submit BDN information immediately to avoid any inconvenience for the next bunkering application.</h4>";
            var x = "<h4>Effectively by 1st August 2021, BDN must be submitted within 72 hours after expiry of petroleum permit for bunker operation issued by JLWS. Failure to do so, applications are not allowed</h4>";
        $("#myModal4 .modal-body").html(x);
        $("#myModal4").modal("show");
    }
    </script>
</asp:Content>
