using EduPlatform.WPF.Service.DataManagement;
using EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures;
using Xunit;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Tests;

public class FileSystemManagerTests(FileSystemManagerFixture fixture) : IClassFixture<FileSystemManagerFixture>
{
    [Fact]
    public void EnsureDirectoryCreated_NullPath_ThrowsArgumentException()
    {
        // Arrange
        const string? path = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => FileSystemManager.EnsureDirectoryCreated(path));
    }

    [Theory]
    [InlineData("")]
    [InlineData("\t\t\t\t")]
    public void EnsureDirectoryCreated_InvalidPath_ThrowsArgumentException(string? path)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => FileSystemManager.EnsureDirectoryCreated(path));
    }

    [Fact]
    public void EnsureDirectoryCreated_PathOfNonExistentDirectory_CreatesDirectory()
    {
        // Arrange
        string newDirPath = $"{fixture.DirPath}/NewDir";

        if (Directory.Exists(newDirPath))
        {
            Directory.Delete(path: newDirPath, recursive: true);
        }

        // Act
        FileSystemManager.EnsureDirectoryCreated(dirPath: newDirPath);

        // Assert
        Assert.True(Directory.Exists(newDirPath));
    }

    [Fact]
    public void EnsureDirectoryCreated_PathOfExistentDirectory_Ignores()
    {
        // Arrange
        string existingDir = $"{fixture.DirPath}/ExistingDir";
        Directory.CreateDirectory(path: existingDir);

        // Act
        FileSystemManager.EnsureDirectoryCreated(dirPath: existingDir);

        // Assert
        Assert.True(Directory.Exists(existingDir));
    }
}