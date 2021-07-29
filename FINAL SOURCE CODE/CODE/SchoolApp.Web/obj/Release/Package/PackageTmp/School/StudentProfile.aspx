<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="SchoolApp.Web.School.StudentProfile" %>

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
                            <a href="Student.aspx">Student</a> / <span>Profile</span>
                            <%}
                                else
                                {
                            %>
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            <a href="StudentClass.aspx?ID=<%=this.GetSelectedClassId%>">Student</a>/<span>Profile</span></h1>
                        <%} %>
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
                                    <%--<img id="img1" runat="server" class="img-circle" style="width: 100%" />--%>
                                    <a id="AID" runat="server" class="example-image-link" data-lightbox="example-set">
                                        <img id="img1" runat="server" class="img-circle responsives" /></a>

                                    <asp:FileUpload ID="fpUploadLogo" Visible="false" runat="server" CssClass="btn btn-primary" onchange="UploadFileNow()" />
                                </div>
                                <div class="info">
                                    <p>
                                        <asp:Literal ID="litStudent" runat="server"></asp:Literal>
                                    </p>
                                    <p>
                                        Student No:
                                        <asp:Literal ID="litIDno" runat="server"></asp:Literal>
                                    </p>
                                </div>
                                <div class="buttons">
                                    <asp:LinkButton ID="lnkReAssign" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#reassign_class">Re-assign Class</asp:LinkButton>
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" class="btn btn-danger" OnClick="btnRemove_Click"></asp:Button>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-9">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12 text-right" style="margin-bottom: 20px;">
                                                <h3 class="pull-left page-header">Profile Info</h3>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-primary" data-toggle="modal" data-target="#editclassmodel"><span class="fa fa-edit"></span> Edit</asp:LinkButton>
                                            </div>
                                            <div class="col-md-6 text-left">
                                                <%--<div class=" col-md-12 tital">Class:</div>
                                                <div class=" col-md-12 ">
                                                    <asp:Literal ID="litClassName" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>--%>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Primary Parent Name:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPPName" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Primary Parent Email:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPPEmail" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>


                                                <div class="col-md-12 tital">Primary Parent Phone Number:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPPPhone" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>


                                                <div class="col-md-12 tital">Primary Parent Relation:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPPRelation" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>


                                            </div>
                                            <div class="col-md-6 text-left">
                                                <div class="col-md-12 tital">Secondary Parent Name:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSPName" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Secondary Parent Email:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSPEmail" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Secondary Parent Phone Number:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSPPhone" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Secondary Parent Relation:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSPRelation" runat="server"></asp:Literal>
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
                    <h3 class="modal-title" id="lineModalLabel">Edit Student</h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="form-group">
                            <label for="classname">Name:</label>
                            <asp:TextBox ID="txtStudentName" onkeyup="this.value=TitleCase(this);" CssClass="form-control" runat="server" BorderColor="Red"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="txtStudentName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Class:</label>
                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" BorderColor="Red"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlClass" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Student No:</label>
                            <asp:TextBox ID="txtStudentNo" CssClass="form-control" runat="server" BorderColor="Red"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtStudentNo" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Primary Parent Name:</label><a class="btn pull-right" href="#" onclick="clearTextBox()">remove</a>
                                    <asp:TextBox ID="txtPPName" onkeyup="this.value=TitleCase(this);" ClientIDMode="Static" CssClass="form-control" runat="server" BorderColor="Red"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Submit" ControlToValidate="txtPPName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="classname">Primary Parent Email:</label>
                                    <h6 style="font-size: xx-small; color: red">Note: Only child parent can change parent email. Please contact child parents for this change</h6>
                                    <asp:TextBox ID="txtPPEmail" ClientIDMode="Static" CssClass="form-control" runat="server" BorderColor="Red"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator6" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtPPEmail" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="Submit" runat="server" ControlToValidate="txtPPEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'>Enter Valid Email Address</a>"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <label for="classname">Primary Parent Phone Number:</label>
                                    <asp:TextBox ID="txtPPPhone" ClientIDMode="Static" CssClass="form-control" runat="server" BorderColor="Red"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="+ " TargetControlID="txtPPPhone" />
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtPPPhone" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Submit" ControlToValidate="txtPPPhone" ValidationExpression="^\d+$" ErrorMessage="<a class='ourtooltip'>Enter Valid Phone Number</a>"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <label for="classname">Primary Parent Relation:</label>
                                    <asp:DropDownList ID="ddlPPRelation" runat="server" ClientIDMode="Static" CssClass="form-control" BorderColor="Red">
                                        <asp:ListItem Value="0" Text="Select Relation"></asp:ListItem>
                                        <asp:ListItem Value="Mother" Text="Mother"></asp:ListItem>
                                        <asp:ListItem Value="Father" Text="Father"></asp:ListItem>
                                        <asp:ListItem Value="Guardian1" Text="Guardian"></asp:ListItem>
                                        <%--<asp:ListItem Value="Friends" Text="Friends"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlPPRelation" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Secondary Parent Name:</label><a class="btn pull-right" href="#" onclick="clearTextBoxSecond()">remove</a>
                                    <asp:TextBox ID="txtSPName" ClientIDMode="Static" onkeyup="this.value=TitleCase(this);" CssClass="form-control" runat="server" OnTextChanged="txtSPName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator10" Enabled="false" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtSPName" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="cvName" ControlToValidate="txtSPName" ValidationGroup="Submit" ForeColor="Red" ControlToCompare="txtPPName" Operator="NotEqual" Type="String" ErrorMessage="Primary Name and Secondary Name cannot be the same" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Secondary Parent Email:</label>
                                    <% if (!IsSecondaryEmailBlank())
                                    { %>
                                    <h6 style="font-size: xx-small; color: red">Note: Only child parent can change parent email. Please contact child parents for this change</h6>
                                    <%}%>
                                    <asp:TextBox ID="txtSPEmail" ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtSPEmail_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator9" Enabled="false" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtSPEmail" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="cvEmail" ControlToValidate="txtSPEmail" ValidationGroup="Submit" ForeColor="Red" ControlToCompare="txtPPEmail" Operator="NotEqual" Type="String" ErrorMessage="Primary Email and Secondary Email cannot be the same" /><br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Enabled="false" ValidationGroup="Submit" runat="server" ControlToValidate="txtSPEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'>Enter Valid Email Address</a>"></asp:RegularExpressionValidator>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Secondary Parent Phone Number:</label>
                                    <asp:TextBox ID="txtSPPhone" ClientIDMode="Static" CssClass="form-control" runat="server" OnTextChanged="txtSPPhone_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="+ " TargetControlID="txtSPPhone" />
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator12" Enabled="false" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtSPPhone" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="cvPhone" ControlToValidate="txtSPPhone" ValidationGroup="Submit" ForeColor="Red" ControlToCompare="txtPPPhone" Operator="NotEqual" Type="String" ErrorMessage="Primary Phone and Secondary Phone cannot be the same" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="classname">Secondary Parent Relation:</label>
                                    <asp:DropDownList ID="ddlSPRelation" runat="server" class="form-control" OnSelectedIndexChanged="ddlSPRelation_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Select Relation"></asp:ListItem>
                                        <asp:ListItem Value="Mother" Text="Mother"></asp:ListItem>
                                        <asp:ListItem Value="Father" Text="Father"></asp:ListItem>
                                        <asp:ListItem Value="Guardian2" Text="Guardian"></asp:ListItem>
                                        <%--<asp:ListItem Value="Friends" Text="Friends"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="Submit" Enabled="false" InitialValue="0" ControlToValidate="ddlSPRelation" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="cvRelation" ControlToValidate="ddlSPRelation" ValidationGroup="Submit" ForeColor="Red" ControlToCompare="ddlPPRelation" Operator="NotEqual" Type="String" ErrorMessage="Primary Relation and Secondary Relation cannot be the same" />

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <button type="button" class="btn btn-default" data-dismiss="modal" role="button">Cancel</button>
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnUpdate" runat="server" ValidationGroup="Submit" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" />
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
                    <h3 class="modal-title" id="reAssignClass">Reassign Class</h3>
                </div>
                <div class="modal-body">
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
                                    <asp:Literal ID="litClassNm" runat="server"></asp:Literal>
                                </p>
                            </div>
                            <div class="col-md-8 col-xs-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlClassTo" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator8" runat="server" InitialValue="0" ValidationGroup="Save" ControlToValidate="ddlClassTo" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <button type="button" class="btn btn-default" data-dismiss="modal" role="button">Cancel</button>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnAssigns" runat="server" ValidationGroup="Save" CssClass="btn btn-primary" OnClick="btnAssign_Click" Text="Update"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function UploadFileNow() {
            var value = $("#fpUploadLogo").val();
            if (value !== '') {
                $("#form1").submit();
            }
        }
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

        function clearTextBox() {
            document.getElementById('txtPPEmail').disabled = false;
            document.getElementById('txtPPName').value = document.getElementById('txtSPName').value !== '' ? document.getElementById('txtSPName').value : '';
            document.getElementById('txtPPEmail').value = document.getElementById('txtSPEmail').value !== '' ? document.getElementById('txtSPEmail').value : '';
            document.getElementById('txtPPPhone').value = document.getElementById('txtSPPhone').value !== '' ? document.getElementById('txtSPPhone').value : '';
            var value = document.getElementById("<%=ddlSPRelation.ClientID%>");

            if (value.options[value.selectedIndex].value !== '0') {

                if (value.options[value.selectedIndex].value == 'Guardian2')
                    document.getElementById("<%=ddlPPRelation.ClientID %>").value = 'Guardian1';
                else
                    document.getElementById("<%=ddlPPRelation.ClientID %>").value = value.options[value.selectedIndex].value;
            }
            else
                document.getElementById("<%=ddlPPRelation.ClientID %>").value = '0';

            <%--//document.getElementById("<%=ddlPPRelation.ClientID %>").value = value.options[value.selectedIndex].value !== '0' ? value.options[value.selectedIndex].value : '0';--%>
            clearTextBoxSecond();
        }
        function clearTextBoxSecond() {
            document.getElementById('txtSPName').value = '';


            document.getElementById('txtSPEmail').disabled = false;



            document.getElementById('txtSPEmail').value = '';
            document.getElementById('txtSPPhone').value = '';
            document.getElementById("<%=ddlSPRelation.ClientID %>").value = '0';




            document.getElementById('txtSPName').style.borderColor = '#ccc';
            document.getElementById('txtSPEmail').style.borderColor = '#ccc';
            document.getElementById('txtSPPhone').style.borderColor = '#ccc';
            document.getElementById("<%=ddlSPRelation.ClientID %>").style.borderColor = '#ccc';

            document.getElementById("<%=Requiredfieldvalidator10.ClientID%>").enabled = false;
            document.getElementById("<%=Requiredfieldvalidator9.ClientID%>").enabled = false;
            document.getElementById("<%=RegularExpressionValidator3.ClientID%>").enabled = false;
            document.getElementById("<%=Requiredfieldvalidator12.ClientID%>").enabled = false;
            document.getElementById("<%=RequiredFieldValidator11.ClientID%>").enabled = false;
        }
    </script>
</asp:Content>
