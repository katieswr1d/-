using System.Windows;

namespace WpfApp1.Views;

public partial class AddContactDialog : Window
{
    public AddContactDialog()
    {
        InitializeComponent();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        // Здесь вы можете собрать данные о новом контакте
        // Например, создать новый объект Contact и передать его в ViewModel
        this.DialogResult = true; // Установите результат диалога
        this.Close(); // Закрыть диалог
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false; // Установите результат диалога
        this.Close(); // Закрыть диалог
    }
}