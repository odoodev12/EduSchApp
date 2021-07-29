<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NonTeacherProfile.aspx.cs" Inherits="SchoolApp.Web.School.NonTeacherProfile" %>

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
                margin-top: 10px;
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

            .feature a {
                text-decoration: none;
            }

        .tital {
            font-size: 16px;
            font-weight: 500;
            color: #00a7ff;
        }

        .bot-border {
            border-bottom: 3px #f8f8f8 solid;
            margin: 15px 0 5px 0
        }

        .picture {
            border-radius: 8px;
        }

        .tital .btn {
            padding: 10px;
            margin-top: 20px;
        }

        .modal-body {
            padding: 15px;
        }

        .card.hovercard {
            position: relative;
            padding-top: 0;
            overflow: hidden;
            text-align: center;
            background-color: rgba(214, 224, 226, 0.2);
        }

            .card.hovercard .cardheader {
                background: url("../img/4.jpg");
                background-size: cover;
                height: 135px;
            }

            .card.hovercard .avatar {
                position: relative;
                top: -50px;
                margin-bottom: -50px;
            }

                .card.hovercard .avatar img {
                    width: 100px;
                    height: 100px;
                    max-width: 100px;
                    max-height: 100px;
                    -webkit-border-radius: 50%;
                    -moz-border-radius: 50%;
                    border-radius: 50%;
                    border: 5px solid rgba(255, 255, 255, 0.5);
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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="~/Teacher/Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            <%}
                                else
                                { %>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            <%} %> /
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="NonTeacher.aspx" Text="NonTeacher"></asp:HyperLink>
                            / <span>Profile</span></h1>

                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features" style="margin-bottom: 100px;">
            <div class="sidebar-section">
                <div class="container">
                    <div class="row">
                        <div class="col-md-3 col-sm-6">

                            <div class="card hovercard ">
                                <div class="cardheader">
                                </div>
                                <div class="avatar">
                                    <%--<asp:Image ID="Image6" runat="server" class="img-circle" width="100%" />--%>
                                    <a id="AID" runat="server" class="example-image-link" data-lightbox="example-set">
                                        <asp:Image ID="Image6" runat="server" class="img-circle responsives" Width="100%" /></a>

                                    <asp:FileUpload ID="fpUploadLogo" runat="server" CssClass="btn btn-primary" onchange="UploadFileNow()" />
                                </div>
                                <div class="info">
                                    <p>
                                        <asp:Literal ID="litTeacherName" runat="server"></asp:Literal>
                                    </p>
                                    <p>
                                        Non-Teacher No:
                                        <asp:Literal ID="litTeacherNo" runat="server"></asp:Literal>
                                    </p>
                                </div>
                                <div class="buttons">

                                    <asp:LinkButton ID="lbtnReAssignClass" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#reassign_class"> Re-assign Class </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-9">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="class-box feature" style="margin-bottom: 100px;">
                                        <div class="row">
                                            <div class="col-md-12 text-right">
                                                <h3 class="pull-left page-header">Profile Info</h3>
                                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" OnClick="LinkButton1_Click"><span class="fa fa-edit"></span> Generate Password</asp:LinkButton>&nbsp;<asp:LinkButton ID="lbtnEdit" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#editclassmodel"><span class="fa fa-edit"></span> Edit</asp:LinkButton>
                                            </div>
                                            <div class="col-md-6 text-left">
                                                <div class=" col-md-12 tital">Class:</div>
                                                <div class=" col-md-12 ">
                                                    <asp:Literal ID="litClassName" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Non-Teacher Name:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litName" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Role:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litRole" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>



                                            </div>
                                            <div class="col-md-6 text-left">

                                                <div class="col-md-12 tital">Email:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Phone number:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPhone" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Status:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litEndDate" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                            </div>

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
    <div class="modal fade" id="editclassmodel" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Edit Non-Teacher</h3>
                </div>
                <div class="modal-body">

                    <!-- content goes here -->
                    <div>

                        <div class="form-group">
                            <label for="classname">Non-Teacher Title:</label>
                            <asp:DropDownList ID="ddlTeacherTitle" runat="server" class="form-control">
                                <asp:ListItem Value="0" Text="Select Title"></asp:ListItem>
                                <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                                <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                                <asp:ListItem Value="Miss." Text="Miss."></asp:ListItem>
                                <asp:ListItem Value="Ms." Text="Ms."></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlTeacherTitle" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Non-Teacher No:</label>
                            <asp:TextBox ID="txtIdNo" runat="server" class="form-control" placeholder="10" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="txtIdNo" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Name:</label>
                            <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Mrs. Melissa robert" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">

                            <label for="classname">First Class*</label>
                            <asp:DropDownList ID="ddlClass1" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <i class='fa fa-plus' id="btnAdd" onclick="AddDropDownList()" style='border: 1px solid gray; padding: 10px; border-radius: 50%; text-align: center;'></i>
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
                        <div class="form-group">
                            <label for="classname">Role:</label>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlRole" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Email:</label>
                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" TextMode="Email" placeholder="Melissa@caludoncastle.co.uk" />
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator7" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtEmail" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="Submit" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'>Enter Valid Email Address</a>"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Phone Number:</label>
                            <asp:TextBox ID="txtPhoneNo" runat="server" class="form-control" placeholder="0000000000" />
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator8" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtPhoneNo" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <div class="col-md-1" style="padding-left:4px !important;">
                                <label for="classyear">Active</label>
                            </div>
                            <label class="switch" style="margin-left: 20px !important;">
                                <asp:CheckBox ID="ChkActive" runat="server" />
                                <span class="slider round"></span>
                            </label>
                            <%--<label for="classname">End Date:</label>
                            <asp:TextBox ID="txtEndDate" runat="server" class="form-control" TextMode="Date" placeholder="MM/dd/yyyy" />--%>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="btnCancle" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnUpdate" runat="server" ValidationGroup="Submit" OnClientClick="return showvals();"  UseSubmitBehavior="true" class="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="editpasswordmodel" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Edit Password</h3>
                </div>
                <div class="modal-body">

                    <!-- content goes here -->
                    <div>


                        <div class="form-group">
                            <label for="classname">Password:</label>
                            <asp:TextBox ID="txtpassword" runat="server" class="form-control" TextMode="Password" placeholder="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="alert  validation" runat="server" ValidationGroup="Submit1" ControlToValidate="txtpassword" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ControlToValidate="txtpassword" ValidationGroup="Submit1" ID="RegularExpressionValidator2" runat="server" ErrorMessage="<a class='ourtooltip'>Password must be 8-15 characters long with at least one numeric,</br> one alphabet and one special character.</a>" ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+}{:;'?/>.<,])(?!.*\s).*$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Confirm:</label>
                            <asp:TextBox ID="txtcpassword" runat="server" class="form-control" TextMode="Password" placeholder="Confirm Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="alert  validation" runat="server" ValidationGroup="Submit1" ControlToValidate="txtcpassword" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtpassword" ValidationGroup="Submit1" ControlToValidate="txtcpassword" ErrorMessage="<a class='ourtooltip'>Password Does not Match</a>" Display="Dynamic" />
                        </div>



                    </div>

                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="Button2" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="resetpassword" runat="server" ValidationGroup="Submit1" class="btn btn-primary" Text="Update" OnClick="resetpassword_Click" />
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
                                    <br />
                                    <asp:Literal ID="litFirstClass" runat="server"></asp:Literal>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" InitialValue="0" ValidationGroup="Save" ControlToValidate="ddl1st" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>

                                    <asp:DropDownList ID="ddl1st" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-xs-6">
                                <p data-toggle="tooltip" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                    <asp:Literal ID="litSecondClass" runat="server"></asp:Literal>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddl2nd" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-xs-6">
                                <p data-toggle="tooltip" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                    <asp:Literal ID="litThirdClass" runat="server"></asp:Literal>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddl3rd" runat="server" class="form-control">
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
                            <asp:Button ID="btnAssignClass" runat="server" OnClientClick="return showval21();" ValidationGroup="Save" class="btn btn-primary" Text="Re-Assign Class" OnClick="btnAssignClass_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            showdrops();
        });

        function removeDropDownList(objID) {
            if (objID == 'CSecond') {
                $("#<%=ddlClass2.ClientID%>").val("0");
            }
            $('#' + objID).hide();

        };
        function showdrops() {
            var ddlclass2 = document.getElementById("MainContent_ddlClass2");
            if (ddlclass2.options[ddlclass2.selectedIndex].value != "0") {
                $('#CSecond').show();
            }

            var ddlclass3 = document.getElementById("MainContent_ddlClass3");
            if (ddlclass3.options[ddlclass3.selectedIndex].value != "0") {
                $('#CThird').show();
            }
        }
        function AddDropDownList() {
            if ($('#CSecond:visible').length == 0) {
                $('#CSecond').show();
                return;
            }
            if ($('#CThird:visible').length == 0) {
                $('#CThird').show();
                return;
            }
            
        };
        
    </script>
    <script type="text/javascript">
        function showvals() {
            debugger;
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

    </script>
    <script type="text/javascript">
        function showval21() {
            debugger;
            var ddlclass1 = document.getElementById("MainContent_ddl1st");
            var ddlclass2 = document.getElementById("MainContent_ddl2nd");

            var ddlclass1Value = ddlclass1 == null ? "0" : ddlclass1.options[ddlclass1.selectedIndex].value;
            var ddlclass2Value = ddlclass2 == null ? "0" : ddlclass2.options[ddlclass2.selectedIndex].value;
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

    <script>
        function SetID(ID) {
            $("#MainContent_HID").val(ID);
        }
        function UploadFileNow() {
            var value = $("#fpUploadLogo").val();
            if (value != '') {
                $("#form1").submit();
            }
        };
    </script>
</asp:Content>
