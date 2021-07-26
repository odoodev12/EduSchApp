<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="SchoolApp.Web.Parent.StudentProfile" %>

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

        #MainContent_rdList > tbody > tr > td {
            padding-left: 10px !important;
        }

            #MainContent_rdList > tbody > tr > td > label {
                padding-left: 10px !important;
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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> Parent</asp:HyperLink>
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
                                    <img id="img1" runat="server" class="img-circle responsives" />
                                    <h6><%--Upload a different photo...--%></h6>
                                    <input type="file" class="text-center center-block hidden well well-sm">
                                </div>
                                <div class="info">
                                    <p>
                                        <asp:Literal ID="litStudent" runat="server"></asp:Literal>
                                    </p>
                                    <%-- <p>
                                        Student No:
                                        <asp:Literal ID="litIDno" runat="server"></asp:Literal>
                                    </p>--%>
                                </div>
                                <div class="form-group">
                                    <asp:FileUpload ID="fpUpload" runat="server" CssClass="btn btn-primary btn-block" onchange="UploadFileNow()" />
                                </div>
                                <%--<div class="buttons">
                                    <asp:LinkButton ID="lnkReAssign" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#reassign_class">Re-assign Class</asp:LinkButton>
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" class="btn btn-danger" OnClick="btnRemove_Click"></asp:Button>
                                </div>--%>
                            </div>

                        </div>
                        <div class="col-md-9">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12 text-right" style="margin-bottom: 20px;">
                                                <h3 class="pull-left page-header">Profile Info</h3>
                                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#editpasswordmodel"><span class="fa fa-edit"></span> Change Parent Password</asp:LinkButton><%-- &nbsp;&nbsp;<asp:LinkButton ID="lnkChangePass" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#editpasswordmodel2"><span class="fa fa-edit"></span> Change Secondary Parent Password</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-primary" data-toggle="modal" data-target="#editclassmodel"><span class="fa fa-edit"></span> Edit Profile </asp:LinkButton>
                                            </div>
                                            <div class="col-md-6 text-left">
                                                <%--<div class=" col-md-12 tital">Class:</div>--%>
                                                <%--<div class=" col-md-12 ">--%>
                                                <asp:Literal ID="litClassName" runat="server" Visible="false"></asp:Literal>
                                                <%--</div>--%>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Parent Name:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPPName" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Parent Email:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPPEmail" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>


                                                <div class="col-md-12 tital">Parent Phone Number:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPPPhone" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>


                                                <div class="col-md-12 tital">Parent Relation:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPPRelation" runat="server"></asp:Literal>
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
    <div class="modal fade" id="editpasswordmodel" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Edit Primary Parent Password</h3>
                </div>
                <div class="modal-body">
                    <!-- content goes here -->
                    <div>
                        <div class="form-group">
                            <label for="classname">New Password:</label>
                            <asp:TextBox ID="txtpassword1" runat="server" class="form-control" TextMode="Password" placeholder="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="alert  validation" runat="server" ValidationGroup="Submit1" ControlToValidate="txtpassword1" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ControlToValidate="txtpassword1" ValidationGroup="Submit1" ID="RegularExpressionValidator2" runat="server" ErrorMessage="<a class='ourtooltip'>Password must be 8-15 characters long with at least one numeric,</br> one alphabet and one special character.</a>" ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+}{:;'?/>.<,])(?!.*\s).*$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Confirm Password:</label>
                            <asp:TextBox ID="txtcpassword1" runat="server" class="form-control" TextMode="Password" placeholder="Confirm Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="alert  validation" runat="server" ValidationGroup="Submit1" ControlToValidate="txtcpassword1" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtpassword1" ValidationGroup="Submit1" ControlToValidate="txtcpassword1" ErrorMessage="<a class='ourtooltip'>Password Does not Match</a>" Display="Dynamic" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <asp:Button ID="btnCancel" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancel" />
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="resetpassword" runat="server" ValidationGroup="Submit1" class="btn btn-primary" Text="Update" OnClick="resetpassword_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="editclassmodel" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Edit Profile</h3>
                </div>
                <div class="modal-body">
                    <!-- content goes here -->
                    <div>
                        <div class="form-group">
                            <label for="classname">Parent Name:</label>
                            <asp:TextBox ID="txtPPName" CssClass="form-control" onkeyup="this.value=TitleCase(this);" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Submit" ControlToValidate="txtPPName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Parent Email:</label>
                            <asp:TextBox ID="txtPPEmail" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator6" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtPPEmail" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="Submit" runat="server" ControlToValidate="txtPPEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'>Enter Valid Email Address</a>"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Parent Phone Number:</label>
                            <asp:TextBox ID="txtPPPhone" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" ValidationGroup="Submit" runat="server" InitialValue="" ControlToValidate="txtPPPhone" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="Submit" runat="server" ControlToValidate="txtPPPhone" ValidationExpression="[0-9]{10}" ErrorMessage="<a class='ourtooltip'>Enter Valid Phone Number</a>"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="form-group">
                            <label for="classname">Parent Relation:</label>
                            <asp:DropDownList ID="ddlPPRelation" runat="server" class="form-control">
                                <asp:ListItem Value="0" Text="Select Relation"></asp:ListItem>
                                <asp:ListItem Value="Mother" Text="Mother"></asp:ListItem>
                                <asp:ListItem Value="Father" Text="Father"></asp:ListItem>
                                <asp:ListItem Value="Guardian" Text="Guardian"></asp:ListItem>
                                <%--<asp:ListItem Value="Friends" Text="Friends"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlPPRelation" runat="server" ErrorMessage="<a class='ourtooltip'>Select Valid Relation</a>"></asp:RequiredFieldValidator>
                        </div>
                        <%--<div class="form-group">
                            <label for="classname">Profile Updation</label>
                            <asp:RadioButtonList ID="rdList" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Update Current Profile" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Update All Profiles" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>--%>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <button type="button" class="btn btn-default" data-dismiss="modal" role="button">Cancel</button>
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnUpdate" runat="server" ValidationGroup="Submit" CssClass="btn btn-primary" Text="Update All Profiles" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<div class="modal fade" id="reassign_class" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
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
    </div>--%>
    <script>
        function TitleCase(objField) {
            var objValues = objField.value.split(" ");
            var outText = "";
            for (var i = 0; i < objValues.length; i++) {
                outText = outText + objValues[i].substr(0, 1).toUpperCase() + objValues[i].substr(1).toLowerCase() + ((i < objValues.length - 1) ? " " : "");
            }
            return outText;
        };

        function UploadFileNow() {

            var value = $("#fpUpload").val();

            if (value != '') {

                $("#form1").submit();

            }
        };
    </script>
</asp:Content>
