<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="OrganisationUserAdd.aspx.cs" Inherits="SchoolApp.Admin.OrganisationUserAdd" %>

<%@ Register Src="~/UserControl/Loader.ascx" TagPrefix="uc1" TagName="Loader" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <link href="Content/css/plugins/iCheck/custom.css" rel="stylesheet">
    <link href="Content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css" rel="stylesheet">
    <link href="Content/css/plugins/steps/jquery.steps.css" rel="stylesheet">
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
                                        <i class="fa fa-th-large"></i>&nbsp; <span>Add Organisation User</span>
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
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>FirstName</strong>
                                                        <br />
                                                        Enter FirstName.</span></a>FirstName<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtFirstName" onkeyup="this.value=TitleCase(this);" autocomplete="off" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtFirstName" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>FirstName Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>LastName</strong>
                                                        <br />
                                                        Enter LastName.</span></a>LastName<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtLastName" onkeyup="this.value=TitleCase(this);" autocomplete="off" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtLastName" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>LastName Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Email</strong>
                                                        <br />
                                                        Enter Email.</span></a>Email<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtEmail" autocomplete="off" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator10" CssClass="alert  validation" runat="server" ValidationGroup="Submit" InitialValue="" ControlToValidate="txtEmail" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Email Address required</span></a>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Enter Valid Email</span></a>"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Password</strong>
                                                        <br />
                                                        Enter Password.</span></a>Password<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" MaxLength="15" TextMode="Password" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtPassword" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Password Required</span></a>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ControlToValidate="txtPassword" ID="RegularExpressionValidator2" runat="server" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Password must be 8-10 characters long with at least one numeric,</br> one alphabet and one special character.</span></a>" ValidationExpression="(?=^.{8,10}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+}{:;'?/>.<,])(?!.*\s).*$"></asp:RegularExpressionValidator>
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
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Organization User Number</strong>
                                                        <br />
                                                        Enter Organization User Number.</span></a>Organization User Number<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtCode" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Code Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label width-300">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Upload Photo</strong><br />
                                                        Photo to Upload 1506X554 size preferred.</span></a>Photo<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:FileUpload ID="UploadImage" runat="server" CssClass="form-control" Width="80%" Height="15%" accept=".png,.jpg,.jpeg,.gif" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label width-400">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Address Line 1</strong><br />
                                                        Enter Address Line 1.</span></a>Address Line 1<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPostalAddress1" Width="80%" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtPostalAddress1" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Address Line 1 Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="float-e-margins">
                                        <div class="form-horizontal formelement">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label width-400">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Address Line 2</strong><br />
                                                        Enter Address Line 2.</span></a>Address Line 2<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPostalAddress2" Width="80%" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtPostalAddress2" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Address Line 2 Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label width-400">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Town</strong><br />
                                                        Enter Town.</span></a>Town<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtTown" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtTown" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Town Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label width-400">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Country</strong><br />
                                                        Select Country.</span></a>Country<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpCountry" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator7" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpCountry" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Country Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <%--<div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Start Date</strong><br />
                                                        Enter Start Date.</span></a>Start Date<span class="color-red">*</span>
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtStartDate" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="datedop" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy" />
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator11" CssClass="alert validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtStartDate" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Start Date Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>--%>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label width-400">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Organisation User Role</strong><br />
                                                        Select Organisation User Role.</span></a>Organisation User Role<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpRole" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator6" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpRole" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>Role Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Active / InActive</strong><br />
                                                        Select Active / InActive.</span></a>Active / InActive
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:CheckBox ID="chkActive" runat="server" CssClass="i-checks" />
                                                    <%--<asp:TextBox ID="txtEndDate" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy" />--%>
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
                        <%--<div class="panel-body">
                                <div class="table-responsive">
                                    <div style="overflow: auto;  width: 100%; height: 100%;">
                                        <asp:GridView ID="GvRoleList" Width="100%" AllowCustomPaging="true" AllowSorting="true" class="table table-striped table-bordered table-hover" ShowHeaderWhenEmpty="true" EmptyDataText="No Rows Found" AutoGenerateColumns="false" runat="server" CssClass="customgrid" AllowPaging="true" DataKeyNames="ID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Role">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Create School">
                                                    <ItemTemplate>
                                                        <center><asp:CheckBox runat="server" /></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Organisation User">
                                                    <ItemTemplate>
                                                        <center><asp:CheckBox runat="server" /></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice">
                                                    <ItemTemplate>
                                                        <center><asp:CheckBox runat="server" /></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Support">
                                                    <ItemTemplate>
                                                        <center><asp:CheckBox runat="server" /></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FAQ">
                                                    <ItemTemplate>
                                                        <center><asp:CheckBox runat="server" /></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Resource">
                                                    <ItemTemplate>
                                                        <center><asp:CheckBox runat="server" /></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Report">
                                                    <ItemTemplate>
                                                        <center><asp:CheckBox runat="server" /></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                            <PagerStyle Font-Bold="true" />
                                            <AlternatingRowStyle CssClass="e-alt_row" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>--%>
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
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
        });
    </script>
</asp:Content>
