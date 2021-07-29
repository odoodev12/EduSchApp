<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassDetail.aspx.cs" Inherits="SchoolApp.Web.School.ClassDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .sidebar-box {
            background: #fff;
            box-shadow: 0px 0px .5px 0px rgba(50, 50, 50, 0.95);
            margin-bottom: 20px;
        }

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
            margin: 5px 0 5px 0;
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
    </style>
    <style type="text/css">
        @media screen and (-webkit-min-device-pixel-ratio:0) {
            div#completionList {
                width: auto !important;
                top: 100px !important;
            }
        }

        .completionList {
            font-size: 14px;
            color: #000;
            padding: 3px 5px;
            border: 1px solid #999;
            background: #fff;
            width: auto;
            float: left;
            z-index: 9999999999 !important;
            position: absolute;
            margin-left: 0px;
            list-style: none;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #ADD6FF;
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

        #MainContent_rdNonListed {
            margin-left: 25px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
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
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            <%} %> /
                            <asp:HyperLink ID="hlinkClass" runat="server" NavigateUrl="Class.aspx" Text="Class"></asp:HyperLink>
                            / <span>
                                <asp:Literal ID="litClassName1" runat="server"></asp:Literal></span></h1>
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
                            <div class="class-box feature text-center">
                                <asp:Image ID="Image1" runat="server" ImageUrl="../img/icon_class.png" />
                                <h3>
                                    <asp:Literal ID="litclassnameleft" runat="server"></asp:Literal></h3>
                                <p>
                                    <asp:Literal ID="litstudentCount" runat="server"></asp:Literal>-Students
                                </p>
                            </div>
                        </div>
                        <div class="col-md-9">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-5 text-left">
                                                <div class=" col-xs-12 tital">Class Name:</div>
                                                <div class=" col-xs-12 ">
                                                    <asp:Literal ID="litclassname" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-xs-12 tital ">Class Year:</div>
                                                <div class="col-xs-12">
                                                    <asp:Literal ID="litClassYear" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-xs-12 tital ">Class Type:</div>
                                                <div class="col-xs-12">
                                                    <asp:Literal ID="litClassType" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-xs-12 tital ">After School Type:</div>
                                                <div class="col-xs-12">
                                                    <asp:Literal ID="litafterschooltype" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>
                                            </div>
                                            <div class="col-md-7 text-left" id="divExternal" data-toggle="tooltip" runat="server">
                                                <div class=" col-xs-12 tital">After School External Organisation:</div>
                                                <div class=" col-xs-12" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                                    <asp:Literal ID="litexrternal" runat="server"></asp:Literal>
                                                </div>

                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-xs-12 tital ">Class Teacher:</div>
                                                <div class="col-xs-12">
                                                    <asp:Literal ID="litTeacher" runat="server"></asp:Literal><%--Teacher Name 1, Teacher Name 2, Teacher Name 3, Teacher Name 4--%>
                                                </div>

                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-xs-12 tital ">Status:</div>
                                                <div class="col-xs-12">
                                                    <asp:Literal ID="litStatus" runat="server"></asp:Literal><%--Teacher Name 1, Teacher Name 2, Teacher Name 3, Teacher Name 4--%>
                                                </div>

                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>
                                                <div class="col-xs-12 col-md-6 tital">
                                                    <asp:LinkButton ID="lbtnViewStudent" runat="server" class="btn btn-success btn-block" Style="background: #232323;" OnClick="lbtnViewStudent_Click">View Student <span class="fa fa-arrow-right"></span></asp:LinkButton>
                                                </div>
                                                <div class="col-xs-12 col-md-6 tital">
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" class="btn btn-primary btn-block" data-toggle="modal" data-target="#editclassmodel"><span class="fa fa-edit"></span> Edit</asp:LinkButton>
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
        </div>
    </section>
    <div class="modal fade" id="editclassmodel" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Edit Class</h3>
                </div>
                <div class="modal-body">

                    <!-- content goes here -->
                    <div id="fm2">
                        <div class="form-group">
                            <label for="classname">Class Teacher</label><br />
                            <p class="">
                                <asp:Label ID="lblClassTeachers" runat="server"></asp:Label>
                            </p>

                        </div>
                        <div class="form-group">
                            <label for="classname">Class Name</label>
                            <asp:TextBox ID="txtClassName" runat="server" class="form-control" placeholder="Class name" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="txtClassName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classyear">Class Type</label>
                            <asp:DropDownList ID="ddlClassType" runat="server" class="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlClassType" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group" id="classyears">
                            <label for="classyear">Class Year</label>
                            <asp:DropDownList ID="ddlClassYear" runat="server" class="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="" ValidationGroup="Submit" ControlToValidate="ddlClassYear" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group" id="afterschooltype">
                            <label for="classyear">After School Type</label>
                            <asp:DropDownList ID="ddlAfterSchoolType" runat="server" class="form-control">
                                <asp:ListItem Value="Internal" Text="Internal"></asp:ListItem>
                                <asp:ListItem Value="External" Text="External"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group" id="external">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <%--<div class="col-md-12" style="padding-left: 197px; padding-right: 9px !important;">
                                        <asp:RadioButton ID="rdListed" Checked="true" GroupName="rdList" runat="server" OnCheckedChanged="rdListed_CheckedChanged" AutoPostBack="true" Text="Listed" />
                                        <asp:RadioButton ID="rdNonListed" GroupName="rdList" runat="server" Text="NonListed" OnCheckedChanged="rdNonListed_CheckedChanged" AutoPostBack="true" />
                                    </div>--%>
                                    <label for="classyear">After School External Organisation</label>
                                    <div class="col-md-12" style="padding-left: 0px !important; padding-right: 9px !important;">
                                        <asp:TextBox ID="txtOrganization" runat="server" class="form-control" placeholder="After School Organisation" />
                                        <ajax:AutoCompleteExtender ID="AutoCompleteTeachers" runat="server" TargetControlID="txtOrganization"
                                            CompletionListCssClass="completionList"
                                            CompletionListElementID="completionList"
                                            CompletionListItemCssClass="listItem"
                                            CompletionListHighlightedItemCssClass="itemHighlighted"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetOrganisation">
                                        </ajax:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOrganization" ValidationGroup="Submit" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <br />
                        <div class="form-group">
                            <div class="col-md-12" style="padding-left: 0px !important;">
                                <div class="col-md-1" style="padding-left: 0px !important;">
                                    <label for="classyear">Active</label>
                                </div>
                                <label class="switch" style="margin-left: 20px !important;">
                                    <asp:CheckBox ID="ChkActive" runat="server" />
                                    <span class="slider round"></span>
                                </label>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="btnCancle" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnUpdate" runat="server" ValidationGroup="Submit" class="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" UseSubmitBehavior="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <script type="text/javascript">
        $('#MainContent_ddlClassType').change(function () {
            $("#classyears").show();
            $("#afterschooltype").show();
            $("#external").show();
            if (this.value == "1") {
                //$("#classyears").hide();
                $("#afterschooltype").hide();
                $("#afterschooltype").val("Internal");
                $("#external").hide();
                document.getElementById("<%=RequiredFieldValidator2.ClientID%>").enabled = true;
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").style.visibility = "hidden";
                document.getElementById("classyears").style.visibility = "visible";
                document.getElementById("external").style.visibility = "hidden";
            }
            else if (this.value == "3") {
                debugger;
                $("#classyears").hide();
                document.getElementById("<%=RequiredFieldValidator2.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator2.ClientID%>").style.visibility = "hidden";
                var items = $('#MainContent_ddlAfterSchoolType').val();
                if (items == "Internal") {
                    $("#external").hide();
                    document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = false;
                    document.getElementById("<%=RequiredFieldValidator4.ClientID%>").style.visibility = "hidden";
                }
                else {
                    $("#external").show();
                    document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = true;
                    document.getElementById("external").style.visibility = "visible";
                }
            }
            else {
                $("#classyears").hide();
                $("#afterschooltype").hide();
                $("#external").hide();
            }
        });


        $('#MainContent_ddlAfterSchoolType').change(function () {
            if (this.value == "External") {
                $("#external").show();
                document.getElementById("external").style.visibility = "visible";
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = true;
                document.getElementById("<%=RequiredFieldValidator2.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator2.ClientID%>").style.visibility = "hidden";
            }

            else {

                $("#external").hide();
                document.getElementById("external").style.visibility = "hidden";
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").style.visibility = "hidden";
            }
        });
    </script>
    <script type="text/javascript">
        function ShowMessage() {
            debugger;
            var item = $('#MainContent_ddlClassType').val();
            if (item == "1") {
                $("#classyears").show();
                $("#afterschooltype").hide();
                $("#external").hide();
                $("#afterschooltype").val("Internal");
                document.getElementById("<%=RequiredFieldValidator2.ClientID%>").enabled = true;

                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").style.visibility = "hidden";
            }
            else if (item == "3") {
                $("#classyears").hide();
                document.getElementById("classyears").style.visibility = "hidden";
                //$("#external").hide();
                $("#afterschooltype").show();
                $("#external").show();
                //$("#external").hide();
                $("#classyears").hide();
                var item = $('#MainContent_ddlAfterSchoolType').val();
                if (item == "External") {
                    $("#external").show();
                    document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = true;
                    document.getElementById("<%=RequiredFieldValidator2.ClientID%>").enabled = false;
                    document.getElementById("<%=RequiredFieldValidator2.ClientID%>").style.visibility = "hidden";
                }
                else {
                    $("#external").hide();
                    $("#afterschooltype").val("Internal");
                    document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = false;
                    document.getElementById("<%=RequiredFieldValidator4.ClientID%>").style.visibility = "hidden";
                    document.getElementById("<%=RequiredFieldValidator2.ClientID%>").enabled = false;
                    document.getElementById("<%=RequiredFieldValidator2.ClientID%>").style.visibility = "hidden";
                }
            }
            else {
                $("#classyears").hide();
                $("#afterschooltype").hide();
                $("#afterschooltype").val("Internal");
                $("#external").hide();
            }
        }
    </script>
</asp:Content>
