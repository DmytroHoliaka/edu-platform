using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class LoadGroupsCommand(GroupStore groupStore) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await groupStore.Load();
            }
            catch (Exception)
            {
                // ToDo: Write correct exception handling
                throw;
            }
        }
    }
}
