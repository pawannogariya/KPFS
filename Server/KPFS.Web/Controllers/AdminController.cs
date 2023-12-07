﻿using AutoMapper;
using KPFS.Business.Models;
using KPFS.Business.Services.Interfaces;
using KPFS.Data.Constants;
using KPFS.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace KPFS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : ApiBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AdminController(
            UserManager<User> userManager,
            IMapper mapper,
            RoleManager<Role> roleManager,
            IEmailService emailService) : base(userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<ResponseDto<IEnumerable<UserDto>>>> GetAllUsers()
        {
            return BuildResponse(_mapper.Map<IEnumerable<UserDto>>(await _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync()));
        }

        [HttpPost("update-user")]
        public async Task<ActionResult<ResponseDto<bool>>> UpdateUser(UpdateUserDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                return BuildFailureResponse<bool>("Cannot find user");
            }

            if (CurrentUser.Email != userDto.Email && userDto.Role == Roles.Admin)
            {
                return BuildFailureResponse<bool>("User cannot be assigned to admin role");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    user.FirstName = userDto.FirstName;
                    user.LastName = userDto.LastName;
                    user.IsActive = userDto.IsActive;

                    await _userManager.UpdateAsync(user);

                    var userCurrentRole = (await _userManager.GetRolesAsync(user)).First();

                    if (CurrentUser.Email != userDto.Email && userCurrentRole != userDto.Role)
                    {
                        await _userManager.RemoveFromRoleAsync(user, userCurrentRole);
                        await _userManager.AddToRoleAsync(user, userDto.Role);
                    }

                    scope.Complete();
                }
                catch
                {
                    scope.Dispose();
                    throw;
                }
            }

            return BuildResponse(true);
        }

        [HttpPost("add-user")]
        public async Task<ActionResult<ResponseDto<UserDto>>> AddUser([FromBody] AddUserDto model)
        {
            if (model.Role == Roles.Admin)
            {
                return BuildFailureResponse<UserDto>("Adding admin is not allowed!");
            }

            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
            {
                if (!userExist.IsActive)
                {
                    return BuildFailureResponse<UserDto>("User already exists but is in-active");
                }

                return BuildFailureResponse<UserDto>("User already exists");
            }

            if (await _roleManager.RoleExistsAsync(model.Role))
            {
                var user = _mapper.Map<User>(model);

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                        if (!result.Succeeded)
                        {
                            var errors = string.Join(Environment.NewLine, result.Errors.Select(x => $"{x.Code}: {x.Description}"));
                            return BuildFailureResponse<UserDto>(errors);
                        }

                        await _userManager.AddToRoleAsync(user, model.Role);

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationLink = Url.Action(nameof(AuthenticationController.ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                        var message = new MessageDto(new string[] { user.Email! }, "KPFS: Confirmation email link", confirmationLink!);
                        _emailService.SendEmail(message);

                        return BuildResponse(_mapper.Map<UserDto>(user));
                    }
                    catch
                    {
                        scope.Dispose();
                        throw;
                    }
                }
            }
            else
            {
                return BuildFailureResponse<UserDto>("This role doesn't exist.");
            }
        }
    }
}





