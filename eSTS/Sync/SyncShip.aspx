<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="SyncShip.aspx.cs" Inherits="eSTS.Sync.SyncShip" %>
<%@ Register assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.Bootstrap" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-content">
                    <asp:EntityDataSource ID="dsShip" runat="server" ConnectionString="name=MMSSyncEntities" DefaultContainerName="MMSSyncEntities" EnableFlattening="False" EntitySetName="ShipMasters" OrderBy="it.[ShipName]" EnableUpdate="True" EnableInsert="True">
                    </asp:EntityDataSource>
                    <dx:BootstrapButton ID="btnSync" runat="server" AutoPostBack="false" OnClick="btnSync_Click" Text="Sync">
                    </dx:BootstrapButton>
                    <dx:BootstrapGridView ID="gridShip" runat="server" AutoGenerateColumns="False">
                          <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                        <SettingsPager AlwaysShowPager="True">
                        </SettingsPager>
                        <SettingsDataSecurity AllowEdit="True" />
                        <Columns>
                            <dx:BootstrapGridViewCommandColumn ShowEditButton="True" VisibleIndex="0">
                            </dx:BootstrapGridViewCommandColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="ShipID" ReadOnly="True" VisibleIndex="1" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="ShipCategory" ReadOnly="True" VisibleIndex="2" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="OffNo" VisibleIndex="4">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="ShipName" VisibleIndex="3">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="PortReg" VisibleIndex="5">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CallSign" VisibleIndex="6">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="IMONo" VisibleIndex="7">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LOA" VisibleIndex="12">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="GRT" VisibleIndex="15">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="NRT" VisibleIndex="16">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="DWT" VisibleIndex="17">
                            </dx:BootstrapGridViewTextColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
