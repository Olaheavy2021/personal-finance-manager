using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PersonalFinanceManager.API.Infrastructure.Settings;

namespace PersonalFinanceManager.Test.UnitTests.Infrastructure.Settings;

internal class ValidatedOptionsDITests
{
    [Test]
    public void OptionsValidationPasses()
    {
        string expectedValue = "Test Text";
        var builder = new ConfigurationBuilder();
        builder.AddInMemoryCollection(new Dictionary<string, string?> { { "BogusConfig:Value", expectedValue } });
        var configuration = builder.Build();
        var services = new ServiceCollection();
        services
            .AddOptions<TestConfiguration>()
            .Bind(configuration.GetSection(TestConfiguration.GetSectionName()))
            .ValidatedOptions();
        var provider = services.BuildServiceProvider();

        var options = provider.GetRequiredService<IOptions<TestConfiguration>>();

        Assert.That(options.Value.Value, Is.EqualTo(expectedValue));
    }

    [Test]
    public void OptionsValidationFails()
    {
        var builder = new ConfigurationBuilder();
        builder.AddInMemoryCollection(new Dictionary<string, string?> { { "BogusConfig:Value", "" } });
        var configuration = builder.Build();
        var services = new ServiceCollection();
        services
            .AddOptions<TestConfiguration>()
            .Bind(configuration.GetSection(TestConfiguration.GetSectionName()))
            .ValidatedOptions();
        var provider = services.BuildServiceProvider();

        var action = () => provider.GetRequiredService<IOptions<TestConfiguration>>().Value;

        Assert.That(action, Throws.TypeOf<OptionsValidationException>());
    }

    public class TestConfiguration : IValidatedOptions<TestConfiguration>
    {
        public string Value { get; set; } = null!;

        public static string GetSectionName() => "BogusConfig";
        string IValidatedOptions<TestConfiguration>.GetSectionName() => GetSectionName();

        IValidator<TestConfiguration> IValidatedOptions<TestConfiguration>.GetValidator() => new Validator();

        private class Validator : AbstractValidator<TestConfiguration>
        {
            public Validator()
            {
                RuleFor(x => x.Value).NotEmpty();
            }
        }
    }
}
