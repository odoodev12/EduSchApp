<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Supportlog.aspx.cs" Inherits="SchoolApp.Web.School.Supportlog" %>

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
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myModal").modal('show');
        });
    </script>
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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="~/Teacher/Home.aspx" Text="Teacher"><i class="fa fa-home"></i> Teacher</asp:HyperLink>
                            <%}
                                else
                                { %>
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx" Text="School"><i class="fa fa-home"></i> School</asp:HyperLink>
                            <%} %>
                            / <span>Support Log</span></h1>
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
                            <div class="form-group">

                                <a href="SupportlogCreate.aspx" class="btn btn-primary btn-block"><i class="fa fa-plus"></i>&nbsp;Create</a>
                            </div>
                            <div class="well" id="LargeFiler">
                                <p>Filter by:</p>

                                <div class="form-group">
                                    <label for="classyear">Date From:</label>

                                    <asp:TextBox ID="txtDateFrom" runat="server" placeHolder="DD/MM/YYYY" class="form-control" TextMode="Date"></asp:TextBox>

                                </div>
                                <div class="form-group">
                                    <label for="classyear">Date To:</label>

                                    <asp:TextBox ID="txtDateTo" runat="server" placeHolder="DD/MM/YYYY" class="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="classname">Status</label>

                                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>

                                <hr />
                                <p>Sort by:</p>


                                <div class="form-group">
                                    <asp:DropDownList ID="drpSortBy" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Date" Text="Date" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="TicketNo" Text="TicketNo"></asp:ListItem>
                                        <asp:ListItem Value="Status" Text="Status"></asp:ListItem>
                                        <asp:ListItem Value="Created" Text="Create By"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <div style="margin-top: 10px;">
                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rbtnAscending" runat="server" GroupName="Ordering" Text="Ascending" />

                                        </label>

                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rbtnDescending" runat="server" GroupName="Ordering" Checked="true" Text="Descending" />

                                        </label>
                                    </div>
                                </div>
                                <center><asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>

                            </div>


                            <%--<div class="form-group">
                                <div class="bs-example" style="visibility: hidden; padding: 10px;">
                                    <button id="filter" class="btn btn-primary pull-right" data-toggle="modal" data-target="#filterModal">Filter</button>
                                </div>
                            </div>--%>
                        </div>
                        <div class="col-md-9 col-xs-12 ">
                            <div class="row">

                                <asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging">
                                    <%----%>
                                    <GroupTemplate>
                                        <div runat="server" id="itemPlaceholder"></div>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <p><b>Ticket No:</b> <%# Eval("TicketNumber") %> </p>
                                                        <p style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><b>Subject:</b> <%# Eval("Subject") %></p>
                                                        <p><b>Created By:</b> <%# Eval("CreatedByName") %></p>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <p><b>Date:</b> <%# Eval("SDate") %></p>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <p><b>Time:</b> <%# Eval("STime") %></p>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 10px;">
                                                        <span class="label label-primary col-xs-12 col-md-6" style="background: #00a7ff; margin-top: 10px; border-radius: 2px; padding: 7px; font-size: 18px;">Status: <%# Eval("Status") %></span>
                                                        <a class="btn btn-primary " href="SupportlogView.aspx?ID=<%# Eval("ID") %>" style="margin: 10px; margin-top: 10px;">View Detail</a>
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
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                            <asp:NumericPagerField></asp:NumericPagerField>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                </asp:ListView>

                                <%-- <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p><b>Ticket No:</b> 12345 </p>
                                                <p><b>Subject:</b>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                                <p><b>Ceated By:</b> Loren ipsum</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Date:</b> 01/08/2017</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Time:</b> 4pm</p>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <span class="label label-primary col-xs-12 col-md-6" style="background: #00a7ff; margin-top: 10px; border-radius: 2px; padding: 7px; font-size: 18px;">Status: Resolved</span>
                                                <a class="btn btn-primary " href="school_home_supportlog_view.aspx" style="margin: 10px; margin-top: 10px;">View Detail</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p><b>Ticket No:</b> 12345 </p>
                                                <p><b>Subject:</b>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                                <p><b>Ceated By:</b> Loren ipsum</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Date:</b> 01/08/2017</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Time:</b> 4pm</p>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <span class="label label-primary col-xs-12 col-md-6" style="background: #00a7ff; margin-top: 10px; border-radius: 2px; padding: 7px; font-size: 18px;">Status: Resolved</span>
                                                <a class="btn btn-primary " href="school_home_supportlog_view.aspx" style="margin: 10px; margin-top: 10px;">View Detail</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p><b>Ticket No:</b> 12345 </p>
                                                <p><b>Subject:</b>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                                <p><b>Ceated By:</b> Loren ipsum</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Date:</b> 01/08/2017</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Time:</b> 4pm</p>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <span class="label label-primary col-xs-12 col-md-6" style="background: #00a7ff; margin-top: 10px; border-radius: 2px; padding: 7px; font-size: 18px;">Status: Resolved</span>
                                                <a class="btn btn-primary " href="school_home_supportlog_view.html" style="margin: 10px; margin-top: 10px;">View Detail</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p><b>Ticket No:</b> 12345 </p>
                                                <p><b>Subject:</b>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                                <p><b>Ceated By:</b> Loren ipsum</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Date:</b> 01/08/2017</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Time:</b> 4pm</p>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <span class="label label-primary col-xs-12 col-md-6" style="background: #00a7ff; margin-top: 10px; border-radius: 2px; padding: 7px; font-size: 18px;">Status: Resolved</span>
                                                <a class="btn btn-primary " href="school_home_supportlog_view.html" style="margin: 10px; margin-top: 10px;">View Detail</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p><b>Ticket No:</b> 12345 </p>
                                                <p><b>Subject:</b>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                                <p><b>Ceated By:</b> Loren ipsum</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Date:</b> 01/08/2017</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Time:</b> 4pm</p>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <span class="label label-primary col-xs-12 col-md-6" style="background: #00a7ff; margin-top: 10px; border-radius: 2px; padding: 7px; font-size: 18px;">Status: Resolved</span>
                                                <a class="btn btn-primary " href="school_home_supportlog_view.html" style="margin: 10px; margin-top: 10px;">View Detail</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p><b>Ticket No:</b> 12345 </p>
                                                <p><b>Subject:</b>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                                <p><b>Ceated By:</b> Loren ipsum</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Date:</b> 01/08/2017</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Time:</b> 4pm</p>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <span class="label label-primary col-xs-12 col-md-6" style="background: #00a7ff; margin-top: 10px; border-radius: 2px; padding: 7px; font-size: 18px;">Status: Resolved</span>
                                                <a class="btn btn-primary " href="school_home_supportlog_view.html" style="margin: 10px; margin-top: 10px;">View Detail</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p><b>Ticket No:</b> 12345 </p>
                                                <p><b>Subject:</b>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                                <p><b>Ceated By:</b> Loren ipsum</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Date:</b> 01/08/2017</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Time:</b> 4pm</p>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <span class="label label-primary col-xs-12 col-md-6" style="background: #00a7ff; margin-top: 10px; border-radius: 2px; padding: 7px; font-size: 18px;">Status: Resolved</span>
                                                <a class="btn btn-primary " href="school_home_supportlog_view.html" style="margin: 10px; margin-top: 10px;">View Detail</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p><b>Ticket No:</b> 12345 </p>
                                                <p><b>Subject:</b>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                                <p><b>Ceated By:</b> Loren ipsum</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Date:</b> 01/08/2017</p>
                                            </div>
                                            <div class="col-md-6">
                                                <p><b>Time:</b> 4pm</p>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <span class="label label-primary col-xs-12 col-md-6" style="background: #00a7ff; margin-top: 10px; border-radius: 2px; padding: 7px; font-size: 18px;">Status: Resolved</span>
                                                <a class="btn btn-primary " href="school_home_supportlog_view.html" style="margin: 10px; margin-top: 10px;">View Detail</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
    <div id="filterModal" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Filter by : </h4>
                </div>
                <div class="modal-body" style="z-index: 9999 !important;">
                    <div class="form-group">
                        <label for="classyear">Date From:</label>
                        <input type="date" class="form-control" id="">
                    </div>
                    <div class="form-group">
                        <label for="classyear">Date To:</label>
                        <input type="date" class="form-control" id="">
                    </div>
                    <div class="form-group">
                        <label for="classname">Status</label>
                        <select class="form-control">
                            <option>Select</option>
                            <option>Resolved</option>
                            <option>Unsolved</option>
                        </select>
                    </div>
                    <hr />
                    <p>Sort by:</p>

                    <div class="form-group">
                        <div style="margin-top: 10px;">
                            <label class="radio-inline">
                                <input type="radio" name="optradio" checked>Ascending
                            </label>
                            <label class="radio-inline">
                                <input type="radio" name="optradio">Descending
                            </label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="classname">Select</label>
                        <select class="form-control">
                            <option>Date</option>
                            <option>Status</option>
                            <option>Create By</option>
                        </select>
                    </div>
                    <button class="btn btn-primary btn-block">Apply</button>

                </div>

            </div>
        </div>
    </div>
</asp:Content>
