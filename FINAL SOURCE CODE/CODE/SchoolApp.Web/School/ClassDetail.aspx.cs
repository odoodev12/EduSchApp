using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.School
{

    public partial class ClassDetail : System.Web.UI.Page
    {

        SchoolAppEntities DB = new SchoolAppEntities();
        ClassManagement objClassManagement = new ClassManagement();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin() && !Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }

            if (!IsPostBack)
            {
                if (OperationManagement.GetOperation("ID") != "")
                {
                    BindDropdown();
                    bindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                    int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                    ISClass Obj = DB.ISClasses.SingleOrDefault(p => p.ID == ID);
                    AutoCompleteTeachers.Enabled = true;
                    if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                    {
                        lbtnEdit.Visible = false;
                    }
                    if (Obj.TypeID == (int)EnumsManagement.CLASSTYPE.Office || Obj.TypeID == (int)EnumsManagement.CLASSTYPE.Club)
                    {
                        lbtnEdit.Visible = false;
                    }
                    else
                    {
                        if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                        {
                            lbtnEdit.Visible = false;
                        }
                        else
                        {
                            lbtnEdit.Visible = true;
                        }
                    }

                    //txtEndDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
        }
        public bool? ISTeacher()
        {
            if (Authentication.LogginTeacher != null)
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                if (objTeacher.ISUserRole.ManageStudentFlag == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void BindDropdown()
        {
            ddlClassYear.DataSource = CommonOperation.GetYear();
            ddlClassYear.DataValueField = "ID";
            ddlClassYear.DataTextField = "Year";
            ddlClassYear.DataBind();
            ddlClassYear.Items.Insert(0, new ListItem { Text = "Select Year", Value = "" });

            ddlClassType.DataSource = objClassManagement.ClassTyleList();
            ddlClassType.DataValueField = "ID";
            ddlClassType.DataTextField = "Name";
            ddlClassType.DataBind();
            ddlClassType.Items.Insert(0, new ListItem { Text = "Select Class Type", Value = "0" });
        }
        public void bindData(int ID)
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                MISClass Obj = objClassManagement.GetClass(ID);
                if (Obj != null)
                {
                    litclassname.Text = Obj.Name;
                    litClassType.Text = Obj.ClassType;
                    int YearID = Obj.Year != "" ? Convert.ToInt32(Obj.Year) : 0;
                    litClassYear.Text = YearID != 0 ? DB.ISClassYears.SingleOrDefault(p => p.ID == YearID).Year : "N/A";
                    litafterschooltype.Text = !String.IsNullOrEmpty(Obj.AfterSchoolType) ? Obj.AfterSchoolType : "N/A";
                    litexrternal.Text = !String.IsNullOrEmpty(Obj.ExternalOrganisation) ? Obj.ExternalOrganisation : "N/A";
                    HtmlControl div = new HtmlGenericControl("divExternal");
                    div.Attributes.Add("title", !String.IsNullOrEmpty(Obj.ExternalOrganisation) ? Obj.ExternalOrganisation : "N/A");
                    litclassnameleft.Text = Obj.Name;
                    litStatus.Text = Obj.Status;
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        litstudentCount.Text = Obj.StudentCount.ToString();
                    }
                    else
                    {
                        ISClass objClass = DB.ISClasses.SingleOrDefault(p => p.ID == ID);
                        if (objClass.Name.Contains("Outside"))
                        {
                            litstudentCount.Text = objClassManagement.getExternalStudentCount(objClass.SchoolID).ToString();
                        }
                        else
                        {
                            litstudentCount.Text = objClassManagement.getStudentCount(objClass.ID, objClass.SchoolID).ToString();
                        }
                    }
                    litTeacher.Text = Obj.TeacherName != "" ? Obj.TeacherName : "No Teacher assigned to this Class";

                    lblClassTeachers.Text = Obj.TeacherName != "" ? Obj.TeacherName : "No Teacher assigned to this Class";
                    if (Obj.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                    {
                        int index = Obj.Name.IndexOf("(");
                        if (index > 0)
                            txtClassName.Text = Obj.Name.Substring(0, index);
                    }
                    else
                    {
                        txtClassName.Text = Obj.Name;
                    }
                    ddlClassYear.SelectedValue = Obj.Year;
                    ddlClassType.SelectedValue = Obj.TypeID.ToString();
                    ddlAfterSchoolType.SelectedValue = Obj.AfterSchoolType;
                    txtOrganization.Text = Obj.ExternalOrganisation;                   
                    if (Obj.TypeID == (int)EnumsManagement.CLASSTYPE.Standard)
                    {
                        txtOrganization.Text = "";
                    }
                    if (Obj.Active == true)
                    {
                        ChkActive.Checked = true;
                    }
                    else
                    {
                        ChkActive.Checked = false;
                    }
                    //txtEndDate.Text = Obj.EndDate != null ? Obj.EndDate.Value.ToString("yyyy-MM-dd") : ""; //Obj.EndDate.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) : "";
                    litClassName1.Text = Obj.Name;
                    ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "ShowMessage();", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }

        protected void lbtnViewStudent_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
            ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ID);
            if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
            {
                Response.Redirect("OfficeClass.aspx");
            }
            else if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Club)
            {
                Response.Redirect("ClubClass.aspx");
            }
            else if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
            {
                Response.Redirect("InternalClass.aspx");
            }
            else
            {
                Response.Redirect("ClassStudents.aspx?ID=" + Convert.ToInt32(OperationManagement.GetOperation("ID")));
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                List<ISClass> ObjClasses = DB.ISClasses.Where(p => p.ID != ID && p.Name == txtClassName.Text && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList();
                if (ObjClasses.Count > 0)
                {
                    bindData(ID);
                    AlertMessageManagement.ServerMessage(Page, "Class already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    if (ddlClassType.SelectedValue == Convert.ToString((int)EnumsManagement.CLASSTYPE.AfterSchool))
                    {
                        ISClass ObjClassses = DB.ISClasses.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                        if (ObjClassses.TypeID != Convert.ToInt32(ddlClassType.SelectedValue))
                        {
                            bindData(ID);
                            AlertMessageManagement.ServerMessage(Page, "Not Allowed. Class With Standard Type CANNOT Be Changed to After-School Type", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else
                        {
                            List<ISClass> ObjClass = DB.ISClasses.Where(p => p.ID != ID && p.SchoolID == Authentication.SchoolID && p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true && p.Deleted == true).ToList();
                            if (ObjClass.Count > 0)
                            {
                                bindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                                AlertMessageManagement.ServerMessage(Page, "Only One After School is Allowed for a Standard School", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else
                            {
                                if (DB.ISSchools.Where(p => p.Name == txtOrganization.Text).Count() <= 0 && ddlAfterSchoolType.SelectedValue == "External")
                                {
                                    BindDropdown();
                                    bindData(ID);
                                    AlertMessageManagement.ServerMessage(Page, "Please Check NonListed CheckBox because your Afterschool Organization Name does not contains in Application", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                                //else if (DB.ISSchools.Where(p => p.Name == txtOrganization.Text).Count() > 0 && ddlAfterSchoolType.SelectedValue == "External")
                                //{
                                //    BindDropdown();
                                //    bindData(ID);
                                //    AlertMessageManagement.ServerMessage(Page, "After School name is already setup on our database. Please select Listed and pick the name of the After School from the search field", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                //}
                                else
                                { 
                                    ISClass ObjClasss = DB.ISClasses.SingleOrDefault(p => p.ID == ID && p.Deleted == true);

                                    if (DB.ISPickups.Where(p => p.PickStatus.Contains("After-School") && p.ISTeacher.SchoolID == Authentication.SchoolID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).Count() > 0 && ObjClasss.AfterSchoolType != ddlAfterSchoolType.SelectedValue)
                                    {
                                        bindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                                        AlertMessageManagement.ServerMessage(Page, "Class With Students Assigned AfterSchool Type Cannot Be Changed", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    //edited by Maharshi @GatistavamSoftech  working:
                                    /*else if (DB.ISPickups.Where(p => p.PickStatus.Contains("After-School-Ex") && p.ISTeacher.SchoolID == Authentication.SchoolID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).Count() > 0 && ChkActive.Checked == false)
                                    {
                                        bindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                                        AlertMessageManagement.ServerMessage(Page, "Class With Students Assigned AfterSchool-Ex Type Cannot Be Changed", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }*/
                                    //edited by Maharshi @GatistavamSoftech  working: chacks if there are students still present in the Afterschool-Internal Class before In-Activating it
                                    else if (DB.ISPickups.Where(p => p.PickStatus.Contains("After-School") && p.ISTeacher.SchoolID == Authentication.SchoolID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).Count() > 0 && ChkActive.Checked == false)
                                    {
                                        bindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                                        AlertMessageManagement.ServerMessage(Page, "Class With Students Assigned Cannot Be Made Inactive", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    else if (DB.ISStudents.Where(p => p.ClassID == ID && p.Deleted == true).Count() > 0)
                                    { 
                                        if (ChkActive.Checked == false)
                                        {
                                            bindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                                            AlertMessageManagement.ServerMessage(Page, "Class With Students Assigned Cannot Be Made Inactive", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }
                                        else
                                        {
                                            if (DB.ISClasses.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true).AfterSchoolType == ddlAfterSchoolType.SelectedValue)
                                            {
                                                //if (ObjClasss.AfterSchoolType == "Internal" && ddlAfterSchoolType.SelectedValue == "External")
                                                //{
                                                //    List<ISTeacherClassAssignment> ObjTList = DB.ISTeacherClassAssignments.Where(p => p.ClassID == ObjClasss.ID && p.Active == true && p.Deleted == true).ToList();
                                                //    DB.ISTeacherClassAssignments.RemoveRange(ObjTList);
                                                //    DB.SaveChanges();
                                                //}
                                                objClassManagement.CreateorUpdateClass(ID, Authentication.SchoolID, txtClassName.Text, ddlClassYear.SelectedValue, Convert.ToInt32(ddlClassType.SelectedValue), ddlAfterSchoolType.SelectedValue, txtOrganization.Text, ChkActive.Checked == true ? true : false);
                                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                                                EmailManage(_Class);
                                                LogManagement.AddLog("Class Updated Successfully" + "Name : " + txtClassName.Text + " Document Category : Class", "Class");
                                                AlertMessageManagement.ServerMessage(Page, "Class Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                BindDropdown();
                                                bindData(ID);
                                            }
                                            else
                                            {
                                                bindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                                                AlertMessageManagement.ServerMessage(Page, "Class With Students Can not allow to Change AfterSchoolType", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                        }
                                    } 
                                    else
                                    {
                                        if (ObjClasss.AfterSchoolType == "Internal" && ddlAfterSchoolType.SelectedValue == "External")
                                        {
                                            List<ISTeacherClassAssignment> ObjTList = DB.ISTeacherClassAssignments.Where(p => p.ClassID == ObjClasss.ID && p.Active == true && p.Deleted == true).ToList();
                                            DB.ISTeacherClassAssignments.RemoveRange(ObjTList);
                                            DB.SaveChanges();
                                        }
                                        objClassManagement.CreateorUpdateClass(ID, Authentication.SchoolID, txtClassName.Text, ddlClassYear.SelectedValue, Convert.ToInt32(ddlClassType.SelectedValue), ddlAfterSchoolType.SelectedValue, txtOrganization.Text, ChkActive.Checked == true ? true : false);
                                        ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                                        EmailManage(_Class);
                                        LogManagement.AddLog("Class Updated Successfully" + "Name : " + txtClassName.Text + " Document Category : Class", "Class");
                                        AlertMessageManagement.ServerMessage(Page, "Class Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                        BindDropdown();
                                        bindData(ID);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        ISClass ObjClassses = DB.ISClasses.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
                        if (ObjClassses.TypeID != Convert.ToInt32(ddlClassType.SelectedValue))
                        {
                            bindData(ID);
                            AlertMessageManagement.ServerMessage(Page, "Not Allowed. Class With After-School Type CANNOT Be Changed to Standard", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else
                        {
                            if (DB.ISStudents.Where(p => p.ClassID == ID && p.Active == true && p.Deleted == true).Count() > 0)
                            {
                                //string dates = txtEndDate.Text;
                                //string Format = "";
                                //if (dates.Contains("/"))
                                //{
                                //    string[] arrDate = dates.Split('/');
                                //    Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                                //}
                                //else
                                //{
                                //    Format = dates;
                                //}
                                //DateTime dt2 = Convert.ToDateTime(Format);
                                if (ChkActive.Checked == false)
                                {
                                    bindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                                    AlertMessageManagement.ServerMessage(Page, "Class With Students Assigned Cannot Be Made Inactive", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                                else
                                {
                                    objClassManagement.CreateorUpdateClass(ID, Authentication.SchoolID, txtClassName.Text, ddlClassYear.SelectedValue, Convert.ToInt32(ddlClassType.SelectedValue), ddlAfterSchoolType.SelectedValue, txtOrganization.Text, ChkActive.Checked == true ? true : false);
                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                                    EmailManage(_Class);
                                    LogManagement.AddLog("Class Updated Successfully" + "Name : " + txtClassName.Text + " Document Category : Class", "Class");
                                    AlertMessageManagement.ServerMessage(Page, "Class Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                    BindDropdown();
                                    bindData(ID);
                                }
                            }
                            else
                            {
                                objClassManagement.CreateorUpdateClass(ID, Authentication.SchoolID, txtClassName.Text, ddlClassYear.SelectedValue, Convert.ToInt32(ddlClassType.SelectedValue), ddlAfterSchoolType.SelectedValue, txtOrganization.Text, ChkActive.Checked == true ? true : false);
                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                                EmailManage(_Class);
                                LogManagement.AddLog("Class Updated Successfully" + "Name : " + txtClassName.Text + " Document Category : Class", "Class");
                                AlertMessageManagement.ServerMessage(Page, "Class Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                BindDropdown();
                                bindData(ID);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        public void EmailManage(ISClass _Class)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
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
                                    Class/Classes have been updated by  : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Update Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact<b>" + LoggedINName + @"</b> should you require more information.
                                   </td>
                                </tr>
                                <tr style = 'float:right;'>
                                    <td>
                                        <br/>
                                        Regards, <br/> " + LoggedINName + @"<br/> " + _School.Name + @"
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
                                    Class/Classes have been updated by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Update Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact<b>" + LoggedINName + @"</b> should you require more information.
                                   </td>
                                </tr>
                                <tr style = 'float:right;'>
                                    <td>
                                        <br/>
                                        Regards, <br/> " + LoggedINName + @"<br/> " + _School.Name + @"
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Class Update Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Class Update Notification", SuperwisorBody);
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetOrganisation(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            TeacherManagement objTeacherManagement = new TeacherManagement();
            List<ISSchool> ObjList = DB.ISSchools.Where(p => p.Deleted == true && p.Active == true && p.Name.ToLower().Contains(prefixText.ToLower()) && p.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool).ToList();

            List<string> AfterSchools = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                AfterSchools.Add(ObjList[i].Name);
            }
            //if (AfterSchools.Count <= 0)
            //{
            //    AfterSchools.Add("Record not Found. Please Select NonListed Radio Button and Write your School Name.");
            //}
            return AfterSchools;
        }

              
    }
}