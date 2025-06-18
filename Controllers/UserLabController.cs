using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLabController : ControllerBase
    {
        public readonly IUserLabService _userLabService;
        public readonly IConfiguration _configuration;
        public UserLabController(IUserLabService userLabService,IConfiguration configuration)
        { 
            _userLabService = userLabService;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("login")]
        public UserLabLoginResponse VerifyLogin(UserLabVerificationInput vLoginInput)
        {
            UserLabLoginResponse res = new UserLabLoginResponse();
            try
            {
                res = _userLabService.VerifyLogin(vLoginInput);
                res.token = GenerateJwtToken(res.data);
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        [HttpPost]
        [Route("userlist")]
        public UserLabLoginResponse List()
        {
            UserLabLoginResponse res = new UserLabLoginResponse();
            List<UserLab> user = new List<UserLab>();
            try
            {


                user = _userLabService.GetAllUsers();

                res.flag = 1;
                res.message = "Success";
                //res.data = clinician;
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
        private string GenerateJwtToken(UserLab user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.USER_NAME),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.USER_ID.ToString()),
            new Claim(ClaimTypes.Role, "Admin"),};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
