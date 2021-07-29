<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="SchoolList.aspx.cs" Inherits="SchoolApp.Admin.SchoolList" %>

<%@ Register Src="~/UserControl/Loader.ascx" TagPrefix="uc1" TagName="Loader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">

    <style>
        .form-control, .single-line {
            border: 1px solid #e5e6e7 !important;
        }

        .modal-dialog i {
            font-size: 45px;
        }

        .modal-dialog p {
            color: #ed5565;
        }
    </style>
    <script type="text/javascript">
        function showModal() {
            $("#myModal").modal('show');
        }
        function hideModal() {
            $("#myModal").modal('hide');
        }
        $(function () {
            $("#btnclose").click(function () {
                //hideModal();
                $("#myModal").hide();
            });
        });
        $(function () {
            $("#btnclose1").click(function () {
                //hideModal();
                $("#myModal").hide();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="HID" runat="server" />
    <div class="row  animated bounceInRight">
        <asp:UpdatePanel ID="MainUpdatePanel" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading additional-padding">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h4 class="panel-title">
                                            <i class="fa fa-th-large"></i>&nbsp; <span>School List</span>
                                        </h4>
                                    </div>
                                    <div class="col-lg-6 pull-right">
                                        <h5 class="panel-title">
                                            <% if (GetUserType() == "2" || GetUserType() == "4" || GetAdmin() == true)
                                                {%>
                                            <i class="fa fa-plus">&nbsp;</i><asp:LinkButton ID="btnNew" runat="server" OnClick="btnNew_Click">New</asp:LinkButton>&nbsp;&nbsp;
                                            <%} %>
                                            <i class="fa fa-search">&nbsp;</i><a data-toggle="collapse" data-parent="#accordion" href="#MainContent_collapseOne1">Filter</a>&nbsp;&nbsp;
                                            <i class="fa fa-file-excel-o">&nbsp;</i><asp:LinkButton ID="btnExport" OnClick="btnExport_Click" runat="server">Export</asp:LinkButton>&nbsp;&nbsp;
                                        </h5>
                                    </div>
                                </div>
                            </div>
                            <div id="collapseOne1" class="panel-collapse collapse" runat="server">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="float-e-margins">
                                                <div class="form-horizontal formelement">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Customer No</strong><br />
                                                                Enter Customer No.</span></a>Customer No
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCustNo" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Name</strong><br />
                                                                Enter School Name.</span></a>School Name
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtSchoolName" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Type</strong>                                                            
                                                                Select School Type.</span></a>School Type
                                                            </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="drpSchoolType" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Account Status</strong>                                                            
                                                                Select Account Status.</span></a>Account Status
                                                            </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="drpAccountStatus" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="hr-line-dashed"></div>
                                        <div class="form-group no-bottom-margin pull-right">
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnFilter" class="btn btn-primary " runat="server" Text="Apply Filter" ValidationGroup="Submit" OnClick="btnFilter_Click" />
                                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="Reset" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <div style="overflow: auto; width: 100%; height: 100%;">
                                        <asp:GridView ID="GvSchoolList" Width="100%" AllowSorting="true" class="table table-striped table-bordered table-hover" ShowHeaderWhenEmpty="true" EmptyDataText="No Rows Found" AutoGenerateColumns="false" runat="server" CssClass="customgrid" AllowPaging="true" PageSize="10" DataKeyNames="ID" OnRowCommand="GvSchoolList_RowCommand" OnPageIndexChanging="GvSchoolList_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-CssClass="command-field-ID" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <center><%#Container.DataItemIndex+1 %></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="School Logo" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Bind("Logo") %>' Height="50px" Width="50px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("CustomerNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="School Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="School Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("SchoolType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Account Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAc" runat="server" Text='<%#Eval("SchoolStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit School" CommandName="btnEdit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <label onclick="getSchoolData('<%#Eval("ID") %>')" data-target="#myModal" data-toggle="modal" title="School Information" style="cursor: pointer;"><i class="fa fa-eye" style="color: #428bca;"></i></label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" ToolTip='<%# Eval("ID")%>' OnCheckedChanged="chk_CheckedChanged" Checked='<%# Eval("ActiveStatus" )%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                            <PagerStyle Font-Bold="true" />
                                            <AlternatingRowStyle CssClass="e-alt_row" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
                            <div class="modal-dialog" style="width: 800px !important;">
                                <div class="modal-content animated flipInY">
                                    <div class="modal-header">
                                        <button type="button" id="btnclose1" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                        <h4 class="modal-title">School Details</h4>
                                    </div>
                                    <div class="modal-body" style="padding: 0 0 0 0 !important;">
                                        <div class="panel-body no-padding-bottom">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="float-e-margins">
                                                        <div class="form-horizontal formelement">

                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    School Number :</label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblSchoolNumber" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    School Name :</label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblSchoolName" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    School Type :
                                                                </label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblType" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Address :
                                                                </label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Website :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblWebSite" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Admin Name :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblAdminName" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Admin E-Mail :</label>
                                                                <div class="col-sm-7" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis; padding-top: 7px !important;">
                                                                    <asp:Label ID="lblAdminEmail" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Supervisor Name :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblSName" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="float-e-margins">
                                                        <div class="form-horizontal rightform formelement">

                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Supervisor E-Mail :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblSEmail" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Account Status :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Created By :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Created Date :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Updated By :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblUpdatedBy" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Updated Date :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblUpdatedDate" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Activated By :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblActivatedBy" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Activated Date :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblActivatedDate" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnclose" class="btn btn-white" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnNew" />
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="GvSchoolList" />
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnFilter" />
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        function getSchoolData(ID) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/APIDetails.aspx?OP=GetSchoolData&ID=" + ID,
                data: "",
                dataType: "json",
                success: function (Result) {
                    if (Result != null) {
                        $("#MainContent_lblSchoolNumber").text(Result.Number);
                        $("#MainContent_lblSchoolName").text(Result.Name);
                        $("#MainContent_lblType").text(Result.SchoolType);
                        $("#MainContent_lblAddress").text(Result.Address1);
                        $("#MainContent_lblWebSite").text(Result.Website);
                        $("#MainContent_lblAdminName").text(Result.AdminFirstName + ' ' + Result.AdminLastName);
                        $("#MainContent_lblAdminEmail").text(Result.AdminEmail);
                        $("#MainContent_lblSName").text(Result.SupervisorFirstname + ' ' + Result.SupervisorLastname);
                        $("#MainContent_lblSEmail").text(Result.SupervisorEmail);
                        $("#MainContent_lblStatus").text(Result.StrAccountStatus);
                        $("#MainContent_lblCreatedBy").text(Result.CreatedByName);
                        $("#MainContent_lblUpdatedBy").text(Result.LastUpdatedBy);
                        $("#MainContent_lblActivatedBy").text(Result.ActivatedBy);
                        $("#MainContent_lblActivatedDate").text(Result.StrActivationDate);
                        $("#MainContent_lblUpdatedDate").text(Result.StrModifyDate);
                        $("#MainContent_lblCreatedDate").text(Result.StrCreatedDate);
                    }
                    $("#myModal").show();
                },
                error: function (Result) {
                    alert("Error");
                }
            });
        }
    </script>
</asp:Content>
