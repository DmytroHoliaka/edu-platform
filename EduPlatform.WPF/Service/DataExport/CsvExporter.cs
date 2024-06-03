using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.IO;
using EduPlatform.WPF.Service.Time;

namespace EduPlatform.WPF.Service.DataExport;

public class CsvExporter : DataExporter
{
    private readonly string _folderPath;

    public CsvExporter(IClock clock, string? folderPath) : base(clock)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(folderPath, nameof(folderPath));
        _folderPath = folderPath;
    }

    public override async Task ExportStudent(GroupViewModel? groupVM)
    {
        ArgumentNullException.ThrowIfNull(groupVM, nameof(groupVM));

        string filePath = GetFilePath(
            groupName: groupVM.GroupName, 
            extension: ".csv",
            folderPath: _folderPath);

        using (StreamWriter writer = new(filePath))
        {
            const string headerLine = $"LocalId,FirstName,LastName";
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