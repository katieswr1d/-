using System.Windows;

namespace WpfApp1.Views;

public partial class SearchContactDialog : Window
{
    public SearchContactDialog()
    {
        InitializeComponent();
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        var query = QueryTextBox.Text; // Получаем текст из текстового поля

        // Передайте параметр в ViewModel
        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow.ViewModel.SearchContacts(query); // Передаем только query

        this.DialogResult = true; // Установите результат диалога
        this.Close(); // Закрыть диалог
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false; // Установите результат диалога на false
        this.Close(); // Закрыть диалог
    }
}