﻿using AutoMapper;
using KPFS.Business.Models;
using KPFS.Business.Services.Interfaces;
using KPFS.Common.Extensions;
using KPFS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KPFS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IEmailService emailService,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            IMapper mapper) : base(userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto<UserDto>>> Register([FromBody] RegisterUserDto model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
            {
                return BuildFailureResponse<UserDto>("User already exists");
            }

            User user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                TwoFactorEnabled = true
            };

            if (await _roleManager.RoleExistsAsync(model.Role))
            {
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(Environment.NewLine, result.Errors.Select(x => $"{x.Code}: {x.Description}"));
                    return BuildFailureResponse<UserDto>(errors);
                }

                await _userManager.AddToRoleAsync(user, model.Role);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new MessageDto(new string[] { user.Email! }, "KPFS: Confirmation email link", confirmationLink!);
                _emailService.SendEmail(message);

                return BuildResponse(_mapper.Map<UserDto>(user));
            }
            else
            {
                return BuildFailureResponse<UserDto>("This role doesn't exist.");
            }
        }

        [HttpGet("confirm-email")]
        public async Task<ActionResult<ResponseDto<string>>> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return BuildResponse("");
                }
                else
                {
                    var errors = string.Join(Environment.NewLine, result.Errors.Select(x => $"{x.Code}: {x.Description}"));
                    return BuildFailureResponse<string>(errors);
                }
            }

            return BuildFailureResponse<string>("User doesn't exist");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login([FromBody] LoginRequestDto loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
                return BuildFailureResponse<LoginResponseDto>($"Login failed. Cannot find user with the email id {loginModel.Email}!");
            }

            await _signInManager.SignOutAsync();
            var signResult = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, true);

            if (user.TwoFactorEnabled)
            {
                if (await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

                    var message = new MessageDto(new string[] { user.Email! }, "OTP Confrimation", token);
                    _emailService.SendEmail(message);

                    return BuildResponse<LoginResponseDto>();
                }
                else
                {
                    return BuildFailureResponse<LoginResponseDto>("Login failed. Wrong password!");
                }
            }

            if (signResult.Succeeded)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return BuildResponse(new LoginResponseDto()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    Expiration = jwtToken.ValidTo,
                    User = _mapper.Map<UserDto>(user)
                });
            }
            else
            {
                return BuildFailureResponse<LoginResponseDto>("Login Failed. Email or password is wrong!");
            }
        }

        [HttpPost]
        [Route("login-2fa")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> LoginWithOTP(string code, string email)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            var signIn = await _signInManager.TwoFactorSignInAsync(TokenOptions.DefaultEmailProvider, code, false, false);
            if (signIn.Succeeded)
            {
                if (user != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwtToken = GetToken(authClaims);

                    return BuildResponse(new LoginResponseDto()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        Expiration = jwtToken.ValidTo
                    });
                }
            }

            return BuildFailureResponse<LoginResponseDto>("Invalid code");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetJwtSecret()));

            var token = new JwtSecurityToken(
                issuer: _configuration.GetJwtValidIssuer(),
                audience: _configuration.GetJwtValidAudience(),
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}