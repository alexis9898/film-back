using BLL.Interfaces;
using BLL.Model;
using DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class AccountService:IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;   //userManager => context 
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> SignUp(SignUpModel signUpModel)
        {
            var user = new AppUser()
            {
                UserName = signUpModel.UserName,
                
            };

            return await _userManager.CreateAsync(user,signUpModel.Password);
        }

        public async Task<UserModel> LoginAsync(SignInModel signInModel)
        {
            var result= await _signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password,false,false);

            if (!result.Succeeded)
            {
                return null;
            }
            var user = await _userManager.FindByNameAsync(signInModel.UserName);
            var id = user.Id;
            var token = generateToken(signInModel.UserName);
            var _token = new JwtSecurityTokenHandler().WriteToken(token);
            var exp = token.ValidTo.Date;

            return new UserModel() { Id = id, Name = signInModel.UserName, _token = _token, _tokenExpirationDate = exp};
        }
         
        private JwtSecurityToken generateToken(string UserName)
        {
            var tokenDetails = new string[2];
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var authSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudiences"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256Signature));

            /*tokenDetails[0] = new JwtSecurityTokenHandler().WriteToken(token);
            var a = token.ValidTo;*/
            return token;
        }
    }
}
