using Xunit;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;

[CollectionDefinition("DatabaseTests")]
public class DatabaseCollectionFixture : ICollectionFixture<DatabaseFixture>
{
    // Empty class to implement collection of fixtures
}