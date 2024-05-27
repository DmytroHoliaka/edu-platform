using EduPlatform.Domain.Models;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EduPlatform.WPF.Commands.GroupCommands;
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

        private Group? _group;
        private bool _isChecked;

        public Guid GroupId =>
            Group.GroupId;

        public Guid? CourseId =>
            Group.Course?.CourseId;

        public string GroupName =>
            string.IsNullOrWhiteSpace(Group?.Name)
                ? "<not specified>"
                : Group.Name;

        public bool IsEnabled { get; set; } = false;

        public CourseViewModel? CourseVM =>
            Group.Course == null ? null : new(Group.Course);

        public ObservableCollection<TeacherViewModel> TeacherVMs =>
            new(Group.Teachers.Select(t => new TeacherViewModel(t)));

        public ObservableCollection<StudentViewModel> StudentVMs =>
            new(Group.Students.Select(s => new StudentViewModel(s)));

        public ICommand? ExportCsvCommand { get; }
        public ICommand? ImportCsvCommand { get; }
        public ICommand? CreateDocsCommand { get; }
        public ICommand? CreatePdfCommand { get; }

        public GroupViewModel(Group groupItem)
        {
            Group = groupItem;
        }

        public GroupViewModel(Group groupItem, StudentStore studentStore)
        {
            Group = groupItem;

            ExportCsvCommand = new ExportStudentsCommand(this);
            ImportCsvCommand = new ImportStudentsCommand(studentStore, this);
            CreateDocsCommand = null;
            CreatePdfCommand = null;
        }
    }
}