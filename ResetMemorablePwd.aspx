<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetMemorablePwd.aspx.cs" Inherits="SchoolApp.Web.ResetMemorablePwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <!-- Meta -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- Favicon -->
    <link href="img/fav.png" rel="shortcut icon" type="image/x-icon" />

    <!-- Title -->
    <title>Reset Memorable Word</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet" />

    <!-- Custom icon Fonts -->
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <!-- Main CSS -->
    <link href="css/theme.css" rel="stylesheet" />
    <link href="css/green.css" rel="stylesheet" id="style_theme" />
    <link href='<%= Page.ResolveClientUrl("~/css/toastr.min.css")%>' rel="stylesheet" />
      <link href="css//bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <link href="css/project.css" rel="stylesheet" />
    <link href="css/lableWidth.css" rel="stylesheet" />
    <!-- Toastr style -->
    <link href="css/plugins/toastr/toastr.min.css" rel="stylesheet" />

    <!-- Gritter -->
    <link href="css/js/plugins/gritter/jquery.gritter.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="../Content/style.css" rel="stylesheet" />
     <style>
        .form-control, .single-line {
            border: 1px solid #e5e6e7 !important;
        }
        .modal-dialog i{
            font-size:45px;
        }
         .modal-dialog p{
            color:#ed5565;
        }

        
    </style>
</head>

<!-- Body -->
<body>
    <!-- Pre Loader -->
    <div class="loading">
        <div class="loader"></div>
    </div>
    <!-- Scroll to Top -->
    <a id="scroll-up"><i class="fa fa-angle-up"></i></a>
    <!-- Top Bar  -->
    <div class="konnect-info">
        <div class="container-fluid">
            <div class="row">
                <!-- Top bar Left -->
                <div class="col-md-6 col-sm-8 hidden-xs">
                    <ul>
                        <li><i class="fa fa-paper-plane" aria-hidden="true"></i>abc@ourmail.com </li>
                        <li class="li-last"><i class="fa fa-volume-control-phone" aria-hidden="true"></i>(040) 123-4567</li>
                    </ul>
                </div>
                <!-- Top bar Right -->
                <div class="col-md-6 col-sm-4">
                    <ul class="konnect-float-right">
                        <li class="li-last hidden-xs hidden-sm">                          
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <!-- Main Navigation + LOGO Area -->
    <nav id="mainNav" class="navbar navbar-default navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <!-- Responsive Menu -->
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <img src="img/icons/menu.png" alt="menu" width="40">
                </button>
                <!-- Logo -->
                <a class="navbar-brand" href="index.aspx">
                    <img class="logo-change" src="<%= Page.ResolveClientUrl("~/img/logo-green.png")%>" alt="logo"></a>
            </div>
        </div>
        <!-- /.container-fluid -->
    </nav>

    <!-- Login page-->
    <section class="section-bottom-border login-signup">
        <div class="container">
            <div class="row">
                <div class="login-main template-form">
                    <div class="footer-widget">
                        <center><h3>
            Reset Memorable Word</h3></center>
                    </div>                   
                    <form id="form1" runat="server">
                        <%--<div class="loginColumns animated fadeInDown">--%>
                            <div class="row">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


                                <asp:UpdatePanel ID="MainUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="input-group m-b">
                                            <%--<span class="input-group-addon"><i class="fa fa-key"></i></span>--%>
                                            <asp:TextBox ID="txtMemorableAnswer" AutoCompleteType="Disabled" required="" placeholder="Enter Memorable Password" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtMemorableAnswer" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Memorable Answer Required</span></a>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ControlToValidate="txtMemorableAnswer" ValidationGroup="Submit" ID="RegularExpressionValidator1" runat="server" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Memorable Answer must be 5-8 characters long with Alphanumeric only.</span></a>" ValidationExpression="\b[a-zA-Z0-9]{5,8}\b"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="input-group m-b">
                                            <%--<span class="input-group-addon"><i class="fa fa-key"></i></span>--%>
                                            <asp:TextBox ID="txtConfirmMemorableAnswer" AutoCompleteType="Disabled" required="" placeholder="Enter Confirm Memorable Password" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtConfirmMemorableAnswer" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Memorable Password Required</span></a>"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="Submit" ControlToCompare="txtMemorableAnswer" ControlToValidate="txtConfirmMemorableAnswer" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Memorable Password Does not Match</span></a>" Display="Dynamic" />
                                        </div>
                                        <asp:Panel ID="pnlInvalid" class="form-group text-center " Visible="false" runat="server">
                                            <span class="label label-danger">Oops! Invalid Credential Entered</span>
                                        </asp:Panel>

                                        <div class="form-group ">
                                            <div class="col-sm-12 no-padding-left no-padding-right">
                                                <asp:Button ID="btnSubmit" class="btn btn-primary block full-width" ValidationGroup="Submit" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-md-6">
                                    <div id="modal-form" class="modal fade" aria-hidden="true">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                                    <center><i class="fa fa-user modal-icon"></i></center>
                                                    <center><h4 class="modal-title">Authenication Failed</h4></center>
                                                    <center><small class="font-bold">contact administrator to get your credential</small></center>
                                                </div>
                                                <div class="modal-body">
                                                    <p>invalid user name or password, please contact your administrator get more details.</p>

                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-white" data-dismiss="modal">Close</button>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        <%--</div>--%>
                    </form>

                </div>
            </div>
        </div>
    </section>

    <!--Main footer-->
    <section class="main-footer">
        <div class="container">
            <div class="row">
                <!--footer widget one-->
                <div class="col-md-4 col-sm-6">
                    <div class="footer-widget">
                        <img src="img/logo-green.png" alt="" class="img-responsive logo-change">
                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Culpa consectetur assumenda est unde animi. Repellat quibusdam et ad aut molestias, facere maxime expedita aperiam sint.</p>
                    </div>
                </div>
                <!--/ footer widget one-->

                <!--footer widget Two-->
                <div class="col-md-4 col-sm-6">
                    <div class="footer-widget address">
                        <h3>Contact</h3>
                        <p>
                            <i class="fa fa-address-card-o" aria-hidden="true"></i><span>#Koramangala, Banglore
                                <br>
                                Karnataka, INDIA</span>
                        </p>
                        <p><i class="fa fa-envelope-o" aria-hidden="true"></i><span>youremail@yourdomain.com</span></p>
                        <p>
                            <i class="fa fa-volume-control-phone" aria-hidden="true"></i><span>+88 (0) 101 0000 000
                                <br>
                                +88 (0) 202 0000 001</span>
                        </p>
                    </div>
                </div>
                <!--/ footer widget Two-->

                <!--footer widget Three-->
                <div class="col-md-4 col-sm-6">
                    <div class="footer-widget quicl-links">
                        <h3>Quick Links</h3>
                        <ul>
                            <li><i class="fa fa-angle-right"></i><a href="Default.aspx">Home</a></li>
                            <li><i class="fa fa-angle-right"></i><a href="GetProduct.aspx">Get Product</a></li>
                            <li><i class="fa fa-angle-right"></i><a href="Resources.aspx">Resources</a></li>
                            <li><i class="fa fa-angle-right"></i><a href="RequestForDemo.aspx">Request Demo</a></li>
                            <li><i class="fa fa-angle-right"></i><a href="faq.aspx">Faq</li>
                            <li><i class="fa fa-angle-right"></i><a href="AboutUs.aspx">About Us</a></li>
                            <li><i class="fa fa-angle-right"></i><a href="ContectUs.aspx">Contact Us</a></li>
                            <li><i class="fa fa-angle-right"></i><a href="login.aspx">Login</li>
                            <li><i class="fa fa-angle-right"></i><a href="Register.aspx">Register</a></li>
                        </ul>
                    </div>
                </div>
                <!--/ footer widget thre-->
            </div>
        </div>
    </section>
    <!--/Main Footer-->

    <!--copyright Footer-->
    <footer>
        <div class="container">
            <div class="row">
                <div class="col-md-6 col-sm-6 text-left">

                    <!--Footer Social Icons-->
                    <div class="contact-social">
                        <p>
                            <a href="#"><i class="fa fa-facebook"></i></a>
                            <a href="#"><i class="fa fa-twitter"></i></a>
                            <a href="#"><i class="fa fa-pinterest"></i></a>
                            <a href="#"><i class="fa fa-youtube"></i></a>
                        </p>
                    </div>
                </div>

                <!-- Footer Copy rights-->
                <div class="col-md-6 col-sm-6 text-right">
                    <p>&copy; Copyright 2017</p>
                </div>
            </div>
        </div>
    </footer>
    <!-- jQuery -->
    <script src='<%= Page.ResolveClientUrl("~/js/jquery.min.js")%>'></script>
    <!-- Bootstrap Core JavaScript -->
    <script src='<%= Page.ResolveClientUrl("~/js/bootstrap.min.js")%>'></script>
    <!-- Theme JavaScript -->

    <script src='<%= Page.ResolveClientUrl("~/js/default.js")%>'></script>
    <script src='<%= Page.ResolveClientUrl("~/js/toastr.min.js")%>'></script>
</body>

</html>
