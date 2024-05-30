using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Service;
using EduPlatform.WPF.ViewModels.NavigationsViewModels;

namespace EduPlatform.WPF.Commands.NavigationCommands;

public class ChangePageCommand(NavigationViewModel navigationViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (parameter is PageId pageId)
        {
            navigationViewModel.PageId = pageId;
        }
    }
}