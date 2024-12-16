using System.Windows;
using ContactBook.Core.Entity;
using WpfApp1.Services;
using WpfApp1.ViewModels;

// Убедитесь, что это подключено

namespace WpfApp1.Views
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; private set; } // Добавляем свойство ViewModel

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(new ApiService("http://localhost:5243/")); // Укажите базовый URL вашего API
            DataContext = ViewModel;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchDialog = new SearchContactDialog();
            if (searchDialog.ShowDialog() == true)
            {
                var query = searchDialog.QueryTextBox.Text; // Предполагается, что вы добавили это поле в диалоговое окно
                await ViewModel.SearchContacts(query);
            }
        }

        private async void FindAllButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadContacts(); // Вызываем метод для загрузки всех контактов
        }

        private async void AddContact_Click(object sender, RoutedEventArgs e)
        {
            var addDialog = new AddContactDialog();
            if (addDialog.ShowDialog() == true)
            {
                var newContact = new Contact
                {
                    FirstName = addDialog.FirstNameTextBox.Text,
                    LastName = addDialog.LastNameTextBox.Text,
                    EmailList = new List<Email> { new Email(addDialog.EmailTextBox.Text) },
                    PhoneNumberList = new List<PhoneNumber> { new PhoneNumber(addDialog.PhoneTextBox.Text) }
                };

                await ViewModel.AddContact(newContact);
            }
        }
    }
    
}