<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SchoolApp.Web.Teacher.Home" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading"><span>Teacher</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <!-- FAQ -->
    <section>
        <div class="container" id="features">
            <div class="row">
                <%if (GetTeacherModule() == true)
                    {%>
                <%if (GetAttendanceRequired())
                    {%>
                        <div class="col-md-3 ">
                    <div class="feature">
                        <a runat="server" id="Attendence" onserverclick="Attendence_ServerClick">
                            <img src="<%= Page.ResolveClientUrl("~/img/icon_dailypickustatus.png")%>" />

                            <h3>Attendance</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                    <%} %>                
                <%} %>
                <div class="col-md-3">
                    <div class="feature">
                        <a href="Pickup.aspx">

                            <img src="<%= Page.ResolveClientUrl("~/img/icon_manage_pickers.png")%>" />
                            <h3>Pickup</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>

                <%if (!GetAttendanceRequired())
                    {%>
                        <div class="col-md-3 ">
                    <div class="feature">
                        <a runat="server" id="A1" onserverclick="Attendence_ServerClick">
                            <img src="<%= Page.ResolveClientUrl("~/img/icon_dailypickustatus.png")%>" />

                            <h3>Class Daily Report</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                    <%} %>

                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="ClassReport.aspx">
                            <img src="<%= Page.ResolveClientUrl("~/img/icon_pickupreport.png")%>" />

                            <h3>Pickup Report</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%if (GetTeacherModule() == true)
                    {%>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="Message.aspx">
                            <img src="<%= Page.ResolveClientUrl("~/img/icon_message_parent.png")%>" />

                            <h3>Message</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%} %>
               <%-- <%if (GetSchoolType() == true)
                    {%>--%>
                <% if (GetClassRole() == true)
                    { %>
                <div class="col-md-3">
                    <div class="feature">
                        <a href="../School/Class.aspx">
                            <img src="../img/icon_class.png" />
                            <h3>Class</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%--<%}--%> 
                <%} %>
                <%if (GetTeacherRole() == true)
                    { %>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="../School/teacher.aspx">
                            <img src="../img/icon_teacher.png" />
                            <h3>Teacher</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%} %>
                <%if (GetSchoolType() == true)
                    {%>
                <% if (GetNonTeacherRole() == true)
                    { %>
                <div class="col-md-3">
                    <div class="feature">
                        <a href="../School/NonTeacher.aspx">
                            <img src="../img/icon_admin.png" />
                            <h3 data-toggle="tooltip" title='Non Teaching Staff' style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">Non Teaching Staff</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%} %>
                <%} %>
                <%if (GetStudentRole() == true)
                    { %>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="../School/Student.aspx">
                            <img src="../img/icon_student.png" />
                            <h3>Student</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%} %>
                <%if (GetHolidayRole() == true)
                    { %>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="../School/HolidayList.aspx">
                            <img src="../img/icon_student.png" />
                            <h3>Holiday</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%} %>
                <%if (GetSupportRole() == true)
                    { %>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="../School/supportlog.aspx">
                            <img src="../img/icon_support.png" />
                            <h3>Support</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%} %>
                <%if (GetViewAccountRole() == true)
                    { %>
                <div class="col-md-3 ">
                    <div class="feature">
                        <a href="../School/payment.aspx">
                            <img src="../img/icon_payment.png" />
                            <h3>Payment</h3>
                            <div class="title_border"></div>
                        </a>
                    </div>
                </div>
                <%} %>
            </div>
        </div>
    </section>
</asp:Content>

