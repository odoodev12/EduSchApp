<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SchoolApp.Web.School.Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">

    <style>
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
            background: #FFFFFF;
            text-align: center;
            margin-bottom: 30px;
            padding: 40px 20px 20px;
            border: solid 1px #cccccc;
        }

            .feature h3 {
                color: #232323;
                font-size: 20px;
                text-transform: uppercase;
            }

            .feature:hover {
                background: #F5F5F5;
                -webkit-transform: translate(0, 1em);
                -moz-transform: translate(0, 1em);
                -o-transform: translate(0, 1em);
                -ms-transform: translate(0, 1em);
                transform: translate(0, 1em);
            }

                .feature:hover img {
                    -webkit-transform: scale(1.1);
                    -moz-transform: scale(1.1);
                    -o-transform: scale(1.1);
                    -ms-transform: scale(1.1);
                    transform: scale(1.1);
                }

                .feature:hover .title_border {
                    background-color: #00a7ff;
                    width: 40%;
                }

            .feature .title_border {
                width: 0%;
                height: 2px;
                background: #00a7ff;
                margin: 0 auto;
                margin-top: 12px;
                margin-bottom: 8px;
            }

            .feature > a {
                text-decoration: none;
            }




        .panel-success > .panel-heading {
            background-color: #1c84c6 !important;
            border-color: #1c84c6;
            color: #ffffff;
        }

        .panel-info > .panel-heading {
            background-color: #23c6c8 !important;
            border-color: #23c6c8;
            color: #ffffff;
        }

        .panel-danger > .panel-heading {
            background-color: #ed5565 !important;
            border-color: #ed5565;
            color: #ffffff;
        }

        .panel-warning > .panel-heading {
            background-color: #f8ac59 !important;
            border-color: #f8ac59;
            color: #ffffff;
        }

        .panel-primary > .panel-heading {
            background-color: #18a689 !important;
            border-color: #18a689;
            color: #ffffff;
        }

        .panel-default > .panel-heading {
            background-color: #f5f5f5 !important;
            border-color: #ddd;
            color: #333;
        }

        .big-icon1 {
            font-size: 40px;
            color: #676a6c;
        }

        .headers {
            font-size: 25px;
        }

        .panel-danger {
            border-color: #ed5565;
        }

        element {
            border-color: #ed5565;
        }

        .panel {
            margin-bottom: 30px !important;
            border-radius: 4px;
            background-color: #fff;
        }


        .panel-body {
            padding: 15px;
        }

        .panel-danger > .panel-heading {
            background-color: #ed5565 !important;
            border-color: #ed5565;
            color: #ffffff;
        }

        .panel-heading {
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-bottom-color: transparent;
            border-top-right-radius: 3px;
            border-top-left-radius: 3px;
        }

        .panel-warning {
            border-color: #f8ac59;
        }

        .panel-info {
            border-color: #23c6c8;
        }

        .panel-success {
            border-color: #1c84c6;
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
                        <h1 id="homeHeading"><span>School</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features">
            <div class="row">

               <%-- <%if (GetSchoolType() == true)
                    {%>--%>
                <div class="col-md-3">
                    <div class="feature">
                        <a href="class.aspx">
                            <img src="../img/icon_class.png" />
                            <h3 data-toggle="tooltip" title='Class'>Class</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%--<%} %>--%>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="bulkdata.aspx">
                            <img src="../img/icon_bulkdata.png" />
                            <h3 data-toggle="tooltip" title='Bulk Data'>Bulk data</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="teacher.aspx">
                            <img src="../img/icon_teacher.png" />
                            <h3 data-toggle="tooltip" title='Teacher'>Teacher</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="student.aspx">
                            <img src="../img/icon_student.png" />
                            <h3 data-toggle="tooltip" title='Student'>Student</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%if (GetSchoolType() == true)
                    {%>
                <div class="col-md-3">
                    <div class="feature">
                        <a href="NonTeacher.aspx">
                            <img src="../img/icon_admin.png" />
                            <h3 data-toggle="tooltip" title='Non Teaching Staff' style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">Non Teaching Staff</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%} %>
                <div class="col-md-3">
                    <div class="feature">
                        <a href="HolidayList.aspx">
                            <img src="../img/icon_profile.png" />
                            <h3 data-toggle="tooltip" title='Holiday'>Holiday</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="supportlog.aspx">
                            <img src="../img/icon_support.png" />
                            <h3 data-toggle="tooltip" title='Support'>Support</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="payment.aspx">
                            <img src="../img/icon_payment.png" />
                            <h3 data-toggle="tooltip" title='Payment'>Payment</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>

                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="logactivity.aspx">
                            <img src="../img/icon_activity.png" />
                            <h3 data-toggle="tooltip" title='Log Activity'>Log Activity</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="NewMessage.aspx"><%--message.aspx--%>
                            <img src="../img/icon_message.png" />
                            <h3 data-toggle="tooltip" title='Message'>Message</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="feature">
                        <a href="Report.aspx">
                            <img src="../img/icon_report.png" />
                            <h3 data-toggle="tooltip" title='Report'>Report</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="feature">
                        <a href="adminrole.aspx">
                            <img src="../img/icon_admin.png" />
                            <h3 data-toggle="tooltip" title='Admin'>Role</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
            </div>
        </div>

    </section>
</asp:Content>
