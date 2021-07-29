using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class SchoolList : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Authentication.ISUserLogin())
                {
                    if (!Authentication.ISOrgUserLogin())
                    {
                        Response.Redirect(Authentication.AuthorizePage());
                    }
                }
                //if (Request.QueryString["OP"] == "GetData")
                //{
                //    GetData(Request.QueryString["ID"].ToString());
                //}
                if (!Page.IsPostBack)
                {
                    drpSchoolType.DataSource = DB.ISSchoolTypes.Where(p => p.Deleted == true && p.Active == true).ToList();
                    drpSchoolType.DataTextField = "Name";
                    drpSchoolType.DataValueField = "ID";
                    drpSchoolType.DataBind();
                    drpSchoolType.Items.Insert(0, new ListItem { Text = "Select School Type", Value = "0" });

                    drpAccountStatus.DataSource = new List<string> { "Active", "Inactive" }.ToList();
                    drpAccountStatus.DataBind();
                    drpAccountStatus.Items.Insert(0, new ListItem { Text = "Select Account Status", Value = "None" });

                    Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                    ltrtitle.Text = "School APP : School List";
                    Session["Objectlist"] = null;
                    OperationManagement.ID = 0;
                    OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                    BindData("", "", 0, "None");
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }
        public string GetUserType()
        {
            if (Authentication.LoggedInOrgUser != null)
            {
                return Authentication.LoggedInOrgUser.RoleID.ToString();
            }
            else
            {
                return "0";
            }
        }
        public bool GetAdmin()
        {
            if (Authentication.ISUserLogin() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void BindData(string SchholName, string customerNo, int schoolTypeId, string accountStatus)
        {
            try
            {
                List<MISSchool> objList = (from item in DB.ISSchools.Where(p => p.Deleted == true).ToList()
                                           select new MISSchool
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
                                               SchoolStatus = item.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.Active ? "Active" : item.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.InProcess ? "InProgress" : "InActive",
                                               Deleted = item.Deleted,
                                               CreatedBy = item.CreatedBy,
                                               CreatedDateTime = item.CreatedDateTime,
                                               ModifyBy = item.ModifyBy,
                                               ModifyDateTime = item.ModifyDateTime,
                                               DeletedBy = item.DeletedBy,
                                               DeletedDateTime = item.DeletedDateTime,
                                               ActiveStatus = item.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.Active ? true : false,
                                           }).ToList();
                //var objList = DB.ISSchools.Where(p => p.Deleted == true).ToList();

                var result = new List<MISSchool>();
                //result.AddRange(objList);


                if (SchholName != "")
                {
                    result.AddRange(objList.Where(p => p.Name.Contains(SchholName)).ToList());
                }
                if (customerNo != "")
                {
                    result.AddRange(objList.Where(p => p.CustomerNumber.Contains(customerNo)).ToList());
                }
                if (schoolTypeId != 0)
                    result.AddRange(objList.Where(p => p.TypeID.Equals(schoolTypeId)).ToList());
                if (accountStatus != "" && accountStatus != "None")
                    result.AddRange(objList.Where(p => p.ActiveStatus.Equals(accountStatus == "Active" ? true : false)).ToList());

                if (SchholName == "" && customerNo == "" && schoolTypeId == 0 && accountStatus == "None")
                    result.AddRange(objList);

                result = result.Select(r => r).Distinct().ToList();

                GvSchoolList.DataSource = result.OrderByDescending(p => p.CreatedDateTime).ToList();
                GvSchoolList.DataBind();
                Session["Objectlist"] = result.ToList();
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("SchoolAdd.aspx");
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                BindData(txtSchoolName.Text, txtCustNo.Text, Convert.ToInt32(drpSchoolType.SelectedValue), drpAccountStatus.SelectedValue);
                PanelManagement.OpenPanel(ref collapseOne1);
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                PanelManagement.ClosePanel(ref collapseOne1);
                BindData("", "", 0, "None");
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }

        private void Clear()
        {
            try
            {
                txtSchoolName.Text = "";
                txtCustNo.Text = "";
                drpAccountStatus.SelectedIndex = 0;
                drpSchoolType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }

        protected void GvSchoolList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnEdit")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("SchoolAdd.aspx?ID=" + ID);
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }

        protected void GvSchoolList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvSchoolList.PageIndex = e.NewPageIndex;
                if (txtSchoolName.Text != "" || txtCustNo.Text != "")
                {
                    BindData(txtSchoolName.Text, txtCustNo.Text, Convert.ToInt32(drpSchoolType.SelectedValue), drpAccountStatus.SelectedValue);
                }
                else
                {
                    BindData("", "", 0, "None");
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("Upload") + "/SchoolList.xlsx");
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }
        public void CreateCell(Row rw, String value)
        {
            try
            {
                Cell newCell = new Cell();
                rw.Append(newCell);
                newCell.CellValue = new CellValue(value);
                newCell.DataType = new EnumValue<CellValues>(CellValues.String);
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }
        private void CreateSpreadsheetWorkbook(string filepath)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = (List<ISSchool>)Session["Objectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("SchoolList").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "CustomerNo");
                CreateCell(Hederrow, "School Name");
                CreateCell(Hederrow, "School Number");
                CreateCell(Hederrow, "TypeID");
                CreateCell(Hederrow, "Address1");
                CreateCell(Hederrow, "Town");
                CreateCell(Hederrow, "AdminFirstName");
                CreateCell(Hederrow, "AdminLastName");
                CreateCell(Hederrow, "AdminEmail");
                CreateCell(Hederrow, "PhoneNumber");
                CreateCell(Hederrow, "Website");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.CustomerNumber);
                    CreateCell(row, item.Name);
                    CreateCell(row, item.Number);
                    CreateCell(row, item.TypeID.ToString());
                    CreateCell(row, item.Address1);
                    CreateCell(row, item.Town);
                    CreateCell(row, item.AdminFirstName);
                    CreateCell(row, item.AdminLastName);
                    CreateCell(row, item.AdminEmail);
                    CreateCell(row, item.PhoneNumber);
                    CreateCell(row, item.Website);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=SchoolList.xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                ///GvUserList.HeaderStyle.Font.Bold = true;
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int ID = Convert.ToInt32(chk.ToolTip);
            ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
            if (ObjSchool != null)
            {
                if (chk.Checked)
                {
                    ObjSchool.AccountStatusID = (int?)EnumsManagement.ACCOUNTSTATUS.Active;
                    ObjSchool.ActivatedBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                    ObjSchool.ActivationDate = DateTime.Now;
                    ObjSchool.Active = true;
                    DB.SaveChanges();
                    AlertMessageManagement.ServerMessage(Page, "School Activated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
                else
                {
                    ObjSchool.AccountStatusID = (int?)EnumsManagement.ACCOUNTSTATUS.InActive;
                    // ObjSchool.ActivationDate = DateTime.Now;
                    ObjSchool.Active = false;
                    DB.SaveChanges();
                    AlertMessageManagement.ServerMessage(Page, "School InActivate Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
            }
            else
            {
                AlertMessageManagement.ServerMessage(Page, "School not Found", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            BindData(txtSchoolName.Text, txtCustNo.Text, Convert.ToInt32(drpSchoolType.SelectedValue), drpAccountStatus.SelectedValue);
        }

        //public string GetData(string ID)
        //{
        //    try
        //    {
        //        OperationManagement.ID = Convert.ToInt32(ID);
        //        ISSchool obj = DB.ISSchools.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
        //        if (obj != null)
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ErrorLogManagement.AddLog(ex);
        //    }
        //    return "";
        //}

    }
}