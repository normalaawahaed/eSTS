<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="ShipLicenseInfo.aspx.cs" Inherits="eSTS.License.ShipLicenseInfo" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField runat="server" ID="hfLicCompID" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfShipID" ClientIDMode="Static" />
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="alert alert-success" id="success_alert" style="display: none">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            Record saved successfully.
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
                        <h3 class="box-title"></h3>
                        <h5>STS Operator</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">
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
                                        <dt>Location:</dt>
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <dd class="mb-1">
                                            <h3 class="text-navy"><strong>
                                                <asp:Label ID="lblLocation" runat="server"></asp:Label></strong></h3>
                                        </dd>
                                    </div>
                                </dl>
                                <dl class="row mb-0">
                                    <div class="col-sm-4 text-sm-right">
                                        <dt>License Expired Date:</dt>
                                    </div>
                                    <div class="col-sm-8 text-sm-left">
                                        <dd class="mb-1">
                                            <h3 class="text-navy"><strong>
                                                <asp:Label ID="lblExpDate" runat="server"></asp:Label></strong></h3>
                                        </dd>
                                    </div>
                                </dl>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            <div class="col-lg-12">
                <div class="ibox" id="divShip" runat="server">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5>FSU Vessel Particular</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="col-lg-12">
                            <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Vessel Name <font color="red">*</font></label>
                                <asp:HiddenField runat="server" ID="hfShipRecID" ClientIDMode="Static" />
                                <div class="col-sm-4" style="padding: 0px">
                                    <dx:BootstrapTextBox ID="txtShipName" ClientInstanceName="txtShipName" ReadOnly="true" runat="server" MaxLength="50">
                                        <ValidationSettings ValidationGroup="AddShip">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:BootstrapTextBox>
                                </div>
                                <div class="col-sm-1" style="padding-right: 0">
                                    <dx:BootstrapButton ID="btnPopupSearch" runat="server" AutoPostBack="False">
                                        <CssClasses Icon="fa fa-search" />
                                        <SettingsBootstrap RenderOption="Warning" />
                                        <ClientSideEvents Click="function(s, e) {
	pcSearchShip.Show();
}" />
                                    </dx:BootstrapButton>
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
                                                        <dx:BootstrapTextBox ID="txtIMONo" runat="server" ClientInstanceName="txtIMONo" MaxLength="20"></dx:BootstrapTextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-3 text-sm-right">Vessel Official No</label>
                                                    <div class="col-sm-7">
                                                        <dx:BootstrapTextBox ID="txtOffNo" runat="server" ClientInstanceName="txtOffNo" MaxLength="40"></dx:BootstrapTextBox>
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
                                                                <dx:BootstrapGridViewTextColumn FieldName="ShipName" VisibleIndex="2">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="PortReg" VisibleIndex="6">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="CallSign" VisibleIndex="3">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="IMONo" VisibleIndex="4">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="YearReg" VisibleIndex="7">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                 <dx:BootstrapGridViewTextColumn FieldName="GRT"  Visible="False" VisibleIndex="4">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                 <dx:BootstrapGridViewTextColumn FieldName="NRT"  Visible="False" VisibleIndex="4">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                 <dx:BootstrapGridViewTextColumn FieldName="LOA"  Visible="False" VisibleIndex="4">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                 <dx:BootstrapGridViewTextColumn FieldName="OffNo"  Visible="False" VisibleIndex="4">
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

                                <div class="col-sm-5">
                                </div>
                            </div>
                            <div class="form-group row">
                               <label class="col-sm-2 text-sm-right">Vessel IMO No. <font color="red">*</font></label>
                            <div class="col-sm-2"  style="padding: 0px">
                                <dx:BootstrapTextBox ID="txtIMONo2" runat="server" ReadOnly="true" ClientInstanceName="txtIMONo2"></dx:BootstrapTextBox>
                            </div>
                            <label class="col-sm-2 text-sm-right">Vessel Official No.  <font color="red">*</font></label>
                            <div class="col-sm-2" style="padding: 0px">
                                <dx:BootstrapTextBox ID="txtOffNo2" runat="server" ReadOnly="true" ClientInstanceName="txtOffNo2"></dx:BootstrapTextBox>
                            </div>
                                  <div class="col-lg-4">
                                                    </div>
                        </div>
                            <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Vessel NRT <font color="red">*</font></label>
                                <div class="col-sm-2"  style="padding: 0px">
                                     <dx:BootstrapTextBox ID="txtNRT" runat="server" ClientInstanceName="txtNRT">
                                    <MaskSettings Mask="&lt;0..999999g&gt;.&lt;00..99&gt;" />
                                </dx:BootstrapTextBox>
                                </div>
                                <label class="col-sm-2 text-sm-right">Vessel GRT  <font color="red">*</font></label>
                                <div class="col-sm-2"  style="padding: 0px">
                                    <dx:BootstrapTextBox ID="txtGRT" runat="server" ClientInstanceName="txtGRT"> 
                                        <MaskSettings Mask="&lt;0..999999g&gt;.&lt;00..99&gt;" />
                                </dx:BootstrapTextBox>
                                </div>
                                  <div class="col-lg-4">
                                                    </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Vessel LOA <font color="red">*</font></label>
                                <div class="col-sm-2"  style="padding: 0px">
                                    <dx:BootstrapTextBox ID="txtLOA" runat="server" ClientInstanceName="txtLOA">
                                     <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..99&gt;" />
                                </dx:BootstrapTextBox>
                                </div>
                                <label class="col-sm-2 text-sm-right">Vessel MMSI No.  <font color="red">*</font></label>
                                <div class="col-sm-2"  style="padding: 0px">
                                    <dx:BootstrapTextBox ID="txtMMSINo" runat="server" ClientInstanceName="txtMMSINo"></dx:BootstrapTextBox>
                                </div>
                                  <div class="col-lg-4">
                                                    </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Vessel Call Sign <font color="red">*</font></label>
                                <div class="col-sm-2"  style="padding: 0px">
                                    <dx:BootstrapTextBox ID="txtCallSign" runat="server" ClientInstanceName="txtCallSign"></dx:BootstrapTextBox>
                                </div>
                               
                                  <div class="col-lg-8">
                                                    </div>
                            </div>
                            <div class="form-group row">
                                                    <label class="col-sm-2 text-sm-right">Latitude (N) <font color="red">*</font></label>
                                                    <div class="col-lg-1" style="padding: 0px">
                                                        <dx:BootstrapTextBox ID="txtLatDegree" ClientInstanceName="txtLatDegree" runat="server">
                                                            <ValidationSettings ValidationGroup="validLatLong">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <MaskSettings Mask="&lt;0..999&gt;" />
                                                        </dx:BootstrapTextBox>
                                                        <medium> Degree</medium>
                                                    </div>
                                                    <div class="col-lg-2" >
                                                        <dx:BootstrapTextBox ID="txtLatMin" ClientInstanceName="txtLatMin" runat="server">
                                                            <ValidationSettings ValidationGroup="validLatLong">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>

                                                            <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..9999&gt;" />

                                                        </dx:BootstrapTextBox>
                                                        <medium> Minute</medium>
                                                    </div>
                                                    <div class="col-lg-7">
                                                    </div>
                                                </div>
                            <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Longitude (E) <font color="red">*</font></label>
                                <div class="col-lg-1" style="padding: 0px">
                                    <dx:BootstrapTextBox ID="txtLongDegree" ClientInstanceName="txtLongDegree" runat="server">
                                        <MaskSettings Mask="&lt;0..999&gt;" />
                                        <ValidationSettings ValidationGroup="validLatLong">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:BootstrapTextBox>
                                    <medium> Degree</medium>
                                </div>
                                <div class="col-lg-2" >
                                    <dx:BootstrapTextBox ID="txtLongMin" ClientInstanceName="txtLongMin" runat="server" >

                                        <ValidationSettings ValidationGroup="validLatLong">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..9999&gt;" />
                                    </dx:BootstrapTextBox>
                                    <medium> Minute</medium>
                                </div>
                                <div class="col-lg-7">
                                </div>
                            </div>
                             <div class="form-group row">
                                  <div class="col-lg-12 text-right">
                           <dx:BootstrapButton ID="btnSaveBO" runat="server" AutoPostBack="true" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save">
                                    <CssClasses Icon="fa fa-save" />
                                    <SettingsBootstrap RenderOption="Success" />
                                </dx:BootstrapButton>
                                 </div>
                                 </div>
                        </div>
                    </div>
              
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox" id="div1" runat="server">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5>Supporting Documents (FSU Vessel)</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="col-lg-12">
                              <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Document Type <font color="red">*</font></label>
                                <div class="col-sm-5" style="padding: 0px">
                                     <asp:EntityDataSource ID="dsSetupAttachType" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_SuppDoc" Where="it.[ModuleID]='STSVL'" OrderBy="it.[DocCode]"></asp:EntityDataSource>
                               
                                      <dx:BootstrapComboBox ID="cbAttachType" NullText="Select.." runat="server" DataSourceID="dsSetupAttachType" TextField="DocDesc" ValueField="MSDocTypeID" ValueType="System.Guid" TextFormatString="{0}-{1}" NullValueItemDisplayText="{0}-{1}">
                                            <Fields>
                                                <dx:BootstrapListBoxField FieldName="DocCode" />
                                                <dx:BootstrapListBoxField FieldName="DocDesc" />
                                            </Fields>
                                        </dx:BootstrapComboBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Ref. No</label>
                                <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static" />
                                <div class="col-sm-4" style="padding: 0px">
                                    <dx:BootstrapTextBox ID="txtLicenseNo" ClientInstanceName="txtLicenseNo" runat="server" MaxLength="50">
                                        <%--    <ValidationSettings ValidationGroup="AddShip">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>--%>
                                    </dx:BootstrapTextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 text-sm-right">Valid From <font color="red">*</font></label>
                                <asp:HiddenField runat="server" ID="HiddenField4" ClientIDMode="Static" />
                                <div class="col-sm-3" style="padding: 0px">
                                    <dx:BootstrapDateEdit ID="dtValidFrom" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom" runat="server">
                                        <ValidationSettings ValidationGroup="AddShip">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:BootstrapDateEdit>
                                </div>
                                <label class="col-sm-1 text-sm-right">To</label>
                                <div class="col-sm-3">
                                    <dx:BootstrapDateEdit ID="dtValidTo" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom" runat="server">
                                        <ValidationSettings ValidationGroup="AddShip">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:BootstrapDateEdit>
                                </div>
                            </div>
                            <div id="divAttachForm" runat="server" class="all-form-element-inner">
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
                                        </dx:BootstrapButton>
                                    </div>

                                </div>
                            </div>
                            <div id="divAttachGrid" runat="server" class="form-group row" style="margin: 5px">
                                <asp:EntityDataSource ID="dsAttach" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="LicCompanyVesselAttaches" EnableInsert="True" EnableUpdate="True" EnableDelete="True" Where="it.[LicCompanyVesselID] = @pLicCompanyVesselID" OnSelecting="dsAttach_Selecting">
                                    <WhereParameters>
                                        <asp:Parameter DbType="Guid" Name="pLicCompanyVesselID" />
                                    </WhereParameters>
                                </asp:EntityDataSource>
                                <dx:BootstrapGridView ID="gridAttach" runat="server" AutoGenerateColumns="False" DataSourceID="dsAttach" KeyFieldName="AttachID" ClientInstanceName="gridAttach" OnRowDeleting="gridAttach_RowDeleting">
                                      <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                                    <SettingsCommandButton>
                                        <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                                    </SettingsCommandButton>
                                    <SettingsDataSecurity AllowDelete="True" />
                                    <SettingsBehavior AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
                                    <Columns>
                                        <dx:BootstrapGridViewCommandColumn ShowDeleteButton="True"  VisibleIndex="0">
                                        </dx:BootstrapGridViewCommandColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="AttachID" ReadOnly="True" Visible="False" VisibleIndex="1">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="LicCompanyVesselID" VisibleIndex="2" Visible="False">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="LicenseNo" VisibleIndex="4">
                                        </dx:BootstrapGridViewTextColumn>
<dx:BootstrapGridViewDateColumn FieldName="ValidFrom" VisibleIndex="5">
     <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
</dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="ValidTo" VisibleIndex="6">
                                             <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                                        </dx:BootstrapGridViewDateColumn>
                                       <dx:BootstrapGridViewHyperLinkColumn Caption="Download" FieldName="Path" VisibleIndex="8">
                                            <PropertiesHyperLinkEdit Text="Download" Target="_blank">
                                            </PropertiesHyperLinkEdit>
                                        </dx:BootstrapGridViewHyperLinkColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" Visible="False" VisibleIndex="9">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" Visible="False" VisibleIndex="10">
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="UpdatedBy" Visible="False" VisibleIndex="11">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="UpdatedDate" Visible="False" VisibleIndex="12">
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewComboBoxColumn Caption="Document Type" FieldName="AttchTypeID" VisibleIndex="3">
                                            <PropertiesComboBox DataSourceID="dsSetupAttachType" TextField="DocDesc" ValueField="MSDocTypeID">
                                            </PropertiesComboBox>
                                        </dx:BootstrapGridViewComboBoxColumn>
                                    </Columns>
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function grid_SelectionChanged(s, e) {

            s.GetSelectedFieldValues("ShipName;ShipID;IMONo;GRT;NRT;LOA;OffNo", GetSelectedFieldValuesCallback);
        }
        function GetSelectedFieldValuesCallback(values) {
            console.log(" value " + values[0][0]);
            console.log(" value " + values[0][1]);
            console.log(" value " + values[0][2]);
            console.log(" value " + values[0][3]);
            console.log(" value " + values[0][4]);
            console.log(" value " + values[0][5]);
            console.log(" value " + values[0][6]);

            txtShipName.SetText(values[0][0]);
            txtIMONo2.SetText(values[0][2]);
            txtGRT.SetText(values[0][3]);
            txtNRT.SetText(values[0][4]);
            txtLOA.SetText(values[0][5]);
            txtOffNo2.SetText(values[0][6]);

            $("#hfShipRecID").val(values[0][1]);

            txtSearchShipName.SetText('');
            txtIMONo.SetText('');
            txtOffNo.SetText('');

            pcSearchShip.PerformCallback();
            pcSearchShip.Hide();

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
