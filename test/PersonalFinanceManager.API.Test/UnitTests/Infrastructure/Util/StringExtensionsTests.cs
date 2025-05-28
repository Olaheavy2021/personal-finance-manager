using NUnit.Framework;
using PersonalFinanceManager.API.Infrastructure.Utils;

namespace PersonalFinanceManager.API.Test.UnitTests.Infrastructure.Util;

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
        Assert.That(value.ToCamelCase(), Is.EqualTo(expected));
}
