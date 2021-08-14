using Newtonsoft.Json;
using SchoolApp.API.Models;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Services
{
    public class AdminService
    {
        public SchoolAppEntities entity;

        public AdminService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        #region
        public ReturnResponce AdminLogin(string username, string password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    var EncPassword = EncryptionHelper.Encrypt(password);
                    var responce = entity.ISAdminLogins.Where(p => p.Email == username && p.Pass == EncPassword && p.Active == true && p.Deleted == true).ToList();

                    return responce.Count > 0 ? new ReturnResponce(responce, new[] { "" }) : new ReturnResponce("Invalid Username & Password");
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

        #region School List

        public ReturnResponce GetSchoolTypes()
        {
            try
            {
                var responce = entity.ISSchoolTypes.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, new[] { "ISSchools" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetSchool(int SchoolId)
        {
            try
            {
                var responce = entity.ISSchools.Where(p => p.Deleted == true && p.Active == true && p.ID == SchoolId).FirstOrDefault();
                return new ReturnResponce(responce, EntityJsonIgnore.SchoolIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce SetSchool(School school)
        {
            try
            {
                ISSchool insertUpdate = school;

                if (school.ID > 0)
                {
                    insertUpdate = entity.ISSchools.Where(w => w.ID == school.ID).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    if (insertUpdate.ID == 0) //// Insert
                    {
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        entity.ISSchools.Add(insertUpdate);
                    }
                    else if (insertUpdate.ID > 0) //// Update
                    {
                        insertUpdate.Name = school.Name;
                        insertUpdate.Website = school.Website;
                        insertUpdate.Logo = school.Logo;
                        insertUpdate.AdminFirstName = school.AdminFirstName;
                        insertUpdate.AdminLastName = school.AdminLastName;
                        insertUpdate.SupervisorFirstname = school.SupervisorFirstname;
                        insertUpdate.SupervisorLastname = school.SupervisorLastname;
                        insertUpdate.SupervisorEmail = school.SupervisorEmail;
                        insertUpdate.OpningTime = school.OpningTime;
                        insertUpdate.ClosingTime = school.ClosingTime;
                        insertUpdate.LateMinAfterClosing = school.LateMinAfterClosing;
                        insertUpdate.ChargeMinutesAfterClosing = school.ChargeMinutesAfterClosing;
                        insertUpdate.PhoneNumber = school.PhoneNumber;
                        insertUpdate.TypeID = school.TypeID;
                        insertUpdate.ChargeMinutesAfterClosing = school.ChargeMinutesAfterClosing;
                        insertUpdate.Address1 = school.Address1;
                        insertUpdate.Address2 = school.Address2;
                        insertUpdate.Town = school.Town;
                        insertUpdate.CountryID = school.CountryID;
                        insertUpdate.PostCode = school.PostCode;

                        insertUpdate.BillingAddress = school.BillingAddress;
                        insertUpdate.BillingAddress2 = school.BillingAddress2;
                        insertUpdate.BillingPostCode = school.BillingPostCode;
                        insertUpdate.BillingTown = school.BillingTown;

                        insertUpdate.ModifyDateTime = school.ModifyDateTime;

                    }

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, new[] { "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISStudents", "ISTeacherClassAssignments", "ISClassType" });
                }
                else
                {
                    ///// Error Responce that invalid data here
                    return new ReturnResponce("Invalid model or data, Please try with valid data.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        #endregion

        #region Class List 

        public ReturnResponce GetClassList()
        {
            try
            {
                var responce = entity.ISClasses.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, new[] { "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISStudents", "ISTeacherClassAssignments", "ISClassType" });


            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetClassList(int SchoolId)
        {
            try
            {
                var responce = entity.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList();
                return new ReturnResponce(responce, new[] { "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISStudents", "ISTeacherClassAssignments", "ISClassType" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetClass(int ClassID)
        {
            try
            {
                var responce = entity.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.ID == ClassID).FirstOrDefault();
                return new ReturnResponce(responce, new[] { "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISStudents", "ISTeacherClassAssignments", "ISClassType" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        #endregion

        #region Teacher List 

        public ReturnResponce GetTeacherList(int SchoolId)
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList();
                return new ReturnResponce(responce, new[] { "ISAttendances", "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISPickups", "ISSchool", "ISUserRole", "ISTeacherClassAssignments" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetTeacherList()
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, new[] { "ISAttendances", "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISPickups", "ISSchool", "ISUserRole", "ISTeacherClassAssignments" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetTeacher(int TeacherId)
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.ID == TeacherId).ToList();
                return new ReturnResponce(responce, new[] { "ISAttendances", "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISPickups", "ISSchool", "ISUserRole", "ISTeacherClassAssignments" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        #endregion
    }
}