<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Adminrole.aspx.cs" Inherits="SchoolApp.Web.School.Adminrole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .class-box {
            padding: 20px;
            background: #fff;
            margin-bottom: 20px;
        }

            .class-box p {
                margin-top: 0px;
                margin-bottom: 0px;
            }

            .class-box h3 {
                margin-bottom: 2px;
                margin-top: 0px;
            }



        .feature,
        .feature h3,
        .feature img,
        .feature .title_border {
            -webkit-transition: all .5s ease-in-out;
            -moz-transition: all .5s ease-in-out;
            -o-transition: all .5s ease-in-out;
            transition: all .5s ease-in-out;
        }

        .feature {
            border: solid 1px #cccccc;
        }

            .feature:hover {
                background: #F5F5F5;
                -webkit-transform: translate(0, .5em);
                -moz-transform: translate(0, .5em);
                -o-transform: translate(0, .5em);
                -ms-transform: translate(0, .5em);
                transform: translate(0, .5em);
            }

        .view_detail a {
            font-size: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading">
                            <asp:HyperLink ID="hlinkSchool" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> School</asp:HyperLink>
                            / <span>Custom Role</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <asp:UpdatePanel ID="Up1" runat="server">
            <ContentTemplate>
                <div class="container" id="features">
                    <%--<div class="sidebar-section">--%>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:HyperLink ID="hlink" runat="server" NavigateUrl="AdminroleCreate.aspx" class="btn btn-primary btn-block" Style="border: none; padding: 10px;"><i class="fa fa-plus"></i> Create Custom Role</asp:HyperLink>
                                <hr />
                                <div class="well">
                                    <p>Filter by:</p>
                                    <div id="fm1">
                                        <div class="form-group">
                                            <label for="classyear">Role Name:</label>
                                            <asp:TextBox ID="txtRoleName" runat="server" class="form-control" />
                                        </div>
                                         <%if (IsStandardSchool())
                                                                { %>
                                        <div class="form-group">
                                            <label for="classyear">Role Type:</label>
                                            <asp:DropDownList ID="drpTeacherRole" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Select Role Type" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Teaching Role" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Non Teaching Role" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                          <%} %>
                                        <div class="form-group">
                                            <label for="classyear">Status:</label>
                                            <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <center><asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="row">
                                    <asp:ListView runat="server" ID="lstAdminRoles" OnItemCommand="lstAdminRoles_ItemCommand" OnPagePropertiesChanging="lstAdminRoles_PagePropertiesChanging" OnDataBound="lstAdminRoles_DataBound">
                                        <GroupTemplate>
                                            <div runat="server" id="itemPlaceholder"></div>
                                        </GroupTemplate>
                                        <ItemTemplate>

                                            <div class="col-md-6">
                                                <div class="class-box feature">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <p>
                                                                <b>Role Name:</b> <%# Eval("RoleName") %> <span class="pull-right">
                                                                    <asp:HyperLink ID="HyperLink9" runat="server" Style="font-size: 15px;" NavigateUrl='<%# "adminroleview.aspx?ID="+Eval("ID") %>' Text="View Detail"></asp:HyperLink></span>
                                                            </p>
                                                           
                                                            <p>
                                                                <b>Role Type:</b> <%# Eval("RoleTypes") %> <span class="pull-right" style="margin-top: 10px;">
                                                                    <%--<b>Created By:</b> <%# Eval("strCreateBy") %> <span class="pull-right" style="margin-top: 10px;">--%>
                                                                    
                                                                    
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="lnkDelete" Visible ='<%# (!Convert.ToBoolean(Eval("IsDefaultRole")))%>' CommandArgument='<%# Eval("ID") %>' OnClientClick="return ConfirmMsg();" class="btn btn-danger">Delete</asp:LinkButton>
                                                                </span>                                                        
                                                        </p>
                                                          
                                                                <p><b>Date Created:</b> <%# Eval("strCreatedDate") %> </p>
                                                        <p><b>Status:</b> <%# Eval("Status") %> </p>

                                                    </div>
                                                </div>
                                            </div>
                                            </div>

                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <table runat="server" class="col-md-12">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <tr runat="server" id="groupPlaceholder"></tr>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <asp:DataPager runat="server" PageSize="10" ID="DataPager1">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                                <asp:NumericPagerField></asp:NumericPagerField>
                                                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                    </asp:ListView>
                                </div>
                            </div>

                        </div>
                    </div>
                    <%--</div>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />
        <br />
        <br />
        <br />
    </section>
    <script type="text/javascript">

        function ConfirmMsg() {
            if (confirm("Do you want this class to be deleted ? Users assigned to this role will be assigned “Standard role”")) {
                //SetID(ID);
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
