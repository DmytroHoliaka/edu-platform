using EduPlatform.WPF.ViewModels.GeneralViewModels;

namespace EduPlatform.WPF.ViewModels.GroupViewModels
{
    public class UpdateGroupViewModel : ViewModelBase
    {
        public GroupDetailsFormViewModel GroupDetailsFormViewModel { get; }

        public UpdateGroupViewModel()
        {
            GroupDetailsFormViewModel = new();
        }
    }
}
