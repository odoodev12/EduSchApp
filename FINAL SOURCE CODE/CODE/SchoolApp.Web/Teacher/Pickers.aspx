<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Pickers.aspx.cs" Inherits="SchoolApp.Web.Teacher.Pickers" %>


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

        .checkbox_style {
            display: block;
            position: relative;
            padding-left: 35px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: 16px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            /* Hide the browser's default checkbox */

            .checkbox_style input {
                position: absolute;
                opacity: 0;
                cursor: pointer;
            }

        /* Create a custom checkbox */

        .checkmark {
            position: absolute;
            top: 0;
            left: 0;
            height: 25px;
            width: 25px;
            background-color: #eee;
        }

        /* On mouse-over, add a grey background color */

        .checkbox_style:hover input ~ .checkmark {
            background-color: #ccc;
        }

        /* When the checkbox is checked, add a blue background */

        .checkbox_style input:checked ~ .checkmark {
            background-color: #2196F3;
        }

        /* Create the checkmark/indicator (hidden when not checked) */

        .checkmark:after {
            content: "";
            position: absolute;
            display: none;
        }

        /* Show the checkmark when checked */

        .checkbox_style input:checked ~ .checkmark:after {
            display: block;
        }

        /* Style the checkmark/indicator */

        .checkbox_style .checkmark:after {
            left: 9px;
            top: 5px;
            width: 5px;
            height: 10px;
            border: solid white;
            border-width: 0 3px 3px 0;
            -webkit-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            transform: rotate(45deg);
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
                            / <a href="pickup.aspx">Pickup</a> / <span>Pickers</span></h1>
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
                        <div class="col-sm-12 col-lg-12 col-md-12 col-xs-12 well">
                            <div class="row">
                                <div class="col-sm-5 col-lg-2 col-md-2 col-xs-5">
                                    <%--<asp:Image ID="imgStudent" runat="server" class="img-circle" Width="100px" Style="border: 2px solid #00a7ff;" />--%>
                                    <a id="AID" runat="server" class="example-image-link" data-lightbox="example-set">
                                        <asp:Image ID="imgStudent" runat="server" class="img-circle responsives" Style="border: 2px solid #00a7ff;" /></a>
                                    <%-- <img src='<%# "../" + Eval("Photo") %>' class="img-circle" width="100px" style="border: 2px solid #00a7ff;" />--%>
                                </div>
                                <div class="col-sm-7 col-lg-8 col-md-8 col-xs-7">
                                    <h3>
                                        <asp:Literal ID="litStudentName" runat="server"></asp:Literal></h3>
                                    <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-lg-12 col-md-12 col-xs-12 well" style="background: white;">
                            <h3>Parent Message</h3>
                            <p>
                                <asp:Literal ID="litParentMessage" runat="server"></asp:Literal>
                            </p>
                            <asp:Panel ID="PanelAfterSchool" runat="server">
                                <div class="col-sm-12 col-lg-2 col-md-2 col-xs-12">

                                    <label class="checkbox_style">
                                        Mark as Absent
												 
                                    <asp:RadioButton ID="chbMarkAbsent" runat="server" GroupName="Absent" AutoPostBack="true" OnCheckedChanged="chbMarkAbsent_CheckedChanged" />
                                        <span class="checkmark"></span>

                                    </label>
                                </div>
                                <div class="col-sm-12 col-md-2 col-xs-12">
                                    <label class="checkbox_style">
                                        Send to Office
												 
                                     <asp:RadioButton ID="chbSendDriver" runat="server" GroupName="Absent" AutoPostBack="true" OnCheckedChanged="chbSendDriver_CheckedChanged" />
                                        <span class="checkmark"></span>
                                    </label>
                                </div>
                                <div class="col-sm-12 col-lg-3 col-md-3 col-xs-12">
                                    <label class="checkbox_style">
                                        Send to After-School          
                                                          
                                     <asp:RadioButton ID="chbSendAftSchool" runat="server" GroupName="Absent" AutoPostBack="true" OnCheckedChanged="chbSendAftSchool_CheckedChanged" />
                                        <span class="checkmark"></span>
                                    </label>
                                </div>
                                <div class="col-sm-12 col-lg-2 col-md-2 col-xs-12">
                                    <label class="checkbox_style">
                                        Send to Club
												 
                                     <asp:RadioButton ID="chbSendToClub" runat="server" GroupName="Absent" AutoPostBack="true" OnCheckedChanged="chbSendToClub_CheckedChanged" />
                                        <span class="checkmark"></span>
                                    </label>
                                </div>
                                
                            </asp:Panel>
                            
                            <asp:Panel ID="PanelUnPick" runat="server">
                                <div class="col-sm-12 col-lg-3 col-md-3 col-xs-12">
                                    <asp:Button ID="btnUnpickAleart" runat="server" class="btn btn-danger btn-block" Text="Send Unpick Alert" OnClick="btnUnpickAleart_Click"></asp:Button>
                                </div>
                                <div class="col-sm-12 col-lg-2 col-md-2 col-xs-12">

                                    <asp:LinkButton ID="lnkUnPicked" Style="font-size: 17px !important;" runat="server" OnClick="lnkUnPicked_Click"><i class="fa fa-undo" aria-hidden="true"></i> Revert </asp:LinkButton>
                                    <%--<label class="checkbox_style">
                                       
												 
                                     <asp:RadioButton ID="chbUnpicked" runat="server" GroupName="Absent" AutoPostBack="true" OnCheckedChanged="chbUnpicked_CheckedChanged" />
                                        <span class="checkmark"></span>
                                    </label>--%>
                                </div>
                            </asp:Panel>

                            <%--</ItemTemplate>
                            </asp:ListView>--%>
                        </div>
                        <div class="col-md-12 col-xs-12 well">
                            <h3>Pickers</h3>
                        </div>
                        <asp:Label ID="lblNoPicker" runat="server"></asp:Label>
                        <asp:ListView ID="lstPicker" runat="server" OnItemDataBound="lstPicker_ItemDataBound" OnItemCommand="lstPicker_ItemCommand">
                            <ItemTemplate>
                                <div class="col-sm-12 col-lg-6 col-md-6 col-xs-12">
                                    <div class="class-box feature" style='<%# Eval("BorderColorCode") != "" ? 
                                                                            Eval("BorderColorCode").ToString(): 
                                                                (Eval("PickerType").ToString() == "2") ? 
                                                                        "border:1px solid #2196F3" :  "border:1px solid #cccccc" %>'>
                                        <div class="row">
                                            <div class="col-sm-12 col-lg-3 col-md-3 col-xs-12">
                                                <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("PickerName") %>'>
                                                    <img class="img-circle responsives" src='<%# "../" + Eval("Photo") %>' alt="" /></a>
                                            </div>
                                            <div class="col-sm-12 col-lg-9 col-md-9 col-xs-12" style="padding-left: 0px;">
                                                <h3><%# Eval("PickerName") %>
                                                    <asp:Label ID="lbl" runat="server" ForeColor="#00a7ff" Text='<%# (Eval("strDate").ToString() != Eval("CurrentDate").ToString()) ? "": "(Picker Today)" %>'></asp:Label></h3>
                                                <div class="buttons">
                                                    <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                    <asp:HiddenField ID="HID2" runat="server" Value='<%# Eval("ID") %>' />
                                                    <asp:TextBox ID="txtConfirm" runat="server" placeholder="Enter Pickup Code" class="form-control"></asp:TextBox>
                                                </div>
                                                <asp:Button ID="btnConfirm" runat="server" Text="Confirm Pickup" CommandName="btnConfirm" 
                                                            Enabled='<%# (!Convert.ToBoolean(Eval("Count")) && Eval("strDate").ToString() != Eval("CurrentDate").ToString() && HID.Value == "False") ?  false : !Convert.ToBoolean(Eval("IsRedZone"))  %>' 
                                                    CommandArgument='<%# Eval("ID") %>' Style="margin-top: 10px;" CssClass='<%# (Eval("strDate").ToString() != Eval("CurrentDate").ToString()) ? "btn btn-primary" : "btn btn-primary" %>'></asp:Button>
                                                <asp:Label ID="Label1" runat="server" ForeColor="#cc0000" Text='Image Required' Font-Size="X-Small"
                                                        Visible='<%# (Eval("BorderColorCode") == "") ? false : Eval("BorderColorCode") == "border:1px solid #cc0000" ? true : false %>' ></asp:Label>
                                                <asp:Label ID="Label2" runat="server" ForeColor="#0000FF" Text='<%# (Convert.ToString(Eval("CodeRequiredText"))) %>' Font-Size="X-Small"
                                                         ></asp:Label>
                                                <div class="pull-right">
                                                    <asp:CheckBox ID="chkID" runat="server" Text="ID Checked" />
                                                </div>
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
    </section>
    <br />
    <br />

    <asp:HiddenField ID="HID" runat="server" />
</asp:Content>
