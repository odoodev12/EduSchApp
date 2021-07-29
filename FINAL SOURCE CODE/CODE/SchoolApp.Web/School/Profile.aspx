<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="SchoolApp.Web.School.Profile" %>

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
            margin: 5px 0 5px 0
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
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> School</asp:HyperLink>
                            / <span>Profile</span></h1>
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
                        <div class="col-md-3 col-sm-6">

                            <div class="card hovercard ">
                                <div class="cardheader">
                                </div>
                                <div class="avatar">

                                    <a id="AID" runat="server" class="example-image-link" data-lightbox="example-set">
                                        <asp:Image ID="Image1" runat="server" class="example-image img-circle responsives" AlternateText="" /></a>
                                    <asp:FileUpload ID="fpUploadLogo" runat="server" CssClass="btn btn-primary" onchange="UploadFileNow()" />
                                    <%--<h6>Upload a different photo...</h6>
                                    <input type="file" class="text-center center-block hidden well well-sm">--%>
                                </div>
                                <div class="info">

                                    <p>
                                        <asp:Literal ID="litSchoolName" runat="server"></asp:Literal>
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
                                                <h3 class="pull-left page-header">Profile Info</h3>
                                                <asp:LinkButton ID="lnkChangeProfile" runat="server" class="btn btn-primary" PostBackUrl="~/School/SupportlogCreate.aspx"><span class="fa fa-edit"></span>&nbsp;Request Change to Profile</asp:LinkButton>
                                                <%--<button class="btn btn-primary" data-toggle="modal" data-target="#editclassmodel"><span class="fa fa-edit"></span>Edit</button>--%>
                                                <asp:LinkButton ID="lbtn1" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#editclassmodel"><span class="fa fa-edit"></span>&nbsp;Edit</asp:LinkButton>
                                            </div>
                                            <div class="col-md-6 text-left">
                                                <div class=" col-md-12 tital">Customer Number:</div>
                                                <div class=" col-md-12 ">
                                                    <asp:Literal ID="litCustomerNo" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">School Name:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSchoolName1" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">School Number:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSchoolNo" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>


                                                <div class="col-md-12 tital">School Type:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSchoolType" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>


                                                <div class="col-md-12 tital">School Status:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSchoolStatus" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Phone Number:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPhoneNo" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">School Address:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSchAddress" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Town:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litTown" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Postcode:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPostCode" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Country:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litCountry" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">School Billing Address:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSchBillingAddress" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">School Billing Town:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSchBillingTown" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Billing Postcode:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litBillingPostCode" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                            </div>
                                            <div class="col-md-6 text-left">
                                                <div class="col-md-12 tital">School Billing Country:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSchBillingCountry" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">School Website:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litWebsite" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Admin Supervisor Name:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSupervisorName" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Admin Supervisor Email:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litSupervisorEmail" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Admin Name:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litAdminName" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Admin Email:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litAdminEmail" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Opening Time:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litOpeningTime" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Closing Time:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litClosingTime" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Late Minutes After Closing:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litLateMinAftClosing" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Charge Minutes After Closing:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litChargeMinAftClosing" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Portable Minutes After Closing:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litPortableMinAftClosing" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Notification After Confirmed Pickup:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litNotyAftConfirmPickup" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>

                                                <div class="col-md-12 tital">Notification After Confirmed Attendance:</div>
                                                <div class="col-md-12">
                                                    <asp:Literal ID="litNotyAftConfirmAttedance" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="bot-border"></div>
                                            </div>
                                            <%--<asp:Panel ID="PanelStandard" runat="server">
                                                <div class="col-md-3 text-center">
                                                </div>
                                                <div class="col-md-6 text-center">
                                                    <div class="col-md-12 tital">Attendance Module Activated:</div>
                                                    <div class="col-md-12">
                                                        <asp:Literal ID="litAttendenceModule" runat="server"></asp:Literal>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="bot-border"></div>
                                                </div>
                                                <div class="col-md-3 text-center">
                                                </div>
                                            </asp:Panel>--%>
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
    <div class="modal fade" id="editclassmodel" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title" id="lineModalLabel">Edit Class</h3>
                </div>
                <div class="modal-body">

                    <!-- content goes here -->

                    <div class="form-group">
                        <label for="classname">Opening Time:</label>
                        <asp:TextBox ID="txtOpeningTime" runat="server" class="form-control" placeholder="10 AM"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="classname">Closing Time:</label>
                        <asp:TextBox ID="txtClosingTime" runat="server" CssClass="form-control" placeholder="10 AM"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="classname">Late Minutes After Closing:</label>
                        <%--<asp:TextBox ID="txtLateMinAftClosing" runat="server" class="form-control" placeholder="10 Minutes"></asp:TextBox>--%>
                        <asp:DropDownList ID="drpLateAfterClose" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="classname">Charge Minutes After Closing:</label>
                        <%--<asp:TextBox ID="txtChargeMinAftClosing" runat="server" class="form-control" placeholder="10 Minutes"></asp:TextBox>--%>
                        <asp:DropDownList ID="drpChargeAfterClose" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="classname">Reportable Minutes After Closing:</label>
                        <%--<asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="10 Minutes"></asp:TextBox>--%>
                        <asp:DropDownList ID="drpReportMinsAfterClose" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="classyear">Notification After Confirmed Pickup:</label>
                        <asp:DropDownList ID="drpNotifyAftPickup" runat="server" class="form-control">
                            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="classyear">Notification After Confirmed Attendance:</label>
                        <asp:DropDownList ID="drpNotifyAftAttendance" runat="server" class="form-control">
                            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <%--<asp:Panel ID="PanelStandard1" runat="server">
                    <div class="form-group">
                        <label for="classyear">Want to Activate Attendance Module ?</label>
                        <asp:DropDownList ID="drpAttendanceModule" runat="server" class="form-control">
                            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    </asp:Panel>--%>

                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                        <div class="btn-group">
                            <button type="button" class="btn btn-default" data-dismiss="modal" role="button">Cancel</button>
                        </div>
                        <div class="btn-group">
                            <asp:Button ID="btnUPdate" runat="server" class="btn btn-primary" Text="Update" OnClick="btnUPdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script lang="javascript" type="text/javascript">

        function UploadFileNow() {

            var value = $("#fpUploadLogo").val();

            if (value != '') {

                $("#form1").submit();

            }

        };

    </script>
</asp:Content>
