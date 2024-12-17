

using System.Windows;
using ContactBook.Core.Entity;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(new ApiService("http://localhost:5243/"));
            DataContext = ViewModel;
        }
    }
}