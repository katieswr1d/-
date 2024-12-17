using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class SearchContactDialog : Window
    {
        public SearchContactDialog(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = new SearchContactViewModel(mainViewModel); // Устанавливаем контекст данных
        }
    }
}