using EduPlatform.WPF.Commands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.TeachersViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class GroupSequenceViewModel : ViewModelBase
    {
        public ObservableCollection<GroupViewModel> GroupVMs { get; }

        public GroupViewModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));

                ((OpenUpdateGroupFormCommand)UpdateGroupCommand).UpdatingGroup = value;
                ((CommandBase)UpdateGroupCommand).OnCanExecutedChanded();

                ((DeleteGroupCommand)DeleteGroupCommand).DeletingGroup = value;
                ((CommandBase)DeleteGroupCommand).OnCanExecutedChanded();
            }
        }

        public ICommand CreateGroupCommand { get; private set; }
        public ICommand UpdateGroupCommand { get; private set; }
        public ICommand DeleteGroupCommand { get; private set; }

        private readonly GroupStore _groupStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private GroupViewModel? _selectedGroup;
        private TeacherSequenceViewModel? _teacherSequenceVM;
        private StudentSequenceViewModel? _studentSequenceVM;


        public GroupSequenceViewModel(GroupStore groupStore,
                                      ViewStore viewStore,
                                      ModalNavigationStore modalNavigationStore)
        {
            GroupVMs = [];

            _groupStore = groupStore;
            _groupStore.GroupAdded += GroupStore_GroupAdded;
            _groupStore.GroupUpdated += GroupStore_GroupUpdated;
            _groupStore.GroupDeleted += GroupStore_GroupDeleted;

            _viewStore = viewStore;
            _viewStore.GroupUnfocused += ViewStore_GroupUnfocused;

            _modalNavigationStore = modalNavigationStore;
        }

        public void SetTeacherSequence(TeacherSequenceViewModel newTeacherSequence)
        {
            _teacherSequenceVM = newTeacherSequence;
        }

        public void SetStudentSequence(StudentSequenceViewModel newStudentSequence)
        {
            _studentSequenceVM = newStudentSequence;
        }

        public void ConfigureCommands()
        {
            CreateGroupCommand = new OpenCreateGroupFormCommand
            (
                _groupStore, 
                _viewStore, 
                _modalNavigationStore, 
                _teacherSequenceVM, 
                _studentSequenceVM
            );

            UpdateGroupCommand = new OpenUpdateGroupFormCommand
            (
                _groupStore, 
                _viewStore, 
                _modalNavigationStore, 
                _teacherSequenceVM, 
                _studentSequenceVM
            );

            DeleteGroupCommand = new DeleteGroupCommand(_groupStore);
        }

        // ToDo: Remove
        public void InsertTestData()
        {
            GroupVMs.Add(new GroupViewModel(new Group()
            {
                GroupId = Guid.NewGuid(),
                Name = "Group 1",
                CourseId = 1,
                Teachers = _teacherSequenceVM.TeacherVMs.Select(tvm => tvm.Teacher).Take(2).ToList(),
                Students = _studentSequenceVM.StudentVMs.Select(svm => svm.Student).Take(2).ToList(),
            }));

            GroupVMs.Add(new GroupViewModel(new Group()
            {
                GroupId = Guid.NewGuid(),
                Name = "Group 2",
                CourseId = 2,
                Teachers = _teacherSequenceVM.TeacherVMs.Select(tvm => tvm.Teacher).TakeLast(2).ToList(),
                Students = _studentSequenceVM.StudentVMs.Select(svm => svm.Student).TakeLast(2).ToList(),
            }));
        }

        protected override void Dispose()
        {
            _groupStore.GroupAdded -= GroupStore_GroupAdded;
            _groupStore.GroupUpdated -= GroupStore_GroupUpdated;

            base.Dispose();
        }

        private void GroupStore_GroupAdded(Group groupItem)
        {
            AddGroup(groupItem);
        }

        private void AddGroup(Group groupItem)
        {
            GroupViewModel item = new(groupItem);

            GroupVMs.Add(item);
        }

        private void GroupStore_GroupUpdated(Guid sourceId, Group targetGroup)
        {
            UpdateGroup(sourceId, targetGroup);
        }

        private void UpdateGroup(Guid sourceId, Group targetGroup)
        {
            GroupViewModel? sourceGroupVM = GroupVMs.FirstOrDefault(g => g.GroupId == targetGroup.GroupId);

            if (sourceGroupVM is null)
            {
                return;
            }

            sourceGroupVM.Group = targetGroup;
        }

        private void GroupStore_GroupDeleted(Guid groupId)
        {
            DeleteGroup(groupId);
        }

        private void DeleteGroup(Guid groupId)
        {
            GroupViewModel? group = GroupVMs.FirstOrDefault(g => g.GroupId == groupId);

            if (group is null)
            {
                return;
            }

            GroupVMs.Remove(group);
        }

        private void ViewStore_GroupUnfocused()
        {
            UnfocusGroup();
        }

        private void UnfocusGroup()
        {
            SelectedGroup = null;
        }
    }
}
