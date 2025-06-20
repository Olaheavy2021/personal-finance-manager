﻿namespace PersonalFinanceManager.API.Infrastructure.Validation;

public static class ConfigureValidation
{
    public static IServiceCollection AddValidators(
    this IServiceCollection services,
    params Type[] validatorAssemblyMarkerTypes)
    {
        services.AddValidatorsFromAssemblies(validatorAssemblyMarkerTypes.Select(t => t.Assembly));
        ValidatorOptions.Global.PropertyNameResolver = ValidatorNameResolver.ResolvePropertyName;
        ValidatorOptions.Global.DisplayNameResolver = ValidatorNameResolver.ResolveDisplayName;
        return services;
    }
}
