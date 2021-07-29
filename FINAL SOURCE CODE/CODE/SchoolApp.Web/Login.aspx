<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SchoolApp.Web.Login" %>
<!DOCTYPE html>
<html lang="en">
<head>
<!-- Meta -->
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<!-- Favicon -->
<link href="img/fav.png" rel="shortcut icon" type="image/x-icon"/>

<!-- Title -->
<title>OLA</title>

<!-- Bootstrap Core CSS -->
<link href="css/bootstrap.css" rel="stylesheet">

<!-- Custom icon Fonts -->
<link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">

<!-- Main CSS -->
<link href="css/theme.css" rel="stylesheet">
<link href="css/green.css" rel="stylesheet" id="style_theme">
   
</head>

<!-- Body -->
<body>
<!-- Pre Loader -->
<div class="loading">
  <div class="loader"></div>
</div>
<!-- Scroll to Top --> 
<a id="scroll-up" ><i class="fa fa-angle-up"></i></a> 
<!-- Top Bar  -->
<div class="konnect-info">
  <div class="container-fluid">
    <div class="row"> 
      <!-- Top bar Left -->
      <div class="col-md-6 col-sm-8 hidden-xs">
        <ul>
          <li><i class="fa fa-paper-plane" aria-hidden="true"></i> abc@ourmail.com </li>
          <li class="li-last"> <i class="fa fa-volume-control-phone" aria-hidden="true"></i> (040) 123-4567</li>
        </ul>
      </div>
      <!-- Top bar Right -->
      <div class="col-md-6 col-sm-4">
        <ul class="konnect-float-right"><li class="li-last hidden-xs hidden-sm">
				<a target="_blank" href="#"><i class="fa fa-facebook" aria-hidden="true"></i></a>
				<a target="_blank" href="#"><i class="fa fa-twitter" aria-hidden="true"></i></a>
				<a target="_blank" href="#"><i class="fa fa-pinterest" aria-hidden="true"></i></a>
				<a target="_blank" href="#"><i class="fa fa-youtube" aria-hidden="true"></i></a>
		  </li>
          <li class="dropdown_category"><a href="#" class="customer_login_btn dropdown-toggle" data-toggle="dropdown"><i class="fa fa-user-o" ></i> Login <i class="caret"></i> </a>
		   <ul class="dropdown-menu">
            <li><a href="login.aspx?OP=School">School Login</a></li>
            <li><a href="login.aspx?OP=Teacher">Teacher Login</a></li>
            <li><a href="login.aspx?OP=Parent">Parent Login</a></li>			
          </ul>
		  </li>
          
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
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span> <img src="img/icons/menu.png" alt="menu" width="40"> </button>
      <!-- Logo --> 
      <a class="navbar-brand" href="index.aspx"><img class="logo-change" src="<%= Page.ResolveClientUrl("~/img/logo-green.png")%>" alt="logo"></a> </div>
    
    <!-- Menu Items -->
    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
      <ul class="nav navbar-nav navbar-right">
        <li><a href="default.aspx">Home</a></li>
        <li><a href="GetProduct.aspx">Get Product</a></li>
			<li><a href="Resources.aspx">Resources</a></li>
        <li><a href="RequestForDemo.aspx">Request Demo</a></li>
        <li><a href="faq.aspx">Faq</a></li>
		 <li><a href="AboutUs.aspx">About Us</a></li>
        <li><a href="ContectUs.aspx">Contact Us</a></li>
      </ul>
    </div>
    <!-- /.navbar-collapse --> 
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
            <asp:Label ID="lblLogin" runat="server" Text="Login"></asp:Label> Login</h3></center></div>
        <form runat="server">
          <div class="form-group">
            <label for="inputUsernameEmail">Username or email</label>
            <asp:TextBox  class="form-control" id="txtUserName" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
          </div>
          <div class="form-group"><asp:LinkButton ID="lnkForgot" CssClass="pull-right" runat="server" OnClick="lnkForgot_Click">Forgot password?</asp:LinkButton> <%--<a class="pull-right" href="#"></a>--%>
            <label for="inputPassword">Password</label>
               <asp:TextBox  class="form-control" TextMode="Password" id="txtPassword" runat="server" autocomplete="off"></asp:TextBox>
            </div>
          <div class="checkbox pull-right">
            <label>
              <input type="checkbox">
              Remember me </label>
          </div>
          <asp:Button runat="server" ID="btnLogin" Text="Log In" OnClick="btnLogin_Click" class="btn btn btn-primary"> </asp:Button>
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
        <div class="footer-widget"> <img src="img/logo-green.png" alt="" class="img-responsive logo-change">
          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Culpa consectetur assumenda est unde animi. Repellat quibusdam et ad aut molestias, facere maxime expedita aperiam sint.</p>
         </div>
      </div>
      <!--/ footer widget one--> 
      
      <!--footer widget Two-->
      <div class="col-md-4 col-sm-6">
        <div class="footer-widget address">
          <h3>Contact</h3>
          <p><i class="fa fa-address-card-o" aria-hidden="true"></i> <span>#Koramangala, Banglore <br>
            Karnataka, INDIA</span></p>
          <p><i class="fa fa-envelope-o" aria-hidden="true"></i> <span>youremail@yourdomain.com</span></p>
          <p><i class="fa fa-volume-control-phone" aria-hidden="true"></i> <span>+88 (0) 101 0000 000 <br>
            +88 (0) 202 0000 001</span></p>
        </div>
      </div>
      <!--/ footer widget Two--> 
      
      <!--footer widget Three-->
      <div class="col-md-4 col-sm-6">
        <div class="footer-widget quicl-links">
          <h3>Quick Links</h3>
         <ul>
            <li><i class="fa fa-angle-right"></i> <a href="Default.aspx">Home</a></li>
            <li><i class="fa fa-angle-right"></i> <a href="GetProduct.aspx">Get Product</a></li>
            <li><i class="fa fa-angle-right"></i> <a href="Resources.aspx">Resources</a></li>
            <li><i class="fa fa-angle-right"></i> <a href="RequestForDemo.aspx">Request Demo</a></li>
            <li><i class="fa fa-angle-right"></i> <a href="faq.aspx">Faq</li>
			<li><i class="fa fa-angle-right"></i> <a href="AboutUs.aspx">About Us</a></li>
            <li><i class="fa fa-angle-right"></i> <a href="ContectUs.aspx">Contact Us</a></li>
			<li><i class="fa fa-angle-right"></i> <a href="login.aspx">Login</li>
            <li><i class="fa fa-angle-right"></i> <a href="Register.aspx">Register</a></li>
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
		  <a href="#"> <i class="fa fa-facebook"></i> </a>
		  <a href="#"> <i class="fa fa-twitter"></i> </a>
		  <a href="#"> <i class="fa fa-pinterest"></i> </a> 
		  <a href="#"> <i class="fa fa-youtube"></i> </a> 
		  </p>
        </div>
      </div>
      
      <!-- Footer Copy rights-->
      <div class="col-md-6 col-sm-6 text-right">
        <p> &copy; Copyright 2017</p>
      </div>
    </div>
  </div>
</footer>
<!-- jQuery --> 
<script src="js/jquery.min.js"></script> 

<!-- Bootstrap Core JavaScript --> 
<script src="js/bootstrap.min.js"></script> 

<!-- Theme JavaScript --> 
<script src="js/default.js"></script>
</body>

</html>