using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.IO;

namespace EduPlatform.WPF.Service.DataExport;

public abstract class DataExporter
{
    public abstract Task ExportStudent(GroupViewModel groupVM);

    protected string GetFilePath(string groupName, string extension)
    {
        string fileName = $"Students of {groupName}({DateTime.Now:yyyy.MM.dd hh-mm-ss.ffff}){extension}";
        DirectoryInfo currentDir = new(AppDomain.CurrentDomain.BaseDirectory);

        string dirPath = currentDir?.Parent?.Parent?.Parent?.Parent?.FullName + "/ExportedData/";
        FileSystemManager.EnsureDirectoryCreated(dirPath);
        string filePath = dirPath + "/" + fileName;

        return filePath;
    }
}