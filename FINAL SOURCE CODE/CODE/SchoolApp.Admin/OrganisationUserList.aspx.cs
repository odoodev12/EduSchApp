using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class OrganisationUserList : System.Web.UI.Page
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
                    else
                    {
                        if (Authentication.LoggedInOrgUser.RoleID == 1 || Authentication.LoggedInOrgUser.RoleID == 2)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                        }
                    }
                }
                if (!Page.IsPostBack)
                {
                    drpRole.DataSource = DB.ISRoles.Where(p => p.Deleted == true && p.Active == true).ToList();
                    drpRole.DataTextField = "Name";
                    drpRole.DataValueField = "ID";
                    drpRole.DataBind();
                    drpRole.Items.Insert(0, new ListItem { Text = "Select Organisation User Role", Value = "0" });

                    drpStatus.DataSource = new List<string> { "Active", "InActive" }.ToList();
                    drpStatus.DataBind();
                    drpStatus.Items.Insert(0, new ListItem { Text = "Select Status", Value = "None" });

                    Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                    ltrtitle.Text = "School APP : Organisation List";
                    Session["Objectlist"] = null;
                    OperationManagement.ID = 0;
                    OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                    BindData("", "", "", "", "",0,"None");
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void BindData(string Code, string firstName, string lastName, string email, string town, int userRoleId, string status)
        {
            try
            {
                List<MISOrganisationUser> objList = new List<MISOrganisationUser>();
                objList = (from Obj in DB.ISOrganisationUsers.Where(p => p.Deleted == true).ToList()
                           select new MISOrganisationUser
                           {
                               ID = Obj.ID,
                               Photo = Obj.Photo,
                               FirstName = Obj.FirstName,
                               LastName = Obj.LastName,
                               OrgCode = Obj.OrgCode,
                               Email = Obj.Email,
                               Town = Obj.Town,
                               RoleName = Obj.ISRole.Name,
                               Status = Obj.Active == true ? "Active" : "InActive",
                               CreatedDateTime = Obj.CreatedDateTime,
                               Active = Obj.Active,
                               RoleID = Obj.RoleID
                           }).ToList();

                var result = new List<MISOrganisationUser>();

                if (Code != "")
                {
                    result.AddRange(objList.Where(p => p.OrgCode == Code).ToList());
                }
                if (firstName != "")
                    result.AddRange(objList.Where(p => p.FirstName == firstName).ToList());
                if (lastName != "")
                    result.AddRange(objList.Where(p => p.LastName == lastName).ToList());
                if (email != "")
                    result.AddRange(objList.Where(p => p.Email.Contains(email)).ToList());
                if (town != "")
                    result.AddRange(objList.Where(p => p.Town.Contains(town)).ToList());
                if(userRoleId != 0)
                    result.AddRange(objList.Where(p => p.RoleID == userRoleId).ToList());
                if (status != "None")
                    result.AddRange(objList.Where(p => p.Status == status).ToList());

                if (Code == "" && firstName == "" && lastName == "" && email == "" && town == "" && userRoleId == 0 && status == "None")
                    result = objList.ToList();

                result = result.Select(r => r).Distinct().ToList();

                GvUserList.DataSource = result.OrderByDescending(p => p.CreatedDateTime).ToList();
                GvUserList.DataBind();
                Session["Objectlist"] = result.ToList();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public string GetPasswords(int UserID)
        {
            string Password = "";
            try
            {
                ISOrganisationUser obj = DB.ISOrganisationUsers.SingleOrDefault(p => p.ID == UserID);
                if (obj != null)
                {
                    Password = EncryptionHelper.Decrypt(obj.Password);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
            return Password;
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("OrganisationUserAdd.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                BindData(txtOrgCode.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtTown.Text,Convert.ToInt32(drpRole.SelectedValue), drpStatus.SelectedValue);
                PanelManagement.OpenPanel(ref collapseOne1);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                PanelManagement.ClosePanel(ref collapseOne1);
                BindData("", "", "", "", "",0,"None");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        private void Clear()
        {
            try
            {
                txtOrgCode.Text = txtFirstName.Text = txtLastName.Text = txtOrgCode.Text = txtTown.Text = "";
                drpStatus.SelectedIndex = 0;
                drpStatus.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void GvUserList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnEdit")
                {
                    if (GetUserType() == "2" || GetUserType() == "4" || GetAdmin() == true)
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("OrganisationUserAdd.aspx?ID=" + ID);
                    }
                    else
                    {
                        AlertMessageManagement.ServerMessage(Page, "You don't have any rights to Edit Organisation Users.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void GvUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvUserList.PageIndex = e.NewPageIndex;
                if (txtOrgCode.Text != "")
                {
                    BindData(txtOrgCode.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtTown.Text, Convert.ToInt32(drpRole.SelectedValue), drpStatus.SelectedValue);
                }
                else
                {
                    BindData("", "", "", "", "",0,"None");
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("Upload") + "/OrganisationList.xlsx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
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
                ErrorLogManagement.AddLog(ex);
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
                var lists = (List<MISOrganisationUser>)Session["Objectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("OrganisationList").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "FirstName");
                CreateCell(Hederrow, "LastName");
                CreateCell(Hederrow, "Org_Code");
                CreateCell(Hederrow, "Address1");
                CreateCell(Hederrow, "Address2");
                CreateCell(Hederrow, "Town");
                CreateCell(Hederrow, "Email");
                CreateCell(Hederrow, "Password");
                CreateCell(Hederrow, "StartDate");
                CreateCell(Hederrow, "EndDate");
                CreateCell(Hederrow, "Role");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.FirstName);
                    CreateCell(row, item.LastName);
                    CreateCell(row, item.OrgCode);
                    CreateCell(row, item.Address1);
                    CreateCell(row, item.Address2);
                    CreateCell(row, item.Town);
                    CreateCell(row, item.Email);
                    CreateCell(row, GetPasswords(item.ID));
                    CreateCell(row, item.StartDate.Value.ToShortDateString());
                    CreateCell(row, item.EndDate.Value.ToShortDateString());
                    CreateCell(row, item.ISRole.Name);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=OrganisationList.xlsx");
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
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GetUserType() == "2" || GetUserType() == "4" || GetAdmin() == true)
                {
                    CheckBox chk = sender as CheckBox;
                    int ID = Convert.ToInt32(chk.ToolTip);
                    ISOrganisationUser ObjSchool = DB.ISOrganisationUsers.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                    if (ObjSchool != null)
                    {
                        if (chk.Checked)
                        {
                            ObjSchool.Active = true;
                            ObjSchool.ActivationBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                            ObjSchool.ActivationDate = DateTime.Now;
                            ObjSchool.LastUpdatedBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                            ObjSchool.ModifyBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.ID : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.ID : 1;
                            ObjSchool.ModifyDateTime = DateTime.Now;
                            DB.SaveChanges();
                            AlertMessageManagement.ServerMessage(Page, "Organisation User Activated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                        else
                        {
                            ObjSchool.Active = false;
                            ObjSchool.LastUpdatedBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                            ObjSchool.ModifyBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.ID : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.ID : 1;
                            ObjSchool.ModifyDateTime = DateTime.Now;
                            DB.SaveChanges();
                            AlertMessageManagement.ServerMessage(Page, "Organisation User InActivate Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                    }
                    else
                    {
                        AlertMessageManagement.ServerMessage(Page, "Organisation User not Found", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
                else
                {
                    AlertMessageManagement.ServerMessage(Page, "You don't have any rights to Edit Organisation Users.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                BindData(txtOrgCode.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtTown.Text, Convert.ToInt32(drpRole.SelectedValue), drpStatus.SelectedValue);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
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
    }
}