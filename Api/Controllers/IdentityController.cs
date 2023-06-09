﻿using Application.Common.Contracts.Services;
using Application.Models.Car;
using Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase {
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService) {
        this._identityService = identityService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthenticateResponse), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request) {
        var result = await _identityService.SignInAsync(request);
        if(!result.Succeeded) {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthenticateResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request) {
        var result = await _identityService.SignUpAsync(request);
        if(!result.Succeeded) {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("add-to-role")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(ManageRoleResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddUserToRoleAsync([FromBody] AddToRoleRequest request) {
        var result = await _identityService.AddUserToRoleAsync(request);
        if (!result.Succeeded) {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("new-role")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(RoleResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddRoleAsync([FromBody] CreateRoleRequest request) {
        var result = await _identityService.CreateRoleAsync(request);
        if (!result.Succeeded) {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
