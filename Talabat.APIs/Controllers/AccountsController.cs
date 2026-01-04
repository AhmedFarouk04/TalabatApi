using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    public class AccountsController:ApiBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountsController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        // POST: api/accounts/login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new ApiErrorResponse(401));

            var result = await signInManager
                .CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiErrorResponse(401));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegitserDto model)
        {

            if (checkEmailExsist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "This Email is Already Exists" } });

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiErrorResponse(400));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });
        }



        [Authorize]
        [HttpGet("currentuser")] // GET: api/accounts/currentuser
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);  //User fun in Controller class if the usser is authorized 

            var user = await userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)//the best create table in database store token not create new token
            });



        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserWithAddressByEmailAsync(User);

            var address = mapper.Map<Address, AddressDto>(user.Address);
            return Ok(address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        {
            var user = await userManager.FindUserWithAddressByEmailAsync(User);

            if (user == null)
                return Unauthorized();

            if (user.Address == null)
            {
                // CREATE
                user.Address = mapper.Map<Address>(updatedAddress);
                user.Address.AppUserId = user.Id;
            }
            else
            {
                // UPDATE (الأهم)
                mapper.Map(updatedAddress, user.Address);
            }

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest("Problem updating address");

            return Ok(updatedAddress);
        }

        [HttpGet("checkemail")]
        public async Task<ActionResult<bool>> checkEmailExsist(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }






    }
}
