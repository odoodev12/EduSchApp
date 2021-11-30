using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Services
{
    public class SupportService
    {
        public SchoolAppEntities entity;

        public SupportService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }


        #region Support

        public ReturnResponce GetSupportList()
        {
            try
            {
                var responce = entity.ISSupports.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.SupportIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetSupport(int SupportId)
        {
            try
            {
                var responce = entity.ISSupports.Where(p => p.Deleted == true && p.Active == true && p.ID == SupportId).FirstOrDefault();
                return new ReturnResponce(responce, EntityJsonIgnore.SupportIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetSupportList(int SchoolId)
        {
            try
            {
                var responce = entity.ISSupports.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.SupportIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce PostReplay(ReplayMessage model)
        {
            try
            {
                if (model != null)
                {
                    var InsertUpdate = new ISTicketMessage();
                    InsertUpdate.SupportID = model.SupportID;
                    InsertUpdate.SenderID = model.LoggedInUserId;
                    InsertUpdate.Message = model.Message;
                    InsertUpdate.SelectFile = model.SelectFile;
                    InsertUpdate.UserTypeID = model.UserTypeId;
                    entity.ISTicketMessages.Add(InsertUpdate);
                    entity.SaveChanges();
                    return new ReturnResponce(InsertUpdate, new[] { "ISSupport" });
                }
                else
                {
                    return new ReturnResponce("Invalid model data & and Login Detail's");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetOrganizationUsersList(int RoleId)
        {
            try
            {
                var responce = entity.ISOrganisationUsers.Where(p => p.Deleted == true && p.Active == true && p.RoleID == RoleId).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.OrganisationUserIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.ToString());
            }
        }
        public ReturnResponce GetOrganizationUser(int OrganizationUserId)
        {
            try
            {
                var responce = entity.ISOrganisationUsers.Where(p => p.Deleted == true && p.Active == true && p.ID == OrganizationUserId).SingleOrDefault();
                return new ReturnResponce(responce, EntityJsonIgnore.OrganisationUserIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.ToString());
            }
        }

        public ReturnResponce SetSupport(ISSupport model, int LogedInuser)
        {
            try
            {
                if (model != null)
                {
                    var InsertUpdate = entity.ISSupports.Where(p => p.Active == true && p.Deleted == true && p.ID == model.ID).SingleOrDefault();
                    if (InsertUpdate != null)
                    {
                        InsertUpdate.Priority = model.Priority;
                        if (InsertUpdate.SupportOfficerID != model.SupportOfficerID)
                        {
                            InsertUpdate.AssignBy = LogedInuser.ToString();
                            InsertUpdate.AssignDate = DateTime.Now;
                        }
                        InsertUpdate.SupportOfficerID = model.SupportOfficerID;
                        InsertUpdate.StatusID = model.StatusID;
                        entity.SaveChanges();
                        return new ReturnResponce(InsertUpdate, EntityJsonIgnore.SupportIgnore);
                    }
                    else
                    {
                        return new ReturnResponce("Invalid model data & and Login Detail's");
                    }
                }
                else
                {
                    return new ReturnResponce("Invalid model data & and Login Detail's");
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