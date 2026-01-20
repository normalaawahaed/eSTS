<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="AnalysisReport.aspx.cs" Inherits="eSTS.Operation.AnalysisReport" %>

<%@ Register Assembly="DevExpress.XtraCharts.v18.2.Web, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:HiddenField ID="hfCompID" runat="server" ClientIDMode="Static" />
    <div class="row">
        <div class="col-lg-12">
            <dl class="row mb-0">
                <label class="col-sm-2 text-sm-right">
                    <b>Year </b>
                    <br />
                    <i>Tahun</i>  <font color="red">*</font>
                </label>
                <div class="col-sm-3 text-sm-left">
                    <dx:BootstrapComboBox ID="cbYear" ClientInstanceName="cbYear" EncodeHtml="False" runat="server" MaxLength="100" ValueType="System.Int32">
                    </dx:BootstrapComboBox>
                </div>
                <div class="col-sm-2" style="padding: 0">
                    <dx:BootstrapButton ID="btnSearch" CssClasses-Icon="fa fa-search" runat="server" Text="Search" OnClick="btnSearch_Click">
                        <SettingsBootstrap RenderOption="Warning" />
                        <CssClasses Icon="fa fa-search"></CssClasses>
                    </dx:BootstrapButton>
                </div>
            </dl>
        </div>
    </div>
     <div class="row">
        <div class="col-lg-12">
          <dx:BootstrapChart runat="server" DataSourceID="SqlDataSource2" TitleText="Volume Handling By Month" >
                <SettingsExport Enabled="true" Formats="JPEG, PNG, SVG, GIF, PDF" FileName="VolumeByOilType" />
                <SettingsSeriesTemplate NameField="Mth"/>
                <SettingsCommonSeries Type="Bar" ArgumentField="Mth" ShowInLegend="false"  ValueField="Total" >
                    <Label Visible="true"></Label>
                </SettingsCommonSeries>
              <ArgumentAxis ArgumentType="System.Int32" ValueMarginsEnabled="false" Type="Discrete" DiscreteAxisDivisionMode="CrossLabels" GridVisible="true" TickInterval="1">  
    
</ArgumentAxis>  
              <%--  <SettingsLegend VerticalAlignment="Bottom" HorizontalAlignment="Center" />--%>
            </dx:BootstrapChart>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:STSConnectionString %>" SelectCommand="spReport" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cbYear" Name="pCurrYear" PropertyName="Value" Type="Int32" />
                    <asp:Parameter Name="pChartType" Type="Int32" DefaultValue="3" />
                    <asp:ControlParameter ControlID="hfCompID" Name="pAgentID" PropertyName="Value" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:HiddenField ID="hfChartType2" runat="server" ClientIDMode="Static" Value="2" />
  
        </div>
    </div>
 
   <div class="row">
        <div class="col-lg-6"  style="">

            <dx:BootstrapChart runat="server" DataSourceID="SqlDataSource" TitleText="Volume Handling Based on Method" >
                  <SettingsExport Enabled="true" Formats="JPEG, PNG, SVG, GIF, PDF" FileName="VolumeByOilType" />
                <SettingsSeriesTemplate NameField="MedhodName" OnClientCustomizeSeries="customizeSeries" />
                <SettingsCommonSeries Type="Bar" ArgumentField="MedhodName" ValueField="Total">
                    <Label Visible="true"></Label>
                </SettingsCommonSeries>
                <SettingsLegend VerticalAlignment="Bottom" HorizontalAlignment="Center" />

            </dx:BootstrapChart>
         <%--   <dx:BootstrapChart ID="BootstrapChart1" runat="server" DataSourceID="SqlDataSource2" EncodeHtml="True" Palette="Office">
                <SeriesCollection>
                    <dx:BootstrapChartBarSeries ArgumentField="Mth" BarPadding="-1" TagField="MthDesc" ValueField="Total">
                        <Label>
                            <Format Type="Millions" />
                        </Label>
                    </dx:BootstrapChartBarSeries>
                </SeriesCollection>
            </dx:BootstrapChart>--%>
            <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:STSConnectionString %>" SelectCommand="spReport" SelectCommandType="StoredProcedure">
               <SelectParameters>
                    <asp:ControlParameter ControlID="cbYear" Name="pCurrYear" PropertyName="Value" Type="Int32" />
                    <asp:Parameter Name="pChartType" Type="Int32" DefaultValue="1" />
                    <asp:ControlParameter ControlID="hfCompID" Name="pAgentID" PropertyName="Value" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
           
            <asp:HiddenField ID="hfChartType1" runat="server" ClientIDMode="Static" Value="1" />
           
        </div>
        <div class="col-lg-6">
               <dx:BootstrapPieChart runat="server" DataSourceID="SqlDataSource3" Type="Doughnut" TitleText="Volume Handling Based on Oil Type" InnerRadius="0.3">
                <SettingsExport Enabled="true" Formats="JPEG, PNG, SVG, GIF, PDF" FileName="VolumeByOilType" />
                <SeriesCollection>
                    <dx:BootstrapPieChartSeries ArgumentField="OilTypeCode" ValueField="Total">
                        <Label Visible="true">
                            <Format Type="Decimal"  />
                        </Label>
                    </dx:BootstrapPieChartSeries>
                </SeriesCollection>
            </dx:BootstrapPieChart>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:STSConnectionString %>" SelectCommand="spReport" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cbYear" Name="pCurrYear" PropertyName="Value" Type="Int32" />
                    <asp:Parameter Name="pChartType" Type="Int32" DefaultValue="2" />
                    <asp:ControlParameter ControlID="hfCompID" Name="pAgentID" PropertyName="Value" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
             
        </div>
    </div>
   
     <div class="row">
        <div class="col-lg-12">
              </div>
    </div>
    <script>
        function customizeSeries(valueFromNameField) {
            return valueFromNameField === 2009 ? { type: "line", label: { visible: true }, color: "#ff3f7a" } : {};
        }</script>

</asp:Content>
