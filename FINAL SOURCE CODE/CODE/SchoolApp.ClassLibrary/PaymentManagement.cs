using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class PaymentManagement
    {
        public List<MISSchoolInvoice> InvoicesList(int SchoolID, string FromDate, string ToDate, string Status, string OrderBy, String SortBy)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<MISSchoolInvoice> objList = (from item in DB.ISSchoolInvoices.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                              select new MISSchoolInvoice
                                              {
                                                  ID = item.ID,
                                                  InvoiceNumber = item.InvoiceNo,
                                                  strTransectionType = item.ISTrasectionType.Name,
                                                  DateFrom = item.DateFrom,
                                                  DateTo = item.DateTo,
                                                  Transaction_Amount = item.TransactionAmount.ToString(),
                                                  TransactionAmount = item.TransactionAmount,
                                                  Description = item.TransactionDesc,
                                                  TaxRate = item.TaxRate,
                                                  TaxRates = !String.IsNullOrEmpty(item.TaxRate.ToString()) ? item.TaxRate.ToString() : "",
                                                  strStatus = item.ISTrasectionStatu.Name,
                                                  strCreatedDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy hh:mm tt"),
                                                  CreatedBy = item.CreatedBy,
                                                  CreatedDateTime = item.CreatedDateTime.Value.Date,
                                                  StatusID = item.StatusID
                                              }).ToList();
            if (FromDate != "")
            {
                string dates = FromDate;
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
            if (ToDate != "")
            {
                string dates = ToDate;
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
                int StatusID = Convert.ToInt32(Status);
                objList = objList.Where(p => p.StatusID == StatusID).ToList();
            }
            if (OrderBy == "ASC")
            {
                if (SortBy == "Status")
                {
                    objList = objList.OrderBy(p => p.strStatus).ToList();
                }
                else if (SortBy == "InvoiceNo")
                {
                    objList = objList.OrderBy(p => p.InvoiceNumber).ToList();
                }
                else if (SortBy == "TransactionType")
                {
                    objList = objList.OrderBy(p => p.strTransectionType).ToList();
                }
                else if (SortBy == "Amount")
                {
                    objList = objList.OrderBy(p => p.TransactionAmount).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.CreatedDateTime).ToList();
                }
            }
            else
            {
                if (SortBy == "Status")
                {
                    objList = objList.OrderByDescending(p => p.strStatus).ToList();
                }
                else if (SortBy == "InvoiceNo")
                {
                    objList = objList.OrderByDescending(p => p.InvoiceNumber).ToList();
                }
                else if (SortBy == "TransactionType")
                {
                    objList = objList.OrderByDescending(p => p.strTransectionType).ToList();
                }
                else if (SortBy == "Amount")
                {
                    objList = objList.OrderByDescending(p => p.TransactionAmount).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.CreatedDateTime).ToList();
                }
            }
            return objList;
        }

    }
}
