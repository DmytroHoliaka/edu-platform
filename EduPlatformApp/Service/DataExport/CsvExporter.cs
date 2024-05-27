using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.IO;

namespace EduPlatform.WPF.Service.DataExport;

public class CsvExporter : IDataExporter
{
    public async Task ExportStudent(GroupViewModel groupVM)
    {
        string fileName = $"Students of {groupVM.GroupName}({DateTime.Now:yyyy.MM.dd hh-mm-ss.ffff}).csv";
        DirectoryInfo currentDir = new(AppDomain.CurrentDomain.BaseDirectory);

        string dirPath = currentDir?.Parent?.Parent?.Parent?.Parent?.FullName + "/ExportedData/";
        FileSystemManager.EnsureDirectoryCreated(dirPath);
        string filePath = dirPath + "/" + fileName;

        using (StreamWriter writer = new(filePath))
        {
            const string headerLine = $"LocalId, FirstName, LastName";
            await writer.WriteLineAsync(headerLine);

            int localId = 0;

            foreach (StudentViewModel studentVM in groupVM.StudentVMs)
            {
                localId += 1;
                string line = $"{localId},{studentVM.FirstName},{studentVM.LastName}";
                await writer.WriteLineAsync(line);
            }
        }
    }
}