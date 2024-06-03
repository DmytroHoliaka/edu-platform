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
    public async Task ExecuteAsync_NonExistingGroup_ThrowsInvalidDataException()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Group newGroup = ModelGenerator.GetUnfilledGroup();
        UpdateGroupCommand command = new(fixture.DbContextFactory);

        // Assert
        await Assert.ThrowsAsync<InvalidDataException>(
            async () => await command.ExecuteAsync(targetGroup: newGroup));
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

    [Fact]
    public async Task ExecuteAsync_GroupWithDuplicateName_ThrowsInvalidDataException()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Group sourceGroup = ModelGenerator.GetUnfilledGroup();
        Group duplicateNameGroup = GetDuplicateNameGroup(sourceGroup);

        UpdateGroupCommand command = new(fixture.DbContextFactory);

        using (EduPlatformDbContext context = fixture.DbContextFactory.Create())
        {
            await context.Groups.AddAsync(sourceGroup);
            await context.SaveChangesAsync();
        }

        // Act & Assert
        await Assert.ThrowsAsync<InvalidDataException>(
            async () => await command.ExecuteAsync(targetGroup: duplicateNameGroup));
    }

    private static Group GetDuplicateNameGroup(Group sourceGroup)
    {
        Group duplicateNameGroup = ModelGenerator.GetUnfilledGroup();
        duplicateNameGroup.Name = sourceGroup.Name;
        return duplicateNameGroup;
    }
}