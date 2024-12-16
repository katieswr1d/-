using ContactBook.Core.Entity;

// Убедитесь, что пространство имен доступно

namespace WpfApp1.Models;

public class ApiResponse
{
    public List<Contact>? Result { get; set; } // Список контактов, получаемый от API
    public int Id { get; set; }
    public string? Exception { get; set; }
    public int Status { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsCompletedSuccessfully { get; set; }
    public int CreationOptions { get; set; }
    public object? AsyncState { get; set; }
    public bool IsFaulted { get; set; }}