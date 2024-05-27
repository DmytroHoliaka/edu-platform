using System.Windows.Forms;
using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Service.DataExport;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands;

public class ExportStudentsCommand(GroupViewModel groupVM, DataExporter exporter) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object? parameter)
    {
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