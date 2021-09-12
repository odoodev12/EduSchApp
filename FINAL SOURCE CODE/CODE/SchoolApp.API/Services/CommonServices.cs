using Microsoft.IdentityModel.Tokens;
using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

                    return responce != null ? new ReturnResponceWithTokem(responce, GetToken(), new[] { "" }) : new ReturnResponceWithTokem("Invalid Username & Password");
                }
                else
                {
                    return new ReturnResponceWithTokem("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponceWithTokem(ex.Message);
            }
        }


        public object TeacherLogin(string username, string password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    var EncPassword = EncryptionHelper.Encrypt(password);
                    var responce = (from Teacher in entity.ISTeachers.Where(p => p.Email == username && p.Password == EncPassword && p.Active == true && p.Deleted == true && (p.ISActivated == null || !p.ISActivated.Value) != null)
                                    select new TeacherDetails()
                                    {
                                        ID = Teacher.ID,
                                        Name = Teacher.Name,
                                        TeacherNo = Teacher.TeacherNo,
                                        Title = Teacher.Title,
                                        Photo = Teacher.Photo,
                                        PhoneNo = Teacher.PhoneNo,
                                        TeacherEndDate = Teacher.EndDate.Value,
                                        SchoolID = Teacher.SchoolID,
                                        SchoolName = Teacher.ISSchool.Name,
                                        RoleID = Teacher.RoleID,
                                        RoleName = Teacher.ISUserRole.RoleName,
                                        ClassID = Teacher.ClassID,
                                        ClassName = (Teacher.ClassID != null && Teacher.ClassID != 0) ? entity.ISClasses.FirstOrDefault(p => p.ID == Teacher.ClassID).Name : "",
                                        Role = Teacher.Role,
                                        UserRoleName = Teacher.ISUserRole.RoleName,
                                        RoleType = Teacher.Role == 1 ? "Teaching Role" : "Non Teaching Role",
                                        SchoolType = Teacher.ISSchool.TypeID,
                                        SchoolTypeName = Teacher.ISSchool.ISSchoolType.Name,
                                        Email = Teacher.Email,
                                        Deleted = Teacher.Deleted,
                                        ISActivated = Teacher.ISActivated,
                                        Active = Teacher.Active,
                                        MemorableQueAnswer = Teacher.MemorableQueAnswer,
                                        //IsActivationID = s.IsActivationID,
                                    }).FirstOrDefault();

                    if (responce != null && !string.IsNullOrWhiteSpace(responce.MemorableQueAnswer))
                    {
                        responce.MemorableQueAnswer = EncryptionHelper.Decrypt(responce.MemorableQueAnswer);
                    }

                    return responce != null ? new ReturnResponceWithTokem(responce, GetToken(), new[] { "" }) : new ReturnResponceWithTokem("Invalid Username & Password");
                }
                else
                {
                    return new ReturnResponceWithTokem("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponceWithTokem(ex.Message);
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
                            AdminEmail = s.AdminEmail,
                            Deleted = s.Deleted,
                            ISActivated = s.ISActivated,
                            Active = s.Active,
                            Name = s.Name,
                            MemorableQueAnswer = s.MemorableQueAnswer,
                            ID = s.ID,
                            CustomerNumber = s.CustomerNumber,
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
                            Password = s.Password,
                            PhoneNumber = s.PhoneNumber,
                            Website = s.Website,
                            SupervisorFirstname = s.SupervisorFirstname,
                            SupervisorLastname = s.SupervisorLastname,
                            SupervisorEmail = s.SupervisorEmail,
                            OpningTime = s.OpningTime,
                            ClosingTime = s.ClosingTime,
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
                            BillingCountryName = s.BillingCountryID != null ? entity.ISCountries.FirstOrDefault(p => p.ID == s.BillingCountryID).Name : "",
                            BillingTown = s.BillingTown,
                            Classfile = s.Classfile,
                            Teacherfile = s.Teacherfile,
                            Studentfile = s.Studentfile,
                            Reportable = s.Reportable,
                            PaymentSystems = s.PaymentSystems,
                            CustSigned = s.CustSigned,
                            AccountStatusID = s.AccountStatusID,
                            AccountStatusName= s.ISAccountStatu.Name,
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

                    return responce != null ? new ReturnResponceWithTokem(responce, GetToken(), new[] { "" }) : new ReturnResponceWithTokem("Invalid Username & Password");
                }
                else
                {
                    return new ReturnResponceWithTokem("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponceWithTokem(ex.Message);
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
                            ClassName= s.ISClass.Name ,
                            ParentEmail = s.ParantEmail1 == username ? s.ParantEmail1 : s.ParantEmail2,
                            ParentName = s.ParantEmail1 == username ? s.ParantName1 : s.ParantName2,
                            //Password = s.ParantEmail1 == username ? EncryptionHelper.Decrypt(s.ParantPassword1) : EncryptionHelper.Decrypt(s.ParantPassword2),
                            ParentPhone = s.ParantEmail1 == username ? s.ParantPhone1 : s.ParantPhone2,
                            ParentRelation = s.ParantEmail1 == username ? s.ParantRelation1 : s.ParantRelation2,
                            SchoolID = s.SchoolID,
                            SchoolName=s.ISSchool.Name
                        }).FirstOrDefault();


                    return responce != null ? new ReturnResponceWithTokem(responce, GetToken(), new[] { "" }) : new ReturnResponceWithTokem("Invalid Username & Password");
                }
                else
                {
                    return new ReturnResponceWithTokem("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponceWithTokem(ex.Message);
            }
        }


        private string GetToken()
        {
            string key = "wwwapithestmanager123";
            var issuer = "http://www.api.thestmanager.com";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "SchoolAPI"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }


        #endregion
    }
}