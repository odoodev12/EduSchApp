<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PickupReport.aspx.cs" Inherits="SchoolApp.Web.Parent.PickupReport" %>

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
                            \ <span>Pickup Report</span></h1>


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

                                <asp:ListView ID="lstParentPickup" runat="server" OnItemDataBound="lstParentPickup_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <div class="class-box feature">
                                                <div class="row">

                                                    <div class="col-md-4 col-xs-4">
                                                        <%--<asp:Image ID="img0" runat="server" ImageUrl='<%# "../" + Eval("StudentPic") %>' class="img-circle" Width="100%" Style="border: 2px solid #00a7ff;" />--%>
                                                        <a class="example-image-link" href='<%# "../" + Eval("StudentPic") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                            <img class="example-image img-circle responsives" style="border: 2px solid #00a7ff;" src='<%# "../" + Eval("StudentPic") %>' alt="" /></a>
                                                    </div>
                                                    <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                        <h3>
                                                            <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                                                        </h3>
                                                        <%--<asp:Label ID="lblSchool" runat="server" Text='<%# Eval("SchoolName") %>'></asp:Label>--%>
                                                        <asp:DropDownList ID="drpSchool" runat="server" CssClass="form-control" Width="100%" onchange="ChangeSchool(this)"></asp:DropDownList>
                                                        <div class="buttons">
                                                            <a href="ViewPickupReport.aspx?Name=<%# Eval("StudentName") %>&SchoolID=<%# Eval("SchoolID") %>" class="btn btn-primary">View Pickup Report </a>
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
        function ChangeSchool(object) {
            var obj = $("#" + object.id);
            var link = obj.next().children()[0].href.split('&');
            obj.next().children()[0].href = link[0] + "&SchoolID=" + object.value;
        }
    </script>
</asp:Content>

