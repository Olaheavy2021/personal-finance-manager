using NUnit.Framework;
using PersonalFinanceManager.API.Infrastructure.Utils;

namespace PersonalFinanceManager.API.Test.UnitTests.Infrastructure.Util;

public class EnumExtensionsTests
{
    [Test]
    public void ToValuesExtractsFlags()
    {
        const TestEnum value = TestEnum.A | TestEnum.C;
        var result = value.ToValues<TestEnum, TestEnum>();
        Assert.That(result, Is.EqualTo(new[] { TestEnum.A, TestEnum.C }));
    }

    [Test]
    public void ToValuesReturnsEmptyArrayForNone()
    {
        const TestEnum value = TestEnum.None;
        var result = value.ToValues<TestEnum, TestEnum>();
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ToValuesCanConvertToTarget()
    {
        const TestEnum value = TestEnum.A | TestEnum.C;
        var result = value.ToValues<TestEnum, TestEnum2>();
        Assert.That(result, Is.EqualTo(new[] { TestEnum2.A, TestEnum2.C }));
    }

    [Test]
    public void ToFlagsSetsCorrectBits()
    {
        var values = new[] { TestEnum.A, TestEnum.C };
        var result = values.ToFlags();
        Assert.That(result, Is.EqualTo(TestEnum.A | TestEnum.C));
    }

    [Test]
    public void ToFlagsCanConvertToTarget()
    {
        var values = new[] { TestEnum.A, TestEnum.C };
        var result = values.ToFlags<TestEnum, TestEnum2>();
        Assert.That(result, Is.EqualTo(TestEnum2.A | TestEnum2.C));
    }

    [Test]
    public void ToFlagsUsesNoneForEmpty()
    {
        var values = Array.Empty<TestEnum>();
        var result = values.ToFlags();
        Assert.That(result, Is.EqualTo(TestEnum.None));
    }

    [Flags]
    public enum TestEnum
    {
        None = 0,
        A = 1 << 0,
        B = 1 << 1,
        C = 1 << 2
    }

    [Flags]
    public enum TestEnum2
    {
        None = 0,
        A = 1 << 0,
        B = 1 << 1,
        C = 1 << 2
    }
}
