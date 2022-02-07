using GringottsBank.Service.DTO;
using GringottsBank.Service.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;

namespace GringottsBank.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<LoginController> _logger;

        public LoginController(JwtSettings jwtSettings,ILogger<LoginController> logger)
        {
            _jwtSettings = jwtSettings;
            _logger = logger;
        }
        private IEnumerable<Users> logins = new List<Users>() {
            new Users() {
                    Id = Guid.NewGuid(),
                        EmailId = "admin@gmail.com",
                        UserName = "Admin",
                        Password = "Admin",
                },
                new Users() {
                    Id = Guid.NewGuid(),
                        EmailId = "admin@gmail.com",
                        UserName = "User1",
                        Password = "Admin",
                }
        };
        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetToken(UsersLoginDTO userLogins)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = logins.Any(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = logins.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelper.GenTokenkey(new UserTokens()
                    {
                        EmailId = user.EmailId,
                        GuidId = Guid.NewGuid(),
                        UserName = user.UserName,
                        Id = user.Id,
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("wrong password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get token");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        /// <summary>
        /// Get List of UserAccounts
        /// </summary>
        /// <returns>List Of UserAccounts</returns>
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetList()
        {
            return Ok(logins);
        }
    }
}
