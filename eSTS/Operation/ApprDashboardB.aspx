<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspiniaMethodB.Master" AutoEventWireup="true" CodeBehind="ApprDashboardB.aspx.cs" Inherits="eSTS.Operation.ApprDashboardB" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="alert alert-danger" id="error_alert" style="display: none">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
        <h4><i class="icon fa fa-check"></i>Alert!</h4>
        <dx:ASPxLabel ID="lblErrMsg" runat="server" Text="" CssClass="description" EnableViewState="False">
        </dx:ASPxLabel>
    </div>
    <div class="row">
        <asp:HiddenField runat="server" ID="hfApplicationID" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hfApplicationFlowID" ClientIDMode="Static" />
    </div>
    <div class="row">
         <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" ClientInstanceName="lPanel" Modal="true" runat="server"></dx:ASPxLoadingPanel>
          <dx:BootstrapPopupControl ID="pcReject" runat="server" Width="600px" CloseAction="CloseButton" CloseOnEscape="True" Modal="false"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcReject"
                            HeaderText="Reject Application" OnCallback="pcReject_Callback">
                            <ClientSideEvents EndCallback="function(s, e) {
	grid.Refresh();
    gridActive.Refresh();
}" />
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Remark / Reason</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapMemo ID="txtReject" runat="server" ClientInstanceName="txtReject">
                                                <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="btnCancel"></ValidationSettings>
                            </dx:BootstrapMemo>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-9" style="align-items: center">
                                        </div>
                                        <div class="col-lg-3" style="padding-top: 5px">
                                            <dx:BootstrapButton ID="btnRejectApp" runat="server" ClientInstanceName="btnRejectApp" AutoPostBack="False" Text="Reject" EnableTheming="True" >
                                                <CssClasses Icon="fa fa-times fa-lg" />
                                                <SettingsBootstrap RenderOption="Danger" />
                                                <ClientSideEvents Click="function(s, e) {
                                                    lPanel.Show();  
    e.processOnServer = true;  
	pcReject.PerformCallback();
	pcReject.Hide();

}" />
                                            </dx:BootstrapButton>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>

                        </dx:BootstrapPopupControl>
        <div class="col-lg-12">
            <div class="panel panel-warning">
                <div class="panel-heading">
                    <%--<i class="fa fa-warning"></i>--%> Pending FSU To Receiver Vessel Application
                </div>
                <div class="panel-body">
                    <%--<div class="form-group row">
                        <div class="col-lg-1 text-right" style="padding-bottom: 5px; padding-left: 30px">
                            <dx:BootstrapButton ID="btnReject" runat="server" AutoPostBack="False" Text="Reject" EnableTheming="True">
                                <CssClasses Icon="fa fa-remove" />
                                <SettingsBootstrap RenderOption="Danger" />
                                <ClientSideEvents Click="function(s, e) {
	ShowWindow('');
}" />
                            </dx:BootstrapButton>
                        </div>
                        <div class="col-lg-11 text-left" style="padding-left: 30px">
                            <dx:BootstrapButton ID="btnApprove" runat="server" AutoPostBack="true" Text="Verify" EnableTheming="True" OnClick="btnApprove_Click">
                                <CssClasses Icon="fa fa-check" />
                                <SettingsBootstrap RenderOption="Success" />
                                <ClientSideEvents Click="function(s, e) {
            lPanel.Show();  
    e.processOnServer = true;  
	
}" />
                            </dx:BootstrapButton>
                        </div>

                    </div>--%>
                    <div class="col-lg-12">
                        <asp:EntityDataSource ID="dsOperationApp" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_OperationApp" OnSelecting="dsOperationApp_Selecting" OrderBy="it.[EstOperationTime] desc"  Select="it.[OperationAppID], it.[SupplyMethodID], it.[CaseNum], it.[DeliveryLocation], it.[VSLatDegree], it.[VSLatMin], it.[VSLongDegree], it.[VSLongMin], it.[VRLatDegree], it.[VRLatMin], it.[VRLongDegree], it.[VRLongMin], it.[VSIMONo], it.[VSName], it.[VSFlag], it.[VSPortReg], it.[VSLOA], it.[VSGRT], it.[VSNRT], it.[VRLOA], it.[VRGRT], it.[VRNRT], it.[VRMMSINo], it.[UOMCode], it.[EstOilMT], it.[EstOperationDateTime], it.[EstOperationTime], it.[OilTypeDesc], it.[ActionStatus], it.[LabelColor], it.[VRIMONo], it.[VRName], it.[VRFlag], it.[VRPortReg], it.[CompanyName], it.[IsPayment], it.[PermitDocLink], it.[NPDocLink], it.[Lampiran1DocLink], it.[IsSubmitBL], it.[FlowActionStatusID],it.[VSLatitude],it.[VSLongitude],it.[SOCompanyName]" >
                          
                        </asp:EntityDataSource>
                    
                        <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid" DataSourceID="dsOperationApp" KeyFieldName="OperationAppID" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared" OnCommandButtonInitialize="grid_CommandButtonInitialize" >
                            <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                            <SettingsPager EnableAdaptivity="True" PageSize="3">
                            </SettingsPager>
                            <Columns>
                             <%--   <dx:BootstrapGridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                                </dx:BootstrapGridViewCommandColumn>--%>
                                <dx:BootstrapGridViewTextColumn FieldName="OperationAppID" Caption="#" VisibleIndex="1">
                                    <CssClasses DataCell="text-navy" />
                                    <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="lilView_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                          <dx:ASPxHyperLink ID="LilLampiran" runat="server" OnInit="LilLampiran_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CompID" VisibleIndex="2" Caption="ROC No." Visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CaseNum" VisibleIndex="3" Caption="Case Num">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="5" Caption="Company Name">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="DeliveryLocation" Caption="Delivery Location" visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="VSName"  Caption="Vessel Supplier (FSU)"  VisibleIndex="6" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="VRName"  Caption="Vessel Receiver"  VisibleIndex="7" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="UOMCode" Caption="Product Supply" ReadOnly="True" VisibleIndex="8">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="ActionStatus" Caption="Status" VisibleIndex="9" ReadOnly="True">
                                </dx:BootstrapGridViewTextColumn>
                            </Columns>
                            <SettingsSearchPanel Visible="True" />
                        </dx:BootstrapGridView>
                    </div>
                </div>
            </div>
        </div>
        </div>
        <dx:BootstrapPopupControl ID="pcPayment" runat="server" Width="430px" CloseAction="CloseButton" CloseOnEscape="True"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcPayment"
                            HeaderText="Payment &amp; Permit Petrol"  >
                            <ClientSideEvents EndCallback="function(s, e) {
	gridActive.Refresh();
}" />
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Payment Date</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapDateEdit ID="dtPaymentDate" ClientInstanceName="dtPaymentDate" runat="server" DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:BootstrapDateEdit>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Payment Time</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTimeEdit ID="paymentTime" ClientInstanceName="paymentTime" runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm"></dx:BootstrapTimeEdit>

                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Payment Amount</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTextBox ID="txtPaymentAmt" ClientInstanceName="txtPaymentAmt" runat="server" class="form-control required">
                                                <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..99&gt;" />
                                            </dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Receipt No</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTextBox ID="txtReceiptNo" ClientInstanceName="txtReceiptNo" runat="server" class="form-control required"></dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Permit Reference</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTextBox ID="txtPermitRef" ClientInstanceName="txtPermitRef" runat="server" class="form-control required"></dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6" style="align-items: center">
                                        </div>
                                        <div class="col-lg-6" style="padding-top: 5px">
                                            <dx:BootstrapButton ID="btnUpdatePayment" runat="server" AutoPostBack="False" Text="Save &amp; Generate Permit" EnableTheming="True">
                                                <CssClasses Icon="fa fa-save" />
                                                <SettingsBootstrap RenderOption="Success" />
                                                <ClientSideEvents Click="function(s, e) {
                                                         lPanel.Show();  
    e.processOnServer = true;  
	pcPayment.PerformCallback();
	pcPayment.Hide();
}" />
                                            </dx:BootstrapButton>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>

                        </dx:BootstrapPopupControl>
                   
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <%--<i class="fa fa-warning"></i>--%> Approved  FSU To Receiver Vessel Application
                </div>
                <div class="panel-body">
                    <div class="col-lg-12">
                        <asp:EntityDataSource ID="dsActive" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_OperationApp" OnSelecting="dsActiveApp_Selecting" OrderBy="it.[EstOperationTime] desc" Select="it.[OperationAppID], it.[SupplyMethodID], it.[CaseNum], it.[DeliveryLocation], it.[VSLatDegree], it.[VSLatMin], it.[VSLongDegree], it.[VSLongMin], it.[VRLatDegree], it.[VRLatMin], it.[VRLongDegree], it.[VRLongMin], it.[VSIMONo], it.[VSName], it.[VSFlag], it.[VSPortReg], it.[VSLOA], it.[VSGRT], it.[VSNRT], it.[VRLOA], it.[VRGRT], it.[VRNRT], it.[VRMMSINo], it.[UOMCode], it.[EstOilMT], it.[EstOperationDateTime], it.[EstOperationTime], it.[OilTypeDesc], it.[ActionStatus], it.[LabelColor], it.[VRIMONo], it.[VRName], it.[VRFlag], it.[VRPortReg], it.[CompanyName], it.[IsPayment], it.[PermitDocLink], it.[NPDocLink], it.[Lampiran1DocLink], it.[IsSubmitBL], it.[FlowActionStatusID],it.[VSLatitude],it.[VSLongitude],it.[SOCompanyName],it.[ValidPermit],it.[IsAmend],it.[IsAmendApprove]" >
                        </asp:EntityDataSource>
                        <dx:BootstrapGridView ID="gridActive" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridActive" DataSourceID="dsActive" KeyFieldName="OperationAppID" EnableTheming="False" OnHtmlDataCellPrepared="gridActive_HtmlDataCellPrepared">
                            <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsPager EnableAdaptivity="True" PageSize="3">
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
                                        <dx:ASPxHyperLink ID="linkPayment" runat="server" OnInit="lilPayment_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                          <%--<dx:ASPxHyperLink ID="lilNotis" runat="server" OnInit="lilNotis_Init" Target="_self">
                                        </dx:ASPxHyperLink>--%>
                                          <dx:ASPxHyperLink ID="LilLampiran" runat="server" OnInit="LilLampiran_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="lilPermit" runat="server" OnInit="lilPermit_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                           <dx:ASPxHyperLink ID="lilCM" runat="server" OnInit="lilCM_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                         <dx:ASPxHyperLink ID="lilCancel" runat="server" OnInit="lilCancel_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="CompID" VisibleIndex="1" Caption="ROC No." Visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CaseNum" VisibleIndex="2" Caption="Case Num">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="3" Caption="Company Name">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="DeliveryLocation" Caption="Delivery Location" visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="VSName"  Caption="Vessel Supplier (FSU)"  VisibleIndex="3" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="VRName"  Caption="Vessel Receiver"  VisibleIndex="4" ReadOnly="True" >
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

<%--    <div class="row">
        <div class="col-lg-12">
           <div class="panel panel-danger">
                <div class="panel-heading">
                    <%--<i class="fa fa-warning"></i> Pending BDN Exceeding 72 Hrs 
                </div>
                <div class="panel-body">
                    <div class="col-lg-12">
                        <asp:EntityDataSource ID="dsPendingBDN" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities"  EnableFlattening="False" EntitySetName="v_OperationApp" OnSelecting="dsPendingBDN_Selecting" Select="it.[OperationAppID], it.[SupplyMethodID], it.[CaseNum], it.[DeliveryLocation], it.[LatDegree], it.[LatMin], it.[LongDegree], it.[LongMin], it.[VSIMONo], it.[VSName], it.[VSFlag], it.[VSPortReg], it.[VSLOA], it.[VSGRT], it.[VSNRT], it.[VRLOA], it.[VRGRT], it.[VRNRT], it.[VRMMSINo], it.[UOMCode], it.[EstOilMT], it.[EstOperationDateTime], it.[EstOperationTime], it.[OilTypeDesc], it.[ActionStatus], it.[LabelColor], it.[VRIMONo], it.[VRName], it.[VRFlag], it.[VRPortReg],it.[CompanyName],it.[IsPayment],it.[PermitDocLink]" OrderBy="it.[EstOperationDateTime] desc" >
                        </asp:EntityDataSource>
                        <dx:BootstrapGridView ID="gridPendingBDN" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridPendingBDN" DataSourceID="dsPendingBDN" KeyFieldName="OperationAppID" EnableTheming="False" OnHtmlDataCellPrepared="gridPendingBDN_HtmlDataCellPrepared">
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
                                        <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="lilView_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="linkPayment" runat="server" OnInit="lilPayment_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="lilPermit" runat="server" OnInit="lilPermit_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="lilLongLat" runat="server" OnInit="lilLongLat_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="CompID" VisibleIndex="1" Caption="ROC No." Visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CaseNum" VisibleIndex="2" Caption="Case Num">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="3" Caption="Company Name">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="DeliveryLocation" Caption="Delivery Location" VisibleIndex="2" ReadOnly="True">
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
                        </dx:BootstrapGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <script type="text/javascript">
        function ShowWindow(Id) {
            pcReject.Show();
        }
        function popUpNewPayment(id, id2) {
            // alert(id);
            $("#hfApplicationID").val(id);
            $("#hfApplicationFlowID").val(id2);

            pcPayment.ShowWindow(pcPayment.GetWindow(0));

        }
        function popUpPayment(id, id2, dtPayYear, dtPayMth, dtPayDay, timePayHour, timePayMin, paymentAmt, receiptNo, permitRef) {
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
            $("#hfApplicationFlowID").val(id2);

            var sPayDate = new Date(dtPayYear, parseInt(dtPayMth)-1, parseInt(dtPayDay), 00, 00, 00);
            dtPaymentDate.SetValue(sPayDate);

            var sPayTime = new Date(dtPayYear, parseInt(dtPayMth)-1, parseInt(dtPayDay), timePayHour, timePayMin, 00);
            paymentTime.SetValue(sPayTime);

            txtPaymentAmt.SetValue(paymentAmt);
            txtReceiptNo.SetValue(receiptNo);
            txtPermitRef.SetValue(permitRef);
            pcPayment.ShowWindow(pcPayment.GetWindow(0));

        }
        function popUpLongLatForm(id, latDegree) {
            // alert(id);
            $("#hfApplicationID").val(id);

            pcLongLat.ShowWindow(pcLongLat.GetWindow(0));

        }
        function showMap(lat, long) {
            if (lat != "" || long != "")
                window.open("http://maps.google.com/maps?q=" + lat + "," + long + "&z=18");
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
        function successAlert() {
            var elem = document.getElementById('success_alert');
            elem.style.display = 'block';
            //$('#success_alert').delay(800).fadeOut('slow');
        }
        function errorAlert() {
            console.log("masuk");
            var elem = document.getElementById('error_alert');
            elem.style.display = 'block';
            // $('#error_alert').delay(800).fadeOut('slow');
        }
        function showRejectReason(reason) {
            txtReject.SetValue(reason);
            txtReject.SetEnabled(true);
            btnRejectApp.SetEnabled(true);
            pcReject.ShowWindow();

        }
    </script>
</asp:Content>
