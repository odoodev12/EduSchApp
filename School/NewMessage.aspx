<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="SchoolApp.Web.School.NewMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .float-label {
            font-size: 10px;
        }


        /*Content Container*/

        .content-container {
            background-color: #fff;
            padding: 20px 0px;
            margin-bottom: 20px;
        }

        h1.content-title {
            font-size: 32px;
            font-weight: 300;
            text-align: center;
            margin-top: 20px;
            margin-bottom: 20px;
            font-family: 'Open Sans', sans-serif !important;
        }

        /*Compose*/

        .btn-send {
            margin-top: 20px;
        }

        /*mail list*/

        ul.mail-list {
            padding: 0;
            margin: 0;
            list-style: none;
            margin-top: 30px;
        }

            ul.mail-list li a {
                display: block;
                border-bottom: 1px dotted #CFCFCF;
                padding: 20px;
                text-decoration: none;
            }

            ul.mail-list li:last-child a {
                border-bottom: none;
            }

            ul.mail-list li a:hover {
                background-color: #DBF9FF;
            }

            ul.mail-list li span {
                display: block;
            }

                ul.mail-list li span.mail-sender {
                    font-weight: 600;
                    color: #8F8F8F;
                }

                ul.mail-list li span.mail-subject {
                    color: #8C8C8C;
                }

                ul.mail-list li span.mail-message-preview {
                    display: block;
                    color: #A8A8A8;
                }

        .nav > li > a {
            position: relative;
            display: block;
            padding: 10px 8px;
        }
    </style>
    <style>
        .chip {
            display: inline-block;
            padding: 0 25px;
            height: 40px;
            font-size: 18px;
            line-height: 40px;
            border-radius: 36px;
            background-color: #f1f1f1;
        }

            .chip img {
                float: left;
                margin: 0 10px 0 -25px;
                height: 50px;
                width: 50px;
                border-radius: 50%;
            }

        .closebtn {
            padding-left: 10px;
            color: #888;
            font-weight: bold;
            float: right;
            font-size: 20px;
            cursor: pointer;
        }

            .closebtn:hover {
                color: #000;
            }

        .btn-file {
            position: relative;
            overflow: hidden;
        }

            .btn-file input[type=file] {
                position: absolute;
                top: 0;
                right: 0;
                min-width: 100%;
                min-height: 100%;
                font-size: 100px;
                text-align: right;
                filter: alpha(opacity=0);
                opacity: 0;
                outline: none;
                background: white;
                cursor: inherit;
                display: block;
            }
    </style>
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

        .modal-body {
            padding: 15px;
        }

        .view_detail a {
            font-size: 10px;
        }

        .bot-border {
            border-bottom: 2px #f7f7f7 solid;
            margin: 15px 0 15px 0;
        }
    </style>
    <style>
        .btn-primary {
            color: #ffffff !important;
            background-color: #00a7ff !important;
            border-color: #357ebd !important;
        }

        .btn {
            display: inline-block !important;
            padding: 6px 12px !important;
            margin-bottom: 0 !important;
            font-size: 14px !important;
            font-weight: normal !important;
            line-height: 1.428571429 !important;
            text-align: center !important;
            white-space: nowrap !important;
            vertical-align: middle !important;
            cursor: pointer !important;
            border: 1px solid transparent !important;
            border-radius: 4px !important;
            -webkit-user-select: none !important;
            -moz-user-select: none !important;
            -ms-user-select: none !important;
            -o-user-select: none !important;
            user-select: none !important;
        }

        .btn-danger {
            color: #ffffff !important;
            background-color: #d9534f !important;
            border-color: #d43f3a !important;
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
                            <asp:HyperLink ID="hlinkHome" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> School</asp:HyperLink>
                            / <span>Compose Message</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="col-md-12" style="margin-bottom: 100px;">
            <section>
                <div class="container">
                    <div class="sidebar-section">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="class-box feature">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h1 class="content-title">Compose</h1>
                                                    <div class="form-group">
                                                        <label for="classname">Select Receiver Group</label>
                                                        <asp:DropDownList OnSelectedIndexChanged="drpReceiverGroup_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control" ID="drpReceiverGroup">
                                                            <%--<asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                            <asp:ListItem Value="-1">ALL</asp:ListItem>
                                                            <asp:ListItem Value="2">Teacher</asp:ListItem>
                                                            <asp:ListItem Value="3">Parent</asp:ListItem>
                                                            <asp:ListItem Value="4">NonTeacher</asp:ListItem>--%>
                                                            

                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save" InitialValue="0" ControlToValidate="drpReceiverGroup" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="classname">Select Class</label>
                                                        <asp:DropDownList runat="server" class="form-control" ID="drpClass" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="classname">Select Receiver</label>
                                                        <asp:DropDownList runat="server" class="form-control" ID="drpReceiver">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save" InitialValue="0" ControlToValidate="drpReceiver" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <label for="classname">Subject</label>
                                                        <asp:TextBox ID="txtSubject" runat="server" class="form-control" placeholder="Enter Subject" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save" ControlToValidate="txtSubject" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <label for="classname">Description</label>
                                                        <asp:TextBox ID="txtDescription" runat="server" Rows="4" TextMode="MultiLine" Style="width: 100%; padding: 10px;" placeholder="Write here"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Save" ControlToValidate="txtDescription" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 paddi" style="margin-bottom: 10px;">
                                                    <asp:FileUpload ID="fpUploadAttachment" runat="server" Height="15%" />
                                                    <asp:Label ID="lblFileName" Visible ="false" runat="server" ForeColor="Green" />
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px;">
                                                    <asp:LinkButton ID="btnSends" runat="server" CssClass="btn btn-primary" ValidationGroup="Save" OnClick="btnSend_Click"><i class="fa fa-plane"></i>&nbsp;&nbsp;Send</asp:LinkButton>

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
        </div>
    </section>
</asp:Content>
