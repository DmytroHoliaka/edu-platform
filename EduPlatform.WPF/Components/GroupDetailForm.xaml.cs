﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace EduPlatform.WPF.Components
{
    /// <summary>
    /// Interaction logic for GroupDetailForm.xaml
    /// </summary>
    public partial class GroupDetailForm
    {
        public GroupDetailForm()
        {
            
            InitializeComponent();
        }

        private void RadioButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.IsChecked == true)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    radioButton.IsChecked = false;
                }), DispatcherPriority.Input);

                e.Handled = true;
            }
        }

        private void CheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.IsChecked == true)
                {
                    checkBox.IsEnabled = true;
                }
            }
        }

        private void CheckBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.IsChecked == false)
                {
                    checkBox.IsEnabled = true;
                }
            }
        }
    }
}
