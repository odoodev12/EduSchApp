<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pickup.aspx.cs" Inherits="SchoolApp.Web.Teacher.Pickup" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <script type="text/javascript">
        $('#inputDate').DatePicker({
            format: 'm/d/Y',
            date: $('#inputDate').val(),
            current: $('#inputDate').val(),
            starts: 1,
            position: '',
            onBeforeShow: function () {
                $('#inputDate').DatePickerSetDate($('#inputDate').val(), true);
            },
            onChange: function (formated, dates) {
                $('#inputDate').val(formated);
                if ($('#closeOnSelect input').attr('checked')) {
                    $('#inputDate').DatePickerHide();
                }
            }
        });
    </script>
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

        .responsive {
            width: 100px !important;
            max-width: 100px !important;
            height: 100px !important;
            max-height: 100px !important;
        }
        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
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
                        <h1 id="homeHeading"><asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> Teacher</asp:HyperLink> / <span>Pickup</span></h1>
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
                            <div class="form-group">
                                <asp:Label runat="server" for="studentname">Select Class</asp:Label>
                                <asp:DropDownList ID="drpClass" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:Label runat="server" for="studentname">Select Student</asp:Label>
                                <asp:DropDownList ID="drpStudentName" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpStudentName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <hr />
                            <div class="form-group">
                                <asp:Label runat="server" for="">Date:</asp:Label>&nbsp;&nbsp;
                                <%--<asp:TextBox runat="server" class="form-control" ID="txtDate" ReadOnly="true"></asp:TextBox>--%>
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-9">
                            <div class="well">
                                <h4>Complete Pickup Run
                                    <label class="switch pull-right">
                                        <asp:CheckBox ID="chkPicked" runat="server" onclick="return ConfirmMsg();" OnCheckedChanged="chkPicked_CheckedChanged" AutoPostBack="true" />
                                        <span class="slider round"></span>
                                    </label>
                                </h4>
                            </div>
                            <div class="row">
                                <asp:ListView ID="lstPickUp" runat="server" OnPagePropertiesChanging="lstPickUp_PagePropertiesChanging">
                                    <GroupTemplate>
                                        <div runat="server" id="itemPlaceholder"></div>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <a class="example-image-link" href='<%# "../" + Eval("StudentPic") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                            <img class="img-circle responsive" src='<%# "../" + Eval("StudentPic") %>' alt="" /></a>
                                                        <%--<img src='<%# "../" + Eval("StudentPic") %>' class="img-circle" width="100%" />--%>
                                                    </div>
                                                    <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                        <h3><%# Eval("StudentName") %></h3>
                                                        <p><%# Eval("PickStatus") %></p>
                                                        <p>Parent Message <span class="badge badge-danger"><%# Eval("ParentMessage") %></span></p>
                                                        <div class="buttons">
                                                            <a href="Pickers.aspx?ID=<%# Eval("StudentID") %>" class="btn btn-primary">View Pickers</a>
                                                        </div>
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
    </section>
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
    <script type="text/javascript">
        function ConfirmMsg() {
            if (confirm("Are you sure want to Activate? Students with Unpicked Pickup Status with Only be Marked as Picked(Late), Picked(Reportable), Picked(Chargeable) after this Action. Click No to Cancel Or Ok to Proceed.")) {
                //SetID(ID);
                __doPostBack('<%= chkPicked.ClientID %>', '');
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
