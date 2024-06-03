using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class DeleteTeacherCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NonExistingGuid_ThrowsInvalidDataException()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Guid nonExistingGuid = Guid.Empty;
        DeleteTeacherCommand command = new(contextFactory: fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidDataException>(
            async () => await command.ExecuteAsync(teacherId: nonExistingGuid));
    }

    [Fact]
    public async Task ExecuteAsync_ExistingGuid_DeletesTeacherFromDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Teacher teacher = ModelGenerator.GetUnfilledTeacher();
        DeleteTeacherCommand command = new(fixture.DbContextFactory);

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();
        }

        // Act
        await command.ExecuteAsync(teacherId: teacher.TeacherId);

        // Assert
        Assert.Empty(fixture.DbManager.GetAllTeachersFromDatabase());
    }
}