using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EduPlatform.WPF.Commands.CourseCommands;
using EduPlatform.WPF.Service;
using EduPlatform.WPF.Commands.BaseCommands;

namespace EduPlatform.WPF.ViewModels.CoursesViewModels
{
    public class CourseSequenceViewModel : ViewModelBase, ISequenceViewModel
    {
        private readonly ObservableCollection<CourseViewModel> _courserVMs;

        public IEnumerable<CourseViewModel> CourseVMs =>
            _courserVMs.Select(cvm => new CourseViewModel(cvm.Course));

        public CourseViewModel? SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged(nameof(SelectedCourse));

                if (UpdateCourseCommand is not null)
                {
                    ((OpenUpdateCourseFormCommand)UpdateCourseCommand).UpdatingCourse = value;
                    ((OpenUpdateCourseFormCommand)UpdateCourseCommand).OnCanExecutedChanged();
                }

                if (DeleteCourseCommand is not null)
                {
                    ((DeleteCourseCommand)DeleteCourseCommand).DeletingCourse = value;
                    ((DeleteCourseCommand)DeleteCourseCommand).OnCanExecutedChanged();
                }
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

        public bool HasErrorMessage => string.IsNullOrEmpty(_errorMessage) == false;

        public ICommand? LoadCoursesCommand { get; private set; }
        public ICommand? CreateCourseCommand { get; private set; }
        public ICommand? UpdateCourseCommand { get; private set; }
        public ICommand? DeleteCourseCommand { get; private set; }

        private GroupSequenceViewModel? _groupSequenceVM;
        private CourseViewModel? _selectedCourse;
        private readonly CourseStore _courseStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private string? _errorMessage;


        public CourseSequenceViewModel
        (
            CourseStore courseStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _courserVMs = [];
            _courseStore = courseStore;
            _courseStore.CoursesLoaded += CourseStore_CoursesLoaded;
            _courseStore.CourseAdded += CourseStore_CourseAdded;
            _courseStore.CourseUpdated += CourseStore_CourseUpdated;
            _courseStore.CourseDeleted += CourseStore_CourseDeleted;

            _viewStore = viewStore;
            _viewStore.CourseUnfocused += ViewStore_CourseUnfocused;

            _modalNavigationStore = modalNavigationStore;
        }

        public override void Dispose()
        {
            _courseStore.CoursesLoaded -= CourseStore_CoursesLoaded;
            _courseStore.CourseAdded -= CourseStore_CourseAdded;
            _courseStore.CourseUpdated -= CourseStore_CourseUpdated;
            _courseStore.CourseDeleted -= CourseStore_CourseDeleted;
            _viewStore.CourseUnfocused -= ViewStore_CourseUnfocused;

            base.Dispose();
        }

        public void SetGroupSequence(GroupSequenceViewModel newGroup)
        {
            _groupSequenceVM = newGroup;
        }

        public void ConfigureCommands()
        {
            if (_groupSequenceVM is null)
            {
                return;
            }

            LoadCoursesCommand = new LoadCoursesCommand(_courseStore, this);

            CreateCourseCommand = new OpenCreateCourseFormCommand(
                _courseStore,
                _viewStore,
                _modalNavigationStore,
                _groupSequenceVM);

            UpdateCourseCommand = new OpenUpdateCourseFormCommand(
                _courseStore,
                _selectedCourse,
                _viewStore,
                _modalNavigationStore,
                _groupSequenceVM);

            DeleteCourseCommand = new DeleteCourseCommand(_courseStore);
        }

        private void RefreshDependenciesOnAdding(Course newCourse)
        {
            newCourse.Groups.ToList()
                .ForEach(
                    g =>
                    {
                        g.CourseId = newCourse.CourseId;
                        g.Course = newCourse;
                    });
        }

        private void RefreshDependenciesOnUpdating(Course targetCourse)
        {
            CourseViewModel? sourceCourse = GetCourseViewModelById(targetCourse.CourseId);

            sourceCourse?.Groups.ToList()
                .ForEach(
                    g =>
                    {
                        g.CourseId = null;
                        g.Course = null;
                    });

            targetCourse.Groups.ToList()
                .ForEach(
                    g =>
                    {
                        g.CourseId = targetCourse.CourseId;
                        g.Course = targetCourse;
                    });
        }

        private void RefreshDependenciesOnDeleting(Guid courseId)
        {
            CourseViewModel courseVM = GetCourseViewModelById(courseId)!;
            courseVM.Course.Groups.ToList().ForEach(
                g =>
                {
                    g.CourseId = null;
                    g.Course = null;
                });
        }

        private CourseViewModel? GetCourseViewModelById(Guid id)
        {
            CourseViewModel? courseVM = _courserVMs.FirstOrDefault(cvm => cvm.CourseId == id);
            return courseVM;
        }

        public Task? LoadCourses()
        {
            return (LoadCoursesCommand as AsyncCommandBase)?.ExecuteAsync(null);
        }

        private void AddCourse(Course course)
        {
            CourseViewModel courseVM = new(course);
            _courserVMs.Add(courseVM);
        }

        private void UpdateCourse(Course targetCourse)
        {
            Guid targetId = targetCourse.CourseId;
            CourseViewModel sourceCourse = GetCourseViewModelById(targetId)!;
            sourceCourse.Course = targetCourse;
        }

        private void DeleteCourse(Guid courseId)
        {
            CourseViewModel courseVM = GetCourseViewModelById(courseId)!;
            _courserVMs.Remove(courseVM);
        }

        private void UnfocusCourse()
        {
            SelectedCourse = null;
        }

        private void CourseStore_CoursesLoaded()
        {
            _courserVMs.Clear();
            _courseStore.Courses.ToList().ForEach(
                c =>
                {
                    Course clonedCourse = SerializationCopier.DeepCopy(c)!;
                    clonedCourse.Groups = [];
                    AddCourse(clonedCourse);
                });
        }

        private void CourseStore_CourseAdded(Course newCourse)
        {
            RefreshDependenciesOnAdding(newCourse);
            AddCourse(newCourse);
            OnPropertyChanged(nameof(CourseVMs));
        }

        private void CourseStore_CourseUpdated(Course targetCourse)
        {
            RefreshDependenciesOnUpdating(targetCourse);
            UpdateCourse(targetCourse);
            OnPropertyChanged(nameof(CourseVMs));
        }

        private void CourseStore_CourseDeleted(Guid courseId)
        {
            RefreshDependenciesOnDeleting(courseId);
            DeleteCourse(courseId);
            OnPropertyChanged(nameof(CourseVMs));
        }

        private void ViewStore_CourseUnfocused()
        {
            UnfocusCourse();
        }
    }
}