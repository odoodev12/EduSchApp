<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassAttendanceReport.aspx.cs" Inherits="SchoolApp.Web.Teacher.ClassAttendanceReport" %>

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
                        <h1 id="homeHeading"><asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> / <a href="attendance.aspx">Attendance</a> / <span>Class Attendance Report</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features">
            <div class="sidebar-section">


                <div class="col-md-3" id="LargeFiler">
                    <div class="form-group">
                        <asp:Label runat="server" for="studentname">Select Class</asp:Label>
                        <asp:DropDownList ID="drpClass" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="">Date:</asp:Label>
                        <asp:TextBox runat="server" class="form-control" ID="txtDate" TextMode="Date" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                        <%--<ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="txtDate" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>--%>
                    </div>
                    <div class="well">
                        <p>Filter by:</p>
                        <div class="form-group">
                            <asp:Label runat="server" for="studentname">Student Name</asp:Label>
                            <asp:DropDownList ID="drpStudent" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                        <%--<div class="form-group">
                            <asp:Label runat="server" for="classname">Picker</asp:Label>
                            <asp:DropDownList ID="drpPicker" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>--%>
                        <div class="form-group">
                            <asp:Label runat="server" for="classname">Attendance Status </asp:Label>
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
                                <asp:ListItem Value="0" Text="Select Sorting"></asp:ListItem>
                                <asp:ListItem Value="StudentName" Text="Student Name"></asp:ListItem>
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
                    <%--Start by shailesh parmar--%>
                    <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" class="btn btn-primary btn-block" Text="Export to Excel" OnClick="btnExport_Click" ></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="email" class="btn btn-primary btn-block" Text="Export to Email" OnClick="email_Click" ></asp:Button>
                            </div>
                    </div>
                <%--End--%>
                </div>
                <div class="bs-example" style="visibility: hidden;">
                    <button id="filter" class="btn btn-primary pull-right" data-toggle="modal" data-target="#filterModal">Filter</button>
                </div>
                <div class="col-md-9 col-xs-12 table-responsive" style="padding: 0px;">
                    <table class="table">
                        <thead style="background: #232323; color: white;">
                            <tr>
                                <th style="width: 420px;">Student</th>
                                <th style="width: 120px;">Attendance</th>
                                <th style="width: 130px;">Marked Time</th>
                                <th style="width: 180px;">Teacher</th>
                            </tr>
                        </thead>

                        <tbody>
                            <asp:ListView ID="lstAttendanceRpt" runat="server" OnPagePropertiesChanging="lstAttendanceRpt_PagePropertiesChanging" OnItemDataBound="lstAttendanceRpt_ItemDataBound">
                                <GroupTemplate>
                                    <td runat="server" id="itemPlaceholder"></td>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <p>
                                                <span class="chat-img">
                                                    <%--<img width="60" height="60" class="img-circle" style="border: 2px solid #00a7ff;" alt="User Avatar" src='<%# "../" + Eval("StudentPic") %>'>--%>
                                                    <a class="example-image-link" href='<%# "../" + Eval("StudentPic") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                        <img class="img-circle responsives" style="border: 2px solid #00a7ff;" alt="" src='<%# "../" + Eval("StudentPic") %>'></a>
                                                    <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("StudentID") %>' />
                                                </span>
                                            </p>
                                            <p><%# Eval("StudentName") %>
                                                <asp:HyperLink ID="lnkReport" NavigateUrl='<%# string.Format("StudentAttendanceReport.aspx?ID={0}", HttpUtility.UrlEncode(Eval("StudentID").ToString())) %>' class="pull-right" runat="server">Attendance Report</asp:HyperLink>
                                                </p>
                                            <%--StudentAttendanceReport.aspx?ID=<%# Eval("StudentID") %>--%>
                                        </td>
                                        <td class="color_blue" style="border-bottom: 1px solid #ddd !important;"><%# Eval("Status") %></td>
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
    </section>
    <div id="filterModal" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Filter</h4>
                </div>
                <div class="modal-body" style="z-index: 9999 !important;">
                    <div class="form-group">
                        <asp:Label runat="server" for="studentname">Select Class Year</asp:Label>
                        <asp:DropDownList runat="server" class="form-control">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Class 1</asp:ListItem>
                            <asp:ListItem>Class 2</asp:ListItem>
                            <asp:ListItem>Class 3</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="">Date:</asp:Label>
                        <asp:TextBox runat="server" type="date" class="form-control" ID="TextBox1"></asp:TextBox>
                    </div>
                    <div class="well">
                        <p>Filter by:</p>
                        <div class="form-group">
                            <asp:Label runat="server" for="studentname">Student Name</asp:Label>
                            <asp:DropDownList runat="server" class="form-control">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Student name 1</asp:ListItem>
                                <asp:ListItem>Student name 2</asp:ListItem>
                                <asp:ListItem>Student name 3</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" for="classname">Teacher</asp:Label>
                            <asp:DropDownList runat="server" class="form-control">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Picker name 1</asp:ListItem>
                                <asp:ListItem>Picker name 2</asp:ListItem>
                                <asp:ListItem>Picker name 3</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" for="classname">Attendance Status </asp:Label>
                            <asp:DropDownList runat="server" class="form-control">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Present</asp:ListItem>
                                <asp:ListItem>Absent</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <hr />
                        <p>Sort by:</p>

                        <div class="form-group">
                            <label for="classname">Student Name</label>
                            <asp:DropDownList runat="server" class="form-control">
                                <asp:ListItem>Student name 1</asp:ListItem>
                                <asp:ListItem>Student name 2</asp:ListItem>
                                <asp:ListItem>Student name 3</asp:ListItem>
                                <asp:ListItem>Student name 4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
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
                        <asp:Button runat="server" class="btn btn-primary btn-block" Text="Apply"></asp:Button>

                    </div>
            </div>
        </div>
    </div>
    <!-- jQuery -->
    <script src="js/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Theme JavaScript -->
    <script src="js/default.js"></script>


</asp:Content>
