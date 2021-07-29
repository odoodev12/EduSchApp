using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class TokenManagement
    {
        public string GenerateToken(string IMEINo)
        {
            try
            {
                SchoolAppEntities DB = new SchoolAppEntities();
                ISAuthToken obj = new ISAuthToken();
                obj.IMEINO = IMEINo;
                obj.Token = Guid.NewGuid().ToString();
                obj.Active = true;
                obj.Deleted = true;
                DB.ISAuthTokens.Add(obj);
                DB.SaveChanges();
                return obj.Token;
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
                return "";
            }

        }
        public bool CheckToken(string Token)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            try
            {
                return DB.ISAuthTokens.Any(p => p.Token == Token);
                    
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
                return false;
            }

        }
    }
}
