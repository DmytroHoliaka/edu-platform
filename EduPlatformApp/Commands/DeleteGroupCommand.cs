using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands
{
    public class DeleteGroupCommand : AsyncCommandBase
    {
        public GroupViewModel? DeletingGroup { get; set; }
        private readonly GroupStore _groupStore;

        public DeleteGroupCommand(GroupStore groupStore)
        {
            _groupStore = groupStore;
        }

        public override bool CanExecute(object? parameter)
        {
            if (DeletingGroup is null)
            {
                return false;
            }

            return DeletingGroup.GroupStudents.Count == 0;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _groupStore.Delete(DeletingGroup!.GroupId);
            }
            catch (Exception) { /*ToDo: Make label with error message*/ }
        }
    }
}
