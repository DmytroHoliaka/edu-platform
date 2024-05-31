using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class UpdateStudentCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NullStudent_ThrowsArgumentNullException()
    {
        // Arrange
        Student? student = null;
        UpdateStudentCommand command = new(fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await command.ExecuteAsync(targetStudent: student));
    }

    [Fact]
    public async Task ExecuteAsync_NonExistingStudent_CreatesNewStudentInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Student newStudent = ModelGenerator.GetUnfilledStudent();
        UpdateStudentCommand command = new(fixture.DbContextFactory);

        // Act
        await command.ExecuteAsync(targetStudent: newStudent);

        // Assert
        Assert.Equal(
            expected: newStudent,
            actual: fixture.DbManager.GetSingleStudentFromDatabase(),
            comparer: new StudentEqualityComparer());
    }

    [Fact]
    public async Task ExecuteAsync_ExistingStudent_UpdatesStudentInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Student student = ModelGenerator.GetUnfilledStudent();
        UpdateStudentCommand command = new(fixture.DbContextFactory);

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        student.FirstName = "Updated first name";
        student.LastName = "Updated last name";

        // Act
        await command.ExecuteAsync(targetStudent: student);

        // Assert
        Assert.Equal(
            expected: student,
            actual: fixture.DbManager.GetSingleStudentFromDatabase(),
            comparer: new StudentEqualityComparer());
    }
}