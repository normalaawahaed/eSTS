<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="SyncCompanyProfile.aspx.cs" Inherits="eSTS.Sync.SyncCompanyProfile" %>
<%@ Register assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.Bootstrap" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:BootstrapButton ID="btnSync" runat="server" AutoPostBack="false" OnClick="btnSync_Click" Text="Sync">
</dx:BootstrapButton>
    <asp:EntityDataSource ID="dsCompanyProfile" runat="server" ConnectionString="name=MMSSyncEntities" DefaultContainerName="MMSSyncEntities" EnableFlattening="False" EnableUpdate="True" EntitySetName="CompanyProfiles" orderby="it.[CompanyName]">
    </asp:EntityDataSource>
    <dx:BootstrapGridView ID="gridCompanyProfile" runat="server" AutoGenerateColumns="False" DataSourceID="dsCompanyProfile" KeyFieldName="Orgzid">
        <SettingsAdaptivity AdaptivityMode="HideDataCells">
        </SettingsAdaptivity>
        <SettingsPager PageSize="20">
        </SettingsPager>
        <Columns>
            <dx:BootstrapGridViewTextColumn FieldName="Orgzid" VisibleIndex="0" ReadOnly="True">
            </dx:BootstrapGridViewTextColumn>
           <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="1">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="Address1" VisibleIndex="2">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="Address2" VisibleIndex="3">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="Address3" VisibleIndex="4">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="TelNo" VisibleIndex="5">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="FaxNo" VisibleIndex="6">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="ContactPerson" VisibleIndex="7">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="EmailAddress" VisibleIndex="8">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="OrgzType" VisibleIndex="9">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewCheckColumn FieldName="IsLock" VisibleIndex="10">
            </dx:BootstrapGridViewCheckColumn>
            <dx:BootstrapGridViewCheckColumn FieldName="IsBlacklist" VisibleIndex="11">
            </dx:BootstrapGridViewCheckColumn>
            <dx:BootstrapGridViewDateColumn FieldName="SyncDate" VisibleIndex="12">
            </dx:BootstrapGridViewDateColumn>
        </Columns>
        <SettingsSearchPanel Visible="True" />
    </dx:BootstrapGridView>
    <br />
</asp:Content>
