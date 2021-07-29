<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="GetProduct.aspx.cs" Inherits="SchoolApp.Web.GetProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <header class="inner">
        <!-- Banner -->
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <h1 id="homeHeading"><a href="default.aspx">Home</a> / <span>Get Product</span></h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <div class="container">
  <h2 style="text-align: center;">Get product web page</h2>         
  <table class="table table-bordered">
    <thead>
        <tr>
            <th>Features</th>
            <th>Primary Schools (Include Internal Afterschool Classes and Clubs)</th>
            <th>After Schools Organisations and External School Clubs</th>
            <th>Nursery Schools and Daycare</th>
            <th>Other Organisations</th>
        </tr>
    </thead>
    <tbody>
      <tr>
        <td>Class</td>
        <td style="text-align:center;">✔</td>
        <td style="text-align:center;">✔</td>
        <td style="text-align:center;">✔</td>
        <td style="text-align:center;">✔</td>
      </tr>
      <tr>
        <td>Teacher</td>
        <td style="text-align:center;">✔</td>
       <td style="text-align:center;">✔</td>
       <td style="text-align:center;">✔</td>
       <td style="text-align:center;">✔</td>
      </tr>
      <tr>
        <td>Bulk</td>
        <td style="text-align:center;">✔</td>
        <td style="text-align:center;">✔</td>
        <td style="text-align:center;">✔</td>
        <td style="text-align:center;">✔</td>
      </tr>
      <tr>
        <td>Bulk</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      </tr>
      <tr>
        <td>holiday</td>
        <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      </tr>
      <tr>
        <td>Pickup Report</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      </tr>
      <tr>
        <td>Support</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      <td style="text-align:center;">✔</td>
      </tr>
      <tr>
        <td>Attendance</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
      </tr>
      <tr>
        <td>Send Student to Club</td>
        <td style="text-align:center;">✔</td>
        <td></td>
        <td></td>
        <td></td>
      </tr>
      <tr>
        <td>Send to After</td>
        <td style="text-align:center;">✔</td>
        <td></td>
        <td></td>
        <td></td>
      </tr>
      <tr>
        <td>Send to Office</td>
      <td style="text-align:center;">✔</td>
        <td></td>
        <td></td>
        <td></td>
      </tr>
      <tr>
        <td>price</td>
      </tr>
      <tr>
        <td></td>
        <td style="text-align: center;"> <asp:Button ID="btnGetThisProduct1" runat="Server" OnClick="btnGetThisProduct1_Click" Text="Get this product" CssClass="btn btn btn-primary pull-center" /></td>
        <td style="text-align: center;"> <asp:Button ID="btnGetThisProduct2" runat="Server" OnClick="btnGetThisProduct2_Click" Text="Get this product" CssClass="btn btn btn-primary pull-center" /></td>
        <td style="text-align: center;">  <asp:Button ID="btnGetThisProduct3" runat="Server" OnClick="btnGetThisProduct3_Click" Text="Get this product" CssClass="btn btn btn-primary pull-center" /></td>
        <td style="text-align: center;">  <asp:Button ID="btnGetThisProduct4" runat="Server" OnClick="btnGetThisProduct4_Click" Text="Get this product" CssClass="btn btn btn-primary pull-center" /></td>
      </tr>
    </tbody>
  </table>
</div>
<style>
    th {
    color: red;
}
</style>
<script type="text/javascript">
        function showModal(message) {
            alert(message);
        }
</script>

</asp:Content>
