<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BulkData.aspx.cs" Inherits="SchoolApp.Web.School.BulkData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <link href="../css/jquery.steps.css" rel="stylesheet" />
    <style>
        .rightform .control-label {
            padding-left: 0px !important;
            padding-right: 0px !important;
        }

        #wizHeader li .prevStep {
            background-color: #669966 !important;
        }

            #wizHeader li .prevStep:after {
                border-left-color: #669966 !important;
            }

        #wizHeader li .currentStep {
            background-color: #C36615 !important;
        }

            #wizHeader li .currentStep:after {
                border-left-color: #C36615 !important;
            }

        #wizHeader li .nextStep {
            background-color: #C2C2C2 !important;
        }

            #wizHeader li .nextStep:after {
                border-left-color: #C2C2C2 !important;
            }

        #wizHeader {
            list-style: none !important;
            overflow: hidden !important;
            font: 18px Helvetica, Arial, Sans-Serif !important;
            margin: 0px !important;
            padding: 0px;
            /*padding-top:22px !important;*/
        }

            #wizHeader li {
                float: left !important;
            }

                #wizHeader li a {
                    color: white !important;
                    text-decoration: none !important;
                    padding: 10px 0 10px 55px !important;
                    background: brown !important;
                    background: hsla(34,85%,35%,1) !important;
                    position: relative !important;
                    display: block !important;
                    float: left !important;
                }

                    #wizHeader li a:after {
                        content: " " !important;
                        display: block !important;
                        width: 0 !important;
                        height: 0 !important;
                        border-top: 50px solid transparent !important;
                        border-bottom: 50px solid transparent !important;
                        border-left: 30px solid hsla(34,85%,35%,1) !important;
                        position: absolute !important;
                        top: 50% !important;
                        margin-top: -50px !important;
                        left: 100% !important;
                        z-index: 2 !important;
                    }

                    #wizHeader li a:before {
                        content: " " !important;
                        display: block !important;
                        width: 0 !important;
                        height: 0 !important;
                        border-top: 50px solid transparent !important;
                        border-bottom: 50px solid transparent !important;
                        border-left: 30px solid white !important;
                        position: absolute !important;
                        top: 50% !important;
                        margin-top: -50px !important;
                        margin-left: 1px !important;
                        left: 100% !important;
                        z-index: 1 !important;
                    }

                #wizHeader li:first-child a {
                    padding-left: 10px !important;
                }

                #wizHeader li:last-child {
                    padding-right: 50px !important;
                }

                #wizHeader li a:hover {
                    background: #FE9400 !important;
                }

                    #wizHeader li a:hover:after {
                        border-left-color: #FE9400 !important;
                    }
    </style>
    <style>
        .brand-pills > li > a {
            border-top-right-radius: 0px;
            border-bottom-right-radius: 0px;
        }

        li.brand-nav.active a:after {
            content: " ";
            display: block;
            width: 0;
            height: 0;
            border-top: 30px solid transparent;
            border-bottom: 30px solid transparent;
            border-left: 13px solid #00a7ff;
            position: absolute;
            top: 50%;
            margin-top: -30px;
            left: 100%;
            z-index: 0;
        }

        .nav-pills > li.active > a, .nav-pills > li.active > a:hover, .nav-pills > li.active > a:focus {
            color: #fff;
            background-color: #00a7ff;
        }

        .nav > li > a {
            position: relative;
            display: block;
            padding: 20px 15px;
        }

        .panel-body a {
            border-bottom: 2px solid #eaeaea;
            padding: 7px 0px;
            display: block;
            cursor: pointer;
        }

        .sidebar-section {
            min-height: 500px;
        }


        .template_upload ul li {
            color: #AAAAAA;
            display: block;
            position: relative;
            float: left;
            width: 100%;
        }

            .template_upload ul li input[type=radio] {
                position: absolute;
                visibility: hidden;
            }

            .template_upload ul li label {
                display: block;
                position: relative;
                font-weight: 300;
                font-size: 1.35em;
                padding: 25px 25px 25px 50px;
                margin: 5px auto;
                height: 30px;
                z-index: 9;
                cursor: pointer;
                -webkit-transition: all 0.25s linear;
            }

            .template_upload ul li:hover label {
                color: #232323;
            }

            .template_upload ul li .check {
                display: block;
                position: absolute;
                border: 5px solid #AAAAAA;
                border-radius: 100%;
                height: 25px;
                width: 25px;
                top: 30px;
                left: 20px;
                z-index: 5;
                transition: border .25s linear;
                -webkit-transition: border .25s linear;
            }

            .template_upload ul li:hover .check {
                border: 5px solid #232323;
            }

            .template_upload ul li .check::before {
                display: block;
                position: absolute;
                content: '';
                border-radius: 100%;
                height: 10px;
                width: 10px;
                top: 3px;
                left: 3px;
                margin: auto;
                transition: background 0.25s linear;
                -webkit-transition: background 0.25s linear;
            }

        .template_upload input[type=radio]:checked ~ .check {
            border: 5px solid #00a7ff;
        }

            .template_upload input[type=radio]:checked ~ .check::before {
                background: #00a7ff;
            }

        .template_upload input[type=radio]:checked ~ label {
            color: #00a7ff;
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
                            / <span>Bulk Data</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features">
            <div class="sidebar-section ">
                <div class="container">
                    <div class="row">

                        <div role="tabpanel">
                            <div class="col-md-2" style="width: 19.667% !important;">
                                <ul class="nav nav-pills brand-pills nav-stacked" role="tablist">
                                    <li role="presentation" class="brand-nav active">
                                        <asp:HyperLink ID="hlink1" runat="server" NavigateUrl="#tab1" aria-controls="tab1" role="tab" data-toggle="tab">Template Downloads <i class="pull-right fa fa-download"></i></asp:HyperLink></li>
                                    <li role="presentation" class="brand-nav">
                                        <asp:HyperLink ID="hlink2" runat="server" NavigateUrl="#tab2" aria-controls="tab2" role="tab" data-toggle="tab">Template Uploads <i class="pull-right fa fa-upload"></i></asp:HyperLink></li>
                                    <%--<asp:Panel ID="PanelAfterSchool" runat="server">--%>
                                    <li role="presentation" class="brand-nav">
                                        <asp:HyperLink ID="hlink3" runat="server" NavigateUrl="#tab3" aria-controls="tab3" role="tab" data-toggle="tab">Student Class Rollover <i class="pull-right fa fa-refresh"></i></asp:HyperLink></li>
                                    <li role="presentation" class="brand-nav">
                                        <asp:HyperLink ID="hlink4" runat="server" NavigateUrl="#tab4" role="tab" data-toggle="tab">Teacher Class Rollover <i class="pull-right fa fa-repeat"></i></asp:HyperLink></li>
                                    <%--</asp:Panel>--%>
                                </ul>
                            </div>
                            <div class="col-md-10" style="width: 80.333% !important;">
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
                                                    <div class="col-md-4">
                                                        <a href="../Template/Upload/Student.xlsx">Create Student Template<i class="pull-right fa fa-download"></i></a>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <a href="../Template/Upload/UpdateStudent.xlsx"><i class="pull-right fa fa-download"></i>Update Student Template</a>
                                                    </div>
                                               <%-- </div>
                                                <div class="panel-body">--%>
                                                    <div class="col-md-4">
                                                        <asp:LinkButton ID="lnkMasterClick" runat="server" OnClick="lnkMasterClick_Click"><i class="pull-right fa fa-download"></i>Master File Data</asp:LinkButton>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:LinkButton ID="lnkStudentExportReports" runat="server" OnClick="lnkStudentExportReports_Click"><i class="pull-right fa fa-download"></i>Student Reports</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <asp:Panel ID="PanelClass" runat="server">
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
                                                        <%if (IsStandardSchool())
                                                            { %>
                                                        <div class="col-md-4">
                                                            <a href="../Template/Upload/Class.xlsx">Create Class Template<i class="pull-right fa fa-download"></i></a>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <a href="../Template/Upload/UpdateClass.xlsx">Update Class Template<i class="pull-right fa fa-download"></i></a>
                                                        </div>
                                                        <%} %>
                                                        <%else { %>

                                                        <div class="col-md-4">
                                                            <a href="../Template/Upload/AfterSchoolClass.xlsx">Create Class Template<i class="pull-right fa fa-download"></i></a>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <a href="../Template/Upload/AfterSchoolUpdateClass.xlsx">Update Class Template<i class="pull-right fa fa-download"></i></a>
                                                        </div>

                                                        <%} %>

                                                   <%-- </div>
                                                    <div class="panel-body">--%>
                                                        <div class="col-md-4">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkMasterClick_Click"><i class="pull-right fa fa-download"></i>Master File Data</asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lnkClassReportClick_Click"><i class="pull-right fa fa-download"></i>Class Report</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </asp:Panel>
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
                                                    <div class="col-md-4">
                                                        <a href="../Template/Upload/Teacher.xlsx">Create Teacher Template<i class="pull-right fa fa-download"></i></a>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <a href="../Template/Upload/UpdateTeacher.xlsx">Update Teacher Template<i class="pull-right fa fa-download"></i></a>
                                                    </div>
                                               <%-- </div>
                                                <div class="panel-body">--%>
                                                    <div class="col-md-4">
                                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnkMasterClick_Click"><i class="pull-right fa fa-download"></i>Master File Data</asp:LinkButton>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lnkTeacherReport_Click"><i class="pull-right fa fa-download"></i>Teacher Report</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <% if(IsStandardSchool()){ %>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#filterPanel4">Non Teacher Template</a>
                                                    <span class="pull-right panel-collapse-clickable" data-toggle="collapse" data-parent="#accordion" href="#filterPanel">
                                                        <i class="glyphicon glyphicon-chevron-down"></i>
                                                    </span>
                                                </h4>
                                            </div>
                                            <div id="filterPanel4" class="panel-collapse  collapse">                                                
                                                <div class="panel-body">
                                                    <div class="col-md-4">
                                                        <a href="../Template/Upload/NonTeacher.xlsx">Create Non Teacher Template<i class="pull-right fa fa-download"></i></a>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <a href="../Template/Upload/UpdateNonTeacher.xlsx">Update Non Teacher Template<i class="pull-right fa fa-download"></i></a>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:LinkButton ID="LinkButton5" runat="server" OnClick="lnkMasterClick_Click"><i class="pull-right fa fa-download"></i>Master File Data</asp:LinkButton>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lnkNonTeacherReport_Click"><i class="pull-right fa fa-download"></i>Non Teacher Report</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <%} %>
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
                                                <asp:Panel ID="PanelClass1" runat="server">
                                                    <asp:RadioButton ID="rbtnClass" runat="server" GroupName="Selector" Text="Class" />
                                                    <div class="check">
                                                        <div class="inside"></div>
                                                    </div>
                                                </asp:Panel>
                                            </li>
                                            <li>
                                                <asp:RadioButton ID="rbtnTeacher" runat="server" GroupName="Selector" Text="Teacher" />
                                                <div class="check">
                                                    <div class="inside"></div>
                                                </div>
                                            </li>
                                            <%if (IsStandardSchool())
                                                { %>
                                            <li>
                                                <asp:RadioButton ID="rbtnNonTeacher" runat="server" GroupName="Selector" Text="Non-Teacher" />
                                                <div class="check">
                                                    <div class="inside"></div>
                                                </div>
                                            </li>
                                            <%} %>
                                        </ul>
                                        <div class="">
                                            <asp:Button ID="btnUpload" runat="server" class="btn btn-primary" Style="margin-left: 20px; padding: 10px 10px; margin-top: 20px !important;" Text="Upload" OnClick="btnUpload_Click" />
                                        </div>
                                    </div>

                                    <div role="tabpanel" class="tab-pane" id="tab3">
                                        <p>
                                            Class Rollover MUST be processed starting from higher class, updating it and working the way down to lower classes.
                                        </p>
                                        <small style="color: #00a7ff;">(For example, begin with class 5 to 6 and work down to class 1-2)
                                        </small>
                                        <p style="color: red !important">
                                            Note : When Finished Rollover Must need to Click on FINISH ROLLOVER Button
                                        </p>

                                        <asp:UpdatePanel ID="Up1" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <%--<div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <h2>Level Transfer</h2>
                                                            <asp:DropDownList ID="ddlLevelTransfer" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLevelTransfer_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6 col-xs-6">
                                                        <label for="leveltransfer">Class From:</label>
                                                    </div>
                                                    <div class="col-md-6 col-xs-6">
                                                        <label for="leveltransfer">Class To:</label>
                                                    </div>
                                                </div>
                                                <asp:ListView ID="lstClass" runat="server" OnItemDataBound="lstClass_ItemDataBound" OnItemEditing="lstClass_ItemEditing" OnItemUpdating="lstClass_ItemUpdating">
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                            <div class="col-md-6 col-xs-6">
                                                                <p>
                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </p>
                                                            </div>
                                                            <div class="col-md-6 col-xs-6">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="drpClass" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>--%>
                                                <asp:Wizard ID="Wizard1" OnNextButtonClick="Wizard1_NextButtonClick" runat="server" DisplaySideBar="false" Width="100%">
                                                    <HeaderTemplate>
                                                        <ul id="wizHeader">
                                                            <asp:Repeater ID="SideBarList" runat="server">
                                                                <ItemTemplate>
                                                                    <li><a class="<%# GetClassesForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                                                        <%# Eval("Name")%></a> </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </HeaderTemplate>
                                                    <WizardSteps>
                                                        <asp:WizardStep ID="WizardStep1" runat="server" Title="5 to 6">
                                                            <div class="panel panel-default">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="float-e-margins">
                                                                            <div class="form-horizontal formelement">
                                                                                <br />
                                                                                <div class="row">
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class From:</label>
                                                                                    </div>
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class To:</label>
                                                                                    </div>
                                                                                </div>
                                                                                <asp:ListView ID="lst56" runat="server" OnItemDataBound="lst56_ItemDataBound" OnItemEditing="lst56_ItemEditing">
                                                                                    <ItemTemplate>
                                                                                        <div class="row">
                                                                                            <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <p>
                                                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                                </p>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <div class="form-group">
                                                                                                    <asp:DropDownList ID="drpClass" runat="server" class="form-control">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" ValidationGroup="Submit" ControlToValidate="drpClass" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:WizardStep>
                                                        <asp:WizardStep ID="WizardStep2" runat="server" Title="4 to 5">
                                                            <div class="panel panel-default">

                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="float-e-margins">
                                                                            <div class="form-horizontal formelement">
                                                                                <br />
                                                                                <div class="row">
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class From:</label>
                                                                                    </div>
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class To:</label>
                                                                                    </div>
                                                                                </div>
                                                                                <asp:ListView ID="lst45" runat="server" OnItemDataBound="lst45_ItemDataBound" OnItemEditing="lst45_ItemEditing">
                                                                                    <ItemTemplate>
                                                                                        <div class="row">
                                                                                            <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <p>
                                                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                                </p>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <div class="form-group">
                                                                                                    <asp:DropDownList ID="drpClass" runat="server" class="form-control">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" ValidationGroup="Submit" ControlToValidate="drpClass" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:WizardStep>
                                                        <asp:WizardStep ID="WizardStep3" runat="server" Title="3 to 4">
                                                            <div class="panel panel-default">

                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="float-e-margins">
                                                                            <div class="form-horizontal formelement">
                                                                                <br />
                                                                                <div class="row">
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class From:</label>
                                                                                    </div>
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class To:</label>
                                                                                    </div>
                                                                                </div>
                                                                                <asp:ListView ID="lst34" runat="server" OnItemDataBound="lst34_ItemDataBound" OnItemEditing="lst34_ItemEditing">
                                                                                    <ItemTemplate>
                                                                                        <div class="row">
                                                                                            <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <p>
                                                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                                </p>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <div class="form-group">
                                                                                                    <asp:DropDownList ID="drpClass" runat="server" class="form-control">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" ValidationGroup="Submit" ControlToValidate="drpClass" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:WizardStep>
                                                        <asp:WizardStep ID="WizardStep4" runat="server" Title="2 to 3">
                                                            <div class="panel panel-default">

                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="float-e-margins">
                                                                            <div class="form-horizontal formelement">
                                                                                <br />
                                                                                <div class="row">
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class From:</label>
                                                                                    </div>
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class To:</label>
                                                                                    </div>
                                                                                </div>
                                                                                <asp:ListView ID="lst23" runat="server" OnItemDataBound="lst23_ItemDataBound" OnItemEditing="lst23_ItemEditing">
                                                                                    <ItemTemplate>
                                                                                        <div class="row">
                                                                                            <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <p>
                                                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                                </p>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <div class="form-group">
                                                                                                    <asp:DropDownList ID="drpClass" runat="server" class="form-control">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" ValidationGroup="Submit" ControlToValidate="drpClass" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:WizardStep>
                                                        <asp:WizardStep ID="WizardStep5" runat="server" Title="1 to 2">
                                                            <div class="panel panel-default">

                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="float-e-margins">
                                                                            <div class="form-horizontal formelement">
                                                                                <br />
                                                                                <div class="row">
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class From:</label>
                                                                                    </div>
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class To:</label>
                                                                                    </div>
                                                                                </div>
                                                                                <asp:ListView ID="lst12" runat="server" OnItemDataBound="lst12_ItemDataBound" OnItemEditing="lst12_ItemEditing">
                                                                                    <ItemTemplate>
                                                                                        <div class="row">
                                                                                            <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <p>
                                                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                                </p>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <div class="form-group">
                                                                                                    <asp:DropDownList ID="drpClass" runat="server" class="form-control">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" ValidationGroup="Submit" ControlToValidate="drpClass" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:WizardStep>
                                                        <asp:WizardStep ID="WizardStep6" runat="server" Title="Reception to 1">
                                                            <div class="panel panel-default">

                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="float-e-margins">
                                                                            <div class="form-horizontal formelement">
                                                                                <br />
                                                                                <div class="row">
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class From:</label>
                                                                                    </div>
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class To:</label>
                                                                                    </div>
                                                                                </div>
                                                                                <asp:ListView ID="lstR1" runat="server" OnItemDataBound="lstR1_ItemDataBound" OnItemEditing="lstR1_ItemEditing">
                                                                                    <ItemTemplate>
                                                                                        <div class="row">
                                                                                            <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <p>
                                                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                                </p>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <div class="form-group">
                                                                                                    <asp:DropDownList ID="drpClass" runat="server" class="form-control">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" ValidationGroup="Submit" ControlToValidate="drpClass" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:WizardStep>
                                                        <asp:WizardStep ID="WizardStep7" runat="server" Title="Nursery to Reception">
                                                            <div class="panel panel-default">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="float-e-margins">
                                                                            <div class="form-horizontal formelement">
                                                                                <br />
                                                                                <div class="row">
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class From:</label>
                                                                                    </div>
                                                                                    <div class="col-md-6 col-xs-6">
                                                                                        <label for="leveltransfer">Class To:</label>
                                                                                    </div>
                                                                                </div>
                                                                                <asp:ListView ID="lstNR" runat="server" OnItemDataBound="lstNR_ItemDataBound" OnItemEditing="lstNR_ItemEditing">
                                                                                    <ItemTemplate>
                                                                                        <div class="row">
                                                                                            <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <p>
                                                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                                </p>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-xs-6">
                                                                                                <div class="form-group">
                                                                                                    <asp:DropDownList ID="drpClass" runat="server" class="form-control">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" ValidationGroup="Submit" ControlToValidate="drpClass" runat="server" ErrorMessage="<a class='ourtooltip'>Required</a>"></asp:RequiredFieldValidator>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:WizardStep>
                                                    </WizardSteps>
                                                    <StepNavigationTemplate>
                                                        <div class="form-group" style="margin-top: 5px">
                                                            <asp:Button ID="btnReset" CssClass="btn btn-reset" runat="server" Text="Previous" OnClick="btnReset_Click" />
                                                            <asp:Button ID="btnSave" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="Submit" OnClick="btnSave_Click" runat="server" Text="Save & Next" />&nbsp;&nbsp;
                                                        </div>
                                                    </StepNavigationTemplate>
                                                    <StartNavigationTemplate>
                                                        <div class="form-group" style="margin-top: 5px">
                                                            <asp:Button ID="btnSave" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="Submit" OnClick="btnSave_Click" runat="server" Text="Save & Next" />&nbsp;&nbsp;
                                                        </div>
                                                    </StartNavigationTemplate>
                                                    <FinishNavigationTemplate>
                                                        <div class="form-group" style="margin-top: 5px">
                                                            <asp:Button ID="btnReset" CssClass="btn btn-reset" runat="server" Text="Previous" OnClick="btnReset_Click" />
                                                            <asp:Button ID="btnSave" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="Submit" OnClick="btnSave_Click" runat="server" Text="Save" />&nbsp;&nbsp;
                                                        </div>
                                                    </FinishNavigationTemplate>

                                                </asp:Wizard>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <%--<asp:Button ID="btnFinish" runat="server" Style="margin-left: 5px !important" class="btn btn-primary pull-right" Text="Finish RollOver" OnClick="btnFinish_Click" />--%>
                                    </div>

                                    <div role="tabpanel" class="tab-pane" id="tab4">
                                        <p>
                                            Select Class From and Class To below to mass move TEACHERS from one class to the other.
                       
                                        </p>
                                        <small style="color: #00a7ff;">(Maximum of 5 classes can be bulk rollover at a time)
                                        </small>
                                        <p style="color: red !important">
                                            Note : FINISH ROLLOVER Button Must Be Clicked To Complete The Teacher Rollover Exercise
                                        </p>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="row" style="margin-top: 20px;">
                                                    <div class="col-md-4 col-xs-4">
                                                        <label for="leveltransfer">Class From:</label>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4">
                                                        <label for="leveltransfer">Class To:</label>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <label for="leveltransfer">Add:</label>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <label for="leveltransfer">Update:</label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassFrom1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpClassFrom1_SelectedIndexChanged" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassTo1" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClassTo1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd1" Checked="true" runat="server" GroupName="rblist1" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd2" runat="server" GroupName="rblist1" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassFrom2" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClassFrom2_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassTo2" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClassTo2_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd3" Checked="true" runat="server" GroupName="rblist2" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd4" runat="server" GroupName="rblist2" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassFrom3" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClassFrom3_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassTo3" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClassTo3_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd5" Checked="true" runat="server" GroupName="rblist3" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd6" runat="server" GroupName="rblist3" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassFrom4" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClassFrom4_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassTo4" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClassTo4_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd7" Checked="true" runat="server" GroupName="rblist4" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd8" runat="server" GroupName="rblist4" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassFrom5" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClassFrom5_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="drpClassTo5" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClassTo5_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd9" Checked="true" runat="server" GroupName="rblist5" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-xs-1">
                                                        <div class="form-group">
                                                            <asp:RadioButton ID="rd10" runat="server" GroupName="rblist5" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <asp:Button ID="btnFinishTeacher" runat="server" Style="margin-left: 5px !important" class="btn btn-primary pull-right" Text="Finish RollOver" OnClick="btnFinishTeacher_Click" />
                                                <asp:Button ID="btnUpdateTeacherClass" runat="server" class="btn btn-primary pull-right" Text="Update" OnClick="btnUpdateTeacherClass_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
    <script src="../Wizard/js/jquery.steps.js"></script>
</asp:Content>
