<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnPickStudentReport.aspx.cs" Inherits="SchoolApp.Web.Reports.UnPickStudentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading">
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/School/Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> /
                            <asp:HyperLink ID="hlinkPickupReport" runat="server" NavigateUrl="~/School/Report.aspx" Text="Report"></asp:HyperLink>
                            / <span>UnPick Student Report</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features">
            <div class="sidebar-section">
                <div class="container">
                      <div class="row" style="text-align: center; margin-bottom: 15px;">
                    <asp:Button runat="server" Text="Export to Excel" class="btn btn-primary" OnClick="btnExport_Click"></asp:Button>
                          </div>
                    <div class="row">
                       <%-- <div class="col-md-3">
                            <asp:Button runat="server" Text="Export to Excel" class="btn btn-primary btn-block" OnClick="btnExport_Click"></asp:Button>
                        </div>--%>
                        <div class="col-md-12 col-xs-12 table-responsive" style="padding: 0px;">
                            <table class="table">
                                <thead style="background: #232323; color: white;">
                                    <tr>
                                        <th>Student Name</th>
                                        <th>Pickup Status</th>
                                        <th>Primary Parent Email</th>
                                        <th>Secondary Parent Email</th>
                                        <th>Primary Parent Number</th>
                                        <th>Secondary Parent Number</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lstTable" runat="server" OnPagePropertiesChanging="lstTable_PagePropertiesChanging">
                                        <GroupTemplate>
                                            <td runat="server" id="itemPlaceholder"></td>
                                        </GroupTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("StudentName") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("PickStatus") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("ParantEmail1") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("ParantPhone1") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("ParantEmail2") %></td>
                                                <td style="border-bottom: 1px solid #ddd !important;"><%# Eval("ParantPhone2") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <tr runat="server">
                                                <td runat="server" colspan="4">
                                                    <tr runat="server" id="groupPlaceholder"></tr>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td runat="server" colspan="4">
                                                    <asp:DataPager runat="server" PageSize="10" ID="DataPager3">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                            <asp:NumericPagerField></asp:NumericPagerField>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </LayoutTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
</asp:Content>
