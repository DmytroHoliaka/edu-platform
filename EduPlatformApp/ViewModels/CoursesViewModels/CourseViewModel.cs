using EduPlatform.WPF.Models;
using EduPlatform.WPF.ViewModels.GeneralViewModels;

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

        public Guid CourseId => 
            Course.CourseId;

        public IEnumerable<Group> Groups =>
            Course.Groups ?? Enumerable.Empty<Group>();

        public string CourseName =>
            string.IsNullOrWhiteSpace(Course?.Name)
            ? "<not specified>" : Course.Name;

        public string Description =>
            string.IsNullOrWhiteSpace(Course?.Description) 
            ? "<not specified>" : Course.Description;

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

        public bool IsEnabled { get; set; } = false;

        private bool _isChecked;
        private Course? _course;

        public CourseViewModel(Course course)
        {
            Course = course;
            IsChecked = false;
        }
    }
}
