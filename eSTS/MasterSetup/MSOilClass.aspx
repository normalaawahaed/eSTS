<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="MSOilClass.aspx.cs" Inherits="eSTS.MasterSetup.MSOilClass" %>

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

                    <asp:EntityDataSource ID="dsOilClass" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="MSOilClasses" OrderBy="it.[Class]" Where="it.[IsActive]=true" EnableDelete="True" EnableInsert="True" EnableUpdate="True">
                    </asp:EntityDataSource>

                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsOilClass" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" KeyFieldName="OilClassID">
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
                            <dx:BootstrapGridViewTextColumn FieldName="OilClassID" ReadOnly="True" VisibleIndex="1" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="OilClass" VisibleIndex="2">
                            </dx:BootstrapGridViewTextColumn>
                             <dx:BootstrapGridViewTextColumn FieldName="Class" VisibleIndex="2">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewCheckColumn FieldName="IsActive" VisibleIndex="4">
                            </dx:BootstrapGridViewCheckColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" Visible="False" VisibleIndex="5">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" Visible="False" VisibleIndex="6">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UpdatedBy" Visible="False" VisibleIndex="7">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="UpdatedDate" Visible="False" VisibleIndex="8">
                            </dx:BootstrapGridViewDateColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
