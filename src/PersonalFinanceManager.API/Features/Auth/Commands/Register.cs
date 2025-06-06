namespace PersonalFinanceManager.API.Features.Auth.Commands;

[UsedImplicitly]
public class Register : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Auth.Register,
            ([FromBody] RegisterCommand command, IMediator mediator)
            => mediator.Send(command))
            .HasApiVersion(WebApplicationBuilderExtension.V1)
            .WithName(nameof(Register))
            .WithTags(nameof(Auth))
            .WithDescription("Register a user")
            .WithSummary("Register a new user");
    }

    [UsedImplicitly]
    public record RegisterCommand(string Name, string Email, string Password) : IRequest<IResult>;

    [UsedImplicitly]
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().
                WithMessage("Name is required.").WithHttpStatus(HttpStatusCode.BadRequest);
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress().WithMessage("Valid email is required.").WithHttpStatus(HttpStatusCode.BadRequest);
            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.").WithHttpStatus(HttpStatusCode.BadRequest);
        }
    }

    [UsedImplicitly]
    public class RegisterCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger logger) : IRequestHandler<RegisterCommand, IResult>
    {
        public async Task<IResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await userManager.FindByNameAsync(command.Email);
                if (existingUser != null)
                {
                    return Results.BadRequest("User already exists");
                }

                // Create User role if it doesn't exist
                if ((await roleManager.RoleExistsAsync(Roles.User)) == false)
                {
                    var roleResult = await roleManager
                          .CreateAsync(new IdentityRole(Roles.User));

                    if (roleResult.Succeeded == false)
                    {
                        var roleErros = roleResult.Errors.Select(e => e.Description);
                        logger.Error($"Failed to create user role. Errors : {string.Join(",", roleErros)}");
                        return Results.BadRequest($"Failed to create user role. Errors : {string.Join(",", roleErros)}");
                    }
                }

                ApplicationUser user = new()
                {
                    Email = command.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = command.Email,
                    Name = command.Name,
                    EmailConfirmed = true
                };

                // Attempt to create a user
                var createUserResult = await userManager.CreateAsync(user, command.Password);

                // Validate user creation. If user is not created, log the error and
                // return the BadRequest along with the errors
                if (createUserResult.Succeeded == false)
                {
                    var errors = createUserResult.Errors.Select(e => e.Description);
                    logger.Error(
                        $"Failed to create user. Errors: {string.Join(", ", errors)}"
                    );
                    return Results.BadRequest($"Failed to create user. Errors: {string.Join(", ", errors)}");
                }

                // adding role to user
                var addUserToRoleResult = await userManager.AddToRoleAsync(user: user, role: Roles.User);

                if (addUserToRoleResult.Succeeded == false)
                {
                    var errors = addUserToRoleResult.Errors.Select(e => e.Description);
                    logger.Error($"Failed to add role to the user. Errors : {string.Join(",", errors)}");
                }
                return Results.Created();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}

