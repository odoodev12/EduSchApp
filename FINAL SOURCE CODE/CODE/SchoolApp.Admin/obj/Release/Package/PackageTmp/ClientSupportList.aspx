<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ClientSupportList.aspx.cs" Inherits="SchoolApp.Admin.ClientSupport" %>

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
                                            <i class="fa fa-th-large"></i>&nbsp; <span>Client Support</span>
                                        </h4>
                                    </div>
                                    <div class="col-lg-6 pull-right">
                                        <h5 class="panel-title">

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
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Ticket No.</strong><br />
                                                                Enter Ticket No.</span></a>Ticket No
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtTicketNo" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                <label class="col-sm-4 control-label">
                                                    <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Priority</strong>
                                                        <br />
                                                        Select Priority.</span></a>Priority</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpPriority" CssClass="form-control" Width="80%" runat="server"></asp:DropDownList>
                                                    
                                                </div>
                                            </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Created By.</strong><br />
                                                                Enter Created By.</span></a>Created By
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCreatedBy" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
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
                                        <asp:GridView ID="GvClientSupportList" Width="100%" AllowSorting="true" class="table table-striped table-bordered table-hover" ShowHeaderWhenEmpty="true" EmptyDataText="No Rows Found" AutoGenerateColumns="false" runat="server" CssClass="customgrid" AllowPaging="true" PageSize="10" DataKeyNames="ID" OnRowCommand="GvClientSupportList_RowCommand" OnPageIndexChanging="GvClientSupportList_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-CssClass="command-field-ID" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ticket No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTicketNo" runat="server" Text='<%#Eval("TicketNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequest" runat="server" Text='<%#Eval("Subject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Priority">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPriority" runat="server" Text='<%#Eval("Priorities") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Support Officer">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupportOfficer" runat="server" Text='<%#Eval("SupportOfficer") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedByName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Created Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedAt" runat="server" Text='<%#Eval("SDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-view" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server" ToolTip="Edit Client Support" CommandName="btnView" CommandArgument='<%# Eval("ID" )%>'><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-view" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <label onclick="getSupportData('<%#Eval("ID") %>')" data-target="#myModal" data-toggle="modal" title="View Support Details" style="cursor: pointer;"><i class="fa fa-eye" style="color: #428bca;"></i></label>
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
                            <div class="modal-dialog">
                                <div class="modal-content animated flipInY">
                                    <div class="modal-header">
                                        <button type="button" id="btnclose1" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                        <h4 class="modal-title">Support Details</h4>
                                    </div>
                                    <div class="modal-body" style="padding: 0 0 0 0 !important;">
                                        <div class="panel-body no-padding-bottom">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="float-e-margins">
                                                        <div class="form-horizontal formelement">

                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Created By :</label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">
                                                                    Assign By :</label>
                                                                <label class="col-sm-7" style="padding-top: 7px !important; font-weight: normal !important;">
                                                                    <asp:Label ID="lblAssignBy" runat="server"></asp:Label>
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="float-e-margins">
                                                        <div class="form-horizontal rightform formelement">
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label" style="padding-left: 23px !important;">
                                                                    Create Date :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label" style="padding-left: 23px !important;">
                                                                    Assign Date :</label>
                                                                <div class="col-sm-7" style="padding-top: 7px !important;">
                                                                    <asp:Label ID="lblAssignDate" runat="server"></asp:Label>
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
            <%-- <Triggers>
                <asp:PostBackTrigger ControlID="btnNew" />
            </Triggers>--%>
            <Triggers>
                <asp:PostBackTrigger ControlID="GvClientSupportList" />
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
        function getSupportData(ID) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/APIDetails.aspx?OP=GetSupportData&ID=" + ID,
                data: "",
                dataType: "json",
                success: function (Result) {
                    debugger;
                    if (Result != null) {
                        $("#MainContent_lblCreatedBy").text(Result.CreatedByName);
                        $("#MainContent_lblAssignBy").text(Result.AssignBy);
                        $("#MainContent_lblCreatedDate").text(Result.SDate);
                        $("#MainContent_lblAssignDate").text(Result.StrAssignDate);
                    }
                    $("#myModal").show();
                },
                error: function (Result) {
                    debugger;
                    alert("Error");
                }
            });
        }
    </script>
</asp:Content>
