using EduPlatform.WPF.Service.Validators;
using Xunit;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Tests;

public class GeneralFileValidatorTests
{
    [Theory]
    [InlineData(null, ".csv")]
    [InlineData("", ".csv")]
    [InlineData("    ", ".csv")]
    public void IsExtensionCorrect_InvalidPath_ReturnsFalse(string? path, string extension)
    {
        // Act
        bool actual = GeneralFileValidator.IsExtensionCorrect(path, extension);

        // Assert
        Assert.False(actual);
    }

    [Theory]
    [InlineData("file.csv", null)]
    [InlineData("file.csv", "")]
    [InlineData("file.csv", "   ")]
    public void IsExtensionCorrect_InvalidExtension_ReturnsFalse(string path, string? extension)
    {
        // Act
        bool actual = GeneralFileValidator.IsExtensionCorrect(path, extension);

        // Assert
        Assert.False(actual);
    }

    [Theory]
    [InlineData("file.csv", ".")]
    [InlineData("file.csv", ".xml")]
    public void IsExtensionCorrect_InappropriateExtension_ReturnsFalse(string path, string extension)
    {
        // Act
        bool actual = GeneralFileValidator.IsExtensionCorrect(path, extension);

        // Assert
        Assert.False(actual);
    }

    [Theory]
    [InlineData("file.csv", ".csv")]
    [InlineData("file.html", ".html")]
    public void IsExtensionCorrect_AppropriateExtension_ReturnsFalse(string path, string extension)
    {
        // Act
        bool actual = GeneralFileValidator.IsExtensionCorrect(path, extension);

        // Assert
        Assert.True(actual);
    }
}