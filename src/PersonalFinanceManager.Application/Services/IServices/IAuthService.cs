using FluentResults;
using PersonalFinanceManager.Application.Contracts;

namespace PersonalFinanceManager.Application.Services.IServices;

public interface IAuthService
{
    Task<Result<TokenResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}
