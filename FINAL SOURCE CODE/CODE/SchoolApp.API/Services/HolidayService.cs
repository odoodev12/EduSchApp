using Newtonsoft.Json;
using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Services
{
    public class HolidayService
    {
        public SchoolAppEntities entity;
        public HolidayService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        #region Holiday
        public ReturnResponce GetHoliday(int HoliDayId)
        {
            try
            {
                var responce = entity.ISHolidays.Where(p => p.Deleted == true && p.ID == HoliDayId).ToList();
                return new ReturnResponce(responce, new[] { "ISSchool" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetHolidayList(int SchoolId)
        {
            try
            {
                var responce = entity.ISHolidays.Where(p => p.Deleted == true && p.SchoolID == SchoolId).ToList();
                return new ReturnResponce(responce, new[] { "ISSchool" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }


        public ReturnResponce GetHolidayFilterList(int SchoolId, string Name, DateTime? Datefrom = null, DateTime? Dateto = null, int? Status = null)
        {
            try
            {
                List<MISHoliday> objList = new List<MISHoliday>();
                objList = (from Obj in entity.ISHolidays.Where(p => p.SchoolID == SchoolId && p.Deleted == true).ToList()
                           select new MISHoliday
                           {
                               ID = Obj.ID,
                               Name = Obj.Name,
                               FromDate = Obj.DateFrom.Value.ToString("dd/MM/yyyy"),
                               ToDate = Obj.DateTo.Value.ToString("dd/MM/yyyy"),
                               DateFrom = Obj.DateFrom,
                               DateTo = Obj.DateTo,
                               Active = Obj.Active,
                               ActiveStatus = Obj.Active == true ? "Active" : "InActive"
                           }).ToList();
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    objList = objList.Where(p => p.Name.Trim().ToLower().Contains(Name.Trim().ToLower())).ToList();
                }
                if (Datefrom != null)
                {
                    objList = objList.Where(p => p.DateFrom >= Datefrom).ToList();
                }
                if (Dateto != null)
                {
                    objList = objList.Where(p => p.DateTo <= Dateto).ToList();
                }

                if (Status != null)
                {
                    if (Status == 1)
                    {
                        objList = objList.Where(p => p.Active == true).ToList();
                    }
                    else
                    {
                        objList = objList.Where(p => p.Active == false).ToList();
                    }
                }

                var responce = objList.OrderByDescending(p => p.DateFrom).ToList();

                return new ReturnResponce(responce, new[] { "ISSchool" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce AddHoliday(HolidayAdd model)
        {
            try
            {
                ISHoliday insertUpdate = new ISHoliday();
                insertUpdate.Name = model.Name;
                insertUpdate.SchoolID = model.SchoolID;
                insertUpdate.Active = model.Active;
                insertUpdate.CreatedBy = model.LoginUserId;
                insertUpdate.CreatedDateTime = DateTime.Now;
                insertUpdate.DateFrom = model.DateFrom;
                insertUpdate.DateTo = model.DateTo;
                insertUpdate.Deleted = true;
                entity.ISHolidays.Add(insertUpdate);

                entity.SaveChanges();
                return new ReturnResponce(insertUpdate, new[] { "ISSchool" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce UpdateHoliday(HolidayEdit model)
        {
            try
            {
                ISHoliday insertUpdate = new ISHoliday();

                if (model.ID > 0)
                {
                    insertUpdate = entity.ISHolidays.Where(w => w.ID == model.ID).FirstOrDefault();
                }

                if (insertUpdate != null && insertUpdate.ID > 0)
                {
                    insertUpdate.Name = model.Name;
                    insertUpdate.DateFrom = model.DateFrom;
                    insertUpdate.DateTo = model.DateTo;
                    insertUpdate.Active = model.Active;
                    insertUpdate.ModifyDateTime = DateTime.Now;
                    insertUpdate.ModifyBy = model.LoginUserId;
                    insertUpdate.Deleted = true;

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, new[] { "ISSchool" });
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
        public ReturnResponce DeleteHoliday(int HolidayId, int LoginUserId)
        {
            try
            {
                ISHoliday insertUpdate = entity.ISHolidays.Where(w => w.ID == HolidayId).FirstOrDefault();

                if (insertUpdate != null)
                {
                    insertUpdate.Active = false;
                    insertUpdate.Deleted = false;
                    insertUpdate.DeletedDateTime = DateTime.UtcNow;
                    insertUpdate.DeletedBy = LoginUserId;

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, new[] { "ISSchool" });
                }
                else
                {
                    ///// Error Responce that invalid data here
                    return new ReturnResponce("Invalid HolidayId or LoginUserId, Please try with valid data.");
                }
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