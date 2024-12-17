using System.Collections.ObjectModel;
using System.ComponentModel;
using ContactBook.Core.Entity;
using WpfApp1.Services;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly ApiService _apiService;
    private ObservableCollection<Contact> _contacts;
    private string _searchQuery;

    public ObservableCollection<Contact> Contacts
    {
        get => _contacts;
        set
        {
            _contacts = value;
            OnPropertyChanged(nameof(Contacts));
        }
    }

    public async Task SearchContacts(string query)
    {
        Contacts.Clear(); // Очистка списка перед поиском

        // Проверка, чтобы запрос не был пустым
        if (!string.IsNullOrWhiteSpace(query))
        {
            var contacts = await _apiService.SearchContactsAsync(query); // Передаем только query
            Contacts = new ObservableCollection<Contact>(contacts); // Обновляем список
        }
        else
        {
            // Если запрос пустой, можно загрузить все контакты
            await LoadContacts(); // Можете закомментировать эту строку, если не хотите загружать все контакты при пустом запросе
        }
    }

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            OnPropertyChanged(nameof(SearchQuery));
        }
    }

    public MainViewModel(ApiService apiService) // Внедрение зависимости
    {
        _apiService = apiService;
        Contacts = new ObservableCollection<Contact>();
        InitializeAsync(); // Запускаем асинхронную инициализацию
    }

    private async void InitializeAsync()
    {
        await LoadContacts(); // Вызываем асинхронный метод
    }

    public async Task LoadContacts()
    {
        var contacts = await _apiService.GetContactsAsync(); // Получаем всех контактов
        Contacts = new ObservableCollection<Contact>(contacts); // Обновляем список
    }

    
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


/*
 * Класс MainViewModel служит связующим звеном между пользовательским интерфейсом и логикой работы с данными о контактах.
 * Он управляет состоянием приложения, обрабатывает ввод пользователя (например, поисковые запросы и добавление новых контактов)
 * и взаимодействует с API для получения и изменения данных. Используя паттерн MVVM (Model-View-ViewModel),
 * этот класс способствует более чистой архитектуре приложения, разделяя логику представления от бизнес-логики и данных.
 */