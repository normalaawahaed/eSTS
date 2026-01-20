<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="STSOperatorLic.aspx.cs" Inherits="eSTS.License.STSOperatorLic" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function grid_SelectionChanged(s, e) {

            s.GetSelectedFieldValues("ShipName;ShipID", GetSelectedFieldValuesCallback);
        }
        function GetSelectedFieldValuesCallback(values) {
            console.log(" value " + values[0][0]);
            console.log(" value " + values[0][1]);

            txtShipName.SetText(values[0][0]);
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
        function ShowShipInfoForm() {
            var licComp = $('#hfLicCompID').val();
            window.open("<%= ResolveUrl("~/License/ShipLicenseInfo.aspx?m=n&lno=") %>" + licComp, "_blank");
        }
    </script>
    <asp:HiddenField runat="server" ID="hfLicCompID" ClientIDMode="Static" />
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="alert alert-success" id="success_alert" style="display: none">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            Record submit successfully.
        </div>
        <div class="alert alert-danger" id="error_alert" style="display: none">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            <dx:ASPxLabel ID="lblErrMsg" runat="server" ClientInstanceName="lblErrMsg" Text="" CssClass="description" EnableViewState="False">
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
                            </div>
                        </div>
                    </div>
                 <div class="ibox" id="divAttach" runat="server">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5>STS Operator's Document List</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="col-lg-12">
                            <div id="divAttachForm" runat="server" class="all-form-element-inner">
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Attachment Doc Type <font color="red">*</font></label>
                                    <div class="col-sm-5" style="padding: 0px">
                                        <asp:EntityDataSource ID="dsSetupAttachType" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_SuppDoc" Where="it.[ModuleID]='STSOL' and it.[DocStatus]=1" OrderBy="it.[DocCode]"></asp:EntityDataSource>
                               
                                        <dx:BootstrapComboBox ID="cbAttachType" NullText="Select.." runat="server" DataSourceID="dsSetupAttachType" TextField="DocDesc" ValueField="MSDocTypeID" ValueType="System.Guid" TextFormatString="{0}-{1}" NullValueItemDisplayText="{0}-{1}">
                                            <Fields>
                                                <dx:BootstrapListBoxField FieldName="DocCode" />
                                                <dx:BootstrapListBoxField FieldName="DocDesc" />
                                            </Fields>
                                        </dx:BootstrapComboBox>
                                    </div>

                                </div>
                                  <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Ref. No</label>
                                <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static" />
                                <div class="col-sm-4" style="padding: 0px">
                                    <dx:BootstrapTextBox ID="txtRefNo" ClientInstanceName="txtRefNo" runat="server" MaxLength="50">
                                      
                                    </dx:BootstrapTextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Valid From <font color="red">*</font></label>
                                <asp:HiddenField runat="server" ID="HiddenField4" ClientIDMode="Static" />
                                <div class="col-sm-3" style="padding: 0px">
                                    <dx:BootstrapDateEdit ID="dtValidFrom" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom" runat="server">
                                        <ValidationSettings ValidationGroup="AddDoc">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:BootstrapDateEdit>
                                </div>
                                <label class="col-sm-1 col-form-label">To</label>
                                <div class="col-sm-3">
                                    <dx:BootstrapDateEdit ID="dtValidTo" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom" runat="server">
                                        <ValidationSettings ValidationGroup="AddDoc">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:BootstrapDateEdit>
                                </div>
                            </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label">Attachment File <font color="red">*</font></label>
                                    <div class="col-sm-5" style="padding: 0px">
                                        <dx:BootstrapUploadControl ID="uploadFile" runat="server">
                                             <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.png,.pdf" />

                                        </dx:BootstrapUploadControl>
                                        <small>Allowed file extensions: .jpg, .jpeg, .png, .pdf</small>
                                        <br />
                                        <small>Maximum file size: 4 MB.</small>

                                    </div>
                                    <div class="col-sm-4">
                                        <dx:BootstrapButton ID="btnSaveAttachDoc" runat="server" Text="Upload Attach" EnableTheming="True" OnClick="btnSaveAttachDoc_Click" ValidationGroup="AddDoc">
                                            <CssClasses Icon="fa fa-upload" />
                                            <SettingsBootstrap RenderOption="Dark" />
                                            <%-- <ClientSideEvents Click="function(s, e) {
	gridAttach.PerformCallback();
}" />--%>
                                        </dx:BootstrapButton>
                                    </div>

                                </div>
                           
                            <div id="divAttachGrid" runat="server" class="form-group row" style="margin: 5px">
                                <asp:EntityDataSource ID="dsAttach" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_LicCompanyAttach" EnableInsert="True" EnableUpdate="True" EnableDelete="True" Where="it.[LicCompanyID] = @pLicCompanyID" OnSelecting="dsAttach_Selecting">
                                    <WhereParameters>
                                        <asp:Parameter DbType="Guid" Name="pLicCompanyID" />
                                    </WhereParameters>
                                </asp:EntityDataSource>
                                 <dx:BootstrapGridView ID="gridAttach" runat="server" AutoGenerateColumns="False" DataSourceID="dsAttach" KeyFieldName="AttachID" ClientInstanceName="gridAttach" OnRowDeleting="gridAttach_RowDeleting">
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
                                        <dx:BootstrapGridViewTextColumn FieldName="DocTitle" Visible="False" VisibleIndex="4">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewHyperLinkColumn Caption="Download" FieldName="Path" VisibleIndex="7">
                                            <PropertiesHyperLinkEdit Text="Download" Target="_blank">
                                            </PropertiesHyperLinkEdit>
                                        </dx:BootstrapGridViewHyperLinkColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" Visible="False" VisibleIndex="6">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" Visible="False" VisibleIndex="7">
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="UpdatedBy" Visible="False" VisibleIndex="8">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="UpdatedDate" Visible="False" VisibleIndex="9">
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="AttachID" ReadOnly="True" Visible="False" VisibleIndex="10">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="LicCompanyID" Visible="False" VisibleIndex="11">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="AttchTypeID" Visible="False" VisibleIndex="12">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Caption="Code" FieldName="DocCode" VisibleIndex="1">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Caption="Document Type" FieldName="DocDesc" VisibleIndex="2">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="DocType" Visible="False" VisibleIndex="3">
                                        </dx:BootstrapGridViewTextColumn>
                                         <dx:BootstrapGridViewTextColumn FieldName="RefNo" VisibleIndex="4">
                                        </dx:BootstrapGridViewTextColumn>
<dx:BootstrapGridViewDateColumn FieldName="ValidFrom" VisibleIndex="5">
     <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
</dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="ValidTo" VisibleIndex="6">
                                             <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                                            </dx:BootstrapGridViewDateColumn>
                                    </Columns>
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                    </div>
                </div>

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
                                   <div class="col-sm-11" style="padding: 0px">
                                       </div>
                                   <div class="col-sm-1">
                                    <dx:BootstrapButton ID="btnAddShip" runat="server" AutoPostBack="False" Text="Add FSU"  >
                                        <CssClasses Icon="fa fa-plus" />
                                        <SettingsBootstrap RenderOption="Success" />
                                       <%-- <ClientSideEvents Click="function(s, e) {
	gridShip.PerformCallback();
}" />--%>
                                         <ClientSideEvents Click="function(s, e) {
	ShowShipInfoForm();
}" />
                                    </dx:BootstrapButton>
                                </div>
                                   </div>
                            <div class="form-group row">
                                <dx:BootstrapGridView ID="gridShip" runat="server" AutoGenerateColumns="False" KeyFieldName="LicCompanyVesselID" ClientInstanceName="gridShip" DataSourceID="dsShip"  OnCommandButtonInitialize="gridShip_CommandButtonInitialize" >
                                     <ClientSideEvents EndCallback="function(s, e) {
    lblErrMsg.SetText('');

	if (s.cpDelete)
	{	
            successAlert();
    } 
    else if (s.cpDelete == false)
     {   
        lblErrMsg.SetText('Record failed to delete.!');
        errorAlert();
     }
}" />
                                   <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <Columns>
                                    
                                        <dx:BootstrapGridViewTextColumn FieldName="LicCompanyVesselID" Caption=" " ReadOnly="True" VisibleIndex="1">
                                              <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="lilView" runat="server" OnInit="lilView_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="lilEdit" runat="server" OnInit="lilEdit_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="LicCompanyID" VisibleIndex="2" Visible="False">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="ShipLicenseNo" VisibleIndex="10" Visible="False">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="ShipID" VisibleIndex="3" Visible="False">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="CompID" VisibleIndex="4" Visible="False">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="OffNo" VisibleIndex="6">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="ShipName" VisibleIndex="5" Caption="FSU">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="PortReg" VisibleIndex="8">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="CallSign" VisibleIndex="9">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="IMONo" VisibleIndex="7" Caption="IMO No">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="YearReg" VisibleIndex="11">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="YearBuilt" VisibleIndex="12">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="GRT" VisibleIndex="18" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="NRT" VisibleIndex="20" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="DWT" VisibleIndex="21" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="ShipType" VisibleIndex="13" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="VoyageType" VisibleIndex="16" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="LOA" VisibleIndex="22" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Status" VisibleIndex="23" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="OwnerName" VisibleIndex="24" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Owner" VisibleIndex="25" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="ShipFlag" VisibleIndex="26" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Breadth" VisibleIndex="27" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Depth" VisibleIndex="28" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="STDDraft" VisibleIndex="29" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="ShipCapacity" VisibleIndex="30" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="ShipBeam" VisibleIndex="31" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="DispmtWeight" VisibleIndex="32" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="DSLValidFrom" Caption="DSL Valid From" VisibleIndex="14" Visible="False">
                                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                            </PropertiesDateEdit>
                                            <SettingsEditForm Visible="True" />
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="DSLValidTo" Caption="DSL Valid To" VisibleIndex="15" Visible="False">
                                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                            </PropertiesDateEdit>
                                            <SettingsEditForm Visible="True" />
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="STSPermitValidFrom" VisibleIndex="17" Visible="False">
                                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                            </PropertiesDateEdit>
                                            <SettingsEditForm Visible="True" />
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="STSPermitValidTo" VisibleIndex="19" Visible="False">
                                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                            </PropertiesDateEdit>
                                            <SettingsEditForm Visible="True" />
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" VisibleIndex="33" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" VisibleIndex="34" Visible="False">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="UpdatedBy" Visible="False" VisibleIndex="35">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewDateColumn FieldName="UpdatedDate" Visible="False" VisibleIndex="36">
                                            <SettingsEditForm Visible="False" />
                                        </dx:BootstrapGridViewDateColumn>
                                    </Columns>
                                </dx:BootstrapGridView>
                                <asp:EntityDataSource ID="dsShip" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="LicCompanyVessels" Where="it.[LicCompanyID]=@pLicCompanyID" EnableDelete="True" EnableInsert="True" EnableUpdate="True">
                                    <WhereParameters>
                                        <asp:ControlParameter ControlID="hfLicCompID" DbType="Guid" Name="pLicCompanyID" PropertyName="Value" />
                                    </WhereParameters>
                                </asp:EntityDataSource>
                            </div>
                        </div>
                    </div>
                </div>
               

            </div>
        </div>
    </div>
</asp:Content>
