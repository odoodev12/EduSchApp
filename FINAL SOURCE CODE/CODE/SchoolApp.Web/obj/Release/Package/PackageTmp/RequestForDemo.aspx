<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="RequestForDemo.aspx.cs" Inherits="SchoolApp.Web.RequestForDemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading"><a href="default.aspx">Home</a> / <span>Request For Demo</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <!-- FAQ -->
    <section class="section-bottom-border login-signup">
        <div class="container">
            <div class="row">
                <div class="login template-form">
                    <div class="col-md-12">
                        <h2 class="para-heading">Send Request</h2>
                    </div>
                    <div class="col-md-6">

                        <div class="form-group">
                            <asp:Label ID="lblFullName" runat="server" Text="UserName">Full Name</asp:Label>
                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblschoolName" runat="server" Text="SchoolName">School Name</asp:Label>
                            <asp:TextBox ID="textSchoolName" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEmail" runat="server" Text="EmailID">Email ID</asp:Label>
                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblDate" runat="server" Text="Date">Date</asp:Label>
                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblTime" runat="server" Text="Time">Time</asp:Label>
                            <asp:TextBox ID="txtTime" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblinformation" runat="server" Text="Information">Information</asp:Label>
                            <asp:TextBox ID="txtInformation" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                        </div>
                        <asp:Button ID="btnSubmit" runat="Server" Text="SUBMIT" CssClass="btn btn btn-primary pull-right" />
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--/ Call To Action-->
</asp:Content>
