<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="SchoolApp.Web.School.Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .sidebar-box {
            background: #fff;
            box-shadow: 0px 0px .5px 0px rgba(50, 50, 50, 0.95);
            margin-bottom: 20px;
        }

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
            }



        .feature, .feature h3, .feature img, .feature .title_border {
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
                -webkit-transform: translate(0,.5em);
                -moz-transform: translate(0,.5em);
                -o-transform: translate(0,.5em);
                -ms-transform: translate(0,.5em);
                transform: translate(0,.5em);
            }

        .modal-body {
            padding: 15px;
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
                            <% if (ISTeacher() == true)
                                        { %>
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="~/Teacher/Home.aspx"><i class="fa fa-home"></i> Teacher</asp:HyperLink>
                            <%} else { %>
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> School</asp:HyperLink>
                             <%} %>
                            / <span>Student</span></h1>
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
                        <div class="col-md-3">
                            <!-- <h3 style="margin-top: 0px;">Select Class Year</h3>
                           <!-- <div class="sidebar-box">
                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                            </div> -->
                            <h3 style="margin-top: 0px;">Select Class</h3>
                            <div class="sidebar-box">
                                <asp:DropDownList ID="ddlClass" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <asp:ListView runat="server" ID="lstClasses">
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ClassType").ToString() == "Office" ? "OfficeClass.aspx" : Eval("ClassType").ToString() == "After School" ? "InternalClass.aspx" : "StudentClass.aspx?ID="+Eval("ID") %>'>
                                                    <div class="row">
                                                        <div class="col-md-4 col-xs-4">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../img/icon_class.png" />
                                                        </div>
                                                        <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                            <h3><%# Eval("Name") %> <span>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="../img/icon_right_arrow_gray.png" Width="30px" /></span></h3>
                                                            <p>Students: <asp:Label ID="lblStudent" runat="server" Text='<%# Eval("StudentCount") %>'></asp:Label></p>
                                                        </div>
                                                    </div>
                                                </asp:HyperLink>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                <%--<div class="col-md-6">
				   <div class="class-box feature">
					<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="school_home_student_class.aspx">
					<div class="row">
						<div class="col-md-4 col-xs-4">
						<asp:Image ID="Image3" runat="server" ImageUrl="img/icon_class.png" width="100%" />
						</div>
						<div class="col-md-8 col-xs-8" style="padding-left:0px;">
						<h3>Class 1 <span><asp:Image ID="Image4" runat="server" ImageUrl="img/icon_right_arrow_gray.png" width="30px"/></span></h3>
						<p>Students: 65</p>										
						</div>
						</div>
					</asp:HyperLink>
				   </div>
			   </div>
			   <div class="col-md-6">
				   <div class="class-box feature">
					<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="school_home_student_class.aspx">
					<div class="row">
						<div class="col-md-4 col-xs-4">
						<asp:Image ID="Image5" runat="server" ImageUrl="img/icon_class.png" width="100%" />
						</div>
						<div class="col-md-8 col-xs-8" style="padding-left:0px;">
						<h3>Class 1 <span><asp:Image ID="Image6" runat="server" ImageUrl="img/icon_right_arrow_gray.png" width="30px"/></span></h3>
						<p>Students: 65</p>										
						</div>
						</div>
					</asp:HyperLink>
				   </div>
			   </div>
			   <div class="col-md-6">
				   <div class="class-box feature">
					<asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="school_home_student_class.aspx">
					<div class="row">
						<div class="col-md-4 col-xs-4">
						<asp:Image ID="Image7" runat="server" ImageUrl="img/icon_class.png" width="100%" />
						</div>
						<div class="col-md-8 col-xs-8" style="padding-left:0px;">
						<h3>Class 1 <span><asp:Image ID="Image8" runat="server" ImageUrl="img/icon_right_arrow_gray.png" width="30px"/></span></h3>
						<p>Students: 65</p>										
						</div>
						</div>
					</asp:HyperLink>
				   </div>
			   </div>
			   <div class="col-md-6">
				   <div class="class-box feature">
					<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="school_home_student_class.aspx">
					<div class="row">
						<div class="col-md-4 col-xs-4">
						<asp:Image ID="Image9" runat="server" ImageUrl="img/icon_class.png" width="100%" />
						</div>
						<div class="col-md-8 col-xs-8" style="padding-left:0px;">
						<h3>Class 1 <span><asp:Image ID="Image10" runat="server" ImageUrl="img/icon_right_arrow_gray.png" width="30px"/></span></h3>
						<p>Students: 65</p>										
						</div>
						</div>
					</asp:HyperLink>
				   </div>
			   </div>
			   <div class="col-md-6">
				   <div class="class-box feature">
					<asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="school_home_student_class.aspx">
					<div class="row">
						<div class="col-md-4 col-xs-4">
						<asp:Image ID="Image11" runat="server" ImageUrl="img/icon_class.png" width="100%" />
						</div>
						<div class="col-md-8 col-xs-8" style="padding-left:0px;">
						<h3>Class 1 <span><asp:Image ID="Image12" runat="server" ImageUrl="img/icon_right_arrow_gray.png" width="30px"/></span></h3>
						<p>Students: 65</p>										
						</div>
						</div>
					</asp:HyperLink>
				   </div>
			   </div>
			   <div class="col-md-6">
				   <div class="class-box feature">
					<asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="school_home_student_class.aspx">
					<div class="row">
						<div class="col-md-4 col-xs-4">
						<asp:Image ID="Image13" runat="server" ImageUrl="img/icon_class.png" width="100%" />
						</div>
						<div class="col-md-8 col-xs-8" style="padding-left:0px;">
						<h3>Class 1 <span><asp:Image ID="Image14" runat="server" ImageUrl="img/icon_right_arrow_gray.png" width="30px"/></span></h3>
						<p>Students: 65</p>										
						</div>
						</div>
					</asp:HyperLink>
				   </div>
			   </div>
			   <div class="col-md-6">
				   <div class="class-box feature">
					<asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="school_home_student_class.aspx">
					<div class="row">
						<div class="col-md-4 col-xs-4">
						<asp:Image ID="Image15" runat="server" ImageUrl="img/icon_class.png" width="100%" />
						</div>
						<div class="col-md-8 col-xs-8" style="padding-left:0px;">
						<h3>Class 1 <span><asp:Image ID="Image16" runat="server" ImageUrl="img/icon_right_arrow_gray.png" width="30px"/></span></h3>
						<p>Students: 65</p>										
						</div>
						</div>
					</asp:HyperLink>
				   </div>
			   </div>--%>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
