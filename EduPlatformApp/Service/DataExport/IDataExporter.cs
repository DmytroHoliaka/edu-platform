using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Service.DataExport;

public interface IDataExporter
{
    Task ExportStudent(GroupViewModel groupVm);
}