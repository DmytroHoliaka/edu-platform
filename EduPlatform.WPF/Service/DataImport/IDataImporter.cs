using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Service.DataImport;

public interface IDataImporter
{
    Task ImportStudents(StudentStore studentStore, Group? group, string? path);
}