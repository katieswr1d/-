using System.Collections.ObjectModel;
using System.ComponentModel;
using ContactBook.Core.Entity;
using WpfApp1.Services;



namespace WpfApp1.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly ApiService _apiService;
    private ObservableCollection<Contact> _contacts;
    private string _searchQuery;

    public ObservableCollection<Contact> Contacts
    {
        get => _contacts;
        set { _contacts = value; OnPropertyChanged(nameof(Contacts)); }
    }

    public string SearchQuery
    {
        get => _searchQuery;
        set { _searchQuery = value; OnPropertyChanged(nameof(SearchQuery)); }
    }

    public MainViewModel(ApiService apiService) // Внедрение зависимости
    {
        _apiService = apiService;
        Contacts = new ObservableCollection<Contact>();
        LoadContacts();
    }

    public async Task LoadContacts()
    {
        var contacts = await _apiService.GetContactsAsync(); // Получаем всех контактов
        Contacts = new ObservableCollection<Contact>(contacts); // Обновляем список
    }

    public async Task SearchContacts(string query)
    {
        Contacts.Clear(); // Очистка списка перед поиском
    
        // Проверка, чтобы запрос не был пустым
        if (!string.IsNullOrWhiteSpace(query))
        {
            var contacts = await _apiService.SearchContactsAsync(query); // Передаем только query
            Contacts = new ObservableCollection<Contact>(contacts);
        }
        
    }

    public async Task AddContact(Contact contact)
    {
        await _apiService.CreateContactAsync(contact);
        LoadContacts(); // Обновить список контактов
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}