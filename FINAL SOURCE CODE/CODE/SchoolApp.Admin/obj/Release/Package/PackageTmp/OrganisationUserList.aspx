<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="OrganisationUserList.aspx.cs" Inherits="SchoolApp.Admin.OrganisationUserList" %>

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
                                            <i class="fa fa-th-large"></i>&nbsp; <span>Organisation List</span>
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
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Organisation Code</strong><br />
                                                                Enter Organisation Code.</span></a>Organisation Code
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtOrgCode" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>First Name</strong><br />
                                                                Enter First Name.</span></a>First Name
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtFirstName" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Last Name</strong><br />
                                                                Enter Last Name.</span></a>Last Name
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtLastName" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Email</strong><br />
                                                                Enter Email.</span></a>Email
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtEmail" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Town</strong><br />
                                                                Enter Town.</span></a>Town
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtTown" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label width-400">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Organisation User Role</strong><br />
                                                                Select Organisation User Role.</span></a>Organisation User Role

                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="drpRole" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>                                                            
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label width-400">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Status</strong><br />
                                                                Select Status.</span></a>Status

                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="drpStatus" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>                                                            
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
                                        <asp:GridView ID="GvUserList" Width="100%" AllowSorting="true" class="table table-striped table-bordered table-hover" ShowHeaderWhenEmpty="true" EmptyDataText="No Rows Found" AutoGenerateColumns="false" runat="server" CssClass="customgrid" AllowPaging="true" PageSize="10" DataKeyNames="ID" OnRowCommand="GvUserList_RowCommand" OnPageIndexChanging="GvUserList_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-CssClass="command-field-ID" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Image" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Bind("Photo") %>' Height="50px" Width="50px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="First Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLName" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("OrgCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Town">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArea" runat="server" Text='<%#Eval("Town") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Organisation UserRole">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRole" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="command-field-ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit Organisation" CommandName="btnEdit" CommandArgument='<%# Eval("ID" )%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <label onclick="getOrgData('<%#Eval("ID") %>')" data-target="#myModal" data-toggle="modal" title="Organisation Users Information" style="cursor: pointer;"><i class="fa fa-eye" style="color: #428bca;"></i></label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" ToolTip='<%# Eval("ID")%>' OnCheckedChanged="chk_CheckedChanged" Checked='<%# Eval("Active" )%>' />
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
                                        <h4 class="modal-title">Organisation User Details</h4>
                                    </div>
                                    <div class="modal-body" style="padding: 0 0 0 0 !important;">
                                        <div class="panel-body no-padding-bottom">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="float-e-margins">
                                                        <div class="form-horizontal formelement">

                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    First Name :</label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblFName" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Last Name :</label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblLName" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Email :
                                                                </label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Organization User Number :
                                                                </label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblOrgNumber" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Address Line 1 :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblAddress1" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Address Line 2 :</label>
                                                                <div class="col-sm-7" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis; padding-top: 7px !important;">
                                                                    <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Town :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblTown" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Country :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblCountry" runat="server"></asp:Label>
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
                                                                    Organisation User Role :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblRoles" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Status :</label>
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
                                                                    Last Updated Date:</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblUpdateDate" runat="server"></asp:Label>
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
                <asp:PostBackTrigger ControlID="GvUserList" />
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
        function getOrgData(ID) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/APIDetails.aspx?OP=GetOrgData&ID=" + ID,
                data: "",
                dataType: "json",
                success: function (Result) {
                    if (Result != null) {
                        $("#MainContent_lblFName").text(Result.FirstName);
                        $("#MainContent_lblLName").text(Result.LastName);
                        $("#MainContent_lblEmail").text(Result.Email);
                        $("#MainContent_lblOrgNumber").text(Result.OrgCode);
                        $("#MainContent_lblAddress1").text(Result.Address1);
                        $("#MainContent_lblAddress2").text(Result.Address2);
                        $("#MainContent_lblTown").text(Result.Town);
                        $("#MainContent_lblCountry").text(Result.CountryName);
                        $("#MainContent_lblStatus").text(Result.Status);
                        $("#MainContent_lblRoles").text(Result.RoleName);
                        $("#MainContent_lblCreatedBy").text(Result.CreatedByName);
                        $("#MainContent_lblUpdatedBy").text(Result.LastUpdatedBy);
                        $("#MainContent_lblUpdateDate").text(Result.UpdatedDate);
                        $("#MainContent_lblActivatedBy").text(Result.ActivationBy);
                        $("#MainContent_lblActivatedDate").text(Result.StrActivationDate);
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
