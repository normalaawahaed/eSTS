<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="insts.aspx.cs" Inherits="eSTS.insts" %>

<%@ Register assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.Bootstrap" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>



<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>eSTS | Login </title>


</head>
<style>
    body {
        background-image: url('assets/img/IMG_2311.jpg');
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-size: 100% 100%;
    }

    .row {
        display: flex;
        -ms-flex-wrap: wrap;
        flex-wrap: wrap;
        margin-right: -15px;
        margin-left: -15px;
     }

    .h1, h1 {
        font-size: 1.5rem;
        margin-top: 100px;    
    }

    p {
        margin-top: 20px;
        margin-bottom: 1rem;
        text-align: justify;
        font-size: medium;
        color: black;
    }

    img.ex1 {
        margin: left;    
		width: 100px;	
    }

    .button {
        color: white;
        padding: 15px 32px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin: 4px 2px;
        cursor: pointer;
    }

    .button1 {
        background-color: #000000;
    }

    .ibox-content {
        margin-top: 100px;
    }

</style>
<body class="skin-3">
<%--<form id="frmLogin" runat="server">
    <div class="loginColumns animated fadeInDown">
        

        <div class="row">
            <div class="col-sm-3 text-center">
            </div>
            <div class="col-md-6">
                <div class="ibox-content">
                    <div class="form-group">

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
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3 text-center">
            </div>
            <div class="col-md-6">
                <div class="ibox-content">
                    <div class="form-group">


                        <div class="form-group">
                            <input type="text" id="txtUserID" runat="server" class="form-control" placeholder="Username" required="">
                        </div>
                        <div class="form-group">
                            <input type="password" id="txtPassword" runat="server" class="form-control" placeholder="Password" required="">
                        </div>
                        <asp:Button ID="btnLogin" class="btn btn-primary btn-block btn-flat" runat="server" OnClick="btnLogin_Click" Text="Sign In" />
                    </div>
                </div>
            </div>
        </div>
    </div>
        <div class="row">
              <div class="col-sm-3 text-center">
            </div>
            <div class="col-md-6">
                <div class="ibox-content">
                    <div class="form-group">
                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
       <%-- <hr /> 
        <div class="row">
             <div class="col-sm-3 text-center">
            </div>
            <div class="col-md-6">
                 <hr />
           Copyright © 2020 Lembaga Pelabuhan Johor. All rights reserved.
            </div>
           
        </div>
       
</form>--%>
    <div class="container">
        <div class="row">
            <!--Welcome note-->
            <div class="col-md-6">
                <br><br><br><br>
                <h1 style="color:white">Welcome to eSTS</h1>
                <p style="color:White">eSTS is an initiative action by Johor Port Authority (JPA) to develop a comprehensive end-to-end online system 
                    for ship to ship license and operation. This new system allows applicants/ ship to ship operator / ship to ship agent to complete 
                    their application under one roof system with other government agencies collaboration. E STS involve a process 
                    of  application, monitoring, and managing data and analysis continuously from end-to-end procedure.</p>
                <p style="color:White">The innovation and introduction of the E STS will be the turning point of the abolition of the existing Ship To Ship
                    license through Marine Management System (MMS)</p>
                
                <a class="button button1" href="<%= ResolveUrl("assets/img/STSprocessFlow.html")%>"> STS Process Flow</a>
                <%--<a class="button button1" href="<%= ResolveUrl("assets/img/RTSprocessFlow.html")%>"> RT/Pipeline Process Flow</a>--%>
            </div>

        <!--Sign up box-->
            <div class="col-md-6">
                <div class="ibox-content">
                    <form class="m-t" role="form" id="frmLogin" runat="server">
                        <div class="form-group">
                            <div class="col-sm-12 text-center" style="padding-bottom: 10">
                                <h1><i class="fa fa-ship"></i> eSTS</h1>
                            </div>
                        </div>
                     <div class="form-group">
                            <div class="col-sm-12 text-center" style="padding-bottom: 10">
                                <dx:ASPxLabel ID="lblVersion" runat="server" Text=""></dx:ASPxLabel>
                            </div>
                        </div>
                        <div class="form-group">
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
                    
                        <div class="form-group row">
                            <div class="col-lg-12 text-center">
                                <asp:Button ID="btnLogin" class="btn btn-primary btn-block btn-flat" runat="server" OnClick="btnLogin_Click" Text="Sign In" />
                            </div>
                        </div>

                        <div class="form-group text-center">
                            <dx:BootstrapHyperLink ID="BootstrapHyperLink1" runat="server" NavigateUrl="https://lpjpcs.gov.my/LPJConsole/EREG/Registration/FormRegistration.aspx" Text="New Registration">
                            </dx:BootstrapHyperLink>
                        </div>
                    
                        <div class="form-group">
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    
                        <div class="form-group">
                            <div class="col-md-12">
                                Copyright © 2022 Lembaga Pelabuhan Johor. All rights reserved. 
                                <br>                                
                                <img class="ex1" src="assets/img/lpjlogo.png"/>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>

		<link href="<%= ResolveUrl("assets/css/bootstrap.min.css") %>" rel="stylesheet" />
        <link href="<%= ResolveUrl("assets/font-awesome/css/font-awesome.css") %>" rel="stylesheet" />
        <link href="<%= ResolveUrl("assets/css/animate.css") %>" rel="stylesheet" />
        <link href="<%= ResolveUrl("assets/css/style.css") %>" rel="stylesheet" />

    </div>
        <link href="<%= ResolveUrl("assets/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("assets/font-awesome/css/font-awesome.css") %>" rel="stylesheet"/>
    <link href="<%= ResolveUrl("assets/css/animate.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("assets/css/style.css") %>" rel="stylesheet" />
</body>
</html>
