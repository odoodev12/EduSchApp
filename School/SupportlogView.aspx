<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupportlogView.aspx.cs" Inherits="SchoolApp.Web.School.SupportlogView" %>

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

        .mytext{
    border:0;padding:10px;background:whitesmoke;
}
.text{
    width:75%;display:flex;flex-direction:column;
}
.text > p:first-of-type{
    width:100%;margin-top:0;margin-bottom:auto;line-height: 13px;font-size: 12px;
}
.text > p:last-of-type{
    width:100%;text-align:right;color:silver;margin-bottom:-7px;margin-top:auto;
}
.text-l{
    float:left;padding-right:10px;
}        
.text-r{
    float:right;padding-left:10px;
}
.avatar{
    display:flex;
    justify-content:center;
    align-items:center;
    width:25%;
    float:left;
    padding-right:10px;
}
.macro{
    margin-top:5px;width:85%;border-radius:5px;padding:5px;display:flex;
}
.msj-rta{
    float:right;background:whitesmoke;
}
.msj{
    float:left;background:white;
}
.frame{
    background:#e0e0de;
    height:450px;
    overflow:hidden;
    padding:0;
}
.frame > div:last-of-type{
    position:absolute;bottom:0;width:100%;display:flex;
}
body > div > div > div:nth-child(2) > span{
    background: whitesmoke;padding: 10px;font-size: 21px;border-radius: 50%;
}
body > div > div > div.msj-rta.macro{
    margin:auto;margin-left:1%;
}
.ulClass {
    width:100%;
    list-style-type: none;
    padding:18px;
    position:absolute;
    bottom:47px;
    display:flex;
    flex-direction: column;
    top:0;
    overflow-y:scroll;
}
.msj:before{
    width: 0;
    height: 0;
    content:"";
    top:-5px;
    left:-14px;
    position:relative;
    border-style: solid;
    border-width: 0 13px 13px 0;
    border-color: transparent #ffffff transparent transparent;            
}
.msj-rta:after{
    width: 0;
    height: 0;
    content:"";
    top:-5px;
    left:14px;
    position:relative;
    border-style: solid;
    border-width: 13px 13px 0 0;
    border-color: whitesmoke transparent transparent transparent;           
}  
input:focus{
    outline: none;
}        
::-webkit-input-placeholder { /* Chrome/Opera/Safari */
    color: #d4d4d4;
}
::-moz-placeholder { /* Firefox 19+ */
    color: #d4d4d4;
}
:-ms-input-placeholder { /* IE 10+ */
    color: #d4d4d4;
}
:-moz-placeholder { /* Firefox 18- */
    color: #d4d4d4;
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
                        <a href="Supportlog.aspx">Support Log</a> / <span>View</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div>
            <div class="container" id="features">
                <div class="sidebar-section">
                    <div class="container">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="row">

                                    <div class="class-box feature">
                                        <div class="row">

                                            <div class="col-md-6">
                                                <p>
                                                    <b>Ticket No:</b>
                                                    <asp:Literal ID="litTicketNo" runat="server"></asp:Literal>
                                                </p>
                                                <div class="bot-border"></div>
                                            </div>

                                            <div class="col-md-6">
                                                <p>
                                                    <b>Subject:</b>
                                                    <asp:Literal ID="litSubject" runat="server"></asp:Literal>
                                                </p>
                                                <div class="bot-border"></div>
                                            </div>
                                            <div class="col-md-6">
                                                <p>
                                                    <b>Created By:</b>
                                                    <asp:Literal ID="litCreatedBy" runat="server"></asp:Literal>
                                                </p>
                                                <div class="bot-border"></div>
                                            </div>
                                            <div class="col-md-6">
                                                <p>
                                                    <b>Status:</b>
                                                    <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                                                </p>
                                                <div class="bot-border"></div>
                                            </div>
                                            <div class="col-md-6">
                                                <p>
                                                    <b>Date:</b>
                                                    <asp:Literal ID="litDate" runat="server"></asp:Literal>
                                                </p>
                                                <div class="bot-border"></div>
                                            </div>
                                            <div class="col-md-6">
                                                <p>
                                                    <b>Time:</b>
                                                    <asp:Literal ID="litTime" runat="server"></asp:Literal>
                                                </p>
                                                <div class="bot-border"></div>
                                            </div>
                                            
                                            <div class="col-md-12">
                                                <p style="height:300px;">
                                                   <b>Details:</b><br />
                                                    <asp:Literal ID="litDetail"  runat="server"></asp:Literal>
                                                </p>
                                                <div class="bot-border"></div>
                                            </div>
                                            <div class="col-md-12 ">

                                                <asp:TextBox ID="txtComment" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="txtComment" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-12 " style="margin-top: 10px;">
                                                <asp:FileUpload ID="fupload" runat="server" CssClass="btn btn-success" />
                                            </div>
                                            <div class="col-md-12" style="margin-top: 15px;">
                                                <asp:LinkButton ID="lnkSend" runat="server" ValidationGroup="Submit" CssClass="btn btn-primary" OnClick="lnkSend_Click">Send</asp:LinkButton>

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
</asp:Content>
