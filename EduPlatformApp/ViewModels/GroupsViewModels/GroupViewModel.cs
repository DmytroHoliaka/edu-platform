using CommunityToolkit.Mvvm.Input;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class GroupViewModel : ViewModelBase
    {
        public Group Group
        {
            get
            {
                return _group!;
            }
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
            get
            {
                return _isChecked;
            }
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
            ? "<not specified>" : Group.Name;

        public bool IsEnabled { get; set; } = false;

        public CourseViewModel? CourseVM => Group.Course == null ? null : new(Group.Course);
        public ObservableCollection<TeacherViewModel> TeacherVMs => new(Group.Teachers.Select(t => new TeacherViewModel(t)));
        public ObservableCollection<StudentViewModel> StudentVMs => new(Group.Students.Select(s => new StudentViewModel(s)));

        public ICommand? ExportCsvCommand { get; } = new RelayCommand(ExportIntoCsv);
        public ICommand? ImportCsvCommand { get; } = new RelayCommand(ImportFromCsv);
        public ICommand? CreateDocsCommand { get; } = new RelayCommand(CreateDocs);
        public ICommand? CreatePdfCommand { get; } = new RelayCommand(CreatePdf);


        public GroupViewModel(Group groupItem)
        {
            Group = groupItem;
        }

        private static void ExportIntoCsv()
        {
            MessageBox.Show("Export");
        }

        private static void ImportFromCsv()
        {
            MessageBox.Show("Import");
        }

        private static void CreateDocs()
        {
            MessageBox.Show("Docs");
        }

        private static void CreatePdf()
        {
            MessageBox.Show("Pdf");
        }
    }
}
