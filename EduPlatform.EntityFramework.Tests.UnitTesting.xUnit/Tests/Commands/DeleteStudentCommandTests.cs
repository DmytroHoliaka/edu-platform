using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class DeleteStudentCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NonExistingGuid_ThrowsInvalidDataException()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Guid nonExistingGuid = Guid.Empty;
        DeleteStudentCommand command = new(contextFactory: fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidDataException>(
            async () => await command.ExecuteAsync(studentId: nonExistingGuid));
    }

    [Fact]
    public async Task ExecuteAsync_ExistingGuid_DeletesStudentFromDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Student student = ModelGenerator.GetUnfilledStudent();
        DeleteStudentCommand command = new(fixture.DbContextFactory);

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        // Act
        await command.ExecuteAsync(studentId: student.StudentId);

        // Assert
        Assert.Empty(fixture.DbManager.GetAllStudentsFromDatabase());
    }
}