using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class APIDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["OP"] == "GetDatas")
            {
                var Data = GetData(Request.QueryString["ID"].ToString());
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(Data);
                Response.End();
            }
            if (Request.QueryString["OP"] == "GetSupportData")
            {
                var Data = GetSupportDatas(Request.QueryString["ID"].ToString());
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(Data);
                Response.End();
            }
            if (Request.QueryString["OP"] == "GetOrgData")
            {
                var Data = GetOrgDatas(Request.QueryString["ID"].ToString());
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(Data);
                Response.End();
            }
            if (Request.QueryString["OP"] == "GetSchoolData")
            {
                var Data = GetSchoolData(Request.QueryString["ID"].ToString());
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(Data);
                Response.End();
            }
        }
        public string GetData(string ID)
        {
            try
            {
                SchoolAppEntities DB = new SchoolAppEntities();
                DB.Configuration.ProxyCreationEnabled = false;
                OperationManagement.ID = Convert.ToInt32(ID);
                ISSchoolInvoice item = DB.ISSchoolInvoices.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                MISSchoolInvoice ObjList = new MISSchoolInvoice();
                if (item != null)
                {
                    ObjList.ID = item.ID;
                    ObjList.InvoiceNumber = item.InvoiceNo;
                    ObjList.strTransectionType = item.TransactionTypeID != null ? DB.ISTrasectionTypes.SingleOrDefault(p => p.ID == item.TransactionTypeID).Name : "";
                    ObjList.DateFrom = item.DateFrom;
                    ObjList.FromDate = item.DateFrom != (DateTime?)null ? item.DateFrom.Value.ToString("dd/MM/yyyy") : "";
                    ObjList.DateTo = item.DateTo;
                    ObjList.ToDate = item.DateTo != (DateTime?)null ? item.DateTo.Value.ToString("dd/MM/yyyy") : "";
                    ObjList.Transaction_Amount = item.TransactionAmount.ToString();
                    ObjList.TransactionAmount = item.TransactionAmount;
                    ObjList.TaxRate = item.TaxRate;
                    ObjList.TaxRates = item.TaxRate != null ? item.TaxRate.ToString() : "N/A";
                    ObjList.Description = item.TransactionDesc;
                    ObjList.strStatus = item.StatusID != null ? DB.ISTrasectionStatus.SingleOrDefault(p => p.ID == item.StatusID).Name : "";
                    ObjList.strCreatedDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy hh:mm tt");
                    ObjList.CreatedBy = item.CreatedBy;
                    ObjList.CreatedByName = item.CreatedByName;
                    ObjList.CreatedDateTime = item.CreatedDateTime.Value.Date;
                    ObjList.StatusID = item.StatusID;
                    ObjList.SchoolID = item.SchoolID;
                    ObjList.SchoolName = item.SchoolID != null ? DB.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).Name : "";
                    ObjList.Active = item.Active;
                    ObjList.StatusUpdateBy = item.StatusUpdateBy;
                    ObjList.StatusUpdateDate = item.StatusUpdateDate;
                    ObjList.StrStatusUpdatedDate = item.StatusUpdateDate != (DateTime?)null ? item.StatusUpdateDate.Value.ToString("dd/MM/yyyy") : "";
                }
                if (item != null)
                {
                    var serializer = new JavaScriptSerializer();
                    return serializer.Serialize(ObjList);
                }
                return null;

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
            return "";
        }
        public string GetSupportDatas(string ID)
        {
            try
            {
                SchoolAppEntities DB = new SchoolAppEntities();
                OperationManagement.ID = Convert.ToInt32(ID);
                ISSupport item = DB.ISSupports.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                MISSupport ObjList = new MISSupport();
                if (item != null)
                {
                    ObjList.ID = item.ID;
                    ObjList.TicketNumber = item.TicketNo;
                    ObjList.TicketNo = item.TicketNo;
                    ObjList.SDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy hh:mm tt");
                    ObjList.STime = item.CreatedDateTime.Value.ToString("hh:mm tt");
                    ObjList.Subject = item.Request;
                    ObjList.StatusID = item.StatusID;
                    ObjList.Status = item.ISSupportStatu.Name;
                    ObjList.CreatedByName = item.CreatedByName;
                    ObjList.CreatedDateTime = item.CreatedDateTime;
                    ObjList.SupportOfficerID = item.SupportOfficerID != null ? item.SupportOfficerID : 0;
                    ObjList.Priorities = item.Priority == 1 ? "Critical" : item.Priority == 2 ? "High" : item.Priority == 3 ? "Medium" : item.Priority == 4 ? "Low" : "";
                    ObjList.SupportOfficer = item.SupportOfficerID != null ? DB.ISOrganisationUsers.SingleOrDefault(p => p.ID == item.SupportOfficerID).FirstName + " " + DB.ISOrganisationUsers.SingleOrDefault(p => p.ID == item.SupportOfficerID).LastName : "";
                    ObjList.AssignBy = item.AssignBy;
                    ObjList.AssignDate = item.AssignDate;
                    ObjList.StrAssignDate = item.AssignDate != (DateTime?)null ? item.AssignDate.Value.ToString("dd/MM/yyyy hh:mm tt") : "";
                }
                if (item != null)
                {
                    var serializer = new JavaScriptSerializer();
                    return serializer.Serialize(ObjList);
                }
                return null;

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
            return "";
        }
        public string GetOrgDatas(string ID)
        {
            try
            {
                SchoolAppEntities DB = new SchoolAppEntities();
                OperationManagement.ID = Convert.ToInt32(ID);
                ISOrganisationUser Obj = DB.ISOrganisationUsers.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                MISOrganisationUser ObjList = new MISOrganisationUser();
                if (Obj != null)
                {
                    ObjList.ID = Obj.ID;
                    ObjList.Photo = Obj.Photo;
                    ObjList.FirstName = Obj.FirstName;
                    ObjList.LastName = Obj.LastName;
                    ObjList.OrgCode = Obj.OrgCode;
                    ObjList.Email = Obj.Email;
                    ObjList.Address1 = Obj.Address1;
                    ObjList.Address2 = Obj.Address2;
                    ObjList.Town = Obj.Town;
                    ObjList.CountryID = Obj.CountryID;
                    ObjList.CountryName = Obj.CountryID != null ? Obj.ISCountry.Name : "";
                    ObjList.RoleName = Obj.ISRole.Name;
                    ObjList.Status = Obj.Active == true ? "Active" : "InActive";
                    ObjList.CreatedDateTime = Obj.CreatedDateTime;
                    ObjList.Active = Obj.Active;
                    ObjList.CreatedByName = Obj.CreatedByName;
                    ObjList.LastUpdatedBy = Obj.LastUpdatedBy;
                    ObjList.ModifyDateTime = Obj.ModifyDateTime;
                    ObjList.UpdatedDate = Obj.ModifyDateTime != (DateTime?)null ? Obj.ModifyDateTime.Value.ToString("dd/MM/yyyy") : "";
                    ObjList.ActivationBy = Obj.ActivationBy;
                    ObjList.ActivationDate = Obj.ActivationDate;
                    ObjList.StrActivationDate = Obj.ActivationDate != (DateTime?)null ? Obj.ActivationDate.Value.ToString("dd/MM/yyyy") : "";
                    ObjList.StrCreatedDate = Obj.CreatedDateTime != (DateTime?)null ? Obj.CreatedDateTime.Value.ToString("dd/MM/yyyy") : "";
                }
                if (Obj != null)
                {
                    var serializer = new JavaScriptSerializer();
                    return serializer.Serialize(ObjList);
                }
                return null;

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
            return "";
        }
        public string GetSchoolData(string ID)
        {
            try
            {
                SchoolAppEntities DB = new SchoolAppEntities();
                OperationManagement.ID = Convert.ToInt32(ID);
                ISSchool item = DB.ISSchools.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                if (item != null)
                {
                    var serializer = new JavaScriptSerializer();
                    return serializer.Serialize(
                    new MISSchool
                    {
                        ID = item.ID,
                        CustomerNumber = item.CustomerNumber,
                        Name = item.Name,
                        Number = item.Number,
                        TypeID = item.TypeID,
                        SchoolType = item.ISSchoolType.Name,
                        Address1 = item.Address1 + " " + item.Address2 + " , " + item.Town + " - " + item.PostCode + " , " + DB.ISCountries.SingleOrDefault(p => p.ID == item.CountryID).Name,
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
                        isAttendanceModule = item.isAttendanceModule,
                        isNotificationPickup = item.isNotificationPickup,
                        IsNotificationPickupStr = item.isNotificationPickup == true ? "Yes" : "No",
                        NotificationAttendance = item.NotificationAttendance,
                        NotificationAttendenceStr = item.NotificationAttendance == true ? "Yes" : "No",
                        AttendanceModule = item.AttendanceModule,
                        PostCode = item.PostCode,
                        AccountStatusID = item.AccountStatusID,
                        StrAccountStatus = item.ISAccountStatu.Name,
                        Active = item.Active,
                        SchoolStatus = item.Active == true ? "Active" : "InActive",
                        Deleted = item.Deleted,
                        CreatedBy = item.CreatedBy,
                        CreatedDateTime = item.CreatedDateTime,
                        CreatedByName = item.CreatedByName,
                        LastUpdatedBy = item.LastUpdatedBy,
                        ActivatedBy = item.ActivatedBy,
                        ActivationDate = item.ActivationDate,
                        StrActivationDate = item.ActivationDate != (DateTime?)null ? item.ActivationDate.Value.ToString("dd/MM/yyyy") : "",
                        StrModifyDate = item.ModifyDateTime != (DateTime?)null ? item.ModifyDateTime.Value.ToString("dd/MM/yyyy") : "",
                        StrCreatedDate = item.CreatedDateTime != (DateTime?)null ? item.CreatedDateTime.Value.ToString("dd/MM/yyyy") : ""
                    });
                }
                else
                {
                    return null;
                }
                //if (Obj != null)
                //{
                //    
                //    return serializer.Serialize(ObjList);
                //}
                //return null;

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
            return "";
        }
    }
}