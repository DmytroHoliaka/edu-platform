using EduPlatform.Domain.Models;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Collections.ObjectModel;

namespace EduPlatform.WPF.ViewModels.CoursesViewModels
{
    public class CourseViewModel : ViewModelBase
    {
        public Course Course
        {
            get
            {
                return _course!;
            }
            set
            {
                _course = value;
                OnPropertyChanged(nameof(Course));
                OnPropertyChanged(nameof(CourseId));
                OnPropertyChanged(nameof(Groups));
                OnPropertyChanged(nameof(CourseName));
                OnPropertyChanged(nameof(Description));
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

        public bool HasErrorMessage => string.IsNullOrEmpty(_errorMessage) == false;

        public Guid CourseId => 
            Course.CourseId;

        public IEnumerable<Group> Groups =>
            Course.Groups ?? Enumerable.Empty<Group>();

        public ObservableCollection<GroupViewModel> GroupVMs =>
            new(Course.Groups.Select(g => new GroupViewModel(g)));

        public string CourseName =>
            string.IsNullOrWhiteSpace(Course?.Name)
            ? "<not specified>" : Course.Name;

        public string Description =>
            string.IsNullOrWhiteSpace(Course?.Description) 
            ? "<not specified>" : Course.Description;

        public bool IsEnabled { get; set; } = false;

        private bool _isChecked;
        private Course? _course;
        private string? _errorMessage;

        public CourseViewModel(Course course)
        {
            Course = course;
            IsChecked = false;
        }
    }
}
