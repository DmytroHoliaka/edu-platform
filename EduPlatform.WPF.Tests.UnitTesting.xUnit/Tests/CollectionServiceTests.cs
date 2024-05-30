using EduPlatform.WPF.Service.Utilities;
using Xunit;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Tests;

public class CollectionServiceTests
{
    [Fact]
    public void GetString_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        List<string>? list = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => CollectionService.GetString(list));
    }

    [Fact]
    public void GetString_EmptyListOfStrings_ReturnsEmptyString()
    {
        // Arrange
        List<string>? list = [];
        string expected = string.Empty;

        // Act
        string actual = CollectionService.GetString(list);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetString_ListWithStrings_ReturnsPopulatedString()
    {
        // Arrange
        List<string>? list = ["FirstLine", "SecondLine", "ThirdLine"];
        const string expected =
            """
            FirstLine
            SecondLine
            ThirdLine

            """;

        // Act
        string actual = CollectionService.GetString(list);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetString_ListWithObject_ReturnsPopulatedString()
    {
        // Arrange
        List<Person>? people =
        [
            new Person("John", "Doe"),
            new Person("Alex", "Jordan"),
            new Person("Piter", "Howard")
        ];

        const string expected =
            """
            Person { FirstName = John, LastName = Doe }
            Person { FirstName = Alex, LastName = Jordan }
            Person { FirstName = Piter, LastName = Howard }

            """;

        // Act
        string actual = CollectionService.GetString(people);

        // Assert
        Assert.Equal(expected, actual);
    }

    private record Person(string FirstName, string LastName);
}