<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ContectUs.aspx.cs" Inherits="SchoolApp.Web.ContectUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <link href="img/fav.png" rel="shortcut icon" type="image/x-icon" />

    <!-- Title -->
    <title>OLA</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet">

    <!-- Custom icon Fonts -->
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- Main CSS -->
    <link href="css/theme.css" rel="stylesheet">
    <link href="css/green.css" rel="stylesheet" id="style_theme">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div class="container ">
            <div class="row">
                <!-- Contact Form-->
                <div class="col-sm-6 template-form">
                    <h2 class="contact-heading">Get in <span>Touch with us</span></h2>
                    <div class="template-space"></div>

                        <asp:TextBox ID="txtYourName" runat="server" CssClass="form-control" placeholder="Your Name"></asp:TextBox>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Your Phone number"></asp:TextBox>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Your E-Mail"></asp:TextBox>
                        <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Comment here..."></asp:TextBox>
                        <asp:Button ID="btnsubmit" runat="server" CssClass="service-box-button" Text="SUBMIT" />
                </div>
                <!--Google Map-->
                <div class="col-sm-6 small-map">
                    <div id="map"></div>
                    <div id="cont" style="display: none">
                        <img class="logo-change" src="img/logo-green.png" width='120' alt="logo">
                        <p style='color: #333; font-size: 16px; font-weight: 400;'>
                            Koramangala, Banglore<br>
                            Karnataka, INDIA
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script src="js/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Theme JavaScript -->
    <script src="js/default.js"></script>
    <script>
        function initMap() {
            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 13,
                center: { lat: 51.286894, lng: -0.752615 },
                scrollwheel: false,
                getMap: true
            });
            var infowindow = new google.maps.InfoWindow({
                content: document.getElementById("cont").innerHTML
            });
            var marker = new google.maps.Marker({
                position: map.getCenter(),
                icon: "img/map-icon.png",
                map: map
            });
            infowindow.open(map, marker);
        }
    </script>
    <script async defer
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD5u8QmAP6DxLCQrbI5QnH9Y4n6xLBV2V0&amp;callback=initMap">
    </script>

</asp:Content>
