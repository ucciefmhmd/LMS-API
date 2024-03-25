using LMS.BL.DTO;
using LMS.DAL.Database;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(JwtOptions jwtOptions , LMSContext db) : ControllerBase
    {
        [HttpPost("auth")]
        public ActionResult<string> AuthenticationUsers(Users users)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                //Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                    SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                    new Claim(ClaimTypes.Email, users.Email),
                    new Claim(ClaimTypes.Role, users.Role),
                    
                })
        };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return new JsonResult(accessToken);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid login request");

            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null || user.Password != model.Password)
                return NotFound(new { message = "Email or Password is Incorrect" });
            
            if (user.Role != "subadmin" && user.Role != "admin")
                return Unauthorized( new { message = "You don't have necessary permissions to access this resource." });
        

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtOptions.SigningKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("Photo", user.Photo)
                }),
                //Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            return Ok(new { Message = "Login successful", Token = accessToken});
        }


        [HttpPost("signin")]
        public async Task<IActionResult> Signin(LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid login request");

            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null || user.Password != model.Password)
                return NotFound(new { message = "Email or Password is Incorrect" });

            var token = GenerateJwtToken(user);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (user.Role == "student")
                return await StudentAction(user, accessToken);

            else if (user.Role == "instructor")
                return await InstructorAction(user, accessToken);

            else if (user.Role == "admin" || user.Role == "subadmin")
                return await AdminAction(user, accessToken);

            else
                return Unauthorized(new { message = "You don't have necessary permissions to access this resource." });

        }

        private JwtSecurityToken GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtOptions.SigningKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("Photo", user.Photo)
                }),
                //Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.CreateToken(tokenDescriptor) as JwtSecurityToken;
        }

        private async Task<IActionResult> StudentAction(Users user, string accessToken)
        {
            var stdData = await db.Students.FirstOrDefaultAsync(i => i.userID == user.Id);

            if (stdData == null)
                return BadRequest("Student data not found");
            return Ok(new { Message = "Student logged in successfully", Token = accessToken });
        }

        private async Task<IActionResult> InstructorAction(Users user, string accessToken)
        {
            var instData = await db.Instructors.FirstOrDefaultAsync(i => i.userID == user.Id);

            if (instData == null)
                return BadRequest("Instructor data not found");

            return Ok(new { Message = "Instructor logged in successfully", Token = accessToken });
        }

        private async Task<IActionResult> AdminAction(Users user, string accessToken)
        {
            var adminData = await db.Users.FirstOrDefaultAsync(i => i.Id == user.Id);

            if (adminData == null)
                return BadRequest("Admin data not found");

            return Ok(new { Message = "Admin or Subadmin logged in successfully", Token = accessToken });
        }









    }




}

