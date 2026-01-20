<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspiniaMethodB.Master" AutoEventWireup="true" CodeBehind="OpAppB.aspx.cs" Inherits="eSTS.Operation.OpAppB" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="alert alert-success" id="success_alert" style="display: none">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            Record submit successfully.
        </div>
        <div class="alert alert-danger" id="error_alert" style="display: none">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            <dx:ASPxLabel ID="lblErrMsg" runat="server" Text="" CssClass="description" EnableViewState="False">
            </dx:ASPxLabel>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title">
                            <asp:HiddenField ID="hfApplicationID" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hfLicCompID" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hfSupplyMethodCode" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hfLicExpDate" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hfLicLocation" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hfLicVesselID" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hfCompID" runat="server" ClientIDMode="Static" />
                        </h3>
                        <h5><i class="fa fa-user"></i>STS Agent </h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-10">
                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                        <dt>Company Name:</dt>
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <dd class="mb-1">
                                            <h3 class="text-navy"><strong>
                                                <asp:Label ID="lblCompanyName" runat="server"></asp:Label></strong></h3>
                                        </dd>
                                    </div>
                                </dl>
                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                        <dt>Telephone No:</dt>
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <dd class="mb-1">
                                            <asp:Label ID="lblTelNo" runat="server"></asp:Label></dd>
                                    </div>
                                </dl>
                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                        <dt>Telephone No:</dt>
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <dd class="mb-1">
                                            <asp:Label ID="lblFaxNo" runat="server"></asp:Label></dd>
                                    </div>
                                </dl>
                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                        <dt>Contact Person Name:</dt>
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <dd class="mb-1">
                                            <dx:BootstrapTextBox ID="txtContactPerson" runat="server" MaxLength="100"></dx:BootstrapTextBox>
                                        </dd>
                                    </div>
                                </dl>
                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                        <dt>Contact Person Email Address:</dt>
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <dd class="mb-1">
                                            <dx:BootstrapTextBox ID="txtAgentEmail" runat="server" MaxLength="200"></dx:BootstrapTextBox>
                                        </dd>
                                    </div>
                                </dl>
                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <asp:Label ID="lblEmailMsg" runat="server" Text="Please use semi colon (;)  as an email separator e.g: abc@zzz.com.my;xyz@zzz.com.my" ForeColor="Blue"></asp:Label>
                                    </div>
                                </dl>
                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                        <dt>I/C Number:</dt>
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <dd class="mb-1">
                                            <dx:BootstrapTextBox ID="txtICNumber" runat="server" MaxLength="14"></dx:BootstrapTextBox>
                                        </dd>
                                    </div>
                                </dl>
                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                        <dt>Designation:</dt>
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <dd class="mb-1">
                                            <dx:BootstrapTextBox ID="txtDesignation" runat="server" MaxLength="100"></dx:BootstrapTextBox>
                                        </dd>
                                    </div>
                                </dl>

                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                        <dt>Agent Code:</dt>
                                        <font color="red">*</font>
                                    </div>
                                    <div class="col-sm-4 text-sm-left">
                                        <dd class="mb-1">
                                            <dx:BootstrapTextBox ID="txtAgentCode" runat="server" MaxLength="10"></dx:BootstrapTextBox>
                                        </dd>
                                    </div>
                                </dl>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-drivers-license-o"></i>STS Operator / Vessel FSU</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group row" id="divBONewApp" runat="server">
                            <label class="col-sm-2 h7 font-bold text-sm-right">STS Operator  <font color="red">*</font></label>
                            <div class="col-sm-5">
                                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" ClientInstanceName="lPanel" Modal="true" runat="server"></dx:ASPxLoadingPanel>
                                <dx:BootstrapComboBox ID="cbSTSO" runat="server" ClientInstanceName="cbSTSO" DataSourceID="dsSTSOperator" TextField="SOCompName" ValueField="SOCompID" OnValueChanged="cbSTSO_ValueChanged" AutoPostBack="True" class="form-control required" TextFormatString="{1}-{0}">
                                    <Fields>
                                        <dx:BootstrapListBoxField FieldName="SOCompName" />
                                        <dx:BootstrapListBoxField FieldName="SOCompID" />
                                    </Fields>
                                    <ClientSideEvents ValueChanged="function(s, e) {
                                               
            lPanel.Show();  
    e.processOnServer = true;  
	
}" />
                                </dx:BootstrapComboBox>
                                <asp:EntityDataSource ID="dsSTSOperator" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_AppointAgentNew" OrderBy="it.[SOCompName]" OnSelecting="dsSTSOperator_Selecting">
                                </asp:EntityDataSource>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Operation/AppointAgent.aspx">Add STS Operator</asp:HyperLink>
                            </div>
                        </div>
                        <div class="form-group row" id="divBOViewApp" runat="server">
                            <label class="col-sm-2 h7 font-bold text-sm-right">STS Operator</label>
                            <div class="col-sm-6 text-sm-left">
                                <dx:BootstrapTextBox ID="txtBOName" runat="server" ReadOnly="true"></dx:BootstrapTextBox>
                            </div>
                            <div class="col-lg-2">
                                <div id="divFileAppoint" runat="server" class="form-group row">
                                    <asp:Literal ID="lilFile" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="col-sm-2 h7 font-bold text-sm-right">Vessel FSU Name  <font color="red">*</font></label>
                            <div class="col-sm-3">
                                <dx:BootstrapComboBox ID="cbFSU" runat="server" ClientInstanceName="cbFSU" OnCallback="cbFSU_Callback" DataSourceID="dsFSU" TextField="ShipName" TextFormatString="{0}" ValueField="LicCompanyVesselID" ValueType="System.Guid" NullValueItemDisplayText="{0}" AutoPostBack="True" OnValueChanged="cbFSU_ValueChanged">
                                    <Fields>
                                        <dx:BootstrapListBoxField FieldName="ShipName" />
                                    </Fields>
                                </dx:BootstrapComboBox>
                                <asp:EntityDataSource ID="dsFSU" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_OpLicCompanyVessel" OnSelecting="dsFSU_Selecting" Where="it.[LicCompanyID]=@pLicCompanyID" Select="it.[LicCompanyVesselID], it.[LicCompanyID], it.[ShipLicenseNo], it.[CompID], it.[ShipID], it.[OffNo], it.[ShipName], it.[PortReg], it.[CallSign], it.[IMONo], it.[YearReg], it.[YearBuilt], it.[GRT], it.[NRT], it.[LatDegree], it.[LatMin], it.[LatSec], it.[Latitude], it.[Longitude], it.[LongMin], it.[LongDegree], it.[LongSec]" EntityTypeFilter="">
                                    <WhereParameters>
                                        <asp:ControlParameter ControlID="cbSTSO" DbType="Guid" Name="pLicCompanyID" PropertyName="Value" />
                                    </WhereParameters>
                                </asp:EntityDataSource>
                            </div>
                            <div class="col-sm-1 text-left" style="padding: 0px">
                                <dx:BootstrapButton ID="btnEditFSU" ClientInstanceName="btnEditFSU" ToolTip="Edit Vessel FSU" runat="server" AutoPostBack="False" Text="Vessel">
                                    <CssClasses Icon="fa fa-pencil-square-o" />
                                    <SettingsBootstrap RenderOption="Success" />
                                    <ClientSideEvents Click="function(s, e) {
		ShowShipInfoForm('e');
}" />
                                </dx:BootstrapButton>
                            </div>
                            <div class="col-sm-1 text-left" style="padding: 0px">
                                <dx:BootstrapButton ID="btnRefreshFSU" runat="server" AutoPostBack="False" ToolTip="Refresh Vessel FSU" Text="Vessel" ClientInstanceName="btnRefreshFSU" OnClick="btnRefreshFSU_Click">
                                    <CssClasses Icon="fa fa-refresh" />
                                    <SettingsBootstrap RenderOption="Success" />
                                </dx:BootstrapButton>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>

                        <div class="form-group row" runat="server">
                            <label class="col-sm-2 h7 font-bold text-sm-right">License Validation Date:</label>

                            <div class="col-sm-4 text-sm-left">
                                <dx:ASPxLabel ID="lblLicValid" Font-Bold="true" runat="server"></dx:ASPxLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Supritendant Name</label>
                            <div class="col-lg-4">
                                <dx:BootstrapTextBox ID="txtSupName" ClientInstanceName="txtSupName" runat="server" MaxLength="50">
                                </dx:BootstrapTextBox>
                            </div>
                            <label class="col-sm-1 text-sm-right">Tel No.</label>
                            <div class="col-lg-3">
                                <dx:BootstrapTextBox ID="txtSupTelNo" ClientInstanceName="txtSupTelNo" runat="server" MaxLength="50">
                                </dx:BootstrapTextBox>

                            </div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                        <div class="form-group row" runat="server">
                            <label class="col-sm-2 text-sm-right">FSU Call Sign:</label>

                            <div class="col-sm-4 text-sm-left">
                                <dx:BootstrapTextBox ID="txtFSUCallSign" ClientInstanceName="txtFSUCallSign" runat="server" MaxLength="10">
                                </dx:BootstrapTextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Latitude (N)</label>
                            <div class="col-lg-2">
                                <dx:BootstrapTextBox ID="txtLatDegree" ClientInstanceName="txtLatDegree" runat="server">
                                    <ValidationSettings ValidationGroup="validLatLong">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                    <MaskSettings Mask="&lt;0..999&gt;" />
                                </dx:BootstrapTextBox>
                                <medium> Degree</medium>
                            </div>
                            <div class="col-lg-2">
                                <dx:BootstrapTextBox ID="txtLatMin" ClientInstanceName="txtLatMin" runat="server" MaxLength="50">
                                    <ValidationSettings ValidationGroup="validLatLong">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>

                                    <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..9999&gt;" />

                                </dx:BootstrapTextBox>
                                <medium> Minute</medium>
                            </div>
                            <div class="col-lg-6">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Longitude (E)</label>
                            <div class="col-lg-2">
                                <dx:BootstrapTextBox ID="txtLongDegree" ClientInstanceName="txtLongDegree" runat="server">
                                    <MaskSettings Mask="&lt;0..999&gt;" />
                                    <ValidationSettings ValidationGroup="validLatLong">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:BootstrapTextBox>
                                <medium> Degree</medium>
                            </div>
                            <div class="col-lg-2">
                                <dx:BootstrapTextBox ID="txtLongMin" ClientInstanceName="txtLongMin" runat="server" MaxLength="50">

                                    <ValidationSettings ValidationGroup="validLatLong">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                    <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..9999&gt;" />
                                </dx:BootstrapTextBox>
                                <medium> Minute</medium>
                            </div>
                            <div class="col-lg-6">
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
      <%--  <div class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-drivers-license-o"></i>STS Operator License's Supporting Documents</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group row">
                            <div class="col-sm-10 text-right" style="padding: 0px">
                                <dx:BootstrapButton ID="btnSODocRefresh" ClientInstanceName="btnSODocRefresh" ToolTip="Refresh STS Operator Document" runat="server" AutoPostBack="False" Text="Document">
                                    <CssClasses Icon="fa fa-refresh" />
                                    <SettingsBootstrap RenderOption="Success" />
                                    <ClientSideEvents Click="function(s, e) {
	gridSODoc.Refresh();
}" />
                                </dx:BootstrapButton>
                            </div>
                            <div class="col-sm-2">
                                <dx:BootstrapButton ID="btnSODocAdd" runat="server" AutoPostBack="False" ToolTip="Add STS Operator Document" Text="Document" ClientInstanceName="btnSODocAdd">
                                    <CssClasses Icon="fa fa-plus" />
                                    <SettingsBootstrap RenderOption="Success" />
                                    <ClientSideEvents Click="function(s, e) {
		ShowSOForm('e');
}" />
                                </dx:BootstrapButton>
                            </div>
                        </div>
                        <div class="form-group row">
                            <asp:EntityDataSource ID="dsSOdoc" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_LicCompanyAttach" Where="it.[LicCompanyID] = @pLicCompanyID" OnSelecting="dsSOdoc_Selecting" Select="it.[DocCode], it.[DocDesc], it.[DocType], it.[RefNo], it.[ValidFrom], it.[ValidTo], it.[Path]">
                                <WhereParameters>
                                    <asp:Parameter DbType="Guid" Name="pLicCompanyID" />
                                </WhereParameters>
                            </asp:EntityDataSource>

                            <dx:BootstrapGridView ID="gridSODoc" runat="server" AutoGenerateColumns="False" DataSourceID="dsSOdoc" ClientInstanceName="gridSODoc">
                                <SettingsAdaptivity AdaptivityMode="HideDataCells">
                                </SettingsAdaptivity>
                                <SettingsBehavior AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
                                <Columns>
                                    <dx:BootstrapGridViewTextColumn FieldName="DocCode" Caption="Document Code" ReadOnly="True" VisibleIndex="0">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="DocDesc" Caption="Document Type" VisibleIndex="1" ReadOnly="True">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="RefNo" Caption="Ref No." VisibleIndex="3" ReadOnly="True">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewDateColumn FieldName="ValidFrom" Caption="Valid From" VisibleIndex="4" ReadOnly="True">
                                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </PropertiesDateEdit>
                                    </dx:BootstrapGridViewDateColumn>
                                    <dx:BootstrapGridViewDateColumn FieldName="ValidTo" Caption="Valid To" VisibleIndex="5" ReadOnly="True">
                                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </PropertiesDateEdit>
                                    </dx:BootstrapGridViewDateColumn>
                                    <dx:BootstrapGridViewHyperLinkColumn Caption="Download" FieldName="Path" VisibleIndex="6" ReadOnly="True">
                                        <PropertiesHyperLinkEdit Text="Download" Target="_blank">
                                        </PropertiesHyperLinkEdit>
                                    </dx:BootstrapGridViewHyperLinkColumn>
                                </Columns>
                            </dx:BootstrapGridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-drivers-license-o"></i>STS FSU License's Supporting Documents</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group row">
                            <div class="col-sm-10 text-right" style="padding: 0px">
                                <dx:BootstrapButton ID="btnRefVesselDoc" ClientInstanceName="btnRefVesselDoc" ToolTip="Refresh Vessel Supplier Document" runat="server" Text="Document">
                                    <CssClasses Icon="fa fa-refresh" />
                                    <SettingsBootstrap RenderOption="Success" />
                                    <ClientSideEvents Click="function(s, e) {
	gridDocVesselFSU.Refresh();
}" />
                                </dx:BootstrapButton>
                            </div>
                            <div class="col-sm-2">
                                <dx:BootstrapButton ID="btnAddVesselDoc" runat="server" ToolTip="Add Vessel Supplier Document" Text="Document" ClientInstanceName="btnAddVesselDoc">
                                    <CssClasses Icon="fa fa-plus" />
                                    <SettingsBootstrap RenderOption="Success" />
                                    <ClientSideEvents Click="function(s, e) {
		ShowShipInfoForm('e');
}" />
                                </dx:BootstrapButton>
                            </div>
                        </div>
                        <div class="form-group row">
                            <asp:EntityDataSource ID="dsDocVesselFSU" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EntitySetName="v_LicCompanyVesselAttach" Where="it.[LicCompanyVesselID] = @pLicCompanyVesselID" OnSelecting="dsDocVesselFSU_Selecting" Select="it.[DocCode], it.[DocDesc], it.[LicenseNo], it.[ValidFrom], it.[ValidTo], it.[Path]" EnableFlattening="False">
                                <WhereParameters>
                                    <asp:Parameter DbType="Guid" Name="pLicCompanyVesselID" />
                                </WhereParameters>
                            </asp:EntityDataSource>

                            <dx:BootstrapGridView ID="gridDocVesselFSU" runat="server" AutoGenerateColumns="False" DataSourceID="dsDocVesselFSU" ClientInstanceName="gridDocVesselFSU">
                                <SettingsAdaptivity AdaptivityMode="HideDataCells">
                                </SettingsAdaptivity>

                                <SettingsBehavior AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
                                <Columns>
                                    <dx:BootstrapGridViewTextColumn FieldName="DocCode" Caption="Document Code" ReadOnly="True" VisibleIndex="0">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="DocDesc" Caption="Document Type" VisibleIndex="1" ReadOnly="True">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="LicenseNo" Caption="License No." VisibleIndex="3" ReadOnly="True">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewDateColumn FieldName="ValidFrom" Caption="Valid From" VisibleIndex="4" ReadOnly="True">
                                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </PropertiesDateEdit>
                                    </dx:BootstrapGridViewDateColumn>
                                    <dx:BootstrapGridViewDateColumn FieldName="ValidTo" Caption="Valid To" VisibleIndex="5" ReadOnly="True">
                                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </PropertiesDateEdit>
                                    </dx:BootstrapGridViewDateColumn>
                                    <dx:BootstrapGridViewHyperLinkColumn Caption="Download" FieldName="Path" VisibleIndex="6" ReadOnly="True">
                                        <PropertiesHyperLinkEdit Text="Download" Target="_blank">
                                        </PropertiesHyperLinkEdit>
                                    </dx:BootstrapGridViewHyperLinkColumn>
                                </Columns>
                            </dx:BootstrapGridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-location-arrow"></i>Operation Location</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <%-- </fieldset>--%>

                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Delivery Location  <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapComboBox ID="cbDeliveryLoc" ClientInstanceName="cbDeliveryLoc" runat="server" DataSourceID="dsDeliveryLoc" TextField="DeliveryLocation" ValueField="DeliveryLocID" ValueType="System.Guid">
                                </dx:BootstrapComboBox>
                                <asp:EntityDataSource ID="dsDeliveryLoc" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" OnSelecting="dsDeliveryLoc_Selecting" EnableFlattening="False" Where="it.[Location]=@pLocation" EntitySetName="MSDeliveryLocs" EntityTypeFilter="" Select="">
                                    <WhereParameters>
                                        <asp:Parameter DbType="Int32" Name="pLocation" />
                                    </WhereParameters>

                                </asp:EntityDataSource>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">MARDEPT Port Office (Permit Issuer)  <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapComboBox ID="cbPermitIssuer" ClientInstanceName="cbPermitIssuer" runat="server" DataSourceID="dsPermitIssuer" TextField="PermitIssuer" ValueField="MSPermitIssuerID" ValueType="System.Guid" OnCallback="cbDeliveryLoc_Callback">
                                </dx:BootstrapComboBox>
                                <asp:EntityDataSource ID="dsPermitIssuer" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" Where="it.[MSDeliveryLocID]=@pMSDeliveryLocID" EntitySetName="MSPermitIssuers" OnSelecting="dsPermitIssuer_Selecting" EntityTypeFilter="" Select="">
                                    <WhereParameters>
                                        <asp:Parameter DbType="Guid" Name="pMSDeliveryLocID" />
                                    </WhereParameters>

                                </asp:EntityDataSource>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-ship"></i>Vessel Receiver</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Vessel Receiver IMO No <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapTextBox ID="txtIMONo" runat="server" ClientInstanceName="txtIMONo" MaxLength="50"></dx:BootstrapTextBox>
                            </div>
                            <div class="col-sm-1" style="padding: 0px">
                                <dx:BootstrapButton ID="btnPopupSearch" runat="server" AutoPostBack="False">
                                    <CssClasses Icon="fa fa-search" />
                                    <SettingsBootstrap RenderOption="Warning" />
                                    <ClientSideEvents Click="function(s, e) {
	pcSearchShip.Show();
}" />
                                </dx:BootstrapButton>
                            </div>
                            <div class="col-sm-1" style="padding: 0px">
                                <label>Port Register  <font color="red">*</font></label>

                            </div>

                            <div class="col-sm-3">
                                <dx:BootstrapTextBox ID="txtPortReg" runat="server" ClientInstanceName="txtPortReg" MaxLength="50"></dx:BootstrapTextBox>
                            </div>

                            <dx:BootstrapPopupControl ID="pcSearchShip" runat="server" ClientInstanceName="pcSearchShip" EncodeHtml="false" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="500px" CloseAction="CloseButton" CloseOnEscape="true" HeaderText="<span class='fa fa-search fa-2x' aria-hidden='true' style='color:#9ccf39;'></span> <b> SEARCH VESSEL </b>" OnCallback="pcSearchShip_Callback">
                                <SettingsAdaptivity Mode="OnWindowInnerWidth" />
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <div class="form-group row">
                                            <label class="col-sm-3 text-sm-right">Vessel Name</label>
                                            <div class="col-sm-7">
                                                <dx:BootstrapTextBox ID="txtSearchShipName" runat="server" ClientInstanceName="txtSearchShipName"></dx:BootstrapTextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-3 text-sm-right">Vessel IMO No</label>
                                            <div class="col-sm-7">
                                                <dx:BootstrapTextBox ID="txtSearchIMONo" runat="server" ClientInstanceName="txtSearchIMONo"></dx:BootstrapTextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-3 text-sm-right">Vessel Off. No</label>
                                            <div class="col-sm-7">
                                                <dx:BootstrapTextBox ID="txtOffNo" runat="server" ClientInstanceName="txtOffNo"></dx:BootstrapTextBox>
                                            </div>
                                            <div class="col-sm-1" style="padding: 0">
                                                <dx:BootstrapButton ID="btnShipSearch" CssClasses-Icon="fa fa-search" runat="server" AutoPostBack="False" Text=" ">
                                                    <SettingsBootstrap RenderOption="Warning" />
                                                    <CssClasses Icon="fa fa-search"></CssClasses>

                                                    <ClientSideEvents Click="function(s, e) {
	pcSearchShip.PerformCallback();
}" />
                                                </dx:BootstrapButton>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                <dx:BootstrapGridView ID="gridSearchShip" runat="server" AutoGenerateColumns="False" KeyFieldName="ShipID" ClientInstanceName="gridSearchShip">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCells">
                                                    </SettingsAdaptivity>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                    <Columns>
                                                        <dx:BootstrapGridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                                                        </dx:BootstrapGridViewCommandColumn>
                                                        <dx:BootstrapGridViewTextColumn FieldName="ShipID" ReadOnly="True" Visible="False" VisibleIndex="1">
                                                        </dx:BootstrapGridViewTextColumn>
                                                        <dx:BootstrapGridViewTextColumn FieldName="OffNo" VisibleIndex="5">
                                                        </dx:BootstrapGridViewTextColumn>
                                                        <dx:BootstrapGridViewTextColumn FieldName="ShipName" VisibleIndex="2" Caption="Vessel Name">
                                                        </dx:BootstrapGridViewTextColumn>
                                                        <dx:BootstrapGridViewTextColumn FieldName="PortReg" VisibleIndex="6">
                                                        </dx:BootstrapGridViewTextColumn>
                                                        <dx:BootstrapGridViewTextColumn FieldName="CallSign" VisibleIndex="3">
                                                        </dx:BootstrapGridViewTextColumn>
                                                        <dx:BootstrapGridViewTextColumn FieldName="IMONo" VisibleIndex="4">
                                                        </dx:BootstrapGridViewTextColumn>
                                                        <dx:BootstrapGridViewTextColumn FieldName="ShipFlag" VisibleIndex="7">
                                                        </dx:BootstrapGridViewTextColumn>
                                                        <dx:BootstrapGridViewTextColumn FieldName="ShipType" VisibleIndex="7">
                                                        </dx:BootstrapGridViewTextColumn>
                                                        <dx:BootstrapGridViewTextColumn FieldName="ShipTypeID" VisibleIndex="7">
                                                        </dx:BootstrapGridViewTextColumn>
                                                    </Columns>
                                                    <ClientSideEvents SelectionChanged="grid_SelectionChanged" />
                                                </dx:BootstrapGridView>
                                            </div>
                                        </div>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapPopupControl>

                        </div>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Vessel Receiver Name  <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapTextBox ID="txtVesselName" runat="server" ClientInstanceName="txtVesselName" MaxLength="50"></dx:BootstrapTextBox>
                            </div>
                            <label class="col-sm-2 text-sm-right">
                                Vessel Receiver Flag <font color="red">*</font>
                            </label>
                            <div class="col-sm-4">
                                <dx:BootstrapComboBox ID="cbFlag" runat="server" ClientInstanceName="cbFlag">
                                </dx:BootstrapComboBox>
                            </div>
                        </div>
                        <%--  <div class="form-group  row">
                          
                            <label class="col-sm-2 text-sm-right">
                                Vessel SupplierType <font color="red">*</font>
                            </label>
                            <div class="col-sm-4">
                                <dx:BootstrapComboBox ID="cbVesselType" runat="server" ClientInstanceName="cbVesselType">
                                </dx:BootstrapComboBox>
                            </div>
                            
                        </div>--%>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Vessel Receiver GRT  <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapTextBox ID="txtGRT" runat="server" ClientInstanceName="txtGRT">
                                    <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..99&gt;" />
                                </dx:BootstrapTextBox>
                            </div>
                            <label class="col-sm-2 text-sm-right">Vessel Receiver NRT <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapTextBox ID="txtNRT" runat="server" ClientInstanceName="txtNRT">
                                    <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..99&gt;" />
                                </dx:BootstrapTextBox>
                            </div>

                        </div>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Vessel Receiver LOA <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapTextBox ID="txtLOA" runat="server" ClientInstanceName="txtLOA">
                                    <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..99&gt;" />
                                </dx:BootstrapTextBox>
                            </div>
                            <label class="col-sm-2 text-sm-right">Vessel MMSI No. <%--<font color="red">*</font>--%></label>
                            <div class="col-sm-4">
                                <dx:BootstrapTextBox ID="txtMMSINo" runat="server" ClientInstanceName="txtMMSINo" MaxLength="20"></dx:BootstrapTextBox>
                            </div>

                        </div>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Vessel Call Sign <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapTextBox ID="txtCallSign" runat="server" ClientInstanceName="txtCallSign" MaxLength="20">
                                </dx:BootstrapTextBox>
                            </div>
                            <div class="col-sm-6">
                            </div>

                        </div>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Last port of Call <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapComboBox ID="cbLastPort" runat="server" ClientInstanceName="cbLastPort" TextField="port_code" TextFormatString="{0}-{1}" ValueField="locode" NullValueItemDisplayText="{0}-{1}" DataSourceID="dsLastPort">
                                    <Fields>
                                        <dx:BootstrapListBoxField FieldName="country_name" />
                                        <dx:BootstrapListBoxField FieldName="port_name" />
                                    </Fields>
                                </dx:BootstrapComboBox>
                                <asp:EntityDataSource ID="dsLastPort" runat="server" ConnectionString="name=MMSSyncEntities" DefaultContainerName="MMSSyncEntities" EnableFlattening="False" EntitySetName="Ports" Where="" OrderBy="it.[locode]" Select="it.[locode], it.[country_code], it.[country_name], it.[port_code], it.[port_name]">
                                </asp:EntityDataSource>
                            </div>
                            <label class="col-sm-2 text-sm-right">Next port of Call <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapComboBox ID="cbNextPort" runat="server" ClientInstanceName="cbNextPort" TextField="port_code" TextFormatString="{0}-{1}" ValueField="locode" NullValueItemDisplayText="{0}-{1}" DataSourceID="dsNextPort">
                                    <Fields>
                                        <dx:BootstrapListBoxField FieldName="country_name" />
                                        <dx:BootstrapListBoxField FieldName="port_name" />
                                    </Fields>
                                </dx:BootstrapComboBox>
                                <asp:EntityDataSource ID="dsNextPort" runat="server" ConnectionString="name=MMSSyncEntities" DefaultContainerName="MMSSyncEntities" EnableFlattening="False" EntitySetName="Ports" Where="" OrderBy="it.[locode]" Select="it.[locode], it.[country_code], it.[country_name], it.[port_code], it.[port_name]">
                                </asp:EntityDataSource>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-tint"></i>Product Supply</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Operation Date/Time <font color="red">*</font></label>
                            <div class="col-sm-4">
                                <dx:BootstrapDateEdit ID="dtOperationDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom" runat="server"></dx:BootstrapDateEdit>
                            </div>
                            <div class="col-sm-3">
                                <dx:BootstrapTimeEdit ID="timeOperation" runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm"></dx:BootstrapTimeEdit>
                            </div>
                            <div class="col-sm-3">
                            </div>
                        </div>
                        <div class="form-group  row">
                            <label class="col-sm-2 text-sm-right">
                                Oil Type <font color="red">*</font>
                            </label>
                            <div class="col-sm-4">
                                <dx:BootstrapComboBox ID="cbOilType" runat="server" DataSourceID="dsOilType" TextField="OilTypeDesc" ValueField="OilTypeID" ValueType="System.Guid">
                                    <CssClasses Button="form-control m-b" />
                                </dx:BootstrapComboBox>
                                <asp:EntityDataSource ID="dsOilType" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" OrderBy="it.[OilTypeDesc]" EntitySetName="MSOilTypes">
                                </asp:EntityDataSource>
                            </div>

                        </div>
                        <div class="form-group row">
                            <label class="col-sm-2 text-sm-right">Oil Quantity <font color="red">*</font></label>
                            <div class="col-sm-2">
                                <dx:BootstrapTextBox ID="txtMT" runat="server">
                                    <MaskSettings Mask="&lt;0..999999g&gt;.&lt;00..99&gt;" />
                                </dx:BootstrapTextBox>
                            </div>
                            <label class="col-sm-1 text-sm-right">
                                Unit <font color="red">*</font>
                            </label>
                            <div class="col-sm-2">
                                <dx:BootstrapComboBox ID="cbUOM" runat="server" DataSourceID="dsUOM" TextField="UOMDesc" ValueField="UOMID" ValueType="System.Guid">
                                    <CssClasses Button="form-control m-b" />
                                </dx:BootstrapComboBox>
                                <asp:EntityDataSource ID="dsUOM" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" OrderBy="it.[UOMDesc]" EntitySetName="MSUOMs" EntityTypeFilter="" Select="it.[UOMID], it.[UOMCode], it.[UOMDesc], it.[IsActive]" Where="it.[IsActive]=true">
                                </asp:EntityDataSource>
                            </div>
                            <div class="col-sm-5">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            <div class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-file-text-o"></i> Permit Bunkering </h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div id="divAttachForm" runat="server" class="all-form-element-inner">
                            <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Document Type <font color="red">*</font></label>
                                <div class="col-sm-5" style="padding: 0px">
                                    <dx:BootstrapComboBox ID="cbAttachType" NullText="Select.." runat="server" DataSourceID="dsSetupAttachType" TextField="DocDesc" ValueField="MSDocTypeID" ValueType="System.Guid" TextFormatString="{0}-{1}" NullValueItemDisplayText="{0}-{1}">
                                        <Fields>
                                            <dx:BootstrapListBoxField FieldName="DocCode" />
                                            <dx:BootstrapListBoxField FieldName="DocDesc" />
                                        </Fields>
                                    </dx:BootstrapComboBox>
                                </div>
                                <!--Permit Expiry Date-->
                                <%-- <label class="col-sm-2 text-sm-right"  >Document Title <font color="red">*</font></label>
                                    <div class="col-sm-3" style="padding: 0px">
                                        <dx:BootstrapTextBox ID="txtAttachDocDesc" NullText="Enter" runat="server">
                                            <ValidationSettings ValidationGroup="SaveAttach">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:BootstrapTextBox>
                                    </div>--%>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Attachment File <font color="red">*</font></label>
                                <div class="col-sm-5" style="padding: 0px">
                                    <dx:BootstrapUploadControl ID="uploadFile" runat="server">
                                        <%--<ClientSideEvents FileUploadComplete="onFileUploadComplete" FilesUploadStart="onFilesUploadStart" />--%>
                                        <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.png,.pdf" />

                                    </dx:BootstrapUploadControl>
                                    <small>Allowed file extensions: .jpg, .jpeg, .png, .pdf</small>
                                    <br />
                                    <small>Maximum file size: 4 MB.</small>

                                </div>
                                <div class="col-sm-4">
                                    <dx:BootstrapButton ID="btnSaveAttachDoc" runat="server" Text="Upload Attach" EnableTheming="True" OnClick="btnSaveAttachDoc_Click" ValidationGroup="SaveAttach">
                                        <CssClasses Icon="fa fa-upload" />
                                        <SettingsBootstrap RenderOption="Dark" />

                                        <%-- <ClientSideEvents Click="function(s, e) {
	gridAttach.PerformCallback();
}" />--%>
                                    </dx:BootstrapButton>
                                </div>

                            </div>
                        </div>
                        <div id="divAttachGrid" runat="server" class="form-group row" style="margin: 5px">
                            <asp:EntityDataSource ID="dsAttach" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_OperationAppAttach" EnableInsert="True" EnableUpdate="True" EnableDelete="True" Where="it.[OperationAppID] = @pOperationAppID and it.[ModuleID]='PP'" OnSelecting="dsAttach_Selecting">
                                <WhereParameters>
                                    <asp:Parameter DbType="Guid" Name="pOperationAppID" />
                                </WhereParameters>
                            </asp:EntityDataSource>
                            <asp:EntityDataSource ID="dsSetupAttachType" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_SuppDoc" Where="it.[ModuleID]='PP'" OrderBy="it.[DocCode]"></asp:EntityDataSource>
                            <dx:BootstrapGridView ID="gridAttach" runat="server" AutoGenerateColumns="False" DataSourceID="dsAttach" KeyFieldName="AttachID" ClientInstanceName="gridAttach" OnRowDeleting="gridAttach_RowDeleting" OnCommandButtonInitialize="gridAttach_CommandButtonInitialize">
                                  <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                                <SettingsCommandButton>
                                    <EditButton IconCssClass="fa fa-edit" Text=" " />
                                    <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                                </SettingsCommandButton>
                                <SettingsDataSecurity AllowDelete="True" />
                                <SettingsBehavior AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
                                <Columns>
                                    <dx:BootstrapGridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0">
                                    </dx:BootstrapGridViewCommandColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="AttachID" ReadOnly="True" Visible="False" VisibleIndex="10">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="AttchTypeID" Visible="False" VisibleIndex="12">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn Caption="Code" FieldName="DocCode" VisibleIndex="1">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn Caption="Document Type" FieldName="DocDesc" VisibleIndex="2">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="DocType" Visible="False" VisibleIndex="3">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewHyperLinkColumn Caption="Download" FieldName="Path" VisibleIndex="4">
                                        <PropertiesHyperLinkEdit Text="Download" Target="_blank">
                                        </PropertiesHyperLinkEdit>
                                    </dx:BootstrapGridViewHyperLinkColumn>
                                </Columns>
                            </dx:BootstrapGridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="row" id="divAck" runat="server">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-check-square-o"></i>Company Acknowledgement & Integrity Clause</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group row">
                            <div class="col-sm-10">
                                &nbsp;<dx:ASPxCheckBox ID="chkAck" runat="server" Text="I hereby / we acknowledged that all details of information above mentioned is true, with the latest of company information. I agree to notify LPJ immediately, if there are any change in the company information states in this registration form. I, we agree to comply rules and regulation enforced upon from time to time. ">
                                    <ValidationSettings ValidationGroup="btnSubmit" SetFocusOnError="True">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-10">
                                &nbsp;<dx:ASPxCheckBox ID="chkIntegrity" runat="server" Text="By Checking the checkbox, you have agreed to the Integrity Clause By LPJ.">
                                    <ValidationSettings ValidationGroup="btnSubmit" SetFocusOnError="True">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox>
                                <label data-toggle="modal" data-target="#myModal5" style="">
                                    <u style="text-decoration-color: blue">View Integrity Clause</u>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divCancel" runat="server">
        <div class="form-group row">
            <label class="col-sm-2 col-form-label">Cancel Reason <font color="red">*</font></label>
            <div class="col-lg-4">
                <dx:BootstrapMemo ID="txtCancel" runat="server" ClientInstanceName="txtCancel">
                    <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="btnCancel"></ValidationSettings>
                </dx:BootstrapMemo>
            </div>
            <div class="col-lg-2">
                <dx:BootstrapButton ID="btnCancel" runat="server" Text="Submit Cancellation" EnableTheming="True" OnClick="btnCancel_Click" ValidationGroup="btnCancel" CausesValidation="False">
                    <CssClasses Icon="fa fa-location-arrow" />
                    <SettingsBootstrap RenderOption="danger" />
                    <ClientSideEvents Click="function(s, e) {
       lPanel.Show();  
    e.processOnServer = true;  
	}" />
                </dx:BootstrapButton>
            </div>
            <div class="col-lg-4">
            </div>
        </div>
    </div>
    <div id="divAmend" runat="server">
        <div class="form-group row">
            <label class="col-sm-2 col-form-label">Amendment Reason <font color="red">*</font></label>
            <div class="col-lg-4">
                <dx:BootstrapMemo ID="txtAmend" runat="server" ClientInstanceName="txtAmend">
                    <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="btnAmend"></ValidationSettings>
                </dx:BootstrapMemo>
            </div>
            <div class="col-lg-2">
                <dx:BootstrapButton ID="btnAmend" runat="server" Text="Submit Amendments" EnableTheming="True" OnClick="btnAmend_Click" ValidationGroup="btnAmend" CausesValidation="False">
                    <CssClasses Icon="fa fa-location-arrow" />
                    <SettingsBootstrap RenderOption="Primary" />
                    <ClientSideEvents Click="function(s, e) {
       lPanel.Show();  
    e.processOnServer = true;  
	}" />
                </dx:BootstrapButton>
            </div>
            <div class="col-lg-4">
            </div>
        </div>
    </div>
    <div class="form-group row" id="divApply" runat="server">
        <div class="col-sm-4 col-sm-offset-1">
            <dx:BootstrapButton ID="btnDraft" runat="server" Text="Draft" EnableTheming="True" OnClick="btnDraft_Click">
                <CssClasses Icon="fa fa-pencil" />
                <SettingsBootstrap RenderOption="Default" />

                <ClientSideEvents Click="function(s, e) {
	       lPanel.Show();  
    e.processOnServer = true;  

}" />

            </dx:BootstrapButton>
            <dx:BootstrapButton ID="btnSave" runat="server" Text="Submit" EnableTheming="True" OnClick="btnSave_Click" ValidationGroup="btnSubmit" CausesValidation="False">
                <CssClasses Icon="fa fa-location-arrow" />
                <SettingsBootstrap RenderOption="Primary" />
                <ClientSideEvents Click="function(s, e) {
       lPanel.Show();  
    e.processOnServer = true;  
	}" />
            </dx:BootstrapButton>
        </div>
    </div>
    <div id="divApprover" runat="server">
        <dx:BootstrapPopupControl ID="pcReject" runat="server" Width="600px" CloseAction="CloseButton" CloseOnEscape="True" Modal="false"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcReject"
            HeaderText="Cancel Application">
            <ClientSideEvents EndCallback="function(s, e) {
	grid.Refresh();
}" />
            <ContentCollection>
                <dx:ContentControl runat="server">
                    <div class="form-group row">
                        <label class="col-sm-4 col-form-label">Remark / Reason</label>
                        <div class="col-lg-8">
                            <%-- <dx:BootstrapMemo ID="txtReject" runat="server" ClientInstanceName="txtReject">
                                                <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="btnCancel"></ValidationSettings>
                            </dx:BootstrapMemo>--%>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-9" style="align-items: center">
                        </div>
                        <div class="col-lg-3" style="padding-top: 5px">
                            <dx:BootstrapButton ID="btnRejectApp" runat="server" AutoPostBack="False" Text="Reject" EnableTheming="True">
                                <CssClasses Icon="fa fa-times fa-lg" />
                                <SettingsBootstrap RenderOption="Danger" />
                                <ClientSideEvents Click="function(s, e) {
	pcReject.PerformCallback();
	pcReject.Hide();
}" />
                            </dx:BootstrapButton>
                        </div>
                    </div>
                </dx:ContentControl>
            </ContentCollection>

        </dx:BootstrapPopupControl>

        <div class="form-group row">
            <label class="col-sm-2 col-form-label">Reject Reason <font color="red">*</font></label>
            <div class="col-lg-4">
                <dx:BootstrapMemo ID="txtReject" runat="server" ClientInstanceName="txtReject">
                    <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="btnCancel"></ValidationSettings>
                </dx:BootstrapMemo>
            </div>
            <div class="col-lg-6">
            </div>
        </div>


        <div class="col-sm-4 col-sm-offset-1">

            <dx:BootstrapButton ID="btnReject" runat="server" Text="Reject" EnableTheming="True" OnClick="btnReject_Click">
                <CssClasses Icon="fa fa-times" />
                <SettingsBootstrap RenderOption="Danger" />
            </dx:BootstrapButton>
            <dx:BootstrapButton ID="btnApprove" runat="server" Text="Approve" EnableTheming="True" OnClick="btnApprove_Click">
                <CssClasses Icon="fa fa-check" />
                <SettingsBootstrap RenderOption="Success" />
                <ClientSideEvents Click="function(s, e) {
                     var result=confirm('Are you sure to approve this application?');
                                    if(result                                                                                                                                                                                                                                                                       
                                    {   
                                        e.processOnServer = true
                                        lPanel.Show();  
                                    }
                                    else
                                        e.processOnServer = false;
}" />
            </dx:BootstrapButton>
        </div>

    </div>
    <div class="form-group row">
        <div class="col-sm-8">
            <div id="myModal5" aria-hidden="true" class="modal inmodal fade" role="dialog" tabindex="-1">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button class="close" data-dismiss="modal" type="button">
                                <span aria-hidden="true">×</span><span class="sr-only">Close</span>
                            </button>
                            <h4 class="modal-title">INTEGRITY CLAUSE</h4>

                        </div>
                        <div class="modal-body">
                            <p>I/Company or our servants hereby declare that I/company or our servants will not offer a bribe to Johor Port Authority&rsquo;s servants or other individual which involved direct or indirect business practice to get the approved license.</p>
                            <p>If I/Company or servants is found to have violated or involved in violation of the integrity pact of any corrupt business practice, then I/Company or servants shall be entitled to:</p>
                            <p>
                                Termination of the license or<br />
                                Blacklisted and<br />
                                Disciplinary action following by Malaysian government procurement regulations<br />
                                If I/Company or our servants receive an offer/ a bribe from Johor Port Authority&rsquo;s servants or other individual which involved direct or indirect to give the approved license, I/Company or our servants promises that I/Company or our servants will report to Malaysian Anti-Corruption Commission (MACC) or police station immediately.
                            </p>
                            <p>&nbsp;</p>
                            <p>Saya/ Syarikat dengan ini mengisytiharkan bahawa saya atau mana-mana individu dalam yang mewakili syarikat ini tidak akan menawar atau memberi rasuah kepada mana-mana individu dalam Lembaga Pelabuhan Johor atau mana-mana individu lain, sebagai ganjaran mendapatkan kelulusan lesen seperti di atas.</p>
                            <p>Sekiranya saya atau mana-mana individu yang mewakili syarikat ini di dapati bersalah menawar atau memberi rasuah kepada mana-mana individu dalam Lembaga Pelabuhan Johor atau mana-mana individu lain sebagai ganjaran mendapatkan kelulusan lesen seperti di atas, maka saya sebagai wakil syarikat bersetuju tindakan-tindakan berikut diambil :</p>
                            <p>
                                Penarikan balik lesen aktiviti pelabuhan; dan<br />
                                Disenarai hitam untuk mohon lesen aktiviti pelabuhan; atau<br />
                                Lain-lain tindakan tatatertib mengikut peraturan Perolehan Kerajaan.<br />
                                Sekiranya terdapat mana-mana individu cuba meminta rasuah daripada saya atau mana-mana individu yang berkaitan dengan syarikat ini sebagai ganjaran mendapatkan sebut harga seperti di atas, maka saya berjanji akan dengan segera melaporkan perbuatan tersebut kepada pejabat Suruhanjaya Rasuah Malaysia(SPRM) atau balai polis yang berhampiran.
                            </p>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-2">
        </div>
        <div class="col-sm-2">
        </div>
    </div>

    <div class="col-lg-8 m-b-lg">
        <asp:Literal ID="lilTimeline" runat="server"></asp:Literal>
    </div>
    <script type="text/javascript">
        function ShowWindow(Id) {
            pcReject.Show();
        }
        function grid_SelectionChanged(s, e) {
            console.log('masuk');
            s.GetSelectedFieldValues("ShipName;PortReg;IMONo;ShipFlag;ShipTypeID", GetSelectedFieldValuesCallback);
        }
        function GetSelectedFieldValuesCallback(values) {
            console.log(" ShipName " + values[0][0]);
            console.log(" ShipID " + values[0][1]);
            console.log(" IMONo " + values[0][2]);
            console.log(" ShipFlag " + values[0][3]);
            console.log(" ShipTypeID " + values[0][4]);

            txtVesselName.SetText(values[0][0]);
            txtPortReg.SetText(values[0][1]);
            txtIMONo.SetText(values[0][2]);
            cbFlag.SetValue(values[0][3]);
            //cbVesselType.SetValue(values[0][4]);

            txtSearchShipName.SetText('');
            txtSearchIMONo.SetText('');
            txtOffNo.SetText('');

            pcSearchShip.PerformCallback();
            pcSearchShip.Hide();

        }

        function ShowSOForm(mode) {
            var licComp = $('#hfLicCompID').val();
            window.open("<%= ResolveUrl("~/License/STSOperatorLic.aspx?") %>" + "mode=" + mode + "&sno=" + licComp, "_blank");
        }
        function ShowShipInfoForm(mode) {
            var cid = cbFSU.GetValue();
            var licComp = $('#hfLicVesselID').val();
            if (cid != "")
                window.open("<%= ResolveUrl("~/License/ShipLicenseInfo.aspx?") %>" + "m=" + mode + "&lno=" + licComp + "&sid=" + cid, "_blank");
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
    </script>
</asp:Content>
