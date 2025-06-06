using FluentResults;
using Microsoft.AspNetCore.Identity;
using PersonalFinanceManager.Application.Contracts;
using PersonalFinanceManager.Application.Database;
using PersonalFinanceManager.Application.Models;
using PersonalFinanceManager.Application.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace PersonalFinanceManager.Application.Services;

public class AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, AppDbContext context) : IAuthService
{
    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {

        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return Result.Fail<TokenResponse>("Invalid username or password.");

        if (!await userManager.CheckPasswordAsync(user, request.Password))
            return Result.Fail<TokenResponse>("Invalid username or password.");

        List<Claim> authClaims = [
                new (ClaimTypes.Name, user.UserName!),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new (JwtRegisteredClaimNames.Sub, user.UserName!),
                        new (JwtRegisteredClaimNames.Email, user.Email!),
                        new (ClaimTypes.NameIdentifier, user.Id!),
        ];

        var userRoles = await userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = tokenService.GenerateAccessToken(authClaims);
        string refreshToken = tokenService.GenerateRefreshToken();
        var tokenInfo = context.TokenInfos.
                    FirstOrDefault(a => a.Username == user.UserName);
        if (tokenInfo == null)
        {
            var ti = new TokenInfo
            {
                Username = user.UserName!,
                RefreshToken = refreshToken,
                ExpiredAt = DateTime.UtcNow.AddDays(7)
            };
            context.TokenInfos.Add(ti);
        }
        else
        {
            tokenInfo.RefreshToken = refreshToken;
            tokenInfo.ExpiredAt = DateTime.UtcNow.AddDays(7);
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok(new TokenResponse(token, refreshToken));

    }
}
