<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="SchoolApp.Web.School.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .class-box {
            padding: 20px;
            background: #fff;
            margin-bottom: 100px;
        }

            .class-box p {
                margin-top: 0px;
                margin-bottom: 0px;
            }

            .class-box h3 {
                margin-bottom: 2px;
                margin-top: 0px;
            }

        .feature {
            border: solid 1px #cccccc;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading">
                            <asp:HyperLink ID="hlinkSupportLog" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> School</asp:HyperLink>
                            / <span>Report</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container" id="features">
                    <div class="sidebar-section">
                        <div class="container">
                            <div class="row">

                                <div class="class-box feature">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Select Report Type</label>
                                                <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Teacher" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Student" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="PickUp" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Attendance" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Admin" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlReportType" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Select Report</label>
                                                <asp:DropDownList ID="ddlReport" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlReport" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>
    </section>
</asp:Content>
