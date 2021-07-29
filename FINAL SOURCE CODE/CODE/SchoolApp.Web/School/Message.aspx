<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="SchoolApp.Web.School.Message" %>

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
            background-color: #428bca !important;
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
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> /
                            <asp:HyperLink ID="hlinkHome" runat="server" NavigateUrl="Home.aspx">Home</asp:HyperLink>
                            / <span>Message</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="col-md-12" style="margin-bottom: 100px;">
            <div class="container" id="features">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="pill" href="#inbox">Sent</a></li>
                    <li><a data-toggle="pill" href="#sent-mail">Received</a></li>
                    <li class="pull-right"><a class="btn btn-primary" href="NewMessage.aspx">New Message </a></li>
                </ul>
            </div>
            <div class="tab-content">
                <div class="tab-pane active" id="inbox">
                    <div class="container">
                        <div class="content-container clearfix">
                            <div class="col-md-12">
                                <h1 class="content-title">Sent</h1>
                                <ul class="mail-list">

                                    <asp:ListView runat="server" ID="lstSent" OnItemCommand="lstSent_ItemCommand">
                                        <ItemTemplate>
                                            <li style="border-bottom: 1px dotted #CFCFCF !important; padding-bottom: 20px !important; padding-top: 20px !important;">

                                                <span class="mail-sender">To: 
                                           <%# Eval("Receiver") %>
                                                    <span class="pull-right">
                                                        <%# Eval("STime") %>
                                                    </span>
                                                </span>
                                                <span class="mail-subject">Subject : <%# Eval("Subject") %></span>
                                                <span class="mail-message-preview"><%# Eval("Description") %> </span>
                                                <div style="margin-top: 10px;">
                                                    <asp:LinkButton ID="lbtnReply" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("ID") %>' CommandName="lbtnReply"><i class="fa fa-reply"></i> Reply</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnForward" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("ID") %>' CommandName="lbtnForward"><i class="fa fa-arrow-right"></i> Forward</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Eval("ID") %>' CommandName="lbtnDelete"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="sent-mail">
                    <div class="container">
                        <div class="content-container clearfix">
                            <div class="col-md-12">
                                <h1 class="content-title">Received</h1>
                                <ul class="mail-list">
                                    <asp:ListView runat="server" ID="lstReceived" OnItemCommand="lstReceived_ItemCommand">
                                        <ItemTemplate>
                                            <li style="border-bottom: 1px dotted #CFCFCF !important; padding-bottom: 20px !important; padding-top: 20px !important;">

                                                <span class="mail-sender">From: 
                                           <%# Eval("Sender") %>
                                                    <span class="pull-right">
                                                        <%# Eval("STime") %>
                                                    </span>
                                                </span>
                                                <span class="mail-subject">Subject : <%# Eval("Subject") %></span>
                                                <span class="mail-message-preview"><%# Eval("Description") %> </span>
                                                <div style="margin-top: 10px;">
                                                    <asp:LinkButton ID="lbtnReply" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("ID") %>' CommandName="lbtnRReply"><i class="fa fa-reply"></i> Reply</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnForward" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("ID") %>' CommandName="lbtnRForward"><i class="fa fa-arrow-right"></i> Forward</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Eval("ID") %>' CommandName="lbtnRDelete"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
    </section>

</asp:Content>
