using System.Net.Http;
using Newtonsoft.Json;
using ContactBook.Core.Entity;
using WpfApp1.Models;

// Используйте существующие модели

namespace WpfApp1.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(string baseUrl)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    // Получение всех контактов
    public async Task<List<Contact>> GetContactsAsync()
    {
        var response = await _httpClient.GetAsync("api/contacts");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Contact>>(json);
    }

    // Добавление нового контакта
    public async Task<bool> CreateContactAsync(Contact contact)
    {
        var json = JsonConvert.SerializeObject(contact);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/contacts", content);
        return response.IsSuccessStatusCode;
    }

    // Поиск контактов
    public async Task<List<Contact>> SearchContactsAsync(string query)
    {
        // Создайте запрос с единственным параметром query
        var response = await _httpClient.GetAsync($"api/contacts/search?query={query}");
        response.EnsureSuccessStatusCode(); // Проверка успешного ответа

        var json = await response.Content.ReadAsStringAsync();

        // Десериализуйте JSON в ApiResponse
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);
    
        // Вернуть список контактов из результата
        return apiResponse.Result; // Предполагается, что Result содержит список Contact
    }
}

/*
 * Класс `ApiService` служит для абстракции взаимодействия с API для управления контактами.
 * Он предоставляет методы для получения всех контактов, создания нового контакта и поиска по контактам.
 * Использование асинхронных методов позволяет не блокировать основной поток приложения при выполнении сетевых операций,
 * что особенно важно в пользовательских интерфейсах, таких как WPF.
*/