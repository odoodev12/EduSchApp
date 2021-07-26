<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="SchoolApp.Web.Teacher.Message" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading"><asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> Teacher</asp:HyperLink> / <span>Message</span></h1>
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

                        <div class="col-md-12">
                            <div class="row">

                                <div class="class-box feature">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Select Receiver Group</label>
                                                <asp:DropDownList runat="server" class="form-control" ID="drpReceiverGroup" OnSelectedIndexChanged="drpReceiverGroup_SelectedIndexChanged" AutoPostBack="true">
                                                    <%--<asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">School</asp:ListItem>
                                                    <asp:ListItem Value="3">Parent</asp:ListItem>
                                                    <asp:ListItem Value="4">Teacher</asp:ListItem>
                                                    <asp:ListItem Value="5">Non-Teacher</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpReceiverGroup" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpReceiver" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12 ">
                                            <div class="form-group">
                                                <asp:Label runat="server" for="classname">Subject</asp:Label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtSubject" placeholder="Enter Subject"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtSubject" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-12 ">
                                            <div class="form-group">
                                                <asp:Label runat="server" for="classname">Description</asp:Label>
                                                <asp:TextBox Rows="4" ID="txtDesc" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Write here"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Submit" ControlToValidate="txtDesc" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-bottom: 10px;">
                                            <%--<asp:Button runat="server" class="btn btn-success" Text="+ Add Attachment"></asp:Button>--%>
                                            <asp:FileUpload ID="fpUploadAttachment" runat="server" CssClass="btn btn-success" Height="15%" />
                                            <asp:Label ID="lblFileName" Visible ="false" runat="server" ForeColor="Green" />
                                        </div>


                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <%--<asp:Button ID="btnSend" runat="server" ValidationGroup="Submit" CssClass="fa fa-plane btn btn-primary" Text="Send" OnClick="btnSend_Click"></asp:Button>--%>
                                            <asp:LinkButton ID="btnSends" runat="server" CssClass="btn btn-primary" ValidationGroup="Submit" OnClick="btnSend_Click"><i class="fa fa-plane"></i>&nbsp;&nbsp;Send</asp:LinkButton>
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
    <br />
    <br />
</asp:Content>
