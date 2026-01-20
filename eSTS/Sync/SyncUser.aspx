<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="SyncUser.aspx.cs" Inherits="eSTS.Sync.SyncUser" %>
<%@ Register assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.Bootstrap" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-content">
                    <asp:EntityDataSource ID="dsUsers" runat="server" ConnectionString="name=MMSSyncEntities" DefaultContainerName="MMSSyncEntities" EnableFlattening="False" EntitySetName="Users" OrderBy="it.[UserID]" EnableUpdate="True">
                    </asp:EntityDataSource>
                 <%--    <asp:EntityDataSource ID="dsEBAccessGroup" runat="server" ConnectionString="name=eBunkering_LiveEntities" DefaultContainerName="eBunkering_LiveEntities" EnableFlattening="False" EntitySetName="AccessGroups" OrderBy="it.[AccessGroupName]" Select="it.[AccessGroupName], it.[AccessGroupID]">
                    </asp:EntityDataSource>--%>
                    <asp:EntityDataSource ID="dsSTSAccessGroup" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="AccessGroups" OrderBy="it.[AccessGroupName]" Select="it.[AccessGroupID], it.[AccessGroupName]">
                    </asp:EntityDataSource>
                    <asp:EntityDataSource ID="dsCompany" runat="server" ConnectionString="name=MMSSyncEntities" DefaultContainerName="MMSSyncEntities" EnableFlattening="False" EntitySetName="CompanyProfiles" OrderBy="it.[CompanyName]" Select="it.[Orgzid], it.[CompanyName]">
                    </asp:EntityDataSource>
                    <dx:BootstrapButton ID="btnSync" runat="server" AutoPostBack="false" OnClick="btnSync_Click" Text="Sync">
                    </dx:BootstrapButton>
                    <dx:BootstrapGridView ID="gridUsers" runat="server" AutoGenerateColumns="False" DataSourceID="dsUsers" KeyFieldName="id" OnRowUpdating="gridUsers_RowUpdating">
                          <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                        <SettingsPager AlwaysShowPager="True">
                        </SettingsPager>
                        <SettingsDataSecurity AllowEdit="True" />
                          <Settings ShowFilterRow="True" />
                        <Columns>
                            <dx:BootstrapGridViewCommandColumn ShowEditButton="True" VisibleIndex="0">
                            </dx:BootstrapGridViewCommandColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UserID" ReadOnly="True" VisibleIndex="2">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="FullName" ReadOnly="True" VisibleIndex="3">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="EmailAddress" ReadOnly="True" VisibleIndex="4">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UserType" ReadOnly="True" VisibleIndex="7">
                            </dx:BootstrapGridViewTextColumn>
                           <%-- <dx:BootstrapGridViewComboBoxColumn Caption="EB Access Group" FieldName="AccessGroupID" VisibleIndex="5">
                                <PropertiesComboBox DataSourceID="dsEBAccessGroup" TextField="AccessGroupName" ValueField="AccessGroupID">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>--%>
                            <dx:BootstrapGridViewComboBoxColumn Caption="STS Access Group" FieldName="STSAccessGroupID" VisibleIndex="6">
                                <PropertiesComboBox DataSourceID="dsSTSAccessGroup" TextField="AccessGroupName" ValueField="AccessGroupID">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                            <dx:BootstrapGridViewComboBoxColumn FieldName="OrgzID" ReadOnly="True" VisibleIndex="1">
                                <PropertiesComboBox DataSourceID="dsCompany" NullValueItemDisplayText="{0} - {1}" TextField="CompanyName" TextFormatString="{0} - {1}" ValueField="Orgzid">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
