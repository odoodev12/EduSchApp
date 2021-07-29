<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="SchoolApp.Admin.FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .modal-lg {
            width: 900px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:HiddenField ID="HID" runat="server" />
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server">
        <ContentTemplate>--%>
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="col-sm-4">
                    <h2>FAQ</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="Dashboard.aspx">Home</a>
                        </li>
                        <li class="active">
                            <strong>Frequently asked questions</strong>
                        </li>
                    </ol>
                </div>
                <div class="col-sm-8">
                    <div class="title-action">
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal4">
                            <i class="fa fa-plus"></i><span class="bold">&nbsp;Add question</span>
                        </button>
                        <%--<a href="" class="btn btn-primary btn-sm">Add question</a>--%>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="wrapper wrapper-content animated fadeInRight">
                        <div class="ibox-content m-b-sm border-bottom">
                            <div class="text-center p-lg">
                                <h2>If you don't find the answer to your question</h2>
                                <span>add your question by selecting </span>
                                <%--<button title="Create new cluster" class="btn btn-primary btn-sm" onclick="return false;"><i class="fa fa-plus"></i><span class="bold">&nbsp;Add question</span></button>--%>
                                button
                            </div>
                        </div>
                        <div class="faq-item">
                            <div class="row">
                                <div class="col-md-7">
                                    <a data-toggle="collapse" href="#faq1" class="faq-question">What It a long established fact that a reader ?</a>
                                    <small>Added by <strong>Alex Smith</strong> <i class="fa fa-clock-o"></i>Today 2:40 pm - 24.06.2014</small>
                                </div>
                                <div class="col-md-3">
                                    <span class="small font-bold">Robert Nowak</span>
                                    <div class="tag-list">
                                        <span class="tag-item">General</span>
                                        <span class="tag-item">License</span>
                                    </div>
                                </div>
                                <div class="col-md-2 text-right">
                                    <span class="small font-bold">Voting </span>
                                    <br />
                                    42
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div id="faq1" class="panel-collapse collapse ">
                                        <div class="faq-answer">
                                            <p>
                                                It is a long established fact that a reader will be distracted by the
                                                readable content of a page when looking at its layout. The point of
                                                using Lorem Ipsum is that it has a more-or-less normal distribution of
                                                letters, as opposed to using 'Content here, content here', making it
                                                look like readable English.
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal inmodal fade" id="myModal4" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" onclick="myFunction()" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                <h4 class="modal-title">Add Question</h4>
                                <small>Frequently Asked Question</small>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="float-e-margins">
                                            <div class="form-horizontal formelement">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">
                                                        <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Question</strong>
                                                            <br />
                                                            Enter Question.</span></a>Question<span class="color-red">*</span></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtQuestions" autocomplete="off" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="alert  validation" runat="server" ValidationGroup="Submit" ControlToValidate="txtQuestions" ErrorMessage="<a class='ourtooltip'><i class='fa fa-exclamation-circle'></i><span><b></b>FirstName Required</span></a>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label width-300">
                                                        <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Upload Attachment</strong><br />
                                                            Attachment to Upload.</span></a>Upload Attachment<span class="color-red">*</span></label>
                                                    <div class="col-sm-8">
                                                        <asp:FileUpload ID="UploadAttachments" runat="server" CssClass="form-control" Width="90%" Height="15%" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label width-400">
                                                        <a class='Helptooltip'><i class='fa fa-question-circle'></i><span><b></b><strong>Description</strong><br />
                                                            Enter Description.</span></a>Description<span class="color-red">*</span></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtDescriptions" Width="90%" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" onclick="myFunction()" class="btn btn-white" data-dismiss="modal">Close</button>
                                <%--<button type="button" id="searchinfo" runat="server" onserverclick="btnSave_Click" class="btn btn-primary">Save Changes</button>--%>
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" UseSubmitBehavior="false" ValidationGroup="Submit" OnClick="btnSave_Click" runat="server" Text="Save Changes" CausesValidation="false" />&nbsp;&nbsp;
                            </div>
                        </div>
                    </div>
                </div>
            </div>
       <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script>
        function myFunction() {
            document.getElementById("MainContent_txtQuestion").value = "";
            document.getElementById("MainContent_txtDescription").value = "";
            document.getElementById("MainContent_UploadAttachment").value = '';
        }
    </script>
</asp:Content>
