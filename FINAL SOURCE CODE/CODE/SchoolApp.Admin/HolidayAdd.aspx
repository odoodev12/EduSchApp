<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="HolidayAdd.aspx.cs" Inherits="SchoolApp.Admin.HolidayAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row  animated bounceInRight">
        
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="panel panel-default">
                        <div class="panel-heading additional-padding">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h4 class="panel-title">
                                        <i class="fa fa-th-large"></i>&nbsp; <span>Add Holiday</span>
                                    </h4>
                                </div>
                            </div>
                        </div>

                        <div class="panel-body no-padding-bottom">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="float-e-margins">
                                        <div class="form-horizontal formelement">
                                            <%--<div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>UserType</strong>
                                                                <br />
                                                                Select User Type.</span></a>UserType<span class="color-red">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="drpUserType" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpUserType" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>User Type Required</span></a>"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>--%>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Holiday Name</strong>
                                                        <br />
                                                        Enter Holiday Name.</span></a>Holiday Name<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtHolidayName" autocomplete="off" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtHolidayName" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Holiday Name Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                           <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>From Date</strong><br />
                                                        Enter Holiday From Date.</span></a>From Date<span class="color-red">*</span>
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtFromDate" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="datedop" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator11" CssClass="alert validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtFromDate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Holiday From Date Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>To Date</strong><br />
                                                        Enter Holiday To Date.</span></a>To Date<span class="color-red">*</span>
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtToDate" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" />
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" CssClass="alert validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtToDate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Holiday To Date Required</span></a>"></asp:RequiredFieldValidator>
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
                                           <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Reset" OnClick="btnReset_Click" />
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
