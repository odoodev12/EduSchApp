<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Faq.aspx.cs" Inherits="SchoolApp.Web.Faq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div class="container ">
            <div class="row">
                <!--Faq style 1-->
                <div class="col-md-12">
                    <h2 class="para-heading">Frequently Asked Questions</h2>
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapse1" aria-expanded="true">Question 1</a> </h4>
                            </div>
                            <div id="collapse1" class="panel-collapse collapse in">
                                <div class="panel-body">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur lobortis, lorem velefficitur interdum, massa lacus hendrerit mi.Duis feugiat metus quis nunc rutrum, sit amet sagittis eros varius. Proin nulla dolor, porta id vestibulum nec, tempus id purus. In viverra commodo tellus, vitae iaculis dui rutrum eu. Cum sociis natoque penatibus et magnis dis parturient montes.</div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Question 2</a> </h4>
                            </div>
                            <div id="collapse2" class="panel-collapse collapse">
                                <div class="panel-body">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur lobortis, lorem velefficitur interdum, massa lacus hendrerit mi.Duis feugiat metus quis nunc rutrum, sit amet sagittis eros varius. Proin nulla dolor, porta id vestibulum nec, tempus id purus. In viverra commodo tellus, vitae iaculis dui rutrum eu. Cum sociis natoque penatibus et magnis dis parturient montes.</div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Question 3</a> </h4>
                            </div>
                            <div id="collapse3" class="panel-collapse collapse">
                                <div class="panel-body">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur lobortis, lorem velefficitur interdum, massa lacus hendrerit mi.Duis feugiat metus quis nunc rutrum, sit amet sagittis eros varius. Proin nulla dolor, porta id vestibulum nec, tempus id purus. In viverra commodo tellus, vitae iaculis dui rutrum eu. Cum sociis natoque penatibus et magnis dis parturient montes.</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
