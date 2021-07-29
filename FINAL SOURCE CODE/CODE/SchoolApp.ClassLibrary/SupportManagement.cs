using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class SupportManagement
    {
        public List<MISSupport> SupportList(int SchoolID, string DateFrom, string DateTo, string Status, string OrderBy, string SortBy)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISSupport> objList = (from item in DB.ISSupports.Where(p => p.SchoolID == SchoolID && p.Deleted == true && p.Active == true).ToList()
                                        select new MISSupport
                                        {
                                            ID = item.ID,
                                            TicketNumber = item.TicketNo,
                                            TicketNo = item.TicketNo,
                                            SDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy"),
                                            STime = item.CreatedDateTime.Value.ToString("hh:mm tt"),
                                            Subject = item.Request,
                                            StatusID = item.StatusID,
                                            Status = item.ISSupportStatu.Name,
                                            CreatedByName = item.CreatedByName,
                                            CreatedDateTime = item.CreatedDateTime
                                        }).ToList();

            if (DateFrom != "")
            {
                string dates = DateFrom;
                string Format = "";
                if (dates.Contains("/"))
                {
                    string[] arrDate = dates.Split('/');
                    Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                }
                else
                {
                    Format = dates;
                }
                DateTime dt1 = Convert.ToDateTime(Format);
                objList = objList.Where(p => p.CreatedDateTime.Value.Date >= dt1.Date).ToList();
            }
            if (DateTo != "")
            {
                string dates = DateTo;
                string Format = "";
                if (dates.Contains("/"))
                {
                    string[] arrDate = dates.Split('/');
                    Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                }
                else
                {
                    Format = dates;
                }
                DateTime dt2 = Convert.ToDateTime(Format);
                objList = objList.Where(p => p.CreatedDateTime.Value.Date <= dt2.Date).ToList();
            }

            if (Status != "0")
            {
                int ID = Convert.ToInt32(Status);
                objList = objList.Where(p => p.StatusID == ID).ToList();
            }

            if (OrderBy == "ASC")
            {
                if (SortBy == "TicketNo")
                {
                    objList = objList.OrderBy(p => p.TicketNo).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else if (SortBy == "Created")
                {
                    objList = objList.OrderBy(p => p.CreatedByName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.CreatedDateTime).ToList();
                }
            }
            else
            {
                if (SortBy == "TicketNo")
                {
                    objList = objList.OrderByDescending(p => p.TicketNo).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else if (SortBy == "Created")
                {
                    objList = objList.OrderByDescending(p => p.CreatedByName).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.CreatedDateTime).ToList();
                }
            }
            return objList;
        }
        public List<MISSupportStatus> SupportStatus()
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<MISSupportStatus> objList = (from item in DB.ISSupportStatus.Where(p => p.Deleted == true && p.Active == true).ToList()
                                              select new MISSupportStatus
                                              {
                                                  ID = item.ID,
                                                  Name = item.Name
                                              }).ToList();

            return objList;
        }
        public MISSupport GetSupport(int ID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISSupport Obj = DB.ISSupports.SingleOrDefault(p => p.ID == ID);
            MISSupport objMSupport = new MISSupport();
            if (Obj != null)
            {
                objMSupport.ID = Obj.ID;
                objMSupport.TicketNo = Obj.TicketNo;
                objMSupport.Request = Obj.Request;
                objMSupport.SDate = Obj.CreatedDateTime.Value.ToString("dd/MM/yyyy");
                objMSupport.STime = Obj.CreatedDateTime.Value.ToString("hh:mm tt");
                objMSupport.Status = Obj.ISSupportStatu.Name;
                objMSupport.StatusID = Obj.StatusID;
                objMSupport.Priority = Obj.Priority;
                objMSupport.SupportOfficerID = Obj.SupportOfficerID;
                objMSupport.CreatedByName = Obj.CreatedByName;
            }
            
            return objMSupport;
        }
        public List<MISLogType> LogTypeList()
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISLogType> objList = (from item in DB.ISLogTypes.Where(p => p.Deleted == true && p.Active == true).ToList()
                                              select new MISLogType
                                              {
                                                  ID = item.ID,
                                                  LogTypeName = item.LogTypeName
                                              }).ToList();
            return objList;
        }
    }
}
