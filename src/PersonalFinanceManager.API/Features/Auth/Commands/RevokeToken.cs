using Microsoft.AspNetCore.Authorization;

namespace PersonalFinanceManager.API.Features.Auth.Commands;

[UsedImplicitly]
public class RevokeToken : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Auth.RevokeToken,
        (IMediator mediator)
            => mediator.Send(RevokeTokenCommand.Instance))
            .HasApiVersion(WebApplicationBuilderExtension.V1)
            .WithName(nameof(RevokeToken))
            .Produces<TokenResponse>(StatusCodes.Status200OK)
            .WithTags(nameof(Auth))
            .WithDescription("Revoke Token")
            .WithSummary("Revoke Access Token")
            .RequireAuthorization();
    }

    [UsedImplicitly]
    public record RevokeTokenCommand : IRequest<IResult>
    {
        public static RevokeTokenCommand Instance { get; } = new();
    }

    [UsedImplicitly]
    public class RevokeTokenCommandHandler(AppDbContext context, ClaimsPrincipal claims, ILogger logger) : IRequestHandler<RevokeTokenCommand, IResult>
    {
        public async Task<IResult> Handle(RevokeTokenCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var username = claims.Identity!.Name;

                var user = context.TokenInfos.SingleOrDefault(u => u.Username == username);
                if (user == null)
                {
                    return Results.BadRequest();
                }

                user.RefreshToken = string.Empty;
                await context.SaveChangesAsync(cancellationToken);

                return Results.Ok(true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Results.Problem(ex.Message);
            }
        }
    }
}

