<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassReport.aspx.cs" Inherits="SchoolApp.Web.Teacher.ClassReport" %>


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

        .buttons .btn {
            margin-top: 10px;
            border-radius: 1px;
        }
        .responsive {
            width: 100px !important;
            max-width: 100px !important;
            height: 100px !important;
            max-height:100px !important;
        }
    </style>
    <script type="text/javascript">
        function AddDropDownList() {
            //Build an array containing Customer records.
            var customers = [{
                teacher_role: 1,
                Name: "Select"
            },
            {
                teacher_role: 2,
                Name: "5"
            },
            {
                teacher_role: 3,
                Name: "6"
            },
            {
                teacher_role: 4,
                Name: "7"
            }
            ];
            //Create a DropDownList element.
            var ddlCustomers = $("<select class='form-control' />");

            //Add the Options to the DropDownList.
            $(customers).each(function () {
                var option = $("<option />");

                //Set Customer Name in Text part.
                option.html(this.Name);

                //Set teacher_role in Value part.
                option.val(this.teacher_role);

                console.log(option);
                //Add the Option element to DropDownList.
                ddlCustomers.append(option);
            });

            //Reference the container DIV.
            var dvContainer = $("#dvContainer")

            //Add the DropDownList to DIV.
            var div = $("<div />");
            div.append(ddlCustomers);

            //Create a Remove Button.
            var btnRemove = $("<i class='fa fa-minus' style='border:1px solid gray; padding:10px; border-radius:50%; text-align:center;'></i>");
            btnRemove.click(function () {
                $(this).parent().remove();
            });

            //Add the Remove Buttton to DIV.
            div.append(btnRemove);

            //Add the DIV to the container DIV.
            dvContainer.append(div);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading"><asp:HyperLink ID="hlinkTeacher" runat="server" NavigateUrl="Home.aspx"><i class="fa fa-home"></i> Teacher</asp:HyperLink> / <span>Class Report</span></h1>
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
                        <div class="col-md-3">
                            <h3 style="margin-top: 0px;">Select Class</h3>
                            <div class="sidebar-box">
                                <asp:DropDownList ID="drpClass" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <hr />
                            <a href="ClassDailyReport.aspx" class="btn btn-primary btn-block">Daily Status Report</a>
                        </div>
                        <div class="col-md-9">
                            <div class="well" id="Averageclass" runat="server">
                                <h4>Class Pickup Average :
                                    <asp:Label  ID="lblPickup" runat="server" ></asp:Label></h4>
                            </div>
                            <div class="row">

                                <asp:ListView ID="ListView1" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <div class="class-box feature">
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-4">
                                                        <%--<img src='<%# "../" + Eval("StudentPic") %>' class="img-circle" width="100px" style="border: 2px solid #00a7ff;" />--%>
                                    <a class="example-image-link" href='<%# "../" + Eval("StudentPic") %>' data-lightbox="example-set" data-title='<%# Eval("StudentName") %>'><img src='<%# "../" + Eval("StudentPic") %>' class="img-circle responsive" style="border: 2px solid #00a7ff;" /></a>

                                                    </div>
                                                    <div class="col-md-8 col-xs-8" style="padding-left: 0px;">

                                                        <h3>
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>

                                                        </h3>
                                                         <p>Pickup Average : <asp:Label ID="lblPickupAvg" runat="server" Text='<%# String.Format("{0:N2}", Eval(" StudentPickUpAverage")) %>'></asp:Label>
                                             </p>
                                                        <div class="buttons">
                                                            <a href="StudentPickupReport.aspx?ID=<%# Eval("StudentID") %>" class="btn btn-primary" style="font-size: 9px;">View Student Pickup Report</a>
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
            </div>
        </div>
    </section>
</asp:Content>

