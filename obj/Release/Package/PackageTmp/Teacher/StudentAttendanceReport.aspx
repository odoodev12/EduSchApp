<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentAttendanceReport.aspx.cs" Inherits="SchoolApp.Web.Teacher.StudentAttendanceReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">

    <style>
        @media only screen and (max-width: 750px) {
            #email {
                margin-top: 10px !important;
            }

            #filter {
                visibility: visible;
            }

            #LargeFiler {
                display: none;
            }
        }
    </style>
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

        .responsive {
            width: 100px !important;
            max-width: 100px !important;
            height: 100px !important;
            max-height: 100px !important;
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
                        <h1 id="homeHeading"><asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> / <a href="ClassAttendanceReport.aspx">Class Attendance Report</a> / <span>Student Attendance Report</span></h1>
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
                        <div class="col-md-12 well">
                            <div class="row">
                                <asp:ListView ID="ListView1" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-2 col-xs-5">
                                            <%--<img src='<%# "../" + Eval("Photo") %>' class="img-circle" width="100px" style="border: 2px solid #00a7ff;" />--%>
                                            <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                <img src='<%# "../" + Eval("Photo") %>' class="img-circle responsive" style="border: 2px solid #00a7ff;" /></a>

                                        </div>
                                        <div class="col-md-8 col-xs-7">
                                            <h3>
                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label></h3>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="well" id="LargeFiler">
                                <p>Filter by:</p>
                                <div class="form-group">
                                    <asp:Label runat="server" for="">From Date:</asp:Label>
                                    <asp:TextBox runat="server" type="date" ID="txtFromDate" class="form-control" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="">To Date:</asp:Label>
                                    <asp:TextBox runat="server" type="date" ID="txtToDate" class="form-control" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="studentname">Teacher</asp:Label>
                                    <asp:DropDownList runat="server" ID="drpTeacher" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="classname">Status</asp:Label>
                                    <asp:DropDownList ID="drpStatus" runat="server" class="form-control">
                                        <asp:ListItem Value="" Selected="True" Text="Select Status"></asp:ListItem>
                                        <asp:ListItem Value="Not Marked" Text="Not Marked"></asp:ListItem>
                                        <asp:ListItem Value="Present" Text="Present"></asp:ListItem>
                                        <asp:ListItem Value="Absent" Text="Absent"></asp:ListItem>
                                        <asp:ListItem Value="Present(Late)" Text="Present(Late)"></asp:ListItem>
                                        <asp:ListItem Value="Close" Text="Close"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <hr />
                                <p>Sort by:</p>
                                <div class="form-group">
                                    <asp:DropDownList ID="drpSort" runat="server" class="form-control">
                                        <asp:ListItem Value="Date" Text="Date"></asp:ListItem>
                                        <asp:ListItem Value="AttendenceStatus" Text="Attendence Status"></asp:ListItem>
                                        <asp:ListItem Value="TeacherName" Text="Teacher Name"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <div style="margin-top: 10px;">
                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rbAsc" runat="server" GroupName="optradio" />Ascending
										
                                        </label>

                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rbDesc" runat="server" GroupName="optradio" Checked="true" />Descending
										
                                        </label>
                                    </div>
                                </div>
                                <center><asp:Button ID="btnApply" runat="server" class="btn btn-primary" Text="Apply" OnClick="btnApply_Click"></asp:Button>
                         &nbsp;<asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>
                            </div>
                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <div class="form-group">
                                    <asp:Button runat="server" Text="Export to Excel" class="btn btn-primary btn-block" OnClick="btnExport_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <div class="form-group">
                                    <asp:Button runat="server" Text="Export to Email" ID="email" class="btn btn-primary btn-block" OnClick="email_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <%--<div class="bs-example" style="visibility: hidden;">
                            <button id="filter" class="btn btn-primary pull-right" data-toggle="modal" data-target="#filterModal">Filter</button>
                        </div>--%>
                        <div class="col-md-9 col-xs-12 table-responsive" style="padding: 0px;">
                            <asp:Label ID="lblInstruction" runat="server" Style="color: red !important; float: right !important;">Only 30 days record is displayed. Please use Filter section to see more</asp:Label>
                            <table class="table">
                                <thead style="background: #232323; color: white;">
                                    <tr>
                                        <th>Date</th>
                                        <th>Status</th>
                                        <th>Marked Time</th>
                                        <th>Teacher</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lstAttendance" runat="server" OnPagePropertiesChanging="lstAttendance_PagePropertiesChanging">
                                        <GroupTemplate>
                                            <td runat="server" id="itemPlaceholder"></td>
                                        </GroupTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("MarkedDate") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("Status") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("MarkedTime") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("TeacherName") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <tr runat="server">
                                                <td runat="server" colspan="4">
                                                    <tr runat="server" id="groupPlaceholder"></tr>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td runat="server">
                                                    <asp:DataPager runat="server" PageSize="30" ID="DataPager1">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                            <asp:NumericPagerField></asp:NumericPagerField>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </LayoutTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
    </section>
</asp:Content>

