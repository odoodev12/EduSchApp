<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SchoolApp.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .panel-success > .panel-heading {
            background-color: #1c84c6 !important;
            border-color: #1c84c6;
            color: #ffffff;
        }

        .panel-info > .panel-heading {
            background-color: #23c6c8 !important;
            border-color: #23c6c8;
            color: #ffffff;
        }

        .panel-danger > .panel-heading {
            background-color: #ed5565 !important;
            border-color: #ed5565;
            color: #ffffff;
        }

        .panel-warning > .panel-heading {
            background-color: #f8ac59 !important;
            border-color: #f8ac59;
            color: #ffffff;
        }

        .panel-primary > .panel-heading {
            background-color: #18a689 !important;
            border-color: #18a689;
            color: #ffffff;
        }

        .panel-default > .panel-heading {
            background-color: #f5f5f5 !important;
            border-color: #ddd;
            color: #333;
        }

        .big-icon1 {
            font-size: 40px;
            color: #676a6c;
        }

        .headers {
            font-size: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Counter" runat="server">
        <a href="SchoolList.aspx" style="color:#676a6c !important;">
            <div class="col-md-3" id="Schools" runat="server">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h3 class="panel-title">Total Schools</h3>
                    </div>
                    <div class="panel-body">
                        <i class="fa fa-university big-icon1" aria-hidden="true"></i>
                        <div class="pull-right">
                            <h3 class="headers">
                                <asp:Label runat="server" ID="lblSchool" Text="0"></asp:Label></h3>
                        </div>
                    </div>
                </div>
            </div>
        </a>
        <a href="OrganisationUserList.aspx" style="color:#676a6c !important;">
            <div class="col-md-3" id="Organisations" runat="server">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h3 class="panel-title">Total Organisation Users</h3>
                    </div>
                    <div class="panel-body">
                        <i class="fa fa-users big-icon1" aria-hidden="true"></i>
                        <div class="pull-right">
                            <h3 class="headers">
                                <asp:Label runat="server" ID="lblOrgUsers" Text="0"></asp:Label></h3>
                        </div>
                    </div>
                </div>
            </div>
        </a>
        <div class="col-md-3" id="Classes" runat="server">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Total Classes</h3>
                </div>
                <div class="panel-body">
                    <i class="fa fa-indent big-icon1" aria-hidden="true"></i>
                    <div class="pull-right">
                        <h3 class="headers">
                            <asp:Label runat="server" ID="lblClass" Text="0"></asp:Label></h3>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-3" id="Teachers" runat="server">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">Total Teachers</h3>
                </div>
                <div class="panel-body">
                    <i class="fa fa-users big-icon1" aria-hidden="true"></i>
                    <div class="pull-right">
                        <h3 class="headers">
                            <asp:Label runat="server" ID="lblTeachers" Text="0"></asp:Label></h3>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3" id="Students" runat="server">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">Total Students</h3>
                </div>
                <div class="panel-body">
                    <i class="fa fa-users big-icon1" aria-hidden="true"></i>
                    <div class="pull-right">
                        <h3 class="headers">
                            <asp:Label runat="server" ID="lblStudents" Text="0"></asp:Label></h3>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3" id="Pickers" runat="server">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Total Pickers</h3>
                </div>
                <div class="panel-body">
                    <i class="fa fa-users big-icon1" aria-hidden="true"></i>
                    <div class="pull-right">
                        <h3 class="headers">
                            <asp:Label runat="server" ID="lblPickers" Text="0"></asp:Label></h3>
                    </div>
                </div>
            </div>
        </div>
        <a href="ClientSupportList.aspx" style="color:#676a6c !important;">
            <div class="col-md-3" id="Support" runat="server">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h3 class="panel-title">Total Outstanding Support</h3>
                    </div>
                    <div class="panel-body">
                        <i class="fa fa-ticket big-icon1" aria-hidden="true"></i>
                        <div class="pull-right">
                            <h3 class="headers">
                                <asp:Label runat="server" ID="lblSupport" Text="0"></asp:Label></h3>
                        </div>
                    </div>
                </div>
            </div>
        </a>
        <a href="ClientSupportList.aspx" style="color:#676a6c !important;">
            <div class="col-md-3" id="UnresolvedSupport" runat="server">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h3 class="panel-title">Total UnResolved Support</h3>
                    </div>
                    <div class="panel-body">
                        <i class="fa fa-ticket big-icon1" aria-hidden="true"></i>
                        <div class="pull-right">
                            <h3 class="headers">
                                <asp:Label runat="server" ID="lblUnResolvedSupport" Text="0"></asp:Label></h3>
                        </div>
                    </div>
                </div>
            </div>
        </a>
        <a href="SchoolInvoiceList.aspx" style="color:#676a6c !important;">
            <div class="col-md-3" id="Invoice" runat="server">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h3 class="panel-title">Total Unpaid Invoice</h3>
                    </div>
                    <div class="panel-body">
                        <i class="fa fa-money big-icon1" aria-hidden="true"></i>
                        <div class="pull-right">
                            <h3 class="headers">
                                <asp:Label runat="server" ID="lblInvoice" Text="0"></asp:Label></h3>
                        </div>
                    </div>
                </div>
            </div>
        </a>
        <a href="SchoolList.aspx" style="color:#676a6c !important;">
            <div class="col-md-3" id="AfterSchools" runat="server">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h3 class="panel-title">Total Non-Standard Schools</h3>
                    </div>
                    <div class="panel-body">
                        <i class="fa fa-university big-icon1" aria-hidden="true"></i>
                        <div class="pull-right">
                            <h3 class="headers">
                                <asp:Label runat="server" ID="lblAfterSchool" Text="0"></asp:Label></h3>
                        </div>
                    </div>
                </div>
            </div>
        </a>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
