<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logactivity.aspx.cs" Inherits="SchoolApp.Web.School.Logactivity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .class-box {
            padding: 20px;
            background: #fff;
            margin-bottom: 20px;
        }

            .class-box p {
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
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> School</asp:HyperLink>
                            / <span>Log Activity</span></h1>
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

                            <div class="well">
                                <p>Filter by:</p>
                                <div id="fm1">
                                    <div class="form-group">
                                        <label for="classyear">Date From:</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" placeholder="MM/dd/yyyy" TextMode="date" class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label for="classyear">Date To:</label>
                                        <asp:TextBox ID="txtToDate" runat="server" placeholder="MM/dd/yyyy" TextMode="date" class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label for="classyear">Log Name:</label>
                                        <%--<asp:TextBox ID="txtLogName" runat="server" class="form-control" />--%>
                                        <asp:DropDownList ID="drpLogName" runat="server" class="form-control">
                                            <asp:ListItem Text="Select Log Name" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="School Profile" Value="Profile"></asp:ListItem>
                                            <asp:ListItem Text="Class" Value="Class"></asp:ListItem>
                                            <asp:ListItem Text="BulkData" Value="BulkData"></asp:ListItem>
                                            <asp:ListItem Text="Teacher" Value="Teacher"></asp:ListItem>
                                            <asp:ListItem Text="NonTeacher" Value="NonTeacher"></asp:ListItem>
                                            <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                                            <asp:ListItem Text="UserRole" Value="UserRole"></asp:ListItem>
                                            <asp:ListItem Text="Holiday" Value="Holiday"></asp:ListItem>
                                            <asp:ListItem Text="Support" Value="Support"></asp:ListItem>
                                            <asp:ListItem Text="Message" Value="Message"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <%-- <div class="form-group">
                                        <label for="classyear">Document Category:</label>
                                        <asp:TextBox ID="txtDocCategory" runat="server" class="form-control" />
                                    </div>--%>
                                    <div class="form-group">
                                        <label for="classyear">Updated By:</label>
                                        <%--<asp:TextBox ID="txtUpdatedBy" runat="server" class="form-control" />
                                        <ajax:AutoCompleteExtender ID="AutoCompleteTeachers" runat="server" TargetControlID="txtUpdatedBy"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetUsers">
                                        </ajax:AutoCompleteExtender>--%>
                                        <asp:DropDownList ID="ddlUpdatedBy" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <p>Sort by:</p>
                                <div id="fm2">

                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Date" Text="Date"></asp:ListItem>
                                            <asp:ListItem Value="LogName" Text="Log Name"></asp:ListItem>
                                            <asp:ListItem Value="UpdatedBy" Text="Updated By"></asp:ListItem>
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
                                <hr />
                                <asp:LinkButton ID="hlink" runat="server" class="btn btn-primary btn-block" Style="border: none; padding: 10px;" OnClick="hlink_Click"><i class="fa fa-trash"></i> Clear Activity Logs</asp:LinkButton>
                            </div>

                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <asp:ListView runat="server" ID="lstLog" OnPagePropertiesChanging="lstLog_PagePropertiesChanging">
                                    <GroupTemplate>
                                        <div runat="server" id="itemPlaceholder"></div>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <div class="row">
                                                    <div class="col-md-2 col-xs-5">
                                                        <img src="../img/icon_activity.png" class="img-circle pull-right img-responsive" width="100%" />
                                                    </div>
                                                    <div class="col-md-10 col-xs-7" style="padding-left: 0px;">
                                                        <p data-toggle="tooltip" title='<%# Eval("LogText") %>' style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                                            <%# Eval("ShortLogText") %>
                                                            <br />
                                                            <%# Eval("StrDate") %>
                                                        </p>
                                                        <h4><b>Updated By:</b> <%# Eval("UserName") %></h4>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <table runat="server" class="col-md-12">
                                            <tr runat="server">
                                                <td runat="server" colspan="4">
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
                                <%--<div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-5">
                                                <img src="img/icon_activity.png" class="img-circle pull-right img-responsive" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-7" style="padding-left:0px;">
                                                <p>The new class has been created on <br/> 07/09/2017 at 07:05 PM</p>
                                                <h4><b>Update By:</b> Eric Banes</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-5">
                                                <img src="img/icon_activity.png" class="img-circle pull-right img-responsive" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-7" style="padding-left:0px;">
                                                <p>The new class has been created on <br/> 07/09/2017 at 07:05 PM</p>
                                                <h4><b>Update By:</b> Eric Banes</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-5">
                                                <img src="img/icon_activity.png" class="img-circle pull-right img-responsive" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-7" style="padding-left:0px;">
                                                <p>The new class has been created on <br/> 07/09/2017 at 07:05 PM</p>
                                                <h4><b>Update By:</b> Eric Banes</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-5">
                                                <img src="img/icon_activity.png" class="img-circle pull-right img-responsive" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-7" style="padding-left:0px;">
                                                <p>The new class has been created on <br/> 07/09/2017 at 07:05 PM</p>
                                                <h4><b>Update By:</b> Eric Banes</h4>
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
</asp:Content>
