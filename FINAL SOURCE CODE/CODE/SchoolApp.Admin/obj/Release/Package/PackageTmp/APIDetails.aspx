<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APIDetails.aspx.cs" Inherits="SchoolApp.Admin.APIDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table
        {
            border:solid 1px #000000;
             border-collapse: collapse;
            
        }
        td{
           border:solid 1px #000000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%">
                <tr>
                    <td>URL PARAMETER</td>
                    <td>REQUEST PARAMETER</td>
                    <td>RESPONSE</td>
                </tr>
                <tr>
                    <td>OP=GETTOKEN</td>
                    <td>IMEINO=123456789</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Token Generated Successfully","Data":"bacfa690-ae9f-42c6-aa17-4db08e30519b"}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Parameter IMEINO Required","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>OP=PARENTLOGIN<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>EMAIL=Parent2@gmail.com&PASSWORD=125125</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Logged in Successfully","Data":{"SchoolName":null,"ParentName":"Parent2","ParentPhone":"0123456789","ParentEmail":"Parent2@gmail.com","Password":"125125","ParentRelation":"Uncle","School_ID":1,"ID":1,"StudentNo":null,"ClassID":null,"SchoolID":null,"StudentName":null,"Photo":null,"DOB":null,"ParantName1":null,"ParantEmail1":null,"ParantPassword1":null,"ParantPhone1":null,"ParantRelation1":null,"ParantPhoto1":null,"ParantName2":null,"ParantEmail2":null,"ParantPassword2":null,"ParantPhone2":null,"ParantRelation2":null,"ParantPhoto2":null,"PickupMessageID":null,"PickupMessageTime":null,"PickupMessageDate":null,"PickupMessageLastUpdatedParent":null,"AttendanceEmailFlag":null,"LastAttendanceEmailDate":null,"LastAttendanceEmailTime":null,"LastUnpickAlertSentTime":null,"UnpickAlertDate":null,"LastUnpickAlertSentTeacherID":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISAttendances":[],"ISClass":null,"ISPickers":[],"ISPickers1":[],"ISPickups":[],"ISSchool":null}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Invalid Credential","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>OP=PARENTFORGOTPASSWORD<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>EMAIL=Parent2@gmail.com</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Password Send Successfully in your Email","Data":""}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Email address not found!","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>


            </table>
        </div>
    </form>
</body>
</html>
