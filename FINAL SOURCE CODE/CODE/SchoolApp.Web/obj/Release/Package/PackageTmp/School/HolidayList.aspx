<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HolidayList.aspx.cs" Inherits="SchoolApp.Web.School.HolidayList" %>

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

        .view_detail a {
            font-size: 10px;
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
                            <% if (ISTeacher() == true)
                                { %>
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="~/Teacher/Home.aspx"><i class="fa fa-home"></i>Teacher</asp:HyperLink>
                            <%}
                                else
                                { %>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i>School</asp:HyperLink>
                            <%} %> / <span>Holiday</span></h1>
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
                        <div class="col-md-3">
                            <asp:HyperLink ID="hlink" runat="server" NavigateUrl="HolidayAdd.aspx" class="btn btn-primary btn-block" Style="border: none; padding: 10px;"><i class="fa fa-plus"></i> Add Holiday</asp:HyperLink>
                            <hr />
                            <div class="well">
                                <h4 style="margin-top: 0px;">Holiday Name</h4>
                                <div class="sidebar-box">
                                    <asp:TextBox ID="txtHolidayName" runat="server" class="form-control" placeholder="Holiday Name" />
                                </div>

                                <h4 style="margin-top: 0px;">Date From</h4>
                                <div class="sidebar-box">
                                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" placeholder="MM/dd/yyyy" CssClass="form-control" />

                                </div>
                                <h4 style="margin-top: 0px;">Date To</h4>
                                <div class="sidebar-box">
                                    <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" placeholder="MM/dd/yyyy" CssClass="form-control" />
                                </div>
                                <h4 style="margin-top: 0px;">Select Class Status</h4>
                                <div class="sidebar-box">
                                    <asp:DropDownList ID="drpClassStatus" runat="server" class="form-control" data-style="btn-primary">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="InActive" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <center><asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>

                            </div>
                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <asp:ListView runat="server" ID="lstHoliday" OnItemCommand="lstHoliday_ItemCommand" OnPagePropertiesChanging="lstHoliday_PagePropertiesChanging">
                                    <GroupTemplate>
                                        <div runat="server" id="itemPlaceholder"></div>
                                    </GroupTemplate>
                                    <ItemTemplate>

                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <p>
                                                            <b>Holiday Name:</b> <%# Eval("Name") %> <span class="pull-right">
                                                                <asp:Button ID="btnEdit" runat="server" CommandName="btnEdit" CommandArgument='<%# Eval("ID") %>' class="btn btn-primary" Text="Edit" />
                                                            </span>
                                                        </p>
                                                        <p>
                                                            <b>Date:</b> <%# Eval("FromDate") %> - <%# Eval("ToDate") %> <span class="pull-right" style="margin-top: 15px; margin-right: -63px;">
                                                                <asp:Button ID="btnDelete" runat="server" CommandName="btnDelete" CommandArgument='<%# Eval("ID") %>' OnClientClick="return ConfirmMsg();" class="btn btn-danger" Text="Delete" /></span>
                                                        </p>
                                                        <p><b>Status:</b> <%# Eval("ActiveStatus") %> </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <table runat="server" class="col-md-12">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <tr runat="server" id="groupPlaceholder"></tr>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td runat="server">
                                                    <asp:DataPager runat="server" PageSize="10" ID="DataPager1">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ButtonCssClass="btn btn-primary" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                            <asp:NumericPagerField NumericButtonCssClass="btn btn-default"></asp:NumericPagerField>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ButtonCssClass="btn btn-primary" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
    <script type="text/javascript">
        function ConfirmMsg() {
            if (confirm("Do you want this Holiday to be deleted ?")) {
                //SetID(ID);
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
