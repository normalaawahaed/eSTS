<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetAD.aspx.cs" Inherits="eSTS.ResetAD" %>

<%@ Register assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.Bootstrap" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title> RESET AD </title>
  
      <link href="assets/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="assets/css/animate.css" rel="stylesheet">
    <link href="assets/css/style.css" rel="stylesheet">
    <script>
        function successAlert() {
            var elem = document.getElementById('success_alert');
            elem.style.display = 'block';
            //$('#success_alert').delay(800).fadeOut('slow');
        }
        function errorAlert() {
            console.log("masuk");
            var elem = document.getElementById('error_alert');
            elem.style.display = 'block';
            // $('#error_alert').delay(800).fadeOut('slow');
        }
    </script>
</head>
<body class="gray-bg">
     <div class="alert alert-success" id="success_alert" style="display: none">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            Password reset successfully.
        </div>
        <div class="alert alert-danger" id="error_alert" style="display: none">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            <dx:ASPxLabel ID="lblErrMsg" runat="server" Text="" CssClass="description" EnableViewState="False">
            </dx:ASPxLabel>
        </div>


    <div class="passwordBox animated fadeInDown">
        <div class="row">
             <div class="col-md-12">
                <div class="ibox-content">

                    <h2 class="font-bold">RESET AD PASSWORD</h2>

                    <p>
                     
                    </p>

                    <div class="row">

                        <div class="col-lg-12">
                            <form class="m-t" role="form" id="frmResetAD" runat="server">
                                <div>
                                    <div class="form-group row">
                                        <label class="col-sm-5 col-form-label">UserID</label>
                                        <div class="col-lg-7">
                                            <dx:BootstrapComboBox ID="cbUID" runat="server" required=""></dx:BootstrapComboBox>
                                         
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-5 col-form-label">New Password</label>
                                        <div class="col-lg-7">
                                            <input type="password" id="txtPwd" runat="server" class="form-control" placeholder="Password" required="">
                                        </div>
                                    </div>
                                    <dx:BootstrapButton ID="btnReset" runat="server" AutoPostBack="false" Text="Reset" OnClick="btnReset_Click">
                                    </dx:BootstrapButton>
                                </div>
                            </form>
                            </div>
                        </div>
                    </div>
                 </div>
            
        </div>
    </div>
</body>
</html>
