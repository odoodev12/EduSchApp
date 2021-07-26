<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="SchoolApp.Web.Teacher.Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        @media only screen and (max-width: 750px) {
            #email {
                margin-top: 10px !important;
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

        .buttons .btn {
            margin-top: 10px;
            border-radius: 1px;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 58px;
            height: 30px;
        }

            .switch input {
                display: none;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 2px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .checkbox_style {
            display: block;
            position: relative;
            padding-left: 35px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: 16px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }
            /* Hide the browser's default checkbox */

            .checkbox_style input {
                position: absolute;
                opacity: 0;
                cursor: pointer;
            }

        /* Create a custom checkbox */

        .checkmark {
            position: absolute;
            top: 0;
            left: 0;
            height: 25px;
            width: 25px;
            background-color: #eee;
        }

        /* On mouse-over, add a grey background color */

        .checkbox_style:hover input ~ .checkmark {
            background-color: #ccc;
        }

        /* When the checkbox is checked, add a blue background */

        .checkbox_style input:checked ~ .checkmark {
            background-color: #2196F3;
        }

        /* Create the checkmark/indicator (hidden when not checked) */

        .checkmark:after {
            content: "";
            position: absolute;
            display: none;
        }

        /* Show the checkmark when checked */

        .checkbox_style input:checked ~ .checkmark:after {
            display: block;
        }

        /* Style the checkmark/indicator */

        .checkbox_style .checkmark:after {
            left: 9px;
            top: 5px;
            width: 5px;
            height: 10px;
            border: solid white;
            border-width: 0 3px 3px 0;
            -webkit-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            transform: rotate(45deg);
        }

        .responsives {
            width: 100px !important;
            max-height: 100px !important;
            max-width: 100px !important;
            height: 100px !important;
        }

    </style>
    <script>
        function SetID(ID) {
            $("#HiddenField1").val(ID);
        }
       <%-- $("#HiddenField1").val("<%# Eval("HiddenField1.ClientID")%>")--%>
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading"><asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> Teacher</asp:HyperLink> / <span>Attendance</span></h1>
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
                            <h3 style="margin-top: 0px;">Select Class</h3>
                            <div class="sidebar-box">
                                <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <hr />
                            <div class="form-group">
                                <h3 style="margin-top: 0px;">Date</h3>
                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                            </div>
                            <br />
                            <div class="well">
                                <p>Filter by:</p>
                                <div>
                                    <div class="form-group">
                                        <label for="classyear">Student Name</label>                                        
                                        <asp:DropDownList ID="drpStudent" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpStudent_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div>
                                        <%--<center><asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>--%>
                                    </div>
                                </div>
                                <hr />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnViewAttendence" runat="server" class="btn btn-primary btn-block" Text="View Daily Attendance Report" OnClick="btnViewAttendence_Click"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" class="btn btn-primary btn-block" Text="Export to Excel" OnClick="btnExport_Click"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="email" class="btn btn-primary btn-block" Text="Export to Email" OnClick="email_Click"></asp:Button>
                            </div>
                            
                        </div>
                        <div class="col-md-9">
                            <div class="well">
                                <h4>Complete Attendance Run
                               <label class="switch pull-right">
                                   <asp:CheckBox ID="chkPresent" runat="server" OnCheckedChanged="chkPresent_CheckedChanged" AutoPostBack="true" />
                                   <span class="slider round"></span>
                               </label>
                                </h4>
                            </div>
                            <div class="row">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:ListView ID="lstAttendance" runat="server" OnPagePropertiesChanging="lstAttendance_PagePropertiesChanging" OnItemDataBound="lstAttendance_ItemDataBound" OnItemCommand="lstAttendance_ItemCommand">
                                    <GroupTemplate>
                                        <div runat="server" id="itemPlaceholderContainer">
                                            <div runat="server" id="itemPlaceholder"></div>
                                        </div>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <div class="row">

                                                    <div class="col-md-3 col-xs-4">
                                                        <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                            <img class="img-circle responsives" src='<%# "../" + Eval("Photo") %>' alt="" /></a>

                                                        <%--<img src='<%# "../" + Eval("Photo") %>' class="img-circle" width="100%" />--%>
                                                    </div>
                                                    <div class="col-md-9 col-xs-8" style="padding-left: 25px;">
                                                        <h3><%# Eval("StudentName") %></h3>
                                                        <asp:Label ID="StylistIdlbl" runat="server" Visible="false" Text='<%#Eval("ID")%>'></asp:Label>

                                                        <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                            <%--<label class="checkbox_style" style="float: left;">--%>
                                                                Present
												            <%--<asp:CheckBox ID="chkPresent" runat="server" />--%>
                                                            <asp:RadioButton ID="rbtnPresent" runat="server" GroupName="Attendance" AutoPostBack="true" ToolTip='<%#Eval("ID")%>' OnCheckedChanged="rbtnPresent_CheckedChanged" />
                                                            <%-- <span class="checkmark"></span>
                                                                <%--<asp:Label ID="StylistIdlbl" runat="server" Visible="false" Text='<%#Eval("Status")%>'></asp:Label>--%>
                                                            <%--</label>--%>
                                                        </div>
                                                        <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                            Absent
                                                            <asp:RadioButton ID="rbtnAbsent" runat="server" GroupName="Attendance" OnCheckedChanged="rbtnAbsent_CheckedChanged" AutoPostBack="true" ToolTip='<%#Eval("ID")%>' />
                                                            <%-- <label class="checkbox_style">
                                                                Absent
												   <%--<asp:CheckBox ID="chkAbsent" runat="server" />
                                                                <asp:RadioButton ID="rbtnAbsent" runat="server" GroupName="Attendance" />
                                                                <span class="checkmark"></span>
                                                            </label>--%>
                                                        </div>
                                                        <div class="col-md-12" style="padding: 0px;">
                                                            <br />
                                                            <div class="">
                                                                <asp:LinkButton ID="lbtnSend" runat="server" class="btn btn-primary" CommandName="SendStatus" CommandArgument='<%# Eval("ID") %>'><span class="fa fa-envelope"></span>Send Status</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <table runat="server" class="col-md-12">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table runat="server" id="groupPlaceholderContainer" border="0">
                                                        <tr runat="server" id="groupPlaceholder"></tr>
                                                    </table>
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
                                <%--   <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-4">
                                                <img src="img/student_profile.jpg" class="img-circle" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-8" style="padding-left: 0px;">
                                                <h3>Student Name 1</h3>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style" style="float: left;">
                                                        Present
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style">
                                                        Absent
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-12" style="padding: 0px;">
                                                    <div class="">
                                                        <button href="teacher_pickers.html" class="btn btn-primary"><span class="fa fa-envelope"></span>Send Status</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-4">
                                                <img src="img/student_profile.jpg" class="img-circle" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-8" style="padding-left: 0px;">
                                                <h3>Student Name 1</h3>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style" style="float: left;">
                                                        Present
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style">
                                                        Absent
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-12" style="padding: 0px;">
                                                    <div class="">
                                                        <button href="teacher_pickers.html" class="btn btn-primary"><span class="fa fa-envelope"></span>Send Status</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-4">
                                                <img src="img/student_profile.jpg" class="img-circle" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-8" style="padding-left: 0px;">
                                                <h3>Student Name 1</h3>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style" style="float: left;">
                                                        Present
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style">
                                                        Absent
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-12" style="padding: 0px;">
                                                    <div class="">
                                                        <button href="teacher_pickers.html" class="btn btn-primary"><span class="fa fa-envelope"></span>Send Status</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-4">
                                                <img src="img/student_profile.jpg" class="img-circle" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-8" style="padding-left: 0px;">
                                                <h3>Student Name 1</h3>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style" style="float: left;">
                                                        Present
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style">
                                                        Absent
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-12" style="padding: 0px;">
                                                    <div class="">
                                                        <button href="teacher_pickers.html" class="btn btn-primary"><span class="fa fa-envelope"></span>Send Status</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-4">
                                                <img src="img/student_profile.jpg" class="img-circle" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-8" style="padding-left: 0px;">
                                                <h3>Student Name 1</h3>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style" style="float: left;">
                                                        Present
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style">
                                                        Absent
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-12" style="padding: 0px;">
                                                    <div class="">
                                                        <button href="teacher_pickers.html" class="btn btn-primary"><span class="fa fa-envelope"></span>Send Status</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-4">
                                                <img src="img/student_profile.jpg" class="img-circle" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-8" style="padding-left: 0px;">
                                                <h3>Student Name 1</h3>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style" style="float: left;">
                                                        Present
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style">
                                                        Absent
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-12" style="padding: 0px;">
                                                    <div class="">
                                                        <button href="teacher_pickers.html" class="btn btn-primary"><span class="fa fa-envelope"></span>Send Status</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-2 col-xs-4">
                                                <img src="img/student_profile.jpg" class="img-circle" width="100%" />
                                            </div>
                                            <div class="col-md-10 col-xs-8" style="padding-left: 0px;">
                                                <h3>Student Name 1</h3>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style" style="float: left;">
                                                        Present
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-4 col-xs-8" style="padding: 0px;">
                                                    <label class="checkbox_style">
                                                        Absent
												  <input checked="checked" type="checkbox">
                                                        <span class="checkmark"></span>
                                                        <div class="bot-border"></div>
                                                    </label>
                                                </div>
                                                <div class="col-md-12" style="padding: 0px;">
                                                    <div class="">
                                                        <button href="teacher_pickers.html" class="btn btn-primary"><span class="fa fa-envelope"></span>Send Status</button>
                                                    </div>
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
