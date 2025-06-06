using FluentValidation;

namespace PersonalFinanceManager.Application.Settings.OptionsFactory;

public interface IValidatedOptions<in TOptions> where TOptions : new()
{
    string GetSectionName();

    IValidator<TOptions> GetValidator();
}
