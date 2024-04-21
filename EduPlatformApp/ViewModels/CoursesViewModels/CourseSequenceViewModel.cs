using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EduPlatform.WPF.Commands.CourseCommands;
using EduPlatform.WPF.Commands.GroupCommands;
using EduPlatform.WPF.Service;
using EduPlatform.WPF.Commands.BaseCommands;

namespace EduPlatform.WPF.ViewModels.CoursesViewModels
{
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
                ((OpenUpdateCourseFormCommand)UpdateCourseCommand).OnCanExecutedChanged();

                ((DeleteCourseCommand)DeleteCourseCommand).DeletingCourse = value;
                ((DeleteCourseCommand)DeleteCourseCommand).OnCanExecutedChanged();
            }
        }

        public ICommand LoadCoursesCommand { get; private set; }
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
            _courseStore.CoursesLoaded += CourseStore_CoursesLoaded;
            _courseStore.CourseAdded += CourseStore_CourseAdded;
            _courseStore.CourseUpdated += CourseStore_CourseUpdated;
            _courseStore.CourseDeleted += CourseStore_CourseDeleted;

            _viewStore = viewStore;
            _viewStore.CourseUnfocused += ViewStore_CourseUnfocused;

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

            LoadCoursesCommand = new LoadCoursesCommand(_courseStore);

            CreateCourseCommand = new OpenCreateCourseFormCommand(_courseStore,
                                                                  _viewStore,
                                                                  _modalNavigationStore,
                                                                  _groupSequenceVM);

            UpdateCourseCommand = new OpenUpdateCourseFormCommand(_courseStore,
                                                                  _selectedCourse,
                                                                  _viewStore,
                                                                  _modalNavigationStore,
                                                                  _groupSequenceVM);

            DeleteCourseCommand = new DeleteCourseCommand(_courseStore);
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

        private void RefreshDependenciesOnAdding(Course newCourse)
        {
            newCourse.Groups.ToList()
                .ForEach(g =>
                {
                    g.CourseId = newCourse.CourseId;
                    g.Course = newCourse;
                });
        }

        private void RefreshDependenciesOnUpdating(Course targetCourse)
        {
            CourseViewModel? sourceCourse = GetCourseViewModelById(targetCourse.CourseId);

            sourceCourse?.Groups.ToList()
                .ForEach(g =>
                {
                    g.CourseId = null;
                    g.Course = null;
                });

            targetCourse.Groups.ToList()
                .ForEach(g =>
                {
                    g.CourseId = targetCourse.CourseId;
                    g.Course = targetCourse;
                });
        }

        private void RefreshDependenciesOnDeleting(Guid courseId)
        {
            CourseViewModel courseVM = GetCourseViewModelById(courseId)!;
            courseVM.Course.Groups.ToList().ForEach(g =>
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

        public Task LoadCourses()
        {
            return ((AsyncCommandBase)LoadCoursesCommand).ExecuteAsync(null);
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
