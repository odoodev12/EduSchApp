<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="SchoolInvoiceAdd.aspx.cs" Inherits="SchoolApp.Admin.SchoolInvoiceAdd" %>

<%@ Register Src="~/UserControl/Loader.ascx" TagPrefix="uc1" TagName="Loader" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .rightform .control-label {
            padding-left: 0px !important;
            padding-right: 0px !important;
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
                                        <i class="fa fa-th-large"></i>&nbsp; <span>Add School Invoice</span>
                                    </h4>
                                </div>
                            </div>
                        </div>

                        <div class="panel-body no-padding-bottom">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="float-e-margins">
                                        <div class="form-horizontal formelement">

                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Name</strong>
                                                        <br />
                                                        Enter School Name.</span></a>School Name<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpSchool" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpSchool" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>School Required</span></a>"></asp:RequiredFieldValidator>
                                                    <%--<asp:TextBox ID="txtSchoolName" autocomplete="off" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtSchoolName" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>School Name Required</span></a>"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Invoice No</strong>
                                                        <br />
                                                        Enter Invoice No.</span></a>Invoice No<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtInvoiceNo" autocomplete="off" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtInvoiceNo" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>InvoiceNo Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Invoice Date From</strong><br />
                                                        Enter Invoice Date From.</span></a>Invoice Date From
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDateFrom" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateFrom" Format="dd/MM/yyyy" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtDateFrom" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Invoice Date From Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Invoice Date To</strong><br />
                                                        Enter Invoice Date To.</span></a>Invoice Date To
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDateTo" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateTo" Format="dd/MM/yyyy" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtDateTo" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Invoice Date To Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Type</strong>
                                                        <br />
                                                        Select Transaction Type.</span></a>Transaction Type<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpTransactionTypeID" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpTransactionTypeID" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Transaction Type Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="float-e-margins">
                                        <div class="form-horizontal rightform formelement">
                                            
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Desc</strong>
                                                        <br />
                                                        Enter Transaction Desc.</span></a>Transaction Desc<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtTransactionDesc" autocomplete="off" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtTransactionDesc" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Transaction Description Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Amount</strong>
                                                        <br />
                                                        Enter Transaction Amount.</span></a>Transaction Amount<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtTransactionAmount" autocomplete="off" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtTransactionAmount" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtTransactionAmount" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Transaction Amount Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Tax Rate</strong>
                                                        <br />
                                                        Enter Tax Rate.</span></a>Tax Rate</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtTaxRate" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="filterextender" runat="server" TargetControlID="txtTaxRate" FilterType="Numbers, Custom" ValidChars="." />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtTransactionAmount" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Tax Rate Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Status</strong>
                                                        <br />
                                                        Select Transaction Status.</span></a>Transaction Status<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpStatus" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpStatus" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Transaction status Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body no-padding-top">
                            <div class="hr-line-dashed"></div>
                            <div class="form-group no-bottom-margin">
                                <label class="col-sm-2 control-label width-160"></label>
                                <div class="col-sm-4">
                                    <asp:Button ID="btnSave" CssClass="btn btn-primary" ValidationGroup="Submit" runat="server" Text="Save Changes" OnClick="btnSave_Click" />&nbsp;&nbsp;
                                           <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnReset_Click" />
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
