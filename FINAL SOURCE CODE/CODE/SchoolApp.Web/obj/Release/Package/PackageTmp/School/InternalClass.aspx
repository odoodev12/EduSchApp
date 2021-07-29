<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InternalClass.aspx.cs" Inherits="SchoolApp.Web.School.InternalClass" %>

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
                            <% if (ISTeachers() == true)
                                { %>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Teacher/Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> /
                            <asp:HyperLink ID="hlinkTeacher" runat="server"></asp:HyperLink>
                            <%}
                                else
                                { %>
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> /
                            <asp:HyperLink ID="hlinkSchool" runat="server"></asp:HyperLink>
                            <%} %>
                            / <span>
                                <asp:Literal ID="litClassName" runat="server"></asp:Literal></span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <asp:HiddenField ID="HID" runat="server" />
        <div class="container" id="features">
            <div class="sidebar-section">
                <div class="container">
                    <div class="row">
                        <div class="col-md-3">
                            <h3 style="margin-top: 0px;">Total Student:
                                <asp:Literal ID="litTotalStudent" runat="server"></asp:Literal></h3>
                            <h4 style="margin-top: 0px;">Class Type:
                                <asp:Literal ID="litClassYear" runat="server" Text="After School"></asp:Literal></h4>

                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <asp:Label ID="lbl" runat="server"></asp:Label>
                                <asp:ListView runat="server" ID="lstClasses" OnPagePropertiesChanging="lstClasses_PagePropertiesChanging">
                                    <GroupTemplate>
                                        <div runat="server" id="itemPlaceholder"></div>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">

                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                            <img class="img-circle responsives" style="border: 2px solid #00a7ff;" alt="" src='<%# "../" + Eval("Photo") %>'></a>

                                                    </div>
                                                    <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                        <h3><%# Eval("StudentName") %></h3>
                                                        Student No: <%# Eval("StudentNo") %>

                                                        <%--<span class="pull-right"><a style="font-size: 15px;" href="StudentProfile.aspx?ID=<%# Eval("ID") %>">View Detail</a></span>--%>
                                                        <% if (ISTeachers() == true)
                                                            { %>
                                                        <% if (ISInternal() == true)
                                                            { %>
                                                        <div class="buttons">
                                                            <a href="../Teacher/Pickers.aspx?ID=<%# Eval("ID") %>" class="btn btn-primary">View Pickers</a>
                                                        </div>
                                                        <%} %>
                                                        <%} %>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <table runat="server" class="col-md-12">
                                            <tr runat="server">
                                                <td runat="server" colspan="4">
                                                    <tr runat="server" id="groupPlaceholder"></tr>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td runat="server">
                                                    <asp:DataPager runat="server" PageSize="10" ID="DataPager1">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                            <asp:NumericPagerField></asp:NumericPagerField>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script>
        function SetID(ID) {
            $("#MainContent_HID").val(ID);
            getData();
        }
        //function getData() {
        //    $.ajax({
        //        type: "POST",
        //        url: "StudentClass.aspx/StudentAssignClass",
        //        data: JSON.stringify({ ID: $("#MainContent_HID").val() }),
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data)           //what to do if succedded
        //        {
        //            //alert(data.d);
        //            var person = $.parseJSON(data.d);
        //            //alert(data.d.FirstName);
        //            $('#MainContent_litFirstClass').text(person.FirstClass);
        //        }
        //    });
        //}
    </script>
</asp:Content>
