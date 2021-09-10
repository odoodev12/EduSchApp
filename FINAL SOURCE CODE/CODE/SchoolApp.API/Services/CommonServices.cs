using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Services
{
    public class CommonServices
    {
        public SchoolAppEntities entity;

        public CommonServices()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        #region
        public object AdminLogin(string username, string password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    var EncPassword = EncryptionHelper.Encrypt(password);
                    var responce = entity.ISAdminLogins.Where(p => p.Email == username && p.Pass == EncPassword && p.Active == true && p.Deleted == true)
                        .Select(s => new AdminDetails
                        {
                            Email = s.Email,
                            Deleted = s.Deleted,
                            ISActivated = s.ISActivated,
                            Active = s.Active,
                            FullName = s.FullName,
                            MemorableQueAnswer = s.MemorableQueAnswer
                        }).FirstOrDefault();

                    if (responce != null && !string.IsNullOrWhiteSpace(responce.MemorableQueAnswer))
                    {
                        responce.MemorableQueAnswer = EncryptionHelper.Decrypt(responce.MemorableQueAnswer);
                    }

                    return responce != null ? new ReturnResponce(responce, new[] { "" }) : new ReturnResponce("Invalid Username & Password");
                }
                else
                {
                    return new ReturnResponce("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        public object TeacherLogin(string username, string password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    var EncPassword = EncryptionHelper.Encrypt(password);
                    var responce = entity.ISTeachers.Where(p => p.Email == username && p.Password == EncPassword && p.Active == true && p.Deleted == true && (p.ISActivated == null || !p.ISActivated.Value) != null)
                        .Select(s => new TeacherDetails
                        {
                            Email = s.Email,
                            Deleted = s.Deleted,
                            ISActivated = s.ISActivated,
                            Active = s.Active,
                            FullName = s.Name,
                            MemorableQueAnswer = s.MemorableQueAnswer,
                            ID = s.ID,
                            Name = s.Name,
                            TeacherNo = s.TeacherNo,
                            Title = s.Title,
                            Photo = s.Photo,
                            PhoneNo = s.PhoneNo,
                            TeacherEndDate = s.EndDate != null ? s.EndDate.Value.ToString() : "",
                            SchoolID = s.SchoolID,
                            RoleID = s.RoleID,
                            ClassID = s.ClassID,
                            ClassName = (s.ClassID != null && s.ClassID != 0) ? entity.ISClasses.FirstOrDefault(p => p.ID == s.ClassID).Name : "",
                            RoleName = s.ISUserRole.RoleName,
                            Role = s.Role,
                            RoleType = s.Role == 1 ? "Teaching Role" : "Non Teaching Role",
                            SchoolType = s.ISSchool.TypeID,
                            //IsActivationID = s.IsActivationID,
                        }).FirstOrDefault();

                    if (responce != null && !string.IsNullOrWhiteSpace(responce.MemorableQueAnswer))
                    {
                        responce.MemorableQueAnswer = EncryptionHelper.Decrypt(responce.MemorableQueAnswer);
                    }

                    return responce != null ? new ReturnResponce(responce, new[] { "" }) : new ReturnResponce("Invalid Username & Password");
                }
                else
                {
                    return new ReturnResponce("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        public object SchoolLogin(string username, string password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    var EncPassword = EncryptionHelper.Encrypt(password);
                    var responce = entity.ISSchools.Where(p => p.AdminEmail == username && p.Password == EncPassword && p.Active == true && p.Deleted == true && p.AccountStatusID == 2 && (p.ISActivated == null || !p.ISActivated.Value) != null)
                        .Select(s => new SchoolDetails
                        {
                            Email = s.AdminEmail,
                            Deleted = s.Deleted,
                            ISActivated = s.ISActivated,
                            Active = s.Active,
                            FullName = s.Name,
                            MemorableQueAnswer = s.MemorableQueAnswer,
                            ID = s.ID,
                            CustomerNumber = s.CustomerNumber,
                            Name = s.Name,
                            Number = s.Number,
                            TypeID = s.TypeID,
                            SchoolType = s.ISSchoolType.Name,
                            Address1 = s.Address1,
                            Address2 = s.Address2,
                            Town = s.Town,
                            CountryID = s.CountryID,
                            CountryName = s.CountryID != null ? entity.ISCountries.FirstOrDefault(p => p.ID == s.CountryID).Name : "",
                            Logo = s.Logo,
                            AdminFirstName = s.AdminFirstName,
                            AdminLastName = s.AdminLastName,
                            AdminEmail = s.AdminEmail,
                            Password = s.Password,
                            PhoneNumber = s.PhoneNumber,
                            Website = s.Website,
                            SupervisorFirstname = s.SupervisorFirstname,
                            SupervisorLastname = s.SupervisorLastname,
                            SupervisorEmail = s.SupervisorEmail,
                            OpningTime = s.OpningTime,
                            OpeningTimeStr = s.OpningTime.Value.ToString("hh:mm"),
                            ClosingTime = s.ClosingTime,
                            ClosingTimeStr = s.ClosingTime.Value.ToString("hh:mm"),
                            LateMinAfterClosing = s.LateMinAfterClosing,
                            ChargeMinutesAfterClosing = s.ChargeMinutesAfterClosing,
                            ReportableMinutesAfterClosing = s.ReportableMinutesAfterClosing,
                            SetupTrainingStatus = s.SetupTrainingStatus,
                            SetupTrainingDate = s.SetupTrainingDate,
                            ActivationDate = s.ActivationDate,
                            SchoolEndDate = s.SchoolEndDate,
                            isAttendanceModule = s.isAttendanceModule,
                            isNotificationPickup = s.isNotificationPickup,
                            IsNotificationPickupStr = s.isNotificationPickup == true ? "Yes" : "No",
                            NotificationAttendance = s.NotificationAttendance,
                            NotificationAttendenceStr = s.NotificationAttendance == true ? "Yes" : "No",
                            AttendanceModule = s.AttendanceModule,
                            PostCode = s.PostCode,
                            BillingAddress = s.BillingAddress,
                            BillingAddress2 = s.BillingAddress2,
                            BillingPostCode = s.BillingPostCode,
                            BillingCountryID = s.BillingCountryID,
                            BillingCountryName = s.BillingCountryID != null ? entity.ISCountries.SingleOrDefault(p => p.ID == s.BillingCountryID).Name : "",
                            BillingTown = s.BillingTown,
                            Classfile = s.Classfile,
                            Teacherfile = s.Teacherfile,
                            Studentfile = s.Studentfile,
                            Reportable = s.Reportable,
                            PaymentSystems = s.PaymentSystems,
                            CustSigned = s.CustSigned,
                            AccountStatusID = s.AccountStatusID,
                            SchoolStatus = s.Active == true ? "Active" : "InActive",
                            CreatedBy = s.CreatedBy,
                            CreatedDateTime = s.CreatedDateTime,
                            ModifyBy = s.ModifyBy,
                            ModifyDateTime = s.ModifyDateTime,
                            DeletedBy = s.DeletedBy,
                            DeletedDateTime = s.DeletedDateTime,
                            IsActivationID = s.IsActivationID,
                          


                        }).FirstOrDefault();

                    if (responce != null && !string.IsNullOrWhiteSpace(responce.MemorableQueAnswer))
                    {
                        responce.MemorableQueAnswer = EncryptionHelper.Decrypt(responce.MemorableQueAnswer);
                    }

                    return responce != null ? new ReturnResponce(responce, new[] { "" }) : new ReturnResponce("Invalid Username & Password");
                }
                else
                {
                    return new ReturnResponce("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        public object ParentLogin(string username, string password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    var EncPassword = EncryptionHelper.Encrypt(password);
                    var responce = entity.ISStudents.Where(p => (p.ParantEmail1.ToLower() == username.ToLower() && p.ParantPassword1 == EncPassword)
                || (p.ParantEmail2.ToLower() == username.ToLower() && p.ParantPassword2 == EncPassword) && p.Active == true && p.Deleted == true)
                        .Select(s => new StudentParentDetails
                        {
                            Deleted = s.Deleted,
                            Active = s.Active,
                            StudentName = s.StudentName,
                            ID = s.ID,
                            StudentNo = s.StudentNo,
                            Photo = s.Photo,
                            ClassID = s.ClassID,
                            ParentEmail = s.ParantEmail1 == username ? s.ParantEmail1 : s.ParantEmail2,
                            ParentName = s.ParantEmail1 == username ? s.ParantName1 : s.ParantName2,
                            Password = s.ParantEmail1 == username ? EncryptionHelper.Decrypt(s.ParantPassword1) : EncryptionHelper.Decrypt(s.ParantPassword2),
                            ParentPhone = s.ParantEmail1 == username ? s.ParantPhone1 : s.ParantPhone2,
                            ParentRelation = s.ParantEmail1 == username ? s.ParantRelation1 : s.ParantRelation2,
                            SchoolID = s.SchoolID,
                        }).FirstOrDefault();


                    return responce != null ? new ReturnResponce(responce, new[] { "" }) : new ReturnResponce("Invalid Username & Password");
                }
                else
                {
                    return new ReturnResponce("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }


        #endregion
    }
}