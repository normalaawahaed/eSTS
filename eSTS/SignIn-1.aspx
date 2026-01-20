<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="eBunkering.SignIn" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>EBunkering | Login </title>
    
   
</head>

<body class="gray-bg">
      <form id="frmLogin" runat="server">
    <div class="loginColumns animated fadeInDown">
        <div class="row">
            <div class="col-sm-3 text-center">
                
                </div>
            <div class="col-sm-6 text-center">
                <div class="row">
                    <div class="col-sm-2 text-right" style="padding-right:0">
                       <%--  <img src="<%= ResolveUrl("~/assets/img/tank_truck.png")%>" alt="" />--%>
                    </div>
                    <div class="col-sm-10 text-left" style="padding-bottom:10" >
                        <h1 ><i class="fa fa-ship"></i> eBunkering</h1>
                    </div>
                </div>
           

            </div>
            </div>
            <div class="row">
               
            <div class="col-sm-3 text-center">
                
                </div>
                <div class="col-md-7">
                    <div class="ibox-content">
                        <div class="form-group row">
                               <div class="col-lg-10">
                                   <dx:BootstrapRadioButtonList ID="rbUserType" runat="server" RepeatDirection="Horizontal">
                                       <Items>
                                           <dx:BootstrapListEditItem Text="Non-Goverment" Value="NGOV">
                                           </dx:BootstrapListEditItem>
                                           <dx:BootstrapListEditItem Text="Government" Value="GOV">
                                           </dx:BootstrapListEditItem>
                                       </Items>
                                   </dx:BootstrapRadioButtonList>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-5 col-form-label">ROC/ROB/Org.No.</label>
                            <div class="col-lg-7">
                                <input type="text" id="txtOrgzID" runat="server" class="form-control" placeholder="ROC/ROB/Org. No." required="">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-5 col-form-label">User ID</label>
                            <div class="col-lg-7">
                                <input type="text" id="txtUserID" runat="server" class="form-control" placeholder="User ID" required="">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-5 col-form-label">Password</label>
                            <div class="col-lg-7">
                                <input type="password" id="txtPassword" runat="server" class="form-control" placeholder="Password" required="">
                            </div>
                        </div>
                        <asp:Button ID="btnLogin" class="btn btn-primary btn-block btn-flat" runat="server" OnClick="btnLogin_Click" Text="Sign In" />

                    </div>
                    <div class="col-md-2 text-center">
                    </div>
                </div>
           
            </div>
        <div class="row">
             <div class="col-sm-3 text-center">
                
             </div>
                <div class="col-md-7">
                    <div class="ibox-content">
                         <div class="form-group text-center">
                              <dx:BootstrapHyperLink ID="BootstrapHyperLink1" runat="server" NavigateUrl="https://lpjpcs.gov.my/LPJConsole/EREG/Registration/FormRegistration.aspx" Text="New Registration">
                 </dx:BootstrapHyperLink>
                              </div>
                        <div class="form-group">
               <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
            </div>
       <%-- <hr />--%>
        <div class="row">
             <div class="col-md-3 text-right">
              <%--  <small>© </small>--%>
            </div>
            <div class="col-md-6">
                 <hr />
           Copyright © 2020 Lembaga Pelabuhan Johor. All rights reserved.
            </div>
            <div class="col-md-3 text-right">
              <%--  <small>© </small>--%>
            </div>
        </div>
          </div>
 <link href="<%= ResolveUrl("assets/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("assets/font-awesome/css/font-awesome.css") %>" rel="stylesheet"/>
    <link href="<%= ResolveUrl("assets/css/animate.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("assets/css/style.css") %>" rel="stylesheet" />
</form>
</body>
</html>
