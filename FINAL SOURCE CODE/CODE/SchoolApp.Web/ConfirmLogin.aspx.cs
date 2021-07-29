using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web
{
    public partial class ConfirmLogin : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        string userType = "";
        string answer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] != null && Session["LoggedInUserType"] != null && Convert.ToString(Session["LoggedInUserType"]).Length > 0)
            {
                userType = Convert.ToString(Session["LoggedInUserType"]);

                if (!IsPostBack)
                {
                    BindComboBox();
                }
            }
            else
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }

        }

        private void BindComboBox()
        {
            drpFirstAns.DataSource = GetAtoZWord();
            drpFirstAns.DataBind();


            drpSecondAns.DataSource = GetAtoZWord();
            drpSecondAns.DataBind();


            drpThirdAns.DataSource = GetAtoZWord();
            drpThirdAns.DataBind();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            if (!IsPostBack)
                SetQuestionLabel();
        }

        private void SetQuestionLabel()
        {
            if (userType == EnumsManagement.USERTYPE.SCHOOL.ToString())
            {
                ISSchool isSchoolLogin = Session["LoggedInUser"] as ISSchool;

                if (isSchoolLogin != null)
                {
                    answer = EncryptionHelper.Decrypt(isSchoolLogin.MemorableQueAnswer);
                }
            }
            else if (userType == EnumsManagement.USERTYPE.TEACHER.ToString())
            {
                ISTeacher teacherLogin = Session["LoggedInUser"] as ISTeacher;

                if (teacherLogin != null)
                {
                    answer = EncryptionHelper.Decrypt(teacherLogin.MemorableQueAnswer);
                }
            }
            else if (userType == EnumsManagement.USERTYPE.PARENT.ToString())
            {
                MISStudent misStudent = Session["LoggedInUserMisStudent"] as MISStudent;

                if (misStudent != null)
                {
                    ISStudentConfirmLogin confirnLogin = DB.ISStudentConfirmLogins.FirstOrDefault(r => r.Email.ToLower() == misStudent.ParentEmail.ToLower());
                    answer = EncryptionHelper.Decrypt(confirnLogin.MemorableQueAnswer);
                }
            }

            Random random = new Random();
            List<int> randomList = new List<int>();
            bool isValid = true;
            while (isValid)
            {
                int temp = random.Next(1, answer.Length);

                if (!randomList.Contains(temp))
                    randomList.Add(temp);

                if (randomList.Count >= 3)
                    isValid = false;
            }

            if (randomList.Count > 0)
            {
                lblFirstAns.Text = $"{(randomList[0])} char";
                lblSecondAns.Text = $"{(randomList[1])} char";
                lblThirdAns.Text = $"{(randomList[2])} char";

                Session["RandomList"] = randomList;
                Session["Answer"] = answer;
            }

        }

        private string GetString(int number)
        {
            string returnString = "";

            switch (number)
            {
                case 1:
                    returnString = "First";
                    break;
                case 2:
                    returnString = "Second";
                    break;
                case 3:
                    returnString = "Third";
                    break;
                case 4:
                    returnString = "Fourth";
                    break;
                case 5:
                    returnString = "Fifth";
                    break;
                case 6:
                    returnString = "Sixth";
                    break;
                case 7:
                    returnString = "Seventh";
                    break;
                case 8:
                    returnString = "Eighth";
                    break;
            }

            return returnString;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<int> randomList = new List<int>();
            try
            {
                if (Session["RandomList"] != null)
                    randomList = Session["RandomList"] as List<int>;

                if (Session["Answer"] != null)
                    answer = Convert.ToString(Session["Answer"]);

                int first = randomList[0] - 1;
                int second = randomList[1] - 1;
                int third = randomList[2] - 1;

                if (drpFirstAns.SelectedValue.ToLower() == answer[first].ToString().ToLower() &&
                drpSecondAns.SelectedValue.ToLower() == answer[second].ToString().ToLower() &&
                drpThirdAns.SelectedValue.ToLower() == answer[third].ToString().ToLower())
                {
                    if (userType == EnumsManagement.USERTYPE.SCHOOL.ToString())
                    {
                        ISSchool schoolUser = Session["LoggedInUser"] as ISSchool;
                        Authentication.LogginSchool = schoolUser;
                        Authentication.SchoolID = schoolUser.ID;
                        Authentication.USERTYPE = EnumsManagement.USERTYPE.SCHOOL.ToString();
                        Authentication.SchoolTypeID = Convert.ToInt32(schoolUser.TypeID);
                        Authentication.isLogin = true;
                        Response.Redirect("~/School/Home.aspx", false);
                    }
                    else if (userType == EnumsManagement.USERTYPE.TEACHER.ToString())
                    {
                        ISTeacher ObjTeacher = Session["LoggedInUser"] as ISTeacher;

                        ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ObjTeacher.SchoolID);
                        Authentication.LogginTeacher = ObjTeacher;
                        Authentication.SchoolID = ObjTeacher.SchoolID;
                        Authentication.USERTYPE = EnumsManagement.USERTYPE.TEACHER.ToString();
                        Authentication.SchoolTypeID = Convert.ToInt32(ObjSchool.TypeID);
                        Authentication.isLogin = true;
                        Response.Redirect("~/Teacher/Home.aspx");
                    }
                    else
                    {
                        ISStudent ObjStudent = Session["LoggedInUser"] as ISStudent;
                        MISStudent misStudent = Session["LoggedInUserMisStudent"] as MISStudent;

                        ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ObjStudent.SchoolID);

                        Authentication.LogginParent = ObjStudent;
                        Authentication.LoginParentEmail = misStudent.ParentEmail;
                        Authentication.LoginParentName = misStudent.ParentName;
                        Authentication.LoginParentRelation = misStudent.ParentRelation;
                        Authentication.SchoolID = ObjStudent.SchoolID.Value;
                        Authentication.USERTYPE = EnumsManagement.USERTYPE.PARENT.ToString();
                        Authentication.SchoolTypeID = Convert.ToInt32(ObjSchool.TypeID);
                        Authentication.isLogin = true;
                        Response.Redirect("~/Parent/Home.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('You have entered wrong character. please try again');", true);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private List<string> GetAtoZWord()
        {
            List<string> wordLIst = new List<string>();

            for (int i = 97; i <= 122; i++)
            {
                char ff = (char)i;
                wordLIst.Add(ff.ToString());
            }

            for (int j = 0; j <= 9; j++)
                wordLIst.Add(j.ToString());

            return wordLIst;
        }

        protected void lnkForgot_Click(object sender, EventArgs e)
        {
            try
            {
                if (userType != "")
                {
                    string loginUrl = WebConfigurationManager.AppSettings["LoginURL"].ToString().Replace("/Login.aspx", "");
                    string Type = userType;
                    // string Email = txtUserName.Text;
                    string LoggedINType = string.Empty;

                    string vid = EncryptionHelper.Encrypt(Type);
                    string activationId = Guid.NewGuid().ToString().Replace("-", "");
                    string encryptedActivationId = EncryptionHelper.Encrypt(activationId);
                    if (Type.ToLower() == "school")
                    {
                        ISSchool _School = Session["LoggedInUser"] as ISSchool; ///DB.ISSchools.OrderBy(p => p.ID).FirstOrDefault(p => p.AdminEmail == Email && p.Deleted == true);
                        if (_School != null)
                        {
                            //_School.Password = EncryptionHelper.Encrypt(Password);

                            ISSchool tempResult = DB.ISSchools.Where(r => r.ID == _School.ID).FirstOrDefault();
                            tempResult.IsActivationID = encryptedActivationId;
                            DB.SaveChanges();

                            LoggedINType = _School.Name;

                            string resetLink = $"{loginUrl}/ResetMemorablePwd.aspx?eid={EncryptionHelper.Encrypt(_School.AdminEmail)}&vid={EncryptionHelper.Encrypt(EnumsManagement.USERTYPE.SCHOOL.ToString())}&pwd={tempResult.Password}&gid={activationId}";

                            string AdminBody = String.Empty;

                            string tblAdminBody = string.Empty;

                            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Your Memorable word has been Reset. Please reset click on <a href=""" + resetLink + @""" target=""_blank"">Reset Memorable word</a>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password Reset Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"<br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact Admin.
                                    </td>
                                </tr></table>";

                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                            {
                                AdminBody += reader.ReadToEnd();
                            }
                            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                            bool isSent = _EmailManagement.SendEmail(_School.AdminEmail, "Reset Password Email", AdminBody);
                            if (isSent)
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Reset memorable password link has been sent your email.');", true);
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Failed to send reset memorable password link in your email.');", true);
                        }
                        else
                        {
                            AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Email Address.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                    }
                    else if (Type.ToLower() == "teacher")
                    {
                        ISTeacher _Teacher = Session["LoggedInUser"] as ISTeacher; //DB.ISTeachers.OrderBy(p => p.ID).FirstOrDefault(p => p.Email == Email && p.Deleted == true);
                        if (_Teacher != null)
                        {
                            LoggedINType = _Teacher.Name;
                            ISTeacher tempTeach = DB.ISTeachers.FirstOrDefault(r => r.ID == _Teacher.ID);

                            //_Teacher.Password = EncryptionHelper.Encrypt(Password);
                            tempTeach.IsActivationID = encryptedActivationId;
                            DB.SaveChanges();
                            string AdminBody = String.Empty;

                            string resetLink = $"{loginUrl}/ResetMemorablePwd.aspx?eid={EncryptionHelper.Encrypt(tempTeach.Email)}&vid={EncryptionHelper.Encrypt(EnumsManagement.USERTYPE.TEACHER.ToString())}&pwd={tempTeach.Password}&gid={activationId}";

                            string tblAdminBody = string.Empty;

                            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Teacher.Name + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Your Memorable word has been Reset. Please reset click on <a href=""" + resetLink + @""" target=""_blank"">Reset Memorable word</a>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password Reset Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"<br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + tempTeach.ISSchool.Name + @".
                                    </td>
                                </tr></table>";

                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                            {
                                AdminBody += reader.ReadToEnd();
                            }
                            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                            bool isSent = _EmailManagement.SendEmail(_Teacher.Email, "Reset Memorable word", AdminBody);
                            if (isSent)
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Reset memorable password link has been sent your email.');", true);
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Failed to send reset memorable password link in your email.');", true);
                        }
                        else
                        {
                            AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Email Address.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                    }
                    else if (Type.ToLower() == "parent")
                    {

                        ISStudent _Student = Session["LoggedInUser"] as ISStudent;
                        MISStudent misStudent = Session["LoggedInUserMisStudent"] as MISStudent;

                        //ISStudent _Student = DB.ISStudents.OrderBy(p => p.ID).FirstOrDefault(p => p.ParantEmail1 == Email || p.ParantEmail2 == Email && p.Active == true && p.Deleted == true);
                        if (_Student != null)
                        {
                            LoggedINType = _Student.StudentName;

                            string AdminBody = String.Empty;
                            string pwd = EncryptionHelper.Encrypt(misStudent.Password);

                            ISStudent findStudObject = DB.ISStudents.FirstOrDefault(r => r.ID == _Student.ID);

                            ISStudentConfirmLogin confirmStudent = DB.ISStudentConfirmLogins.FirstOrDefault(r => r.Email.ToLower() == misStudent.ParentEmail.ToLower());
                            confirmStudent.IsActivationID = encryptedActivationId;
                            DB.SaveChanges();

                            string resetLink = $"{loginUrl}/ResetMemorablePwd.aspx?eid={EncryptionHelper.Encrypt(misStudent.ParentEmail)}&vid={EncryptionHelper.Encrypt(EnumsManagement.USERTYPE.PARENT.ToString())}&pwd={pwd}&gid={activationId}";

                            string tblAdminBody = string.Empty;

                            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent of " + LoggedINType + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Your Memorable word has been Reset. Please reset click on <a href=""" + resetLink + @""" target=""_blank"">Reset Memorable word</a>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password Reset Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"<br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + findStudObject.ISSchool.Name + @".
                                    </td>
                                </tr></table>";

                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                            {
                                AdminBody += reader.ReadToEnd();
                            }
                            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                            bool isSent = _EmailManagement.SendEmail(misStudent.ParentEmail, "Reset Password Email", AdminBody);
                            if (isSent)
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Reset memorable password link has been sent your email.');", true);
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Failed to send reset memorable password link in your email.');", true);

                            try
                            {
                                //Response.Redirect("Login.aspx?OP=Parent", false);
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        else
                        {
                            AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Email Address.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                    }
                    else
                    {
                        AlertMessageManagement.ServerMessage(Page, "Please Select atleast one type.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            //Response.Redirect($"~/ResetMemorablePwd.aspx?uid={EncryptionHelper.Encrypt(userType)}");
        }
    }
}