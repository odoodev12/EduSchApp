<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="HolidayList.aspx.cs" Inherits="SchoolApp.Admin.HolidayList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                                            <i class="fa fa-th-large"></i>&nbsp; <span>Holiday List</span>
                                        </h4>
                                    </div>
                                    <div class="col-lg-6 pull-right">
                                        <h5 class="panel-title">
                                            <i class="fa fa-plus">&nbsp;</i><asp:LinkButton ID="btnNew" runat="server" OnClick="btnNew_Click">New</asp:LinkButton>&nbsp;&nbsp;
                                            <i class="fa fa-search">&nbsp;</i><a data-toggle="collapse" data-parent="#accordion" href="#MainContent_collapseOne1">Filter</a>&nbsp;&nbsp;
                                            <%--<i class="fa fa-file-excel-o">&nbsp;</i><asp:LinkButton ID="btnExport"  runat="server">Export</asp:LinkButton>&nbsp;&nbsp;--%>
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
                                                            <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Holiday Name</strong><br />
                                                                Enter Holiday Name.</span></a>Holiday Name
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtHolidayName" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                <asp:Button ID="btnFilter" class="btn btn-primary " runat="server" Text="Apply Filter" ValidationGroup="Submit" OnClick="btnFilter_Click"  />
                                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="Reset" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <div style="overflow: auto; width: 100%; height: 100%;">
                                        <asp:GridView ID="GvHolidayList" Width="100%" AllowSorting="true" class="table table-striped table-bordered table-hover" ShowHeaderWhenEmpty="true" EmptyDataText="No Rows Found" AutoGenerateColumns="false" runat="server" CssClass="customgrid" AllowPaging="true" PageSize="10" DataKeyNames="ID" OnRowCommand="GvHolidayList_RowCommand" OnPageIndexChanging="GvHolidayList_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-CssClass="command-field-ID" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                 
                                                <asp:TemplateField HeaderText="Holiday Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date" ItemStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLName" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit Organisation" CommandName="btnEdit" CommandArgument='<%# Eval("ID" )%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="command-field-delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete Organisation" OnClientClick="return ConfirmMsg();" CommandName="btnDelete" CommandArgument='<%# Eval("ID" )%>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnNew" />
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="GvHolidayList" />
            </Triggers>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnFilter" />
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
