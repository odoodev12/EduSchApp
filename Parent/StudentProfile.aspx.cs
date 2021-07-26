using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Parent
{
    public partial class StudentProfile : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISParentLogin())
            {
                Response.Redirect(Authentication.ParentAuthorizePage());
            }
            if (!IsPostBack)
            {
                BindData(Convert.ToInt32(Authentication.LogginParent.ID));
            }
            if (IsPostBack && fpUpload.PostedFile != null && fpUpload != null)
            {
                if (fpUpload.PostedFile.FileName.Length > 0)
                {
                    List<ISPicker> listOfPickers = DB.ISPickers.Where(r => r.Email.ToLower() == Authentication.LoginParentEmail.ToLower() && r.Deleted == true).ToList();

                    string filename = System.IO.Path.GetFileName(fpUpload.PostedFile.FileName);
                    fpUpload.SaveAs(Server.MapPath("~/Upload/" + filename));

                    if (listOfPickers?.Count > 0)
                    {

                        foreach (var item in listOfPickers)
                        {
                            item.Photo = "Upload/" + filename;
                            DB.SaveChanges();
                        }

                        AlertMessageManagement.ServerMessage(Page, "Photo Uploaded Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    }

                    BindData(Convert.ToInt32(Authentication.LogginParent.ID));
                }
            }
        }

        public void BindData(int ID)
        {
            ClassManagement objClassManagement = new ClassManagement();
            MISStudent Obj = objClassManagement.GetStudentInfo(ID);
            if (Obj != null)
            {
                litClassName.Text = Obj.ClassName;
                litPPName.Text = Obj.ParantEmail1 == Authentication.LoginParentEmail ? Obj.ParantName1 : Obj.ParantName2;
                litPPEmail.Text = Obj.ParantEmail1 == Authentication.LoginParentEmail ? Obj.ParantEmail1 : Obj.ParantEmail2;
                litPPPhone.Text = Obj.ParantEmail1 == Authentication.LoginParentEmail ? Obj.ParantPhone1 : Obj.ParantPhone2;
                litPPRelation.Text = Obj.ParantEmail1 == Authentication.LoginParentEmail ? Obj.ParantRelation1 : Obj.ParantRelation2;
                litStudent.Text = Obj.ParantEmail1 == Authentication.LoginParentEmail ? Obj.ParantName1 : Obj.ParantName2;
                //litIDno.Text = Obj.StudentNo;
                txtPPName.Text = Obj.ParantEmail1 == Authentication.LoginParentEmail ? Obj.ParantName1 : Obj.ParantName2;
                txtPPEmail.Text = Obj.ParantEmail1 == Authentication.LoginParentEmail ? Obj.ParantEmail1 : Obj.ParantEmail2;
                txtPPPhone.Text = Obj.ParantEmail1 == Authentication.LoginParentEmail ? Obj.ParantPhone1 : Obj.ParantPhone2;
                ddlPPRelation.SelectedValue = Obj.ParantEmail1 == Authentication.LoginParentEmail ? Obj.ParantRelation1 : Obj.ParantRelation2;

                ISPicker iSPicker = DB.ISPickers.FirstOrDefault(r => r.Email.ToLower() == Authentication.LoginParentEmail.ToLower() && r.Deleted == true);

                img1.Src = String.Format("{0}{1}", "../", iSPicker.Photo);
            }
        }

        protected void resetpassword_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(Authentication.LogginParent.ID);
                ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                if (objStudent != null)
                {
                    if (Authentication.LoginParentEmail == objStudent.ParantEmail1)
                    {
                        objStudent.ParantPassword1 = EncryptionHelper.Encrypt(txtpassword1.Text);
                    }
                    if (Authentication.LoginParentEmail == objStudent.ParantEmail2)
                    {
                        objStudent.ParantPassword2 = EncryptionHelper.Encrypt(txtpassword1.Text);
                    }
                    objStudent.ModifyBy = Authentication.LogginParent.ID;
                    objStudent.ModifyDateTime = DateTime.Now;
                    DB.SaveChanges();
                    Clear();
                    AlertMessageManagement.ServerMessage(Page, "Parent Password Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        private void Clear()
        {
            txtcpassword1.Text = "";
            txtpassword1.Text = "";
            txtPPName.Text = "";
            txtPPEmail.Text = "";
            txtPPPhone.Text = "";
            ddlPPRelation.SelectedValue = "0";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool isEmailIdChanged = false;
                bool isParamryParentEmailChanged = true;
                string newPassward = EncryptionHelper.Encrypt(CommonOperation.GenerateNewRandom());

                ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == Authentication.LogginParent.ID && p.Deleted == true);
                int ID = _Student.ID;
                if (_Student != null)
                {
                    Authentication.LoginParentName = txtPPName.Text;
                    Authentication.LoginParentRelation = ddlPPRelation.SelectedItem.Text;
                    string Email = Authentication.LoginParentEmail;
                    string Name = Authentication.LoginParentName + "(" + Authentication.LoginParentRelation + ")";
                    string PPEmail = _Student.ParantEmail1;
                    string newEmail = txtPPEmail.Text;

                    if (Email.ToLower() != newEmail.ToLower())
                        isEmailIdChanged = true;


                    if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        IList<ISStudent> obj2 = DB.ISStudents.Where(a => a.ID != ID && a.StudentNo == _Student.StudentNo && a.SchoolID == Authentication.SchoolID && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                        bool isConflictFound = false;
                        List<ISStudent> obj3 = DB.ISStudents.Where(p => (p.ParantEmail1 == Email || p.ParantEmail2 == Email) && p.Deleted == true).ToList();
                        string conflictStudentName = string.Empty;
                        foreach (var item in obj3)
                        {
                            conflictStudentName = item.StudentName;
                            if (Email == item.ParantEmail1 && item.ParantRelation2 == ddlPPRelation.SelectedValue && ddlPPRelation.SelectedItem.Text != "Guardian")
                            {
                                isConflictFound = true;
                                break;
                            }
                            else if (Email == item.ParantEmail2 && item.ParantRelation1 == ddlPPRelation.SelectedValue && ddlPPRelation.SelectedItem.Text != "Guardian")
                            {
                                isConflictFound = true;
                                break;
                            }
                        }

                        if (obj2.Count > 0)
                        {
                            AlertMessageManagement.ServerMessage(Page, "Not Allowed. Student Number Must Be Unique", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (isConflictFound)
                        {
                            AlertMessageManagement.ServerMessage(Page, $"Relationship on student records duplicating. Please contact the relevant School Admin for this change.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (DB.ISStudents.Where(a => a.ID != ID && a.StudentName == _Student.StudentName && a.ParantEmail1 == txtPPEmail.Text && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                        {
                            AlertMessageManagement.ServerMessage(Page, "Student Name With Primary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (DB.ISStudents.Where(a => a.ID != ID && a.StudentName == _Student.StudentName && a.ParantEmail2 == txtPPEmail.Text && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                        {
                            AlertMessageManagement.ServerMessage(Page, "Student Name With Secondary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else
                        {

                            List<ISStudent> _StudentList = DB.ISStudents.Where(p => (p.ParantEmail1.ToLower() == Email.ToLower() || p.ParantEmail2.ToLower() == Email.ToLower()) && p.Deleted == true).ToList();
                            foreach (var item in _StudentList)
                            {
                                try
                                {
                                    if (Email.ToLower() == item.ParantEmail1.ToLower())
                                    {
                                        List<ISPicker> _PickerList = DB.ISPickers.Where(p => p.StudentID == item.ID && p.Email.ToLower() == Email.ToLower() && p.Active == true && p.Deleted == true).ToList();
                                        foreach (var items in _PickerList)
                                        {
                                            items.FirstName = txtPPName.Text + "(" + ddlPPRelation.SelectedItem.Text + ")";
                                            items.Email = txtPPEmail.Text;
                                            items.Phone = txtPPPhone.Text;
                                            items.ModifyBy = Authentication.SchoolID;
                                            items.ModifyDateTime = DateTime.Now;
                                            DB.SaveChanges();
                                        }

                                        item.ParantName1 = txtPPName.Text;
                                        item.ParantEmail1 = txtPPEmail.Text;
                                        item.ParantPhone1 = txtPPPhone.Text;

                                        if (isEmailIdChanged)
                                            item.ParantPassword1 = newPassward;
                                        
                                        item.ParantRelation1 = ddlPPRelation.SelectedItem.Text;
                                        DB.SaveChanges();
                                      


                                        //ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.StudentID == item.ID && p.Email == Email && p.Active == true && p.Deleted == true);
                                        //if (objPicker != null)
                                        //{

                                        //}
                                    }
                                    else
                                    {
                                        List<ISPicker> _PickerList = DB.ISPickers.Where(p => p.StudentID == item.ID && p.Email.ToLower() == Email.ToLower() && p.Active == true && p.Deleted == true).ToList();
                                        foreach (var items in _PickerList)
                                        {
                                            items.FirstName = txtPPName.Text + "(" + ddlPPRelation.SelectedItem.Text + ")";
                                            items.Email = txtPPEmail.Text;
                                            items.Phone = txtPPPhone.Text;
                                            items.ModifyBy = Authentication.SchoolID;
                                            items.ModifyDateTime = DateTime.Now;
                                            DB.SaveChanges();
                                        }

                                        item.ParantName2 = txtPPName.Text;
                                        item.ParantEmail2 = txtPPEmail.Text;
                                        if (isEmailIdChanged)
                                        {
                                            item.ParantPassword2 = newPassward;
                                            isParamryParentEmailChanged = false;
                                        }

                                        item.ParantPhone2 = txtPPPhone.Text;
                                        item.ParantRelation2 = ddlPPRelation.SelectedItem.Text;
                                        DB.SaveChanges();
                                        


                                    }

                                    var PickerList = DB.ISPickers.Where(p => p.StudentID == item.ID && p.CreatedByEmail.ToLower() == Email.ToLower() && p.Active == true && p.Deleted == true).ToList();
                                    foreach (var items in PickerList)
                                    {

                                        items.CreatedByEmail = txtPPEmail.Text;
                                        items.CreatedByName = txtPPName.Text;
                                        items.ModifyBy = Authentication.SchoolID;
                                        items.ModifyDateTime = DateTime.Now;
                                        DB.SaveChanges();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogManagement.AddLog(ex);
                                }
                            }

                            // DB.updateParantEmailIdInWhole()
                        }
                    }
                    else
                    {
                        IList<ISStudent> obj2 = DB.ISStudents.Where(a => a.ID != ID && a.StudentNo == _Student.StudentNo && a.SchoolID == Authentication.SchoolID && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                        bool isConflictFound = false;
                        List<ISStudent> obj3 = DB.ISStudents.Where(p => (p.ParantEmail1.ToLower() == Email.ToLower() || p.ParantEmail2.ToLower() == Email.ToLower()) && p.Deleted == true).ToList();
                        string conflictStudentName = string.Empty;
                        foreach (var item in obj3)
                        {
                            conflictStudentName = item.StudentName;
                            if (Email == item.ParantEmail1 && item.ParantRelation2 == ddlPPRelation.SelectedItem.Text && ddlPPRelation.SelectedItem.Text != "Guardian")
                            {
                                isConflictFound = true;
                                break;
                            }
                            else if (Email == item.ParantEmail2 && item.ParantRelation1 == ddlPPRelation.SelectedItem.Text && ddlPPRelation.SelectedItem.Text != "Guardian")
                            {
                                isConflictFound = true;
                                break;
                            }
                        }
                        if (obj2.Count > 0)
                        {
                            AlertMessageManagement.ServerMessage(Page, "Not Allowed. Student Number Must Be Unique", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (isConflictFound)
                        {
                            AlertMessageManagement.ServerMessage(Page, $"Relationship on student records duplicating. Please contact the relevant School Admin for this change.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (DB.ISStudents.Where(a => a.ID != ID && a.StudentName == _Student.StudentName && a.ParantEmail1.ToLower() == txtPPEmail.Text.ToLower() && a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool && a.Active == true && a.Deleted == true).ToList().Count > 0)
                        {
                            AlertMessageManagement.ServerMessage(Page, "Student Name With Primary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (DB.ISStudents.Where(a => a.ID != ID && a.StudentName == _Student.StudentName && a.ParantEmail2.ToLower() == txtPPEmail.Text.ToLower() && a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool && a.Active == true && a.Deleted == true).ToList().Count > 0)
                        {
                            AlertMessageManagement.ServerMessage(Page, "Student Name With Secondary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else
                        {

                            List<ISStudent> _StudentList = DB.ISStudents.Where(p => (p.ParantEmail1.ToLower() == Email.ToLower() || p.ParantEmail2.ToLower() == Email.ToLower()) && p.Deleted == true).ToList();
                            foreach (var item in _StudentList)
                            {
                                try
                                {
                                    if (Email.ToLower() == item.ParantEmail1.ToLower())
                                    {
                                        List<ISPicker> _PickerList = DB.ISPickers.Where(p => p.StudentID == item.ID && p.Email.ToLower() == Email.ToLower() && p.Active == true && p.Deleted == true).ToList();
                                        foreach (var items in _PickerList)
                                        {
                                            items.FirstName = txtPPName.Text + "(" + ddlPPRelation.SelectedItem.Text + ")";
                                            items.Email = txtPPEmail.Text;
                                            items.Phone = txtPPPhone.Text;
                                            items.ModifyBy = Authentication.SchoolID;
                                            items.ModifyDateTime = DateTime.Now;
                                            DB.SaveChanges();
                                        }

                                        item.ParantName1 = txtPPName.Text;
                                        item.ParantEmail1 = txtPPEmail.Text;

                                        if (isEmailIdChanged)
                                            item.ParantPassword1 = newPassward;

                                        item.ParantPhone1 = txtPPPhone.Text;
                                        item.ParantRelation1 = ddlPPRelation.SelectedItem.Text;
                                        DB.SaveChanges();
                                     
                                    }
                                    else
                                    {
                                        List<ISPicker> _PickerList = DB.ISPickers.Where(p => p.StudentID == item.ID && p.Email.ToLower() == Email.ToLower() && p.Active == true && p.Deleted == true).ToList();
                                        foreach (var items in _PickerList)
                                        {
                                            items.FirstName = txtPPName.Text + "(" + ddlPPRelation.SelectedItem.Text + ")";
                                            items.Email = txtPPEmail.Text;
                                            items.Phone = txtPPPhone.Text;
                                            items.ModifyBy = Authentication.SchoolID;
                                            items.ModifyDateTime = DateTime.Now;
                                            DB.SaveChanges();
                                        }

                                        item.ParantName2 = txtPPName.Text;
                                        item.ParantEmail2 = txtPPEmail.Text;

                                        if (isEmailIdChanged)
                                        {
                                            item.ParantPassword2 = newPassward;
                                            isParamryParentEmailChanged = false;
                                        }

                                        item.ParantPhone2 = txtPPPhone.Text;
                                        item.ParantRelation2 = ddlPPRelation.SelectedItem.Text;
                                        DB.SaveChanges();
                                       
                                    }

                                    var PickerList = DB.ISPickers.Where(p => p.StudentID == item.ID && p.CreatedByEmail.ToLower() == Email.ToLower() && p.Active == true && p.Deleted == true).ToList();
                                    foreach (var items in PickerList)
                                    {

                                        items.CreatedByEmail = txtPPEmail.Text;
                                        items.CreatedByName = txtPPName.Text;
                                        items.ModifyBy = Authentication.SchoolID;
                                        items.ModifyDateTime = DateTime.Now;
                                        DB.SaveChanges();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogManagement.AddLog(ex);
                                }
                            }


                        }
                    }
                    AlertMessageManagement.ServerMessage(Page, "Parent Profile Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    Clear();
                    BindData(Authentication.LogginParent.ID);
                    _Student = DB.ISStudents.SingleOrDefault(p => p.ID == Authentication.LogginParent.ID && p.Deleted == true);
                    if (isEmailIdChanged)
                    {
                        string parentName = _Student.ParantEmail1.ToLower() == txtPPEmail.Text.ToLower() ? _Student.ParantName1 : _Student.ParantName2;
                        

                        CommonOperation.OldEmailChangeNotification(parentName, txtPPEmail.Text, Email);
                        CommonOperation.NewEmailChangeNotification(parentName, txtPPEmail.Text, EncryptionHelper.Decrypt(newPassward));

                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        Session.Clear();
                        Response.Redirect("~/Login.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }

        }
    }
}