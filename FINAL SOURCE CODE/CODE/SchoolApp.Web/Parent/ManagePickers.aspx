<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagePickers.aspx.cs" Inherits="SchoolApp.Web.Parent.ManagePickers" %>

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


        .buttons .btn {
            margin-top: 10px;
            border-radius: 1px;
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
                            <asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> Parent</asp:HyperLink>
                            / <span>Manage Pickers</span></h1>

                    </div>
                </div>
            </div>
        </div>
    </header>
    <section>
        <div class="container" id="features">
            <div class="sidebar-section">
                <div class="container">
                    <div class="row-md-3">
                        <div class="col-md-3">
                            
                                <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="StudentAllPicker.aspx" class="btn btn-primary" Text="Manage Existing Pickers" Style="border: none; padding: 10px;margin:inherit"></asp:HyperLink>
                                <asp:Button ID="btnCreatePicker" runat="server" class="btn btn-primary" Width="200px" Style="border: none; padding: 10px; margin:inherit" Text="Create New Picker" OnClick="btnCreatePicker_Click"></asp:Button>
                            
                        </div>                     
                    <div class="col-md-9">
                        <div class="row">
                            <asp:ListView ID="lstPicker" runat="server" OnPagePropertiesChanging="lstPicker_PagePropertiesChanging" OnItemDataBound="lstPicker_ItemDataBound" OnItemCommand="lstPicker_ItemCommand">
                                <GroupTemplate>
                                    <div runat="server" id="itemPlaceholder"></div>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <div class="col-md-6">
                                        <div class="class-box feature">

                                            <div class="row">
                                                <div class="col-md-4 col-xs-4">
                                                    <a class="example-image-link" href='<%# "../" + Eval("Photo") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'>
                                                        <img class="example-image img-circle responsives" src='<%# "../" + Eval("Photo") %>' onclick="sendEmail" alt="" /></a>
                                                    <%--<img src='<%# "../" +  Eval("Photo") %>' class="img-circle" width="100%"/>--%>
                                                </div>
                                                <div class="col-md-8 col-xs-8" style="padding-left: 0px;">
                                                    <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
                                                    <asp:HiddenField ID="HID1" runat="server" Value='<%# Eval("ID") %>' />
                                                    <h3><%# Eval("StudentName") %></h3>
                                                    <div class="col-md-10 col-xs-10 no-padding">
                                                        <asp:DropDownList ID="drpSchool" runat="server" CssClass="form-control" Width="100%" ClientIDMode="Inherit" onchange="ChangeSchool(this)"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2 col-xs-2"><a id="PhoneNumber" href="tel:<%# Eval("SchoolNumber") %>" class="btn btn-primary" style="margin-left: 5px !important; margin-bottom: 5px !important;"><i class="fa fa-phone" aria-hidden="true"></i></a></div>

                                                    <%--<p data-toggle="tooltip" title='<%# Eval("SchoolName") %>' style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><a href="tel:<%# Eval("SchoolNumber") %>" class="btn btn-primary" style="margin-left: 5px !important; margin-bottom: 5px !important;"><i class="fa fa-phone" aria-hidden="true"></i></a>&nbsp;<%# Eval("SchoolName") %></p>--%>
                                                    <div class="col-md-12 col-xs-12 no-padding" style="margin-top: 7px !important;">
                                                        <asp:FileUpload ID="fpUpload" runat="server" CssClass="btn btn-primary" Width="100%" onchange="UploadFileNow()" />
                                                    </div>
                                                    <div class="buttons col-md-12 col-xs-12 no-padding">
                                                        <a href="StudentPicker.aspx?ID=<%# Eval("ID") %>&StudentName=<%# Eval("StudentName")%>&SchoolName=<%# Eval("SchoolName") %>" class="btn btn-primary">View Child Pickers</a>
                                                        
                                                       <%-- <asp:Button ID="btnViewPicker" runat="server" CommandArgument='<%# Eval("ID") %>' Text="View Child Pickers"
                                                            CommandName="btnViewPicker" class="btn btn-primary" Style="margin-top: 10px;"  />--%>

                                                    </div>
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
        </div>
        </div>
        <br />
        <br />
        <br />
    </section>
    <script type="text/javascript">
        function ChangeSchool(object) {
            var obj = $("#" + object.id);
            var telWithScoolId = object.value.split('##');
            obj.parent().next().children()[0].href = "tel:" + telWithScoolId[1];
            //obj.parent().prevObject[0].selectedOptions[0].innerText
            var link = obj.parent().next().next().next().children()[0].href.split('&');
            obj.parent().next().next().next().children()[0].href = link[0] + '&' +link[1] + "&SchoolName=" + obj.parent().prevObject[0].selectedOptions[0].innerText;
        }
        function UploadFileNow() {
            var value = $("#fpUpload").val();
            if (value !== '') {
                $("#form1").submit();
            }
        }
        function someFunction(selectObject) {
            //debugger;
            //var obj = document.getElementsByClassName("classs");
            //setAttribute("href", "tel:" + selectObject); 
        }
    </script>
</asp:Content>
