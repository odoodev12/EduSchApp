using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolApp.ClassLibrary
{
    public class ParentManagement
    {

        public MISStudent ParentLogin(string Email, string Password)
        {
            string password = EncryptionHelper.Encrypt(Password);
            SchoolAppEntities DB = new SchoolAppEntities();
            ISStudent Obj = DB.ISStudents.OrderBy(p => p.ID).FirstOrDefault(p => ((p.ParantEmail1 == Email && p.ParantPassword1 == password) || (p.ParantEmail2 == Email && p.ParantPassword2 == password)) && p.Active == true && p.Deleted == true);
            if (Obj != null)
            {
                MISStudent ObjM = new MISStudent
                {
                    ID = Obj.ID,
                    StudentName = Obj.StudentName,
                    StudentNo = Obj.StudentNo,
                    Photo = Obj.Photo,
                    ClassID = Obj.ClassID,
                    ParentEmail = Obj.ParantEmail1 == Email ? Obj.ParantEmail1 : Obj.ParantEmail2,
                    ParentName = Obj.ParantEmail1 == Email ? Obj.ParantName1 : Obj.ParantName2,
                    Password = Obj.ParantEmail1 == Email ? EncryptionHelper.Decrypt(Obj.ParantPassword1) : EncryptionHelper.Decrypt(Obj.ParantPassword2),
                    ParentPhone = Obj.ParantEmail1 == Email ? Obj.ParantPhone1 : Obj.ParantPhone2,
                    ParentRelation = Obj.ParantEmail1 == Email ? Obj.ParantRelation1 : Obj.ParantRelation2,
                    SchoolID = Obj.SchoolID,
                };
                return ObjM;
            }
            else
            {
                return null;
            }

        }

        public List<MISStudent> ParentList(int SchoolID)
        {

            SchoolAppEntities DB = new SchoolAppEntities();

            List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.SchoolID == SchoolID && p.Deleted == true && p.Active == true).ToList()
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            StudentName = item.StudentName,
                                            ParantEmail1 = item.ParantEmail1,
                                            ParantEmail2 = item.ParantEmail2,
                                            SchoolName = item.ISSchool.Name,
                                            Photo = item.Photo,
                                            ParantName1 = item.ParantName1,
                                            ParantName2 = item.ParantName2,
                                            ClassID = item.ClassID,
                                            StartDate = item.StartDate,
                                            EndDate = item.EndDate,
                                        }).ToList();
            return objList;

        }
        public List<MISStudent> StudentListByParent(string Email)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISStudent> objList = (from item in DB.ISStudents.Where(p => (p.ParantEmail1 == Email || p.ParantEmail2 == Email) && p.Active == true && p.Deleted == true).ToList()
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            SchoolID = item.SchoolID,
                                            StudentName = item.StudentName,
                                            SchoolName = item.ISSchool.Name,
                                            SchoolNumber = item.ISSchool.PhoneNumber,
                                            Photo = item.Photo,
                                            PickUpAverage = DB.ISPickups.Where(p => p.StudentID == item.ID).Count(),
                                        }).ToList();
            return objList;
        }
        public void NoImageEmailManage(ISStudent _Student)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            EmailManagement _EmailManagement = new EmailManagement();
            string PP1body = String.Empty;
            string PP2Body = String.Empty;
            string TeacherBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == _Student.SchoolID);
            string tblPP1body = string.Empty;
            string tblPP2Body = string.Empty;
            string tblTeacherBody = string.Empty;
            tblPP1body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Our database records show that a child, " + _Student.StudentName + @" has no identifiable image. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please update the record by uploading the correct/latest image for this child.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Failure to do so will result in  you receiving this notification in the future, and the  child pickup process could also become more difficult.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please use the FAQs, videos and guides on your account or contact the School Administrator if you experience any difficulty in carrying out this task.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
            //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            //{

            //}

            //DB.

            //string path = HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html");
            //string content = System.IO.File.ReadAllText(path);
            string content = DB.EmailTemplates.FirstOrDefault(r => r.Id == 1).value;
            PP1body += content;
            PP1body = PP1body.Replace("{Body}", tblPP1body);
            _EmailManagement.SendEmail(_Student.ParantEmail1, "No Child Image", PP1body);

            if (!String.IsNullOrEmpty(_Student.ParantName2) && !String.IsNullOrEmpty(_Student.ParantEmail2))
            {
                tblPP2Body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Our database records show that a child, " + _Student.StudentName + @" has no identifiable image. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please update the record by uploading the correct/latest image for this child.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Failure to do so will result in  you receiving this notification in the future, and the  child pickup process could also become more difficult.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please use the FAQs, videos and guides on your account or contact the School Administrator if you experience any difficulty in carrying out this task.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";

                //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                //{
                PP2Body += content;
                //}
                PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                _EmailManagement.SendEmail(_Student.ParantEmail2, "No Child Image", PP2Body);
            }
        }

    }
}
