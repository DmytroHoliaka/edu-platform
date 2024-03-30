using CommunityToolkit.Mvvm.Input;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.GroupViewModels
{
    public class StudentFormItemViewModel : ViewModelBase
    {
        public string? FullName { get; }

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        private bool _isChecked;


        public StudentFormItemViewModel(string fullName, bool isSelected = false)
        {
            FullName = fullName;
            IsChecked = isSelected;
        }
    }
}
