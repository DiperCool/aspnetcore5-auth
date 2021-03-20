using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Web.Api.Filters;
using Web.Infrastructure.Auth;
using Web.Infrastructure.EmailSender;
using Web.Infrastructure.ExtensionMethods;
using Web.Infrastructure.JWT;
using Web.Infrastructure.Validations.ValidationUser;
using Web.Models.Entity;
using Web.Models.Models;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Web.Api.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        IJWT _JWT; 
        IValidationUser _validation;
        IAuth _auth;
        IEmailSender _emailSender;
        IWebHostEnvironment _appEnvironment;

        public AuthController(IJWT jWT, IValidationUser validation, IAuth auth, IEmailSender emailSender, IWebHostEnvironment appEnvironment)
        {
            _JWT = jWT;
            _validation = validation;
            _auth = auth;
            _emailSender = emailSender;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        [ModelValidation]
        public async Task<IActionResult> Register([FromBody] RegistarationModel model){
            if(await _validation.userIsExist(model.Email)){
                ModelState.AddModelError("LoginException","This mail exist");
                return BadRequest(ModelState);
            }

            User user = new User() {Email = model.Email, Password = BCrypt.Net.BCrypt.HashPassword(model.Password), FirstName=model.FirstName, LastName=model.LastName};
            await _auth.CreateUser(user);
            string guid= await _auth.CreateGuidForConfirmEmail(user.Id);
            await _emailSender.SendConfirmEmail(model.Email,string.Format("{0}/api/auth/confirmEmail?guid={1}", 
                                                            HttpContext.Request.GetFullUrl(),guid));
            return await GenerateTokens(user);
        }

        [ModelValidation]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            User user = await _auth.GetUser(model.Email);
            if (user==null || (user.Password==null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password)))
            {
                return BadRequest("Email or password is incorrect");
            }
            return await GenerateTokens(user);
            
        }
        [ModelValidation]
        [HttpPost]
        public async Task<IActionResult>  Refresh([FromBody] ReturnTokensModel model)
        {
            var principal = _JWT.GetPrincipalFromExpiredToken(model.Token);
            var userId = principal.Claims.GetId();
            var user= await _auth.GetUser(userId);
            var savedRefreshToken = user.RefreshToken; //retrieve the refresh token from a data store
            if (savedRefreshToken != model.RefreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newRefreshToken = _JWT.GenerateRefreshToken();
            await _auth.SaveRefreshToken(userId, newRefreshToken);

            return await GenerateTokens(user);
        }

        [HttpPost]
        public async Task<IActionResult> signinGoogle([FromBody]UserView userView){

            Payload payload = await GoogleJsonWebSignature.ValidateAsync(userView.tokenId, new GoogleJsonWebSignature.ValidationSettings());
            User user = await _auth.GetUser(payload.Email);
            if(user==null)
            {
                User u= new User{Email=payload.Email, FirstName=payload.GivenName, LastName= payload.FamilyName, EmailIsVerified=true};
                u= await _auth.CreateUser(u);
                return await GenerateTokens(u);
            }
            return await GenerateTokens(user);
        }

        [Authorize]
        [HttpGet]
        public IActionResult getLogin(){
            return Ok(User.Identity.Name);
        }
        [HttpGet]
        public async Task<IActionResult> confirmEmail(string guid)
        {
            await _auth.ConfirmEmail(guid);
            return Ok();
        }



        private async Task<IActionResult> GenerateTokens(User user)
        {
            var refreshToken = _JWT.GenerateRefreshToken();
            await _auth.SetRefreshToken(user.Id, refreshToken);
            IEnumerable<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"),
            };
            string token = _JWT.GenerateToken(claims);
            return Ok(new ReturnTokensModel(token, refreshToken));
        }
    }
}
