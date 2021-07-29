<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherProfile.aspx.cs" Inherits="SchoolApp.Web.Teacher.TeacherProfile" %>

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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> Teacher</asp:HyperLink>
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
                                        <asp:Image ID="Image6" runat="server" class="img-circle responsives" /></a>
                                </div>
                                <div class="info">
                                    <p>
                                        <asp:Literal ID="litTeacherName" runat="server"></asp:Literal>
                                    </p>
                                    <p>
                                        Teacher No:
                                        <asp:Literal ID="litTeacherNo" runat="server"></asp:Literal>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <asp:FileUpload ID="fpUpload" runat="server" CssClass="btn btn-primary btn-block" ToolTip="Upload your Photo" onchange="UploadFileNow()" />
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
                                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#editpasswordmodel"><span class="fa fa-edit"></span> Change Password</asp:LinkButton>
                                            </div>
                                            <div class="col-md-6 text-left">
                                                <div class=" col-md-12 tital">Assigned Classes:</div>
                                                <div class=" col-md-12 ">
                                                    <asp:Literal ID="litClassName" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Teacher Name:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litTitle" runat="server"></asp:Literal>
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
                                                <div class="col-md-12 tital">End Date:</div>
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
    <div class="modal fade" id="editpasswordmodel" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Reset Password</h3>
                </div>
                <div class="modal-body">
                    <!-- content goes here -->
                    <div>
                        <div class="form-group">
                            <label for="classname">New Password:</label>
                            <asp:TextBox ID="txtpassword" runat="server" class="form-control" TextMode="Password" placeholder="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="alert  validation" runat="server" ValidationGroup="Submit1" ControlToValidate="txtpassword" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ControlToValidate="txtpassword" ValidationGroup="Submit1" ID="RegularExpressionValidator2" runat="server" ErrorMessage="<a class='ourtooltip'>Password must be 8-15 characters long with at least one numeric,</br> one alphabet and one special character.</a>" ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+}{:;'?/>.<,])(?!.*\s).*$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="classname">Confirm Password:</label>
                            <asp:TextBox ID="txtcpassword" runat="server" class="form-control" TextMode="Password" placeholder="Confirm Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="alert  validation" runat="server" ValidationGroup="Submit1" ControlToValidate="txtcpassword" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtpassword" ValidationGroup="Submit1" ControlToValidate="txtcpassword" ErrorMessage="<a class='ourtooltip'>Password Does not Match</a>" Display="Dynamic" />
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
    <script>
        function UploadFileNow() {
            var value = $("#fpUpload").val();
            if (value != '') {
                $("#form1").submit();
            }
        };
    </script>
    <script>
        function SetID(ID) {
            $("#MainContent_HID").val(ID);
        }
    </script>
</asp:Content>
