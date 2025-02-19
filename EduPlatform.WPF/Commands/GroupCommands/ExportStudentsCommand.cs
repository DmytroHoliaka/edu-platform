﻿using System.Windows.Forms;
using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Service.DataExport;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands;

public class ExportStudentsCommand(
    GroupViewModel groupVM, 
    DataExporter exporter, 
    string? folderName) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await exporter.ExportStudent(groupVM);
            MessageBox.Show($"Students successfully exported (folderName {folderName ?? "<not specified>"} " +
                            $"in the root directory)");
        }
        catch (Exception)
        {
            MessageBox.Show("There was an exception during the export. Please try again later.");
        }
    }
}