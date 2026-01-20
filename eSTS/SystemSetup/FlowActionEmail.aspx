
<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="FlowActionEmail.aspx.cs" Inherits="eSTS.SystemSetup.FlowActionEmail" %>

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

                    <asp:EntityDataSource ID="dsFlowActionEmail" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="FlowActionEmails">
                    </asp:EntityDataSource>

                    <asp:EntityDataSource ID="dsFlowActionStatus" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="FlowActionStatus" OrderBy="it.[ActionStatusSeq]">
                    </asp:EntityDataSource>

                    <asp:EntityDataSource ID="dsEmailTemplate" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="EmailTemplates" OrderBy="it.[TempSubject]" Select="it.[TemplateCode], it.[TempSubject], it.[EmailTempID]">
                    </asp:EntityDataSource>

                    <asp:EntityDataSource ID="dsUserGroup1" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="AccessGroups" EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="it.[AccessGroupName]">
                    </asp:EntityDataSource>

                   
                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsFlowActionEmail" KeyFieldName="FlowActionEmailID" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating">
                          <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsCommandButton>
                            <EditButton IconCssClass="fa fa-edit text-success" Text=" " />
                            <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                        </SettingsCommandButton>
                        <SettingsDataSecurity AllowDelete="True" AllowInsert="True" AllowEdit="True" />
                        <Columns>
                            <dx:BootstrapGridViewCommandColumn ShowDeleteButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" ShowEditButton="True" ShowClearFilterButton="True">
                            </dx:BootstrapGridViewCommandColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="FlowActionEmailID" ReadOnly="True" Visible="False" VisibleIndex="1">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewComboBoxColumn Caption="Flow Action Status" FieldName="FlowActionStatusID" VisibleIndex="3">
                                <PropertiesComboBox DataSourceID="dsFlowActionStatus" TextField="ActionStatus" ValueField="FlowActionStatusID">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                            <dx:BootstrapGridViewComboBoxColumn Caption="Email Template" FieldName="EmailTempID" VisibleIndex="4">
                                <PropertiesComboBox DataSourceID="dsEmailTemplate" TextField="TempSubject" ValueField="EmailTempID" NullValueItemDisplayText="{1} - {2}" TextFormatString="{1} - {2}">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                            <dx:BootstrapGridViewComboBoxColumn Caption="Receipient Access Group" FieldName="ReceiptAGID" VisibleIndex="5">
                                <PropertiesComboBox DataSourceID="dsUserGroup1" TextField="AccessGroupDesc" ValueField="AccessGroupID">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                            <dx:BootstrapGridViewCheckColumn FieldName="IsApplicant" VisibleIndex="6">
                            </dx:BootstrapGridViewCheckColumn>
                            <dx:BootstrapGridViewCheckColumn FieldName="IsOperator" VisibleIndex="7">
                            </dx:BootstrapGridViewCheckColumn>
                           
                        </Columns>
                     
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
