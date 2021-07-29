using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class SchoolManagement
    {
        public MISSchool SchoolLogin(string Email, string Password)
        {
            string password = EncryptionHelper.Encrypt(Password);
            SchoolAppEntities DB = new SchoolAppEntities();
            ISSchool item = DB.ISSchools.SingleOrDefault(p => p.AdminEmail == Email && p.Password == password && p.Deleted == true && p.Active == true && p.AccountStatusID == 2);
            if (item != null)
            {
                return new MISSchool
                {
                    ID = item.ID,
                    CustomerNumber = item.CustomerNumber,
                    Name = item.Name,
                    Number = item.Number,
                    TypeID = item.TypeID,
                    SchoolType = item.ISSchoolType.Name,
                    Address1 = item.Address1,
                    Address2 = item.Address2,
                    Town = item.Town,
                    CountryID = item.CountryID,
                    CountryName = item.CountryID != null ? DB.ISCountries.SingleOrDefault(p => p.ID == item.CountryID).Name : "",
                    Logo = item.Logo,
                    AdminFirstName = item.AdminFirstName,
                    AdminLastName = item.AdminLastName,
                    AdminEmail = item.AdminEmail,
                    Password = item.Password,
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
                    IsNotificationPickupStr = item.isNotificationPickup == true ? "Yes" : "No",
                    NotificationAttendance = item.NotificationAttendance,
                    NotificationAttendenceStr = item.NotificationAttendance == true ? "Yes" : "No",
                    AttendanceModule = item.AttendanceModule,
                    PostCode = item.PostCode,
                    BillingAddress = item.BillingAddress,
                    BillingAddress2 = item.BillingAddress2,
                    BillingPostCode = item.BillingPostCode,
                    BillingCountryID = item.BillingCountryID,
                    BillingCountryName = item.BillingCountryID != null ? DB.ISCountries.SingleOrDefault(p => p.ID == item.BillingCountryID).Name : "",
                    BillingTown = item.BillingTown,
                    Classfile = item.Classfile,
                    Teacherfile = item.Teacherfile,
                    Studentfile = item.Studentfile,
                    Reportable = item.Reportable,
                    PaymentSystems = item.PaymentSystems,
                    CustSigned = item.CustSigned,
                    AccountStatusID = item.AccountStatusID,
                    Active = item.Active,
                    SchoolStatus = item.Active == true ? "Active" : "InActive",
                    Deleted = item.Deleted,
                    CreatedBy = item.CreatedBy,
                    CreatedDateTime = item.CreatedDateTime,
                    ModifyBy = item.ModifyBy,
                    ModifyDateTime = item.ModifyDateTime,
                    DeletedBy = item.DeletedBy,
                    DeletedDateTime = item.DeletedDateTime,
                    ISActivated = item.ISActivated,
                    IsActivationID = item.IsActivationID,
                    MemorableQueAnswer = item.MemorableQueAnswer
                };
            }
            else
            {
                return null;
            }
        }
        public List<MISSchoolType> SchoolTypeList()
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<MISSchoolType> objList = (from item in DB.ISSchoolTypes.Where(p => p.Deleted == true).ToList()
                                           select new MISSchoolType
                                           {
                                               ID = item.ID,
                                               Name = item.Name,
                                               Active = item.Active,
                                               Deleted = item.Deleted,
                                               CreatedBy = item.CreatedBy,
                                               CreatedDateTime = item.CreatedDateTime,
                                               ModifyBy = item.ModifyBy,
                                               ModifyDateTime = item.ModifyDateTime,
                                               DeletedBy = item.DeletedBy,
                                               DeletedDateTime = item.DeletedDateTime,
                                           }).ToList();

            return objList;
        }
        public List<MISSchool> SchoolList()
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<MISSchool> objList = (from item in DB.ISSchools.Where(p => p.Deleted == true).ToList()
                                       select new MISSchool
                                       {
                                           ID = item.ID,
                                           Name = item.Name,
                                           AdminEmail = item.AdminEmail,
                                           TypeID = item.TypeID,
                                           Active = item.Active,
                                           Deleted = item.Deleted,
                                           CreatedBy = item.CreatedBy,
                                           CreatedDateTime = item.CreatedDateTime,
                                           ModifyBy = item.ModifyBy,
                                           ModifyDateTime = item.ModifyDateTime,
                                           DeletedBy = item.DeletedBy,
                                           DeletedDateTime = item.DeletedDateTime,
                                       }).ToList();

            return objList;
        }
    }
}
