using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using SchoolApp.Database;

namespace SchoolApp.ClassLibrary
{
    public static class CommonOperation
    {

        public static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
        public static string GenerateSequenceNumber()
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            string tempId = DB.ISSupports.ToList().Count.ToString();
            int id = Convert.ToInt32(tempId);
            id = id + 1;
            string autoId = "SUP" + String.Format("{0:00000}", id);
            return autoId;
        }
        public static List<ISClassYear> GetYear()
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<ISClassYear> objList = DB.ISClassYears.Where(p => p.Deleted == true).ToList();
            return objList;
            //List<string> yearlist = new List<string>();
            //for(int i=0;i<10; i++)
            //{
            //    yearlist.Add(((DateTime.Now.Year - 1) + i).ToString());
            //}
            //return yearlist;
        }
        public static string GenerateRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
        public static string GetMyTable<T>(IEnumerable<T> list, params Expression<Func<T, object>>[] fxns)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<TABLE>\n");

            sb.Append("<TR>\n");
            foreach (var fxn in fxns)
            {
                sb.Append("<TD>");
                sb.Append(GetName(fxn));
                sb.Append("</TD>");
            }
            sb.Append("</TR> <!-- HEADER -->\n");


            foreach (var item in list)
            {
                sb.Append("<TR>\n");
                foreach (var fxn in fxns)
                {
                    sb.Append("<TD>");
                    sb.Append(fxn.Compile()(item));
                    sb.Append("</TD>");
                }
                sb.Append("</TR>\n");
            }
            sb.Append("</TABLE>");

            return sb.ToString();
        }

        static string GetName<T>(Expression<Func<T, object>> expr)
        {
            var member = expr.Body as MemberExpression;
            if (member != null)
                return GetName2(member);

            var unary = expr.Body as UnaryExpression;
            if (unary != null)
                return GetName2((MemberExpression)unary.Operand);

            return "?+?";
        }

        static string GetName2(MemberExpression member)
        {
            var fieldInfo = member.Member as FieldInfo;
            if (fieldInfo != null)
            {
                var d = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (d != null) return d.Description;
                return fieldInfo.Name;
            }

            var propertInfo = member.Member as PropertyInfo;
            if (propertInfo != null)
            {
                var d = propertInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (d != null) return d.Description;
                return propertInfo.Name;
            }

            return "?-?";
        }

        public static int ExcelColumnNameToNumber(string reference)
        {

            int ci = 0;
            reference = reference.ToUpper();
            for (int ix = 0; ix < reference.Length && reference[ix] >= 'A'; ix++)
                ci = (ci * 26) + ((int)reference[ix] - 64);
            return ci;

            //if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException("columnName");

            //columnName = columnName.ToUpperInvariant();

            //int sum = 0;

            //for (int i = 0; i < columnName.Length-1; i++)
            //{
            //    sum *= 26;
            //    sum += (columnName[i] - 'A' + 1);
            //}

            //return sum;
        }

        public static string GetParentRelation1(String relation)
        {
            if (relation == "Guardian")
                return "Guardian1";
            else
                return relation;
        }

        public static string GetParentRelation2(String relation)
        {
            if (relation == "Guardian")
                return "Guardian2";
            else
                return relation;
        }

        public static void EmailManage(ISStudent _Student)
        {

            EmailManagement _EmailManagement = new EmailManagement();
            SchoolAppEntities DB = new SchoolAppEntities();

            if (DB.ISStudents.Where(p => (p.ParantEmail1.ToLower() == _Student.ParantEmail1.ToLower() || p.ParantEmail2.ToLower() == _Student.ParantEmail2.ToLower()) && p.Active == true && p.Deleted == true).Count() > 0)
            {
                string PP1body = String.Empty;
                string PP2Body = String.Empty;
                string TeacherBody = String.Empty;

                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
                string tblPP1body = string.Empty;
                string tblPP2Body = string.Empty;
                string tblTeacherBody = string.Empty;
                tblPP1body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName1 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    A new student " + _Student.StudentName + " has been added to your parent account by " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        It is important to contact the School Administartor directly if the student name mentioned above has been spelt incorrectly or wrongly. Inability to do this will imply that you will be having two accounts for one child.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Only activate this account if you think the tudent name is correct by  clicking <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + @">here</a>.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions on useful FAQs or how to use the app effctively is attached to this email or available electronically by clicking on this link
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    PP1body += reader.ReadToEnd();
                }
                PP1body = PP1body.Replace("{Body}", tblPP1body);
                _EmailManagement.SendEmail(_Student.ParantEmail1, "Parent Account Update for Student", PP1body);

                if (DB.ISStudents.Where(p => (p.ParantEmail2.ToLower() == _Student.ParantEmail1.ToLower() || p.ParantEmail2.ToLower() == _Student.ParantEmail2.ToLower()) && p.Active == true && p.Deleted == true).Count() > 0)
                {
                    tblPP2Body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName2 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    A new student " + _Student.StudentName + " has been added to your parent account by " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        It is important to contact the School Administartor directly if the student name mentioned above has been spelt incorrectly or wrongly. Inability to do this will imply that you will be having two accounts for one child.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Only activate this account if you think the tudent name is correct by  clicking <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + @">here</a>.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions on useful FAQs or how to use the app effctively is attached to this email or available electronically by clicking on this link
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        PP2Body += reader.ReadToEnd();
                    }
                    PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Parent Account Update for Student", PP2Body);
                }
                else
                {
                    tblPP2Body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName2 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    A new parent account for " + _Student.StudentName + " has been created for you by " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact the relevant school to correct the student name if incorrect. Otherwise, Click <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + ">Here</a> and use the initial password <b>" + _Student.ParantPassword1 + @"</b> to activate this student on your account
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        The parent profile oon this account has been set with the following. Being the first account created with this email, the profile information will be retained on your profile and on this child account
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Student Name : " + _Student.StudentName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email : " + _Student.ParantEmail2 + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email : " + _Student.ParantPhone2 + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        If the above is incorrect, you will be able to change this using  when you login to your account.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions on useful FAQs or how to use the app effctively is attached to this email, and can also be obtained electronically by clicking on this link
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        PP2Body += reader.ReadToEnd();
                    }
                    PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Add Student Notification", PP2Body);
                }

            }
            else
            {
                string PP1body = String.Empty;
                string PP2Body = String.Empty;
                string TeacherBody = String.Empty;

                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
                string tblPP1body = string.Empty;
                string tblPP2Body = string.Empty;
                string tblTeacherBody = string.Empty;
                tblPP1body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName1 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    A new parent account for " + _Student.StudentName + " has been created for you by " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact the relevant school to correct the student name if incorrect. Otherwise, Click <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + ">Here</a> and use the initial password <b>" + _Student.ParantPassword1 + @"</b> to activate this student on your account
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        The parent profile oon this account has been set with the following. Being the first account created with this email, the profile information will be retained on your profile and on this child account
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Student Name : " + _Student.StudentName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email : " + _Student.ParantEmail1 + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email : " + _Student.ParantPhone1 + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        If the above is incorrect, you will be able to change this using  when you login to your account.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions on useful FAQs or how to use the app effctively is attached to this email, and can also be obtained electronically by clicking on this link
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    PP1body += reader.ReadToEnd();
                }
                PP1body = PP1body.Replace("{Body}", tblPP1body);
                _EmailManagement.SendEmail(_Student.ParantEmail1, "Add Student Notification", PP1body);

                if (DB.ISStudents.Where(p => (p.ParantEmail2.ToLower() == _Student.ParantEmail1.ToLower() || p.ParantEmail2.ToLower() == _Student.ParantEmail2.ToLower()) && p.Active == true && p.Deleted == true).Count() > 0)
                {
                    tblPP2Body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName2 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    A new student " + _Student.StudentName + " has been added to your parent account by " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        It is important to contact the School Administartor directly if the student name mentioned above has been spelt incorrectly or wrongly. Inability to do this will imply that you will be having two accounts for one child.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Only activate this account if you think the tudent name is correct by  clicking <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + @">here</a>.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions on useful FAQs or how to use the app effctively is attached to this email or available electronically by clicking on this link
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        PP2Body += reader.ReadToEnd();
                    }
                    PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Parent Account Update for Student", PP2Body);
                }
                else
                {
                    tblPP2Body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName2 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    A new parent account for " + _Student.StudentName + " has been created for you by " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact the relevant school to correct the student name if incorrect. Otherwise, Click <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + ">Here</a> and use the initial password <b>" + _Student.ParantPassword1 + @"</b> to activate this student on your account
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        The parent profile oon this account has been set with the following. Being the first account created with this email, the profile information will be retained on your profile and on this child account
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Student Name : " + _Student.StudentName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email : " + _Student.ParantEmail2 + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email : " + _Student.ParantPhone2 + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        If the above is incorrect, you will be able to change this using  when you login to your account.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions on useful FAQs or how to use the app effctively is attached to this email, and can also be obtained electronically by clicking on this link
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        PP2Body += reader.ReadToEnd();
                    }
                    PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Add Student Notification", PP2Body);
                }
            }

        }

        public static void NewEmailChangeNotification(string parentName, string newEmailId, string newPwd)
        {

            EmailManagement _EmailManagement = new EmailManagement();
            SchoolAppEntities DB = new SchoolAppEntities();


            string PP1body = String.Empty;
            string PP2Body = String.Empty;
            string TeacherBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string tblPP1body = string.Empty;
            string tblPP2Body = string.Empty;
            string tblTeacherBody = string.Empty;
            tblPP1body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + parentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    A new parent account has been changed by you.<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact the relevant school if you found any missing students by login new parent email, Click <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + ">Here</a> and use the new password <b>" + newPwd + @"</b> to activate your account.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions on useful FAQs or how to use the app effctively is attached to this email, and can also be obtained electronically by clicking on this link
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                PP1body += reader.ReadToEnd();
            }
            PP1body = PP1body.Replace("{Body}", tblPP1body);
            _EmailManagement.SendEmail(newEmailId, "Email Change Notification", PP1body);
        }

        public static void OldEmailChangeNotification(string parentName, string newEmailId, string oldEmailId)
        {

            EmailManagement _EmailManagement = new EmailManagement();
            SchoolAppEntities DB = new SchoolAppEntities();


            string PP1body = String.Empty;
            string PP2Body = String.Empty;
            string TeacherBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);            
            string tblPP1body = string.Empty;
            string tblPP2Body = string.Empty;
            string tblTeacherBody = string.Empty;
            tblPP1body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + parentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Now, onwards, You are no longer with " + _School.Name + @". This account has no children anymore.<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Your all students migrated to new email. Here is " + newEmailId + @".
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact the relevant school if you haven't done to changed your profile setting.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions on useful FAQs or how to use the app effctively is attached to this email, and can also be obtained electronically by clicking on this link
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                PP1body += reader.ReadToEnd();
            }
            PP1body = PP1body.Replace("{Body}", tblPP1body);
            _EmailManagement.SendEmail(oldEmailId, $"No longer with {_School.Name}", PP1body);
        }

        public static bool IsValidEmailId(string emailId)
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(emailId))
            {
                Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                isValid = regex.IsMatch(emailId.Trim());
            }

            return isValid;
        }
    }
}
