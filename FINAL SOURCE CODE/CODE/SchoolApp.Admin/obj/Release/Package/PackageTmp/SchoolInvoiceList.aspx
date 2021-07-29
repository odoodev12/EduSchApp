<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="SchoolInvoiceList.aspx.cs" Inherits="SchoolApp.Admin.SchoolInvoiceList" %>

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

        .headerClass {
            width: 70% !important;
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
                                            <i class="fa fa-th-large"></i>&nbsp; <span>School Invoice List</span>
                                        </h4>
                                    </div>
                                    <div class="col-lg-6 pull-right">
                                        <h5 class="panel-title">
                                            <% if (GetUserType() == "2" ||GetUserType() == "1" || GetUserType() == "4" || GetAdmin() == true)
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
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>School Name</strong>
                                                        <br />
                                                        Enter School Name.</span></a>School Name</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpSchool" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Invoice No</strong><br />
                                                                Enter Invoice No.</span></a>Invoice No
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtSchoolName" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Type</strong>
                                                        <br />
                                                        Select Transaction Type.</span></a>Transaction Type</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpTransactionTypeID" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                    
                                                </div>
                                            </div>
                                                    <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Transaction Status</strong>
                                                        <br />
                                                        Select Transaction Status.</span></a>Transaction Status</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpTranStatus" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                    
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
                                        <asp:GridView ID="GvSchoolInvoiceList" Width="100%" AllowSorting="true" class="table table-striped table-bordered table-hover" ShowHeaderWhenEmpty="true" EmptyDataText="No Rows Found" AutoGenerateColumns="false" runat="server" CssClass="customgrid" AllowPaging="true" PageSize="10" DataKeyNames="ID" OnRowCommand="GvSchoolInvoiceList_RowCommand" OnPageIndexChanging="GvSchoolInvoiceList_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-CssClass="command-field-ID" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="School Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSchoolName" runat="server" Text='<%#Eval("SchoolName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("InvoiceNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transaction Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransactionTypeID" runat="server" Text='<%#Eval("strTransectionType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Transaction Desc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransactionDesc" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Trans. Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransactionAmount" runat="server" Text='<%#Eval("Transaction_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Tax Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxRate" runat="server" Text='<%#Eval("TaxRates") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Transaction Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("strStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Status" ItemStyle-CssClass="command-field-ID">
                                                    <ItemTemplate>
                                                        <center><asp:CheckBox runat="server" Checked='<%# Eval("Active") %>' Enabled="false" /></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit School Invoice" CommandName="btnEdit" CommandArgument='<%# Eval("ID" )%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <label onclick="getData('<%#Eval("ID") %>')" data-target="#myModal" data-toggle="modal" title="Invoice Information" style="cursor: pointer;"><i class="fa fa-eye" style="color: #428bca;"></i></label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete School Invoice" OnClientClick="return ConfirmMsg();" CommandName="btnDelete" CommandArgument='<%# Eval("ID" )%>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
                                        <h4 class="modal-title">School Invoice Details</h4>
                                    </div>
                                    <div class="modal-body" style="padding: 0 0 0 0 !important;">
                                        <div class="panel-body no-padding-bottom">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="float-e-margins">
                                                        <div class="form-horizontal formelement">

                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    School Name :</label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblSchoolName" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Invoice No :</label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblInvoiceNo" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Date From :
                                                                </label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblInvFromDate" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Date To :
                                                                </label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblInvToDate" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Transaction Type :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblTransactionType" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Description :</label>
                                                                <div class="col-sm-7" style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis; padding-top: 7px !important;">
                                                                    <asp:Label ID="lblTranDesc" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Amount :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
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
                                                                    Tax Rate :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblRate" runat="server"></asp:Label>
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
                                                                    <asp:Label ID="lblUpdateBy" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Updated Date :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblUpdatedDate" runat="server"></asp:Label>
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
                <asp:PostBackTrigger ControlID="GvSchoolInvoiceList" />
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
        function getData(ID) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/APIDetails.aspx?OP=GetDatas&ID=" + ID,
                data: "",
                dataType: "json",
                success: function (Result) {
                    if (Result != null) {
                        $("#MainContent_lblSchoolName").text(Result.SchoolName);
                        $("#MainContent_lblInvoiceNo").text(Result.InvoiceNumber);
                        $("#MainContent_lblInvFromDate").text(Result.FromDate);
                        $("#MainContent_lblInvToDate").text(Result.ToDate);
                        $("#MainContent_lblTransactionType").text(Result.strTransectionType);
                        $("#MainContent_lblTranDesc").text(Result.Description);
                        $("#MainContent_lblAmount").text(Result.Transaction_Amount);
                        $("#MainContent_lblRate").text(Result.TaxRates);
                        $("#MainContent_lblStatus").text(Result.strStatus);
                        $("#MainContent_lblCreatedBy").text(Result.CreatedByName);
                        $("#MainContent_lblCreatedDate").text(Result.strCreatedDate);

                        $("#MainContent_lblUpdateBy").text(Result.StatusUpdateBy);
                        $("#MainContent_lblUpdatedDate").text(Result.StrStatusUpdatedDate);
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
