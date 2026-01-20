<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MSOilType.aspx.cs" Inherits="eSTS.MasterSetup.MSOilType" MasterPageFile="~/SiteInspinia.Master" %>

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
                    <asp:EntityDataSource ID="dsOilType" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="MSOilTypes">
                    </asp:EntityDataSource>
                    <asp:EntityDataSource ID="dsOilClass" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="MSOilClasses">
                    </asp:EntityDataSource>
                    <asp:EntityDataSource ID="dsOilCat" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="MSOilCategories">
                    </asp:EntityDataSource>
                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsOilType" KeyFieldName="OilTypeID" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating">
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
                            <dx:BootstrapGridViewTextColumn FieldName="OilTypeID" ReadOnly="True" VisibleIndex="1" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="OilTypeCode" VisibleIndex="2">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="OilTypeDesc" VisibleIndex="3">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewCheckColumn FieldName="IsActive" VisibleIndex="6">
                            </dx:BootstrapGridViewCheckColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" VisibleIndex="7" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" Visible="False" VisibleIndex="8">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UpdatedBy" Visible="False" VisibleIndex="9">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="UpdatedDate" Visible="False" VisibleIndex="10">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewComboBoxColumn Caption="Oil Class" FieldName="OilClassID" VisibleIndex="4">
                                <PropertiesComboBox DataSourceID="dsOilClass" TextField="OilClass" ValueField="OilClassID" ValueType="System.Guid">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                            <dx:BootstrapGridViewComboBoxColumn Caption="Oil Category" FieldName="OilCategoryID" VisibleIndex="5">
                                <PropertiesComboBox DataSourceID="dsOilCat" TextField="OilCategory" ValueField="OilCategoryID">
                                </PropertiesComboBox>
                            </dx:BootstrapGridViewComboBoxColumn>
                        </Columns>
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

