<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentAllPicker.aspx.cs" Inherits="SchoolApp.Web.Parent.StudentAllPicker" %>


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
                font-size: 14px;
            }

            .class-box h4 {
                width: 75%;
                float: left;
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

        .buttons .btn {
            font-size: 12px;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 58px;
            height: 30px;
        }

            .switch input {
                display: none;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 2px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            / 
                            <asp:HyperLink ID="hlinkManagePicker" runat="server" NavigateUrl="~/Parent/ManagePickers.aspx" Text="Manage Picker"></asp:HyperLink>
                            / <span>All Pickers</span></h1>

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
                        <div class="col-md-3">
                            <asp:Button ID="btnCreatePicker" runat="server" class="btn btn-primary btn-block" Style="border: none; padding: 10px;" Text="+ Create New Picker" OnClick="btnCreatePicker_Click"></asp:Button>
                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12 text-right" style="margin-bottom: 20px;">
                                                <h3 class="pull-left pageheader">Pickers</h3>
                                            </div>
                                            <asp:ListView ID="lstStudentPicker" runat="server" OnPagePropertiesChanging="lstStudentPicker_PagePropertiesChanging" OnItemCommand="lstStudentPicker_ItemCommand">
                                                <GroupTemplate>
                                                    <div runat="server" id="itemPlaceholder"></div>
                                                </GroupTemplate>
                                                <ItemTemplate>
                                                    <div class="col-md-6">
                                                        <div class="class-box feature" style='<%# (Eval("PickerType").ToString() == "2") ? "border:1px solid #2196F3": "border:1px solid #cccccc" %>'>
                                                            <div class="row">
                                                                <div class="col-md-4 col-xs-4">
                                                                    <%--<img src='<%# "../" + Eval("Photo") %>' class="img-circle" width="100%" />--%>
                                                                    <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("PickerName") %>'>
                                                                        <img class="example-image img-circle responsives" src='<%# "../" + Eval("Photo") %>' alt="" /></a>
                                                                    <asp:FileUpload ID="fpUploads" runat="server" Enabled='<%# Eval("IsPickerCreatedByMe") %>' CssClass="btn btn-Primary" Width="100%" onchange="UploadFileNow()" />

                                                                    <asp:HiddenField ID="HIDImage" runat="server" Value='<%# Eval("ID") %>' />
                                                                    <%--<asp:HiddenField ID="HIDPickerType" runat="server" Value='<%# Eval("PickerType") %>' />--%>
                                                                    <%--  <label class="text-center btn-block" style="margin-top: 10px; background: white; border: 1px solid #cccccc; color: #232323; border-radius: 5px;">
                                                                        Status<br />
                                                                        <span style="color: #00a7ff;"><%# Eval("ActiveStatus") %></span></label>--%>
                                                                </div>
                                                                <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                                    <h4 data-toggle="tooltip" title='<%# Eval("PickerName") %>' style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><b><%# Eval("PickerName") %></b></h4>
                                                                    <asp:HiddenField ID="chkID" runat="server" Value='<%# Eval("ID") %>' />
                                                                    <label class="switch">
                                                                        <%-- <input type="checkbox" checked>--%>
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Checked='<%# Eval("Active") %>' Enabled='<%# Eval("IsPickerCreatedByMe") %>' OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                                        <span class="slider round" style='<%# Convert.ToBoolean(Eval("IsPickerCreatedByMe")) ? "background: lightgray1;": "background: lightgray;"%>'></span>
                                                                    </label>
                                                                    

                                                                        <p data-toggle="tooltip" title='<%# Eval("Email") %>' style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><i class="fa fa-envelope" aria-hidden="true"></i>&nbsp;<%# (Eval("Email").ToString() != " Enter Email") || !Convert.ToBoolean(Eval("IsPickerCreatedByMe")) ? Eval("Email") :  "<a href='EditPicker.aspx?ID=" + Eval("ID") + "'>Enter Email</a>"%> </p>
                                                                        <p><i class="fa fa-phone" aria-hidden="true"></i>&nbsp;<%# (Eval("Phone").ToString() != " Enter Phone Number" || !Convert.ToBoolean(Eval("IsPickerCreatedByMe"))) ? "&nbsp;" + Eval("Phone") :  "<a href='EditPicker.aspx?ID=" + Eval("ID") + "'> Enter Phone Number</a>"%></p>

                                                                    
                                                                    <p><b>Created By :</b> <%# Eval("ParentName") %></p>
                                                                    <div style="margin: 10px 0px;">
                                                                        <%--<a href="EditPicker.aspx?ID=<%# Eval("ID") %>" class="btn btn-primary" style="background: white; border: 1px solid #cccccc; color: #232323; width: 49%;"><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp; Edit</a>--%>
                                                                        <asp:LinkButton ID="lnkEdit" CommandName="lnkEdit" CommandArgument='<%# Eval("ID") %>' Enabled='<%# Eval("IsPickerCreatedByMe") %>' runat="server" CssClass='<%# Convert.ToBoolean(Eval("IsPickerCreatedByMe")) ? "btn btn-primary" :"btn btn-primary"%>'
                                                                            Style='<%# Convert.ToBoolean(Eval("IsPickerCreatedByMe")) ? "background: white; border: 1px solid #cccccc; color: #232323; width: 49%;": "background: lightgray; border: 1px solid #cccccc; color: #A9A9A9; width: 49%;" %>'>
                                                                            <i class="fa fa-pencil-square-o" aria-hidden="true"></i> &nbsp; Edit</asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDelete" CommandName="lnkDelete" CommandArgument='<%# Eval("ID") %>' CausesValidation="true" OnClientClick= "return ConfirmMsg(this);"  runat="server" Enabled='<%# Eval("IsPickerCreatedByMe") %>' CssClass='<%# Convert.ToBoolean(Eval("IsPickerCreatedByMe")) ? "btn btn-primary" :"btn btn-primary"%>'
                                                                            Style='<%# Convert.ToBoolean(Eval("IsPickerCreatedByMe")) ? "background: white; border: 1px solid #cccccc; color: #232323; width: 49%;": "background: lightgray; border: 1px solid #cccccc; color: #A9A9A9; width: 49%;" %>'>
                                                                            <i class="fa fa-trash" aria-hidden="true"></i> &nbsp; Delete</asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row buttons">
                                                                <div class="col-md-12">
                                                                    <asp:LinkButton ID="lnkEditAssignment" CommandName="lnkEditAssignment" CommandArgument='<%# Eval("ID") %>' runat="server" class="btn btn-primary btn-block">Edit Children Assignment</asp:LinkButton>
                                                                    <%--<a href="EditChildrenAssignment.aspx?ID=<%# Eval("ID") %>" class="btn btn-primary btn-block">Edit Children Assignment</a>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <LayoutTemplate>
                                                    <table runat="server" class="col-md-12">
                                                        <tr runat="server">
                                                            <td runat="server">
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
                </div>
            </div>
        </div>
    </section>
    <script>
        function ConfirmMsg(obj) {
                if (confirm("Do you really want to delete this Picker?")) {
                    return true;
                }
                else {
                    return false;
                }

        };

        function UploadFileNow() {

            var value = $("#fpUpload").val();

            if (value != '') {

                $("#form1").submit();

            }
        };
    </script>
</asp:Content>

