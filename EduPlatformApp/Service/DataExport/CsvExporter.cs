using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.IO;

namespace EduPlatform.WPF.Service.DataExport;

public class CsvExporter : DataExporter
{
    public override async Task ExportStudent(GroupViewModel groupVM)
    {
        string filePath = GetFilePath(
            groupName: groupVM.GroupName, 
            extension: ".csv");

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