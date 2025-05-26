using PersonalFinanceManager.API.Infrastructure.Utils;

namespace PersonalFinanceManager.Test.UnitTests.Infrastructure.Util;

public class StringExtensionsTests
{
    [TestCase(null, null)]
    [TestCase("", "")]
    [TestCase(" ", " ")]
    [TestCase("Foo", "foo")]
    [TestCase("FooBar", "fooBar")]
    [TestCase("foo", "foo")]
    [TestCase("fooBar", "fooBar")]
    public void ToCamelCase(string? value, string? expected) =>
        Assert.That(value.ConvertToCamelCase(), Is.EqualTo(expected));
}
