using EduPlatform.WPF.Service.Validators;
using Xunit;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Tests;

public class DataValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("abc")]
    [InlineData("-1")]
    [InlineData("-10")]
    [InlineData("5.25")]
    [InlineData("2147483648")]  // int.MaxValue + 1
    public void IsValidId_InvalidLine_ReturnsFalse(string? line)
    {
        // Act
        bool actual = DataValidator.IsValidId(lineId: line);

        // Assert
        Assert.False(actual);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2147483647")]  // int.MaxValue
    public void IsValidId_ValidLine_ReturnsFalse(string line)
    {
        // Act
        bool actual = DataValidator.IsValidId(lineId: line);

        // Assert
        Assert.True(actual);
    }
}