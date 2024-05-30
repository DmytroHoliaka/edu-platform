using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;

namespace EduPlatform.WPF.Commands.GeneralCommands
{
    public class CloseFormCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        public override void Execute(object? parameter)
        {
            modalNavigationStore.Close();
        }
    }
}
