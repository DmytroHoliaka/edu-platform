using EduPlatform.WPF.Service.DataManagement;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.IO;
using EduPlatform.WPF.Service.Time;

namespace EduPlatform.WPF.Service.DataExport;

public abstract class DataExporter(IClock clock)
{
    public abstract Task ExportStudent(GroupViewModel groupVM);

    protected string GetFilePath(string groupName, string extension, string folderPath)
    {
        string fileName = $"Students of {groupName}({clock.Now:yyyy.MM.dd hh-mm-ss.ffff}){extension}";
        DirectoryInfo currentDir = new(AppDomain.CurrentDomain.BaseDirectory);

        FileSystemManager.EnsureDirectoryCreated(folderPath);
        string filePath = folderPath + "/" + fileName;

        return filePath;
    }
}