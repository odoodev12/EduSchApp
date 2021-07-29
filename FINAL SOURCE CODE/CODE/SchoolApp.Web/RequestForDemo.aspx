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
        <%--<div class="container">
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
        </div>--%>
    <div class="container">
        <div class="form-group heading col-md-12">
            <h2 class="para-heading">submit details</h2>
        </div>
          <div class="form-row">
              <div class="form-group col-md-6">
                  <asp:Label ID="lblFirstName" runat="server" Text="FirstName">First Name</asp:Label>
                  <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="form-group col-md-6">
                  <asp:Label ID="lblLastName" runat="server" Text="LastName">Last Name</asp:Label>
                  <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="form-group col-md-6">
                  <asp:Label ID="lblRole" runat="server" Text="Role">Role</asp:Label>
                  <asp:DropDownList ID="drpRoles" required="" CssClass="form-control" runat="server" />
              </div>
              <div class="form-group col-md-6">
                  <asp:Label ID="lblSchoolName" runat="server" Text="SchoolName">School Name</asp:Label>
                  <asp:TextBox ID="txtSchoolName" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="form-group col-md-6">
                  <asp:Label ID="lblOfficialEmail" runat="server" Text="officialEmail">Official Email</asp:Label>
                  <asp:TextBox ID="txtOfficialEmail" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="form-group col-md-6">
                  <asp:Label ID="lblPhoneNumber" runat="server" Text="phoneNumber">Offical Phone Number</asp:Label>
                  <asp:TextBox ID="txtOfficialPhoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="form-group col-md-6">
                 <asp:Label ID="lblMobileNumber" runat="server" Text="phoneNumber">Mobile Number</asp:Label>
                  <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="btn col-md-6">
                  <br/>
                  <asp:Button ID="btnSubmit" runat="Server" Text="Submit"  OnClick="btnSubmit_Click" CssClass="btn btn btn-primary pull-center" />
              </div>

          </div>
              
        </div>
 
    </section>

    <!--/ Call To Action-->
        <script type="text/javascript">
            function showModal(message) {
                alert(message);
            }
        </script>
</asp:Content>

