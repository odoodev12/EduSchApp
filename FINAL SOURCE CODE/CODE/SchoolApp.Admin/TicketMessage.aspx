<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="TicketMessage.aspx.cs" Inherits="SchoolApp.Admin.TicketMessage" %>

<%@ Register Src="~/UserControl/Loader.ascx" TagPrefix="uc1" TagName="Loader" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .trs {
            padding-bottom: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="HID" runat="server" />
    <div class="row  animated bounceInRight">
        <%--<asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="MainUpdatePanel">
            <ProgressTemplate>
                <uc1:Loader runat="server" ID="Loader" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="MainUpdatePanel" runat="server">
            <ContentTemplate>--%>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="panel panel-default">
                        <div class="panel-heading additional-padding">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h4 class="panel-title">
                                        <i class="fa fa-th-large"></i>&nbsp; <span>Ticket Message</span>
                                    </h4>
                                </div>
                            </div>
                        </div>

                        <div class="panel-body no-padding-bottom">
                            <div class="row">
                                <div class="col-lg-7">
                                    <div class="float-e-margins">
                                        <div class="form-horizontal formelement">
                                            <div class="ibox-title">
                                                <h3>Message</h3>
                                            </div>
                                            <div class="social-feed-box">
                                                <div class="social-avatar">
                                                    <h4>
                                                        <asp:Literal ID="litCreatedBy" runat="server"></asp:Literal>
                                                    </h4>
                                                </div>
                                                <div class="social-body">
                                                    <p style="overflow-wrap: break-word !important;">
                                                        <asp:Literal ID="litRequest" runat="server"></asp:Literal>
                                                    </p>
                                                </div>
                                                <div class="social-footer">

                                                    <div class="social-comment">
                                                        <div class="media-body">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Reply</strong>
                                                                        <br />
                                                                        Enter Reply.</span></a>Reply<span class="color-red">*</span></label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtReply" autocomplete="off" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" Width="85%"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtReply" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Reply Message Required</span></a>"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Upload Image</strong>
                                                                        <br />
                                                                        Select Image for Upload.</span></a>Upload Image<span class="color-red">*</span></label>
                                                                <div class="col-sm-9">
                                                                    <asp:FileUpload ID="UploadFile" runat="server" CssClass="form-control" Width="85%" Height="15%" />
                                                                </div>
                                                            </div>
                                                            <br />
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="panel-body no-padding-top">
                                                    <div class="hr-line-dashed"></div>
                                                    <div class="form-group no-bottom-margin">
                                                        <label class="col-sm-10 control-label width-160"></label>
                                                        <div class="col-sm-2 pull-right">
                                                            <asp:Button ID="btnPostReply" CssClass="btn btn-primary" ValidationGroup="Submit" runat="server" Text="Post Reply" OnClick="btnPostReply_Click" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Ticket Number</strong>
                                                        <br />
                                                        Enter Ticket Number.</span></a>Ticket Number<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtTicketNo" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Ticket Number Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Request</strong>
                                                        <br />
                                                        Enter Request.</span></a>Request<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRequest" TextMode="MultiLine" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtRequest" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Request Message Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-5">
                                    <div class="float-e-margins">
                                        <div class="form-horizontal formelement">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-title">
                                                    <h5>Ticket Summary</h5>
                                                </div>
                                                <div class="ibox-content">
                                                    <table class="table table-hover table-responsive">
                                                        <tbody>
                                                            <tr>
                                                                <td><b>Ticket No</b></td>
                                                                <td>
                                                                    <asp:Literal ID="litTicketNo" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Create By</b></td>
                                                                <td>
                                                                    <asp:Literal ID="litCreated" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Date</b></td>
                                                                <td>
                                                                    <asp:Literal ID="litDate" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Request</b></td>
                                                                <td>
                                                                    <asp:Literal ID="litAllRequests" runat="server"></asp:Literal>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Status</b></td>
                                                                <td>
                                                                    <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Priority</b></td>
                                                                <td>
                                                                    <asp:Literal ID="litPriority" runat="server"></asp:Literal>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <asp:Panel ID="PanelAdmin" runat="server">
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label">
                                                        <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Priority</strong>
                                                            <br />
                                                            Select Priority.</span></a>Priority<span class="color-red">*</span></label>
                                                    <div class="col-sm-7">
                                                        <asp:DropDownList ID="drpPriority" CssClass="form-control" Width="80%" runat="server">
                                                            <asp:ListItem Value="0" Text="Select Priority"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Critical"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="High"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Medium"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Low"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" CssClass="alert validation" runat="server" ValidationGroup="Save" InitialValue="0" ControlToValidate="drpPriority" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Priority Required</span></a>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label">
                                                        <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Assign To</strong>
                                                            <br />
                                                            Select Assign To Support Officer.</span></a>Assign To<span class="color-red">*</span></label>
                                                    <div class="col-sm-7">
                                                        <asp:DropDownList ID="drpSupportOfficer" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" CssClass="alert validation" runat="server" ValidationGroup="Save" InitialValue="0" ControlToValidate="drpSupportOfficer" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Assign to Required</span></a>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Ticket Summary Status</strong>
                                                        <br />
                                                        Select Ticket Summary Status.</span></a>Ticket Summary Status<span class="color-red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="drpStatus" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator6" CssClass="alert validation" runat="server" ValidationGroup="Save" InitialValue="0" ControlToValidate="drpStatus" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Status Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <%--<div class="form-group">
                                                <label class="col-sm-5 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Assigned By</strong>
                                                        <br />
                                                        Assigned By.</span></a>Assigned By</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblAssignedBy" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Assigned Date</strong>
                                                        <br />
                                                        Assigned Date.</span></a>Assigned Date</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblAssignedDate" runat="server"></asp:Label>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body no-padding-top">
                            <div class="hr-line-dashed"></div>
                            <div class="form-group no-bottom-margin">
                                <label class="col-sm-10 control-label width-160"></label>
                                <div class="col-sm-2 pull-right">
                                    <asp:Button ID="btnUpdateStatus" CssClass="btn btn-primary" ValidationGroup="Save" runat="server" Text="Update Status" OnClick="btnUpdateStatus_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
