<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportableStudentReport.aspx.cs" Inherits="SchoolApp.Web.Reports.ReportableStudentReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/School/Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> /
                            <asp:HyperLink ID="hlinkPickupReport" runat="server" NavigateUrl="~/School/Report.aspx" Text="Report"></asp:HyperLink>
                            / <span>Reportable Student Report</span></h1>
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
                        <div class="col-lg-6">
                            <div class="float-e-margins">
                                <div class="form-horizontal formelement">
                                    <div class="form-group">
                                        <label class="col-sm-3" runat="server" for="">From Date:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="date" ID="txtFromDate" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3" for="classyear">Student No</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtStNo" runat="server" class="form-control" placeholder="Enter Student No" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3" runat="server" for="studentname">
                                            Class Name
                                        </label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList runat="server" ID="drpClassName" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="float-e-margins">
                                <div class="form-horizontal formelement">
                                    <div class="form-group">
                                        <label class="col-sm-3" runat="server" for="">
                                            To Date:
                                        </label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="date" ID="txtToDate" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3" for="classyear">Student Name</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtStName" runat="server" class="form-control" placeholder="Enter Student Name" />
                                            <ajax:AutoCompleteExtender ID="AutoCompleteSt" runat="server" TargetControlID="txtStName"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetStudentss">
                                            </ajax:AutoCompleteExtender>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="row" style="text-align: center; margin-bottom: 15px;">
                        <asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                        &nbsp;
                        <asp:Button runat="server" Text="Export to Excel" class="btn btn-primary" OnClick="btnExport_Click"></asp:Button>
                    </div>
                    <div class="row">
                        <%--<div class="col-md-3">
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
                                        <asp:Label runat="server" for="classyear">Student No</asp:Label>
                                        <asp:TextBox ID="txtStNo" runat="server" class="form-control" placeholder="Enter Student No" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="classyear">Student Name</asp:Label>
                                        <asp:TextBox ID="txtStName" runat="server" class="form-control" placeholder="Enter Student Name" />
                                        <ajax:autocompleteextender id="AutoCompleteSt" runat="server" targetcontrolid="txtStName"
                                            minimumprefixlength="1" enablecaching="true" completionsetcount="1" completioninterval="1000" servicemethod="GetStudentss">
                                        </ajax:autocompleteextender>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="studentname">Class Name</asp:Label>
                                        <asp:DropDownList runat="server" ID="drpClassName" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <hr />
                                    <center><asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>
                                </div>
                                <asp:Button runat="server" Text="Export to Excel" class="btn btn-primary btn-block" OnClick="btnExport_Click"></asp:Button>
                            </div>--%>
                        <div class="col-md-12 col-xs-12 table-responsive" style="padding: 0px;">
                            <div style="color: red; padding-bottom: 10px;">Note : Enter the parameters for your report above and click APPLY. Alternatively,  click APPLY without parameters to retrieve all data
                                <asp:LinkButton runat="server" ID="linkSendEmail" Text="Send Email" Visible="false" OnClick="linkSendEmail_Click"/>
                            </div>
                            <table class="table">
                                <thead style="background: #232323; color: white;">
                                    <tr>
                                        <th>Date</th>
                                        <th>Student Number</th>
                                        <th>Student Name</th>
                                        <th>Class Name</th>
                                        <th>Pickup Time</th>
                                        <th>Pickup Status</th>
                                        <th>Teacher Name</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lstTable" runat="server" OnPagePropertiesChanging="lstTable_PagePropertiesChanging">
                                        <GroupTemplate>
                                            <td runat="server" id="itemPlaceholder"></td>
                                        </GroupTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("Pick_Date") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("StNo") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("StudentName") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("ClassName") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("Pick_Time") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("PickStatus") %></td>
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
                                                    <asp:DataPager runat="server" PageSize="10" ID="DataPager2">
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
    </section>
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
</asp:Content>
