using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class ProfileManagement
    {
        public MISSchool GetSchool(int ID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISSchool item = DB.ISSchools.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
            if (item != null)
            {
                return new MISSchool
                {
                    ID = item.ID,
                    CustomerNumber = item.CustomerNumber,
                    Name = item.Name,
                    Number = item.Number,
                    TypeID = item.TypeID,
                    Address1 = item.Address1,
                    Address2 = item.Address2,
                    Town = item.Town,
                    CountryID = item.CountryID,
                    Logo = item.Logo,
                    AdminFirstName = item.AdminFirstName,
                    AdminLastName = item.AdminLastName,
                    AdminEmail = item.AdminEmail,
                    Password = EncryptionHelper.Decrypt(item.Password),
                    PhoneNumber = item.PhoneNumber,
                    Website = item.Website,
                    SupervisorFirstname = item.SupervisorFirstname,
                    SupervisorLastname = item.SupervisorLastname,
                    SupervisorEmail = item.SupervisorEmail,
                    OpningTime = item.OpningTime,
                    OpeningTimeStr = item.OpningTime.Value.ToString("hh:mm"),
                    ClosingTime = item.ClosingTime,
                    ClosingTimeStr = item.ClosingTime.Value.ToString("hh:mm"),
                    LateMinAfterClosing = item.LateMinAfterClosing,
                    ChargeMinutesAfterClosing = item.ChargeMinutesAfterClosing,
                    ReportableMinutesAfterClosing = item.ReportableMinutesAfterClosing,
                    SetupTrainingStatus = item.SetupTrainingStatus,
                    SetupTrainingDate = item.SetupTrainingDate,
                    ActivationDate = item.ActivationDate,
                    SchoolEndDate = item.SchoolEndDate,
                    isAttendanceModule = item.isAttendanceModule,
                    isNotificationPickup = item.isNotificationPickup,
                    NotificationAttendance = item.NotificationAttendance,
                    AttendanceModule = item.AttendanceModule,
                    PostCode = item.PostCode,
                    BillingAddress = item.BillingAddress,
                    BillingAddress2 = item.BillingAddress2,
                    BillingPostCode = item.BillingPostCode,
                    BillingCountryID = item.BillingCountryID,
                    BillingTown = item.BillingTown,
                    Classfile = item.Classfile,
                    Teacherfile = item.Teacherfile,
                    Studentfile = item.Studentfile,
                    Reportable = item.Reportable,
                    PaymentSystems = item.PaymentSystems,
                    CustSigned = item.CustSigned,
                    AccountStatusID = item.AccountStatusID,
                    Active = item.Active,
                    Deleted = item.Deleted,
                    CreatedBy = item.CreatedBy,
                    CreatedDateTime = item.CreatedDateTime,
                    ModifyBy = item.ModifyBy,
                    ModifyDateTime = item.ModifyDateTime,
                    DeletedBy = item.DeletedBy,
                    DeletedDateTime = item.DeletedDateTime
                };
            }
            else
            {
                return null;
            }
        }
        //public MISTeacher GetTeachers(int ID)
        //{
        //    SchoolAppEntities DB = new SchoolAppEntities();

        //    ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
        //    MISTeacher objMISTeacher = new MISTeacher();
        //    if (Obj != null)
        //    {
        //        objMISTeacher.ID = Obj.ID;
        //        objMISTeacher.SchoolID = Obj.SchoolID;
        //        objMISTeacher.ClassID = Obj.ClassID;
        //        objMISTeacher.RoleID = Obj.RoleID;
        //        objMISTeacher.RoleName = Obj.ISUserRole.RoleName;
        //        objMISTeacher.TeacherNo = Obj.TeacherNo;
        //        objMISTeacher.Title = Obj.Title;
        //        objMISTeacher.Name = Obj.Name;
        //        objMISTeacher.PhoneNo = Obj.PhoneNo;
        //        objMISTeacher.Email = Obj.Email;
        //        objMISTeacher.Password = EncryptionHelper.Decrypt(Obj.Password);
        //        objMISTeacher.TeacherEndDate = Obj.EndDate != null ? Obj.EndDate.Value.ToString("dd/MM/yyyy") : "";
        //        objMISTeacher.Photo = Obj.Photo;
        //        objMISTeacher.ClassName = GetTeacherAssignedClass(Obj.ID);
        //        objMISTeacher.ClassID1 = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == Obj.ID && p.Deleted == true).ToList().OrderBy(p => p.ID).FirstOrDefault().ClassID;
        //        objMISTeacher.ClassID2 = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == Obj.ID && p.Deleted == true).ToList().OrderByDescending(p => p.ID).FirstOrDefault().ClassID;
        //        objMISTeacher.ClassName1 = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == Obj.ID && p.Deleted == true).ToList().OrderBy(p => p.ID).FirstOrDefault().ISClass.Name;
        //        objMISTeacher.ClassName2 = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == Obj.ID && p.Deleted == true).ToList().OrderByDescending(p => p.ID).FirstOrDefault().ISClass.Name;
        //    }
        //    return objMISTeacher;
        //}
        //public string GetTeacherAssignedClass(int TeacherID)
        //{
        //    SchoolAppEntities DB = new SchoolAppEntities();
        //    string Class = "";
        //    string ClassName = "";
        //    var obj = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID).ToList();
        //    if (obj.Count > 0)
        //    {
        //        foreach (var items in obj)
        //        {
        //            Class += items.ISClass.Name + ", ";
        //        }
        //        ClassName = Class.Remove(Class.Length - 2);
        //    }
        //    return ClassName;
        //}
        public MISStudent GetStudent(int ID,string Email)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
            if (Obj != null)
            {
                MISStudent ObjM = new MISStudent
                {
                    ID = Obj.ID,
                    StudentNo = Obj.StudentNo,
                    ClassID = Obj.ClassID,
                    School_ID = Obj.SchoolID,
                    SchoolName = Obj.ISSchool.Name,
                    StudentName = Obj.StudentName,
                    ClassName = Obj.ISClass.Name,
                    Photo = Obj.Photo,
                    DOB = Obj.DOB,
                    ParentName = Obj.ParantEmail1 == Email ? Obj.ParantName1 : Obj.ParantName2,
                    ParentEmail = Obj.ParantEmail1 == Email ? Obj.ParantEmail1 : Obj.ParantEmail2,
                    ParentPassword = Obj.ParantEmail1 == Email ? EncryptionHelper.Decrypt(Obj.ParantPassword1) : EncryptionHelper.Decrypt(Obj.ParantPassword2),
                    ParentPhone = Obj.ParantEmail1 == Email ? Obj.ParantPhone1 : Obj.ParantPhone2,
                    ParentRelation = Obj.ParantEmail1 == Email ? Obj.ParantRelation1 : Obj.ParantRelation2,
                    ParentPhoto = Obj.ParantEmail1 == Email ? Obj.ParantPhoto1 : Obj.ParantPhoto2,
                    Active = Obj.Active,
                    Deleted = Obj.Deleted,
                };
                return ObjM;
            }
            else
            {
                return null;
            }

        }
    }
}
