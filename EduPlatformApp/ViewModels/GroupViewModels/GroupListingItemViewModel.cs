using CommunityToolkit.Mvvm.Input;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.GroupViewModels
{
    public class GroupListingItemViewModel : ViewModelBase
    {
        public string? Name { get; }

        public ICommand? ExportCsvCommand { get; } = new RelayCommand(ExportIntoCsv);
        public ICommand? ImportCsvCommand { get; } = new RelayCommand(ImportFromCsv);
        public ICommand? CreateDocsCommand { get; } = new RelayCommand(CreateDocs);
        public ICommand? CreatePdfCommand { get; } = new RelayCommand(CreatePdf);

        public GroupListingItemViewModel(string name)
        {
            Name = name;
        }

        private static void ExportIntoCsv()
        {
            MessageBox.Show("Export");
        }

        private static void ImportFromCsv()
        {
            MessageBox.Show("Import");
        }

        private static void CreateDocs()
        {
            MessageBox.Show("Docs");
        }

        private static void CreatePdf()
        {
            MessageBox.Show("Pdf");
        }
    }
}
