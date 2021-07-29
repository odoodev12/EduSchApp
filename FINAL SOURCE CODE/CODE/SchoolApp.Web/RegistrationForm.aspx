<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="RegistrationForm.aspx.cs" Inherits="SchoolApp.Web.RegistrationForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="section-bottom-border login-signup">
        <div class="container">
            <div class="form-group heading col-md-12">
                <h2 class="para-heading">submit details</h2>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lblBillingAddress" runat="server" Text="BillingAddress">Billing Address</asp:Label>
                     <asp:TextBox ID="txtBillingAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
               
                <div class="form-group col-md-6">
                     <asp:Label ID="lblSupervisorName" runat="server" Text="SupervisorName">Supervisor Name</asp:Label>
                     <asp:TextBox ID="txtSupervisorName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lblSupervisorEmail" runat="server" Text="SupervisorName">Supervisor Email</asp:Label>
                     <asp:TextBox ID="txtSupervisorEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lblSchoolNumber" runat="server" Text="SupervisorName">School Number</asp:Label>
                     <asp:TextBox ID="txtSchoolNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lblMobileNumber" runat="server" Text="SupervisorName">Mobile Number</asp:Label>
                     <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lblSchoolAddress" runat="server" Text="SupervisorName">School Address</asp:Label>
                     <asp:TextBox ID="txtSchoolAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lblSchoolWebsite" runat="server" Text="SupervisorName">School Website</asp:Label>
                     <asp:TextBox ID="txtSchoolWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                </div>


                <div class="form-group col-md-6 times">
                    <asp:Label ID="lblOpeningTime" runat="server" Text="SchoolOpeningTime"><img src="assets/three-oclock_1f552.png">School Opening Time</asp:Label>
                    <asp:TextBox ID="txtOpeningTime" runat="server" class="form-control" TextMode="Time" format="HH:mm" />
                </div>
                <div class="form-group col-md-6 times">
                    <asp:Label ID="lblClosingTime" runat="server" Text="SchoolClosingTime"><img src="assets/three-oclock_1f552.png">School Closing Time</asp:Label>
                    <asp:TextBox ID="txtClosingTime" runat="server" class="form-control" TextMode="Time" format="HH:mm" />
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lblLastMinutes" runat="server" Text="SchoolClosingTime">Last Minutes After Closing</asp:Label>
                    <asp:TextBox ID="txtLastMinutes" runat="server" class="form-control" TextMode="Time" format="HH:mm" />
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lblChargeMinutes" runat="server" Text="SchoolClosingTime">Charge Minutes After Close</asp:Label>
                    <asp:TextBox ID="txtChargeMinutes" runat="server" class="form-control" TextMode="Time" format="HH:mm" />
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lblReportMinutes" runat="server" Text="SchoolClosingTime">Report Minutes After Close</asp:Label>
                    <asp:TextBox ID="txtReportMinutes" runat="server" class="form-control" TextMode="Time" format="HH:mm" />
                </div>

                <div class=" col-md-6 form-group">
                    Does School want Parent Notifications After Confirm Attendance ?
                    <asp:CheckBox ID="chbNotificationAfterAttendance" runat="server" />
                </div>
                <div class=" col-md-6 form-group">
                    Does School want Parent Notifications After Confirm PickUp ?
                    <asp:CheckBox ID="chbNotificationAfterPickup" runat="server" />
                </div>
                 <div class="form-group col-md-6">
                        <asp:FileUpload id="fileUploadControl" runat="server" style="display:inline;" />  
                        <asp:Button id="btnFileUpload" Text="Upload File" runat="server" />  
                </div>
                <div class="btn col-md-12">
                    <asp:Button ID="btnSubmit" runat="Server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn btn-primary" />
                </div>

            </div>
        </div>

</section>
<script type="text/javascript">
        function showModal(message) {
            alert(message);
        }
</script>
</asp:Content>
