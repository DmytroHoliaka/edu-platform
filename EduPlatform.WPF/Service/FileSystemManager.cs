using System.IO;

namespace EduPlatform.WPF.Service;

public static class FileSystemManager
{
    public static void EnsureDirectoryCreated(string? dirPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(dirPath);

        if (Directory.Exists(dirPath) == false)
        {
            Directory.CreateDirectory(dirPath);
        }
    }
}