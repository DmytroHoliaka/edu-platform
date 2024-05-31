using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class UpdateGroupCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NullGroup_ThrowsArgumentNullException()
    {
        // Arrange
        Group? group = null;
        UpdateGroupCommand command = new(fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await command.ExecuteAsync(targetGroup: group));
    }

    [Fact]
    public async Task ExecuteAsync_NonExistingGroup_CreatesNewGroupInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Group newGroup = ModelGenerator.GetUnfilledGroup();
        UpdateGroupCommand command = new(fixture.DbContextFactory);

        // Act
        await command.ExecuteAsync(targetGroup: newGroup);

        // Assert
        Assert.Equal(
            expected: newGroup,
            actual: fixture.DbManager.GetSingleGroupFromDatabase(),
            comparer: new GroupEqualityComparer());
    }

    [Fact]
    public async Task ExecuteAsync_ExistingGroup_UpdatesGroupInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Group group = ModelGenerator.GetUnfilledGroup();
        UpdateGroupCommand command = new(fixture.DbContextFactory);

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();
        }

        group.Name = "Updated group";

        // Act
        await command.ExecuteAsync(targetGroup: group);

        // Assert
        Assert.Equal(
            expected: group,
            actual: fixture.DbManager.GetSingleGroupFromDatabase(),
            comparer: new GroupEqualityComparer());
    }
}