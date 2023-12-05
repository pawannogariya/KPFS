using AutoMapper;
using KPFS.Business.Models;
using KPFS.Data.Constants;
using KPFS.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : ApiBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AdminController(UserManager<User> userManager, IMapper mapper) : base(userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<ActionResult<ResponseDto<IEnumerable<UserDto>>>> GetAllUsers()
        {
            return BuildResponse<IEnumerable<UserDto>>(_mapper.Map<IEnumerable<UserDto>>(await _userManager.Users.ToListAsync()));
        }

        [HttpPost("update-user")]
        public async Task<ActionResult<ResponseDto<bool>>> UpdateUser(UserDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);

            if(user==null)
            {
                return BuildFailureResponse<bool>("Cannot find user");
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.IsActive = userDto.IsActive;

            await _userManager.UpdateAsync(user);

            return BuildResponse(true);
        }
    }
}





