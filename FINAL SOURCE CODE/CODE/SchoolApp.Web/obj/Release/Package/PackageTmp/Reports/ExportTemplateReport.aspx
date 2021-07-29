<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExportTemplateReport.aspx.cs" Inherits="SchoolApp.Web.Reports.ExportTemplateReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .class-box {
            padding: 20px;
            background: #fff;
            margin-bottom: 20px;
        }

            .class-box p {
                margin-top: 0px;
                margin-bottom: 0px;
            }

            .class-box h3 {
                margin-bottom: 2px;
                margin-top: 0px;
            }

        .feature,
        .feature h3,
        .feature img,
        .feature .title_border {
            -webkit-transition: all .5s ease-in-out;
            -moz-transition: all .5s ease-in-out;
            -o-transition: all .5s ease-in-out;
            transition: all .5s ease-in-out;
        }

        .feature {
            border: solid 1px #cccccc;
        }

            .feature:hover {
                background: #F5F5F5;
                -webkit-transform: translate(0, .5em);
                -moz-transform: translate(0, .5em);
                -o-transform: translate(0, .5em);
                -ms-transform: translate(0, .5em);
                transform: translate(0, .5em);
            }

        .modal-body {
            padding: 15px;
        }

        .view_detail a {
            font-size: 10px;
        }

        .color_blue {
            color: #00a7ff;
        }

        .color_red {
            color: #FF0000;
        }

        .responsives {
            width: 100px !important;
            max-height: 100px !important;
            max-width: 100px !important;
            height: 100px !important;
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
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/School/Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            /
                            <asp:HyperLink ID="hlinkPickupReport" runat="server" NavigateUrl="~/School/Report.aspx" Text="Report"></asp:HyperLink>
                            / <span>Export Template Report</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features">
            <div class="sidebar-section">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-3"></div>
                        <div class="col-lg-6">
                            <div class="float-e-margins">
                                <div class="form-horizontal formelement">
                                    <div class="form-group">
                                        <label class="col-sm-3" runat="server" for="">Export Type:</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="drpExport" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="--Select Export Type" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Class" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Teacher" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="NonTeacher" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Student" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="All" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpExport" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="text-align: center; margin-bottom: 15px;">
                        <asp:Button runat="server" Text="Export" class="btn btn-primary" ID="btnExport" OnClick="btnExport_Click" ValidationGroup="Submit"></asp:Button>
                        &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
</asp:Content>
