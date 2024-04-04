using EduPlatform.WPF.Commands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModel;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class GroupSequenceViewModel : ViewModelBase
    {
        public ObservableCollection<GroupViewModel> GroupVMs { get; }

        public GroupViewModel? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                (UpdateGroupCommand as CommandBase)?.OnCanExecutedChanded();
            }
        }

        public ICommand? CreateGroupCommand { get; }
        public ICommand? UpdateGroupCommand { get; }
        public ICommand? DeleteGroupCommand { get; }

        private GroupViewModel? _selectedItem;
        private readonly GroupStore _groupStore;

        public GroupSequenceViewModel(GroupStore groupStore,
                                      ModalNavigationStore modalNavigationStore,
                                      TeacherSequenceViewModel teacherSequenceVM,
                                      StudentSequenceViewModel studentSequenceVM)
        {
            GroupVMs = [];

            _groupStore = groupStore;
            _groupStore.GroupAdded += GroupStore_GroupAdded;
            _groupStore.GroupUpdated += GroupStore_GroupUpdated;

            CreateGroupCommand = new OpenCreateGroupFormCommand(_groupStore, modalNavigationStore, teacherSequenceVM, studentSequenceVM);
            UpdateGroupCommand = new OpenUpdateGroupFormCommand(this, _groupStore, modalNavigationStore, teacherSequenceVM, studentSequenceVM);
            DeleteGroupCommand = null;
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
    }
}
