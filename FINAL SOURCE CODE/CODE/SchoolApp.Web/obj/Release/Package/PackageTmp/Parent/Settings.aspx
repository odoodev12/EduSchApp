<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="SchoolApp.Web.Parent.Settings" %>


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
                margin-top: 10px;
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

            .feature a {
                text-decoration: none;
            }

        .tital {
            font-size: 16px;
            font-weight: 500;
            color: #00a7ff;
        }

        .bot-border {
            border-bottom: 3px #f8f8f8 solid;
            margin: 15px 0 5px 0
        }

        .picture {
            border-radius: 8px;
        }

        .tital .btn {
            padding: 10px;
            margin-top: 20px;
        }

        .modal-body {
            padding: 15px;
        }

        .card.hovercard {
            position: relative;
            padding-top: 0;
            overflow: hidden;
            text-align: center;
            background-color: rgba(214, 224, 226, 0.2);
        }

            .card.hovercard .cardheader {
                background: url("../img/4.jpg");
                background-size: cover;
                height: 135px;
            }

            .card.hovercard .avatar {
                position: relative;
                top: -50px;
                margin-bottom: -50px;
            }

                .card.hovercard .avatar img {
                    width: 100px;
                    height: 100px;
                    max-width: 100px;
                    max-height: 100px;
                    -webkit-border-radius: 50%;
                    -moz-border-radius: 50%;
                    border-radius: 50%;
                    border: 5px solid rgba(255, 255, 255, 0.5);
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
                            / <span>Settings</span></h1>
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
                        <div class="col-md-3 col-sm-6">

                            <div class="card hovercard ">
                                <div class="cardheader">
                                </div>
                                <div class="avatar">
                                    <img id="img1" runat="server" class="img-circle responsives" />
                                </div>
                                <div class="info">
                                    <p>
                                        <asp:Literal ID="litStudent" runat="server"></asp:Literal>
                                    </p>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12 text-right" style="margin-bottom: 20px;">
                                                <h3 class="pull-left page-header">Email Settings</h3>
                                            </div>
                                            <%--<div class="col-md-12 text-left">
                                                <div class=" col-md-5 tital">Select School Name : </div>
                                                <div class=" col-md-6">
                                                    <asp:DropDownList runat="server" ID="drpSchoolName" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>--%>
                                            <div class="col-md-12 text-left">


                                                <div class=" col-md-5 tital">Receive Email After Confirm Pickup : </div>
                                                <div class=" col-md-6">
                                                    <asp:RadioButton ID="email_confirmation_radio_1" Text="Yes" GroupName="Email_confirmation" runat="server" />
                                                    &nbsp;
                                                        <asp:RadioButton ID="email_confirmation_radio_2" Text="No" GroupName="Email_confirmation" runat="server" />
                                                </div>

                                                <div class="col-md-5 tital">Receive Email After Confirm Attendance : </div>
                                                <div class="col-md-6">
                                                    <asp:RadioButton ID="email_Attendence_marked_1" Text="Yes" GroupName="Email_attendence" runat="server" />
                                                    &nbsp; 
                                                        <asp:RadioButton ID="email_Attendence_marked_2" Text="No" GroupName="Email_attendence" runat="server" />
                                                </div>

                                                <div class="col-md-12 tital"></div>
                                                <div class="col-md-12">
                                                    <br />
                                                    <center><asp:Button class="btn btn-primary" ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" /></center>
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
        </div>
    </section>
</asp:Content>
