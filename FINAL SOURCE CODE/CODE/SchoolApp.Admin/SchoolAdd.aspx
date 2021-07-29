<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="SchoolAdd.aspx.cs" Inherits="SchoolApp.Admin.SchoolAdd" %>

<%@ Register Src="~/UserControl/Loader.ascx" TagPrefix="uc1" TagName="Loader" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <link href="Content/css/plugins/clockpicker/clockpicker.css" rel="stylesheet">
    <link href="Content/css/plugins/iCheck/custom.css" rel="stylesheet">
    <link href="Content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css" rel="stylesheet">
    <link href="Content/css/plugins/steps/jquery.steps.css" rel="stylesheet">
    <style>
        .rightform .control-label {
            padding-left: 0px !important;
            padding-right: 0px !important;
        }

        #wizHeader li .prevStep {
            background-color: #669966;
        }

            #wizHeader li .prevStep:after {
                border-left-color: #669966 !important;
            }

        #wizHeader li .currentStep {
            background-color: #C36615;
        }

            #wizHeader li .currentStep:after {
                border-left-color: #C36615 !important;
            }

        #wizHeader li .nextStep {
            background-color: #C2C2C2;
        }

            #wizHeader li .nextStep:after {
                border-left-color: #C2C2C2 !important;
            }

        #wizHeader {
            list-style: none;
            overflow: hidden;
            font: 18px Helvetica, Arial, Sans-Serif;
            margin: 0px;
            padding: 0px;
        }

            #wizHeader li {
                float: left;
            }

                #wizHeader li a {
                    color: white;
                    text-decoration: none;
                    padding: 10px 0 10px 55px;
                    background: brown; /* fallback color */
                    background: hsla(34,85%,35%,1);
                    position: relative;
                    display: block;
                    float: left;
                }

                    #wizHeader li a:after {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent; /* Go big on the size, and let overflow hide */
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid hsla(34,85%,35%,1);
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        left: 100%;
                        z-index: 2;
                    }

                    #wizHeader li a:before {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent;
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid white;
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        margin-left: 1px;
                        left: 100%;
                        z-index: 1;
                    }

                #wizHeader li:first-child a {
                    padding-left: 10px;
                }

                #wizHeader li:last-child {
                    padding-right: 50px;
                }

                #wizHeader li a:hover {
                    background: #FE9400;
                }

                    #wizHeader li a:hover:after {
                        border-left-color: #FE9400 !important;
                    }

        .content {
            height: 150px;
            padding-top: 75px;
            text-align: center;
            background-color: #F9F9F9;
            font-size: 48px;
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
                                        <i class="fa fa-th-large"></i>&nbsp; <span>Add School</span>
                                    </h4>
                                </div>
                            </div>
                        </div>

                        <div class="panel-body no-padding-bottom">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="float-e-margins">
                                        <div class="form-horizontal formelement">

                                            <asp:Wizard ID="Wizard1" OnNextButtonClick="Wizard1_NextButtonClick" runat="server" DisplaySideBar="false" Width="100%">
                                                <HeaderTemplate>
                                                    <ul id="wizHeader">
                                                        <asp:Repeater ID="SideBarList" runat="server">
                                                            <ItemTemplate>
                                                                <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                                                    <%# Eval("Name")%></a> </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </HeaderTemplate>
                                                <WizardSteps>
                                                    <asp:WizardStep ID="WizardStep1" runat="server" Title="Section 1: General Information">

                                                        <div class="panel panel-default">

                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    <div class="float-e-margins">
                                                                        <div class="form-horizontal formelement">
                                                                            <br />
                                                                            <div class="form-group" id="pnlSelectAgency" runat="server">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Website</strong><br />
                                                                                        Enter School Website.</span></a>School Website <span class="color-red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtSchoolWebsite" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator8" CssClass="alert validation" runat="server" SetFocusOnError="true" ValidationGroup="Submit" ControlToValidate="txtSchoolWebsite" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Website required</span></a>"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtSchoolWebsite" ValidationExpression="^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Enter Valid WebSite</span></a>"></asp:RegularExpressionValidator>
                                                                                </div>

                                                                            </div>
                                                                            <div class="form-group" id="pnlAddAgency" runat="server">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School No</strong><br />
                                                                                        Enter School No.</span></a>School No <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtSchoolNo" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" CssClass="alert  validation" runat="server" SetFocusOnError="true" ValidationGroup="Submit" ControlToValidate="txtSchoolNo" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>School No required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Name</strong><br />
                                                                                        Enter School Name.</span></a>School Name <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtSchoolName" Width="80%" runat="server" onkeyup="this.value=TitleCase(this);" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" CssClass="alert  validation" runat="server" SetFocusOnError="true" ValidationGroup="Submit" ControlToValidate="txtSchoolName" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>SchoolName required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Logo</strong><br />
                                                                                        School Logo Upload 1506X554 size preferred.</span></a>School Logo</label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:FileUpload ID="fpUploadLogo" runat="server" CssClass="form-control" Width="80%" Height="15%" accept=".png,.jpg,.jpeg,.gif" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Admin First Name</strong><br />
                                                                                        Enter Admin First Name.</span></a>Admin First Name <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtAdminFName" Width="80%" runat="server" onkeyup="this.value=TitleCase(this);" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" CssClass="alert  validation" runat="server" ValidationGroup="Submit" SetFocusOnError="true" ControlToValidate="txtAdminFName" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Admin FirstName required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Admin Last Name</strong><br />
                                                                                        Enter Admin Last Name.</span></a>Admin Last Name <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtAdminLName" onkeyup="this.value=TitleCase(this);" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" CssClass="alert  validation" runat="server" ValidationGroup="Submit" SetFocusOnError="true" ControlToValidate="txtAdminLName" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Admin LastName required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Admin Email</strong><br />
                                                                                        Enter School Admin Email.</span></a>School Admin Email <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtAdminEmail" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator6" CssClass="alert  validation" runat="server" ValidationGroup="Submit" InitialValue="" ControlToValidate="txtAdminEmail" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Admin Email Address required</span></a>"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtAdminEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Enter Valid Admin Email</span></a>"></asp:RegularExpressionValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Password</strong>
                                                                                        <br />
                                                                                        Enter Password.</span></a>Password<span class="color-red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Width="80%"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ControlToValidate="txtPassword" ID="RegularExpressionValidator2" runat="server" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Password must be 8-10 characters long with at least one numeric,</br> one alphabet and one special character.</span></a>" ValidationExpression="(?=^.{8,10}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+}{:;'?/>.<,])(?!.*\s).*$"></asp:RegularExpressionValidator>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtPassword" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Password Required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Confirm Password</strong>
                                                                                        <br />
                                                                                        Enter Confirm Password.</span></a>Confirm Password<span class="color-red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="form-control" TextMode="Password" Width="80%"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtConfirmPass" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Confirm Password Required</span></a>"></asp:RequiredFieldValidator>
                                                                                    <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPass" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Password Does not Match</span></a>" Display="Dynamic" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Supervisor First Name</strong><br />
                                                                                        Enter Supervisor First Name.</span></a>Supervisor First Name <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtSupFName" Width="80%" onkeyup="this.value=TitleCase(this);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" CssClass="alert  validation" runat="server" ValidationGroup="Submit" SetFocusOnError="true" ControlToValidate="txtSupFName" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Supervisor FirstName required</span></a>"></asp:RequiredFieldValidator>
                                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtAdminFName" ControlToValidate="txtSupFName" Operator="NotEqual" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Supervisor First Name Must not be Same as Admin First Name</span></a>" Display="Dynamic" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <div class="float-e-margins">
                                                                        <div class="form-horizontal rightform formelement">
                                                                            <br />
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Supervisor Last Name</strong><br />
                                                                                        Enter Supervisor Last Name.</span></a>Supervisor Last Name <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtSupLName" onkeyup="this.value=TitleCase(this);" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator7" CssClass="alert  validation" runat="server" ValidationGroup="Submit" SetFocusOnError="true" ControlToValidate="txtSupLName" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Supervisor LastName required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Supervisor Email</strong><br />
                                                                                        Enter School Supervisor Email.</span></a>School Supervisor Email <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtSupEmail" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator9" CssClass="alert  validation" runat="server" ValidationGroup="Submit" InitialValue="" ControlToValidate="txtSupEmail" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Supervisor Email Address required</span></a>"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtSupEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Enter Valid Supervisor Email</span></a>"></asp:RegularExpressionValidator>
                                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtAdminEmail" ControlToValidate="txtSupEmail" Operator="NotEqual" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Supervisor Email Must not be Same as Admin Email</span></a>" Display="Dynamic" />
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Opening Times</strong><br />
                                                                                        Enter School Opening Times.</span></a>School Opening Times
                                                                                </label>
                                                                                <div class="col-sm-8 input-group clockpicker" data-autoclose="true">
                                                                                    <asp:TextBox ID="txtSchoolOpeningTime" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <%--<span class="input-group-addon">
                                                                                        <span class="fa fa-clock-o"></span>
                                                                                    </span>--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Closing Times</strong><br />
                                                                                        Enter School Closing Times.</span></a>School Closing Times
                                                                                </label>
                                                                                <div class="col-sm-8 input-group clockpicker" data-autoclose="true">
                                                                                    <%--<input type="text" class="form-control" value="09:30">--%>
                                                                                    <asp:TextBox ID="txtSchoolClosingTime" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <%--<asp:CompareValidator ID="cmpVal1" ControlToCompare="txtSchoolOpeningTime" ControlToValidate="txtSchoolClosingTime" Type="Date" Operator="GreaterThanEqual" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Select Time After then Opening Time</span></a>" runat="server"></asp:CompareValidator>--%>
                                                                                    <%--<span class="input-group-addon">
                                                                                            <span class="fa fa-clock-o"></span>
                                                                                        </span>--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Late Minutes After Closing</strong><br />
                                                                                        Enter Late Minutes After Closing.</span></a>Late Minutes After Closing
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <%--<asp:TextBox ID="txtLateMins" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                    <asp:DropDownList ID="drpLateAfterClose" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Charge Minutes After Closing</strong><br />
                                                                                        Enter Charge Minutes After Closing.</span></a>Charge Minutes After Close
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <%--<asp:TextBox ID="txtChargeMins" Width="80%" runat="server" CssClass="form-control" placeholder="Minutes"></asp:TextBox>--%>
                                                                                    <asp:DropDownList ID="drpChargeAfterClose" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Reportable Minutes After Closing</strong><br />
                                                                                        Enter Reportable Minutes After Closing.</span></a>Report Minutes After Close
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <%--<asp:TextBox ID="txtReporatbleMins" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                    <asp:DropDownList ID="drpReportMinsAfterClose" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>
                                                                                </div>
                                                                            </div>

                                                                            <%--<div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>End Date Account</strong><br />
                                                                                        Enter End Date Account.</span></a>End Date Account
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtEndDate" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <ajaxToolkit:CalendarExtender ID="datedop" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy" />
                                                                                </div>
                                                                            </div>--%>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Phone Number</strong><br />
                                                                                        Enter Phone Number.</span></a>Phone Number <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtPhone" MaxLength="15" TextMode="Phone" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtPhone" runat="server" ID="FilteredTextBoxExtender3" ValidChars="+" FilterType="Numbers,Custom" />
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator14" CssClass="alert  validation" runat="server" ValidationGroup="Submit" InitialValue="" ControlToValidate="txtPhone" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Phone Number required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Type</strong>
                                                                                        <br />
                                                                                        Select School Type.</span></a>School Type<span class="color-red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="drpSchoolType" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator13" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpSchoolType" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>School Type Required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Account Manager</strong>
                                                                                        <br />
                                                                                        Select School Account Manager.</span></a>School Account Manager<span class="color-red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="drpSchoolManagerType" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator11" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpSchoolManagerType" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>School Account Manager Required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:WizardStep>
                                                    <asp:WizardStep ID="WizardStep2" runat="server" Title="Section 2: School Addresses ">
                                                        <div class="panel panel-default">

                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    <div class="float-e-margins">
                                                                        <div class="form-horizontal formelement">
                                                                            <br />
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Address Line 1</strong><br />
                                                                                        Enter School Address Line 1.</span></a>School Address Line 1 <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtAddress1" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator10" CssClass="alert  validation" runat="server" ValidationGroup="Submit" SetFocusOnError="true" ControlToValidate="txtAddress1" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Address Line 1 required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Address Line 2</strong><br />
                                                                                        Enter School Address Line 2.</span></a>School Address Line 2 <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtAddress2" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator12" CssClass="alert  validation" runat="server" ValidationGroup="Submit" SetFocusOnError="true" ControlToValidate="txtAddress2" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Address Line 2 required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Town</strong><br />
                                                                                        Enter Town.</span></a>Town <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtTown" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator25" CssClass="alert  validation" runat="server" ValidationGroup="Submit" SetFocusOnError="true" ControlToValidate="txtTown" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Town required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <div class="float-e-margins">
                                                                        <div class="form-horizontal formelement">
                                                                            <br />
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Post Code</strong><br />
                                                                                        Enter Post Code.</span></a>Post Code <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtPostCode" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator15" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtPostCode" SetFocusOnError="true" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Post Code required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Country</strong>
                                                                                        <br />
                                                                                        Select Country.</span></a>Country<span class="color-red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="drpCountry" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator16" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpCountry" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Country Required</span></a>"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-md-7 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Billing Address Same as School Address</strong><br />
                                                                                        Select Billing Address Same as School Address.</span></a>Billing Address Same as School Address <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-md-5">
                                                                                    <asp:CheckBox ID="chkBilling" runat="server" OnCheckedChanged="chkBilling_CheckedChanged" AutoPostBack="true" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <div class="float-e-margins">
                                                                                <div class="form-horizontal formelement">
                                                                                    <br />
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-4 control-label">
                                                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Billing Address Line 1</strong><br />
                                                                                                Enter Billing Address Line 1.</span></a>Billing Address Line 1 <span class="color-red">*</span>
                                                                                        </label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:TextBox ID="txtBillingAddress1" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-4 control-label">
                                                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Billing Address Line 2</strong><br />
                                                                                                Enter Billing Address Line 2.</span></a>Billing Address Line 2 <span class="color-red">*</span>
                                                                                        </label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:TextBox ID="txtBillingAddress2" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-4 control-label">
                                                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Town</strong><br />
                                                                                                Enter Town.</span></a>Town <span class="color-red">*</span>
                                                                                        </label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:TextBox ID="txtBillingTown" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <div class="float-e-margins">
                                                                                <div class="form-horizontal formelement">
                                                                                    <br />
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-4 control-label">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Post Code</strong><br />
                                                                                                Enter Post Code.</span></a>Post Code <span class="color-red">*</span>
                                                                                        </label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:TextBox ID="txtBillingPostCode" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-4 control-label">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Country</strong>
                                                                                                <br />
                                                                                                See Country.</span></a>Country<span class="color-red">*</span></label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:DropDownList ID="drpBillingCountry" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </asp:WizardStep>
                                                    <%--<asp:WizardStep ID="WizardStep3" runat="server" Title="Section 3: Content ">
                                                        <div class="panel panel-default">

                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    <div class="float-e-margins">
                                                                        <div class="form-horizontal formelement">
                                                                            <br />
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Bulk Upload Classes</strong><br />
                                                                                        Select Bulk Upload Classes.</span></a>Bulk Upload Classes</label>
                                                                                <div class="col-sm-6">
                                                                                    <asp:FileUpload ID="FpUploadClasses" runat="server" CssClass="form-control" Width="100%" Height="15%" />
                                                                                </div>
                                                                                <div class="col-sm-2">
                                                                                    <a href="css/bulk.xlsx" style="color: brown;" class="" download="">Download template</a>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Bulk Upload Teachers</strong><br />
                                                                                        Select Bulk Upload Teachers.</span></a>Bulk Upload Teachers</label>
                                                                                <div class="col-sm-6">
                                                                                    <asp:FileUpload ID="FpUploadTeachers" runat="server" CssClass="form-control" Width="100%" Height="15%" />
                                                                                </div>
                                                                                <div class="col-sm-2">
                                                                                    <a href="css/bulk.xlsx" style="color: brown;" class="" download="">Download template</a>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Bulk Upload Students</strong><br />
                                                                                        Select Bulk Upload Students.</span></a>Bulk Upload Students</label>
                                                                                <div class="col-sm-6">
                                                                                    <asp:FileUpload ID="FpUploadStudents" runat="server" CssClass="form-control" Width="100%" Height="15%" />
                                                                                </div>
                                                                                <div class="col-sm-2">
                                                                                    <a href="css/bulk.xlsx" style="color: brown;" class="" download="">Download template</a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:WizardStep>--%>
                                                    <asp:WizardStep ID="WizardStep3" runat="server" Title="Section 3: Activation">
                                                        <div class="panel panel-default">

                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    <div class="float-e-margins">
                                                                        <div class="form-horizontal formelement">
                                                                            <br />
                                                                            <div class="form-group">
                                                                                <label class="col-md-10 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Customer Bank Account Setup in Payment Systems ?</strong><br />
                                                                                        Select Customer Bank Account Setup in Payment Systems ?</span></a>Customer Bank Account Setup in Payment Systems ?<span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-md-2">
                                                                                    <asp:CheckBox ID="chkPayment_Systems" runat="server" CssClass="i-checks" />
                                                                                    <%--<asp:CustomValidator runat="server" ID="CheckBoxRequired" EnableClientScript="true" OnServerValidate="CheckBoxRequired_ServerValidate" ClientValidationFunction="CheckBoxRequired_ClientValidate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Please Select this Box If you want to Proceed</span></a>"></asp:CustomValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-md-10 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Customer Agreement Signed ?</strong><br />
                                                                                        Select Customer Agreement Signed ?</span></a>Customer Agreement Signed ?<span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-md-2">
                                                                                    <asp:CheckBox ID="ChkCustSigned" runat="server" CssClass="i-checks" />
                                                                                    <%--<asp:CustomValidator runat="server" ID="ChkRequired" EnableClientScript="true" OnServerValidate="ChkRequired_ServerValidate" ClientValidationFunction="ChkRequired_ClientValidate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Please Select this Box If you want to Proceed</span></a>"></asp:CustomValidator>--%>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group" runat="server" id="divAttend">
                                                                                <label class="col-md-10 control-label" id="lblAttendance">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Does School want to Activate Attendance Module ?</strong><br />
                                                                                        Select Does School want to Activate Attendance Module ?</span></a>Does School want to Activate Attendance Module ? 
                                                                                </label>
                                                                                <div class="col-md-2">
                                                                                    <asp:CheckBox ID="chkAttendanceModule" runat="server" CssClass="i-checks" AutoPostBack="true" OnCheckedChanged="chkAttendanceModule_CheckedChanged" onclick="myFunction()" />

                                                                                    <%--<asp:CustomValidator runat="server" ID="CustomValidator4" EnableClientScript="true" OnServerValidate="CustomValidator1_ServerValidate" ClientValidationFunction="CustomValidator1_ClientValidate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Attendance module you can not changed it.</span></a>"></asp:CustomValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-md-10 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Does School want Parent Notifications After Confirm Attendance ?</strong><br />
                                                                                        Select Does School want Parent Notifications After Confirm Attendance ?</span></a>Does School want Parent Notifications After Confirm Attendance ? <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-md-2">
                                                                                    <asp:CheckBox ID="ChkConfirmAttendance" runat="server" CssClass="i-checks" />
                                                                                    <%--<asp:CustomValidator runat="server" ID="CustomChkConfirmAttendance" EnableClientScript="true" OnServerValidate="CustomChkConfirmAttendance_ServerValidate" ClientValidationFunction="CustomChkConfirmAttendance_ClientValidate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Please Select this Box If you want to Proceed</span></a>"></asp:CustomValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-md-10 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Does School want Parent Notifications After Confirm PickUp ?</strong><br />
                                                                                        Select Does School want Parent Notifications After Confirm PickUp ?</span></a>Does School want Parent Notifications After Confirm PickUp ? <span class="color-red">*</span>
                                                                                </label>
                                                                                <div class="col-md-2">
                                                                                    <asp:CheckBox ID="chkConfirmPickUp" runat="server" CssClass="i-checks" />
                                                                                    <%--<asp:CustomValidator runat="server" ID="CustomChkConfirmPickUp" EnableClientScript="true" OnServerValidate="CustomChkConfirmPickUp_ServerValidate" ClientValidationFunction="CustomChkConfirmPickUp_ClientValidate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Please Select this Box If you want to Proceed</span></a>"></asp:CustomValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <div class="float-e-margins">
                                                                        <div class="form-horizontal formelement">
                                                                            <br />
                                                                            <div class="form-group">
                                                                                <label class="col-sm-5 control-label width-300">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Initial Training Agreed</strong><br />
                                                                                        Select Initial Training Agreed.</span></a>Initial Training Agreed</label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:RadioButtonList ID="rblist" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="1">&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                                                                        <asp:ListItem Value="0" Selected="True">&nbsp;No</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-5 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Initial Training Date</strong><br />
                                                                                        Enter Initial Training Date.</span></a>Initial Training Date
                                                                                </label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="txtIntTrainingDate" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIntTrainingDate" Format="dd/MM/yyyy" />
                                                                                    <asp:CustomValidator runat="server" ID="valDateRange" ValidationGroup="Submit" ControlToValidate="txtIntTrainingDate" CssClass="alert  validation"
                                                                                        ClientValidationFunction="ValidateDate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Initial Training Date cannot be a date in the past</span></a>" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-5 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Activation Date</strong><br />
                                                                                        Enter Activation Date.</span></a>Activation Date
                                                                                </label>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="txtActivateDate" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtActivateDate" Format="dd/MM/yyyy" />
                                                                                    <asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="Submit" ControlToValidate="txtActivateDate" CssClass="alert  validation"
                                                                                        ClientValidationFunction="ValidateDate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Activation Date cannot be a date in the past</span></a>" />
                                                                                    <%--<asp:CompareValidator ID="CompareValidator3" CssClass="alert  validation" ValidationGroup="Submit" runat="server" ControlToValidate="txtActivateDate" ControlToCompare="txtIntTrainingDate"
                                                                                        Operator="LessThan" Type="Date" SetFocusOnError="true" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Initial Training Date CANNOT be Greater than Activation Date</span></a>"></asp:CompareValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">
                                                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Active / InActive</strong><br />
                                                                                        Select Active / InActive</span></a>Active / InActive
                                                                                </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:CheckBox ID="chkActive" runat="server" CssClass="i-checks" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:WizardStep>
                                                </WizardSteps>
                                                <%--   <StartNextButtonStyle CssClass="btn btn-primary" />
                                                <StepNextButtonStyle CssClass="btn btn-primary" />
                                                <StepPreviousButtonStyle CssClass="btn btn-default" />
                                                <FinishCompleteButtonStyle CssClass="btn btn-primary" />
                                                <FinishPreviousButtonStyle CssClass="btn btn-default" />--%>
                                                <StepNavigationTemplate>
                                                    <div class="form-group" style="margin-top: 5px">
                                                        <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Previous" OnClick="btnReset_Click" />
                                                        <asp:Button ID="btnSave" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="Submit" OnClick="btnSave_Click" runat="server" Text="Save & Next" />&nbsp;&nbsp;
                                                    </div>
                                                </StepNavigationTemplate>
                                                <StartNavigationTemplate>
                                                    <div class="form-group" style="margin-top: 5px">
                                                        <asp:Button ID="btnSave" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="Submit" OnClick="btnSave_Click" runat="server" Text="Save & Next" />&nbsp;&nbsp;
                                                    </div>
                                                </StartNavigationTemplate>
                                                <FinishNavigationTemplate>
                                                    <div class="form-group" style="margin-top: 5px">
                                                        <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Previous" OnClick="btnReset_Click" />
                                                        <asp:Button ID="btnSave" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="Submit" OnClick="btnSave_Click" runat="server" Text="Save" />&nbsp;&nbsp;
                                                    </div>
                                                </FinishNavigationTemplate>

                                            </asp:Wizard>
                                        </div>
                                    </div>
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
    <script src="Scripts/plugins/iCheck/icheck.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
        });
    </script>
    <script src="Scripts/plugins/clockpicker/clockpicker.js"></script>
    <script type="text/javascript">


        function capFirst(string) {
            var sentence = string.toLowerCase().split(" ");
            for (var i = 0; i < sentence.length; i++) {
                sentence[i] = sentence[i][0].toUpperCase() + sentence[i].slice(1);
            }
            document.write(sentence.join(" "));
            return sentence;
        };

        function TitleCase(objField) 
        {
            var objValues = objField.value.split(" ");
            var outText = "";
            for (var i = 0; i < objValues.length; i++) {
                outText = outText + objValues[i].substr(0, 1).toUpperCase() + objValues[i].substr(1).toLowerCase() + ((i < objValues.length - 1) ? " " : "");
            }
            return outText;
        }     



        $(document).ready(function () {
            $('.clockpicker').clockpicker();
        });
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery(".AcceptedAgreement input:checkbox").is(':checked');
        }
        function ChkRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery(".AcceptedAgreement input:checkbox").is(':checked');
        }
        function CustomValidator1_ClientValidate(sender, e) {
            e.IsValid = jQuery(".AcceptedAgreement input:checkbox").is(':checked');
        }
        function CustomChkConfirmAttendance_ClientValidate(sender, e) {
            e.IsValid = jQuery(".AcceptedAgreement input:checkbox").is(':checked');
        }
        function CustomChkConfirmPickUp_ClientValidate(sender, e) {
            e.IsValid = jQuery(".AcceptedAgreement input:checkbox").is(':checked');
        }
        function myFunction() {
            debugger;
            // Get the checkbox
            var checkBox = document.getElementById("chkAttendanceModule");
            // Get the output text
            //var text = document.getElementById("text");

            // If the checkbox is checked, display the output text
            if (checkBox.checked == true) {
                alert("block");
            } else {
                alert("none");
            }
        }
        <%--function TextBoxDCountyClient(sender, args) {
            debugger;
            var AdminFnames = document.getElementById('<%=txtAdminFName.Text%>').value;
            var SupFnames = document.getElementById('<%=txtSupFName.Text%>').value;
            if (SupFnames == AdminFnames) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }--%>
        function ValidateDate(sender, args) {
            var dateString = document.getElementById(sender.controltovalidate).value;
            var regex = /(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;
            if (regex.test(dateString)) {
                var parts = dateString.split("/");
                var dt = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
                args.IsValid = (dt.getDate() == parts[0] && dt.getMonth() + 1 == parts[1] && dt.getFullYear() == parts[2]);
            } else {
                args.IsValid = false;
            }
        }
    </script>
</asp:Content>
