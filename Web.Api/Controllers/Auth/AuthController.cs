using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Web.Api.Filters;
using Web.Infrastructure.Auth;
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
        public AuthController(IJWT jwt, IValidationUser validation,IAuth auth)
        {
            _JWT = jwt;
            _validation = validation;
            _auth = auth;
        }
        [ModelValidation]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistarationModel model){
            if(await _validation.userIsExist(model.Email)){
                ModelState.AddModelError("LoginException","This mail exist");
                return BadRequest(ModelState);
            }

            User user = new User() {Email = model.Email, Password = BCrypt.Net.BCrypt.HashPassword(model.Password), FirstName=model.FirstName, LastName=model.LastName};
            await _auth.CreateUser(user);
            return GenerateTokens(user);
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
            return GenerateTokens(user);
            
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

            return GenerateTokens(user);
        }

        [HttpPost]
        public async Task<IActionResult> signinGoogle([FromBody]UserView userView){

            Payload payload = await GoogleJsonWebSignature.ValidateAsync(userView.tokenId, new GoogleJsonWebSignature.ValidationSettings());
            User user = await _auth.GetUser(payload.Email);
            if(user==null)
            {
                User u= new User{Email=payload.Email, FirstName=payload.GivenName, LastName= payload.FamilyName, EmailIsVerified=true};
                u= await _auth.CreateUser(u);
                return GenerateTokens(u);
            }
            return GenerateTokens(user);
        }

        [Authorize]
        [HttpGet]
        public IActionResult getLogin(){
            return Ok(User.Identity.Name);
        }
        private IActionResult GenerateTokens(User user)
        {
            var refreshToken = _JWT.GenerateRefreshToken();
            _auth.SetRefreshToken(user.Id, refreshToken);
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