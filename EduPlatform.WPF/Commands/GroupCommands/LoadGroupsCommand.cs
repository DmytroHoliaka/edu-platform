using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class LoadGroupsCommand(GroupStore groupStore, GroupSequenceViewModel groupSequenceViewModel) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            groupSequenceViewModel.ErrorMessage = null;

            try
            {
                await groupStore.Load();
            }
            catch (Exception)
            {
                groupSequenceViewModel.ErrorMessage = "Unable to load group data from database";
            }
        }
    }
}
