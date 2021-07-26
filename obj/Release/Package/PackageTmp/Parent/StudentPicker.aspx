<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentPicker.aspx.cs" Inherits="SchoolApp.Web.Parent.StudentPicker" %>

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
                margin-top: 10px;
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

            .feature a {
                text-decoration: none;
            }

        .tital {
            font-size: 16px;
            font-weight: 500;
            color: #00a7ff;
        }

        .bot-border {
            border-bottom: 3px #f8f8f8 solid;
            margin: 15px 0 5px 0
        }

        .picture {
            border-radius: 8px;
        }

        .tital .btn {
            padding: 10px;
            margin-top: 20px;
        }

        .modal-body {
            padding: 15px;
        }

        .card.hovercard {
            position: relative;
            padding-top: 0;
            overflow: hidden;
            text-align: center;
            background-color: rgba(214, 224, 226, 0.2);
        }

            .card.hovercard .cardheader {
                background: url("../img/4.jpg");
                background-size: cover;
                height: 135px;
            }

            .card.hovercard .avatar {
                position: relative;
                top: -50px;
                margin-bottom: -50px;
            }

                .card.hovercard .avatar img {
                    width: 100px;
                    height: 100px;
                    max-width: 100px;
                    max-height: 100px;
                    -webkit-border-radius: 50%;
                    -moz-border-radius: 50%;
                    border-radius: 50%;
                    border: 5px solid rgba(255, 255, 255, 0.5);
                }

        .buttons .btn {
            font-size: 12px;
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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i></asp:HyperLink>
                            / 
                            <asp:HyperLink ID="hlinkManagePicker" runat="server" NavigateUrl="~/Parent/ManagePickers.aspx" Text="Manage Picker"></asp:HyperLink>
                            / <span>
                                <asp:Label ID="lblStudentName" runat="server"></asp:Label></span></h1>

                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features" style="margin-bottom: 100px;">
            <div class="sidebar-section">
                <div class="container">
                    <div class="row">
                        <div class="col-md-3 col-sm-6">

                            <asp:ListView ID="lstStudent" runat="server">
                                <ItemTemplate>
                                    <div class="card hovercard ">
                                        <div class="cardheader">
                                        </div>
                                        <div class="avatar">
                                            <center><a class="example-image-link" href='<%# "../" + Eval("StudentPic") %>' data-lightbox="example-set"><img class="example-image img-rounded responsives" src='<%# "../" + Eval("StudentPic") %>' alt="" /></a></center>
                                            <%--<img src='<%# "../" + Eval("Photo") %>' class="img-rounded img-responsive" width="100%" />--%>
                                        </div>
                                        <div class="info">
                                            <p><%# Eval("StudentName") %></p>
                                            <p>Student No. : <%# Eval("StudentNo") %></p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:FileUpload ID="fpUpload" runat="server" CssClass="btn btn-primary btn-block" onchange="UploadFileNow()" />
                                    </div>
                                    <div class="form-group">
                                        <a href='StudentAddNewPicker.aspx?ID=<%# Eval("ID") %>' class="btn btn-primary btn-block">Add New Picker</a>
                                        <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="parent_student_add_new_picker.aspx?ID=<%# Eval("ID") %>" class="btn btn-primary btn-block" Text="Add New Picker"></asp:HyperLink>--%>
                                    </div>
                                     <div class="form-group">
                                        <a href='StudentAllPicker.aspx?ID=<%# Eval("ID") %>' class="btn btn-primary btn-block">Manage All Pickers</a>
                                        
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                        <div class="col-md-9">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class=" ">
                                        <div class="row">
                                            <div class="col-md-12 text-right" style="margin-bottom: 20px;">
                                                <h3 style="margin: 20px 0 20px;" class="pull-left page-header">Pickers
                                                <asp:label ID="lblSchoolName" runat="server" Text ="@Ola School" ForeColor="Blue"></asp:label></h3>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>

                                            <asp:ListView ID="lstStudentPicker" runat="server" OnItemDataBound="lstStudentPicker_ItemDataBound" OnItemCommand="lstStudentPicker_ItemCommand" OnPagePropertiesChanging="lstStudentPicker_PagePropertiesChanging">
                                                <ItemTemplate>
                                                    <div class="col-md-6">
                                                        <div class="class-box feature" style='<%# (Eval("StrStudentPickAssignDate").ToString() == Eval("CurrentDate").ToString()) ? "border:1px solid #00cc00": (Eval("PickerType").ToString() == "2") ? "border:1px solid #2196F3" : "border:1px solid #cccccc" %>'>
                                                            <div class="row">
                                                                <div class="col-md-8 col-xs-12" style="padding-left: 0px;">
                                                                    <h4 style="padding: 10px;"><%# Eval("PickerName") %></h4>
                                                                    <p style="font-size: 14px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                                                                        <i class="fa fa-envelope" aria-hidden="true" style="padding: 10px;"></i>
                                                                        <asp:LinkButton ID="LnkEmail" runat="server" CommandName="LnkEmail" Text='<%# Eval("PickerEmail").ToString() %>'></asp:LinkButton><%--<%# (Eval("PickerEmail").ToString() != " Enter Email") ? Eval("PickerEmail") :  "<a href='EditPicker.aspx?ID=" + Eval("PickerId") + "'>Enter Email</a>"%>--%>
                                                                    </p>
                                                                    <p style="font-size: 14px;">
                                                                        <i class="fa fa-phone" aria-hidden="true" style="padding: 10px;"></i>
                                                                        <asp:LinkButton ID="lnkPhone" runat="server" CommandName="LnkPhone" Text='<%# Eval("PickerPhone").ToString() %>'></asp:LinkButton>
                                                                        <%--<%# (Eval("PickerPhone").ToString() != " Enter Phone Number") ? Eval("PickerPhone") : "<a href='EditPicker.aspx?ID=" + Eval("PickerId") + "'> Enter Phone Number</a>"%>--%>
                                                                    </p>
                                                                    <p style="font-size: 14px; padding: 10px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;" data-toggle="tooltip" title='<%# Eval("AssignBy") %>'><b>Created By :</b> <%# Eval("CreatedByName") %></p>
                                                                    <asp:Button ID="btnGeneratePickUpCode" CommandArgument='<%# Convert.ToString(Eval("ID")) %>' CommandName="btnGenerateCode"
                                                                        runat="server" class="btn btn-primary btn-block" Style='<%# (Eval("PickerCode") != null && Eval("PickerCode").ToString() != "") ? "background: lightgrey; border: 1px solid #cccccc; color: #232323; padding: 10px;": "background: white; border: 1px solid #cccccc; color: #232323; padding: 10px;" %>'
                                                                        Text='<%# (Eval("PickerCode") != null && Eval("PickerCode").ToString() != "") ? Eval("PickerCode") : "Generate Picker Code" %>' />
                                                                    <asp:CheckBox ID="chkGenerateCodeForAll" Text="Generate code for all" runat="server"
                                                                        Enabled='<%# (Eval("PickerCode") != null && Eval("PickerCode").ToString() != "") ? false: true%>' />
                                                                    <asp:HiddenField ID="HID2" runat="server" Value='<%# Eval("PickerName") %>' />
                                                                    <asp:HiddenField ID="HIDPhone" runat="server" Value='<%# Eval("PickerPhone") %>' />
                                                                    <asp:HiddenField ID="HIDEmail" runat="server" Value='<%# Eval("PickerEmail") %>' />
                                                                </div>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("PickerName") %>'>
                                                                        <img class="example-image img-rounded responsives" src='<%# "../" + Eval("Photo") %>' alt="" /></a>
                                                                    <%--<img src='<%# "../" + Eval("Photo") %>' class="img-rounded img-responsive" width="100%" />--%>
                                                                    <asp:HiddenField ID="HIDImage" runat="server" Value='<%# Eval("PickerId") %>' />
                                                                    <asp:HiddenField ID="HIDPickerType" runat="server" Value='<%# Eval("PickerType") %>' />

                                                                    <asp:FileUpload ID="fpUploads" Visible="false" runat="server" CssClass="btn btn-Primary" Width="100%" onchange="UploadFileNow()" />
                                                                    <br />
                                                                    <label class="text-center btn-block" style="margin-top: 10px; background: white; border: 1px solid #cccccc; color: #232323; border-radius: 5px;">
                                                                        Status<br />
                                                                        <span style="color: #00a7ff;"><%# Eval("Status") %></span></label>
                                                                </div>

                                                            </div>
                                                            <div class="row buttons" style="margin-top: 10px;">
                                                                <div class="col-md-6">
                                                                    <asp:Button ID="btnRemove" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="btnRemove" class="btn btn-primary btn-block" Style="margin-top: 10px;" Text="Remove From Child" OnClientClick="return ConfirmMsgsForRemove()" />
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:Button ID="btnMakePickerToday" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="btnMakePickerToday" class="btn btn-primary btn-block" Style="margin-top: 10px;" OnClientClick="return ConfirmMsgss(this);" Text='<%#Eval("StrStudentPickAssignDate").ToString() == DateTime.Now.ToString("dd/MM/yyyy") ? "Reset Picker Today" : "Make Picker for Today" %>' />
                                                                </div>
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
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="HID" runat="server" />
    </section>
    <script>
        function UploadFileNow() {

            var value = $("#fpUpload").val();

            if (value != '') {

                $("#form1").submit();

            }
        };
        //function UploadFilesNow() {
        //    debugger;
        //    var values = $("#fpUploads").val();
        //    if (value != '') {

        //        $("#form1").submit();

        //    }
        //};
        function ConfirmMsgss(obj) {
            //if ($("#MainContent_HID").val() == "True") {
            if (obj.value != "Reset Picker Today") {
                if (confirm("Do you want to Make this Picker for today?")) {
                    return true;
                }
                else {
                    return false;
                }
                //}
            }
            else {
                if (confirm("Do You Want to Reset this Picker as Picker for Today ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function ConfirmMsgsForRemove() {
            if (confirm("Do You Want to remove this Picker from Picker for Today ?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
