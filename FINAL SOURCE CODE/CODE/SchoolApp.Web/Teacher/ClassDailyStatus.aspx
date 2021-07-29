<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassDailyStatus.aspx.cs" Inherits="SchoolApp.Web.Teacher.ClassDailyStatus" %>

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

        .responsives {
            width: 60px !important;
            max-width: 60px !important;
            height: 60px !important;
            max-height: 60px !important;
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
                        <h1 id="homeHeading"><asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i>Teacher</asp:HyperLink> / <span>Class Daily Report</span></h1>
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
                            <div id="LargeFiler">
                                <div class="form-group">
                                    <asp:Label runat="server" for="studentname">Select Class</asp:Label>
                                    <asp:DropDownList ID="drpClass" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="">Date:</asp:Label>
                                    <asp:TextBox runat="server" type="date" class="form-control" ID="txtDate" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                </div>
                                <div class="well">
                                    <p>Filter by:</p>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="studentname">Student Name</asp:Label>
                                        <asp:DropDownList ID="drpStudent" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" for="classname">Status</asp:Label>
                                        <asp:DropDownList ID="drpStatus" runat="server" class="form-control">
                                            <%--                                            <asp:ListItem Value="" Text="Select Status"></asp:ListItem>
                                            <asp:ListItem Value="Picked" Text="Picked"></asp:ListItem>
                                            <asp:ListItem Value="UnPicked" Text="UnPicked"></asp:ListItem>
                                            <asp:ListItem Value="Mark as Absent" Text="Mark as Absent"></asp:ListItem>
                                            <asp:ListItem Value="SendtoOffice" Text="Send to Office"></asp:ListItem>
                                            <asp:ListItem Value="SendtoAfterSchool" Text="Send to After School"></asp:ListItem>
                                            <asp:ListItem Value="Picked(Late)" Text="Picked(Late)"></asp:ListItem>
                                            <asp:ListItem Value="Picked(Chargeable)" Text="Picked(Chargeable)"></asp:ListItem>
                                            <asp:ListItem Value="Picked(Reportable)" Text="Picked(Reportable)"></asp:ListItem>
                                            <asp:ListItem Value="Closed" Text="Closed"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <hr />
                                    <p>Sort by:</p>
                                    <div class="form-group">
                                        <asp:DropDownList ID="drpSort" runat="server" class="form-control">
                                            <asp:ListItem Value="0" Text="Select Sort By"></asp:ListItem>
                                            <asp:ListItem Value="Student" Text="Student Name"></asp:ListItem>
                                            <asp:ListItem Value="Status" Text="Pickup Status"></asp:ListItem>
                                            <asp:ListItem Value="Picker" Text="Picker"></asp:ListItem>
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
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" Text="Export to Excel" class="btn btn-primary btn-block" OnClick="btnExport_Click"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="email" class="btn btn-primary btn-block" Text="Export to Email" OnClick="email_Click"></asp:Button>
                            </div>
                        </div>
                        <div class="col-md-9 col-xs-12 table-responsive" style="padding: 0px;">
                            <table class="table">
                                <thead style="background: #232323; color: white;">
                                    <tr>
                                        <th>Student Name</th>
                                        <th style="text-align: center;">Student Report</th>
                                        <th>Status</th>
                                        <th>Time</th>
                                        <th>Picker Name</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging" OnItemDataBound="ListView1_ItemDataBound">
                                        <GroupTemplate>
                                            <td runat="server" id="itemPlaceholder"></td>
                                        </GroupTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%--<p>
                                                        <span class="chat-img">
                                                            <a class="example-image-link" href='<%# "../" + Eval("StudentPic") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                                <img class="img-circle responsives" style="border: 2px solid #00a7ff;" alt="" src='<%# "../" + Eval("StudentPic") %>'></a>
                                                        </span>
                                                        &nbsp;
                                                    </p>--%>
                                                    <p>
                                                        <%# Eval("StudentName") %>
                                                </td>
                                                <td style="text-align: center; border-bottom: 1px solid #ddd">
                                                    <p>
                                                        <asp:HyperLink ID="lnkReport" NavigateUrl='<%# string.Format("StudentPickupReport.aspx?ID={0}", HttpUtility.UrlEncode(Eval("StudentID").ToString())) %>' runat="server">View Student Pickup Report</asp:HyperLink>

                                                        <%--StudentPickupReport.aspx?ID=<%# Eval("StudentID") %>--%>
                                                    </p>
                                                </td>
                                                <td class="color_red" style="border-bottom: 1px solid #ddd">
                                                    <div>
                                                        <%# Eval("PickStatus") %>
                                                    </div>                                                    
                                                    <span style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis; font-size: 11px !important;"><%# Eval("SchoolName") %></span>
                                                </td>
                                                <td style="border-bottom: 1px solid #ddd"><%# Eval("Pick_Time") %></td>
                                                <td style="border-bottom: 1px solid #ddd"><%# Eval("PickerName") %></td>
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
    </section>
    <br /><br />
</asp:Content>
