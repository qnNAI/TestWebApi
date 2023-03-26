using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Common;
using Application.Models.Identity;

namespace Application.Common.Contracts.Services;

public interface IIdentityService {

    Task<IEnumerable<UserDto>> GetPageAsync(PageRequest page);

    Task<AuthenticateResponse> SignUpAsync(SignUpRequest request);
    Task<AuthenticateResponse> SignInAsync(SignInRequest request);
    Task<AuthenticateResponse> RefreshTokenAsync(TokenRequest request);

    Task<UserDto> ChangePasswordAsync(ChangePasswordRequest request);
    Task DeleteAsync(string username);
}
    