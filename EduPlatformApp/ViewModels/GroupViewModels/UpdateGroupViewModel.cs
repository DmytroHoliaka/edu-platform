using EduPlatform.WPF.ViewModels.GroupViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlatform.WPF.ViewModels
{
    public class UpdateGroupViewModel
    {
        public GroupDetailsFormViewModel GroupDetailsFormViewModel { get; }

        public UpdateGroupViewModel()
        {
            GroupDetailsFormViewModel = new();
        }
    }
}
