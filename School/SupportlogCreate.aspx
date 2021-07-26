<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupportlogCreate.aspx.cs" Inherits="SchoolApp.Web.School.SupportlogCreate" %>

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
                        <% if (ISTeacher() == true)
                                { %>
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="~/Teacher/Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            <%}
                                else
                                { %>
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            <%} %> /
                        <a href="Supportlog.aspx">Support Log</a> / <span>Create</span></h1>
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
                                            <p><b>Ticket No:</b> <asp:Literal ID="litTicketNumber" runat="server"></asp:Literal></p>
                                            <div class="bot-border"></div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Log Type</label>
                                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control" >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpType" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label for="classname">Subject</label>
                                                <asp:TextBox ID="txtsubject" runat="server" CssClass="form-control" placeholder="Enter Subject"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="txtsubject" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label for="classname">Details</label>
                                                 <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Enter Subject"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" InitialValue="" ControlToValidate="txtDetail" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-bottom: 10px;">
                                            <asp:FileUpload ID="fupload" runat="server" CssClass="btn btn-success" />
                                        </div>
                                        <div class="col-md-12 ">
                                            <p><b>Created By:</b> <asp:Literal ID="litCreatedBy" runat="server"></asp:Literal></p>
                                            <div class="bot-border"></div>
                                        </div>
                                        <div class="col-md-6 ">
                                            <p><b>Date:</b> <asp:Literal ID="litDate" runat="server"></asp:Literal></p>
                                            <div class="bot-border"></div>
                                        </div>
                                        <div class="col-md-6 ">
                                            <p><b>Time:</b> <asp:Literal ID="litTime" runat="server"></asp:Literal></p>
                                            <div class="bot-border"></div>
                                        </div>
                                        <div class="col-md-12 ">
                                            <p><b>Status:</b> <asp:Literal ID="litStatus" runat="server"></asp:Literal></p>
                                            <div class="bot-border"></div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">
                                         
                                               <asp:LinkButton ID="lnkCreate" runat="server" ValidationGroup="Submit" class="btn btn-primary" OnClick="lnkCreate_Click">Create</asp:LinkButton>
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
