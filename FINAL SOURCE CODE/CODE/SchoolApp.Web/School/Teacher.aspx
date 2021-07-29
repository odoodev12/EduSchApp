<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Teacher.aspx.cs" Inherits="SchoolApp.Web.School.Teacher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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

        .buttons .btn {
            margin-top: 10px;
            border-radius: 1px;
        }

        .responsives {
            width: 100px !important;
            max-height: 100px !important;
            max-width: 100px !important;
            height: 100px !important;
        }
    </style>
    <style type="text/css">
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

        .responsive {
            width: 100px !important;
            max-width: 100px !important;
            height: 100px !important;
            max-height: 100px !important;
        }
        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
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
                            / <span>Teacher</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <asp:HiddenField ID="HID" runat="server" />
        <div class="container" id="features">
            <div class="sidebar-section">
                <div class="container">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:LinkButton ID="lbtnAddTeacher" runat="server" class="btn btn-primary btn-block" Style="border: none; padding: 10px;" data-toggle="modal" data-target="#add_teacher"><i class="fa fa-plus"></i> Add Teacher</asp:LinkButton>
                            <hr />
                            <asp:Panel ID="PanelAfterSchool" runat="server">

                                <h3 style="margin-top: 0px;">Select Class Type</h3>
                                <div class="sidebar-box">
                                    <asp:DropDownList ID="ddlClassType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClassType_SelectedIndexChanged" class="form-control" data-style="btn-primary" Style="width: 100%;">
                                    </asp:DropDownList>

                                </div>

                                <h3 style="margin-top: 0px;">Select Class Year</h3>
                                <div class="sidebar-box">
                                    <asp:DropDownList ID="ddlClassYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectClassYear_SelectedIndexChanged" class="form-control" data-style="btn-primary" Style="width: 100%;">
                                    </asp:DropDownList>

                                </div>
                                <hr />
                            </asp:Panel>
                            <div class="well">
                                <p>Filter by:</p>
                                <div>
                                    <div class="form-group">
                                        <label for="classyear">Class name</label>
                                        <asp:DropDownList ID="ddlClassName" runat="server" class="form-control">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group">
                                        <label for="classyear">Teacher name</label>
                                        <asp:TextBox ID="txtTeacherName" runat="server"  class="form-control" placeholder="Enter teacher name" />
                                        <ajax:AutoCompleteExtender ID="AutoCompleteTeachers" runat="server" TargetControlID="txtTeacherName"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetTeachers">
                                        </ajax:AutoCompleteExtender>
                                    </div>
                                    <div class="form-group">
                                        <label for="classyear">Teacher Status</label>
                                        <asp:DropDownList ID="drpStatus" runat="server" class="form-control">
                                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="InActive" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <p>Sort by:</p>
                                <div>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlShortBy" runat="server" class="form-control">
                                            <asp:ListItem Value="Date" Text="Date" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="IdNo" Text="IdNo"></asp:ListItem>
                                            <asp:ListItem Value="TeacherName" Text="TeacherName"></asp:ListItem>
                                            <asp:ListItem Value="RoleName" Text="RoleName"></asp:ListItem>
                                        </asp:DropDownList>

                                        <div style="margin-top: 10px;">
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbtnAscending" runat="server" GroupName="optradio" Text="Ascending" />
                                            </label>
                                            </br>
                                           
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbtndescending" runat="server" GroupName="optradio" Text="Descending" Checked="true" />

                                            </label>
                                        </div>
                                    </div>
                                    <center><asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <asp:ListView runat="server" ID="lstTeachers">
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("Name") %>'>
                                                            <img class="img-circle responsives" style="border: 2px solid #00a7ff;" alt="" src='<%# "../" + Eval("Photo") %>'></a>
                                                        <%--<asp:Image ID="Image9" runat="server" ImageUrl='<%# "../" + Eval("Photo") %>' class="img-circle" Width="100%" />--%>
                                                        <p class="text-center">Teacher No:</p>
                                                        <p class="text-center"><%# Eval("TeacherNo") %></p>
                                                    </div>
                                                    <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                        <h3><%# Eval("Name") %></h3>

                                                        <p data-toggle="tooltip" title='<%# Eval("AssignedClass") %>' style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><b>Class:</b> <%# Eval("AssignedClass") %> </p>
                                                        <p><b>Role:</b> <%# Eval("RoleName") %></p>
                                                        <p>
                                                            <b>Status:</b> <%# Eval("Status") %> <span class="pull-right">
                                                                <asp:HyperLink ID="hlink1" runat="server" Style="font-size: 15px;" NavigateUrl='<%# "TeacherProfile.aspx?ID="+Eval("ID") %>'>View Detail</asp:HyperLink></span>
                                                        </p>
                                                        <div class="buttons">
                                                            <asp:LinkButton ID="lbtn1" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#reassign_class" OnClientClick='<%# "return SetID("+Eval("ID")+")" %>'>Re-assign Class</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <div class="modal fade" id="add_teacher" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Add Teacher</h3>
                </div>
                <div class="modal-body">
                    <!-- content goes here -->
                    <div>
                        <div class="form-group">
                            <label for="classname">Teacher Title</label>
                            <asp:DropDownList ID="ddlTeacherTitle" runat="server" CssClass="form-control">
                                <asp:ListItem Value="" Text="Select Title"></asp:ListItem>
                                <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                                <asp:ListItem Value="Dr." Text="Dr."></asp:ListItem>
                                <asp:ListItem Value="Master." Text="Master."></asp:ListItem>
                                <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                                <asp:ListItem Value="Miss." Text="Miss."></asp:ListItem>
                                <asp:ListItem Value="Ms." Text="Ms."></asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" ValidationGroup="Submit" ControlToValidate="ddlTeacherTitle" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group">
                            <label for="classname">Teacher No*</label>
                            <asp:TextBox ID="txtIdNo" runat="server" class="form-control" placeholder="" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="txtIdNo" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Name*</label>
                            <asp:TextBox ID="txtName" runat="server" onkeyup="this.value=TitleCase(this);" class="form-control" placeholder="" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label for="classname">First Class*</label>
                            <asp:DropDownList ID="ddlClass1" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <i class='fa fa-plus' id="btnAdd" onclick="AddDropDownList()" style='border: 1px solid gray; padding: 10px; border-radius: 50%; text-align: center;'></i>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlClass1" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>


                        <div class="form-group" id="CSecond" style="display: none">
                            <label for="classname">Second Class</label>
                            <asp:DropDownList ID="ddlClass2" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <i class='fa fa-minus' onclick="removeDropDownList('CSecond')" style='border: 1px solid gray; padding: 10px; border-radius: 50%; text-align: center;'></i>
                        </div>

                        <div class="form-group" id="CThird" style="display: none">
                            <label for="classname">Third Class</label>
                            <asp:DropDownList ID="ddlClass3" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <i class='fa fa-minus' onclick="removeDropDownList('CThird')" style='border: 1px solid gray; padding: 10px; border-radius: 50%; text-align: center;'></i>
                        </div>

                        <div class="form-group" id="CForth" style="display: none">
                            <label for="classname">Fourth Class</label>
                            <asp:DropDownList ID="ddlClass4" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <i class='fa fa-minus' onclick="removeDropDownList('CForth')" style='border: 1px solid gray; padding: 10px; border-radius: 50%; text-align: center;'></i>
                        </div>

                        <div class="form-group" id="CFifth" style="display: none">
                            <label for="classname">Fifth Class</label>
                            <asp:DropDownList ID="ddlClass5" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <i class='fa fa-minus' onclick="removeDropDownList('CFifth')" style='border: 1px solid gray; padding: 10px; border-radius: 50%; text-align: center;'></i>
                        </div>










                        <div class="form-group">
                            <label for="classname">Role*</label>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlRole" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>

                        </div>
                        <div class="form-group">
                            <label for="classname">Email*</label>
                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="" />
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator7" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtEmail" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="Submit" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'>Enter Valid Email Address</a>"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Phone Number*</label>
                            <asp:TextBox ID="txtPhoneNo" runat="server" class="form-control" placeholder="" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers, Custom" ValidChars="+" TargetControlID="txtPhoneNo" />
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator8" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtPhoneNo" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <%--<div class="form-group">
                            <div class="col-md-1" style="padding-left:4px !important;">
                                <label for="classyear">Active</label>
                            </div>
                            <label class="switch" style="margin-left: 20px !important;">
                                <asp:CheckBox ID="ChkActive" runat="server" />
                                <span class="slider round"></span>
                            </label>
                        </div>--%>

                        <%--<label for="classname">End Date</label>
                            <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" class="form-control" placeholder="" />
                            <asp:CustomValidator runat="server"
                                ID="valDateRange"
                                ControlToValidate="txtEndDate"
                                OnServerValidate="valDateRange_ServerValidate"
                                ErrorMessage="End-Date cannot be a date in the past" />--%>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="btnCancle" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="buttonAdd" runat="server" ValidationGroup="Submit" OnClientClick="return showval()" class="btn btn-primary" Text="Add" OnClick="buttonAdd_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="reassign_class" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Reassign Class</h3>
                </div>
                <div class="modal-body">

                    <!-- content goes here -->
                    <div>
                        <div class="row">
                            <div class="col-md-4 col-xs-6">
                                <label for="leveltransfer">Class From:</label>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <label for="leveltransfer">Class To:</label>
                            </div>
                        </div>
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                        <div class="row">
                            <div class="col-md-4 col-xs-6">
                                <p data-toggle="tooltip" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                    <asp:Label ID="litFirstClass" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group" style="margin-bottom: 2px !important;">
                                    <asp:DropDownList ID="ddl1stClass" runat="server" class="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" InitialValue="0" ValidationGroup="Save" ControlToValidate="ddl1stClass" runat="server" ErrorMessage="<a class='ourtooltip'>*</a>"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4 col-xs-6">
                                <p data-toggle="tooltip" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                    <asp:Label ID="litSecondClass" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddl2ndClass" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4 col-xs-6">
                                <p data-toggle="tooltip" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                    <asp:Label ID="litThirdClass" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddl3rdClass" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4 col-xs-6">
                                <p data-toggle="tooltip" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                    <asp:Label ID="litFourthClass" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddl4thClass" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4 col-xs-6">
                                <p data-toggle="tooltip" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                    <asp:Label ID="litFifthClass" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddl5thClass" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="Button1" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnAddClassAssign" runat="server" OnClientClick="return showval2()" class="btn btn-primary" Text="Assign Class" ValidationGroup="Save" OnClick="btnAddClassAssign_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function showval() {
            //if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
            //    alert('false');

            //    return false;
            //}
            //else {
            //    alert('true');

            //    return true;
            //}
            if (Page_ClientValidate("Submit")) {
                var ddlclass1 = document.getElementById("MainContent_ddlClass1");
                var ddlclass2 = document.getElementById("MainContent_ddlClass2");
                var ddlclass3 = document.getElementById("MainContent_ddlClass3");
                var ddlclass4 = document.getElementById("MainContent_ddlClass4");
                var ddlclass5 = document.getElementById("MainContent_ddlClass5");

                var ddlclass1Value = ddlclass1.options[ddlclass1.selectedIndex].value;
                var ddlclass2Value = ddlclass2.options[ddlclass2.selectedIndex].value;
                var ddlclass3Value = ddlclass3.options[ddlclass3.selectedIndex].value;
                var ddlclass4Value = ddlclass4.options[ddlclass4.selectedIndex].value;
                var ddlclass5Value = ddlclass5.options[ddlclass5.selectedIndex].value;
                //var strUser = e.options[e.selectedIndex].value;
                if (ddlclass1Value != "0") {
                    if (ddlclass1Value == ddlclass2Value || ddlclass1Value == ddlclass3Value || ddlclass1Value == ddlclass4Value || ddlclass1Value == ddlclass5Value) {
                        alert("Class already added, please select a different one");
                        return false;
                    }
                }
                if (ddlclass2Value != "0") {
                    if (ddlclass2Value == ddlclass1Value || ddlclass2Value == ddlclass3Value || ddlclass2Value == ddlclass4Value || ddlclass2Value == ddlclass5Value) {
                        alert("Class already added, please select a different one");
                        return false;
                    }
                }
                if (ddlclass3Value != "0") {
                    if (ddlclass3Value == ddlclass1Value || ddlclass3Value == ddlclass2Value || ddlclass3Value == ddlclass4Value || ddlclass3Value == ddlclass5Value) {
                        alert("Class already added, please select a different one");
                        return false;
                    }
                }
                if (ddlclass4Value != "0") {
                    if (ddlclass4Value == ddlclass1Value || ddlclass4Value == ddlclass2Value || ddlclass4Value == ddlclass3Value || ddlclass4Value == ddlclass5Value) {
                        alert("Class already added, please select a different one");
                        return false;
                    }
                }
                if (ddlclass5Value != "0") {
                    if (ddlclass5Value == ddlclass1Value || ddlclass5Value == ddlclass2Value || ddlclass5Value == ddlclass3Value || ddlclass5Value == ddlclass4Value) {
                        alert("Class already added, please select a different one");
                        return false;
                    }
                }
            }
            else {
                return false;
            }
        }

        function TitleCase(objField) {
            var objValues = objField.value.split(" ");
            var outText = "";
            for (var i = 0; i < objValues.length; i++) {
                outText = outText + objValues[i].substr(0, 1).toUpperCase() + objValues[i].substr(1).toLowerCase() + ((i < objValues.length - 1) ? " " : "");
            }
            return outText;
        };

    </script>
    <script type="text/javascript">
        function TitleCase(objField) {
            var objValues = objField.value.split(" ");
            var outText = "";
            for (var i = 0; i < objValues.length; i++) {
                outText = outText + objValues[i].substr(0, 1).toUpperCase() + objValues[i].substr(1).toLowerCase() + ((i < objValues.length - 1) ? " " : "");
            }
            return outText;
        };

        function showval2() {
            var ddlclass1 = document.getElementById("MainContent_ddl1stClass");
            var ddlclass2 = document.getElementById("MainContent_ddl2ndClass");
            var ddlclass3 = document.getElementById("MainContent_ddl3rdClass");
            var ddlclass4 = document.getElementById("MainContent_ddl4thClass");
            var ddlclass5 = document.getElementById("MainContent_ddl5thClass");

            var ddlclass1Value = ddlclass1.options[ddlclass1.selectedIndex].value;
            var ddlclass2Value = ddlclass2.options[ddlclass2.selectedIndex].value;
            var ddlclass3Value = ddlclass3.options[ddlclass3.selectedIndex].value;
            var ddlclass4Value = ddlclass4.options[ddlclass4.selectedIndex].value;
            var ddlclass5Value = ddlclass5.options[ddlclass5.selectedIndex].value;
            //var strUser = e.options[e.selectedIndex].value;
            if (ddlclass1Value != "0") {
                if (ddlclass1Value == ddlclass2Value || ddlclass1Value == ddlclass3Value || ddlclass1Value == ddlclass4Value || ddlclass1Value == ddlclass5Value) {
                    alert("Class already added, please select a different one");
                    return false;
                }
            }
            if (ddlclass2Value != "0") {
                if (ddlclass2Value == ddlclass1Value || ddlclass2Value == ddlclass3Value || ddlclass2Value == ddlclass4Value || ddlclass2Value == ddlclass5Value) {
                    alert("Class already added, please select a different one");
                    return false;
                }
            }
            if (ddlclass3Value != "0") {
                if (ddlclass3Value == ddlclass1Value || ddlclass3Value == ddlclass2Value || ddlclass3Value == ddlclass4Value || ddlclass3Value == ddlclass5Value) {
                    alert("Class already added, please select a different one");
                    return false;
                }
            }
            if (ddlclass4Value != "0") {
                if (ddlclass4Value == ddlclass1Value || ddlclass4Value == ddlclass2Value || ddlclass4Value == ddlclass3Value || ddlclass4Value == ddlclass5Value) {
                    alert("Class already added, please select a different one");
                    return false;
                }
            }
            if (ddlclass5Value != "0") {
                if (ddlclass5Value == ddlclass1Value || ddlclass5Value == ddlclass2Value || ddlclass5Value == ddlclass3Value || ddlclass5Value == ddlclass4Value) {
                    alert("Class already added, please select a different one");
                    return false;
                }
            }
        }
    </script>
    <script type="text/javascript">


        function removeDropDownList(objID) {

            if (objID == 'CSecond') {
                $("#<%=ddlClass2.ClientID%>").val("0");
            }

            if (objID == 'CThird') {
                $("#<%=ddlClass3.ClientID%>").val("0");
            }

            if (objID == 'CForth') {
                $("#<%=ddlClass4.ClientID%>").val("0");
            }

            if (objID == 'CFifth') {
                $("#<%=ddlClass5.ClientID%>").val("0");
            }
            $('#' + objID).hide();

        };

        function AddDropDownList() {


            if ($('#CSecond:visible').length == 0) {

                $('#CSecond').show();
                return;
            }
            if ($('#CThird:visible').length == 0) {
                $('#CThird').show();
                return;
            }
            if ($('#CForth:visible').length == 0) {
                $('#CForth').show();
                return;
            }
            if ($('#CFifth:visible').length == 0) {
                $('#CFifth').show();
                return;
            }
        };



    </script>
    <script>
        function SetID(ID) {
            $("#MainContent_HID").val(ID);
            getData();
        }
        function setDropDownList(elementRef, valueToSetTo) {
            var isFound = false;
            for (var i = 0; i < elementRef.options.length; i++) {
                if (elementRef.options[i].value == valueToSetTo) {
                    elementRef.options[i].selected = true;
                    isFound = true;
                }
            }
        }
        function getData() {

            $.ajax({
                type: "POST",
                url: "Teacher.aspx/AssignClass",
                data: JSON.stringify({ ID: $("#MainContent_HID").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data)           //what to do if succedded
                {
                    debugger;
                    //alert(data.d);
                    var person = $.parseJSON(data.d);
                    //alert(data.d.FirstName);
                    $('#MainContent_litFirstClass').text(person.FirstClass);
                    $('#MainContent_litSecondClass').text(person.SecondClass);
                    $('#MainContent_litThirdClass').text(person.ThirdClass);
                    $('#MainContent_litFourthClass').text(person.FourthClass);
                    $('#MainContent_litFifthClass').text(person.FifthClass);
                    setDropDownList(document.getElementById('MainContent_ddl1stClass'), person.FirstClassID);

                    //$("#MainContent_ddl1stClass > option").each(function () {

                    //    console.log(this.text + ' ' + this.value);
                    //    if (this.value == valueToSetTo) {
                    //        this.options[i].selected = true;
                    //        isFound = true;
                    //    }
                    //    //Add operations here

                    //});


                    $('#MainContent_ddl2ndClass').show();
                    $('#MainContent_ddl3rdClass').show();
                    $('#MainContent_ddl4thClass').show();
                    $('#MainContent_ddl5thClass').show();


                    if (person.SecondClass == "") {
                        $('#MainContent_ddl2ndClass').hide();
                    }
                    else {
                        $('#MainContent_ddl2ndClass').val(person.SecondClassID);
                    }
                    if (person.ThirdClass == "") {
                        $('#MainContent_ddl3rdClass').hide();
                    }
                    else {
                        $('#MainContent_ddl3rdClass').val(person.ThirdClassID);
                    }
                    if (person.FourthClass == "") {
                        $('#MainContent_ddl4thClass').hide();
                    }
                    else {
                        $('#MainContent_ddl4thClass').val(person.FourthClassID);
                    }
                    if (person.FifthClass == "") {
                        $('#MainContent_ddl5thClass').hide();
                    }
                    else {
                        $('#MainContent_ddl5thClass').val(person.FifthClassID);
                    }

                }
            });
        }
    </script>
</asp:Content>
