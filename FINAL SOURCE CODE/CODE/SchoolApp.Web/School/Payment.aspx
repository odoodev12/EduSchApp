<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="SchoolApp.Web.School.Payment" %>

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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="~/Teacher/Home.aspx"><i class="fa fa-home"></i> Teacher</asp:HyperLink>
                            <%}
                                else
                                { %>
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> School</asp:HyperLink>
                            <%} %>
                            / <span>Payment</span></h1>

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
                            <div class="well" id="LargeFiler">
                                <p>Filter by:</p>
                                <div>
                                    <div class="form-group">
                                        <label for="classyear">Date From:</label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" placeHolder="DD/MM/YYYY" class="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="classyear">Date To:</label>
                                        <asp:TextBox ID="txtDateTo" runat="server" placeHolder="DD/MM/YYYY" class="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="classyear">Payment Status</label>
                                        <asp:DropDownList ID="drpStatus" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <p>Sort by:</p>
                                <div>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSortBy" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Date" Text="Date" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Status" Text="Status"></asp:ListItem>
                                            <asp:ListItem Value="InvoiceNo" Text="InvoiceNo"></asp:ListItem>
                                            <asp:ListItem Value="TransactionType" Text="TransactionType"></asp:ListItem>
                                            <asp:ListItem Value="Amount" Text="Amount"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <div style="margin-top: 10px;">
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbtnAsending" runat="server" GroupName="optradio" Text="Ascending" />
                                            </label>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbtnDescending" runat="server" GroupName="optradio" Checked="true" Text="Descending" />
                                            </label>
                                        </div>
                                    </div>
                                    <center><asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>
                                </div>
                            </div>
                            <%-- <div class="bs-example" style="visibility:hidden;">
                                <asp:Button ID="btnFilter" runat="server" class="btn btn-primary pull-right" data-toggle="modal" data-target="#filterModal" Text="Filter" />
                            </div>--%>
                        </div>




                        <div class="col-md-9 col-xs-12 ">
                            <div class="row">
                                <asp:Label ID="lblMessage" runat="server" Visible="false" Text="No payment invoice recorded yet for this account"></asp:Label>
                                <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand" OnPagePropertiesChanging="ListView1_PagePropertiesChanging">
                                    <%----%>
                                    <GroupTemplate>
                                        <div runat="server" id="itemPlaceholder"></div>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">

                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../img/icon_payment.png" class="img-circle" Width="100%" />


                                                    </div>
                                                    <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                        <p><b>Invoice:</b> <%# Eval("InvoiceNumber") %> </p>
                                                        <p><b>Amount:</b> <%# Eval("Transaction_Amount") %></p>
                                                        <p data-toggle="tooltip" title='<%# Eval("strTransectionType") %>' style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><b>Transaction Type:</b> <%# Eval("strTransectionType") %></p>
                                                        <p><b>Date:</b> <%# Eval("strCreatedDate") %></p>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <p data-toggle="tooltip" title='<%# Eval("Description") %>' style="white-space: nowrap; margin-top: 10px; overflow: hidden; text-overflow: ellipsis;">
                                                            <b>Description:</b>
                                                            <%# Eval("Description") %>
                                                        </p><br />
                                                    </div>
                                                    <div class="col-md-12">
                                                        <h3 class="pull-left"><asp:LinkButton ID="lnkView" CommandName="btnView" CommandArgument='<%# Eval("ID") %>' runat="server" class="btn btn-primary" style="border-radius: 2px; font-weight:bold;">&nbsp;<i class="fa fa-file-pdf-o"></i>&nbsp;&nbsp;View</asp:LinkButton></h3>
                                                        <h3 class="pull-right"><span class="label label-primary " style="background: #00a7ff; border-radius: 2px;">Status: <%# Eval("strStatus") %></span></h3>
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


                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
    <%-- <div id="filterModal" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Filter by : </h4>
                </div>
                <div class="modal-body" style="z-index: 9999 !important;">
                    <div class="form-group">
                        <label for="classyear">Date From:</label>
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="date" class="form-control"/>
                    </div>
                    <div class="form-group">
                        <label for="classyear">Date To:</label>
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="date" class="form-control"/>
                    </div>
                    <hr />
                    <p>Sort by:</p>
                    <div>
                        <div class="form-group">
                            <div style="margin-top:10px;">
                                <label class="radio-inline">
                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="optradio" Checked="true" Text="Ascending" />
                                </label>
                                <label class="radio-inline">
                                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="optradio" Text="Descending" />
                                </label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="classname">Select</label>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Date"></asp:ListItem>
                                            <asp:ListItem Text="Status"></asp:ListItem>
                                            <asp:ListItem Text="Create"></asp:ListItem>
                                        </asp:DropDownList>
                        </div>
                        <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-block" Text="Apply" />
                    </div>
                </div>

            </div>
        </div>
    </div>--%>
</asp:Content>
