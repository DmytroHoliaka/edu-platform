using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class CreateStudentCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        Student? student = null;
        CreateStudentCommand command = new(contextFactory: fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await command.ExecuteAsync(newStudent: student));
    }

    [Fact]
    public async Task ExecuteAsync_ValidStudent_CreatesNewStudentInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Student expected = ModelGenerator.GetPopulatedStudent();
        CreateStudentCommand command = new(fixture.DbContextFactory);

        // Act
        await command.ExecuteAsync(newStudent: expected);

        // Assert
        Assert.Equal(
            expected: expected,
            actual: fixture.DbManager.GetSingleStudentFromDatabase(),
            comparer: new StudentEqualityComparer());
    }
}