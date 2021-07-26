<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SchoolApp.Web.School.Dashboard" %>

<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>  --%>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
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
            background: #FFFFFF;
            text-align: center;
            margin-bottom: 30px;
            padding: 40px 20px 20px;
            border: solid 1px #cccccc;
        }

            .feature h3 {
                color: #232323;
                font-size: 20px;
                text-transform: uppercase;
            }

            .feature:hover {
                background: #F5F5F5;
                -webkit-transform: translate(0, 1em);
                -moz-transform: translate(0, 1em);
                -o-transform: translate(0, 1em);
                -ms-transform: translate(0, 1em);
                transform: translate(0, 1em);
            }

                .feature:hover img {
                    -webkit-transform: scale(1.1);
                    -moz-transform: scale(1.1);
                    -o-transform: scale(1.1);
                    -ms-transform: scale(1.1);
                    transform: scale(1.1);
                }

                .feature:hover .title_border {
                    background-color: #00a7ff;
                    width: 40%;
                }

            .feature .title_border {
                width: 0%;
                height: 2px;
                background: #00a7ff;
                margin: 0 auto;
                margin-top: 12px;
                margin-bottom: 8px;
            }

            .feature > a {
                text-decoration: none;
            }




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

        .panel-danger {
            border-color: #ed5565;
        }

        element {
            border-color: #ed5565;
        }

        .panel {
            margin-bottom: 30px !important;
            border-radius: 4px;
            background-color: #fff;
        }


        .panel-body {
            padding: 15px;
        }

        .panel-danger > .panel-heading {
            background-color: #ed5565 !important;
            border-color: #ed5565;
            color: #ffffff;
        }

        .panel-heading {
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-bottom-color: transparent;
            border-top-right-radius: 3px;
            border-top-left-radius: 3px;
        }

        .panel-warning {
            border-color: #f8ac59;
        }

        .panel-info {
            border-color: #23c6c8;
        }

        .panel-success {
            border-color: #1c84c6;
        }

        .col-md-3 {
            width: 28% !important;
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
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> School</asp:HyperLink>
                            / <span>Dashboard</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-1"></div>
                    <a href="Supportlog.aspx">
                        <div class="col-md-3" id="Schools" runat="server">
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><b>No of Oustanding Support</b></h3>
                                </div>
                                <div class="panel-body">
                                    <i class="fa fa-indent big-icon1" aria-hidden="true"></i>
                                    <div class="pull-right">
                                        <h3 class="headers">
                                            <asp:Label runat="server" ID="lblOustanding" Text="0"></asp:Label></h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                    <a href="Payment.aspx">
                        <div class="col-md-3" id="Organisations" runat="server">
                            <div class="panel panel-warning">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><b>Unpaid Invoice</b></h3>
                                </div>
                                <div class="panel-body">
                                    <i class="fa fa-users big-icon1" aria-hidden="true"></i>
                                    <div class="pull-right">
                                        <h3 class="headers">
                                            <asp:Label runat="server" ID="lblUnpaidInvoice" Text="0"></asp:Label></h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                    <a href="Student.aspx">
                        <div class="col-md-3" id="Classes" runat="server">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><b>No of Student</b></h3>
                                </div>
                                <div class="panel-body">
                                    <i class="fa fa-users big-icon1" aria-hidden="true"></i>
                                    <div class="pull-right">
                                        <h3 class="headers">
                                            <asp:Label runat="server" ID="lblStudent" Text="0"></asp:Label></h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                    <div class="col-md-3" style="width: 23% !important;">
                    </div>
                    <a href="Teacher.aspx">
                        <div class="col-md-3" id="Teachers" runat="server">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><b>No of Teacher</b> </h3>
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
                    </a>
                    <a href="Class.aspx">
                        <div class="col-md-3" id="Students" runat="server">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><b>No of Class </b></h3>
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
                    </a>
                    <div class="col-md-1"></div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
</asp:Content>
