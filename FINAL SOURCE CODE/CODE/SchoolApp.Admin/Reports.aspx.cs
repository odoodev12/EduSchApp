using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISUserLogin())
            {
                if (!Authentication.ISOrgUserLogin())
                {
                    Response.Redirect(Authentication.AuthorizePage());
                }
            }
            if (!IsPostBack)
            {

                BindSchoolData();
            }
        }

        private void BindSchoolData()
        {
            using (SchoolAppEntities db = new SchoolAppEntities())
            {
                List<ISSchool> schoolList = db.ISSchools.Where(r => r.Active == true).ToList();

                drpSchoolList.DataSource = schoolList;
                drpSchoolList.DataTextField = "Name";
                drpSchoolList.DataValueField = "ID";
                drpSchoolList.DataBind();
                drpSchoolList.Items.Insert(0, new ListItem { Text = "Select School Name", Value = "0" });
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int reportTypeId = Convert.ToInt32(drpReportType.SelectedValue);

            if (reportTypeId == 1)
            {
                var result = GetStudentDeletedReport();
                tablediv.InnerHtml = result.Value;
            }
            else if (reportTypeId == 2)
            {
                var result = GetParentDeletedReport();
                tablediv.InnerHtml = result.Value;
            }
            else if (reportTypeId == 3)
            {
                var result = GetDeletedPickerReport();
                tablediv.InnerHtml = result.Value;
            }
            else
                tablediv.InnerHtml = "";

            //tablediv.Controls.Add(myDiv);

        }

        private KeyValuePair<List<MISStudent>, string> GetStudentDeletedReport(bool isHtmlRequired = true)
        {
            int schholId = Convert.ToInt32(drpSchoolList.SelectedValue);
            StringBuilder stringBuilder = new StringBuilder();
            if (schholId == 0)
                return new KeyValuePair<List<MISStudent>, string>(new List<MISStudent>(), "");

            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISStudent> ObjList = (from item in DB.ISStudents.Where(p => p.SchoolID == schholId && p.Deleted == false && p.Active == false).ToList()
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            StudentName = item.StudentName,
                                            StudentNo = item.StudentNo,
                                            ParantEmail1 = item.ParantEmail1,
                                            ParantEmail2 = item.ParantEmail2,
                                            ParantPhone1 = item.ParantPhone1,
                                            ParantPhone2 = item.ParantPhone2,
                                            ClassID = item.ClassID,
                                            ClassName = item.ClassID != null ? item.ISClass.Name : "",
                                            CreatedDateTime = item.CreatedDateTime,
                                            CreateDate = item.CreatedDateTime != null ? item.CreatedDateTime.Value.ToString("dd/MM/yyyy") : "",
                                        }).ToList();

            if (isHtmlRequired)
            {
                string rowTemplate = @"<tr>
                                                    <td style=/""border-bottom: 1px solid #ddd !important;/"">@studentName</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@ClassName</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@ParantEmail1</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@ParantPhone1</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@ParantEmail2</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@ParantPhone2</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@DeletedDate</td>
                                                </tr>";


                stringBuilder.AppendLine(@"<table class=/""table/"" border=/""1/"">");
                stringBuilder.AppendLine(@"<thead style=/""background: #232323; color: white;/"">
                                        <tr>
                                            <th> Student Name </ th >
                                            <th> Class </ th >
                                            <th> Primary Parent Email </ th >
                                            <th> Primary Parent Number </ th >
                                            <th> Secondary Parent Email </ th >
                                            <th> Secondary Parent Number </ th >
                                            <th> Deleted Date </th>
                                        </tr>
                                    </thead>");

                stringBuilder.AppendLine(@"<tbody>");

                foreach (var student in ObjList)
                {
                    string currentRow = rowTemplate;
                    currentRow = currentRow.Replace("@studentName", student.StudentName);
                    currentRow = currentRow.Replace("@ClassName", student.ClassName);
                    currentRow = currentRow.Replace("@ParantEmail1", student.ParantEmail1);
                    currentRow = currentRow.Replace("@ParantPhone1", student.ParantPhone1);
                    currentRow = currentRow.Replace("@ParantEmail2", student.ParantEmail2);
                    currentRow = currentRow.Replace("@ParantPhone2", student.ParantPhone2);
                    currentRow = currentRow.Replace("@DeletedDate", student.DeletedDateTime.ToString());
                    stringBuilder.AppendLine(currentRow);
                }

                stringBuilder.AppendLine(@"</tbody></table>");
            }
            return new KeyValuePair<List<MISStudent>, string>(ObjList, stringBuilder.ToString());
        }

        private KeyValuePair<List<MISStudent>, string> GetParentDeletedReport(bool isHtmlRequired = true)
        {
            int schholId = Convert.ToInt32(drpSchoolList.SelectedValue);
            StringBuilder stringBuilder = new StringBuilder();
            if (schholId == 0)
                return new KeyValuePair<List<MISStudent>, string>(new List<MISStudent>(), "");

            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISStudent> ObjList = (from item in DB.BckupParents.Where(p => p.SchoolID.Value == schholId).ToList()
                                        select new MISStudent
                                        {
                                            ID = item.ParentId,
                                            StudentId = item.StudentId.Value,
                                            ParentName = item.Name,
                                            ParentPhone = item.Phone,
                                            ParentRelation = item.Relationship,
                                            DeletedByName = item.DeletedByName,
                                            DeletedDateTime = item.DeletedDate,
                                            ParentEmail = item.Email,

                                        }).ToList();
            if (isHtmlRequired)
            {
                string rowTemplate = @"<tr>
                                                    <td style=/""border-bottom: 1px solid #ddd !important;/"">@parentId</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@studentId</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@ParentName</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@ParentEmail</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@ParentPhone</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@ParentRelation</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@DeletedByName</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@DeletedDate</td>
                                                </tr>";

                stringBuilder.AppendLine(@"<table class=/""table/"" border=/""1/"">");
                stringBuilder.AppendLine(@"<thead style=/""background: #232323; color: white;/"">
                                        <tr>
                                            <th> Parent Id </ th >
                                            <th> Student Id </ th >
                                            <th> Parent Name </ th >
                                            <th> Parent Email </th>
                                            <th> Parent Number </ th >
                                            <th> Parent Relation </ th >
                                            <th> Deleted By Name </ th >
                                            <th> Deleted Date </th>
                                        </tr>
                                    </thead>");

                stringBuilder.AppendLine(@"<tbody>");

                foreach (var student in ObjList)
                {
                    string currentRow = rowTemplate;
                    currentRow = currentRow.Replace("@parentId", student.ID.ToString());
                    currentRow = currentRow.Replace("@studentId", student.StudentId.ToString());
                    currentRow = currentRow.Replace("@ParentName", student.ParentName);
                    currentRow = currentRow.Replace("@ParentEmail", student.ParentEmail);
                    currentRow = currentRow.Replace("@ParentPhone", student.ParentPhone);
                    currentRow = currentRow.Replace("@ParentRelation", student.ParentRelation);
                    currentRow = currentRow.Replace("@DeletedByName", student.DeletedByName);
                    currentRow = currentRow.Replace("@DeletedDate", student.DeletedDateTime.ToString());
                    stringBuilder.AppendLine(currentRow);
                }

                stringBuilder.AppendLine(@"</tbody></table>");
            }

            return new KeyValuePair<List<MISStudent>, string>(ObjList, stringBuilder.ToString());
        }

        private KeyValuePair<List<MISPICKER>, string> GetDeletedPickerReport(bool isHtmlRequired = true)
        {
            int schholId = Convert.ToInt32(drpSchoolList.SelectedValue);
            StringBuilder stringBuilder = new StringBuilder();
            if (schholId == 0)
                return new KeyValuePair<List<MISPICKER>, string>(new List<MISPICKER>(), "");

            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISPICKER> ObjList = (from item in DB.ISPickers.Where(p => p.SchoolID.Value == schholId && p.Deleted == false && p.Active == false).ToList()
                                        select new MISPICKER
                                        {
                                            ID = item.ID,
                                            FirstName = item.FirstName,
                                            LastName = item.LastName,
                                            Email = item.Email,
                                            Phone= item.Phone,
                                            StrPickerType = item.PickerType.Value == 1 ? "Individaul" : "Organization",
                                            DeletedByName = item.DeletedByName,
                                            DeletedDateTime = item.DeletedDateTime

                                        }).ToList();
            if (isHtmlRequired)
            {
                string rowTemplate = @"<tr>
                                                    <td style=/""border-bottom: 1px solid #ddd !important;/"">@PickerId</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@FirstName</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@LastName</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@Email</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@Phone</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@PickerType</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@DeletedByName</td>
                                                    <td style= /""border-bottom: 1px solid #ddd !important;/"" >@DeletedDate</td>
                                                </tr>";

                stringBuilder.AppendLine(@"<table class=/""table/"" border=/""1/"">");
                stringBuilder.AppendLine(@"<thead style=/""background: #232323; color: white;/"">
                                        <tr>
                                            <th> Picker Id </ th >
                                            <th> First Name </ th >
                                            <th> Last Name </ th >
                                            <th> Email </th>
                                            <th> Phone </ th >
                                            <th> Picker Type </ th >
                                            <th> Deleted By Name </ th >
                                            <th> Deleted Date </th>
                                        </tr>
                                    </thead>");

                stringBuilder.AppendLine(@"<tbody>");

                foreach (var picker in ObjList)
                {
                    string currentRow = rowTemplate;
                    currentRow = currentRow.Replace("@PickerId", picker.ID.ToString());
                    currentRow = currentRow.Replace("@FirstName", picker.FirstName);
                    currentRow = currentRow.Replace("@LastName", picker.LastName);
                    currentRow = currentRow.Replace("@Email", picker.Email);
                    currentRow = currentRow.Replace("@Phone", picker.Phone);
                    currentRow = currentRow.Replace("@PickerType", picker.StrPickerType);
                    currentRow = currentRow.Replace("@DeletedByName", picker.DeletedByName);
                    currentRow = currentRow.Replace("@DeletedDate", picker.DeletedDateTime.ToString());
                    stringBuilder.AppendLine(currentRow);
                }

                stringBuilder.AppendLine(@"</tbody></table>");
            }

            return new KeyValuePair<List<MISPICKER>, string>(ObjList, stringBuilder.ToString());
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            int reportTypeId = Convert.ToInt32(drpReportType.SelectedValue);
            int schholId = Convert.ToInt32(drpSchoolList.SelectedValue);
            string baseServerPath = Server.MapPath("Upload");
            string fileNameSuffix = "";
            try
            {
                if (reportTypeId == 1)
                {
                    fileNameSuffix = "DeletedStudentReport" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xlsx";
                    var result = GetStudentDeletedReport(false);

                    if (result.Key.Count > 0)
                    {
                        CreateStudentSpreadSheet(Server.MapPath("Upload") + "/" + fileNameSuffix, result.Key, fileNameSuffix);
                    }
                    else
                    {
                        AlertMessageManagement.ServerMessage(Page, "There are no records found.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }

            }
            catch (Exception ex)
            {
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
        private void CreateStudentSpreadSheet(string filepath, List<MISStudent> mISStudents, string fileNameWithSuffix)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                ///Adding Sheet here
                Row Hederrow;

                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = Path.GetFileNameWithoutExtension(fileNameWithSuffix) };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Student Name");
                CreateCell(Hederrow, "Class");
                CreateCell(Hederrow, "Primary Parent Email");
                CreateCell(Hederrow, "Primary Parent Number");
                CreateCell(Hederrow, "Secondary Parent Email");
                CreateCell(Hederrow, "Secondary Parent Number");
                CreateCell(Hederrow, "Deleted Date");
                int Total = 0;
                foreach (var item in mISStudents)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.ClassName);
                    CreateCell(row, item.ParantEmail1);
                    CreateCell(row, item.ParantPhone1);
                    CreateCell(row, item.ParantEmail2);
                    CreateCell(row, item.ParantPhone2);
                    CreateCell(row, item.DeletedDateTime.ToString());
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileNameWithSuffix);
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.End();
                //HttpContext.Current.Response.End();// 
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }


    }
}