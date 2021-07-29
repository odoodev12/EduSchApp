<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OfficeClass.aspx.cs" Inherits="SchoolApp.Web.School.OfficeClass" %>

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
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Teacher/Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> /
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="~/Teacher/Home.aspx" Text="Teacher"></asp:HyperLink>
                            <%}
                                else
                                { %>
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> /
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx" Text="School"></asp:HyperLink>
                            <%} %>
                            / <span>
                                <asp:Literal ID="litClassName" runat="server"></asp:Literal></span></h1>
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
                            <h3 style="margin-top: 0px;">Total Student:
                                <asp:Literal ID="litTotalStudent" runat="server"></asp:Literal></h3>
                            <h4 style="margin-top: 0px;">Class Type:
                                <asp:Literal ID="litClassYear" runat="server" Text="Office"></asp:Literal></h4>

                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <asp:Label ID="lbl" runat="server"></asp:Label>
                                <asp:ListView runat="server" ID="lstClasses" OnPagePropertiesChanging="lstClasses_PagePropertiesChanging">
                                    <GroupTemplate>
                                        <div runat="server" id="itemPlaceholder"></div>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">

                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%# "../" + Eval("Photo") %>' class="img-circle" Width="100%" />--%>
                                                        <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                            <img class="img-circle responsives" style="border: 2px solid #00a7ff;" alt="" src='<%# "../" + Eval("Photo") %>'></a>

                                                    </div>
                                                    <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                        <h3><%# Eval("StudentName") %></h3>
                                                        Student No: <%# Eval("StudentNo") %>

                                                        <%--<span class="pull-right"><a style="font-size: 15px;" href="StudentProfile.aspx?ID=<%# Eval("ID") %>">View Detail</a></span>--%>
                                                        <% if (ISTeacher() == true)
                                                            { %>
                                                        <div class="buttons">
                                                            <a href="../Teacher/Pickers.aspx?ID=<%# Eval("ID") %>" class="btn btn-primary">View Pickers</a>
                                                        </div>
                                                        <%} %>
                                                        <%--<div class="buttons">
                                                            <asp:LinkButton ID="btnReAssign" runat="server" class="btn btn-primary" data-toggle="modal" OnClientClick='<%# "return SetID("+Eval("ID")+")" %>' data-target="#reassign_class">Re-assign Class</asp:LinkButton>
                                                            <asp:Button ID="btnRemove" CommandName="btnRemove" CommandArgument='<%# Eval("ID") %>' runat="server" CausesValidation="false" CssClass="btn btn-danger" Text="Remove" />
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <table runat="server" class="col-md-12">
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
                                        </table>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <%-- <div class="modal fade" id="add_student" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Add Student</h3>
                </div>
                <div class="modal-body">

                    <!-- content goes here -->
                    <div>
                        <div class="form-group">
                            <label for="classname">Name</label>
                            <asp:TextBox ID="txtStudentName" runat="server" class="form-control" placeholder="Enter student name" BorderColor="Red" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtStudentName" ValidationGroup="Submit" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Id no</label>
                            <asp:TextBox ID="txtIdNo" runat="server" class="form-control" placeholder="Enter ID NO." BorderColor="Red" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtIdNo" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Primary Parent Name</label>
                            <asp:TextBox ID="txtPPName" runat="server" class="form-control" placeholder="" BorderColor="Red" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Submit" ControlToValidate="txtPPName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Primary parent Email </label>
                            <asp:TextBox ID="txtPPEmai" runat="server" class="form-control" placeholder="" BorderColor="Red" />
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator6" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtPPEmai" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="Submit" runat="server" ControlToValidate="txtPPEmai" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'>Enter Valid Email Address</a>"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Primary parent phone number</label>
                            <asp:TextBox ID="txtPPPhoneNo" runat="server" class="form-control" placeholder="" BorderColor="Red" MaxLength="15" />
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtPPPhoneNo" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Primary parent relation</label>
                            <asp:DropDownList ID="ddlPPRelation" runat="server" class="form-control" BorderColor="Red">
                                <asp:ListItem Value="0" Text="Select Relation"></asp:ListItem>
                                <asp:ListItem Text="Mother"></asp:ListItem>
                                <asp:ListItem Text="Father"></asp:ListItem>
                                <asp:ListItem Text="Uncle"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlPPRelation" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Secondary Parent Name</label>
                                    <asp:TextBox ID="txtSPName" runat="server" class="form-control" placeholder="" OnTextChanged="txtSPName_TextChanged" AutoPostBack="true"/>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" Enabled="false" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtSPName" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="classname">Secondary parent Email </label>
                                    <asp:TextBox ID="txtSPEmail" runat="server" class="form-control" placeholder="" OnTextChanged="txtSPEmail_TextChanged" />
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator9" Enabled="false" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtSPEmail" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Enabled="false" ValidationGroup="Submit" runat="server" ControlToValidate="txtSPEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'>Enter Valid Email Address</a>"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <label for="classname">Secondary parent phone number</label>
                                    <asp:TextBox ID="txtSPPhoneNo" AutoPostBack="true" runat="server" class="form-control" placeholder="" MaxLength="15" OnTextChanged="txtSPPhoneNo_TextChanged" />
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator10" Enabled="false" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtSPPhoneNo" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="classname">Secondary parent relation</label>
                                    <asp:DropDownList ID="ddlSPRelation" runat="server" class="form-control" OnSelectedIndexChanged="ddlSPRelation_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Select Relation"></asp:ListItem>
                                        <asp:ListItem Text="Mother"></asp:ListItem>
                                        <asp:ListItem Text="Father"></asp:ListItem>
                                        <asp:ListItem Text="Uncle"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="Submit" Enabled="false" InitialValue="0" ControlToValidate="ddlSPRelation" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="btnCancle" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnAdd1" runat="server" ValidationGroup="Submit" class="btn btn-primary" Text="Add" OnClick="btnAdd1_Click" />
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
                                <p>
                                    <asp:Label ID="litFirstClass" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlClassTo" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="Save" InitialValue="0" ControlToValidate="ddlClassTo" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="Button9" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnUpdateRecord" runat="server" class="btn btn-primary" Text="Update" OnClick="btnUpdateRecord_Click" ValidationGroup="Save" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script>
        function SetID(ID) {
            $("#MainContent_HID").val(ID);
            getData();
        }
        //function getData() {
        //    $.ajax({
        //        type: "POST",
        //        url: "StudentClass.aspx/StudentAssignClass",
        //        data: JSON.stringify({ ID: $("#MainContent_HID").val() }),
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data)           //what to do if succedded
        //        {
        //            //alert(data.d);
        //            var person = $.parseJSON(data.d);
        //            //alert(data.d.FirstName);
        //            $('#MainContent_litFirstClass').text(person.FirstClass);
        //        }
        //    });
        //}
    </script>
</asp:Content>
