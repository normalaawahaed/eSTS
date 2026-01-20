<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="AccessGroup.aspx.cs" Inherits="eSTS.SystemSetup.AccessGroup" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <%--<div class="ibox-title">
                            <h5>jQuery Grid Plugin – jqGrid</h5>
                        </div>--%>
                <div class="ibox-content">

                    <asp:EntityDataSource ID="dsAccessGroup" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="AccessGroups" OrderBy="it.[AccessGroupName]">
                    </asp:EntityDataSource>

                    <asp:EntityDataSource ID="dsAccessModule" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="AccessModules" OrderBy="it.[ModuleTitle]">
                    </asp:EntityDataSource>

                    <asp:EntityDataSource ID="dsLocation" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="MSPortLocations">
                    </asp:EntityDataSource>

                    <asp:EntityDataSource ID="dsPermitIssuer" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="MSPermitIssuers" OrderBy="">
                    </asp:EntityDataSource>

                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsAccessGroup" KeyFieldName="AccessGroupID" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating">
                        <SettingsAdaptivity AdaptivityMode="HideDataCells">
                        </SettingsAdaptivity>
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsCommandButton>
                            <EditButton IconCssClass="fa fa-edit text-success" Text=" " />
                            <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                        </SettingsCommandButton>
                        <SettingsDataSecurity AllowDelete="True" AllowEdit="True" AllowInsert="True" />
                        <Columns>
                            <dx:BootstrapGridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                            </dx:BootstrapGridViewCommandColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="AccessGroupID" ReadOnly="True" VisibleIndex="1" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="AccessGroupName" VisibleIndex="2">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="AccessGroupDesc" VisibleIndex="3">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewCheckColumn FieldName="IsActive" VisibleIndex="10">
                            </dx:BootstrapGridViewCheckColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" Visible="False" VisibleIndex="11">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" Visible="False" VisibleIndex="12">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UpdatedBy" Visible="False" VisibleIndex="13">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="UpdatedDate" Visible="False" VisibleIndex="14">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewComboBoxColumn FieldName="MainPageId" VisibleIndex="4">
                                <PropertiesComboBox DataSourceID="dsAccessModule" TextField="ModuleLink" ValueField="ModuleID">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                            <dx:BootstrapGridViewComboBoxColumn Caption="Port Location" FieldName="PortLoc" VisibleIndex="5">
                                <PropertiesComboBox DataSourceID="dsLocation" TextField="PortName" ValueField="Location">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UserManual" VisibleIndex="9">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewCheckColumn FieldName="IsEmailGroup" VisibleIndex="7">
                            </dx:BootstrapGridViewCheckColumn>
                            <dx:BootstrapGridViewComboBoxColumn FieldName="PermitIssuerID" VisibleIndex="6">
                                <PropertiesComboBox DataSourceID="dsPermitIssuer" TextField="PermitIssuer" ValueField="MSPermitIssuerID">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="EmailGroup" VisibleIndex="8">
                            </dx:BootstrapGridViewTextColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
