using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class CreateTeacherCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        Teacher? teacher = null;
        CreateTeacherCommand command = new(contextFactory: fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await command.ExecuteAsync(newTeacher: teacher));
    }

    [Fact]
    public async Task ExecuteAsync_ValidTeacher_CreatesNewTeacherInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Teacher expected = ModelGenerator.GetPopulatedTeacher();
        CreateTeacherCommand command = new(fixture.DbContextFactory);

        // Act
        await command.ExecuteAsync(newTeacher: expected);

        // Assert
        Assert.Equal(
            expected: expected,
            actual: fixture.DbManager.GetSingleTeacherFromDatabase(),
            comparer: new TeacherEqualityComparer());
    }
}