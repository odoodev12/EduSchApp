<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPickupReport.aspx.cs" Inherits="SchoolApp.Web.Parent.ViewPickupReport" %>

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

        .modal-body {
            padding: 15px;
        }

        .view_detail a {
            font-size: 10px;
        }

        .color_blue {
            color: #00a7ff;
        }

        .color_red {
            color: #FF0000;
        }
        .responsives {
            width: 100px !important;
            max-height: 100px !important;
            max-width: 100px !important;
            height: 100px !important;
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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink> / 
                            <asp:HyperLink ID="hlinkPickupReport" runat="server" NavigateUrl="~/Parent/PickupReport.aspx" Text="Pickup Report"></asp:HyperLink>
                            / <span>Student Pickup Report</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features">
            <div class="sidebar-section">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12 well">
                            <div class="row">
                                <asp:ListView ID="lstStudeInfo" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-2 col-xs-5">
                                            <%--<img src='<%# "../" + Eval("StudentPic") %>' class="img-circle" width="100px" style="border: 2px solid #00a7ff;" />--%>
                                            <a class="example-image-link" href='<%# "../" + Eval("StudentPic") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'><img class="example-image img-circle responsives" style="border: 2px solid #00a7ff;" src='<%# "../" + Eval("StudentPic") %>' alt="" /></a>
                                        </div>
                                        <div class="col-md-10 col-xs-7">
                                            <h3>
                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label></h3>
                                            <%--Pickup Average : <asp:Label ID="lblAverage" runat="server" Text='<%# Eval("StudentPickUpAverage") %>'></asp:Label>--%>
                                            Pickup Average : <asp:Label ID="lblAverage" runat="server" Text='<%# String.Format("{0:0.00}",Eval("StudentPickUpAverage")) %>'></asp:Label>
                                            <asp:Label runat="server" Text='<%# Eval("SchoolName") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="well" id="LargeFiler">

                                    <p>Filter by:</p>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="">From Date:</asp:Label>
                                        <asp:TextBox runat="server" type="date" ID="txtFromDate" class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="">To Date:</asp:Label>
                                        <asp:TextBox runat="server" type="date" ID="txtToDate" class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="studentname">Picker</asp:Label>
                                        <asp:DropDownList runat="server" ID="drpPicker" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" for="classname">Status</asp:Label>
                                        <asp:DropDownList ID="drpStatus" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>

                                    <hr />
                                    <p>Sort by:</p>

                                    <div class="form-group">
                                        <asp:DropDownList ID="drpSort" runat="server" class="form-control">
                                            <asp:ListItem Value="Date" Text="Date" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Status" Text="Pickup Status"></asp:ListItem>
                                            <asp:ListItem Value="Picker" Text="Picker Name"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <div style="margin-top: 10px;">
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbAsc" runat="server" GroupName="optradio" />Ascending
                                            </label>

                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbDesc" runat="server" GroupName="optradio" Checked="true" />Descending
                                            </label>
                                        </div>
                                    </div>
                                    <center><asp:Button runat="server" Text="Apply" class="btn btn-primary" ID="btnApply" OnClick="btnApply_Click"></asp:Button> &nbsp;
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click"></asp:Button></center>

                                </div>
                                <asp:Button runat="server" Text="Export to Excel" class="btn btn-primary btn-block" OnClick="btnExport_Click"></asp:Button>
                                <asp:Button runat="server" Text="Send to Email" ID="btnSendEmail" class="btn btn-primary btn-block" OnClick="btnSendEmail_Click"></asp:Button>

                            </div>
                           <%-- <div class="col-md-3 col-xs-12 table-responsive" style="padding: 0px;">
                            </div>--%>


                            <div class="col-md-9 col-xs-12 table-responsive" style="padding: 0px;">
                                <asp:Label ID="lblInstruction" runat="server" style="color:red !important; float:right !important;">Only 30 days record is displayed. Please use Filter section to see more</asp:Label>
                                <table class="table">
                                    <thead style="background: #232323; color: white;">
                                        <tr>
                                            <th>Date</th>
                                            <th>Status</th>
                                            <th>Time</th>
                                            <th>Picker Name</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lstTable" runat="server" OnPagePropertiesChanging="lstTable_PagePropertiesChanging">
                                            <GroupTemplate>
                                                <td runat="server" id="itemPlaceholder"></td>
                                            </GroupTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="border-bottom: 1px solid #ddd !important; width:15% !important;"><%# Eval("Pick_Date") %></td>
                                                    <td style="border-bottom: 1px solid #ddd !important; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><%# Eval("PickStatus") %></td>
                                                    <td style="border-bottom: 1px solid #ddd !important; width:15% !important;"><%# Eval("Pick_Time") %></td>
                                                    <td style="border-bottom: 1px solid #ddd !important; width:20% !important;"><%# Eval("PickerName") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <LayoutTemplate>
                                                <tr runat="server">
                                                    <td runat="server" colspan="4">
                                                        <tr runat="server" id="groupPlaceholder"></tr>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <asp:DataPager runat="server" PageSize="31" ID="DataPager1">
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
        </div>
    </section>
</asp:Content>

