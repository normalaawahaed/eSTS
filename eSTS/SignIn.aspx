<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="eSTS.SignIn" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
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
<body >
<div>
    <div class="container">
        <div class="row">
            <!--Welcome note-->
            <div class="col-md-6">
                <br><br><br><br>
                <h1 style="color:white">Welcome to eSTS</h1>
                <p style="color:White">eSTS is an initiative by Johor Port Authority (JPA) to develop a comprehensive end-to-end online system 
                    for Ship To Ship license and operation. This new system allows applicants/ STS operator / STS agent to complete 
                    their application under one roof system with other government agencies collaboration. eSTS involves a process 
                    of  application, monitoring, and managing data and analysis continuously from end-to-end procedure (licensing and operation).</p>
                <p style="color:White">The innovation and introduction of the eSTS will be the turning point of the abolition of the existing ship to ship
                    license through Marine Management System (MMS)</p>
                
                <%--<a class="button button1" href="<%= ResolveUrl("assets/img/STSprocessFlow.html")%>"> STS Process Flow</a>--%>
               
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

                        <div class="form-group row">
                            <div class="col-lg-6 text-right">
                                <dx:BootstrapHyperLink ID="BootstrapHyperLink1" runat="server" NavigateUrl="https://www.lpjpcs.gov.my/LPJConsoleWebV2/Controllers/EReg/Registration/FormRegistration.aspx" Text="New Registration">
                                </dx:BootstrapHyperLink>
                            </div>
                            <div class="col-lg-6 text-left">
                                    <dx:BootstrapHyperLink ID="BootstrapHyperLink2" runat="server" NavigateUrl="https://www.lpjpcs.gov.my/LPJConsoleWebV2/Account/ForgotPassword/kt_login_forgot" Text="Forgot Password">
                                    </dx:BootstrapHyperLink>
                            </div>
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
</div>  

</body>
</html>
