using System.Windows.Forms;
using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Service.DataImport;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace EduPlatform.WPF.Commands.GroupCommands;

public class ImportStudentsCommand(StudentStore studentStore, GroupViewModel groupVM) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object? parameter)
    {
        OpenFileDialog dialog = new()
        {
            Filter = "CSV Files (*.csv)|*.csv",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (dialog.ShowDialog() == true)
        {
            string filePath = dialog.FileName;

            CsvImporter importer = new();
            await importer.ImportStudents(studentStore, groupVM, filePath);
        }
    }
}