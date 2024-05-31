using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Comparers.EntityEqualityComparers;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;
using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Tests.Commands;

[Collection("DatabaseTests")]
public class CreateGroupCommandTests(DatabaseFixture fixture)
{
    [Fact]
    public async Task ExecuteAsync_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        Group? group = null;
        CreateGroupCommand command = new(contextFactory: fixture.DbContextFactory);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await command.ExecuteAsync(newGroup: group));
    }

    [Fact]
    public async Task ExecuteAsync_ValidGroup_CreatesNewGroupInDatabase()
    {
        // Arrange
        await fixture.DbManager.ClearDatabase();
        Group expected = ModelGenerator.GetPopulatedGroup();
        CreateGroupCommand command = new(fixture.DbContextFactory);

        // Act
        await command.ExecuteAsync(newGroup: expected);

        // Assert
        Assert.Equal(
            expected: expected,
            actual: fixture.DbManager.GetSingleGroupFromDatabase(),
            comparer: new GroupEqualityComparer());
    }
}