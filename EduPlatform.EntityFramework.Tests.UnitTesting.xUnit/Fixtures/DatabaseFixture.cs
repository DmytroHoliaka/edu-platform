using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Fixtures;
// ToDo: Check that creates only one test context for all DatabaseTests collection
public class DatabaseFixture : IDisposable
{
    public EduPlatformDbContextFactory DbContextFactory { get; }
    public DatabaseManager DbManager { get; }

    private const string ConnectionLine =
        "Server=(localdb)\\mssqllocaldb;" +
        "Database=EduPlatformTests;" +
        "Trusted_Connection=True;";

    public DatabaseFixture()
    {
        DbContextOptionsBuilder builder = new();
        builder.UseSqlServer(ConnectionLine);
        DbContextOptions options = builder.Options;

        DbContextFactory = new EduPlatformDbContextFactory(options);
        DbManager = new DatabaseManager(DbContextFactory);

        using (EduPlatformDbContext context = DbContextFactory.Create())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }

    public void Dispose()
    {
        using (EduPlatformDbContext context = DbContextFactory.Create())
        {
            context.Database.EnsureDeleted();
        }
    }
}