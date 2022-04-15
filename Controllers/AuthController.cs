using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PerfectPolicyQuiz.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PerfectPolicyQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        public IConfiguration _config;
        public PerfectPolicyQuizContext _context;

        public AuthController(IConfiguration config, PerfectPolicyQuizContext context)
        {
            _config = config;
            _context = context;
        }

        private UserInfo GetUser(string userName, string password)
        {
            UserInfo user = _context.Users.FirstOrDefault(u => u.Username == userName);
            //UserInfo user2 = _context.Users.Where(u => u.Username == userName).FirstOrDefault();

            // if a matching user was found
            if (user != null && user.Password.Equals(password))
            {
                // Return the user
                return user;
            }
            return null;
        }


        [HttpPost]
        [Route("GenerateToken")]
        public IActionResult GenerateToken(UserInfo _userData)
        {
            if (_userData != null && _userData.Username != null && _userData.Password != null)
            {
                var user = GetUser(_userData.Username, _userData.Password);

                if (user != null)
                {
                    var claims = new[] {
                    // JWT Subject
                    new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                    // JWT ID
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    // JWT Date/Time
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    // JWT User ID
                    new Claim("Id", user.UserInfoId.ToString()),
                    // JWT UserName
                    new Claim("UserName", user.Username)
                };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    // use the generated key to generate new Signing Credentials
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Generate a new token based on all of the details generated so far
                    var token = new JwtSecurityToken(
                        _config["Jwt:Issuer"],
                        _config["Jwt:Audience"],
                        claims,
                        // How long the JWT will be valid for
                        expires: DateTime.UtcNow.AddDays(2),
                        signingCredentials: signIn);

                    // Return the Token via JSON
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }

            else
            {
                return BadRequest();
            }

        }
        /*
        
          [HttpPost]

          public IActionResult SetColour(IFormCollection collection)
          {
              //Retrieve the backcolour from the collection
              var backColour = collection["colourPicker"].ToString();

              // Retrieve the headercolour from the collection
              var headerColour = collection["headerColourPicker"].ToString();
                  []
              // Retrieve the appname from the collection

              var appName = collection["titlePicker"].ToString();

              // Save the retrieved values in the session
              HttpContext.Session.SetString("backColor", backColour);
              HttpContext.Session.SetString("headerColor", headerColour);
              HttpContext.Session.SetString("@appName", appName);

              // Refresh the page
             }
              */




    }
}
