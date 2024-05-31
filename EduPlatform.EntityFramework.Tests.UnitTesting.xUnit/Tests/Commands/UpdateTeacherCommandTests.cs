using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class UpdateTeacherCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NullTeacher_ThrowsArgumentNullException()
    {
        // Arrange
        Teacher? teacher = null;
        UpdateTeacherCommand command = new(fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await command.ExecuteAsync(targetTeacher: teacher));
    }

    [Fact]
    public async Task ExecuteAsync_NonExistingTeacher_CreatesNewTeacherInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Teacher newTeacher = ModelGenerator.GetUnfilledTeacher();
        UpdateTeacherCommand command = new(fixture.DbContextFactory);

        // Act
        await command.ExecuteAsync(targetTeacher: newTeacher);

        // Assert
        Assert.Equal(
            expected: newTeacher,
            actual: fixture.DbManager.GetSingleTeacherFromDatabase(),
            comparer: new TeacherEqualityComparer());
    }

    [Fact]
    public async Task ExecuteAsync_ExistingTeacher_UpdatesTeacherInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Teacher teacher = ModelGenerator.GetUnfilledTeacher();
        UpdateTeacherCommand command = new(fixture.DbContextFactory);

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();
        }

        teacher.FirstName = "Updated first name";
        teacher.LastName = "Updated last name";

        // Act
        await command.ExecuteAsync(targetTeacher: teacher);

        // Assert
        Assert.Equal(
            expected: teacher,
            actual: fixture.DbManager.GetSingleTeacherFromDatabase(),
            comparer: new TeacherEqualityComparer());
    }
}