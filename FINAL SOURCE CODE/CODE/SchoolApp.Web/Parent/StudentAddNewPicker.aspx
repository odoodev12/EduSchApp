<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentAddNewPicker.aspx.cs" Inherits="SchoolApp.Web.Parent.StudentAddNewPicker" %>

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
                background: url("img/4.jpg");
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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> / 
                            <asp:HyperLink ID="hlinkDavidByer" runat="server" NavigateUrl="ManagePickers.aspx">Manage Pickers</asp:HyperLink>
                            / <span>Add New Picker</span></h1>

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
                            <asp:HyperLink ID="hlinkCreateNewPicker" runat="server" NavigateUrl="CreatePicker.aspx" class="btn btn-primary btn-block" Style="border: none; padding: 10px;" Text="+ Create New Picker"></asp:HyperLink>
                        </div>
                        <div class="col-md-9">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="class-box feature">
                                        <div class="row">
                                            <div class="col-md-12 text-right" style="margin-bottom: 20px;">
                                                <h3 class="pull-left pageheader">Pickers</h3>
                                            </div>
                                            <asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging" OnItemCommand="ListView1_ItemCommand">
                                                <%--<GroupTemplate>

                                                    <td runat="server" id="itemPlaceholder"></td>

                                                </GroupTemplate>--%>
                                                <ItemTemplate>
                                                    <div class="col-md-6">
                                                        <div class="class-box feature">
                                                            <div class="row">
                                                                <div class="col-md-4 col-xs-4">
                                                                    <%--<asp:Image ID="img0" runat="server" class="img-circle" Width="100%" ImageUrl='<%# "../" +  Eval("Photo") %>' />--%>
                                                                    <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("PickerName") %>'>
                                                                        <img class="example-image img-circle responsives" src='<%# "../" + Eval("Photo") %>' alt="" /></a>

                                                                </div>
                                                                <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                                    <h4 style="font-size: medium !important;"><b><%# Eval("PickerName") %></b></h4>
                                                                    <asp:HiddenField ID="chkID" runat="server" Value='<%# Eval("ID") %>' />
                                                                    <%--<label class="switch">
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Checked='<%# Eval("Active") %>' OnCheckedChanged="CheckBox1_CheckedChanged"/>

                                                                        <span class="slider round"></span>
                                                                    </label>--%>
                                                                    <h4 style="font-size: small !important;"><b>Created By: </b><%# Eval("ParentName") %></h4> 
                                                                    <h4 style="font-size: small !important;"><b>Email: </b><%# Convert.ToString(Eval("Email")).Length == 0 ? "No Email" : Eval("Email") %></h4> 
                                                                    <%--<p><b>Created By :</b> <%# Eval("ParentName") %></p>--%>
                                                                    <div style="margin: 10px 0px;">
                                                                        <label class="text-center btn-block" style="padding: 5px 0px; background: white; border: 1px solid #cccccc; width: 49%; float: left; color: #232323; border-radius: 5px;">Status :<span style="color: #00a7ff;"><%# Eval("ActiveStatus") %></span></label>

                                                                        <asp:Button ID="btnAdd1" runat="server" CommandName="btnAdd" CommandArgument='<%# Eval("ID") %>' class="btn btn-primary pull-right" Style="width: 49%;" Text="Add" />
                                                                    </div>
                                                                </div>
                                                            </div>


                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <EmptyItemTemplate>
                                                    There are no Pickers to be added. Please create a new one
                                                </EmptyItemTemplate>
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
</asp:Content>
