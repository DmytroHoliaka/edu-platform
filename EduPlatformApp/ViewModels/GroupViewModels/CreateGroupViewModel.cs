using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlatform.WPF.ViewModels.GroupViewModels
{
    public class CreateGroupViewModel : ViewModelBase
    {
        public GroupDetailsFormViewModel GroupDetailsFormViewModel { get; }

        public CreateGroupViewModel()
        {
            GroupDetailsFormViewModel = new();
        }
    }
}
