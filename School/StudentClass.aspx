<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentClass.aspx.cs" EnableEventValidation="false" Inherits="SchoolApp.Web.School.StudentClass" %>

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
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Teacher/Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            /
                            <asp:HyperLink ID="HyperLink1" runat="server" Text="Student" NavigateUrl="~/School/Student.aspx"></asp:HyperLink>
                            <%}
                                else
                                { %>
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            /
                            <asp:HyperLink ID="hlinkSchool" runat="server" Text="Student" NavigateUrl="~/School/Student.aspx"></asp:HyperLink>
                            <%} %>
                            /<span>
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
                            <h4 style="margin-top: 0px;">Year:
                                <asp:Literal ID="litClassYear" runat="server"></asp:Literal></h4>
                            <asp:LinkButton ID="btnAdd" runat="server" class="btn btn-primary btn-block" Style="border: none; padding: 10px;" data-toggle="modal" data-target="#add_student"><i class="fa fa-plus"></i> Add Student</asp:LinkButton>
                            <br />
                            <div class="well">
                                <p>Filter by:</p>
                                <div>
                                    <div class="form-group">
                                        <label for="classyear">Student Name</label>
                                        <asp:TextBox ID="txtStudentNames" runat="server" class="form-control" placeholder="Enter Student Name" />
                                    </div>
                                    <div>
                                        <center><asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>
                                    </div>
                                </div>
                                <hr />
                            </div>
                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <asp:Label ID="lbl" runat="server"></asp:Label>
                                <asp:ListView runat="server" ID="lstClasses" OnItemCommand="lstClasses_ItemCommand" OnPagePropertiesChanging="lstClasses_PagePropertiesChanging">
                                    <GroupTemplate>
                                        <div runat="server" id="itemPlaceholder"></div>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <asp:HiddenField ID="classId" runat="server" />
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%# "../" + Eval("Photo") %>' class="img-circle" Width="100%" />--%>
                                                        <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                            <img class="img-circle responsives" style="border: 2px solid #00a7ff;" alt="" src='<%# "../" + Eval("Photo") %>'></a>

                                                    </div>
                                                    <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                        <h3><%# Eval("StudentName") %></h3>
                                                        Student No: <%# Eval("StudentNo") %>

                                                        <span class="pull-right"><a style="font-size: 15px;" href="StudentProfile.aspx?ID=<%# Eval("ID") %>&ClassId=<%# Eval("ClassID") %>">View Detail</a></span>
                                                        <div class="buttons">
                                                            <asp:LinkButton ID="btnReAssign" runat="server" class="btn btn-primary" Visible='<%# Eval("ClassName").ToString() == "Outside Class" ? false : true %>' data-toggle="modal" OnClientClick='<%# "return SetID("+Eval("ID")+")" %>' data-target="#reassign_class">Re-assign Class</asp:LinkButton>
                                                            <asp:Button ID="btnRemove" OnClientClick='<%# "return ConfirmMsg();" %>' CommandName="btnRemove" CommandArgument='<%# Eval("ID") %>' runat="server" Visible='<%# Eval("ClassName").ToString() == "Outside Class" ? false : true %>' CausesValidation="false" CssClass="btn btn-danger" Text="Remove" />
                                                        </div>
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
    <div class="modal fade" id="add_student" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
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
                            <asp:TextBox ID="txtStudentName" runat="server" class="form-control" onkeyup="this.value=TitleCase(this);" BorderColor="Red" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtStudentName" ValidationGroup="Submit" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <%-- <div class="form-group">
                            <label for="classyear">Class</label>
                            <asp:DropDownList ID="ddlClass" runat="server" class="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlClass" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>--%>
                        <div class="form-group">
                            <label for="classname">Student No</label>
                            <asp:TextBox ID="txtIdNo" runat="server" class="form-control" BorderColor="Red" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtIdNo" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Primary Parent Name</label>
                            <asp:TextBox ID="txtPPName" runat="server" class="form-control" onkeyup="this.value=TitleCase(this);" placeholder="" BorderColor="Red" />
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
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="+ " TargetControlID="txtPPPhoneNo" />
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtPPPhoneNo" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Primary parent relation</label>
                            <asp:DropDownList ID="ddlPPRelation" runat="server" class="form-control" placeholder="" BorderColor="Red">
                                <asp:ListItem Value="0" Text="Select Relation"></asp:ListItem>
                                <asp:ListItem Value="Mother" Text="Mother"></asp:ListItem>
                                <asp:ListItem Value="Father" Text="Father"></asp:ListItem>
                                <asp:ListItem Value="Guardian1" Text="Guardian"></asp:ListItem>
                                <%--<asp:ListItem Value="Friends" Text="Friends"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlPPRelation" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Secondary Parent Name</label>
                                    <asp:TextBox ID="txtSPName" runat="server" class="form-control" onkeyup="this.value=TitleCase(this);" placeholder="" OnTextChanged="txtSPName_TextChanged" AutoPostBack="true" />
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" Enabled="false" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtSPName" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txtPPName" ValidationGroup="Submit" ForeColor="Red" ControlToCompare="txtSPName" Operator="NotEqual" Type="String" ErrorMessage="Primary Name and Secondary Name cannot be the same" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Secondary Parent Email </label>
                                    <asp:TextBox ID="txtSPEmail" AutoPostBack="true" runat="server" class="form-control" placeholder="" OnTextChanged="txtSPEmail_TextChanged" />
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator9" Enabled="false" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtSPEmail" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtPPEmai" ValidationGroup="Submit" ForeColor="Red" ControlToCompare="txtSPEmail" Operator="NotEqual" Type="String" ErrorMessage="Primary Email and Secondary Email cannot be the same" /><br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Enabled="false" ValidationGroup="Submit" runat="server" ControlToValidate="txtSPEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'>Enter Valid Email Address</a>"></asp:RegularExpressionValidator>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Secondary Parent phone number</label>
                                    <asp:TextBox ID="txtSPPhoneNo" AutoPostBack="true" runat="server" class="form-control" placeholder="" MaxLength="15" OnTextChanged="txtSPPhoneNo_TextChanged" />
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="+ " TargetControlID="txtSPPhoneNo" />
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator10" Enabled="false" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtSPPhoneNo" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="CompareValidator2" ControlToValidate="txtPPPhoneNo" ValidationGroup="Submit" ForeColor="Red" ControlToCompare="txtSPPhoneNo" Operator="NotEqual" Type="String" ErrorMessage="Primary Phone and Secondary Phone cannot be the same" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Secondary Parent relation</label>
                                    <asp:DropDownList ID="ddlSPRelation" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlSPRelation_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Select Relation"></asp:ListItem>
                                        <asp:ListItem Value="Mother" Text="Mother"></asp:ListItem>
                                        <asp:ListItem Value="Father" Text="Father"></asp:ListItem>
                                        <asp:ListItem Value="Guardian2" Text="Guardian"></asp:ListItem>
                                        <%--<asp:ListItem Value="Friends" Text="Friends"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="Submit" Enabled="false" InitialValue="0" ControlToValidate="ddlSPRelation" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <%--<asp:CustomValidator runat="server" id="cusCustom" controltovalidate="ddlSPRelation" onservervalidate="cusCustom_ServerValidate" errormessage="Primary Relation and Secondary Relation cannot be the same" />--%>
                                    <asp:CompareValidator runat="server" ID="CompareValidator3" ControlToValidate="ddlPPRelation" ValidationGroup="Submit" ForeColor="Red" ControlToCompare="ddlSPRelation" Operator="NotEqual" Type="String" ErrorMessage="Primary Relation and Secondary Relation cannot be the same" />
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

                            <%--<asp:Button ID="btnUpdate" runat="server" class="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script>

        function TitleCase(objField) {
            var objValues = objField.value.split(" ");
            var outText = "";
            for (var i = 0; i < objValues.length; i++) {
                outText = outText + objValues[i].substr(0, 1).toUpperCase() + objValues[i].substr(1).toLowerCase() + ((i < objValues.length - 1) ? " " : "");
            }
            return outText;
        };
        function SetID(ID) {
            $("#MainContent_HID").val(ID);
            getData();
        }
        function getData() {
            $.ajax({
                type: "POST",
                url: "StudentClass.aspx/StudentAssignClass",
                data: JSON.stringify({ ID: $("#MainContent_HID").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data)           //what to do if succedded
                {
                    //alert(data.d);
                    var person = $.parseJSON(data.d);
                    //alert(data.d.FirstName);
                    $('#MainContent_litFirstClass').text(person.FirstClass);
                }
            });
        }
    </script>
</asp:Content>
