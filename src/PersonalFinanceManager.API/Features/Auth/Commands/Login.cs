namespace PersonalFinanceManager.API.Features.Auth.Commands;

[UsedImplicitly]
public class Login : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Auth.Login,
            ([FromBody] LoginCommand command, IMediator mediator)
            => mediator.Send(command))
            .HasApiVersion(WebApplicationBuilderExtension.V1)
            .WithName(nameof(Login))
            .Produces<TokenResponse>(StatusCodes.Status200OK)
            .WithTags(nameof(Auth))
            .WithDescription("Sign in")
            .WithSummary("Sign in a user");
    }

    [UsedImplicitly]
    public record LoginCommand(string UserName, string Password) : IRequest<IResult>;

    [UsedImplicitly]
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .EmailAddress().WithMessage("Valid email is required.")
                .WithHttpStatus(HttpStatusCode.BadRequest);
            RuleFor(x => x.Password).NotEmpty()
                .WithHttpStatus(HttpStatusCode.BadRequest);
        }
    }

    [UsedImplicitly]
    public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, IResult>
    {
        public async Task<IResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await authService.LoginAsync(new LoginRequest(command.UserName, command.Password), cancellationToken);

            return result switch
            {
                { IsFailed: true } => Results.BadRequest(result.Errors.FirstOrDefault()?.Message),
                { IsSuccess: true, Value: var tokenResponse } => Results.Ok(tokenResponse),
                _ => Results.Problem("An unexpected error occurred.")
            };
        }
    }
}

