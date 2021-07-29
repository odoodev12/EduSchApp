<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NonTeacher.aspx.cs" Inherits="SchoolApp.Web.School.NonTeacher" %>

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
                            / <span>Non-Teacher</span></h1>
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
                            <asp:LinkButton ID="lbtnAddTeacher" runat="server" class="btn btn-primary btn-block" Style="border: none; padding: 10px;" data-toggle="modal" data-target="#add_teacher"><i class="fa fa-plus"></i> Add Non-Teacher</asp:LinkButton>
                            <hr />

                            <div class="well">
                                <p>Filter by:</p>
                                <div>
                                    <div class="form-group">
                                        <label for="classyear">Non Teacher Name</label>
                                        <asp:TextBox ID="txtTeacherName" runat="server" class="form-control" placeholder="Enter Non-Teacher name" />
                                        <ajax:AutoCompleteExtender ID="AutoCompleteNonTeachers" runat="server" TargetControlID="txtTeacherName"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetNonTeachers">
                                        </ajax:AutoCompleteExtender>
                                    </div>
                                    <div class="form-group">
                                        <label for="classyear">Non Teacher Status</label>
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
                                    <center><asp:Button runat="server" Text="Apply" class="btn btn-primary" UseSubmitBehavior="false" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
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
                                                    </div>
                                                    <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                        <h3><%# Eval("Name") %></h3>

                                                        <p style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><b>Non-TeacherNo:</b> <%# Eval("TeacherNo") %> </p>
                                                        <p><b>Role:</b> <%# Eval("RoleName") %></p>
                                                        <p>
                                                            <b>Status:</b> <%# Eval("Status") %> <span class="pull-right">
                                                                <asp:HyperLink ID="hlink1" runat="server" Style="font-size: 15px;" NavigateUrl='<%# "NonTeacherProfile.aspx?ID="+Eval("ID") %>'>View Detail</asp:HyperLink></span>
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
                    <h3 class="modal-title" id="lineModalLabel">Add Non-Teacher</h3>
                </div>
                <div class="modal-body">
                    <!-- content goes here -->
                    <div>
                        <div class="form-group">
                            <label for="classname">Non-Teacher Title</label>
                            <asp:DropDownList ID="ddlTeacherTitle" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0" Text="Select Title"></asp:ListItem>
                                <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                                <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                                <asp:ListItem Value="Miss." Text="Miss."></asp:ListItem>
                                <asp:ListItem Value="Ms." Text="Ms."></asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" ValidationGroup="Submit" ControlToValidate="ddlTeacherTitle" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group">
                            <label for="classname">Non-Teacher No*</label>
                            <asp:TextBox ID="txtIdNo" runat="server" class="form-control" placeholder="" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="txtIdNo" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Name*</label>
                            <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">First Class</label>
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

                        <div class="form-group" id="CThrid" style="display: none">
                            <label for="classname">Third Class</label>
                            <asp:DropDownList ID="ddlClass3" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <i class='fa fa-minus' onclick="removeDropDownList('CThrid')" style='border: 1px solid gray; padding: 10px; border-radius: 50%; text-align: center;'></i>
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
                            <div class="col-md-1" style="padding-left: 4px !important;">
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
                                ID="valDateRanges"
                                ControlToValidate="txtEndDate"
                                OnServerValidate="valDateRanges_ServerValidate"
                                ErrorMessage="End-Date cannot be a date in the past" />--%>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="btnCancle" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="buttonAdd" runat="server" OnClientClick="return showvalss();" ValidationGroup="Submit" class="btn btn-primary" Text="Add" OnClick="buttonAdd_Click" />
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
                                    <asp:Label ID="litThridClass" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddl3rdClass" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="Button1" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnAddClassAssign" runat="server" OnClientClick="return showval23();" class="btn btn-primary" Text="Assign Class" ValidationGroup="Save" OnClick="btnAddClassAssign_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
    <script src='<%= Page.ResolveClientUrl("~/js/jquery.min.js")%>' type="text/javascript"></script>
    <script>
        function SetID(ID) {
            $("#MainContent_HID").val(ID);
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
    </script>
    <script type="text/javascript">
        function showvalss() {
            if (Page_ClientValidate("Submit")) {
                var ddlclass1 = document.getElementById("MainContent_ddlClass1");
                var ddlclass2 = document.getElementById("MainContent_ddlClass2");

                var ddlclass1Value = ddlclass1.options[ddlclass1.selectedIndex].value;
                var ddlclass2Value = ddlclass2.options[ddlclass2.selectedIndex].value;
                if (ddlclass1Value != "0") {
                    if (ddlclass1Value == ddlclass2Value) {
                        alert("Class already added, please select a different one");
                        return false;
                    }
                }
                if (ddlclass2Value != "0") {
                    if (ddlclass2Value == ddlclass1Value) {
                        alert("Class already added, please select a different one");
                        return false;
                    }
                }
            }
            else {
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        function showval23() {
            var ddlclass1 = document.getElementById("MainContent_ddl1stClass");
            var ddlclass2 = document.getElementById("MainContent_ddl2ndClass");

            var ddlclass1Value = ddlclass1.options[ddlclass1.selectedIndex].value;
            var ddlclass2Value = ddlclass2.options[ddlclass2.selectedIndex].value;
            if (ddlclass1Value != "0") {
                if (ddlclass1Value == ddlclass2Value) {
                    alert("Class already added, please select a different one");
                    return false;
                }
            }
            if (ddlclass2Value != "0") {
                if (ddlclass2Value == ddlclass1Value) {
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

            if (objID == 'CThrid') {
                $("#<%=ddlClass3.ClientID%>").val("0");
            }
            $('#' + objID).hide();

            

        };

        function AddDropDownList() {
            if ($('#CSecond:visible').length == 0) {

                $('#CSecond').show();
                return;
            }

             if ($('#CThrid:visible').length == 0) {

                $('#CThrid').show();
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
                url: "NonTeacher.aspx/AssignClasses",
                data: JSON.stringify({ ID: $("#MainContent_HID").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data)           //what to do if succedded
                {
                    var person = $.parseJSON(data.d);
                    $('#MainContent_litFirstClass').text(person.FirstClass);
                    $('#MainContent_litSecondClass').text(person.SecondClass);
                    setDropDownList(document.getElementById('MainContent_ddl1stClass'), person.FirstClassID);

                    $('#MainContent_ddl2ndClass').show();

                    if (person.SecondClass == "") {
                        $('#MainContent_ddl2ndClass').hide();
                    }
                    else {
                        $('#MainContent_ddl2ndClass').val(person.SecondClassID);
                    }
                }
            });
        }
    </script>
</asp:Content>
