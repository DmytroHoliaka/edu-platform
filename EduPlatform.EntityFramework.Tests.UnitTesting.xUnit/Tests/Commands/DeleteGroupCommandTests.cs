using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class DeleteGroupCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NonExistingGuid_ThrowsInvalidDataException()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Guid nonExistingGuid = Guid.Empty;
        DeleteGroupCommand command = new(contextFactory: fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidDataException>(
            async () => await command.ExecuteAsync(groupId: nonExistingGuid));
    }

    [Fact]
    public async Task ExecuteAsync_ExistingGuid_DeletesCourseFromDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Group group = ModelGenerator.GetUnfilledGroup();
        DeleteGroupCommand command = new(fixture.DbContextFactory);

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();
        }

        // Act
        await command.ExecuteAsync(groupId: group.GroupId);

        // Assert
        Assert.Empty(fixture.DbManager.GetAllGroupFromDatabase());
    }
}