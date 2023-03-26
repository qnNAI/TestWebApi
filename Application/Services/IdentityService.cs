using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Repositories;
using Application.Common.Contracts.Services;
using Application.Common.Exceptions;
using Application.Common.Security;
using Application.Models.Common;
using Application.Models.Identity;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class IdentityService : IIdentityService {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _jwtSettings;

    public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, JwtSettings jwtSettings) {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings;
    }

    public async Task<ManageRoleResponse> AddUserToRoleAsync(AddToRoleRequest request) {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null) {
            return new ManageRoleResponse {
                Succeeded = false,
                Errors = new[] {
                    new IdentityError {
                        Code = "UserNotFound",
                        Description = "User not found"
                    }
                }
            };
        }

        var result = await _userManager.AddToRoleAsync(user, request.RoleName);
        if (result.Succeeded) {
            return new ManageRoleResponse {
                Succeeded = true
            };
        }
        
        return new ManageRoleResponse {
            Succeeded = false,
            Errors = new[] {
                new IdentityError {
                    Code = "AddToRoleFailed",
                    Description = "Failed to add user to role"
                }
            }
        };
    }

    public async Task<AuthenticateResponse> SignInAsync(SignInRequest request) {
        if(request is null) {
            throw new ArgumentNullException(nameof(request));
        }

        var user = await _userManager.FindByNameAsync(request.Username);

        if(user is null) {
            return new AuthenticateResponse {
                Succeeded = false,
                Errors = new[] {
                        new IdentityError {
                            Code = "InvalidCredentials",
                            Description = "Invalid credentials"
                        }
                    }
            };
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if(!isPasswordValid) {
            return new AuthenticateResponse {
                Succeeded = false,
                Errors = new[] {
                        new IdentityError {
                            Code = "InvalidCredentials",
                            Description = "Invalid credentials"
                        }
                    }
            };
        }

        var token = await GenerateJwtTokenAsync(user);

        return new AuthenticateResponse {
            Succeeded = true,
            Token = token.Token
        };
    }

    public async Task<AuthenticateResponse> SignUpAsync(SignUpRequest request) {
        if(request is null) {
            throw new ArgumentNullException(nameof(request));
        }

        var appUser = request.Adapt<ApplicationUser>();
        var result = await _userManager.CreateAsync(appUser, request.Password);

        if(!result.Succeeded) {
            return result.Adapt<AuthenticateResponse>();
        }

        var user = await _userManager.FindByNameAsync(request.Username);
        var token = await GenerateJwtTokenAsync(user);

        return new AuthenticateResponse {
            Succeeded = true,
            Token = token.Token
        };
    }

    public async Task<RoleResponse> CreateRoleAsync(CreateRoleRequest request) {
        var result = await _roleManager.CreateAsync(request.Adapt<IdentityRole>());

        if (!result.Succeeded) {
            return new RoleResponse {
                Succeeded = false,
                Errors = result.Errors
            };
        }

        return new RoleResponse { Succeeded = true };
    }

    private async Task<JwtTokenResponse> GenerateJwtTokenAsync(ApplicationUser user) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, user.Id),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new(nameof(user.Id), user.Id)
        };

        foreach(var role in roles) {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.TokenLifetimeSeconds),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            NotBefore = DateTime.UtcNow
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtTokenResponse {
            Id = token.Id,
            Token = tokenHandler.WriteToken(token)
        };
    }
}
