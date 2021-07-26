<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Class.aspx.cs" Inherits="SchoolApp.Web.School.Class" %>

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

        #MainContent_rdNonListed {
            margin-left: 25px !important;
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
    <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>

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
                                    / <span>Class</span></h1>
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
                            <h3 style="margin-top: 0px;">Select Class Status</h3>
                            <div class="sidebar-box">
                                <asp:DropDownList ID="drpClassStatus" AutoPostBack="true" OnSelectedIndexChanged="drpClassStatus_SelectedIndexChanged" runat="server" class="form-control" data-style="btn-primary">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <%if (IsStandardSchool())
                                { %>
                            <h3 style="margin-top: 0px;">Select Class Type</h3>
                            <div class="sidebar-box">
                                <asp:DropDownList ID="ddlSelectClassType" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectClassType_SelectedIndexChanged" runat="server" class="form-control" data-style="btn-primary">
                                </asp:DropDownList>

                            </div>
                            <%} %>
                            <h3 style="margin-top: 0px;">Select Class Year</h3>
                            <div class="sidebar-box">
                                <asp:DropDownList ID="ddlSelectClassYear" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectClassYear_SelectedIndexChanged" runat="server" class="form-control" data-style="btn-primary">
                                </asp:DropDownList>

                            </div>
                            <asp:LinkButton ID="lbtn" runat="server" class="btn btn-primary btn-block" Style="border: none; padding: 10px;" data-toggle="modal" data-target="#squarespaceModal"><i class="fa fa-plus"></i> Create Class</asp:LinkButton>
                        </div>
                        <div class="col-md-9">
                            <div class="row">

                                <asp:ListView runat="server" ID="lstClasses">
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "ClassDetail.aspx?ID="+Eval("ID") %>'>
                                                    <div class="row">
                                                        <div class="col-md-4 col-xs-4">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../img/icon_class.png" Width="100%" />
                                                        </div>
                                                        <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                            <h3 data-toggle="tooltip" title='<%# Eval("Name") %>'>
                                                                <div class="col-md-10 col-xs-10 no-padding" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><%# Eval("Name") %></div>
                                                                <div class="col-md-2 col-xs-2 no-padding">
                                                                    <span>
                                                                        <asp:Image ID="Image2" runat="server" class="pull-right" src="../img/icon_right_arrow_gray.png" Width="30px" /></span>
                                                                </div>
                                                            </h3>
                                                            <p>Class Type : <%# Eval("ClassType") %></p>
                                                            <p>Year : <%# Eval("YearName") %></p>
                                                            <p>Status : <%# Eval("Status") %></p>
                                                        </div>
                                                    </div>
                                                </asp:HyperLink>
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
    <div class="modal fade" id="squarespaceModal" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <%--<div class="modal-header">
                    <asp:LinkButton ID="lbtnClose" runat="server" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></asp:LinkButton>
                    <h3 class="modal-title" id="lineModalLabel">Create Class</h3>
                </div>--%>
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Create Class</h3>
                </div>


                <div class="modal-body">

                    <!-- content goes here -->
                    <div id="fm2">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="form-group">
                                <label for="classname">Class Name*</label>
                                <asp:TextBox ID="txtClassName" runat="server" class="form-control" placeholder="Class name" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtClassName" ValidationGroup="Submit" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="classyear">Class Type*</label>
                                <asp:DropDownList ID="ddlClassType" runat="server" class="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" ValidationGroup="Submit" ControlToValidate="ddlClassType" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group" id="classyear">
                             
                                <label for="classyear">Class Year</label>
                                <asp:DropDownList ID="ddlClassYear" runat="server" class="form-control">
                                </asp:DropDownList>
                                   
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="<%= GetInitailValueOfClassType %>" ValidationGroup="Submit" ControlToValidate="ddlClassYear" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                
                            </div>
                            <div class="form-group" id="afterschooltype">
                                <label for="classyear">After School Type</label>
                                <asp:DropDownList ID="ddlAfterSchoolType" runat="server" class="form-control">
                                    <asp:ListItem Text="Internal" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="External"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" id="external" style="display: none;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <%--<div class="col-md-12" style="padding-left: 197px; padding-right: 9px !important;">
                                            <asp:RadioButton ID="rdListed" Checked="true" GroupName="rdList" runat="server" Text="Listed" AutoPostBack="true" OnCheckedChanged="rdListed_CheckedChanged" />
                                            <asp:RadioButton ID="rdNonListed" GroupName="rdList" runat="server" AutoPostBack="true" OnCheckedChanged="rdNonListed_CheckedChanged" Text="NonListed" />
                                        </div>--%>
                                        <label for="classyear">After School External Organisation</label><asp:Label ID="lblAft" runat="server" Visible="false" ForeColor="Red">Please Select NonListed CheckBox Due to AfterSchool does not Exist in our Application.</asp:Label>
                                        <div class="col-md-12" style="padding-left: 0px !important; padding-right: 9px !important;">
                                            <asp:TextBox ID="txtOrganization" runat="server" class="form-control" placeholder="Listed After School Organisation" />
                                            <ajax:AutoCompleteExtender ID="AutoCompleteTeachers" CompletionListCssClass="completionList"
                                                CompletionListElementID="completionList"
                                                CompletionListItemCssClass="listItem"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" runat="server" TargetControlID="txtOrganization"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetOrganisations">
                                            </ajax:AutoCompleteExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOrganization" ValidationGroup="Submit" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />
                        </asp:Panel>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="btnCancle" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnCreateClass" runat="server" class="btn btn-primary" ValidationGroup="Submit" CausesValidation="true" OnClick="btnCreateClass_Click" Text="Create Class" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script src='<%= Page.ResolveClientUrl("~/js/toastr.min.js")%>'></script>
    <script type="text/javascript">
        function fnTypeFileClass(sender, args) {
            var s = new Date(args.Value);
            args.IsValid = false;
            var minDate = new Date();
            var maxDate = new Date("9999/12/28");// DateTime.Parse("9999/12/28");
            if (s <= maxDate && s >= minDate) {
                args.IsValid = true;
            }
            return args.IsValid;
        }
        $(document).ready(function () {
            $("#classyear").hide();
            $("#afterschooltype").hide();
            $("#external").hide();
        });
        $('#MainContent_ddlClassType').change(function () {
          <% if (IsStandardSchool())
        { %>
            $("#classyear").show();
         <%}%>
            $("#afterschooltype").show();
            $("#external").hide();

            if (this.value == "1") {
                $("#MainContent_ddlAfterSchoolType").val('Internal');
                $("#MainContent_txtOrganization").val('');
                $("#afterschooltype").hide();
                $("#external").hide();

                
                document.getElementById("<%=RequiredFieldValidator2.ClientID%>").enabled = true;
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").style.visibility = "hidden";
            }
            else if (this.value == "3") {
                $("#classyear").hide();
                document.getElementById("<%=RequiredFieldValidator2.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator2.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").style.visibility = "hidden";
            }
            else {
                $("#MainContent_txtOrganization").val('');
                $("#classyear").hide();
                $("#afterschooltype").hide();
                $("#external").hide();
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").style.visibility = "hidden";
            }

        });

        $('#MainContent_ddlAfterSchoolType').change(function () {
            $("#external").hide();
            if (this.value == "External") {
                $("#external").show();
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = true;
            }

            else {
                $('#MainContent_txtOrganization').val('');
                $("#external").hide();
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").enabled = false;
                document.getElementById("<%=RequiredFieldValidator4.ClientID%>").style.visibility = "hidden";
            }
        });
    </script>
</asp:Content>
