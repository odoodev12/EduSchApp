<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePicker.aspx.cs" Inherits="SchoolApp.Web.Parent.CreatePicker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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

        /*#upload {
            display: none
        }*/
        .responsives {
            width: 100px !important;
            max-height: 100px !important;
            max-width: 100px !important;
            height: 100px !important;
        }
    </style>
    <script type="text/javascript">
        function TitleCase(objField) {
            var objValues = objField.value.split(" ");
            var outText = "";
            for (var i = 0; i < objValues.length; i++) {
                outText = outText + objValues[i].substr(0, 1).toUpperCase() + objValues[i].substr(1).toLowerCase() + ((i < objValues.length - 1) ? " " : "");
            }
            return outText;
        };

        function UploadFileNow() {
            var value = $("#fpUpload").val();
            if (value !== '') {
                $("#form1").submit();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading">
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            /
                            <asp:HyperLink ID="AddNewPicker" runat="server" NavigateUrl="StudentAllPicker.aspx" Text="Student All Picker"></asp:HyperLink>
                            / <span>Create Picker</span></h1>

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
                                    <asp:Image ID="UploadPhoto" runat="server" ImageUrl="../img/icon_upload_photo.png" class="img-circle responsives" />

                                </div>
                                <div class="info">
                                    <%--<a href="#" onclick="browse()">Upload Photo..</a>
                                    <div id="div1" style="display: none"></div>--%>


                                    <asp:FileUpload ID="fpUpload" runat="server" CssClass="btn btn-primary" />

                                </div>
                            </div>

                        </div>
                        <div class="col-md-9">
                            <div class="row">

                                <div class="class-box feature">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Picker Type</label>
                                                <asp:DropDownList ID="drpPickerType" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpPickerType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="Individual Picker" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Organisation Picker" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlIndivisualPicker" runat="server" Visible="true">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label for="classname">Picker Title</label>
                                                    <asp:DropDownList ID="drpTitle" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="Select Title" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                                        <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                                        <asp:ListItem Text="Miss." Value="Miss."></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6 ">
                                                <div class="form-group">
                                                    <label for="classname">First Name</label>
                                                    <asp:TextBox ID="txtFirstName" runat="server" onkeyup="this.value=TitleCase(this);" class="form-control" placeholder="Enter First Name" BorderColor="Red"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="txtFirstName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-6 ">
                                                <div class="form-group">
                                                    <label for="classname">Last Name</label>
                                                    <asp:TextBox ID="txtLastName" runat="server" onkeyup="this.value=TitleCase(this);" class="form-control" placeholder="Enter Last Name" BorderColor="Red"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="txtLastName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlOrganizationUser" runat="server" Visible="false">
                                            <div class="col-md-12 ">
                                                <div class="form-group">
                                                    <label for="classname">Picker Organisation Name</label>
                                                    <asp:TextBox ID="txtPickerOrganisationName" runat="server" class="form-control" placeholder="Enter Picker Organisation Name" BorderColor="Red"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" ControlToValidate="txtPickerOrganisationName" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>

                                                </div>
                                                <%--<div class="form-group">
                                                 &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox_idchkd" runat="server" />&nbsp;&nbsp;<label for="classname">ID Checked</label>
                                                    </div>--%>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="classname">Emergency Contact</label>
                                                <asp:DropDownList ID="drpContact" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label for="classname">Email</label>
                                                <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Enter Email"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ValidationGroup="Submit" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'>Enter Valid Email Address</a>"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label for="classname">Phone Number</label>
                                                <asp:TextBox ID="txtPhoneNumber" autocomplete="off" runat="server" class="form-control" placeholder="Enter Phone Numebr" MaxLength="15"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="+" TargetControlID="txtPhoneNumber" />
                                            </div>
                                        </div>
                                        <div class="col-md-12 ">
                                            <div class="form-group">
                                                <asp:CheckBox ID="chbOneOffPicker" runat="server" />

                                                <label for="">One Off Picker</label>
                                            </div>
                                        </div>



                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <asp:Button ID="btnAssignToChildren" runat="server" class="btn btn-primary" ValidationGroup="Submit" Text="Create and Assign" OnClick="btnAssignToChildren_Click" />
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
