using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Repositories;
using Application.Common.Contracts.Services;
using Application.Common.Security;
using Application.Models.Common;
using Application.Models.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class IdentityService : IIdentityService {
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public IdentityService(IUserRepository userRepository, ITokenRepository tokenRepository, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters) {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _jwtSettings = jwtSettings;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public Task<IEnumerable<UserDto>> GetPageAsync(PageRequest page) {
        throw new NotImplementedException();
    }

    public Task<AuthenticateResponse> SignInAsync(SignInRequest request) {
        throw new NotImplementedException();
    }

    public Task<AuthenticateResponse> SignUpAsync(SignUpRequest request) {
        throw new NotImplementedException();
    }

    public Task<AuthenticateResponse> RefreshTokenAsync(TokenRequest request) {
        throw new NotImplementedException();
    }

    public Task<UserDto> ChangePasswordAsync(ChangePasswordRequest request) {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string username) {
        throw new NotImplementedException();
    }
}
