using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[assembly: OwinStartup(typeof(SchoolApp.API.Startup))]

namespace SchoolApp.API
{
    public class Startup
    {        
        public void Configuration(IAppBuilder app)
        {       
            
            app.UseJwtBearerAuthentication(
                
                new JwtBearerAuthenticationOptions
                {  
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://www.api.thestmanager.com", //some string, normally web url,  
                        ValidAudience = "http://www.api.thestmanager.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wwwapithestmanager123"))
                    }
                });
        }
    }
}
