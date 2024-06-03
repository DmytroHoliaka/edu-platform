namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures;

public class FileSystemManagerFixture : IDisposable
{
    public string DirPath { get; }

    public FileSystemManagerFixture()
    {
        DirPath = "FileSystemManagerFixtureTests";

        if (Directory.Exists(DirPath))
        {
            Directory.Delete(path: DirPath, recursive: true);
        }

        Directory.CreateDirectory(path: DirPath);
    }

    public void Dispose()
    {
        Directory.Delete(path: DirPath, recursive: true);
    }
}