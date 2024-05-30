using EduPlatform.Domain.Models;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EduPlatform.WPF.Commands.GroupCommands;
using EduPlatform.WPF.Service.DataExport;
using EduPlatform.WPF.Stores;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class GroupViewModel : ViewModelBase
    {
        public Group Group
        {
            get { return _group!; }
            set
            {
                _group = value;
                OnPropertyChanged(nameof(Group));
                OnPropertyChanged(nameof(GroupId));
                OnPropertyChanged(nameof(GroupName));
                OnPropertyChanged(nameof(CourseVM));
                OnPropertyChanged(nameof(TeacherVMs));
                OnPropertyChanged(nameof(StudentVMs));
            }
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(HasErrorMessage));
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        
        public string GroupName =>
            string.IsNullOrWhiteSpace(Group?.Name)
                ? "<not specified>"
                : Group.Name;

        public bool HasErrorMessage => string.IsNullOrEmpty(_errorMessage) == false;
        public Guid GroupId => Group.GroupId;
        public Guid? CourseId => Group.Course?.CourseId;

        public CourseViewModel? CourseVM =>
            Group.Course == null ? null : new CourseViewModel(Group.Course);

        public ObservableCollection<TeacherViewModel> TeacherVMs =>
            new(Group.Teachers.Select(t => new TeacherViewModel(t)));

        public ObservableCollection<StudentViewModel> StudentVMs =>
            new(Group.Students.Select(s => new StudentViewModel(s)));

        public bool IsEnabled { get; set; } = false;

        public ICommand? ExportCsvCommand { get; }
        public ICommand? ImportCsvCommand { get; }
        public ICommand? CreateDocxCommand { get; }
        public ICommand? CreatePdfCommand { get; }
        
        private Group? _group;
        private bool _isChecked;
        private string? _errorMessage;

        public GroupViewModel(Group groupItem)
        {
            Group = groupItem;
        }

        public GroupViewModel(Group groupItem, GroupStore groupStore, StudentStore studentStore)
        {
            Group = groupItem;

            ExportCsvCommand = new ExportStudentsCommand(this, new CsvExporter());
            ImportCsvCommand = new ImportStudentsCommand(studentStore, groupStore, this);
            CreateDocxCommand = new ExportStudentsCommand(this, new DocxExporter());
            CreatePdfCommand = new ExportStudentsCommand(this, new PdfExporter());
        }
    }
}