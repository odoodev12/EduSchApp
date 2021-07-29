<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SchoolApp.Admin.Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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

        .color_blue {
            color: #00a7ff;
        }

        .color_red {
            color: #FF0000;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label for="classname">Select School</label>
                        <asp:DropDownList ID="drpSchoolList" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpSchoolList" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                    </div>


                    <div class="form-group">
                        <label for="classname">Select Report</label>
                        <asp:DropDownList ID="drpReportType" runat="server" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem Text="Select Report" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Deleted Student Report" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Deleted Parent Report" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Deleted Picker Report" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpReportType" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                    </div>

                </div>
            </div>
            <div class="row" style="text-align: center; margin-bottom: 15px;">
                <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Preview" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnExportToExcel" runat="server" class="btn btn-primary" Text="Export To Excel" ValidationGroup="Submit" OnClick="btnExportToExcel_Click" />
            </div>
            <div id="tablediv" class="col-md-12" style="margin-top: 10px;" runat="server">
                
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>




</asp:Content>
