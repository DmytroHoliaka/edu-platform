using System.IO;
using System.Windows.Forms;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.DataExport;
using EduPlatform.WPF.Service;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands;

public class ExportGroupsCommand(GroupViewModel groupVM) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object? parameter)
    {
        CsvExporter exporter = new();

        try
        {
            await exporter.ExportStudent(groupVM);
            MessageBox.Show("Students successfully exported (folder ExportedData in the root directory)");
        }
        catch (Exception)
        {
            // ToDo: Create correct error handling
            throw;
        }
    }
}