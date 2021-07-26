<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HolidayAdd.aspx.cs" Inherits="SchoolApp.Web.School.HolidayAdd" %>

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

        .modal-body {
            padding: 15px;
        }

        .view_detail a {
            font-size: 10px;
        }

        .bot-border {
            border-bottom: 2px #f7f7f7 solid;
            margin: 15px 0 15px 0;
        }

        /* The container */

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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="~/Teacher/Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            <%}
                                else
                                { %>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            <%} %> /
                            <asp:HyperLink ID="hlinkHolidayAdd" runat="server" NavigateUrl="HolidayList.aspx" Text="Holiday List"></asp:HyperLink>
                            / <span>
                                <% 
                                    if (Request.QueryString["ID"] != null)
                                    { %>
                                        View Holiday
                                <% }
                                    else
                                    {
                                %>
                                    Add Holiday
                                <% } %>

                            </span></h1>
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

                        <div class="col-md-12">
                            <div class="row">

                                <div class="class-box feature">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Holiday Name</label>
                                                <%--<asp:TextBox ID="txtHolidayName" runat="server" class="form-control" placeholder="Holiday Name" />--%>
                                                <asp:TextBox ID="txtHolidayNames" runat="server" class="form-control" placeholder="Holiday Name" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save" ControlToValidate="txtHolidayNames" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Holiday From Date</label>
                                                <asp:TextBox ID="txtFromDate" runat="server" class="form-control" placeholder="" TextMode="Date" />
                                                <%--<ajaxToolkit:CalendarExtender ID="datedop" runat="server" TargetControlID="txtHolidayDate" Format="dd/MM/yyyy" />--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save" ControlToValidate="txtFromDate" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                <%--<asp:CustomValidator runat="server" ID="valDateRange" ValidationGroup="Save" ControlToValidate="txtFromDate"
                                                    ClientValidationFunction="ValidateDate" ErrorMessage="<a class='ourtooltip'>From Date cannot be a date in the past</a>" />--%>
                                            </div>

                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Holiday To Date</label>
                                                <asp:TextBox ID="txtToDate" runat="server" class="form-control" placeholder="" TextMode="Date" />
                                                <%--<ajaxToolkit:CalendarExtender ID="datedop" runat="server" TargetControlID="txtHolidayDate" Format="dd/MM/yyyy" />--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save" ControlToValidate="txtToDate" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                <%--<asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="Save" ControlToValidate="txtToDate"
                                                    ClientValidationFunction="ValidateDate" ErrorMessage="<a class='ourtooltip'>To Date cannot be a date in the past</a>" />--%>
                                            </div>

                                        </div>
                                        <asp:Panel ID="PanelActive" runat="server">
                                            <div class="col-md-12">
                                                <label class="checkbox_style">
                                                    Active or InActive
						  <asp:CheckBox ID="chkActive" runat="server" />
                                                    <span class="checkmark"></span>
                                                    <div class="bot-border"></div>
                                                </label>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="Save" OnClick="btnSave_Click" class="btn btn-primary" Text="Save" /> &nbsp;
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" class="btn btn-reset" Text="Cancel" />
                                        </div>
                                    </div>
                                </div>
                            </div>
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
    <script>
        function ValidateDate(sender, args) {
            var dateString = document.getElementById(sender.controltovalidate).value;
            var regex = /(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;
            if (regex.test(dateString)) {
                var parts = dateString.split("/");
                var dt = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
                args.IsValid = (dt.getDate() == parts[0] && dt.getMonth() + 1 == parts[1] && dt.getFullYear() == parts[2]);
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>
</asp:Content>
