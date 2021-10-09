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
    public class StudentService
    {
        public SchoolAppEntities entity;

        public StudentService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        public ReturnResponce GetStudentList()
        {
            try
            {
                var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetStudentList(int SchoolId)
        {
            try
            {
                var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetStudentList(int SchoolId, int ClassId)
        {
            try
            {
                var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId && p.ClassID == ClassId).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce AddUpdateStudent(ISStudent model, int AdminId)
        {
            try
            {
                ISStudent insertUpdate = new ISStudent();

                if (model.ID > 0)
                {
                    insertUpdate = entity.ISStudents.Where(w => w.ID == model.ID).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    if (insertUpdate.ID == 0) //// Insert
                    {
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        insertUpdate.CreatedBy = AdminId;
                        entity.ISStudents.Add(insertUpdate);
                    }
                    else if (insertUpdate.ID > 0) //// Update
                    {
                        insertUpdate.StudentName = model.StudentName;
                        insertUpdate.SchoolID = model.SchoolID;
                        insertUpdate.Photo = model.Photo;
                        insertUpdate.DOB = model.DOB;
                        insertUpdate.ParantName1 = model.ParantName1;
                        insertUpdate.ParantName2 = model.ParantName2;
                        insertUpdate.ParantEmail1 = model.ParantEmail1;
                        insertUpdate.ParantEmail2 = model.ParantEmail2;
                        insertUpdate.ParantPassword1 = model.ParantPassword1;
                        insertUpdate.ParantPassword2 = model.ParantPassword2;
                        insertUpdate.ParantPhone1 = model.ParantPhone1;
                        insertUpdate.ParantPhone2 = model.ParantPhone2;
                        insertUpdate.ParantRelation1 = model.ParantRelation1;
                        insertUpdate.ParantRelation2 = model.ParantRelation2;
                        insertUpdate.ParantPhoto1 = model.ParantPhoto1;
                        insertUpdate.ParantPhoto2 = model.ParantPhoto2;
                        insertUpdate.PickupMessageID = model.PickupMessageID;
                        insertUpdate.PickupMessageTime = model.PickupMessageTime;
                        insertUpdate.PickupMessageDate = model.PickupMessageDate;

                        insertUpdate.StartDate = model.StartDate;
                        insertUpdate.EndDate = model.EndDate;

                        insertUpdate.ModifyDateTime = model.ModifyDateTime;
                        insertUpdate.ModifyBy = AdminId;
                    }

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, EntityJsonIgnore.StudentIgnore);
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
    }
}