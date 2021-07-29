<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="SchoolBulkUpload.aspx.cs" Inherits="SchoolApp.Admin.SchoolBulkUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <link href="Content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css" rel="stylesheet">
    <link href="Content/css/plugins/iCheck/custom.css" rel="stylesheet">
    <style>
        .text-navy {
            color: #1EAEFC !important;
        }
    </style>
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
                                        <i class="fa fa-th-large"></i>&nbsp; <span>Bulk Upload</span>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body no-padding-bottom">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="float-e-margins">
                                        <div class="form-horizontal formelement">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Select School</strong>
                                                        <br />
                                                        Select School Name.</span></a>Select School<span class="color-red">*</span></label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="drpSchool" CssClass="form-control" Width="95%" runat="server" OnSelectedIndexChanged="drpSchool_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" CssClass="alert validation" runat="server" ValidationGroup="Submit" InitialValue="0" ControlToValidate="drpSchool" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>School Required</span></a>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="ibox float-e-margins" style="border: 2px solid #e7eaec !important;">
                                        <div class="ibox-title  back-change">
                                            <h5>Template</h5>
                                        </div>
                                        <div class="ibox-content no-padding-top">
                                            <table class="table table-stripped small m-t-md">
                                                <tbody>
                                                    <tr>
                                                        <td class="no-borders">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#filterPanel1">Student Template</a>
                                                                        <span class="pull-right panel-collapse-clickable" data-toggle="collapse" data-parent="#accordion" href="#filterPanel1">
                                                                            <i class="fa fa-chevron-down"></i>
                                                                        </span>
                                                                    </h4>
                                                                </div>
                                                                <div id="filterPanel1" class="panel-collapse collapse">
                                                                    <div class="panel-body">
                                                                        <div class="col-md-12">
                                                                            <a href="Template/Student.xlsx"><i class="pull-right fa fa-download"></i>Create Student Template</a>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <a href="Template/UpdateStudent.xlsx"><i class="pull-right fa fa-download"></i>Update Student Template</a>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <asp:LinkButton ID="lnkMasterClick" runat="server" ValidationGroup="Submit" OnClick="lnkMasterClick_Click"><i class="pull-right fa fa-download"></i>Master File Data</asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="Submit" OnClick="lnkStudentExportReports_Click"><i class="pull-right fa fa-download"></i>Student Reports</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--<a href="Template/Student.xlsx">Student Template<i class="pull-right fa fa-download"></i></a>--%>
                                                        </td>
                                                    </tr>
                                                    <asp:Panel ID="PanelClass" runat="server">
                                                        <tr>
                                                            <td>
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading">
                                                                        <h4 class="panel-title">
                                                                            <a data-toggle="collapse" data-parent="#accordion" href="#filterPanel2">Class Template</a>
                                                                            <span class="pull-right panel-collapse-clickable" data-toggle="collapse" data-parent="#accordion" href="#filterPanel2">
                                                                                <i class="fa fa-chevron-down"></i>
                                                                            </span>
                                                                        </h4>
                                                                    </div>
                                                                    <div id="filterPanel2" class="panel-collapse collapse">
                                                                        <div class="panel-body">
                                                                            <div class="col-md-12">
                                                                                <a href="Template/Class.xlsx"><i class="pull-right fa fa-download"></i>Create Class Template</a>
                                                                            </div>
                                                                            <div class="col-md-12">
                                                                                <a href="Template/UpdateClass.xlsx"><i class="pull-right fa fa-download"></i>Update Class Template</a>
                                                                            </div>
                                                                            <div class="col-md-12">
                                                                                <asp:LinkButton ID="lnkMasterClass" runat="server" ValidationGroup="Submit" OnClick="lnkMasterClick_Click"><i class="pull-right fa fa-download"></i>Master File Data</asp:LinkButton>
                                                                            </div>
                                                                            <div class="col-md-12">
                                                                                <asp:LinkButton ID="LinkButton3" runat="server" ValidationGroup="Submit" OnClick="lnkClassReportClick_Click"><i class="pull-right fa fa-download"></i>Class Report</asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%--<a href="Template/Class.xlsx">Class Template<i class="pull-right fa fa-download"></i></a>--%>
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <tr>
                                                        <td>
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#filterPanel3">Teacher Template</a>
                                                                        <span class="pull-right panel-collapse-clickable" data-toggle="collapse" data-parent="#accordion" href="#filterPanel3">
                                                                            <i class="fa fa-chevron-down"></i>
                                                                        </span>
                                                                    </h4>
                                                                </div>
                                                                <div id="filterPanel3" class="panel-collapse collapse">
                                                                    <div class="panel-body">
                                                                        <div class="col-md-12">
                                                                            <a href="Template/Teacher.xlsx"><i class="pull-right fa fa-download"></i>Create Teacher Template</a>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <a href="Template/UpdateTeacher.xlsx"><i class="pull-right fa fa-download"></i>Update Teacher Template</a>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <asp:LinkButton ID="lnkMasterTeacher" runat="server" ValidationGroup="Submit" OnClick="lnkMasterClick_Click"><i class="pull-right fa fa-download"></i>Master File Data</asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="Submit" OnClick="lnkTeacherReportClick_Click"><i class="pull-right fa fa-download"></i>Teacher Report</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--<a href="Template/Teacher.xlsx">Teacher Template<i class="pull-right fa fa-download"></i></a>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#filterPanel4">Non Teacher Template</a>
                                                                        <span class="pull-right panel-collapse-clickable" data-toggle="collapse" data-parent="#accordion" href="#filterPanel4">
                                                                            <i class="fa fa-chevron-down"></i>
                                                                        </span>
                                                                    </h4>
                                                                </div>
                                                                <div id="filterPanel4" class="panel-collapse collapse">
                                                                    <div class="panel-body">
                                                                        <div class="col-md-12">
                                                                            <a href="Template/NonTeacher.xlsx"><i class="pull-right fa fa-download"></i>Create Non Teacher Template</a>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <a href="Template/UpdateNonTeacher.xlsx"><i class="pull-right fa fa-download"></i>Update Non Teacher Template</a>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <asp:LinkButton ID="LinkButton4" runat="server" ValidationGroup="Submit" OnClick="lnkMasterClick_Click"><i class="pull-right fa fa-download"></i>Master File Data</asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <asp:LinkButton ID="LinkButton5" runat="server" ValidationGroup="Submit" OnClick="lnkNonTeacherReportClick_Click"><i class="pull-right fa fa-download"></i>Non Teacher Report</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--<a href="Template/Teacher.xlsx">Teacher Template<i class="pull-right fa fa-download"></i></a>--%>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>
                                    <%--<div class="float-e-margins">
                                        <div class="form-horizontal formelement">
                                            <div role="tabpanel">
                                                <div class="col-md-3">
                                                    <ul class="nav nav-pills brand-pills nav-stacked" role="tablist">
                                                        <li role="presentation" class="brand-nav active">
                                                            <asp:HyperLink ID="hlink1" runat="server" NavigateUrl="#tab1" aria-controls="tab1" role="tab" data-toggle="tab">Template Downloads <i class="pull-right fa fa-download"></i></asp:HyperLink></li>
                                                        <li role="presentation" class="brand-nav">
                                                            <asp:HyperLink ID="hlink2" runat="server" NavigateUrl="#tab2" aria-controls="tab2" role="tab" data-toggle="tab">Template Uploads <i class="pull-right fa fa-upload"></i></asp:HyperLink></li>
                                                    </ul>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="tab-content">
                                                        <div role="tabpanel" class="tab-pane active" id="tab1">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#filterPanel1">Student Template</a>
                                                                        <span class="pull-right panel-collapse-clickable" data-toggle="collapse" data-parent="#accordion" href="#filterPanel">
                                                                            <i class="glyphicon glyphicon-chevron-down"></i>
                                                                        </span>
                                                                    </h4>
                                                                </div>
                                                                <div id="filterPanel1" class="panel-collapse collapse">
                                                                    <div class="panel-body">
                                                                        <a href="../Template/Upload/Student.xlsx">Student Template<i class="pull-right fa fa-download"></i></a>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#filterPanel2">Class Template</a>
                                                                        <span class="pull-right panel-collapse-clickable" data-toggle="collapse" data-parent="#accordion" href="#filterPanel">
                                                                            <i class="glyphicon glyphicon-chevron-down"></i>
                                                                        </span>
                                                                    </h4>
                                                                </div>
                                                                <div id="filterPanel2" class="panel-collapse collapse">
                                                                    <div class="panel-body">
                                                                        <a href="../Template/Upload/Class.xlsx">Class Template<i class="pull-right fa fa-download"></i></a>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#filterPanel3">Teacher Template</a>
                                                                        <span class="pull-right panel-collapse-clickable" data-toggle="collapse" data-parent="#accordion" href="#filterPanel">
                                                                            <i class="glyphicon glyphicon-chevron-down"></i>
                                                                        </span>
                                                                    </h4>
                                                                </div>
                                                                <div id="filterPanel3" class="panel-collapse  collapse">
                                                                    <div class="panel-body">
                                                                        <a href="../Template/Upload/Teacher.xlsx">Teacher Template<i class="pull-right fa fa-download"></i></a>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div role="tabpanel" class="tab-pane template_upload" id="tab2">
                                                            <h2>Select Category</h2>

                                                            <asp:FileUpload ID="FUTemplate" class="btn btn-primary" runat="server" />
                                                            <ul>
                                                                <li>
                                                                    <asp:RadioButton ID="rbtnStudent" runat="server" GroupName="Selector" Checked="true" Text="Student" />
                                                                    <div class="check"></div>
                                                                </li>

                                                                <li>
                                                                    <asp:RadioButton ID="rbtnClass" runat="server" GroupName="Selector" Text="Class" />
                                                                    <div class="check">
                                                                        <div class="inside"></div>
                                                                    </div>
                                                                </li>

                                                                <li>
                                                                    <asp:RadioButton ID="rbtnTeacher" runat="server" GroupName="Selector" Text="Teacher" />
                                                                    <div class="check">
                                                                        <div class="inside"></div>
                                                                    </div>
                                                                </li>
                                                            </ul>
                                                            <div class="">
                                                                <asp:Button ID="btnUpload" runat="server" class="btn btn-primary" Style="margin-left: 20px; padding: 10px 10px; margin-top: 20px !important;" Text="Upload" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>--%>
                                </div>
                                <div class="col-md-8">
                                    <div class="float-e-margins">
                                        <div class="form-horizontal formelement">

                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Upload File</strong>
                                                        <br />
                                                        Upload File.</span></a>Upload File<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:FileUpload ID="FUTemplate" Width="80%" runat="server" CssClass="form-control" Height="15%" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Select Category</strong>
                                                        <br />
                                                        Select Category.</span></a>Select Category<span class="color-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <div class="i-checks">
                                                        <label>
                                                            <asp:RadioButton ID="rbtnStudent" Checked="true" GroupName="rblist" runat="server" />
                                                            <i></i>Student
                                                        </label>
                                                    </div>
                                                    <asp:Panel ID="PanelClass1" runat="server">
                                                        <div class="i-checks">
                                                            <label>
                                                                <asp:RadioButton ID="rbtnClass" GroupName="rblist" runat="server" />
                                                                <i></i>Class
                                                            </label>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="i-checks">
                                                        <label>
                                                            <asp:RadioButton ID="rbtnTeacher" GroupName="rblist" runat="server" />
                                                            <i></i>Teacher
                                                        </label>
                                                    </div>
                                                    <div class="i-checks">
                                                        <label>
                                                            <asp:RadioButton ID="rbtnNonTeacher" GroupName="rblist" runat="server" />
                                                            <i></i>Non Teacher
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body no-padding-top">
                                        <div class="hr-line-dashed"></div>
                                        <div class="form-group no-bottom-margin">
                                            <label class="col-sm-4 control-label width-160"></label>
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
                </div>
            </div>
        </div>
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
</asp:Content>
