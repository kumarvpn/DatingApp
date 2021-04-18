using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extenstions
{
    public static class ApplicationIdentityExtentions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration config)
        {
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>{

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters (){

                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer=false,
                    ValidateAudience=false


                    };



            });

            return  services;
        }
    }
}