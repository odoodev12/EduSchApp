﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminroleCreate.aspx.cs" Inherits="SchoolApp.Web.School.AdminroleCreate" %>

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
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            /
                            <asp:HyperLink ID="hlinkSupportLog" runat="server" NavigateUrl="adminrole.aspx" Text="Admin Roles"></asp:HyperLink>
                            / <span>Create</span></h1>
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
                                                <label for="classname">Role Name</label>
                                                <asp:TextBox ID="txtRollNo" runat="server" class="form-control" placeholder="" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtRollNo" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                              <%if(IsStandardSchool()){%>
                                            <div class="form-group">
                                                <label for="classname">Role Type</label>

                                                <asp:DropDownList ID="drpTeacherRole" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Select Role Type" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Teaching Role" Value="1"></asp:ListItem>
                                                  
                                                    <asp:ListItem Text="Non Teaching Role" Value="2"></asp:ListItem>
                                                 
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpTeacherRole" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>


                                            </div>
                                               <%}%>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="checkbox_style">
                                                Can Manage Class
						  <asp:CheckBox ID="chkmanageclass" runat="server" />
                                                <span class="checkmark"></span>
                                                <div class="bot-border"></div>
                                            </label>
                                            <label class="checkbox_style">
                                                Can Manage Student
						  <asp:CheckBox ID="chkmanagestudent" runat="server" />
                                                <span class="checkmark"></span>
                                                <div class="bot-border"></div>
                                            </label>
                                            <label class="checkbox_style">
                                                Can Manage Holiday
						 <asp:CheckBox ID="chkManageHoliday" runat="server" />
                                                <span class="checkmark"></span>
                                                <div class="bot-border"></div>
                                            </label>
                                            <label class="checkbox_style">
                                                Can View Account
						 <asp:CheckBox ID="chkviewaccount" runat="server" />
                                                <span class="checkmark"></span>
                                                <div class="bot-border"></div>
                                            </label>
                                        </div>

                                        <div class="col-md-6">
                                            <label class="checkbox_style">
                                                Can Manage Teacher
						  <asp:CheckBox ID="chkmanageteacher" runat="server" />
                                                <span class="checkmark"></span>
                                                <div class="bot-border"></div>
                                            </label>
                                            <% if (IsStandardSchool()) { %>
                                            <label class="checkbox_style">
                                                Can Manage NonTeacher
						  <asp:CheckBox ID="chkManageNonTeacher" runat="server" />
                                                <span class="checkmark"></span>
                                                <div class="bot-border"></div>
                                            </label>
                                            <% } %>
                                            <label class="checkbox_style">
                                                Can Access Support Log
						 <asp:CheckBox ID="chkmanagesupport" runat="server" />
                                                <span class="checkmark"></span>
                                                <div class="bot-border"></div>
                                            </label>
                                            <%--</div>
                                        <div class="col-md-6">--%>
                                            <label class="checkbox_style">
                                                Active / InActive
						  <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                                                <span class="checkmark"></span>
                                                <div class="bot-border"></div>
                                            </label>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <asp:Button ID="btnCreate" runat="server" ValidationGroup="Submit" OnClick="btnCreate_Click" class="btn btn-primary" Text="Create" />
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" class="btn btn-primary" Text="Cancel" />
                                        </div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            
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
