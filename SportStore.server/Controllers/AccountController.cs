using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportStore.server.Data.Models;
using SportStore.server.Helpers;
using SportStore.server.Requests;

namespace SportStore.server.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(UserManager<ApplicationUser> userManager, JwtHelper jwtHelper) : ControllerBase
{
    [HttpPost("register")]
    [Authorize(Roles= "admin")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        await userManager.AddToRoleAsync(user, "user");
        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password)) 
            return Unauthorized();
        var token = jwtHelper.GetJwtTokenAsync(user);
        return Ok(new { Token = token });
    }
}