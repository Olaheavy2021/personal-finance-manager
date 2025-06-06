namespace PersonalFinanceManager.API.Features.Auth.Commands;

[UsedImplicitly]
public class RefreshToken : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Auth.RefreshToken,
            ([FromBody] RefreshTokenCommand command, IMediator mediator)
            => mediator.Send(command))
            .HasApiVersion(WebApplicationBuilderExtension.V1)
            .WithName(nameof(RefreshToken))
            .Produces<TokenResponse>(StatusCodes.Status200OK)
            .WithTags(nameof(Auth))
            .WithDescription("Refresh Token")
            .WithSummary("Refresh Access Token");
    }

    [UsedImplicitly]
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<IResult>;

    [UsedImplicitly]
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty()
                .WithHttpStatus(HttpStatusCode.BadRequest);
            RuleFor(x => x.RefreshToken).NotEmpty()
                .WithHttpStatus(HttpStatusCode.BadRequest);
        }
    }

    [UsedImplicitly]
    public class RefreshTokenCommandHandler(ITokenService tokenService, AppDbContext context, ILogger logger) : IRequestHandler<RefreshTokenCommand, IResult>
    {
        public async Task<IResult> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var principal = tokenService.GetPrincipalFromExpiredToken(command.AccessToken);
                var username = principal.Identity!.Name;

                var tokenInfo = context.TokenInfos.SingleOrDefault(u => u.Username == username);
                if (tokenInfo == null
                    || tokenInfo.RefreshToken != command.RefreshToken
                    || tokenInfo.ExpiredAt <= DateTime.UtcNow)
                {
                    return Results.BadRequest("Invalid refresh token. Please login again.");
                }

                var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
                var newRefreshToken = tokenService.GenerateRefreshToken();

                tokenInfo.RefreshToken = newRefreshToken; // rotating the refresh token
                await context.SaveChangesAsync();

                return Results.Ok(new TokenResponse(newAccessToken, newRefreshToken));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Results.Problem(ex.Message);
            }
        }
    }
}

