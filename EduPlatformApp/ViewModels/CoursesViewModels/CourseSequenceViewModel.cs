using EduPlatform.WPF.Commands.TeacherCommands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.CoursesViewModels
{
    // ToDo: Solve problem with "<not specified>". Update with <not specifief> doesn't works
    // ToDo: Make description label highter
    public class CourseSequenceViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CourseViewModel> _courserVMs;
        public IEnumerable<CourseViewModel> CourseVMs =>
            _courserVMs.Select(cvm => new CourseViewModel(cvm.Course));

        public CourseViewModel? SelectedCourse
        {
            get
            {
                return _selectedCourse;
            }
            set
            {
                _selectedCourse = value;
                OnPropertyChanged(nameof(SelectedCourse));

                ((OpenUpdateCourseFormCommand)UpdateCourseCommand).UpdatingCourse = value;
                ((OpenUpdateCourseFormCommand)UpdateCourseCommand).OnCanExecutedChanded();

                ((DeleteCourseCommand)DeleteCourseCommand).DeletingCourse = value;
                ((DeleteCourseCommand)DeleteCourseCommand).OnCanExecutedChanded();
            }
        }

        public ICommand CreateCourseCommand { get; private set; }
        public ICommand UpdateCourseCommand { get; private set; }
        public ICommand DeleteCourseCommand { get; private set; }

        private GroupSequenceViewModel? _groupSequenceVM;
        private CourseViewModel? _selectedCourse;
        private readonly CourseStore _courseStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;


        public CourseSequenceViewModel
        (
            CourseStore courseStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _courserVMs = [];
            _courseStore = courseStore;
            _courseStore.CourseAdded += CourseStore_CourseAdded;
            _courseStore.CourseUpdated += CourseStore_CourseUpdated;
            _courseStore.CourseDeleted += CourseStore_CourseDeleted;

            _viewStore = viewStore;
            _viewStore.TeacherUnfocused += ViewStore_CourseUnfocused;

            _modalNavigationStore = modalNavigationStore;
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

            CreateCourseCommand = new OpenCreateCourseFormCommand(_courseStore,
                                                                  _viewStore,
                                                                  _modalNavigationStore,
                                                                  _groupSequenceVM);

            UpdateCourseCommand = new OpenUpdateCourseFormCommand(_courseStore,
                                                                  _selectedCourse,
                                                                  _viewStore,
                                                                  _modalNavigationStore,
                                                                  _groupSequenceVM);

            DeleteCourseCommand = new DeleteCourseCommand(_courseStore,
                                                          _modalNavigationStore);
        }

        // ToDo: Remove 
        public void InsertTestData()
        {
            Course course1 = new()
            {
                CourseId = Guid.NewGuid(),
                Name = "C# basic",
                Description = "Course about basic C#",
                Groups = _groupSequenceVM!.GroupVMs.Take(new Range(0, 2)).Select(gvm => gvm.Group).ToList(),
            };

            Course course2 = new()
            {
                CourseId = Guid.NewGuid(),
                Name = "Python Fundamentals",
                Description = "Course covering the fundamentals of Python programming",
                Groups = _groupSequenceVM!.GroupVMs.Take(new Range(1, 3)).Select(gvm => gvm.Group).ToList(),
            };

            Course course3 = new()
            {
                CourseId = Guid.NewGuid(),
                Name = "Web Development 101",
                Description = "Introduction to web development technologies and concepts",
                Groups = _groupSequenceVM!.GroupVMs.Take(new Range(3, 4)).Select(gvm => gvm.Group).ToList(),
            };

            Course course4 = new()
            {
                CourseId = Guid.NewGuid(),
                Name = "Data Structures and Algorithms",
                Description = "Course on data structures and algorithms",
                Groups = _groupSequenceVM!.GroupVMs.Take(new Range(2, 3)).Select(gvm => gvm.Group).ToList(),
            };

            Course course5 = new()
            {
                CourseId = Guid.NewGuid(),
                Name = "Mobile App Development",
                Description = "Course on developing mobile applications",
                Groups = _groupSequenceVM!.GroupVMs.Take(new Range(0, 4)).Select(gvm => gvm.Group).ToList(),
            };

            _courserVMs.Add(new CourseViewModel(course1));
            _courserVMs.Add(new CourseViewModel(course2));
            _courserVMs.Add(new CourseViewModel(course3));
            _courserVMs.Add(new CourseViewModel(course4));
            _courserVMs.Add(new CourseViewModel(course5));
        }

        private void RefreshDependentGroups(Course targetCourse)
        {
            CourseViewModel? sourceCourse = GetCourseViewModelById(targetCourse.CourseId);

            //sourceCourse?.Groups.ToList()
            //    .ForEach(g => g.Teachers.Remove(sourceCourse.Teacher));

            //targetCourse.Groups.ToList()
            //    .ForEach(g => g.Teachers.Add(targetCourse));
        }

        private CourseViewModel? GetCourseViewModelById(Guid id)
        {
            CourseViewModel? courseVM = _courserVMs.FirstOrDefault(cvm => cvm.CourseId == id);
            return courseVM;
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

        private void UnfocuseCourse()
        {
            SelectedCourse = null;
        }

        private void CourseStore_CourseAdded(Course course)
        {
            RefreshDependentGroups(course);
            AddCourse(course);
            OnPropertyChanged(nameof(CourseVMs));
        }

        private void CourseStore_CourseUpdated(Course targetCourse)
        {
            RefreshDependentGroups(targetCourse);
            UpdateCourse(targetCourse);
            OnPropertyChanged(nameof(CourseVMs));
        }

        private void CourseStore_CourseDeleted(Guid courseId)
        {
            DeleteCourse(courseId);
            OnPropertyChanged(nameof(CourseVMs));
        }

        private void ViewStore_CourseUnfocused()
        {
            UnfocuseCourse();
        }
    }
}
