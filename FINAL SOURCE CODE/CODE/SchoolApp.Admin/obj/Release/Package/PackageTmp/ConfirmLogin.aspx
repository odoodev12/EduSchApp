<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmLogin.aspx.cs" Inherits="SchoolApp.Admin.ConfirmLogin" %>

<%@ Register Src="~/UserControl/Loader.ascx" TagPrefix="uc1" TagName="Loader" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SCHOOL APP : LOGIN</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../Content/project.css" rel="stylesheet" />
    <link href="../Content/lableWidth.css" rel="stylesheet" />
    <!-- Toastr style -->
    <link href="../Content/css/plugins/toastr/toastr.min.css" rel="stylesheet" />

    <!-- Gritter -->
    <link href="../Content/js/plugins/gritter/jquery.gritter.css" rel="stylesheet" />

    <link href="../Content/css/animate.css" rel="stylesheet" />
    <link href="../Content/style.css" rel="stylesheet" />
    <style>
        .form-control, .single-line {
            border: 1px solid #e5e6e7 !important;
        }
        .modal-dialog i{
            font-size:45px;
        }
         .modal-dialog p{
            color:#ed5565;
        }

        
    </style>
    <script type="text/javascript">
        function showModal() {
            $("#modal-form").modal('show');
        }

        //$(function () {
        //    $("#btnSignin").click(function () {
        //        showModal();
        //    });
        //});
        function hideModal() {
            $("#modal-form").modal('hide');
        }
        $(function () {
            $("#btnclose").click(function () {
                hideModal();
            });
        });
        $(function () {
            $("#btnclose1").click(function () {
                hideModal();
            });
        });
    </script>
</head>
<body class="gray-bg">
    <form id="form1" runat="server">
        <div class="loginColumns animated fadeInDown">
            <div class="row">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="MainUpdatePanel">
                    <ProgressTemplate>
                        <uc1:Loader runat="server" ID="Loader" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="MainUpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-6">


                            <div class="ibox-content">
                                <div class="panel-body">
                                    <div class="text-center">
                                        <div class="icon-object border-slate-300 text-slate-300">

                                            <img width="100px" height="100px" src='<%= Page.ResolveClientUrl("~/Upload/university.png")%>' />

                                        </div>
                                        <h5 class="content-group">SCHOOL APP<br />

                                            <small class="display-block">Enter your credentials to login</small></h5>
                                    </div>
                                    <br />
                                    <br />

                                    <br />                                    
                                    <div class="input-group m-b">                                        
                                        <asp:Label ID="lblMemorableQue" Text="Select character from your memorable word" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="lblFirstAns" required="" CssClass="form-control" runat="server"></asp:Label>
                                        <asp:DropDownList ID="drpFirstAns" required="" CssClass="form-control" runat="server"/>
                                    </div>
                                    <div>
                                        <asp:Label ID="lblSecondAns" required="" CssClass="form-control" runat="server"></asp:Label>
                                        <asp:DropDownList ID="drpSecondAns" required="" CssClass="form-control" runat="server"/>
                                    </div>
                                    <div>
                                        <asp:Label ID="lblThirdAns" required="" CssClass="form-control" runat="server"></asp:Label>
                                        <asp:DropDownList ID="drpThirdAns" required="" CssClass="form-control" runat="server"/>
                                    </div>
                                    <asp:Panel ID="pnlInvalid" class="form-group text-center " Visible="false" runat="server">
                                        <span class="label label-danger">Oops! Invalid Credential Entered</span>
                                    </asp:Panel>

                                    <div class="form-group ">
                                        <div class="col-sm-12 no-padding-left no-padding-right">
                                            <asp:Button ID="btnSubmit" class="btn btn-primary block full-width" ValidationGroup="Submit" OnClick="btnSubmit_Click" runat="server"  Text="Submit" />
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-md-6">
                    <div id="modal-form" class="modal fade" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                    <center><i class="fa fa-user modal-icon"></i></center>
                                    <center><h4 class="modal-title">Authenication Failed</h4></center>
                                    <center><small class="font-bold">contact administrator to get your credential</small></center>
                                </div>
                                <div class="modal-body">
                                    <p >invalid your memorable answer, please contact your administrator get more details.</p>

                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-white" data-dismiss="modal">Close</button>

                                </div>

                                <%--<div class="modal-body">
                                    <div class="row">
                                        <div class="ibox-tools">
                                            <button type="button" id="btnclose" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></button>
                                        </div>
                                        <center><h2><i class="fa fa-user medium-icon" aria-hidden="true"></i></h2></center>
                                        <center><h2>Authenication Failed</h2></center>
                                    </div>
                                    <div class="row">
                                        <div class="ibox-content">
                                            <div class="pull-right">
                                                <button type="button" id="btnclose1" class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-4">
                    Codesture Techno Pvt. Ltd.
                </div>
                <div class="col-md-2 text-right">
                    <small>© 2015-2016</small>
                </div>
                <div class="col-md-3">
                </div>
            </div>
        </div>
    </form>
    <script src="Scripts/jquery-2.1.1.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="Scripts/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <!-- Custom and plugin javascript -->
    <script src="Scripts/inspinia.js"></script>
    <script src="Scripts/plugins/pace/pace.min.js"></script>

</body>
</html>