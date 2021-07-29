<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyPickupStatus.aspx.cs" Inherits="SchoolApp.Web.Parent.DailyPickupStatus" %>

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
            width: 60px !important;
            max-height: 60px !important;
            max-width: 60px !important;
            height: 60px !important;
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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> Parent</asp:HyperLink>
                            / <span>Daily Pickup Status</span></h1>

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

                                <div>
                                    <div class="form-group">
                                        <label for="">Date:</label>
                                        <asp:TextBox ID="txtDate" runat="server" placeHolder="DD/MM/YYYY" class="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>

                                    </div>
                                    <p>Filter by:</p>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="studentname">Student Name</asp:Label>
                                        <asp:DropDownList ID="drpStudent" runat="server" class="form-control">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="classname">Picker</asp:Label>
                                        <asp:DropDownList ID="drpPicker" runat="server" class="form-control">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="classname">Status</asp:Label>
                                        <asp:DropDownList ID="drpStatus" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <p>Sort by:</p>
                                <div>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="classname">Student Name</asp:Label>
                                        <asp:DropDownList ID="drpSort" runat="server" class="form-control">
                                            <asp:ListItem Value="Date" Text="Date" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="ChildName" Text="Child Name"></asp:ListItem>
                                            <asp:ListItem Value="Status" Text="Pickup Status"></asp:ListItem>
                                            <asp:ListItem Value="Picker" Text="Picker"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <div style="margin-top: 10px;">
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbAsc" runat="server" GroupName="optradio" Checked="true" />Ascending
                                            </label>

                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbDesc" runat="server" GroupName="optradio" />Descending
                                            </label>
                                        </div>
                                    </div>
                                    <center><asp:Button ID="btnApply" runat="server" class="btn btn-primary" Text="Apply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-9">

                            <table class="table">
                                <thead style="background: #232323; color: white;">
                                    <tr>
                                        <th>Child Name</th>
                                        <th>Status</th>
                                        <th>Time</th>
                                        <th>Picker Name</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging">
                                        <GroupTemplate>

                                            <td runat="server" id="itemPlaceholder"></td>

                                        </GroupTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <p>
                                                        <span class="chat-img">
                                                            <a class="example-image-link" href='<%# "../" + Eval("StudentPic") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'><img class="example-image img-circle responsives" style="border: 2px solid #00a7ff;" src='<%# "../" + Eval("StudentPic") %>' alt="" /></a>
                                                            <%--<img width="60" class="img-circle" style="border: 2px solid #00a7ff;" src='<%# "../" + Eval("StudentPic") %>'>--%>
                                                        </span>
                                                        &nbsp;
                                                    </p>
                                                    <p><%# Eval("StudentName") %></p>
                                                </td>
                                                <td style="border-bottom: 1px solid #ddd !important;" class="color_blue"><%# Eval("PickStatus") %><br />
                                                    
                                                  <%--  <% if (IsStandardSchool())
                                                        { %>--%>
                                                    <span style="font-size:11px !important;"><%# Eval("AfterSchoolName")%></span></td>
                                                <%--<%} %>--%>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("Pick_Time") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("PickerName") %></td>
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
                                                    <asp:DataPager runat="server" PageSize="10" ID="DataPager1">
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
        <br /><br />
    </section>
</asp:Content>
