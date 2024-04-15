using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands
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
            return 
                DeletingGroup is not null &&
                DeletingGroup.StudentVMs.Count == 0;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // ToDo: Work with database
            Guid groupId = DeletingGroup!.GroupId;

            try
            {
                await _groupStore.Delete(groupId);
            }
            catch (Exception) { /*ToDo: Make label with error message*/ }
        }
    }
}
