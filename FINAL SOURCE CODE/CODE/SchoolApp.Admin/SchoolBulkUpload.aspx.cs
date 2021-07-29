using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class SchoolBulkUpload : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        //string ImportConnString = ConfigurationManager.ConnectionStrings["SchoolAppConnectionString"].ToString();
        //System.Data.OleDb.OleDbConnection AccessConnection;
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISUserLogin())
            {
                if (!Authentication.ISOrgUserLogin())
                {
                    Response.Redirect(Authentication.AuthorizePage());
                }
                else
                {
                    if (Authentication.LoggedInOrgUser.RoleID == 1 || Authentication.LoggedInOrgUser.RoleID == 3)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                    }
                }
            }

            if (!IsPostBack)
            {
                Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                ltrtitle.Text = "School APP : Bulk Upload";
                BindDropdown();
            }
        }

        private void BindDropdown()
        {
            drpSchool.DataSource = DB.ISSchools.Where(p => p.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.Active && p.Deleted == true && p.Active == true).ToList();
            drpSchool.DataTextField = "Name";
            drpSchool.DataValueField = "ID";
            drpSchool.DataBind();
            drpSchool.Items.Insert(0, new ListItem { Text = "Select School", Value = "0" });
        }
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int SchoolID = Convert.ToInt32(drpSchool.SelectedValue);
                ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                EmailManagement _EmailManagement = new EmailManagement();
                if (rbtnStudent.Checked == true)
                {
                    if (FUTemplate.HasFile)
                    {
                        FUTemplate.SaveAs(Server.MapPath("~/Upload/") + FUTemplate.FileName);
                        String file_name = FUTemplate.FileName;

                        SpreadsheetDocument doc = SpreadsheetDocument.Open(Server.MapPath("~/Upload/") + FUTemplate.FileName, false);
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                        IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                        DataTable dt = new DataTable();
                        foreach (Row row in rows)
                        {
                            if (row.RowIndex.Value == 1)
                            {
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    dt.Columns.Add(GetValue(doc, cell));
                                }
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    i = CommonOperation.ExcelColumnNameToNumber(cell.CellReference.InnerText);
                                    dt.Rows[dt.Rows.Count - 1][i - 1] = GetValue(doc, cell);
                                    //i++;
                                }
                            }
                        }
                        //string excelConnectionString = "";
                        //excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Template/") + FUTemplate.FileName + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";

                        //AccessConnection = new System.Data.OleDb.OleDbConnection((excelConnectionString));
                        //AccessConnection.Open();
                        //System.Data.OleDb.OleDbCommand AccessCommand = new System.Data.OleDb.OleDbCommand("select * from [Sheet1$]", AccessConnection);
                        //DataSet DS = new DataSet();
                        //System.Data.OleDb.OleDbDataAdapter Da = new System.Data.OleDb.OleDbDataAdapter(AccessCommand);
                        //Da.Fill(DS);

                        //AccessConnection.Close();
                        //DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                        //// Then use SQL Bulk query to insert those data
                        List<MISStudentBulk> ObjList = new List<MISStudentBulk>();
                        int CreateCount = 0;
                        int UpdateCount = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string PPPassword = CommonOperation.GenerateNewRandom();
                                string SPPassword = CommonOperation.GenerateNewRandom();
                                MISStudentBulk objM = new MISStudentBulk();
                                objM.StudentNo = dt.Rows[i][1].ToString();

                                objM.StudentName = dt.Rows[i][2].ToString();
                                objM.ClassName = dt.Rows[i][3].ToString();
                                objM.SchoolID = SchoolID;
                                objM.Photo = "Upload/user.jpg";
                                objM.ParantName1 = dt.Rows[i][4].ToString();
                                objM.ParantEmail1 = dt.Rows[i][5].ToString();
                                //objM.ParantPassword1 = dt.Rows[i][6].ToString();
                                objM.ParantPhone1 = dt.Rows[i][6].ToString();
                                objM.ParantRelation1 = dt.Rows[i][7].ToString();
                                objM.ParantName2 = dt.Rows[i][8].ToString();
                                objM.ParantEmail2 = dt.Rows[i][9].ToString();
                                //objM.ParantPassword2 = dt.Rows[i][11].ToString();
                                objM.ParantPhone2 = dt.Rows[i][10].ToString();
                                objM.ParantRelation2 = dt.Rows[i][11].ToString();
                                try
                                {
                                    string StNo = dt.Rows[i][1].ToString();
                                    string StName = dt.Rows[i][2].ToString();
                                    string ClassesName = dt.Rows[i][3].ToString();
                                    string StEmail1 = dt.Rows[i][5].ToString();
                                    string StEmail2 = dt.Rows[i][9].ToString();
                                    string PPName = dt.Rows[i][4].ToString();
                                    string SPName = dt.Rows[i][8].ToString();
                                    string PPRelation = dt.Rows[i][7].ToString();
                                    string SPRelation = dt.Rows[i][11].ToString();
                                    string PPPhone = dt.Rows[i][6].ToString();
                                    string SPPhone = dt.Rows[i][10].ToString();
                                    //string PPPassword = dt.Rows[i][6].ToString();
                                    //string SPPassword = dt.Rows[i][11].ToString();
                                    ISClass standardClass = null;

                                    if (!string.IsNullOrEmpty(ClassesName))
                                        standardClass = DB.ISClasses.FirstOrDefault(p => p.TypeID == 1 && p.SchoolID == SchoolID && p.Name.ToLower() == ClassesName.ToLower() && p.Deleted == true);

                                    if (dt.Rows[i][0].ToString() == "Insert" || dt.Rows[i][0].ToString() == "Create")
                                    {
                                        objM.Command = dt.Rows[i][0].ToString();
                                        IList<ISStudent> objStudent = DB.ISStudents.Where(a => a.StudentNo == StNo && a.SchoolID == SchoolID && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                                        if (_School.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                                        {
                                            if (objStudent.Count > 0)
                                            {
                                                objM.Status = "Fail - This Student Number Already Exist";
                                            }
                                            else if (!CommonOperation.IsValidEmailId(StEmail1))
                                            {
                                                objM.Status = "Fail - Primary Parent Email is not valid format.";
                                            }
                                            else if (!CommonOperation.IsValidEmailId(StEmail2))
                                            {
                                                objM.Status = "Fail - Secondary Parent Email is not valid format.";
                                            }
                                            else if (DB.ISStudents.Where(a => a.StudentName.ToLower() == StName.ToLower()
                                            && (a.ParantEmail1.ToLower() == StEmail1.ToLower() || a.ParantEmail2.ToLower() == StEmail1.ToLower())
                                            && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Student Name With Primary Parent Email already Exist";
                                            }
                                            else if (StEmail2.Length > 0 && DB.ISStudents.Where(a => a.StudentName.ToLower() == StName.ToLower()
                                                    && (a.ParantEmail2.ToLower() == StEmail2.ToLower() || a.ParantEmail1.ToLower() == StEmail2.ToLower())
                                                && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Student Name With Secondary Parent Email already Exist";
                                            }
                                            else if (IsValidateStudentRecordnotBlank(objM))
                                            {
                                                // just leave it already error message to be in status property.
                                            }
                                            else if (standardClass == null)
                                            {
                                                objM.Status = "Fail - Student class name should have only standard class.";
                                            }
                                            else if (PPRelation == SPRelation && PPRelation != "Guardian" && SPRelation != "Guardian")
                                            {
                                                objM.Status = "Fail - Relationship on student records duplicating. Please contact the relevant School Admin for this change.";
                                            }
                                            else if (PPPhone == SPPhone)
                                            {
                                                objM.Status = "Fail - Phone number should be different for parent.";
                                            }
                                            else if (StEmail1 == StEmail2)
                                            {
                                                objM.Status = "Fail - Email should be different for parent.";
                                            }
                                            #region CommentedConditions
                                            //else if (DB.ISStudents.Where(a => a.ParantName1 != PPName && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true && a.StartDate == (DateTime?)null).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Primary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantName2 != SPName && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true && a.StartDate == (DateTime?)null).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Secondary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantRelation1 != PPRelation && a.StartDate == (DateTime?)null && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Primary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantRelation2 != SPRelation && a.StartDate == (DateTime?)null && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Secondary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantPhone1 != PPPhone && a.StartDate == (DateTime?)null && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Primary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantPhone2 != SPPhone && a.StartDate == (DateTime?)null && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Secondary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name";
                                            //}
                                            #endregion
                                            else
                                            {
                                                ISStudent obj = new ISStudent();
                                                obj.StudentName = StName;
                                                if (!String.IsNullOrEmpty(ClassesName))
                                                {
                                                    int schoolid = SchoolID;
                                                    string Name = ClassesName;
                                                    ISClass classes = DB.ISClasses.FirstOrDefault(p => p.SchoolID == schoolid && p.Name == Name && p.Deleted == true);
                                                    if (classes != null)
                                                        obj.ClassID = classes.ID;
                                                    else
                                                        obj.ClassID = 0;
                                                }
                                                else
                                                    obj.ClassID = 0;

                                                obj.StudentNo = StNo;
                                                obj.SchoolID = SchoolID;
                                                obj.Photo = "Upload/user.jpg";
                                                obj.ParantName1 = PPName;
                                                obj.ParantEmail1 = StEmail1;
                                                obj.ParantPassword1 = EncryptionHelper.Encrypt(PPPassword);
                                                obj.ParantPhone1 = PPPhone;
                                                obj.ParantRelation1 = PPRelation;
                                                if (!String.IsNullOrEmpty(SPName) && !String.IsNullOrEmpty(StEmail2))
                                                {
                                                    obj.ParantName2 = SPName;
                                                    obj.ParantEmail2 = StEmail2;
                                                    obj.ParantPassword2 = EncryptionHelper.Encrypt(SPPassword);
                                                    obj.ParantPhone2 = SPPhone;
                                                    obj.ParantRelation2 = SPRelation;
                                                }
                                                obj.CreatedBy = SchoolID;
                                                obj.CreatedDateTime = DateTime.Now;
                                                obj.ISImageEmail = false;
                                                obj.SentMailDate = DateTime.Now;
                                                obj.Active = true;
                                                obj.Deleted = true;
                                                obj.Out = 0;
                                                obj.Outbit = false;
                                                DB.ISStudents.Add(obj);
                                                DB.SaveChanges();

                                                ISPicker objPicker = new ISPicker();
                                                objPicker.SchoolID = SchoolID;
                                                objPicker.ParentID = obj.ID;
                                                objPicker.StudentID = obj.ID;
                                                objPicker.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                                objPicker.FirstName = PPName + "(" + PPRelation + ")";
                                                objPicker.Photo = "Upload/user.jpg";
                                                objPicker.Email = StEmail1;
                                                objPicker.Phone = PPPhone;
                                                objPicker.OneOffPickerFlag = false;
                                                objPicker.ActiveStatus = "Active";
                                                objPicker.Active = true;
                                                objPicker.Deleted = true;
                                                objPicker.CreatedBy = SchoolID;
                                                objPicker.CreatedDateTime = DateTime.Now;
                                                DB.ISPickers.Add(objPicker);
                                                DB.SaveChanges();
                                                if (DB.ISPickers.Where(p => p.ID == objPicker.ID && p.Active == true).ToList().Count > 0)
                                                {
                                                    ISPickerAssignment objAssign = new ISPickerAssignment();
                                                    objAssign.PickerId = objPicker.ID;
                                                    objAssign.StudentID = obj.ID;
                                                    objAssign.RemoveChildStatus = 0;
                                                    DB.ISPickerAssignments.Add(objAssign);
                                                    DB.SaveChanges();
                                                }
                                                if (!String.IsNullOrEmpty(SPName) && !String.IsNullOrEmpty(StEmail2))
                                                {
                                                    ISPicker objPickers = new ISPicker();
                                                    objPickers.SchoolID = SchoolID;
                                                    objPickers.ParentID = obj.ID;
                                                    objPickers.StudentID = obj.ID;
                                                    objPickers.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                                    objPickers.FirstName = SPName + "(" + SPRelation + ")";
                                                    objPickers.Photo = "Upload/user.jpg";
                                                    objPickers.Email = StEmail2;
                                                    objPickers.Phone = SPPhone;
                                                    objPickers.OneOffPickerFlag = false;
                                                    objPickers.ActiveStatus = "Active";
                                                    objPickers.Active = true;
                                                    objPickers.Deleted = true;
                                                    objPickers.CreatedBy = SchoolID;
                                                    objPickers.CreatedDateTime = DateTime.Now;
                                                    DB.ISPickers.Add(objPickers);
                                                    DB.SaveChanges();
                                                    if (DB.ISPickers.Where(p => p.ID == objPickers.ID && p.Active == true).ToList().Count > 0)
                                                    {
                                                        ISPickerAssignment objAssigns = new ISPickerAssignment();
                                                        objAssigns.PickerId = objPickers.ID;
                                                        objAssigns.StudentID = obj.ID;
                                                        objAssigns.RemoveChildStatus = 0;
                                                        DB.ISPickerAssignments.Add(objAssigns);
                                                        DB.SaveChanges();
                                                    }
                                                }

                                                objM.Status = "Success";
                                                CreateCount = CreateCount + 1;
                                            }
                                        }
                                        else
                                        {
                                            if (objStudent.Count > 0)
                                            {
                                                objM.Status = "Fail - This Student Number Already Exist";
                                            }
                                            else if (!CommonOperation.IsValidEmailId(StEmail1))
                                            {
                                                objM.Status = "Fail - Primary Parent Email is not valid format.";
                                            }
                                            else if (!CommonOperation.IsValidEmailId(StEmail2))
                                            {
                                                objM.Status = "Fail - Secondary Parent Email is not valid format.";
                                            }
                                            else if (DB.ISStudents.Where(a => a.StudentName.ToLower() == StName.ToLower() &&
                                                (a.ParantEmail1.ToLower() == StEmail1.ToLower() || a.ParantEmail2.ToLower() == StEmail1.ToLower()) &&
                                                a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool
                                            && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Student Name With Primary Parent Email already Exist";
                                            }
                                            else if (IsValidateStudentRecordnotBlank(objM))
                                            {
                                                // just leave it already error message to be in status property.
                                            }
                                            else if (standardClass == null)
                                            {
                                                objM.Status = "Fail - Student class name should have only standard class.";
                                            }
                                            else if (StEmail2.Length > 0 && DB.ISStudents.Where(a => a.StudentName.ToLower() == StName.ToLower() &&
                                            (a.ParantEmail2.ToLower() == StEmail2.ToLower() || a.ParantEmail1.ToLower() == StEmail2.ToLower()) &&
                                            a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool &&
                                            a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Student Name With Secondary Parent Email already Exist";
                                            }
                                            else if (PPRelation == SPRelation && PPRelation != "Guardian" && SPRelation != "Guardian")
                                            {
                                                objM.Status = "Fail - Relationship on student records duplicating. Please contact the relevant School Admin for this change.";
                                            }
                                            else if (PPPhone == SPPhone)
                                            {
                                                objM.Status = "Fail - Phone number should be different for parent.";
                                            }
                                            else if (StEmail1 == StEmail2)
                                            {
                                                objM.Status = "Fail - Email should be different for parent.";
                                            }
                                            #region CommentConditions
                                            //else if (DB.ISStudents.Where(a => a.ParantName1 != PPName && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true && a.StartDate == (DateTime?)null).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Primary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantName2 != SPName && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true && a.StartDate == (DateTime?)null).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Secondary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantRelation1 != PPRelation && a.StartDate == (DateTime?)null && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Primary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantRelation2 != SPRelation && a.StartDate == (DateTime?)null && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Secondary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantPhone1 != PPPhone && a.StartDate == (DateTime?)null && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Primary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name";
                                            //}
                                            //else if (DB.ISStudents.Where(a => a.ParantPhone2 != SPPhone && a.StartDate == (DateTime?)null && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                            //{
                                            //    objM.Status = "Fail - Secondary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name";
                                            //}
                                            #endregion
                                            else
                                            {
                                                ISStudent obj = new ISStudent();
                                                obj.StudentName = StName;
                                                if (!String.IsNullOrEmpty(ClassesName))
                                                {
                                                    int schoolid = SchoolID;
                                                    string Name = ClassesName;
                                                    ISClass classes = DB.ISClasses.FirstOrDefault(p => p.SchoolID == schoolid && p.Name == Name && p.Deleted == true);
                                                    if (classes != null)
                                                        obj.ClassID = classes.ID;
                                                    else
                                                        obj.ClassID = 0;
                                                }
                                                else
                                                    obj.ClassID = 0;
                                                obj.StudentNo = StNo;
                                                obj.SchoolID = SchoolID;
                                                obj.Photo = "Upload/user.jpg";
                                                obj.ParantName1 = PPName;
                                                obj.ParantEmail1 = StEmail1;
                                                obj.ParantPassword1 = EncryptionHelper.Encrypt(PPPassword);
                                                obj.ParantPhone1 = PPPhone;
                                                obj.ParantRelation1 = PPRelation;
                                                if (!String.IsNullOrEmpty(SPName) && !String.IsNullOrEmpty(StEmail2))
                                                {
                                                    obj.ParantName2 = SPName;
                                                    obj.ParantEmail2 = StEmail2;
                                                    obj.ParantPassword2 = EncryptionHelper.Encrypt(SPPassword);
                                                    obj.ParantPhone2 = SPPhone;
                                                    obj.ParantRelation2 = SPRelation;
                                                }
                                                obj.CreatedBy = SchoolID;
                                                obj.CreatedDateTime = DateTime.Now;
                                                obj.ISImageEmail = false;
                                                obj.SentMailDate = DateTime.Now;
                                                obj.Active = true;
                                                obj.Deleted = true;
                                                obj.Out = 0;
                                                obj.Outbit = false;
                                                DB.ISStudents.Add(obj);
                                                DB.SaveChanges();

                                                ISPicker objPicker = new ISPicker();
                                                objPicker.SchoolID = SchoolID;
                                                objPicker.ParentID = obj.ID;
                                                objPicker.StudentID = obj.ID;
                                                objPicker.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                                objPicker.FirstName = PPName + "(" + PPRelation + ")";
                                                objPicker.Photo = "Upload/user.jpg";
                                                objPicker.Email = StEmail1;
                                                objPicker.Phone = PPPhone;
                                                objPicker.OneOffPickerFlag = false;
                                                objPicker.ActiveStatus = "Active";
                                                objPicker.Active = true;
                                                objPicker.Deleted = true;
                                                objPicker.CreatedBy = SchoolID;
                                                objPicker.CreatedDateTime = DateTime.Now;
                                                DB.ISPickers.Add(objPicker);
                                                DB.SaveChanges();
                                                if (DB.ISPickers.Where(p => p.ID == objPicker.ID && p.Active == true).ToList().Count > 0)
                                                {
                                                    ISPickerAssignment objAssign = new ISPickerAssignment();
                                                    objAssign.PickerId = objPicker.ID;
                                                    objAssign.StudentID = obj.ID;
                                                    objAssign.RemoveChildStatus = 0;
                                                    DB.ISPickerAssignments.Add(objAssign);
                                                    DB.SaveChanges();
                                                }
                                                if (!String.IsNullOrEmpty(SPName) && !String.IsNullOrEmpty(StEmail2))
                                                {
                                                    ISPicker objPickers = new ISPicker();
                                                    objPickers.SchoolID = SchoolID;
                                                    objPickers.ParentID = obj.ID;
                                                    objPickers.StudentID = obj.ID;
                                                    objPickers.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                                    objPickers.FirstName = SPName + "(" + SPRelation + ")";
                                                    objPickers.Photo = "Upload/user.jpg";
                                                    objPickers.Email = StEmail2;
                                                    objPickers.Phone = SPPhone;
                                                    objPickers.OneOffPickerFlag = false;
                                                    objPickers.ActiveStatus = "Active";
                                                    objPickers.Active = true;
                                                    objPickers.Deleted = true;
                                                    objPickers.CreatedBy = SchoolID;
                                                    objPickers.CreatedDateTime = DateTime.Now;
                                                    DB.ISPickers.Add(objPickers);
                                                    DB.SaveChanges();
                                                    if (DB.ISPickers.Where(p => p.ID == objPickers.ID && p.Active == true).ToList().Count > 0)
                                                    {
                                                        ISPickerAssignment objAssigns = new ISPickerAssignment();
                                                        objAssigns.PickerId = objPickers.ID;
                                                        objAssigns.StudentID = obj.ID;
                                                        objAssigns.RemoveChildStatus = 0;
                                                        DB.ISPickerAssignments.Add(objAssigns);
                                                        DB.SaveChanges();
                                                    }
                                                }

                                                objM.Status = "Success";
                                                CreateCount = CreateCount + 1;
                                            }
                                        }
                                        try
                                        {
                                            ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                            if (StEmail1 != "")
                                            {
                                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", PPName, StEmail1, PPPassword, objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                                _EmailManagement.SendEmail(StEmail1, "New Student Creation", Message);
                                            }
                                            if (StEmail2 != "")
                                            {
                                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3} <br>Mobile No : {4} <br>Email id : {5} <br>", SPName, StEmail2, SPPassword, objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                                _EmailManagement.SendEmail(StEmail2, "New Student Creation", Message);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorLogManagement.AddLog(ex);
                                        }
                                    }
                                    else
                                    {
                                        objM.Command = dt.Rows[i][0].ToString();
                                        string ID = (!String.IsNullOrEmpty(dt.Rows[i][1].ToString())) ? Convert.ToString(dt.Rows[i][1].ToString()) : "";
                                        if (ID != "")
                                        {
                                            if (_School.TypeID == (int?)EnumsManagement.SCHOOLTYPE.Standard)
                                            {
                                                ISStudent objs = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ID && p.StartDate == (DateTime?)null && p.SchoolID == SchoolID);
                                                IList<ISStudent> obj2 = DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentNo == StNo && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                                                if (obj2.Count > 0)
                                                {
                                                    objM.Status = "Fail - This Student Number Already Exist";
                                                }
                                                else if (!CommonOperation.IsValidEmailId(StEmail1))
                                                {
                                                    objM.Status = "Fail - Primary Parent Email is not valid format.";
                                                }
                                                else if (!CommonOperation.IsValidEmailId(StEmail2))
                                                {
                                                    objM.Status = "Fail - Secondary Parent Email is not valid format.";
                                                }
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentName == StName && a.ParantEmail1 == StEmail1 && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentName.ToLower() == StName.ToLower()
                                            && (a.ParantEmail1.ToLower() == StEmail1.ToLower() || a.ParantEmail2.ToLower() == StEmail1.ToLower())
                                            && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                {
                                                    objM.Status = "Fail - Student Name With Primary Parent Email already Exist";
                                                }
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentName == StName && a.ParantEmail2 == StEmail2 && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                else if (StEmail2.Length > 0 && DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentName.ToLower() == StName.ToLower()
                                               && (a.ParantEmail2.ToLower() == StEmail2.ToLower() || a.ParantEmail1.ToLower() == StEmail2.ToLower())
                                           && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                {
                                                    objM.Status = "Fail - Student Name With Secondary Parent Email already Exist";
                                                }
                                                else if (IsValidateStudentRecordnotBlank(objM))
                                                {
                                                    // just leave it already error message to be in status property.
                                                }
                                                else if (standardClass == null)
                                                {
                                                    objM.Status = "Fail - Student class name should have only standard class.";
                                                }
                                                else if (PPRelation == SPRelation && PPRelation != "Guardian" && SPRelation != "Guardian")
                                                {
                                                    objM.Status = "Fail - Relationship on student records duplicating. Please contact the relevant School Admin for this change.";
                                                }
                                                else if (PPPhone == SPPhone)
                                                {
                                                    objM.Status = "Fail - Phone number should be different for parent.";
                                                }
                                                else if (StEmail1 == StEmail2)
                                                {
                                                    objM.Status = "Fail - Email should be different for parent.";
                                                }
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantName1 != PPName && a.ParantEmail1 == StEmail1 && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Primary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantName2 != SPName && a.ParantEmail2 == StEmail2 && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Secondary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantRelation1 != PPRelation && a.StartDate == (DateTime?)null && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Primary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantRelation2 != SPRelation && a.StartDate == (DateTime?)null && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Secondary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantPhone1 != PPPhone && a.StartDate == (DateTime?)null && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Primary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantPhone2 != SPPhone && a.StartDate == (DateTime?)null && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Secondary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name";
                                                //}
                                                else
                                                {
                                                    Session["PName"] = null;
                                                    Session["SName"] = null;
                                                    ISStudent obj = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ID && p.StartDate == (DateTime?)null && p.SchoolID == SchoolID);
                                                    Session["PName"] = obj.ParantName1 + "(" + obj.ParantRelation1 + ")";
                                                    if (obj.ParantName2 != "" && obj.ParantName2 != null)
                                                    {
                                                        Session["SName"] = obj.ParantName2 + "(" + obj.ParantRelation2 + ")";
                                                    }
                                                    if (obj != null)
                                                    {
                                                        obj.StudentName = dt.Rows[i][2].ToString();
                                                        if (!String.IsNullOrEmpty(dt.Rows[i][3].ToString()))
                                                        {
                                                            int schoolid = SchoolID;
                                                            string Name = dt.Rows[i][3].ToString();
                                                            ISClass classes = DB.ISClasses.FirstOrDefault(p => p.SchoolID == schoolid && p.Name == Name && p.Deleted == true);
                                                            if (classes != null)
                                                                obj.ClassID = classes.ID;
                                                            else
                                                                obj.ClassID = 0;
                                                        }
                                                        else
                                                            obj.ClassID = 0;
                                                        obj.StudentNo = StNo;
                                                        obj.SchoolID = SchoolID;
                                                        obj.Photo = "Upload/user.jpg";
                                                        obj.ParantName1 = PPName;
                                                        obj.ParantEmail1 = StEmail1;
                                                        obj.ParantPassword1 = EncryptionHelper.Encrypt(PPPassword);
                                                        obj.ParantPhone1 = PPPhone;
                                                        obj.ParantRelation1 = PPRelation;
                                                        if (!String.IsNullOrEmpty(SPName) && !String.IsNullOrEmpty(StEmail2))
                                                        {
                                                            obj.ParantName2 = SPName;
                                                            obj.ParantEmail2 = StEmail2;
                                                            obj.ParantPassword2 = EncryptionHelper.Encrypt(SPPassword);
                                                            obj.ParantPhone2 = SPPhone;
                                                            obj.ParantRelation2 = SPRelation;
                                                        }
                                                        else
                                                        {
                                                            obj.ParantName2 = "";
                                                            obj.ParantEmail2 = "";
                                                            obj.ParantPassword2 = "";
                                                            obj.ParantPhone2 = "";
                                                            obj.ParantRelation2 = "";
                                                        }
                                                        obj.ModifyBy = SchoolID;
                                                        obj.ModifyDateTime = DateTime.Now;
                                                        DB.SaveChanges();

                                                        //--------------------Pickers Changes -----------------------//
                                                        string Names = Session["PName"] != null ? Convert.ToString(Session["PName"]) : "";
                                                        ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.StudentID == obj.ID && p.FirstName == Names && p.Active == true && p.Deleted == true);
                                                        if (objPicker != null)
                                                        {
                                                            objPicker.SchoolID = SchoolID;
                                                            objPicker.ParentID = obj.ID;
                                                            objPicker.StudentID = obj.ID;
                                                            objPicker.FirstName = PPName + "(" + PPRelation + ")";
                                                            objPicker.Email = StEmail1;
                                                            objPicker.Phone = PPPhone;
                                                            objPicker.ModifyBy = SchoolID;
                                                            objPicker.ModifyDateTime = DateTime.Now;
                                                            DB.SaveChanges();
                                                        }
                                                        if (!String.IsNullOrEmpty(SPName) && !String.IsNullOrEmpty(StEmail2))
                                                        {
                                                            string NameSecond = Session["SName"] != null ? Session["SName"].ToString() : "";
                                                            ISPicker objPickers = DB.ISPickers.SingleOrDefault(p => p.StudentID == obj.ID && p.FirstName == NameSecond && p.Deleted == true);
                                                            if (objPickers != null)
                                                            {
                                                                objPickers.SchoolID = SchoolID;
                                                                objPickers.ParentID = obj.ID;
                                                                objPickers.StudentID = obj.ID;
                                                                objPickers.FirstName = SPName + "(" + SPRelation + ")";
                                                                objPickers.Email = StEmail2;
                                                                objPickers.Phone = SPPhone;
                                                                objPickers.ModifyBy = SchoolID;
                                                                objPickers.ModifyDateTime = DateTime.Now;
                                                                DB.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                ISPicker objPickers1 = new ISPicker();
                                                                objPickers1.SchoolID = SchoolID;
                                                                objPickers1.ParentID = obj.ID;
                                                                objPickers1.StudentID = obj.ID;
                                                                objPickers1.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                                                objPickers1.FirstName = SPName + "(" + SPRelation + ")";
                                                                objPickers1.Photo = "Upload/user.jpg";
                                                                objPickers1.Email = StEmail2;
                                                                objPickers1.Phone = SPPhone;
                                                                objPickers1.OneOffPickerFlag = false;
                                                                objPickers1.ActiveStatus = "Active";
                                                                objPickers1.Active = true;
                                                                objPickers1.Deleted = true;
                                                                objPickers1.CreatedBy = SchoolID;
                                                                objPickers1.CreatedDateTime = DateTime.Now;
                                                                DB.ISPickers.Add(objPickers1);
                                                                DB.SaveChanges();
                                                                if (DB.ISPickers.Where(p => p.ID == objPickers1.ID && p.Active == true).ToList().Count > 0)
                                                                {
                                                                    ISPickerAssignment objAssigns = new ISPickerAssignment();
                                                                    objAssigns.PickerId = objPickers1.ID;
                                                                    objAssigns.StudentID = obj.ID;
                                                                    objAssigns.RemoveChildStatus = 0;
                                                                    DB.ISPickerAssignments.Add(objAssigns);
                                                                    DB.SaveChanges();
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string NameDelete = Session["SName"] != null ? Session["SName"].ToString() : "";
                                                            ISPicker objPickers = DB.ISPickers.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == obj.ID && p.SchoolID == SchoolID && p.FirstName == NameDelete);
                                                            if (objPickers != null)
                                                            {
                                                                objPickers.Active = false;
                                                                objPickers.Deleted = false;
                                                                objPickers.DeletedBy = SchoolID;
                                                                objPickers.DeletedDateTime = DateTime.Now;
                                                                DB.SaveChanges();
                                                            }
                                                        }
                                                        //-------------------- MClassList Code ---------------------//
                                                        objM.Status = "Success";
                                                        UpdateCount = UpdateCount + 1;
                                                    }
                                                    else
                                                    {
                                                        objM.Status = "Fail";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ISStudent objs = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ID && p.SchoolID == SchoolID);
                                                IList<ISStudent> obj2 = DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentNo == StNo && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                                                if (obj2.Count > 0)
                                                {
                                                    objM.Status = "Fail - This Student Number Already Exist";
                                                }
                                                else if (!CommonOperation.IsValidEmailId(StEmail1))
                                                {
                                                    objM.Status = "Fail - Primary Parent Email is not valid format.";
                                                }
                                                else if (!CommonOperation.IsValidEmailId(StEmail2))
                                                {
                                                    objM.Status = "Fail - Secondary Parent Email is not valid format.";
                                                }
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentName == StName && a.ParantEmail1 == StEmail1 && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentName.ToLower() == StName.ToLower() &&
                                                (a.ParantEmail1.ToLower() == StEmail1.ToLower() || a.ParantEmail2.ToLower() == StEmail1.ToLower()) &&
                                                a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool
                                            && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                {
                                                    objM.Status = "Fail - Student Name With Primary Parent Email already Exist";
                                                }
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentName == StName && a.ParantEmail2 == StEmail2 && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                else if (StEmail2.Length > 0 && DB.ISStudents.Where(a => a.ID != objs.ID && a.StudentName.ToLower() == StName.ToLower() &&
                                            (a.ParantEmail2.ToLower() == StEmail2.ToLower() || a.ParantEmail1.ToLower() == StEmail2.ToLower()) &&
                                            a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool &&
                                            a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                {
                                                    objM.Status = "Fail - Student Name With Secondary Parent Email already Exist";
                                                }
                                                else if (IsValidateStudentRecordnotBlank(objM))
                                                {
                                                    // just leave it already error message to be in status property.
                                                }
                                                else if (standardClass == null)
                                                {
                                                    objM.Status = "Fail - Student class name should have only standard class.";
                                                }
                                                else if (PPRelation == SPRelation && PPRelation != "Guardian" && SPRelation != "Guardian")
                                                {
                                                    objM.Status = "Fail - Relationship on student records duplicating. Please contact the relevant School Admin for this change.";
                                                }
                                                else if (PPPhone == SPPhone)
                                                {
                                                    objM.Status = "Fail - Phone number should be different for parent.";
                                                }
                                                else if (StEmail1 == StEmail2)
                                                {
                                                    objM.Status = "Fail - Email should be different for parent.";
                                                }
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantName1 != PPName && a.ParantEmail1 == StEmail1 && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Primary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantName2 != SPName && a.ParantEmail2 == StEmail2 && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Secondary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantRelation1 != PPRelation && a.StartDate == (DateTime?)null && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Primary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantRelation2 != SPRelation && a.StartDate == (DateTime?)null && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Secondary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantPhone1 != PPPhone && a.StartDate == (DateTime?)null && a.ParantEmail1 == StEmail1 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Primary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name";
                                                //}
                                                //else if (DB.ISStudents.Where(a => a.ID != objs.ID && a.ParantPhone2 != SPPhone && a.StartDate == (DateTime?)null && a.ParantEmail2 == StEmail2 && a.Active == true && a.Deleted == true).ToList().Count > 0)
                                                //{
                                                //    objM.Status = "Fail - Secondary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name";
                                                //}
                                                else
                                                {
                                                    Session["PName"] = null;
                                                    Session["SName"] = null;
                                                    ISStudent obj = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ID && p.SchoolID == SchoolID);
                                                    Session["PName"] = obj.ParantName1 + "(" + obj.ParantRelation1 + ")";
                                                    if (obj.ParantName2 != "" && obj.ParantName2 != null)
                                                    {
                                                        Session["SName"] = obj.ParantName2 + "(" + obj.ParantRelation2 + ")";
                                                    }
                                                    if (obj != null)
                                                    {
                                                        obj.StudentName = StName;
                                                        if (!String.IsNullOrEmpty(ClassesName))
                                                        {
                                                            int schoolid = SchoolID;
                                                            string Name = ClassesName;
                                                            ISClass classes = DB.ISClasses.FirstOrDefault(p => p.SchoolID == schoolid && p.Name == Name && p.Deleted == true);
                                                            if (classes != null)
                                                                obj.ClassID = classes.ID;
                                                            else
                                                                obj.ClassID = 0;
                                                        }
                                                        else
                                                            obj.ClassID = 0;

                                                        obj.StudentNo = StNo;
                                                        obj.SchoolID = SchoolID;
                                                        obj.Photo = "Upload/user.jpg";
                                                        obj.ParantName1 = PPName;
                                                        obj.ParantEmail1 = StEmail1;
                                                        obj.ParantPhone1 = PPPhone;
                                                        obj.ParantRelation1 = PPRelation;
                                                        if (!String.IsNullOrEmpty(SPName) && !String.IsNullOrEmpty(StEmail2))
                                                        {
                                                            obj.ParantName2 = SPName;
                                                            obj.ParantEmail2 = StEmail2;
                                                            obj.ParantPassword2 = EncryptionHelper.Encrypt(SPPassword);
                                                            obj.ParantPhone2 = SPPhone;
                                                            obj.ParantRelation2 = SPRelation;
                                                        }
                                                        else
                                                        {
                                                            obj.ParantName2 = "";
                                                            obj.ParantEmail2 = "";
                                                            obj.ParantPassword2 = "";
                                                            obj.ParantPhone2 = "";
                                                            obj.ParantRelation2 = "";
                                                        }
                                                        obj.ModifyBy = SchoolID;
                                                        obj.ModifyDateTime = DateTime.Now;
                                                        DB.SaveChanges();

                                                        //--------------------Pickers Changes -----------------------//
                                                        string Names = Session["PName"] != null ? Convert.ToString(Session["PName"]) : "";
                                                        ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.StudentID == obj.ID && p.FirstName == Names && p.Active == true && p.Deleted == true);
                                                        if (objPicker != null)
                                                        {
                                                            objPicker.SchoolID = SchoolID;
                                                            objPicker.ParentID = obj.ID;
                                                            objPicker.StudentID = obj.ID;
                                                            objPicker.FirstName = PPName + "(" + PPRelation + ")";
                                                            objPicker.Email = StEmail1;
                                                            objPicker.Phone = PPPhone;
                                                            objPicker.ModifyBy = SchoolID;
                                                            objPicker.ModifyDateTime = DateTime.Now;
                                                            DB.SaveChanges();
                                                        }
                                                        if (!String.IsNullOrEmpty(SPName) && !String.IsNullOrEmpty(StEmail2))
                                                        {
                                                            string NameSecond = Session["SName"] != null ? Session["SName"].ToString() : "";
                                                            ISPicker objPickers = DB.ISPickers.SingleOrDefault(p => p.StudentID == obj.ID && p.FirstName == NameSecond && p.Deleted == true);
                                                            if (objPickers != null)
                                                            {
                                                                objPickers.SchoolID = SchoolID;
                                                                objPickers.ParentID = obj.ID;
                                                                objPickers.StudentID = obj.ID;
                                                                objPickers.FirstName = SPName + "(" + SPRelation + ")";
                                                                objPickers.Email = StEmail2;
                                                                objPickers.Phone = SPPhone;
                                                                objPickers.ModifyBy = SchoolID;
                                                                objPickers.ModifyDateTime = DateTime.Now;
                                                                DB.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                ISPicker objPickers1 = new ISPicker();
                                                                objPickers1.SchoolID = SchoolID;
                                                                objPickers1.ParentID = obj.ID;
                                                                objPickers1.StudentID = obj.ID;
                                                                objPickers1.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                                                objPickers1.FirstName = SPName + "(" + SPRelation + ")";
                                                                objPickers1.Photo = "Upload/user.jpg";
                                                                objPickers1.Email = StEmail2;
                                                                objPickers1.Phone = SPPhone;
                                                                objPickers1.OneOffPickerFlag = false;
                                                                objPickers1.ActiveStatus = "Active";
                                                                objPickers1.Active = true;
                                                                objPickers1.Deleted = true;
                                                                objPickers1.CreatedBy = SchoolID;
                                                                objPickers1.CreatedDateTime = DateTime.Now;
                                                                DB.ISPickers.Add(objPickers1);
                                                                DB.SaveChanges();
                                                                if (DB.ISPickers.Where(p => p.ID == objPickers1.ID && p.Active == true).ToList().Count > 0)
                                                                {
                                                                    ISPickerAssignment objAssigns = new ISPickerAssignment();
                                                                    objAssigns.PickerId = objPickers1.ID;
                                                                    objAssigns.StudentID = obj.ID;
                                                                    objAssigns.RemoveChildStatus = 0;
                                                                    DB.ISPickerAssignments.Add(objAssigns);
                                                                    DB.SaveChanges();
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string NameDelete = Session["SName"] != null ? Session["SName"].ToString() : "";
                                                            ISPicker objPickers = DB.ISPickers.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == obj.ID && p.SchoolID == SchoolID && p.FirstName == NameDelete);
                                                            if (objPickers != null)
                                                            {
                                                                objPickers.Active = false;
                                                                objPickers.Deleted = false;
                                                                objPickers.DeletedBy = SchoolID;
                                                                objPickers.DeletedDateTime = DateTime.Now;
                                                                DB.SaveChanges();
                                                            }
                                                        }
                                                        //-------------------- MClassList Code ---------------------//
                                                        objM.Status = "Success";
                                                        UpdateCount = UpdateCount + 1;
                                                    }
                                                    else
                                                    {
                                                        objM.Status = "Fail";
                                                    }
                                                }
                                            }
                                            try
                                            {
                                                ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                                if (StEmail1 != "")
                                                {
                                                    string Messages = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", PPName, StEmail1, PPPassword, objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                                    _EmailManagement.SendEmail(StEmail1, "Student Updation", Messages);
                                                }
                                                if (StEmail2 != "")
                                                {
                                                    string Messages = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3} <br>Mobile No : {4} <br>Email id : {5} <br>", SPName, StEmail2, SPPassword, objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                                    _EmailManagement.SendEmail(StEmail2, "Student Updation", Messages);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLogManagement.AddLog(ex);
                                            }
                                        }
                                        else
                                        {
                                            objM.Status = "Fail";
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    objM.Status = "Fail";
                                    ErrorLogManagement.AddLog(ex);
                                }
                                ObjList.Add(objM);
                            }
                            ISDataUploadHistory ObjHistory = new ISDataUploadHistory();
                            ObjHistory.SchoolID = SchoolID;
                            ObjHistory.TemplateName = "Student Template";
                            ObjHistory.CreatedCount = CreateCount;
                            ObjHistory.UpdatedCount = UpdateCount;
                            ObjHistory.Date = DateTime.Now;
                            ObjHistory.Active = true;
                            ObjHistory.Deleted = true;
                            ObjHistory.CreatedBy = SchoolID;
                            ObjHistory.CreatedByType = (int)EnumsManagement.CREATEBYTYPE.School;
                            ObjHistory.CreatedDateTime = DateTime.Now;
                            DB.ISDataUploadHistories.Add(ObjHistory);
                            DB.SaveChanges();
                        }
                        LogManagement.AddLog("Student Imported Successfully. Document Category : BulkData", "BulkData");
                        AlertMessageManagement.ServerMessage(Page, "Student Imported Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        EmailManage("Student", file_name);
                        Clear();
                        CreateSpreadsheetWorkbook(Server.MapPath("~/Upload") + "/ImportedStudentStatusList" + CommonOperation.GenerateRandom() + ".xlsx", ObjList);
                    }
                }
                else if (rbtnClass.Checked == true)
                {
                    if (FUTemplate.HasFile)
                    {
                        FUTemplate.SaveAs(Server.MapPath("~/Upload/") + FUTemplate.FileName);
                        String file_name = FUTemplate.FileName;
                        SpreadsheetDocument doc = SpreadsheetDocument.Open(Server.MapPath("~/Upload/") + FUTemplate.FileName, false);
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                        IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                        DataTable dt = new DataTable();
                        foreach (Row row in rows)
                        {
                            if (row.RowIndex.Value == 1)
                            {
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    dt.Columns.Add(GetValue(doc, cell));
                                }
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    i = CommonOperation.ExcelColumnNameToNumber(cell.CellReference.InnerText);
                                    dt.Rows[dt.Rows.Count - 1][i - 1] = GetValue(doc, cell);
                                    //i++;
                                }
                            }
                        }
                        //string excelConnectionString = "";
                        //excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Template/") + FUTemplate.FileName + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";

                        //AccessConnection = new System.Data.OleDb.OleDbConnection((excelConnectionString));
                        //AccessConnection.Open();
                        //System.Data.OleDb.OleDbCommand AccessCommand = new System.Data.OleDb.OleDbCommand("select * from [Sheet1$]", AccessConnection);
                        //DataSet DS = new DataSet();
                        //System.Data.OleDb.OleDbDataAdapter Da = new System.Data.OleDb.OleDbDataAdapter(AccessCommand);
                        //Da.Fill(DS);

                        //AccessConnection.Close();
                        //DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                        //// Then use SQL Bulk query to insert those data
                        List<MISClassBulk> ObjList = new List<MISClassBulk>();
                        int CreateCount = 0;
                        int UpdateCount = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                MISClassBulk objM = new MISClassBulk();
                                objM.Name = dt.Rows[i][1].ToString();
                                //objM.Year = dt.Rows[i][1].ToString();
                                if (!String.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                                {
                                    string year = dt.Rows[i][2].ToString();
                                    ISClassYear cyear = DB.ISClassYears.FirstOrDefault(p => p.Year == year);
                                    if (cyear != null)
                                        objM.Year = cyear.ID.ToString();
                                    else
                                        objM.Year = "";
                                }
                                else
                                    objM.Year = "";
                                if (!String.IsNullOrEmpty(dt.Rows[i][3].ToString()))
                                {
                                    string classtype = dt.Rows[i][3].ToString();
                                    ISClassType ctype = DB.ISClassTypes.FirstOrDefault(p => p.Name == classtype);
                                    if (ctype != null)
                                        objM.TypeID = ctype.ID;
                                    else
                                        objM.TypeID = 0;
                                }
                                else
                                    objM.TypeID = 0;
                                objM.AfterSchoolType = dt.Rows[i][4].ToString();

                                objM.ExternalOrganisation = dt.Rows[i][5].ToString();
                                try
                                {
                                    string ClassNm = dt.Rows[i][1].ToString();
                                    if (dt.Rows[i][0].ToString() == "Insert" || dt.Rows[i][0].ToString() == "Create")
                                    {
                                        objM.Command = dt.Rows[i][0].ToString();
                                        List<ISClass> ObjClasses = DB.ISClasses.Where(p => p.Name == ClassNm && p.SchoolID == SchoolID && p.Deleted == true).ToList();
                                        if (ObjClasses.Count > 0)
                                        {
                                            objM.Status = "Fail - Class already Exist";
                                        }
                                        else if (string.IsNullOrEmpty(ClassNm))
                                        {
                                            objM.Status = "Fail - Class name should not be blank.";
                                        }
                                        else if (string.IsNullOrEmpty(objM.Year))
                                        {
                                            objM.Status = "Fail - Class year should not be blank.";
                                        }
                                        else if (objM.TypeID == 0)
                                        {
                                            objM.Status = "Fail - Class type should not be blank or must valid.";
                                        }
                                        else
                                        {
                                            ISClass obj = new ISClass();
                                            obj.Name = dt.Rows[i][1].ToString();
                                            //obj.Year = dt.Rows[i][1].ToString();
                                            if (!String.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                                            {
                                                string year = dt.Rows[i][2].ToString();
                                                ISClassYear cyear = DB.ISClassYears.FirstOrDefault(p => p.Year == year);
                                                if (cyear != null)
                                                    obj.Year = cyear.ID.ToString();
                                                else
                                                    obj.Year = "";
                                            }
                                            else
                                                obj.Year = "";
                                            if (!String.IsNullOrEmpty(dt.Rows[i][3].ToString()))
                                            {
                                                string classtype = dt.Rows[i][3].ToString();
                                                ISClassType ctype = DB.ISClassTypes.FirstOrDefault(p => p.Name == classtype);
                                                if (ctype != null)
                                                    obj.TypeID = ctype.ID;
                                                else
                                                    obj.TypeID = 0;
                                            }
                                            else
                                                obj.TypeID = 0;
                                            obj.AfterSchoolType = dt.Rows[i][4].ToString();
                                            if (!String.IsNullOrEmpty(obj.ExternalOrganisation))
                                            {
                                                if (DB.ISSchools.Where(p => p.Name == dt.Rows[i][5].ToString() && p.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool && p.Deleted == true).ToList().Count > 0)
                                                {
                                                    obj.ISNonListed = false;
                                                    obj.ExternalOrganisation = dt.Rows[i][5].ToString();
                                                }
                                                else
                                                {
                                                    obj.ISNonListed = true;
                                                    obj.ExternalOrganisation = dt.Rows[i][5].ToString();
                                                }
                                            }
                                            obj.EndDate = new DateTime(2050, 01, 01);
                                            obj.CreatedDateTime = DateTime.Now;
                                            obj.SchoolID = SchoolID;
                                            obj.Active = true;
                                            obj.Deleted = true;
                                            obj.CreatedBy = SchoolID;
                                            DB.ISClasses.Add(obj);
                                            DB.SaveChanges();
                                            objM.Status = "Success";
                                            CreateCount = CreateCount + 1;
                                        }
                                    }
                                    else
                                    {
                                        objM.Command = dt.Rows[i][0].ToString();
                                        List<ISClass> ObjClasses = DB.ISClasses.Where(p => p.Name == ClassNm && p.Deleted == true && p.SchoolID == SchoolID && p.Deleted == true).ToList();
                                        if (ObjClasses.Count > 1)
                                        {
                                            objM.Status = "Fail - Class already Exist";
                                        }
                                        else if (string.IsNullOrEmpty(ClassNm))
                                        {
                                            objM.Status = "Fail - Class name should not be blank.";
                                        }
                                        else if (string.IsNullOrEmpty(objM.Year))
                                        {
                                            objM.Status = "Fail - Class year should not be blank.";
                                        }
                                        else if (objM.TypeID == 0)
                                        {
                                            objM.Status = "Fail - Class type should not be blank or must valid.";
                                        }
                                        else
                                        {
                                            ISClass obj = DB.ISClasses.OrderByDescending(p => p.ID).FirstOrDefault(p => p.Name == ClassNm && p.Deleted == true && p.SchoolID == SchoolID);
                                            if (obj != null)
                                            {
                                                obj.Name = dt.Rows[i][1].ToString();
                                                //obj.Year = dt.Rows[i][1].ToString();
                                                if (!String.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                                                {
                                                    string year = dt.Rows[i][2].ToString();
                                                    ISClassYear cyear = DB.ISClassYears.FirstOrDefault(p => p.Year == year);
                                                    if (cyear != null)
                                                        obj.Year = cyear.ID.ToString();
                                                    else
                                                        obj.Year = "";
                                                }
                                                else
                                                    obj.Year = "";
                                                if (!String.IsNullOrEmpty(dt.Rows[i][3].ToString()))
                                                {
                                                    string classtype = dt.Rows[i][3].ToString();
                                                    ISClassType ctype = DB.ISClassTypes.FirstOrDefault(p => p.Name == classtype);
                                                    if (ctype != null)
                                                        obj.TypeID = ctype.ID;
                                                    else
                                                        obj.TypeID = 0;
                                                }
                                                else
                                                    obj.TypeID = 0;
                                                obj.AfterSchoolType = dt.Rows[i][4].ToString();
                                                //obj.ExternalOrganisation = dt.Rows[i][5].ToString();
                                                if (!String.IsNullOrEmpty(obj.ExternalOrganisation))
                                                {
                                                    if (DB.ISSchools.Where(p => p.Name == dt.Rows[i][5].ToString() && p.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool && p.Deleted == true).ToList().Count > 0)
                                                    {
                                                        obj.ISNonListed = false;
                                                        obj.ExternalOrganisation = dt.Rows[i][5].ToString();
                                                    }
                                                    else
                                                    {
                                                        obj.ISNonListed = true;
                                                        obj.ExternalOrganisation = dt.Rows[i][5].ToString();
                                                    }
                                                }
                                                //if (!String.IsNullOrEmpty(dt.Rows[i][6].ToString()))
                                                //{
                                                //    obj.EndDate = DateTime.Parse(dt.Rows[i][6].ToString());
                                                //}
                                                obj.ModifyBy = SchoolID;
                                                obj.ModifyDateTime = DateTime.Now;
                                                DB.SaveChanges();
                                                objM.Status = "Success";
                                                UpdateCount = UpdateCount + 1;
                                            }
                                            else
                                            {
                                                objM.Status = "Fail";
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    objM.Status = "Fail";
                                    ErrorLogManagement.AddLog(ex);
                                }
                                ObjList.Add(objM);
                            }
                            ISDataUploadHistory ObjHistory = new ISDataUploadHistory();
                            ObjHistory.SchoolID = SchoolID;
                            ObjHistory.TemplateName = "Class Template";
                            ObjHistory.CreatedCount = CreateCount;
                            ObjHistory.UpdatedCount = UpdateCount;
                            ObjHistory.Date = DateTime.Now;
                            ObjHistory.Active = true;
                            ObjHistory.Deleted = true;
                            ObjHistory.CreatedBy = SchoolID;
                            ObjHistory.CreatedByType = (int)EnumsManagement.CREATEBYTYPE.School;
                            ObjHistory.CreatedDateTime = DateTime.Now;
                            DB.ISDataUploadHistories.Add(ObjHistory);
                            DB.SaveChanges();
                        }
                        LogManagement.AddLog("Classes Imported Successfully. Document Category : BulkData", "BulkData");
                        AlertMessageManagement.ServerMessage(Page, "Classes Imported Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                        EmailManage("Class", file_name);

                        Clear();
                        CreateSpreadsheetWorkbooks(Server.MapPath("~/Upload") + "/ImportedClassStatusList" + CommonOperation.GenerateRandom() + ".xlsx", ObjList);
                    }
                }
                else if (rbtnTeacher.Checked == true)
                {
                    if (FUTemplate.HasFile)
                    {
                        FUTemplate.SaveAs(Server.MapPath("~/Upload/") + FUTemplate.FileName);
                        String file_name = FUTemplate.FileName;
                        SpreadsheetDocument doc = SpreadsheetDocument.Open(Server.MapPath("~/Upload/") + FUTemplate.FileName, false);
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                        IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                        DataTable dt = new DataTable();
                        foreach (Row row in rows)
                        {
                            if (row.RowIndex.Value == 1)
                            {
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    dt.Columns.Add(GetValue(doc, cell));
                                }
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    i = CommonOperation.ExcelColumnNameToNumber(cell.CellReference.InnerText);
                                    dt.Rows[dt.Rows.Count - 1][i - 1] = GetValue(doc, cell);
                                    //i++;
                                }
                            }
                        }
                        //string excelConnectionString = "";
                        //excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Template/") + FUTemplate.FileName + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";

                        //AccessConnection = new System.Data.OleDb.OleDbConnection(excelConnectionString);
                        //AccessConnection.Open(); /*Maharshi error is here*/
                        //                         /*MAharshi error is here*/
                        //System.Data.OleDb.OleDbCommand AccessCommand = new System.Data.OleDb.OleDbCommand("select * from [Sheet1$]", AccessConnection);
                        //DataSet DS = new DataSet();
                        //System.Data.OleDb.OleDbDataAdapter Da = new System.Data.OleDb.OleDbDataAdapter(AccessCommand);
                        //Da.Fill(DS);

                        //AccessConnection.Close();
                        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                        List<MISTeacherBulk> objLists = new List<MISTeacherBulk>();
                        int CreateCount = 0;
                        int UpdateCount = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string Password = CommonOperation.GenerateNewRandom();
                                MISTeacherBulk objM = new MISTeacherBulk();
                                objM.Title = dt.Rows[i][1].ToString();
                                objM.TeacherNo = dt.Rows[i][2].ToString();
                                objM.Name = dt.Rows[i][3].ToString();

                                ISClass firstClassObj = null;

                                if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                {
                                    objM.FirstClass = dt.Rows[i][4].ToString();
                                }
                                if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                {
                                    objM.SecondClass = dt.Rows[i][5].ToString();
                                }
                                if (!String.IsNullOrEmpty(dt.Rows[i][6].ToString()))
                                {
                                    objM.RoleName = dt.Rows[i][6].ToString();
                                }

                                objM.Email = dt.Rows[i][7].ToString();
                                objM.PhoneNo = dt.Rows[i][8].ToString();
                                //objM.Password = Password;
                                string RoleName = objM.RoleName;
                                try
                                {
                                    ISUserRole userRoleObject = null;
                                    if (!string.IsNullOrEmpty(RoleName))
                                        userRoleObject = DB.ISUserRoles.FirstOrDefault(p => p.RoleName.ToLower() == RoleName.ToLower() && p.RoleType == (int)EnumsManagement.ROLETYPE.TEACHING && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);

                                    string TName = dt.Rows[i][3].ToString();
                                    string TEmail = dt.Rows[i][7].ToString();
                                    string TPhone = dt.Rows[i][8].ToString();
                                    string TNo = dt.Rows[i][2].ToString();
                                    if (dt.Rows[i][0].ToString() == "Insert" || dt.Rows[i][0].ToString() == "Create")
                                    {
                                        objM.Command = dt.Rows[i][0].ToString();
                                        if (DB.ISTeachers.Where(a => a.Email == TEmail && a.Deleted == true).ToList().Count > 0)
                                        {
                                            objM.Status = "Fail - Not Allowed. Teacher Email Address is Already Setup in the School.";
                                        }
                                        else if (DB.ISTeachers.Where(a => a.PhoneNo == TPhone && a.SchoolID == SchoolID && a.Deleted == true).ToList().Count > 0)
                                        {
                                            objM.Status = "Fail - Not Allowed. Teacher Phone number is Already Setup in the School.";
                                        }
                                        else if (DB.ISTeachers.Where(a => a.TeacherNo == TNo && a.SchoolID == SchoolID && a.Deleted == true).ToList().Count > 0)
                                        {
                                            objM.Status = "Fail - Not Allowed. Teacher Number is Already Setup in the School.";
                                        }
                                        else if (DB.ISTeachers.Where(a => a.Email == TEmail && a.Name == TName && a.Deleted == true).ToList().Count > 0)
                                        {
                                            objM.Status = "Fail - Not Allowed. Teacher Name is Already Setup with this Email in the School.";
                                        }
                                        else if (string.IsNullOrEmpty(objM.TeacherNo))
                                        {
                                            objM.Status = "Fail - Not Allowed. Teacher Number should not be blank.";
                                        }
                                        else if (string.IsNullOrEmpty(objM.Name))
                                        {
                                            objM.Status = "Fail - Not Allowed. Teacher Name should not be blank.";
                                        }
                                        else if (string.IsNullOrEmpty(TEmail))
                                        {
                                            objM.Status = "Fail - Not Allowed. Email should not be blank.";
                                        }
                                        else if (string.IsNullOrEmpty(TPhone))
                                        {
                                            objM.Status = "Fail - Not Allowed. Phone Number should not be blank.";
                                        }
                                        else if (firstClassObj == null)
                                        {
                                            objM.Status = "Fail - Not Allowed. First class must allowed or must valid in the school.";
                                        }
                                        else if (string.IsNullOrEmpty(RoleName))
                                        {
                                            objM.Status = "Fail - Not Allowed. Role Name should not be blank.";
                                        }
                                        else if (userRoleObject == null)
                                        {
                                            objM.Status = "Fail - Not Allowed. Role Name not exists in the school.";
                                        }
                                        else
                                        {
                                            ISTeacher obj = new ISTeacher();
                                            obj.Title = dt.Rows[i][1].ToString();
                                            obj.TeacherNo = dt.Rows[i][2].ToString();
                                            obj.Name = dt.Rows[i][3].ToString();
                                            obj.Photo = "Upload/user.jpg";
                                            if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                            {
                                                string classtype = dt.Rows[i][4].ToString();
                                                ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.SchoolID == SchoolID && p.Active == true && p.Deleted == true);
                                                if (ctype != null)
                                                    obj.ClassID = ctype.ID;
                                                else
                                                    obj.ClassID = 0;
                                            }
                                            else
                                                obj.ClassID = 0;

                                            if (!String.IsNullOrEmpty(dt.Rows[i][6].ToString()))
                                            {   
                                                ISUserRole ctype = DB.ISUserRoles.FirstOrDefault(p => p.RoleName == RoleName && p.RoleType == (int)EnumsManagement.ROLETYPE.TEACHING && p.SchoolID == SchoolID && p.Active == true && p.Deleted == true);
                                                if (ctype != null)
                                                    obj.RoleID = ctype.ID;
                                                else
                                                    obj.RoleID = 0;
                                            }
                                            else
                                                obj.RoleID = 0;
                                            obj.Role = (int)EnumsManagement.ROLETYPE.TEACHING;
                                            obj.Email = dt.Rows[i][7].ToString();
                                            obj.PhoneNo = dt.Rows[i][8].ToString();
                                            obj.Password = EncryptionHelper.Encrypt(Password);
                                            obj.CreatedDateTime = DateTime.Now;
                                            obj.SchoolID = SchoolID;
                                            obj.Active = true;
                                            obj.Deleted = true;
                                            obj.CreatedBy = SchoolID;
                                            DB.ISTeachers.Add(obj);
                                            DB.SaveChanges();

                                            ISTeacherClassAssignment obj1 = new ISTeacherClassAssignment();
                                            if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                            {
                                                string classtype = dt.Rows[i][4].ToString();
                                                ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.SchoolID == SchoolID && p.Active == true && p.Deleted == true);
                                                if (ctype != null)
                                                    obj1.ClassID = ctype.ID;
                                                else
                                                    obj1.ClassID = 0;
                                            }
                                            else
                                                obj1.ClassID = 0;
                                            obj1.TeacherID = obj.ID;
                                            obj1.CreatedDateTime = DateTime.Now;
                                            obj1.Active = true;
                                            obj1.Deleted = true;
                                            obj1.CreatedBy = SchoolID;
                                            obj1.Out = 0;
                                            obj1.Outbit = false;
                                            DB.ISTeacherClassAssignments.Add(obj1);
                                            DB.SaveChanges();
                                            if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                            {
                                                ISTeacherClassAssignment obj2 = new ISTeacherClassAssignment();
                                                if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                                {
                                                    string classtype = dt.Rows[i][5].ToString();
                                                    ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.SchoolID == SchoolID && p.Active == true && p.Deleted == true);
                                                    if (ctype != null)
                                                        obj2.ClassID = ctype.ID;
                                                    else
                                                        obj2.ClassID = 0;
                                                }
                                                else
                                                    obj2.ClassID = 0;
                                                obj2.TeacherID = obj.ID;
                                                obj2.CreatedDateTime = DateTime.Now;
                                                obj2.Active = true;
                                                obj2.Deleted = true;
                                                obj2.CreatedBy = SchoolID;
                                                obj2.Out = 0;
                                                obj2.Outbit = false;
                                                DB.ISTeacherClassAssignments.Add(obj2);
                                                DB.SaveChanges();
                                            }
                                            objM.Status = "Success";
                                            CreateCount = CreateCount + 1;
                                            if (!String.IsNullOrEmpty(dt.Rows[i][7].ToString()))
                                            {
                                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", TName, TEmail, Password, _School.Name, _School.PhoneNumber, _School.AdminEmail);
                                                _EmailManagement.SendEmail(dt.Rows[i][7].ToString(), "Your Account Credentials", Message);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objM.Command = dt.Rows[i][0].ToString();
                                        string ID = dt.Rows[i][2].ToString() != "" ? Convert.ToString(dt.Rows[i][2].ToString()) : "";
                                        if (ID != "")
                                        {
                                            ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.TeacherNo == ID && p.SchoolID == SchoolID && p.Deleted == true && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING);
                                            if (DB.ISTeachers.Where(a => a.ID != ObjTeacher.ID && a.Email == TEmail && a.SchoolID == SchoolID && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Not Allowed. Teacher Email Address is Already Setup in the School.";
                                            }
                                            else if (DB.ISTeachers.Where(a => a.ID != ObjTeacher.ID && a.PhoneNo == TPhone && a.SchoolID == SchoolID && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Not Allowed. Teacher Phone number is Already Setup in the School.";
                                            }
                                            else if (DB.ISTeachers.Where(a => a.ID != ObjTeacher.ID && a.TeacherNo == TNo && a.SchoolID == SchoolID && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Not Allowed. Teacher Number is Already Setup in the School.";
                                            }
                                            else if (DB.ISTeachers.Where(a => a.ID != ObjTeacher.ID && a.Email == TEmail && a.Name == TName && a.SchoolID == SchoolID && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Not Allowed. Teacher Name is Already Setup with this Email in the School.";
                                            }
                                            else if (string.IsNullOrEmpty(objM.TeacherNo))
                                            {
                                                objM.Status = "Fail - Not Allowed. Teacher Number should not be blank.";
                                            }
                                            else if (string.IsNullOrEmpty(objM.Name))
                                            {
                                                objM.Status = "Fail - Not Allowed. Teacher Name should not be blank.";
                                            }
                                            else if (string.IsNullOrEmpty(TEmail))
                                            {
                                                objM.Status = "Fail - Not Allowed. Email should not be blank.";
                                            }
                                            else if (string.IsNullOrEmpty(TPhone))
                                            {
                                                objM.Status = "Fail - Not Allowed. Phone Number should not be blank.";
                                            }
                                            else if (firstClassObj == null)
                                            {
                                                objM.Status = "Fail - Not Allowed. First class must allowed or must valid in the school.";
                                            }
                                            else if (string.IsNullOrEmpty(RoleName))
                                            {
                                                objM.Status = "Fail - Not Allowed. Role Name should not be blank.";
                                            }
                                            else if (userRoleObject == null)
                                            {
                                                objM.Status = "Fail - Not Allowed. Role Name not exists in the school.";
                                            }
                                            else
                                            {
                                                ISTeacher obj = DB.ISTeachers.SingleOrDefault(p => p.TeacherNo == ID && p.SchoolID == SchoolID && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING);
                                                if (obj != null)
                                                {
                                                    var item = dt.Rows[i][1].ToString();
                                                    obj.Title = dt.Rows[i][1].ToString();
                                                    obj.TeacherNo = dt.Rows[i][2].ToString();
                                                    obj.Name = dt.Rows[i][3].ToString();

                                                    if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                                    {
                                                        string classtype = dt.Rows[i][4].ToString();
                                                        ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.SchoolID == SchoolID && p.Active == true && p.Deleted == true);
                                                        if (ctype != null)
                                                            obj.ClassID = ctype.ID;
                                                        else
                                                            obj.ClassID = 0;
                                                    }
                                                    else
                                                        obj.ClassID = 0;

                                                    if (!String.IsNullOrEmpty(dt.Rows[i][6].ToString()))
                                                    {   
                                                        ISUserRole ctype = DB.ISUserRoles.FirstOrDefault(p => p.RoleName == RoleName && p.RoleType == (int)EnumsManagement.ROLETYPE.TEACHING && p.SchoolID == SchoolID && p.Active == true && p.Deleted == true);
                                                        if (ctype != null)
                                                            obj.RoleID = ctype.ID;
                                                        else
                                                            obj.RoleID = 0;
                                                    }
                                                    else
                                                        obj.RoleID = 0;

                                                    obj.Email = dt.Rows[i][7].ToString();
                                                    obj.PhoneNo = dt.Rows[i][8].ToString();
                                                    obj.ModifyBy = SchoolID;
                                                    obj.ModifyDateTime = DateTime.Now;
                                                    obj.SchoolID = SchoolID;
                                                    DB.SaveChanges();
                                                    //Remove Range
                                                    List<ISTeacherClassAssignment> objList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == obj.ID && p.Active == true).ToList();
                                                    DB.ISTeacherClassAssignments.RemoveRange(objList);
                                                    DB.SaveChanges();

                                                    ISTeacherClassAssignment obj1 = new ISTeacherClassAssignment();
                                                    if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                                    {
                                                        string classtype = dt.Rows[i][4].ToString();
                                                        ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.SchoolID == SchoolID && p.Active == true && p.Deleted == true);
                                                        if (ctype != null)
                                                            obj1.ClassID = ctype.ID;
                                                        else
                                                            obj1.ClassID = 0;
                                                    }
                                                    else
                                                        obj1.ClassID = 0;
                                                    obj1.TeacherID = obj.ID;
                                                    obj1.CreatedDateTime = DateTime.Now;
                                                    obj1.Active = true;
                                                    obj1.Deleted = true;
                                                    obj1.CreatedBy = SchoolID;
                                                    obj1.Out = 0;
                                                    obj1.Outbit = false;
                                                    DB.ISTeacherClassAssignments.Add(obj1);
                                                    DB.SaveChanges();
                                                    if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                                    {
                                                        ISTeacherClassAssignment obj2 = new ISTeacherClassAssignment();
                                                        if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                                        {
                                                            string classtype = dt.Rows[i][5].ToString();
                                                            ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.SchoolID == SchoolID && p.Active == true && p.Deleted == true);
                                                            if (ctype != null)
                                                                obj2.ClassID = ctype.ID;
                                                            else
                                                                obj2.ClassID = 0;
                                                        }
                                                        else
                                                            obj2.ClassID = 0;
                                                        obj2.TeacherID = obj.ID;
                                                        obj2.CreatedDateTime = DateTime.Now;
                                                        obj2.Active = true;
                                                        obj2.Deleted = true;
                                                        obj2.CreatedBy = SchoolID;
                                                        obj2.Out = 0;
                                                        obj2.Outbit = false;
                                                        DB.ISTeacherClassAssignments.Add(obj2);
                                                        DB.SaveChanges();
                                                    }
                                                    objM.Status = "Success";
                                                    UpdateCount = UpdateCount + 1;
                                                    if (!String.IsNullOrEmpty(TEmail))
                                                    {
                                                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Your Profile has been Changed Successfully SO if you want to check then Login to School Application.<br /><br>Thanks,<br>School APP<br> {1}<br>Mobile No : {2} <br>Email id : {3}<br>", TName, _School.Name, _School.PhoneNumber, _School.AdminEmail);
                                                        _EmailManagement.SendEmail(TEmail, "Profile Changes", Message);
                                                    }
                                                }
                                                else
                                                {
                                                    objM.Status = "Fail";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            objM.Status = "Fail";
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                    objM.Status = "Fail";
                                    ErrorLogManagement.AddLog(ex);
                                }
                                objLists.Add(objM);
                            }
                            ISDataUploadHistory ObjHistory = new ISDataUploadHistory();
                            ObjHistory.SchoolID = SchoolID;
                            ObjHistory.TemplateName = "Teacher Template";
                            ObjHistory.CreatedCount = CreateCount;
                            ObjHistory.UpdatedCount = UpdateCount;
                            ObjHistory.Date = DateTime.Now;
                            ObjHistory.Active = true;
                            ObjHistory.Deleted = true;
                            ObjHistory.CreatedBy = SchoolID;
                            ObjHistory.CreatedByType = (int)EnumsManagement.CREATEBYTYPE.School;
                            ObjHistory.CreatedDateTime = DateTime.Now;
                            DB.ISDataUploadHistories.Add(ObjHistory);
                            DB.SaveChanges();
                        }
                        LogManagement.AddLog("Teacher Imported Successfully. Document Category : BulkData", "BulkData");
                        AlertMessageManagement.ServerMessage(Page, "Teacher Imported Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                        EmailManage("Teacher", file_name);

                        Clear();
                        CreateSpreadsheetWorkbookss(Server.MapPath("~/Upload") + "/ImportedTeacherStatusList" + CommonOperation.GenerateRandom() + ".xlsx", objLists);
                    }
                }
                else if (rbtnNonTeacher.Checked)
                {
                    if (FUTemplate.HasFile)
                    {
                        FUTemplate.SaveAs(Server.MapPath("~/Upload/") + FUTemplate.FileName);
                        String file_name = FUTemplate.FileName;

                        SpreadsheetDocument doc = SpreadsheetDocument.Open(Server.MapPath("~/Upload/") + FUTemplate.FileName, false);
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                        IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                        DataTable dt = new DataTable();
                        foreach (Row row in rows)
                        {
                            if (row.RowIndex.Value == 1)
                            {
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    dt.Columns.Add(GetValue(doc, cell));
                                }
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (Cell cell in row.Descendants<Cell>())
                                {

                                    i = CommonOperation.ExcelColumnNameToNumber(cell.CellReference.InnerText);
                                    dt.Rows[dt.Rows.Count - 1][i - 1] = GetValue(doc, cell);
                                    //i++;
                                }
                            }
                        }
                        //string excelConnectionString = "";
                        //excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Template/") + FUTemplate.FileName + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";

                        //AccessConnection = new System.Data.OleDb.OleDbConnection(excelConnectionString);
                        //AccessConnection.Open(); /*Maharshi error is here*/
                        //                         /*MAharshi error is here*/
                        //System.Data.OleDb.OleDbCommand AccessCommand = new System.Data.OleDb.OleDbCommand("select * from [Sheet1$]", AccessConnection);
                        //DataSet DS = new DataSet();
                        //System.Data.OleDb.OleDbDataAdapter Da = new System.Data.OleDb.OleDbDataAdapter(AccessCommand);
                        //Da.Fill(DS);

                        //AccessConnection.Close();
                        //DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                        List<MISTeacherBulk> objLists = new List<MISTeacherBulk>();
                        StringBuilder message = new StringBuilder();
                        int CreateCount = 0;
                        int UpdateCount = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string Password = CommonOperation.GenerateNewRandom();
                                MISTeacherBulk objM = new MISTeacherBulk();
                                objM.Title = dt.Rows[i][1].ToString();
                                objM.TeacherNo = dt.Rows[i][2].ToString();
                                objM.Name = dt.Rows[i][3].ToString();

                                ISClass firstClassObj = null;
                                ISClass secondClassObj = null;
                                //DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);

                                if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                {
                                    objM.FirstClass = dt.Rows[i][4].ToString();
                                    firstClassObj = DB.ISClasses.FirstOrDefault(p => p.Name == objM.FirstClass && p.TypeID != 1 && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                }
                                if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                {
                                    objM.SecondClass = dt.Rows[i][5].ToString();
                                    secondClassObj = DB.ISClasses.FirstOrDefault(p => p.Name == objM.SecondClass && p.TypeID != 1 && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                }
                                if (!String.IsNullOrEmpty(dt.Rows[i][6].ToString()))
                                {
                                    objM.RoleName = dt.Rows[i][6].ToString();
                                }

                                objM.Email = dt.Rows[i][7].ToString();
                                objM.PhoneNo = dt.Rows[i][8].ToString();
                                //objM.Password = Password;
                                try
                                {
                                    string TName = dt.Rows[i][3].ToString();
                                    string TEmail = dt.Rows[i][7].ToString();
                                    string TPhone = dt.Rows[i][8].ToString();
                                    string TNo = dt.Rows[i][2].ToString();
                                    string RoleName = dt.Rows[i][6].ToString();
                                    ISUserRole userRoleObject = null;
                                    if (!string.IsNullOrEmpty(RoleName))
                                        userRoleObject = DB.ISUserRoles.FirstOrDefault(p => p.RoleName.ToLower() == RoleName.ToLower() && p.RoleType == (int)EnumsManagement.ROLETYPE.NONTEACHING && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);

                                    if (dt.Rows[i][0].ToString() == "Insert" || dt.Rows[i][0].ToString() == "Create")
                                    {
                                        objM.Command = dt.Rows[i][0].ToString();
                                        if (DB.ISTeachers.Where(a => a.Email == TEmail && a.Deleted == true).ToList().Count > 0)
                                        {
                                            objM.Status = "Fail - Not Allowed. Non-Teacher Email Address is Already Setup in the School.";
                                        }
                                        else if (DB.ISTeachers.Where(a => a.PhoneNo == TPhone && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                                        {
                                            objM.Status = "Fail - Not Allowed. Non-Teacher Phone number is Already Setup in the School.";
                                        }
                                        else if (DB.ISTeachers.Where(a => a.TeacherNo == TNo && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                                        {
                                            objM.Status = "Fail - Not Allowed. Non-Teacher Number is Already Setup in the School.";
                                        }
                                        else if (DB.ISTeachers.Where(a => a.SchoolID == Authentication.SchoolID && a.Name == TName && a.Deleted == true).ToList().Count > 0)
                                        {
                                            objM.Status = "Fail - Not Allowed. Non-Teacher Name is Already Setup with this Email in the School.";
                                        }
                                        else if (string.IsNullOrEmpty(objM.TeacherNo))
                                        {
                                            objM.Status = "Fail - Not Allowed. Teacher Number should not be blank.";
                                        }
                                        else if (string.IsNullOrEmpty(objM.Name))
                                        {
                                            objM.Status = "Fail - Not Allowed. Teacher Name should not be blank.";
                                        }
                                        else if (string.IsNullOrEmpty(TEmail))
                                        {
                                            objM.Status = "Fail - Not Allowed. Email should not be blank.";
                                        }
                                        else if (string.IsNullOrEmpty(TPhone))
                                        {
                                            objM.Status = "Fail - Not Allowed. Phone Number should not be blank.";
                                        }
                                        else if (firstClassObj == null)
                                        {
                                            objM.Status = "Fail - Not Allowed. First class must be allowed or it must valid.";
                                        }
                                        else if (string.IsNullOrEmpty(RoleName))
                                        {
                                            objM.Status = "Fail - Not Allowed. Role Name should not be blank.";
                                        }
                                        else if (userRoleObject == null)
                                        {
                                            objM.Status = "Fail - Not Allowed. Role Name not exists in the school.";
                                        }
                                        else
                                        {
                                            ISTeacher obj = new ISTeacher();
                                            obj.Title = dt.Rows[i][1].ToString();
                                            obj.TeacherNo = dt.Rows[i][2].ToString();
                                            obj.Name = dt.Rows[i][3].ToString();
                                            obj.Photo = "Upload/user.jpg";
                                            if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                            {
                                                string classtype = dt.Rows[i][4].ToString();
                                                ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.TypeID != 1 && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                                if (ctype != null)
                                                    obj.ClassID = ctype.ID;
                                                else
                                                    obj.ClassID = 0;
                                            }
                                            else
                                                obj.ClassID = 0;

                                            if (!String.IsNullOrEmpty(dt.Rows[i][6].ToString()))
                                            {
                                                RoleName = dt.Rows[i][6].ToString();
                                                ISUserRole ctype = DB.ISUserRoles.FirstOrDefault(p => p.RoleName.ToLower() == RoleName.ToLower() && p.RoleType == (int)EnumsManagement.ROLETYPE.NONTEACHING && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                                if (ctype != null)
                                                    obj.RoleID = ctype.ID;
                                                else
                                                    obj.RoleID = 0;
                                            }
                                            else
                                                obj.RoleID = 0;


                                            obj.Role = (int)EnumsManagement.ROLETYPE.NONTEACHING;
                                            obj.Email = dt.Rows[i][7].ToString();
                                            obj.PhoneNo = dt.Rows[i][8].ToString();
                                            obj.Password = EncryptionHelper.Encrypt(Password);
                                            obj.CreatedDateTime = DateTime.Now;
                                            obj.SchoolID = Authentication.SchoolID;
                                            obj.Active = true;
                                            obj.Deleted = true;
                                            obj.CreatedBy = Authentication.LogginSchool.ID;
                                            DB.ISTeachers.Add(obj);
                                            DB.SaveChanges();

                                            ISTeacherClassAssignment obj1 = new ISTeacherClassAssignment();
                                            if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                            {
                                                string classtype = dt.Rows[i][4].ToString();
                                                ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.TypeID != 1 && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                                if (ctype != null)
                                                    obj1.ClassID = ctype.ID;
                                                else
                                                    obj1.ClassID = 0;
                                            }
                                            else
                                                obj1.ClassID = 0;

                                            obj1.TeacherID = obj.ID;
                                            obj1.CreatedDateTime = DateTime.Now;
                                            obj1.Active = true;
                                            obj1.Deleted = true;
                                            obj1.CreatedBy = Authentication.SchoolID;
                                            obj1.Out = 0;
                                            obj1.Outbit = false;
                                            DB.ISTeacherClassAssignments.Add(obj1);
                                            DB.SaveChanges();
                                            if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                            {
                                                ISTeacherClassAssignment obj2 = new ISTeacherClassAssignment();
                                                if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                                {
                                                    string classtype = dt.Rows[i][5].ToString();
                                                    ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.TypeID != 1 && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                                    if (ctype != null)
                                                        obj2.ClassID = ctype.ID;
                                                    else
                                                        obj2.ClassID = 0;
                                                }
                                                else
                                                    obj2.ClassID = 0;
                                                obj2.TeacherID = obj.ID;
                                                obj2.CreatedDateTime = DateTime.Now;
                                                obj2.Active = true;
                                                obj2.Deleted = true;
                                                obj2.CreatedBy = Authentication.LogginSchool.ID;
                                                obj2.Out = 0;
                                                obj2.Outbit = false;
                                                DB.ISTeacherClassAssignments.Add(obj2);
                                                DB.SaveChanges();
                                            }
                                            objM.Status = "Success";
                                            CreateCount = CreateCount + 1;
                                            if (!String.IsNullOrEmpty(dt.Rows[i][7].ToString()))
                                            {
                                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", TName, TEmail, Password, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                                                _EmailManagement.SendEmail(dt.Rows[i][7].ToString(), "Your Account Credentials", Message);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objM.Command = dt.Rows[i][0].ToString();
                                        string ID = dt.Rows[i][2].ToString() != "" ? Convert.ToString(dt.Rows[i][2].ToString()) : "";
                                        if (ID != "")
                                        {
                                            ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.TeacherNo == ID && p.SchoolID == Authentication.SchoolID && p.Deleted == true && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.SchoolID == Authentication.SchoolID);
                                            if (DB.ISTeachers.Where(a => a.ID != ObjTeacher.ID && a.Email == TEmail && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Not Allowed. Non-Teacher Email Address is Already Setup in the School.";
                                            }
                                            else if (DB.ISTeachers.Where(a => a.ID != ObjTeacher.ID && a.PhoneNo == TPhone && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Not Allowed. Non-Teacher Phone number is Already Setup in the School.";
                                            }
                                            else if (DB.ISTeachers.Where(a => a.ID != ObjTeacher.ID && a.TeacherNo == TNo && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Not Allowed. Non-Teacher Number is Already Setup in the School.";
                                            }
                                            else if (DB.ISTeachers.Where(a => a.ID != ObjTeacher.ID && a.Name == TName && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                                            {
                                                objM.Status = "Fail - Not Allowed. Non-Teacher Name is Already Setup with this Email in the School.";
                                            }
                                            else if (string.IsNullOrEmpty(objM.TeacherNo))
                                            {
                                                objM.Status = "Fail - Not Allowed. Teacher Number should not be blank.";
                                            }
                                            else if (string.IsNullOrEmpty(objM.Name))
                                            {
                                                objM.Status = "Fail - Not Allowed. Teacher Name should not be blank.";
                                            }
                                            else if (string.IsNullOrEmpty(TEmail))
                                            {
                                                objM.Status = "Fail - Not Allowed. Email should not be blank.";
                                            }
                                            else if (string.IsNullOrEmpty(TPhone))
                                            {
                                                objM.Status = "Fail - Not Allowed. Phone Number should not be blank.";
                                            }
                                            else if (firstClassObj == null)
                                            {
                                                objM.Status = "Fail - Not Allowed. First class must be allowed or it must valid.";
                                            }
                                            else if (string.IsNullOrEmpty(RoleName))
                                            {
                                                objM.Status = "Fail - Not Allowed. Role Name should not be blank.";
                                            }
                                            else if (userRoleObject == null)
                                            {
                                                objM.Status = "Fail - Not Allowed. Role Name not exists in the school.";
                                            }
                                            else
                                            {
                                                ISTeacher obj = DB.ISTeachers.SingleOrDefault(p => p.TeacherNo == ID && p.SchoolID == Authentication.SchoolID && p.Role == (int)EnumsManagement.ROLETYPE.NONTEACHING && p.SchoolID == Authentication.SchoolID);
                                                if (obj != null)
                                                {
                                                    var item = dt.Rows[i][1].ToString();
                                                    obj.Title = dt.Rows[i][1].ToString();
                                                    obj.TeacherNo = dt.Rows[i][2].ToString();
                                                    obj.Name = dt.Rows[i][3].ToString();

                                                    if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                                    {
                                                        string classtype = dt.Rows[i][4].ToString();
                                                        ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.TypeID != 1 && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                                        if (ctype != null)
                                                            obj.ClassID = ctype.ID;
                                                        else
                                                            obj.ClassID = 0;
                                                    }
                                                    else
                                                        obj.ClassID = 0;

                                                    if (!String.IsNullOrEmpty(dt.Rows[i][6].ToString()))
                                                    {
                                                        RoleName = dt.Rows[i][6].ToString();
                                                        ISUserRole ctype = DB.ISUserRoles.FirstOrDefault(p => p.RoleName.ToLower() == RoleName.ToLower() && p.RoleType == (int)EnumsManagement.ROLETYPE.NONTEACHING && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                                        if (ctype != null)
                                                            obj.RoleID = ctype.ID;
                                                        else
                                                            obj.RoleID = 0;
                                                    }
                                                    else
                                                        obj.RoleID = 0;

                                                    obj.Email = dt.Rows[i][7].ToString();
                                                    obj.PhoneNo = dt.Rows[i][8].ToString();
                                                    obj.ModifyBy = Authentication.SchoolID;
                                                    obj.ModifyDateTime = DateTime.Now;
                                                    obj.SchoolID = Authentication.SchoolID;
                                                    DB.SaveChanges();
                                                    //Remove Range
                                                    List<ISTeacherClassAssignment> objList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == obj.ID && p.Active == true).ToList();
                                                    DB.ISTeacherClassAssignments.RemoveRange(objList);
                                                    DB.SaveChanges();

                                                    ISTeacherClassAssignment obj1 = new ISTeacherClassAssignment();
                                                    if (!String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                                                    {
                                                        string classtype = dt.Rows[i][4].ToString();
                                                        ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.TypeID != 1 && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                                        if (ctype != null)
                                                            obj1.ClassID = ctype.ID;
                                                        else
                                                            obj1.ClassID = 0;
                                                    }
                                                    else
                                                        obj1.ClassID = 0;
                                                    obj1.TeacherID = obj.ID;
                                                    obj1.CreatedDateTime = DateTime.Now;
                                                    obj1.Active = true;
                                                    obj1.Deleted = true;
                                                    obj1.CreatedBy = Authentication.SchoolID;
                                                    obj1.Out = 0;
                                                    obj1.Outbit = false;
                                                    DB.ISTeacherClassAssignments.Add(obj1);
                                                    DB.SaveChanges();
                                                    if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                                    {
                                                        ISTeacherClassAssignment obj2 = new ISTeacherClassAssignment();
                                                        if (!String.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                                                        {
                                                            string classtype = dt.Rows[i][5].ToString();
                                                            ISClass ctype = DB.ISClasses.FirstOrDefault(p => p.Name == classtype && p.TypeID != 1 && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
                                                            if (ctype != null)
                                                                obj2.ClassID = ctype.ID;
                                                            else
                                                                obj2.ClassID = 0;
                                                        }
                                                        else
                                                            obj2.ClassID = 0;
                                                        obj2.TeacherID = obj.ID;
                                                        obj2.CreatedDateTime = DateTime.Now;
                                                        obj2.Active = true;
                                                        obj2.Deleted = true;
                                                        obj2.CreatedBy = Authentication.SchoolID;
                                                        obj2.Out = 0;
                                                        obj2.Outbit = false;
                                                        DB.ISTeacherClassAssignments.Add(obj2);
                                                        DB.SaveChanges();
                                                    }
                                                    objM.Status = "Success";
                                                    UpdateCount = UpdateCount + 1;
                                                    if (!String.IsNullOrEmpty(TEmail))
                                                    {
                                                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Your Profile has been Changed Successfully SO if you want to check then Login to School Application.<br /><br>Thanks,<br>School APP<br> {1}<br>Mobile No : {2} <br>Email id : {3}<br>", TName, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                                                        _EmailManagement.SendEmail(TEmail, "Profile Changes", Message);
                                                    }
                                                }
                                                else
                                                {
                                                    objM.Status = "Fail";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            objM.Status = "Fail";
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                    objM.Status = "Fail";
                                    ErrorLogManagement.AddLog(ex);
                                }
                                objLists.Add(objM);
                            }
                            ISDataUploadHistory ObjHistory = new ISDataUploadHistory();
                            ObjHistory.SchoolID = Authentication.SchoolID;
                            ObjHistory.TemplateName = "Teacher Template";
                            ObjHistory.CreatedCount = CreateCount;
                            ObjHistory.UpdatedCount = UpdateCount;
                            ObjHistory.Date = DateTime.Now;
                            ObjHistory.Active = true;
                            ObjHistory.Deleted = true;
                            ObjHistory.CreatedBy = Authentication.SchoolID;
                            ObjHistory.CreatedByType = (int)EnumsManagement.CREATEBYTYPE.School;
                            ObjHistory.CreatedDateTime = DateTime.Now;
                            DB.ISDataUploadHistories.Add(ObjHistory);
                            DB.SaveChanges();
                        }

                        message.AppendLine("Teacher Imported Successfully. Document Category : BulkData");
                        objLists.ForEach(r => message.AppendLine($"{r.Name}\t{r.Status}"));



                        LogManagement.AddLog(message.ToString(), "BulkData");
                        AlertMessageManagement.ServerMessage(Page, "Teacher Imported Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                        EmailManage("Teacher", file_name);

                        Clear();
                        CreateSpreadsheetWorkbookss(Server.MapPath("~/Upload") + "/ImportedTeacherStatusList" + CommonOperation.GenerateRandom() + ".xlsx", objLists);
                    }
                }
            }
            catch (Exception ex)
            {
                //AccessConnection.Close();
                ErrorLogManagement.AddLog(ex);
                AlertMessageManagement.ServerMessage(Page, "This is catch block failed upload", (int)AlertMessageManagement.MESSAGETYPE.Error);
            }
        }
        public bool IsValidateStudentRecordnotBlank(MISStudentBulk objM)
        {
            objM.Status = "";

            if (string.IsNullOrEmpty(objM.StudentNo))
                objM.Status = "Fail - Student Number should not be blank.";
            else if (string.IsNullOrEmpty(objM.StudentName))
                objM.Status = "Fail - Student Name should not be blank.";
            else if (string.IsNullOrEmpty(objM.ClassName))
                objM.Status = "Fail - Class should not be blank.";
            else if (string.IsNullOrEmpty(objM.ParantName1))
                objM.Status = "Fail - Primary parent name should not be blank.";
            else if (string.IsNullOrEmpty(objM.ParantEmail1))
                objM.Status = "Fail - Primary parent email should not be blank.";
            else if (string.IsNullOrEmpty(objM.ParantRelation1))
                objM.Status = "Fail - Primary parent relation should not be blank.";
            else if (string.IsNullOrEmpty(objM.ParantPhone1))
                objM.Status = "Fail - Primary parent phone number should not be blank.";
            else
            {
                bool isAnyValueFound = !string.IsNullOrEmpty(objM.ParantName2) ||
                    !string.IsNullOrEmpty(objM.ParantEmail2) ||
                    !string.IsNullOrEmpty(objM.ParantRelation2) ||
                    !string.IsNullOrEmpty(objM.ParantPhone2);

                if (isAnyValueFound)
                {
                    if (string.IsNullOrEmpty(objM.ParantName2))
                        objM.Status = "Fail - Secondary parent name should not be blank.";
                    else if (string.IsNullOrEmpty(objM.ParantEmail2))
                        objM.Status = "Fail - Secondary parent email should not be blank.";
                    else if (string.IsNullOrEmpty(objM.ParantRelation2))
                        objM.Status = "Fail - Secondary parent relation should not be blank.";
                    else if (string.IsNullOrEmpty(objM.ParantPhone2))
                        objM.Status = "Fail - Secondary parent phone number should not be blank.";
                }

            }
            return !string.IsNullOrEmpty(objM.Status);
        }
        public void EmailManage(string ObjectName, string TemplateName)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;
            int SchoolID = Convert.ToInt32(drpSchool.SelectedValue);
            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == SchoolID);
            string LoggedINName = _School.AdminFirstName + " " + _School.AdminLastName;
            string tblSupbody = string.Empty;
            string tblAdminBody = string.Empty;
            tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.SupervisorFirstname + ' ' + _School.SupervisorLastname + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    Data Bulk Upload activity was carried out by  : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Upload Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Upload Type : Create / Edit " + ObjectName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Upload Template Used : " + TemplateName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact<b>" + LoggedINName + @"</b> should you require more information.
                                   </td>
                                </tr></table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                SuperwisorBody += reader.ReadToEnd();
            }
            SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    Data Bulk Upload activity was carried out by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Upload Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Upload Type : Create / Edit " + ObjectName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Upload Template Used : " + TemplateName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact<b>" + LoggedINName + @"</b> should you require more information.
                                   </td>
                                </tr>
                                </table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Bulk Upload Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Bulk Upload Notification", SuperwisorBody);
        }
        private void Clear()
        {
            drpSchool.SelectedValue = "0";
            FUTemplate.Dispose();
            rbtnStudent.Checked = true;
            rbtnTeacher.Checked = false;
            rbtnNonTeacher.Checked = false;
            rbtnClass.Checked = false;
            PanelClass.Visible = true;
            PanelClass1.Visible = true;
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
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void CreateSpreadsheetWorkbook(string filepath, List<MISStudentBulk> list)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;

                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("ImportedStudentStatusList").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Command");
                CreateCell(Hederrow, "StudentNo");
                CreateCell(Hederrow, "Name");
                CreateCell(Hederrow, "Class");
                CreateCell(Hederrow, "PrimaryParentName");
                CreateCell(Hederrow, "PrimaryparentEmail ");
                CreateCell(Hederrow, "Primaryparentphonenumber");
                CreateCell(Hederrow, "Primaryparentrelation");
                CreateCell(Hederrow, "SecondaryParentName");
                CreateCell(Hederrow, "SecondaryparentEmail");
                CreateCell(Hederrow, "Secondaryparentphonenumber");
                CreateCell(Hederrow, "Secondaryparentrelation");
                CreateCell(Hederrow, "Status");
                int Total = 0;
                foreach (var item in list)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, item.Command);
                    CreateCell(row, item.StudentNo);
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.ClassName);
                    CreateCell(row, item.ParantName1);
                    CreateCell(row, item.ParantEmail1);
                    CreateCell(row, item.ParantPhone1);
                    CreateCell(row, item.ParantRelation1);
                    CreateCell(row, item.ParantName2);
                    CreateCell(row, item.ParantEmail2);
                    CreateCell(row, item.ParantPhone2);
                    CreateCell(row, item.ParantRelation2);
                    CreateCell(row, item.Status);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=ImportedStudentStatusList.xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void CreateSpreadsheetWorkbooks(string filepath, List<MISClassBulk> list)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;

                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("ImportedClassStatusList").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Command");
                CreateCell(Hederrow, "ClassName");
                CreateCell(Hederrow, "ClassYear");
                CreateCell(Hederrow, "ClassType");
                CreateCell(Hederrow, "AfterSchoolType");
                CreateCell(Hederrow, "AfterSchoolExternalOrganisation ");
                CreateCell(Hederrow, "Status");
                int Total = 0;
                foreach (var item in list)
                {
                    int Years = item.Year != "" ? Convert.ToInt32(item.Year) : 0;
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, item.Command);
                    CreateCell(row, item.Name);
                    CreateCell(row, item.Year != "" ? DB.ISClassYears.SingleOrDefault(p => p.ID == Years).Year : "");
                    CreateCell(row, item.TypeID != 0 ? DB.ISClassTypes.SingleOrDefault(p => p.ID == item.TypeID).Name : "");
                    CreateCell(row, item.AfterSchoolType);
                    CreateCell(row, item.ExternalOrganisation);
                    CreateCell(row, item.Status);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=ImportedClassStatusList.xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.Close();
                Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void CreateSpreadsheetWorkbookss(string filepath, List<MISTeacherBulk> list)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;

                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("ImportedTeacherStatusList").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Command");
                CreateCell(Hederrow, "TeacherTitle");
                CreateCell(Hederrow, "TeacherNo");
                CreateCell(Hederrow, "Name");
                CreateCell(Hederrow, "1stClass");
                CreateCell(Hederrow, "2ndClass ");
                CreateCell(Hederrow, "Role");
                CreateCell(Hederrow, "Email");
                CreateCell(Hederrow, "PhoneNumber");
                CreateCell(Hederrow, "Status");
                foreach (var item in list)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, item.Command);
                    CreateCell(row, item.Title);
                    CreateCell(row, item.TeacherNo);
                    CreateCell(row, item.Name);
                    CreateCell(row, item.FirstClass);
                    CreateCell(row, item.SecondClass);
                    CreateCell(row, item.RoleName);
                    CreateCell(row, item.Email);
                    CreateCell(row, item.PhoneNo);
                    CreateCell(row, item.Status);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=ImportedTeacherStatusList.xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void drpSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSchool.SelectedValue != "0")
            {
                int ID = Convert.ToInt32(drpSchool.SelectedValue);
                ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                if (_School != null)
                {
                    if (_School.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                    {
                        PanelClass.Visible = false;
                        PanelClass1.Visible = false;
                    }
                    else
                    {
                        PanelClass.Visible = true;
                        PanelClass1.Visible = true;
                    }
                }
            }
            else
            {
                PanelClass.Visible = true;
                PanelClass1.Visible = true;
            }
        }

        protected void lnkMasterClick_Click(object sender, EventArgs e)
        {
            try
            {
                CreateMasterWorkbook(Server.MapPath("~/Upload") + "/MasterDataSheet" + CommonOperation.GenerateRandom() + ".xlsx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void lnkStudentExportReports_Click(object sender, EventArgs e)
        {
            try
            {
                CreateMasterWorkbook(Server.MapPath("~/Upload") + "/MasterDataSheet" + CommonOperation.GenerateRandom() + ".xlsx",ReportTemplateType.STUDENT);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        protected void lnkTeacherReportClick_Click(object sender, EventArgs e)
        {
            try
            {
                CreateMasterWorkbook(Server.MapPath("~/Upload") + "/MasterDataSheet" + CommonOperation.GenerateRandom() + ".xlsx", ReportTemplateType.TEACHER);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        protected void lnkNonTeacherReportClick_Click(object sender, EventArgs e)
        {
            try
            {
                CreateMasterWorkbook(Server.MapPath("~/Upload") + "/MasterDataSheet" + CommonOperation.GenerateRandom() + ".xlsx", ReportTemplateType.NONTEACHER);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        
        protected void lnkClassReportClick_Click(object sender, EventArgs e)
        {
            try
            {
                CreateMasterWorkbook(Server.MapPath("~/Upload") + "/MasterDataSheet" + CommonOperation.GenerateRandom() + ".xlsx", ReportTemplateType.CLASS);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }


        private void CreateMasterWorkbook(string filepath, ReportTemplateType templateType)
        {
            TeacherManagement _TeacherManagement = new TeacherManagement();
            ClassManagement _ClassManagement = new ClassManagement();
            StudentManagement _StudentManagement = new StudentManagement();
            Random generator = new Random();
            int SchoolID = Convert.ToInt32(drpSchool.SelectedValue);
            int randomNumber = generator.Next(100000, 1000000);
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                if (templateType == ReportTemplateType.CLASS)
                {
                    // Class Names
                    List<MISClass> _ClassList = _ClassManagement.ClassListByFilter(SchoolID, "", 0, "0");
                    WorksheetPart ClassworksheetParts = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData ClasssheetDatas = new SheetData();
                    ClassworksheetParts.Worksheet = new Worksheet(ClasssheetDatas);

                    Sheet Classsheetss = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(ClassworksheetParts), SheetId = 1, Name = ("Classes").ToString() };
                    sheets.Append(Classsheetss);

                    Hederrow = new Row();
                    ClasssheetDatas.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "ClassName");
                    CreateCell(Hederrow, "Year");
                    CreateCell(Hederrow, "ClassType");
                    CreateCell(Hederrow, "AfterSchoolType");
                    CreateCell(Hederrow, "ExternalOrganisation");
                    CreateCell(Hederrow, "Status");
                    CreateCell(Hederrow, "CreatedBy");
                    CreateCell(Hederrow, "CreatedDate");
                    int ClassTotal = 0;
                    foreach (var item in _ClassList)
                    {
                        Row row = new Row();
                        ClasssheetDatas.Append(row);
                        CreateCell(row, (ClassTotal += 1).ToString());
                        CreateCell(row, item.Name);
                        CreateCell(row, item.YearName);
                        CreateCell(row, item.ClassType);
                        CreateCell(row, item.TypeID == (int?)EnumsManagement.CLASSTYPE.AfterSchool ? item.AfterSchoolType : "");
                        CreateCell(row, item.TypeID == (int?)EnumsManagement.CLASSTYPE.AfterSchool ? item.ExternalOrganisation : "");
                        CreateCell(row, item.Status);
                        CreateCell(row, item.CreateBy);
                        CreateCell(row, item.CreatedDateTime.Value.ToString("dd/MM/yyyy"));
                    }
                    // Over Class Names
                }
                else if (templateType == ReportTemplateType.TEACHER)
                {
                    // Teacher
                    List<MISTeacher> list = _TeacherManagement.TeacherList(SchoolID, "", 0, "", "ASC", "", 0, "0");
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("Teachers").ToString() };
                    sheets.Append(sheet);

                    Hederrow = new Row();
                    sheetData.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "TeacherNo");
                    CreateCell(Hederrow, "Title");
                    CreateCell(Hederrow, "Name");
                    CreateCell(Hederrow, "Role");
                    CreateCell(Hederrow, "Phone");
                    CreateCell(Hederrow, "Email");
                    CreateCell(Hederrow, "AssignedClasses");
                    CreateCell(Hederrow, "Status");
                    CreateCell(Hederrow, "CreatedBy");
                    CreateCell(Hederrow, "CreatedDate");
                    int Total = 0;
                    foreach (var item in list)
                    {
                        Row row = new Row();
                        sheetData.Append(row);
                        CreateCell(row, (Total += 1).ToString());
                        CreateCell(row, item.TeacherNo);
                        CreateCell(row, item.Title);
                        CreateCell(row, item.Name);
                        CreateCell(row, item.RoleName);
                        CreateCell(row, item.PhoneNo);
                        CreateCell(row, item.Email);
                        CreateCell(row, item.AssignedClass);
                        CreateCell(row, item.Status);
                        CreateCell(row, item.CreateBy);
                        CreateCell(row, item.CreatedDateTime.Value.ToString("dd/MM/yyyy"));
                    }
                    // Over Teacher
                }
                else if (templateType == ReportTemplateType.NONTEACHER)
                {
                    // NonTeacher
                    List<MISTeacher> list = _TeacherManagement.NonTeacherList(SchoolID, "", "ASC", "");
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("NonTeachers").ToString() };
                    sheets.Append(sheet);

                    Hederrow = new Row();
                    sheetData.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "TeacherNo");
                    CreateCell(Hederrow, "Title");
                    CreateCell(Hederrow, "Name");
                    CreateCell(Hederrow, "Role");
                    CreateCell(Hederrow, "Phone");
                    CreateCell(Hederrow, "Email");
                    CreateCell(Hederrow, "AssignedClasses");
                    CreateCell(Hederrow, "Status");
                    CreateCell(Hederrow, "CreatedBy");
                    CreateCell(Hederrow, "CreatedDate");
                    int Total = 0;
                    foreach (var item in list)
                    {
                        Row row = new Row();
                        sheetData.Append(row);
                        CreateCell(row, (Total += 1).ToString());
                        CreateCell(row, item.TeacherNo);
                        CreateCell(row, item.Title);
                        CreateCell(row, item.Name);
                        CreateCell(row, item.RoleName);
                        CreateCell(row, item.PhoneNo);
                        CreateCell(row, item.Email);
                        CreateCell(row, item.ClassName1 + (!String.IsNullOrEmpty(item.ClassName2) ? ", " + item.ClassName2 : ""));
                        CreateCell(row, item.Status);
                        CreateCell(row, item.CreateBy);
                        CreateCell(row, item.CreatedDateTime.Value.ToString("dd/MM/yyyy"));
                    }
                    // Over NonTeacher
                }
                else if (templateType == ReportTemplateType.STUDENT)
                {
                    // Student
                    List<MISStudent> list = new List<MISStudent>();
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        list = _ClassManagement.StudentFullListByClass(SchoolID, 0);
                    }
                    else
                    {
                        List<ISClass> _ClassList = DB.ISClasses.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList();
                        foreach (var items in _ClassList)
                        {
                            if (items.Name.Contains("Outside"))
                            {
                                list.AddRange(_ClassManagement.StudentFullListByExtClass(SchoolID, items.ID));
                            }
                            else
                            {
                                list.AddRange(_ClassManagement.StudentFullListByClass(SchoolID, items.ID));
                            }
                        }
                    }
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("Students").ToString() };
                    sheets.Append(sheet);

                    Hederrow = new Row();
                    sheetData.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "StudentNo");
                    CreateCell(Hederrow, "Name");
                    CreateCell(Hederrow, "ClassName");
                    CreateCell(Hederrow, "PrimaryParentName");
                    CreateCell(Hederrow, "PrimaryParentEmail");
                    CreateCell(Hederrow, "PrimaryParentPhone");
                    CreateCell(Hederrow, "PrimaryParentRelation");
                    CreateCell(Hederrow, "SecondaryParentName");
                    CreateCell(Hederrow, "SecondaryParentEmail");
                    CreateCell(Hederrow, "SecondaryParentPhone");
                    CreateCell(Hederrow, "SecondaryParentRelation");
                    CreateCell(Hederrow, "EmailAfterConfirmPickUp");
                    CreateCell(Hederrow, "EmailAfterConfirmAttendance");
                    int Total = 0;
                    foreach (var item in list)
                    {
                        Row row = new Row();
                        sheetData.Append(row);
                        CreateCell(row, (Total += 1).ToString());
                        CreateCell(row, item.StudentNo);
                        CreateCell(row, item.StudentName);
                        CreateCell(row, item.ClassName);
                        CreateCell(row, item.ParantName1);
                        CreateCell(row, item.ParantEmail1);
                        CreateCell(row, item.ParantPhone1);
                        CreateCell(row, item.ParantRelation1);
                        CreateCell(row, item.ParantName2);
                        CreateCell(row, item.ParantEmail2);
                        CreateCell(row, item.ParantPhone2);
                        CreateCell(row, item.ParantRelation2);
                        CreateCell(row, item.EmailAfterConfirmPickUp == true ? "Yes" : "No");
                        CreateCell(row, item.EmailAfterConfirmAttendence == true ? "Yes" : "No");
                    }
                    // Over Student
                }
                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=ExportTemplateReport" + randomNumber.ToString() + ".xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        private void CreateMasterWorkbook(string filepath)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                int SchoolID = Convert.ToInt32(drpSchool.SelectedValue);
                ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                //////////////////Adding Sheet here
                Row Hederrow;
                if (_School.TypeID == (int?)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    // Class Type Data
                    List<ISClassType> list = DB.ISClassTypes.Where(p => p.Deleted == true).ToList();
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("ClassType").ToString() };
                    sheets.Append(sheet);

                    Hederrow = new Row();
                    sheetData.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "Class Types");
                    int Total = 0;
                    foreach (var item in list)
                    {
                        Row row = new Row();
                        sheetData.Append(row);
                        CreateCell(row, (Total += 1).ToString());
                        CreateCell(row, item.Name);
                    }
                    // Over Class Type
                }
                if (_School.TypeID == (int?)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    // Class Year
                    List<ISClassYear> yearlist = DB.ISClassYears.Where(p => p.Deleted == true).ToList();
                    WorksheetPart worksheetParts = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetDatas = new SheetData();
                    worksheetParts.Worksheet = new Worksheet(sheetDatas);

                    Sheet sheetss = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetParts), SheetId = 1, Name = ("ClassYear").ToString() };
                    sheets.Append(sheetss);

                    Hederrow = new Row();
                    sheetDatas.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "Class Year");
                    int YearTotal = 0;
                    foreach (var item in yearlist)
                    {
                        Row row = new Row();
                        sheetDatas.Append(row);
                        CreateCell(row, (YearTotal += 1).ToString());
                        CreateCell(row, item.Year);
                    }
                    // Over Class Year
                }
                if (_School.TypeID == (int?)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    // AfterSchool Type
                    List<MISAfterSchoolType> _AfterTypes = new List<MISAfterSchoolType>{
                   new MISAfterSchoolType{ID = 1, AfterSchoolType = "Internal"},
                   new MISAfterSchoolType{ID = 2, AfterSchoolType = "External"}
                   };
                    WorksheetPart AfterworksheetParts = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData AftersheetDatas = new SheetData();
                    AfterworksheetParts.Worksheet = new Worksheet(AftersheetDatas);

                    Sheet Aftersheetss = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(AfterworksheetParts), SheetId = 1, Name = ("AfterSchoolType").ToString() };
                    sheets.Append(Aftersheetss);

                    Hederrow = new Row();
                    AftersheetDatas.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "AfterSchoolType");
                    int AfterTotal = 0;
                    foreach (var item in _AfterTypes)
                    {
                        Row row = new Row();
                        AftersheetDatas.Append(row);
                        CreateCell(row, (AfterTotal += 1).ToString());
                        CreateCell(row, item.AfterSchoolType);
                    }
                    // Over After School Type
                }
                // Teacher Title
                List<MISTeacherTitle> _Title = new List<MISTeacherTitle>{
                   new MISTeacherTitle{ID = 1, Title = "Mr."},
                   new MISTeacherTitle{ID = 2, Title = "Master."},
                   new MISTeacherTitle{ID = 3, Title = "Dr."},
                   new MISTeacherTitle{ID = 4, Title = "Mrs."},
                   new MISTeacherTitle{ID = 5, Title = "Miss."}
                   };
                WorksheetPart TitleworksheetParts = workbookpart.AddNewPart<WorksheetPart>();
                SheetData TitlesheetDatas = new SheetData();
                TitleworksheetParts.Worksheet = new Worksheet(TitlesheetDatas);

                Sheet Titlesheetss = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(TitleworksheetParts), SheetId = 1, Name = ("Teacher Title").ToString() };
                sheets.Append(Titlesheetss);

                Hederrow = new Row();
                TitlesheetDatas.Append(Hederrow);
                CreateCell(Hederrow, "Sr.No.");
                CreateCell(Hederrow, "Teacher Title");
                int TitleTotal = 0;
                foreach (var item in _Title)
                {
                    Row row = new Row();
                    TitlesheetDatas.Append(row);
                    CreateCell(row, (TitleTotal += 1).ToString());
                    CreateCell(row, item.Title);
                }
                // Over Teacher Title

                // Class Names
                List<ISClass> _ClassList = DB.ISClasses.Where(p => p.SchoolID == SchoolID && p.Active == true && p.Deleted == true).ToList();
                WorksheetPart ClassworksheetParts = workbookpart.AddNewPart<WorksheetPart>();
                SheetData ClasssheetDatas = new SheetData();
                ClassworksheetParts.Worksheet = new Worksheet(ClasssheetDatas);

                Sheet Classsheetss = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(ClassworksheetParts), SheetId = 1, Name = ("Class List").ToString() };
                sheets.Append(Classsheetss);

                Hederrow = new Row();
                ClasssheetDatas.Append(Hederrow);
                CreateCell(Hederrow, "Sr.No.");
                CreateCell(Hederrow, "Class Name");
                int ClassTotal = 0;
                foreach (var item in _ClassList)
                {
                    Row row = new Row();
                    ClasssheetDatas.Append(row);
                    CreateCell(row, (ClassTotal += 1).ToString());
                    CreateCell(row, item.Name);
                }
                // Over Class Names
                // Student Relation Ships
                List<MISRelation> _Relation = new List<MISRelation>{
                   new MISRelation{ID = 1, Relation = "Mother"},
                   new MISRelation{ID = 2, Relation = "Father"},
                   new MISRelation{ID = 3, Relation = "Guardian"},
                   //new MISRelation{ID = 4, Relation = "Friends"}
                   };
                WorksheetPart RelworksheetParts = workbookpart.AddNewPart<WorksheetPart>();
                SheetData RelsheetDatas = new SheetData();
                RelworksheetParts.Worksheet = new Worksheet(RelsheetDatas);

                Sheet Relsheetss = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(RelworksheetParts), SheetId = 1, Name = ("Parent RelationShip").ToString() };
                sheets.Append(Relsheetss);

                Hederrow = new Row();
                RelsheetDatas.Append(Hederrow);
                CreateCell(Hederrow, "Sr.No.");
                CreateCell(Hederrow, "RelationShip");
                int RelTotal = 0;
                foreach (var item in _Relation)
                {
                    Row row = new Row();
                    RelsheetDatas.Append(row);
                    CreateCell(row, (RelTotal += 1).ToString());
                    CreateCell(row, item.Relation);
                }
                // Over Student Relation Ships

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=MasterDataSheet.xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }

    public enum ReportTemplateType
    {
        CLASS = 1,
        TEACHER,
        NONTEACHER,
        STUDENT
    }
}