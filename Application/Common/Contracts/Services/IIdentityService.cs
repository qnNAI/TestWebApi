using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Common;
using Application.Models.Identity;

namespace Application.Common.Contracts.Services;

public interface IIdentityService {

    Task<ManageRoleResponse> AddUserToRoleAsync(AddToRoleRequest request);
    Task<RoleResponse> CreateRoleAsync(CreateRoleRequest request);
    Task<AuthenticateResponse> SignUpAsync(SignUpRequest request);
    Task<AuthenticateResponse> SignInAsync(SignInRequest request);
}
    