using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.DataExport;

public interface IDataExporter
{
    Task ExportStudent(GroupViewModel groupVm);
}