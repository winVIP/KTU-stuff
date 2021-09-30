using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using System.Security.Claims;
using Saitynai.Models;

namespace Saitynai.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        public Object GetToken(Login login)
        {
            if(login != null)
            {
                if (login.username == "admin" && login.password == "admin")
                {
                    string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
                    var issuer = "http://saitynaiapi.azurewebsites.net";  //normally this will be your site URL    

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    //Create a List of Claims, Keep claims name short    
                    var permClaims = new List<Claim>();
                    permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    permClaims.Add(new Claim("valid", "1"));
                    permClaims.Add(new Claim("userid", "0"));
                    permClaims.Add(new Claim("name", "admin"));

                    //Create Security Token object by giving required parameters    
                    var token = new JwtSecurityToken(issuer, //Issure    
                                    issuer,  //Audience    
                                    permClaims,
                                    expires: DateTime.Now.AddDays(1),
                                    signingCredentials: credentials);
                    var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                    return new { data = jwt_token };
                }
                else if(login.username == "user" && login.password == "user")
                {
                    string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
                    var issuer = "http://saitynaiapi.azurewebsites.net";  //normally this will be your site URL    

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    //Create a List of Claims, Keep claims name short    
                    var permClaims = new List<Claim>();
                    permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    permClaims.Add(new Claim("valid", "2"));
                    permClaims.Add(new Claim("userid", "0"));
                    permClaims.Add(new Claim("name", "user"));

                    //Create Security Token object by giving required parameters    
                    var token = new JwtSecurityToken(issuer, //Issure    
                                    issuer,  //Audience    
                                    permClaims,
                                    expires: DateTime.Now.AddDays(1),
                                    signingCredentials: credentials);
                    var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                    return new { data = jwt_token };
                }
            }
            else
            {
                return "no login credentials have been inputed";
            }
            return "non admin users don't need to login";
        }



        //[Route("api/values/getname1")]
        //[HttpPost]
        //public String GetName1()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var identity = User.Identity as ClaimsIdentity;
        //        if (identity != null)
        //        {
        //            IEnumerable<Claim> claims = identity.Claims;
        //        }
        //        return "Valid";
        //    }
        //    else
        //    {
        //        return "Invalid";
        //    }
        //}

        //[Route("api/values/getname2")]
        //[Authorize]
        //[HttpPost]
        //public Object GetName2()
        //{
        //    var identity = User.Identity as ClaimsIdentity;
        //    if (identity != null)
        //    {
        //        IEnumerable<Claim> claims = identity.Claims;
        //        var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
        //        return new
        //        {
        //            data = name
        //        };

        //    }
        //    return null;
        //}
    }
}
