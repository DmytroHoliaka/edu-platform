using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.NavigationsViewModels;
using EduPlatform.WPF.ViewModels.OverviewViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.ViewModels.GeneralViewModels
{
    public class HubViewModel : ViewModelBase
    {
        public NavigationViewModel NavigationVM { get; }
        public EduPlatformOverviewViewModel EduPlatformOverviewVM { get; }
        public CourseSequenceViewModel CourseSequenceVM { get; }
        public GroupSequenceViewModel GroupSequenceVM { get; }
        public StudentSequenceViewModel StudentSequenceVM { get; }
        public TeacherSequenceViewModel TeacherSequenceVM { get; }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => string.IsNullOrEmpty(ErrorMessage) == false;

        private string? _errorMessage;
        private readonly GroupStore _groupStore;

        public HubViewModel
        (
            CourseStore courseStore,
            GroupStore groupStore,
            StudentStore studentStore,
            TeacherStore teacherStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _groupStore = groupStore;

            EduPlatformOverviewVM = new();
            NavigationVM = new();

            GroupSequenceVM = new
            (
                _groupStore,
                studentStore,
                viewStore,
                modalNavigationStore
            );

            CourseSequenceVM = new
            (
                courseStore,
                viewStore,
                modalNavigationStore
            );

            TeacherSequenceVM = new
            (
                teacherStore,
                viewStore,
                modalNavigationStore
            );

            StudentSequenceVM = new
            (
                studentStore,
                viewStore,
                modalNavigationStore
            );

            GroupSequenceVM.SetCourseSequence(CourseSequenceVM);
            GroupSequenceVM.SetStudentSequence(StudentSequenceVM);
            GroupSequenceVM.SetTeacherSequence(TeacherSequenceVM);
            GroupSequenceVM.ConfigureCommands();

            CourseSequenceVM.SetGroupSequence(GroupSequenceVM);
            CourseSequenceVM.ConfigureCommands();

            TeacherSequenceVM.SetGroupSequence(GroupSequenceVM);
            TeacherSequenceVM.ConfigureCommands();

            StudentSequenceVM.SetGroupSequence(GroupSequenceVM);
            StudentSequenceVM.ConfigureCommands();
        }

        public async Task ConfigureDataFromDatabase()
        {
            await LoadDataAsync();

            List<ISequenceViewModel> errorComponents =
            [
                CourseSequenceVM,
                GroupSequenceVM,
                StudentSequenceVM,
                TeacherSequenceVM
            ];

            if (errorComponents.Exists(e => string.IsNullOrWhiteSpace(e.ErrorMessage) == false))
            {
                ErrorMessage = BuildErrorMessage(errorComponents);
                return;
            }

            SetRelationships();
        }

        private static string BuildErrorMessage(List<ISequenceViewModel> errorComponents)
        {
            string errorMsg =
                string.Join(
                    separator: Environment.NewLine,
                    values: errorComponents
                        .Where(e => string.IsNullOrWhiteSpace(e.ErrorMessage) == false)
                        .Select(e => e.ErrorMessage));

            return errorMsg;
        }

        private async Task LoadDataAsync()
        {
            await CourseSequenceVM.LoadCourses()!;
            await GroupSequenceVM.LoadGroups()!;
            await StudentSequenceVM.LoadStudents()!;
            await TeacherSequenceVM.LoadTeachers()!;
        }

        private void SetRelationships()
        {
            _groupStore.Groups.ToList().ForEach(
                g =>
                {
                    Guid? courseId = g.CourseId;
                    IEnumerable<Guid> studentIds = g.Students.Select(s => s.StudentId);
                    IEnumerable<Guid> teacherIds = g.Teachers.Select(t => t.TeacherId);

                    Course? relatedCourse = CourseSequenceVM.CourseVMs
                        .FirstOrDefault(c => c.CourseId == courseId)?.Course;

                    List<Student> relatedStudents = StudentSequenceVM.StudentVMs
                        .Where(svm => studentIds.Contains(svm.StudentId))
                        .Select(svm => svm.Student)
                        .ToList();

                    List<Teacher> relatedTeachers = TeacherSequenceVM.TeacherVMs
                        .Where(tvm => teacherIds.Contains(tvm.TeacherId))
                        .Select(tvm => tvm.Teacher)
                        .ToList();

                    Group group = GroupSequenceVM.GroupVMs
                        .First(gvm => gvm.GroupId == g.GroupId).Group;

                    group.Course = relatedCourse;
                    group.Students = relatedStudents;
                    group.Teachers = relatedTeachers;

                    relatedCourse?.Groups.Add(group);
                    relatedStudents.ForEach(s => s.Group = group);
                    relatedTeachers.ForEach(t => t.Groups.Add(group));
                });
        }
    }
}