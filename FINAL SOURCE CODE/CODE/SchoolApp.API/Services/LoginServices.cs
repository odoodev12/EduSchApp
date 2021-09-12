using Microsoft.IdentityModel.Tokens;
using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using static SchoolApp.ClassLibrary.EnumsManagement;

namespace SchoolApp.API.Services
{
    public class LoginServices
    {
        public SchoolAppEntities entity;

        public LoginServices()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        #region

        public ReturnResponceWithTokem AdminLogin(string username, string password)
        {
            try
            {
                String pass = EncryptionHelper.Encrypt(password);
                //string UName = "";
                //int UType = 0;

                var obj = entity.ISAdminLogins.Where(p => p.Email == username && p.Pass == pass && p.Active == true && p.Deleted == true)
                    .Select(s => new AdminDetails
                    {
                        Email = s.Email,
                        Deleted = s.Deleted,
                        Active = s.Active,
                        FullName = s.FullName,
                        ISActivated = s.ISActivated,
                        MemorableQueAnswer = s.MemorableQueAnswer,
                        USERTYPE = USERTYPE.ADMIN,
                        Id = s.ID,
                    }).FirstOrDefault();               

                if (obj != null)
                {
                    /////Here if user is not activated.
                    if (obj.ISActivated == null || obj.ISActivated.Value == false)
                    {
                        return new ReturnResponceWithTokem("You are not activated user, Login with Web application for activated your user!");
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(obj.MemorableQueAnswer))
                        {
                            obj.MemorableQueAnswer = EncryptionHelper.Decrypt(obj.MemorableQueAnswer);
                        }

                        return new ReturnResponceWithTokem(obj, GetToken(), new[] { "" });
                    }

                }
                else if (entity.ISOrganisationUsers.Where(p => p.Email == username && p.Password == pass && p.Active == true && p.Deleted == true).Count() > 0)
                {
                    var objs = entity.ISOrganisationUsers.Where(p => p.Email == username && p.Password == pass && p.Active == true && p.Deleted == true)
                        .Select(s => new AdminDetails
                        {
                            Email = s.Email,
                            Deleted = s.Deleted,
                            Active = s.Active,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            ISActivated = s.ISActivated,
                            MemorableQueAnswer = s.MemorableQueAnswer,
                            USERTYPE = USERTYPE.ADMIN,
                            Id = s.ID,
                            StartDate = s.StartDate,
                            EndDate = s.EndDate

                        }).FirstOrDefault();

                    if (objs.StartDate.Value.Date <= DateTime.Now.Date && objs.EndDate.Value.Date >= DateTime.Now.Date)
                    {
                        ///Here if user is not activated.
                        if (objs.ISActivated == null || objs.ISActivated.Value == false)
                        {
                            return new ReturnResponceWithTokem("You are not activated user, Login with Web application for activated your user!");
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(objs.MemorableQueAnswer))
                            {
                                objs.MemorableQueAnswer = EncryptionHelper.Decrypt(objs.MemorableQueAnswer);
                            }

                            return new ReturnResponceWithTokem(objs, GetToken(), new[] { "" });
                        }
                    }
                    else
                    {
                        return new ReturnResponceWithTokem("User access has expired, Please contact to administrator!");
                    }
                }
                else
                {
                    return new ReturnResponceWithTokem("No records found, Invalid UserName and Password!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponceWithTokem(ex.Message);
            }
        }
        public ReturnResponceWithTokem TeacherLogin(string username, string Password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(Password))
                {
                    TeacherManagement objTeacherManagement = new TeacherManagement();
                    string pass = EncryptionHelper.Encrypt(Password);

                    if (!entity.ISTeachers.Any(r => r.Email.ToLower() == username.ToLower() && r.Active == true && r.Deleted == true))
                    {
                        return new ReturnResponceWithTokem("Teacher Account email does not exists. Please contact School Admin!");
                    }
                    else if (!entity.ISTeachers.Any(r => r.Email.ToLower() == username.ToLower() && r.Password == pass && r.Active == true && r.Deleted == true))
                    {
                        return new ReturnResponceWithTokem("You have enterted wrong password. Please contact School Admin!");
                    }
                    else
                    {
                        var ObjTeacher = objTeacherManagement.TeacherLogin(username, Password);
                        if (ObjTeacher != null)
                        {
                            if (ObjTeacher.ISActivated == null || ObjTeacher.ISActivated.Value == false)
                            {
                                return new ReturnResponceWithTokem("You are not activated user, Login with Web application for activated your user!");
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(ObjTeacher.MemorableQueAnswer))
                                {
                                    ObjTeacher.MemorableQueAnswer = EncryptionHelper.Decrypt(ObjTeacher.MemorableQueAnswer);
                                }

                               return new ReturnResponceWithTokem(ObjTeacher, GetToken(), new[] { "" });
                            }
                        }
                        else
                        {
                            return new ReturnResponceWithTokem("No record found, Invalid username or password!");
                        }
                    }
                }
                else
                {
                    return new ReturnResponceWithTokem("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponceWithTokem(ex.Message);
            }
        }
        public ReturnResponceWithTokem SchoolLogin(string UserName, string Password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
                {
                    SchoolManagement objSchoolManagement = new SchoolManagement();
                    string pass = EncryptionHelper.Encrypt(Password);

                    if (entity.ISSchools.Any(r => r.AdminEmail.ToLower() == UserName.ToLower() && r.Active == false))
                    {
                        return new ReturnResponceWithTokem("School Account email has been deactivated by admin. Please contact School Admin!");
                    }
                    else if (!entity.ISSchools.Any(r => r.AdminEmail.ToLower() == UserName.ToLower() && r.Deleted == true && r.Active == true))
                    {
                        return new ReturnResponceWithTokem("School Account email does not exists. Please contact School Admin!");
                    }
                    else if (!entity.ISSchools.Any(r => r.AdminEmail.ToLower() == UserName.ToLower() && r.Password == pass && r.Deleted == true && r.Active == true))
                    {
                        return new ReturnResponceWithTokem("You have entered wrong password. Please contact School Admin.!");
                    }
                    else
                    {
                        var  _School = entity.ISSchools.SingleOrDefault(p => p.AdminEmail == UserName && p.Password == pass && p.Deleted == true && p.Active == true && (p.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.InActive || p.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.InProcess));
                        if (_School != null)
                        {
                            if (_School.ActivationDate.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                            {
                                _School.AccountStatusID = (int?)EnumsManagement.ACCOUNTSTATUS.Active;
                                entity.SaveChanges();
                            }
                        }
                        var ObjISSchool = objSchoolManagement.SchoolLogin(UserName, Password);

                        if (ObjISSchool != null)
                        {
                            if (ObjISSchool.ISActivated == null || ObjISSchool.ISActivated.Value == false)
                            {
                                return new ReturnResponceWithTokem("You are not activated user, Login with Web application for activated your user!");
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(ObjISSchool.MemorableQueAnswer))
                                {
                                    ObjISSchool.MemorableQueAnswer = EncryptionHelper.Decrypt(ObjISSchool.MemorableQueAnswer);
                                }

                                return new ReturnResponceWithTokem(ObjISSchool, GetToken(), new[] { "" });
                            }
                        }
                        else
                        {
                            return new ReturnResponceWithTokem("No record found, Invalid username or password!");
                        }
                    }
                }
                else
                {
                    return new ReturnResponceWithTokem("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponceWithTokem(ex.Message);
            }
        }
        public ReturnResponceWithTokem ParentLogin(string UserName, string Password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
                {
                    ParentManagement objParentManagement = new ParentManagement();
                    string password = EncryptionHelper.Encrypt(Password);

                    bool isParantEmail1 = entity.ISStudents.Any(r => r.ParantEmail1.ToLower() == UserName.ToLower() && r.Active == true && r.Deleted == true);
                    bool isParantEmail2 = entity.ISStudents.Any(r => r.ParantEmail2.ToLower() == UserName.ToLower() && r.Active == true && r.Deleted == true);

                    bool isemailIdPwd1 = entity.ISStudents.Any(r => r.ParantEmail1.ToLower() == UserName.ToLower() && r.ParantPassword1 == password && r.Active == true && r.Deleted == true);
                    bool isemailIdPwd2 = entity.ISStudents.Any(r => r.ParantEmail2.ToLower() == UserName.ToLower() && r.ParantPassword2 == password && r.Active == true && r.Deleted == true);

                    if (!isParantEmail1 && !isParantEmail2)
                    {
                        return new ReturnResponceWithTokem("Parent Account email does not exists. Please contact School Admin!");
                    }
                    else if (!isemailIdPwd1 && !isemailIdPwd2)
                    {
                        return new ReturnResponceWithTokem("You have enterted wrong password. Please contact School Admin!");
                    }
                    else
                    {
                        var ObjStudent = objParentManagement.ParentLogin(UserName, Password);
                        if (ObjStudent != null)
                        {
                            var stdLogin = entity.ISStudentConfirmLogins.FirstOrDefault(r => r.Email.ToLower() == UserName.ToLower());
                            string eid = EncryptionHelper.Encrypt(UserName);

                            if (stdLogin.ISActivated == null || stdLogin.ISActivated.Value == false)
                            {
                                return new ReturnResponceWithTokem("You are not activated user, Login with Web application for activated your user!");
                            }
                            else
                            {
                                //var misStudent = objParentManagement.ParentLogin(UserName, password);
                                if (!string.IsNullOrWhiteSpace(stdLogin.MemorableQueAnswer))
                                {
                                    stdLogin.MemorableQueAnswer = EncryptionHelper.Decrypt(stdLogin.MemorableQueAnswer);
                                }

                                return new ReturnResponceWithTokem(stdLogin, GetToken(), new[] { "" });
                            }
                        }
                        else
                        {
                            return new ReturnResponceWithTokem("No record found, Invalid username or password!");
                        }
                    }
                }
                else
                {
                    return new ReturnResponceWithTokem("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponceWithTokem(ex.Message);
            }
        }
        private string GetToken()
        {
            string key = "wwwapithestmanager123";
            var issuer = "http://www.api.thestmanager.com";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "SchoolAPI"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }


        #endregion
    }
}