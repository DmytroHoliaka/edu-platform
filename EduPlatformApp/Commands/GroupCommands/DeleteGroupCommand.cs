using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class DeleteGroupCommand(GroupStore groupStore) : AsyncCommandBase
    {
        public GroupViewModel? DeletingGroup { get; set; }

        public override bool CanExecute(object? parameter)
        {
            return 
                DeletingGroup is not null &&
                DeletingGroup.StudentVMs.Count == 0;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Guid groupId = DeletingGroup!.GroupId;

            try
            {
                await groupStore.Delete(groupId);
            }
            catch (Exception) { /*ToDo: Make label with error message*/ }
        }
    }
}
