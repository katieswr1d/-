using ContactBook.Core.Entity;


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
    
/*
 * Класс ApiResponse предназначен для представления структуры ответа от API в приложении WPF.
 * Он содержит информацию о результатах запроса (например, список контактов), статусе выполнения и возможных ошибках.
 * Это позволяет удобно обрабатывать данные и состояние операции в приложении.
*/