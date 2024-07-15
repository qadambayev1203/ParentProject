using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Model;
using Entities.Model.AnyClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace TSTUWebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IOptions<AppSettings> appSettings;
        private readonly IMapper mapper;
        private readonly JwtSecurityTokenHandler securityTokenHandler;
        private readonly ILogger<UserController> _logger;
        private UserAuthInfoDTO authInfo;
        private User user;



        public UserController(IRepositoryManager repositoryManager, IOptions<AppSettings> appSettings, IMapper mapper, ILogger<UserController> logger)
        {
            this.repositoryManager = repositoryManager;
            this.appSettings = appSettings;
            this.mapper = mapper;
            this.securityTokenHandler = new JwtSecurityTokenHandler();
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserAuthInfoDTO>> LoginAsync([FromBody] UserCredentials credentials, CancellationToken cancelationToken)
        {
            authInfo = new UserAuthInfoDTO();
            if (credentials == null)
            {
                return BadRequest("No data");
            }
            credentials.Password = PasswordManager.EncryptPassword((credentials.Login + credentials.Password).ToString());


            user = await repositoryManager.User.LoginAsync(credentials.Login, credentials.Password, false, cancelationToken);
            if (user != null)
            {
                _logger.LogInformation($"Get By Login and Password - User " + JsonConvert.SerializeObject(credentials));
                try
                {
                    var key = Encoding.ASCII.GetBytes(appSettings.Value.SecretKey);
                    var tokenDescriptoir = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(
                            new Claim[]
                            {
                            new Claim("UserId", user.id.ToString()),
                            new Claim(ClaimTypes.NameIdentifier, user.login.ToString()),
                            new Claim(ClaimTypes.Role, user.user_type_.type.ToString()),
                            }
                           ),
                        Expires = DateTime.UtcNow.AddDays(20),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    };
                    var securityToken = securityTokenHandler.CreateToken(tokenDescriptoir);
                    authInfo.Token = securityTokenHandler.WriteToken(securityToken);
                    authInfo.UserDetails = mapper.Map<UserDTO>(user);

                    authInfo.UserDetails.UserType = user.user_type_.type;
                }
                catch { }
            }
            if (string.IsNullOrEmpty(authInfo?.Token))
            {
                return Unauthorized("Error login or password");
            }

            SessionClass.token = "Bearer " + authInfo.Token;
            SessionClass.id = authInfo.UserDetails.Id;
            return Ok(authInfo);
        }

        [Authorize]
        [HttpGet("verification")]
        public IActionResult TokenCheckModel()
        {
            try
            {
                string token1 = HttpContext.Request.Headers["Authorization"].ToString();
                int prefixLength = "Bearer ".Length;
                string token = token1.Substring(prefixLength);
                if (string.IsNullOrEmpty(token))
                    return BadRequest(); 

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Value.SecretKey);


                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                                              
                var check = principal.Identity.IsAuthenticated;

                if (!check)
                {
                    return StatusCode(401);
                }

                TokenVerify tokenVerify = new TokenVerify()
                {
                    verification = true
                };
                return Ok(tokenVerify);

            }
            catch
            {
                _logger.LogInformation($"invalid token");
                return StatusCode(401);
            }
        }

    }

}
