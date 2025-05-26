namespace PersonalFinanceManager.API.Infrastructure.Behaviors.Domain;

[UsedImplicitly]
public class DomainException(string? message) : Exception(message);
