<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="SchoolInvoiceEdit.aspx.cs" Inherits="SchoolApp.Admin.SchoolInvoiceEdit" %>

<%@ Register Src="~/UserControl/Loader.ascx" TagPrefix="uc1" TagName="Loader" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="HID" runat="server" />
    <div class="row  animated bounceInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="panel panel-default">
                        <div class="panel-heading additional-padding">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h4 class="panel-title">
                                        <i class="fa fa-th-large"></i>&nbsp; <span>Edit School Invoice</span>
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
                                                        Enter School Name.</span></a>School Name</label>
                                                <label class="col-sm-8 control-label">
                                                    <asp:Label ID="lblSchoolName" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Invoice No</strong>
                                                        <br />
                                                        Enter Invoice No.</span></a>Invoice No</label>
                                                 <label class="col-sm-8 control-label">
                                                    <asp:Label ID="lblInvoiceNo" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Invoice Date From</strong><br />
                                                        Enter Invoice Date From.</span></a>Invoice Date From
                                                </label>
                                                <label class="col-sm-8 control-label">
                                                    <asp:Label ID="lblInvFromDate" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Invoice Date To</strong><br />
                                                        Enter Invoice Date To.</span></a>Invoice Date To
                                                </label>
                                                <label class="col-sm-8 control-label">
                                                    <asp:Label ID="lblInvToDate" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Type</strong>
                                                        <br />
                                                        Select Transaction Type.</span></a>Transaction Type</label>
                                                <div class="col-sm-8 control-label" style="font-weight:600">
                                                    <asp:Label ID="lblTransactionType" runat="server"></asp:Label>
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
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Desc</strong>
                                                        <br />
                                                        Enter Transaction Desc.</span></a>Transaction Desc</label>
                                                <div class="col-sm-8 control-label" style="font-weight:600;white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                                    <asp:Label ID="lblTranDesc" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Amount</strong>
                                                        <br />
                                                        Enter Transaction Amount.</span></a>Transaction Amount</label>
                                                <div class="col-sm-8 control-label" style="font-weight:600">
                                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Tax Rate</strong>
                                                        <br />
                                                        Enter Tax Rate.</span></a>Tax Rate</label>
                                                <div class="col-sm-8 control-label" style="font-weight:600">
                                                    <asp:Label ID="lblRate" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Status</strong>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
