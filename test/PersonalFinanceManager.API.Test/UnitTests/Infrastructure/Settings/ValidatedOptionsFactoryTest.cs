using FluentValidation;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PersonalFinanceManager.API.Infrastructure.Settings;

namespace PersonalFinanceManager.API.Test.UnitTests.Infrastructure.Settings;

public class ValidatedOptionsFactoryTest
{
    [Test]
    public void OptionsValidationPasses()
    {
        string expectedValue = "Test Text";
        var builder = new ConfigurationBuilder();
        builder.AddInMemoryCollection(new Dictionary<string, string?> { { "BogusConfig:Value", expectedValue } });
        var configuration = builder.Build();

        var options = ValidatedOptionsFactory.Create<TestConfiguration>(configuration);

        Assert.That(options.Value, Is.EqualTo(expectedValue));
    }

    [Test]
    public void OptionsValidationFails()
    {
        var builder = new ConfigurationBuilder();
        builder.AddInMemoryCollection(new Dictionary<string, string?> { { "BogusConfig:Value", "" } });
        var configuration = builder.Build();

        TestDelegate action = () => ValidatedOptionsFactory.Create<TestConfiguration>(configuration);

        Assert.That(action, Throws.Exception);
    }

    public class TestConfiguration : IValidatedOptions<TestConfiguration>
    {
        public string Value { get; set; } = null!;

        string IValidatedOptions<TestConfiguration>.GetSectionName() => "BogusConfig";

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
