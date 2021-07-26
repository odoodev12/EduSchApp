<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewMessageaspx.aspx.cs" Inherits="SchoolApp.Web.Parent.NewMessageaspx" %>

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
                        <h1 id="homeHeading">
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> / 
                            <asp:HyperLink ID="hlinkParent" runat="server" NavigateUrl="Message.aspx" Text="Message"></asp:HyperLink>
                            / <span>New Message</span></h1>

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

                        <div class="col-md-12">
                            <div class="row">

                                <div class="class-box feature">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <p><b>New Message
                                                <asp:Label runat="server" Text=" @ School Name" ID="lblSchoolNamewithColor" ForeColor="Blue"></asp:Label>
                                               </b></p>
                                            <div class="bot-border"></div>
                                        </div>
                                        <asp:HiddenField ID="hdnfldEmailIds" runat="server" />
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label runat="server" for="classname">Select Receiver Group</asp:Label>
                                                <asp:DropDownList runat="server" class="form-control" ID="drpReceiverGroup" OnSelectedIndexChanged="drpReceiverGroup_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">School</asp:ListItem>
                                                    <asp:ListItem Value="2">Teacher</asp:ListItem>
                                                    <asp:ListItem Value="3">NonTeacher</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpReceiverGroup" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <%--<div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Select Receiver</label>
                                                <asp:DropDownList runat="server" class="form-control" ID="drpReceiver">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpReceiver" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>--%>
                                        <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label for="classname">Subject</label>
                                                <asp:TextBox ID="txtsubject" runat="server" class="form-control" placeholder="Enter Subject" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtsubject" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label for="classname">Description</label>
                                                <asp:TextBox ID="txtDescription" runat="server" class="form-control" TextMode="MultiLine" placeholder="Write here" Rows="4" Style="width: 100%; padding: 10px;" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="txtDescription" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-bottom: 10px;">
                                            <div class="form-group">
                                                <label for="classname">Attachment</label>
                                                <%--<asp:Button runat="server" class="btn btn-success" Text="+ Add Attachment"></asp:Button>--%>
                                                <asp:FileUpload ID="fpUploadAttachment" runat="server" CssClass="btn btn-success" Height="15%" />
                                                <asp:Label ID="lblFileName" Visible ="false" runat="server" ForeColor="Green" />
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <asp:LinkButton ID="btnCancel" runat="server" class="btn btn-primary" OnClick="btnCancel_Click"><i class="fa fa-arrow-left" aria-hidden="true"></i> Cancel</asp:LinkButton>

                                            <asp:LinkButton ID="btnSend" runat="server" class="btn btn-primary" ValidationGroup="Submit" OnClick="btnSend_Click"><i class="fa fa-paper-plane" aria-hidden="true"></i> Send</asp:LinkButton>

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
</asp:Content>
