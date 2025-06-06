namespace PersonalFinanceManager.Application.Contracts;

public record TokenResponse(string AccessToken, string RefreshToken);

public record LoginRequest(string UserName, string Password);