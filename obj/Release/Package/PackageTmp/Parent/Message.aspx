<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="SchoolApp.Web.Parent.Message" %>

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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> Parent</asp:HyperLink>
                            / <span>Message</span></h1>

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

                                <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <%--<asp:Image ID="img0" runat="server" ImageUrl='<%# "../" + Eval("Photo") %>' class="img-circle" Width="100%" Style="border: 2px solid #00a7ff;" />--%>
                                                        <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                            <img class="example-image img-circle responsives" style="border: 2px solid #00a7ff;" width="100px" src='<%# "../" + Eval("Photo") %>' alt="" /></a>
                                                    </div>
                                                    <div class="col-md-9" style="padding-left: 0px;">
                                                        <div class="col-md-6">
                                                            <h3>
                                                                <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                                                            </h3>
                                                            Student No : 
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("StudentNo") %>'></asp:Label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="drpSchool" runat="server" CssClass="form-control" Width="100%" onchange="ChangeSchool(this)"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-12 buttons">
                                                            <a href="NewMessageaspx.aspx?ID=<%# Eval("ID") %>&Name=<%# Eval("StudentName") %>&SchoolID=<%# Eval("SchoolID") %>" class="btn btn-primary"><i class="fa fa-envelope" aria-hidden="true"></i>Msg School / Teacher</a>
                                                            <a href="PickupMessage.aspx?ID=<%# Eval("ID") %>&Name=<%# Eval("StudentName") %>&SchoolID=<%# Eval("SchoolID") %>" class="btn btn-danger"><i class="fa fa-envelope" aria-hidden="true"></i>Pickup Msg</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script type="text/javascript">
        function ChangeSchool(object)
        {
            var obj = $("#" + object.id);
            var link = obj.parent().next().children()[0].href.split('&');
            obj.parent().next().children()[0].href = link[0] +'&'+ link[1] + "&SchoolID=" + object.value;
            var link1 = obj.parent().next().children().next()[0].href.split('&');
            obj.parent().next().children().next()[0].href = link1[0] +'&'+ link1[1] + "&SchoolID=" + object.value;
        }
    </script>
</asp:Content>
