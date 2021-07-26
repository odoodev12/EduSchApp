<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APIDetails.aspx.cs" Inherits="SchoolApp.Web.APIDetails" %>
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
                <tr style="background-color:darkgray;">
                    <td colspan="3"><b>PARENT API LIST</b></td>
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
                    <td>OP=IMAGEUPLOAD<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>IMAGE TO UPLOAD</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Image Uploaded Successfully","Data":"Upload/user.jpg"}</td>
                            </tr>
                            <tr>
                                <td></td>
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

                <tr>
                    <td>OP=DAILYPICKUPSTATUS<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>DATE=12/06/2018,STUDENTID=1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Daily PickUp Status Found Successfully","Data":{"StudentName":"Student1","Status":null,"StudentPic":null,"PickerName":"sardar patel","Pick_Date":"06/12/2018","Pick_Time":"12:00","ID":13,"StudentID":1,"TeacherID":null,"PickerID":1,"PickTime":null,"PickDate":null,"PickStatus":"Picked","ISPicker":null,"ISStudent":null,"ISTeacher":null}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Data found for Daily PickUp","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=STUDENTLISTBYPARENT<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>EMAIL=Parent1@gmail.com</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Student Found Successfully","Data":{"SchoolName":"sarvoday high school","ParentName":null,"ParentPhone":null,"ParentEmail":null,"Password":null,"ParentRelation":null,"School_ID":null,"ID":1,"StudentNo":null,"ClassID":null,"SchoolID":null,"StudentName":"Student1","Photo":null,"DOB":null,"ParantName1":null,"ParantEmail1":null,"ParantPassword1":null,"ParantPhone1":null,"ParantRelation1":null,"ParantPhoto1":null,"ParantName2":null,"ParantEmail2":null,"ParantPassword2":null,"ParantPhone2":null,"ParantRelation2":null,"ParantPhoto2":null,"PickupMessageID":null,"PickupMessageTime":null,"PickupMessageDate":null,"PickupMessageLastUpdatedParent":null,"AttendanceEmailFlag":null,"LastAttendanceEmailDate":null,"LastAttendanceEmailTime":null,"LastUnpickAlertSentTime":null,"UnpickAlertDate":null,"LastUnpickAlertSentTeacherID":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISClass":null,"ISPickups":[],"ISSchool":null}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Student Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=ALLPICKERLIST<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>PARENTID=1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Picker List Found Successfully","Data":{"PickerName":"sardar patel","ParentName":"Parent1","ID":1,"SchoolID":null,"ParentID":null,"StudentID":null,"Title":"sardar patel","FirstName":null,"LastName":null,"Photo":null,"Email":"sp@gmail.com","Phone":null,"PickupCodeCunter":null,"OneOffPickerFlag":null,"EndDate":null,"ActiveStatus":null,"ActiveStatusLastUpdatedDate":null,"ActiveStatusLastUpdatedParent":null,"ActiveStatusLastUpdatedParentEmail":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISStudent":null,"ISStudent1":null,"ISPickups":[]}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Picker List Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=PICKERLIST<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>PARENTID=1&STUDENTID=1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Picker List Found Successfully","Data":{"PickerName":"sardar patel","ParentName":"Parent1","ID":1,"SchoolID":null,"ParentID":1,"StudentID":1,"Title":"sardar patel","FirstName":null,"LastName":null,"Photo":null,"Email":"sp@gmail.com","Phone":null,"PickupCodeCunter":null,"OneOffPickerFlag":null,"EndDate":null,"ActiveStatus":null,"ActiveStatusLastUpdatedDate":null,"ActiveStatusLastUpdatedParent":null,"ActiveStatusLastUpdatedParentEmail":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISStudent":null,"ISStudent1":null,"ISPickups":[]}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Picker List Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=GENERATEPICKERCODE<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>PICKERID=1&PARENTID=1&STUDENTID=1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Picker Code Generated Successfully","Data":[{"PickerName":null,"ParentName":null,"SchoolName":null,"ID":1,"SchoolID":null,"ParentID":1,"StudentID":1,"Title":null,"FirstName":null,"LastName":null,"Photo":null,"Email":null,"Phone":null,"PickupCodeCunter":"346669","OneOffPickerFlag":null,"EndDate":null,"ActiveStatus":null,"ActiveStatusLastUpdatedDate":null,"ActiveStatusLastUpdatedParent":null,"ActiveStatusLastUpdatedParentEmail":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISStudent":null,"ISSchool":null,"ISStudent1":null,"ISPickups":[]}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Picker Code not Generated","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=DELETEPICKER<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>PICKERID=1&PARENTID=1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Picker Deleted Successfully","Data":null}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Picker not Deleted","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                 <tr>
                    <td>OP=GETPICKER<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>PICKERID=1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Picker Data Found Successfully","Data":{"PickerName":null,"ParentName":null,"SchoolName":null,"ID":1,"SchoolID":null,"ParentID":1,"StudentID":1,"Title":"SardarPatel","FirstName":"Sardar","LastName":"Patel","Photo":"Upload/user.jpg","Email":"sp008@gmail.com","Phone":"1234567890","PickupCodeCunter":null,"OneOffPickerFlag":null,"EndDate":null,"ActiveStatus":null,"ActiveStatusLastUpdatedDate":null,"ActiveStatusLastUpdatedParent":null,"ActiveStatusLastUpdatedParentEmail":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISStudent":null,"ISSchool":null,"ISStudent1":null,"ISPickups":[]}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Picker Data Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=ADDUPDATEPICKER<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>PICKERID=56&STUDENTID=1&SCHOOLID=1&PARENTID=1&TITLE=Mr.&FIRSTNAME=Pick1&LASTNAME=Pick2&PHOTOURL=Upload/user.jpg&EMAIL=Pick1@gmail.com&PHONE=0123456789&ONEOFFPICKER=TRUE</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Picker Updated Successfully","Data":""}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"S","Message":"Picker Added Successfully","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="background-color:darkgray;">
                    <td colspan="3"><b>TEACHER API LIST</b></td>
                </tr>
                <tr>
                    <td>OP=TEACHERLOGIN<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>EMAIL=teacher@gmail.com&PASSWORD=125125</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Logged in Successfully","Data":{"TeacherEndDate":"","ClassName":"Class-7","RoleName":"Admin","ID":4,"SchoolID":1,"ClassID":1,"RoleID":1,"TeacherNo":"1","Title":null,"Name":"Mr. Kumar","PhoneNo":"9876543210","Email":"teacher@gmail.com","Password":null,"EndDate":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISClass":null,"ISRole":null,"ISSchool":null}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Invalid Credential","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=TEACHERFORGOTPASSWORD<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>EMAIL=teacher@gmail.com</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Password Send Successfully in your Email","Data":""}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Email sent failed","Data":""}  /  {"Status":"F","Message":"Email address not found!","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=PICKUPLIST<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>TEACHERID=4&CLASSID=1&CREATEDATE=12/06/2018</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"PickUp List Found Successfully","Data":{"StudentName":"Student1","ClassID":1,"Status":null,"StudentPic":"Upload/user.jpg","PickerName":null,"Pick_Date":null,"Pick_Time":null,"ID":13,"StudentID":null,"TeacherID":4,"PickerID":null,"PickTime":null,"PickDate":null,"PickStatus":"Picked","ISPicker":null,"ISStudent":null,"ISTeacher":null}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No PickUp List Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=DAILYCLASSREPORT<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>TEACHERID=4&CLASSID=6&DATE=12/06/2018</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Class Report Found Successfully","Data":[{"StudentPickUpAverage":1,"PickUpAverage":1,"StudentName":"Student1","PickDateStr":null,"ClassID":6,"Status":null,"StudentPic":"Upload/user.jpg","PickerName":"Sardar Patel","Pick_Date":"12/06/2018","Pick_Time":"12:36","SchoolName":null,"ID":13,"StudentID":1,"TeacherID":4,"PickerID":1,"PickTime":null,"PickDate":"\/Date(1528741800000)\/","PickStatus":"Picked","ISPicker":null,"ISStudent":null,"ISTeacher":null}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Class Report Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=TEACHERCLASSASSIGNMENT<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>TEACHERID=4</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Class Assignment Found Successfully","Data":[{"ClassName":"Class-7","ID":1,"TeacherID":4,"ClassID":1,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISClass":null,"ISTeacher":null},{"ClassName":"Class-8","ID":2,"TeacherID":4,"ClassID":2,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISClass":null,"ISTeacher":null}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Class Assignment Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=DAILYCLASSREPORTFILTER<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>STUDENTID=1&STATUS=Picked&SORTBY=Student&ORDERBY=ASC</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Class Daily Report Found Successfully","Data":[{"StudentPickUpAverage":null,"PickUpAverage":null,"StudentName":"Student1","PickDateStr":null,"ClassID":1,"Status":null,"StudentPic":"Upload/user.jpg","PickerName":"Sardar Patel","Pick_Date":"12/06/2018","Pick_Time":"12:36","SchoolName":null,"ID":13,"StudentID":1,"TeacherID":4,"PickerID":1,"PickTime":null,"PickDate":null,"PickStatus":"Picked","ISPicker":null,"ISStudent":null,"ISTeacher":null}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":""No Class Daily Report Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=STUDENTLISTBYCLASS<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>SCHOOLID=1&CLASSID=3</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Student List Found Successfully","Data":[{"SchoolName":null,"ClassName":null,"ParentName":null,"ParentPhone":null,"ParentEmail":null,"Password":null,"ParentRelation":null,"School_ID":null,"ID":3,"StudentNo":"3","ClassID":3,"SchoolID":null,"StudentName":"Student3","Photo":"Upload/user.jpg","DOB":null,"ParantName1":null,"ParantEmail1":null,"ParantPassword1":null,"ParantPhone1":null,"ParantRelation1":null,"ParantPhoto1":null,"ParantName2":null,"ParantEmail2":null,"ParantPassword2":null,"ParantPhone2":null,"ParantRelation2":null,"ParantPhoto2":null,"PickupMessageID":null,"PickupMessageTime":null,"PickupMessageDate":null,"PickupMessageLastUpdatedParent":null,"AttendanceEmailFlag":null,"LastAttendanceEmailDate":null,"LastAttendanceEmailTime":null,"LastUnpickAlertSentTime":null,"UnpickAlertDate":null,"LastUnpickAlertSentTeacherID":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISAttendances":[],"ISClass":null,"ISPickers":[],"ISPickers1":[],"ISPickups":[],"ISSchool":null},{"SchoolName":null,"ClassName":null,"ParentName":null,"ParentPhone":null,"ParentEmail":null,"Password":null,"ParentRelation":null,"School_ID":null,"ID":4,"StudentNo":"125","ClassID":3,"SchoolID":null,"StudentName":"Student5","Photo":"Upload/user.jpg","DOB":null,"ParantName1":null,"ParantEmail1":null,"ParantPassword1":null,"ParantPhone1":null,"ParantRelation1":null,"ParantPhoto1":null,"ParantName2":null,"ParantEmail2":null,"ParantPassword2":null,"ParantPhone2":null,"ParantRelation2":null,"ParantPhoto2":null,"PickupMessageID":null,"PickupMessageTime":null,"PickupMessageDate":null,"PickupMessageLastUpdatedParent":null,"AttendanceEmailFlag":null,"LastAttendanceEmailDate":null,"LastAttendanceEmailTime":null,"LastUnpickAlertSentTime":null,"UnpickAlertDate":null,"LastUnpickAlertSentTeacherID":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISAttendances":[],"ISClass":null,"ISPickers":[],"ISPickers1":[],"ISPickups":[],"ISSchool":null},{"SchoolName":null,"ClassName":null,"ParentName":null,"ParentPhone":null,"ParentEmail":null,"Password":null,"ParentRelation":null,"School_ID":null,"ID":6,"StudentNo":"203","ClassID":3,"SchoolID":null,"StudentName":"Vimal","Photo":"Upload/user.jpg","DOB":null,"ParantName1":null,"ParantEmail1":null,"ParantPassword1":null,"ParantPhone1":null,"ParantRelation1":null,"ParantPhoto1":null,"ParantName2":null,"ParantEmail2":null,"ParantPassword2":null,"ParantPhone2":null,"ParantRelation2":null,"ParantPhoto2":null,"PickupMessageID":null,"PickupMessageTime":null,"PickupMessageDate":null,"PickupMessageLastUpdatedParent":null,"AttendanceEmailFlag":null,"LastAttendanceEmailDate":null,"LastAttendanceEmailTime":null,"LastUnpickAlertSentTime":null,"UnpickAlertDate":null,"LastUnpickAlertSentTeacherID":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISAttendances":[],"ISClass":null,"ISPickers":[],"ISPickers1":[],"ISPickups":[],"ISSchool":null},{"SchoolName":null,"ClassName":null,"ParentName":null,"ParentPhone":null,"ParentEmail":null,"Password":null,"ParentRelation":null,"School_ID":null,"ID":7,"StudentNo":"122","ClassID":3,"SchoolID":null,"StudentName":"Vimal d","Photo":"Upload/user.jpg","DOB":null,"ParantName1":null,"ParantEmail1":null,"ParantPassword1":null,"ParantPhone1":null,"ParantRelation1":null,"ParantPhoto1":null,"ParantName2":null,"ParantEmail2":null,"ParantPassword2":null,"ParantPhone2":null,"ParantRelation2":null,"ParantPhoto2":null,"PickupMessageID":null,"PickupMessageTime":null,"PickupMessageDate":null,"PickupMessageLastUpdatedParent":null,"AttendanceEmailFlag":null,"LastAttendanceEmailDate":null,"LastAttendanceEmailTime":null,"LastUnpickAlertSentTime":null,"UnpickAlertDate":null,"LastUnpickAlertSentTeacherID":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISAttendances":[],"ISClass":null,"ISPickers":[],"ISPickers1":[],"ISPickups":[],"ISSchool":null}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":""No Student List Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=STUDENTPICKUPREPORT<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>STUDENTID=1&DATEFROM=11/06/2018&DATETO=16/06/2018&PICKERID=1&STATUS=Picked&SORTBY=Date&ORDERBY=ASC</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Student PickUp Report Found Successfully","Data":[{"StudentPickUpAverage":null,"PickUpAverage":null,"StudentName":"Student1","PickDateStr":null,"ClassID":null,"Status":null,"StudentPic":"Upload/user.jpg","PickerName":"Sardar Patel","Pick_Date":"12/06/2018","Pick_Time":"12:36","SchoolName":null,"ID":13,"StudentID":1,"TeacherID":null,"PickerID":1,"PickTime":null,"PickDate":"\/Date(1528741800000)\/","PickStatus":"Picked","ISPicker":null,"ISStudent":null,"ISTeacher":null}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":""No Student PickUp Report Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="background-color:darkgray;">
                    <td colspan="3"><b>SCHOOL API LIST</b></td>
                </tr>
                <tr>
                    <td>OP=SCHOOLLOGIN<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>EMAIL=anilsolanki295@gmail.com&PASSWORD=125125</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Logged in Successfully","Data":{"ID":1,"CustomerNumber":"CUST00001","Name":"sarvoday high school","Number":"1234567","TypeID":1,"Address1":"hjhjhkjgh","Address2":"near temple Sarkhej","Town":"Ahmadabad","CountryID":1,"Logo":"Upload/Chrysanthemum.jpg","AdminFirstName":"solanki","AdminLastName":"anilsinh","AdminEmail":"anilsolanki295@gmail.com","Password":"4bn9GpOHMkE=","PhoneNumber":"987654321212","Website":"aaa","SupervisorFirstname":"samir","SupervisorLastname":"patel","SupervisorEmail":"abc@gmail.com","OpningTime":"\/Date(1514781060000)\/","ClosingTime":"\/Date(1514781060000)\/","LateMinAfterClosing":"aaa","ChargeMinutesAfterClosing":"aaaa","ReportableMinutesAfterClosing":"ssss","SetupTrainingStatus":true,"SetupTrainingDate":"\/Date(1514745000000)\/","ActivationDate":"\/Date(1528482600000)\/","SchoolEndDate":"\/Date(1528482600000)\/","isAttendanceModule":true,"isNotificationPickup":true,"NotificationAttendance":false,"AttendanceModule":"a","PostCode":"363001","BillingAddress":"Thaltej","BillingAddress2":"near temple Sarkhej","BillingPostCode":null,"BillingCountryID":null,"BillingTown":null,"Classfile":"Upload/Desert.jpg","Teacherfile":"Upload/Jellyfish.jpg","Studentfile":"Upload/Koala.jpg","Reportable":null,"PaymentSystems":true,"CustSigned":true,"AccountStatusID":null,"Active":true,"Deleted":true,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":1,"ModifyDateTime":"\/Date(1528723693310)\/","DeletedBy":1,"DeletedDateTime":"\/Date(1528550830757)\/","ISAccountStatu":null,"ISCountry":null,"ISPickers":[],"ISStudents":[],"ISTeachers":[],"ISUserActivities":[],"ISSchool1":null,"ISSchool2":null,"ISSchoolType":null,"ISSchoolInvoices":[],"ISUserRoles":[]}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":""Invalid Credential","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=SCHOOLFORGOTPASSWORD<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>EMAIL=hardik@gatistavamsoftech.net</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Password Send Successfully in your Email","Data":""}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Email sent failed","Data":""}  /  {"Status":"F","Message":"Email address not found!","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=GETADMINROLES<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>SCHOOLID=1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Roles Found","Data":[{"strCreateBy":"solanki anilsinh","strCreatedDate":"18/06/2018","ID":2,"SchoolID":1,"RoleName":"Teacher","ManageCalenderFlag":true,"ManageTeacherFlag":false,"ManageClassFlag":true,"ManageSupportFlag":false,"ManageStudentFlag":true,"ManageViewAccountFlag":false,"Active":true,"Deleted":true,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISSchool":null},{"strCreateBy":"solanki anilsinh","strCreatedDate":"18/06/2018","ID":3,"SchoolID":1,"RoleName":"Superviser","ManageCalenderFlag":true,"ManageTeacherFlag":true,"ManageClassFlag":false,"ManageSupportFlag":true,"ManageStudentFlag":true,"ManageViewAccountFlag":true,"Active":true,"Deleted":true,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISSchool":null},{"strCreateBy":"solanki anilsinh","strCreatedDate":"19/06/2018","ID":9,"SchoolID":1,"RoleName":"gdhjs","ManageCalenderFlag":true,"ManageTeacherFlag":false,"ManageClassFlag":true,"ManageSupportFlag":false,"ManageStudentFlag":false,"ManageViewAccountFlag":false,"Active":true,"Deleted":true,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISSchool":null},{"strCreateBy":"solanki anilsinh","strCreatedDate":"19/06/2018","ID":8,"SchoolID":1,"RoleName":"Accountant","ManageCalenderFlag":false,"ManageTeacherFlag":false,"ManageClassFlag":false,"ManageSupportFlag":false,"ManageStudentFlag":false,"ManageViewAccountFlag":true,"Active":true,"Deleted":true,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISSchool":null}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"no Roles Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=SCHOOLCLASSYEAR<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td></td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Year Found Successfully","Data":["2017","2018","2019","2020","2021","2022","2023","2024","2025","2026"]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Year Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=GETCLASSBYYEAR<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>SCHOOLID=1&YEAR=2019</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Class Found Successfully","Data":[{"ClassType":"Standard","StudentCount":1,"TeacherName":null,"ID":2,"SchoolID":1,"Name":"Class-8","Year":"2019","TypeID":1,"AfterSchoolType":"Internal","ExternalOrganisation":"Organization","EndDate":null,"PickupComplete":null,"Active":true,"Deleted":true,"CreatedBy":1,"CreatedDateTime":null,"ModifyBy":1,"ModifyDateTime":"\/Date(1529477477397)\/","DeletedBy":null,"DeletedDateTime":null,"ISStudents":[],"ISTeacherClassAssignments":[],"ISTeachers":[],"ISClassType":null},{"ClassType":"Standard","StudentCount":4,"TeacherName":null,"ID":3,"SchoolID":1,"Name":"Class-9","Year":"2019","TypeID":1,"AfterSchoolType":"Internal","ExternalOrganisation":"Organization 2","EndDate":null,"PickupComplete":null,"Active":true,"Deleted":true,"CreatedBy":1,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISStudents":[],"ISTeacherClassAssignments":[],"ISTeachers":[],"ISClassType":null},{"ClassType":"Standard","StudentCount":0,"TeacherName":null,"ID":10,"SchoolID":1,"Name":"Class-4","Year":"2019","TypeID":1,"AfterSchoolType":"Internal","ExternalOrganisation":"Organization 2","EndDate":null,"PickupComplete":null,"Active":true,"Deleted":true,"CreatedBy":1,"CreatedDateTime":"\/Date(1529388220547)\/","ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISStudents":[],"ISTeacherClassAssignments":[],"ISTeachers":[],"ISClassType":null}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Class Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=GETCLASSTYPELIST<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td></td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"ClassTypes Found Successfully","Data":[{"ID":1,"Name":"Standard","Active":true,"Deleted":true,"CreatedBy":1,"CreatedDateTime":"\/Date(1525458600000)\/","ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISClasses":[]}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No ClassType Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=GETSCHOOLTYPE<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td></td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"After School Types Found Successfully","Data":[{"ID":1,"AfterSchoolType":"Internal"},{"ID":2,"AfterSchoolType":"External"}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No After School Type Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=CREATECLASS<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>SCHOOLID=1&CLASSNAME=Class212&YEAR=2019&CLASSTYPEID=2&AFTERSCHOOLTYPE=Internal&EXTERNALORGANISATION=Organisation 1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Class Created Successfully","Data":""}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Class not Created","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=DELETEADMINROLES<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>SCHOOLID=1&ID=4</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Role Deleted Successfully","Data":""}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Role not Deleted","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=ADDUPDATEADMINROLES<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>ID=1&SCHOOLID=1&ROLENAME=Test&MANAGECALENDERFLAG=True&MANAGETEACHERFLAG=True&MANAGECLASSFLAG=True&MANAGESUPPORTFLAG=True&MANAGESTUDENTFLAG=True&MANAGEVIEWACCOUNTFLAG=True</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Role Updated Successfully","Data":""}  / {"Status":"S","Message":"Role Added Successfully","Data":""} </td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Role not Updated","Data":""}  /  {"Status":"F","Message":"Role not Added","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=EDITADMINROLES<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>ID=1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Role Edited Successfully","Data":{"strCreateBy":null,"strCreatedDate":null,"ID":4,"SchoolID":1,"RoleName":"New Roles","ManageCalenderFlag":true,"ManageTeacherFlag":true,"ManageClassFlag":false,"ManageSupportFlag":true,"ManageStudentFlag":false,"ManageViewAccountFlag":true,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISSchool":null}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Role not Edited","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=CLASSDETAILS<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>CLASSID=4</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Class Details Found Successfully","Data":{"ClassType":"Standard","StudentCount":1,"TeacherName":"","ID":4,"SchoolID":0,"Name":"Class1","Year":"2017","TypeID":1,"AfterSchoolType":"Internal","ExternalOrganisation":"Organization 1","EndDate":null,"PickupComplete":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISStudents":[],"ISTeacherClassAssignments":[],"ISTeachers":[],"ISClassType":null}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Class Details Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=UPDATECLASS<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>ID=4&SCHOOLID=1&CLASSNAME=Class212&YEAR=2019&CLASSTYPEID=2&AFTERSCHOOLTYPE=Internal&EXTERNALORGANISATION=Organisation 1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Class Updated Successfully","Data":""}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Class not Updated","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=USERACTIVITY<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>SCHOOLID=1&DATEFROM=13/6/2018&DATETO=19/6/2018&ORDERBY=DESC&SORTBY=Date</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"User Activity Found Successfully","Data":[{"SchoolName":"sarvoday high school","StrDate":"19/06/2018 01:45 AM","UpdatedBy":null,"ID":1,"SchoolID":1,"UserName":null,"LogText":"The New Class has been created on","Active":null,"Deleted":null,"CreatedBy":1,"CreatedDateTime":"\/Date(1529397900000)\/","ISSchool":null},{"SchoolName":"sarvoday high school","StrDate":"13/06/2018 01:34 AM","UpdatedBy":null,"ID":2,"SchoolID":1,"UserName":null,"LogText":"Test","Active":null,"Deleted":null,"CreatedBy":1,"CreatedDateTime":"\/Date(1528878840000)\/","ISSchool":null}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"User Activity not Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=PAYMENTLIST<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>SCHOOLID=1&DATEFROM=9/6/2018&DATETO=9/6/2018&ORDERBY=DESC&SORTBY=Date</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Payment List Found Successfully","Data":[{"strTransectionType":"Cash","strStatus":"Pending","InvoiceNumber":"101","FromDate":null,"ToDate":null,"Description":"Test FOr School","Transaction_Amount":"1000","strCreatedDate":"09/06/2018 03:13 PM","ID":1,"SchoolID":null,"InvoiceNo":null,"DateFrom":"\/Date(1527836400000)\/","DateTo":"\/Date(1527836400000)\/","TransactionTypeID":null,"TransactionDesc":null,"TransactionAmount":null,"StatusID":null,"Active":null,"Deleted":null,"CreatedBy":1,"CreatedDateTime":"\/Date(1528527600000)\/","ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISSchool":null,"ISTrasectionStatu":null,"ISTrasectionType":null}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Payment List not Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=TEACHERLIST<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>SCHOOLID=1&YEAR=2018&CLASSID=7&TeacherName=Teacher22&ORDERBY=ASC&SORTBY=IdNo</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Teacher List Found Successfully","Data":[{"TeacherEndDate":"6/29/2018","ClassName":"Class-9","RoleName":"Support Officer","AssignedClass":"Class-9, Class-11","ID":5,"SchoolID":1,"ClassID":3,"RoleID":3,"TeacherNo":"125","Title":"Mr.","Name":"Teacher22","PhoneNo":"1251251251","Email":"teacher2@gmail.com","Password":null,"EndDate":null,"Photo":"Upload/user.jpg","Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISAttendances":[],"ISClass":null,"ISPickups":[],"ISRole":null,"ISSchool":null,"ISTeacherClassAssignments":[]}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Teacher List not Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=GETSTUDENTINFO<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>ID=1</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Student Information Found Successfully","Data":{"SchoolName":null,"ClassName":"CLASS5","ParentName":null,"ParentPhone":null,"ParentEmail":null,"Password":null,"ParentRelation":null,"School_ID":null,"ID":0,"StudentNo":"1","ClassID":6,"SchoolID":null,"StudentName":"Student1","Photo":"Upload/user.jpg","DOB":null,"ParantName1":"Parent1","ParantEmail1":"Parent1@gmail.com","ParantPassword1":null,"ParantPhone1":"1234555555","ParantRelation1":"Father","ParantPhoto1":null,"ParantName2":"Parent2","ParantEmail2":"Parent2a@gmail.com","ParantPassword2":null,"ParantPhone2":"0123456789","ParantRelation2":"Uncle","ParantPhoto2":null,"PickupMessageID":null,"PickupMessageTime":null,"PickupMessageDate":null,"PickupMessageLastUpdatedParent":null,"AttendanceEmailFlag":null,"LastAttendanceEmailDate":null,"LastAttendanceEmailTime":null,"LastUnpickAlertSentTime":null,"UnpickAlertDate":null,"LastUnpickAlertSentTeacherID":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":null,"ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISAttendances":[],"ISClass":null,"ISPickers":[],"ISPickers1":[],"ISPickups":[],"ISSchool":null}}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"No Student Information Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=CREATEUPDATESTUDENT<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>ID=1&CLASSID=6&SCHOOLID=1&STUDENTNAME=Student1&STUDENTNO=1&PHOTOURL=Upload/user.jpg&PPNAME=Parent1&PPEMAIL=Parent1@gmail.com&PPPHONE=1234567890&PPRELATION=Father&SPNAME=Parent2&SPEMAIL=Parent2@gmail.com&SPPHONE=9876543210&SPRELATION=Uncle</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Student Updated Successfully","Data":""}  /  {"Status":"S","Message":"Student Created Successfully","Data":""}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Student not Updated","Data":""}  /  {"Status":"F","Message":"Student not Created","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>OP=SUPPORTLIST<br />
                        TOKEN=fa00fccd-e783-4cf4-be95-f87c377e960f</td>
                    <td>SCHOOLID=1&DATEFROM=13/06/2018&DATETO=25/06/2018&STATUSID=1&ORDERBY=DESC&SORTBY=Date</td>
                    <td>
                        <table>
                            <tr>
                                <td>{"Status":"S","Message":"Support List Found Successfully","Data":[{"TicketNumber":"209846","Subject":"Hello","SDate":"20/06/2018","STime":"06:58 PM","Created":"1","Status":"UnResolved","ID":2,"TicketNo":null,"Request":null,"SchoolID":null,"StatusID":1,"LogTypeID":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":"\/Date(1529546291727)\/","ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISLogType":null,"ISSupportStatu":null,"ISTicketMessages":[]},{"TicketNumber":"630080","Subject":"safsaf","SDate":"20/06/2018","STime":"06:59 PM","Created":"1","Status":"UnResolved","ID":3,"TicketNo":null,"Request":null,"SchoolID":null,"StatusID":1,"LogTypeID":null,"Active":null,"Deleted":null,"CreatedBy":null,"CreatedDateTime":"\/Date(1529546386830)\/","ModifyBy":null,"ModifyDateTime":null,"DeletedBy":null,"DeletedDateTime":null,"ISLogType":null,"ISSupportStatu":null,"ISTicketMessages":[]}]}</td>
                            </tr>
                            <tr>
                                <td>{"Status":"F","Message":"Support List not Found","Data":""}</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
