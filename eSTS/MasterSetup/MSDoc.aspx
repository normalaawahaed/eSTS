<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="MSDoc.aspx.cs" Inherits="eSTS.MasterSetup.MSDoc" %>

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

                    <asp:EntityDataSource ID="dsMSDoc" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_SuppDoc" OrderBy="it.[ModuleID],it.[DocCode]" Where="it.[DocStatus]=1" EnableDelete="True" EnableInsert="True" EnableUpdate="True">
                    </asp:EntityDataSource>

                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsMSDoc" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" KeyFieldName="ModuleAttachID">
                          <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                        <Columns>
                           
                            <dx:BootstrapGridViewTextColumn FieldName="ModuleAttachID" Caption="" ReadOnly="True"   VisibleIndex="1">
                                   <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="lilNew" runat="server" OnInit="lilNew_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                         <dx:ASPxHyperLink ID="lilEdit" runat="server" OnInit="lilEdit_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                         
                                    </DataItemTemplate>
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="DocCode" VisibleIndex="2" Caption="Document Code">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="DocDesc" VisibleIndex="3" Caption="Document Description">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="ModuleID" Visible="False" VisibleIndex="4">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="MSDocTypeID" Visible="False" VisibleIndex="1" ReadOnly="True">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="DocType" Visible="False" VisibleIndex="6">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewComboBoxColumn FieldName="ModuleDesc" VisibleIndex="5" Caption="Module">
                                <PropertiesComboBox TextField="{0}-{1}" ValueField="{0}">
                                    <Items>
                                        <dx:BootstrapListEditItem Text="STS Operator License" Value="STSOL">
                                        </dx:BootstrapListEditItem>
                                        <dx:BootstrapListEditItem Text="STS FSU License" Value="STSVL">
                                        </dx:BootstrapListEditItem>
                                        <dx:BootstrapListEditItem Text="STS Receiver/Supplier Vessel" Value="STSO">
                                        </dx:BootstrapListEditItem>
                                    </Items>
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                            <dx:BootstrapGridViewCheckColumn FieldName="DocStatus" VisibleIndex="7">
                            </dx:BootstrapGridViewCheckColumn>

                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
