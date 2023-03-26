using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase {
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityController(UserManager<ApplicationUser> userManager) {
        this._userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> Get() {
        var t = await _userManager.CreateAsync(new ApplicationUser { 
            UserName = "test"
        }, "Qwerty-123");

        return Ok();
    }
}
