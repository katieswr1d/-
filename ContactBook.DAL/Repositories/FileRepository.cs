using ContactBook.Core.Entity; 
using ContactBook.DAL.Data; 
using Microsoft.EntityFrameworkCore; 

namespace ContactBook.DAL.Repositories; 

// Реализация интерфейса IRepositoty для работы с контактами
public class FileRepository : IRepositoty
{
    private readonly ContactBookDbContext _context; // Наша БД, контекст для работы с сущностями

    // Конструктор класса, принимает контекст базы данных
    // Контекст-фигня для связи бд и кода
    public FileRepository(ContactBookDbContext context)
    {
        _context = context; // Присваиваем контекст, переданный в конструктор
    }

   
    // Метод для создания нового контакта(по частям)
    public async Task<bool> Create(string firstName, string lastName, List<string> emailList,
        List<string> phoneNumberList)
    {

        var emails = emailList.Select(e => new Email(e)).ToList();
        var phones = phoneNumberList.Select(p => new PhoneNumber(p)).ToList();


        var contact = new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            Emails = emails,
            Phones = phones
        };

        // Добавляем новый контакт в контекст
        _context.contacts.Add(contact);
        await _context.SaveChangesAsync(); // Сохраняем изменения в базе данных
        return true;
    }
    
    //Метод такой же но принимает готовый контакт 
    public async Task<bool> Create(Contact contact)
    {
        
        // Добавляем новый контакт в контекст
        _context.contacts.Add(contact);
        await _context.SaveChangesAsync(); // Сохраняем изменения в базе данных
        return true;
    }

    // Метод для чтения всех контактов из базы данных
    public async Task<IEnumerable<Contact>> ReadAll()
    {
        // Возвращаем список всех контактов с подгруженными связанными данными
        return await _context.contacts
            .Include(c => c.Emails) // Подгружает списки email
            .Include(c => c.Phones) // Подгружает списки телефонов
            .ToListAsync(); // Асинхронное выполнение
    }

    // Метод для фильтрации контактов по заданному предикату (условию)
    public async Task<IEnumerable<Contact>> FilterContacts(Func<Contact, bool> predicate)
    {
        // Получаем все контакты и фильтруем их по заданному предикату
        var contacts = await _context.contacts
            .Include(c => c.Emails) // Подгружает списки email
            .Include(c => c.Phones) // Подгружает списки телефонов
            .ToListAsync(); // Асинхронное выполнение

        return contacts.Where(predicate); // Применяем предикат для фильтрации
    }
    /*
     * Функция Include() в LINQ служит для жадной загрузки связанных сущностей, предотвращая тем самым проблему N+1 запроса.
     * Она дополняет основной запрос данными о связях, выполняя по существу функцию SQL JOIN.
     * Когда вы используете Include, вы говорите Entity Framework, что хотите загрузить определенные связанные сущности вместе с основной сущностью.
       Например, если у вас есть сущность Contact, которая имеет связи с EmailList и PhoneNumberList, использование Include позволяет загрузить
       эти связанные данные в одном запросе к базе данных
.      Это снижает количество запросов и улучшает производительность.
     */

    /* Метод для фильтрации контактов по заданному предикату (условию)
    public async Task<IEnumerable<Contact>> FilterContacts(Func<Contact, bool> predicate)
    {
        return await Task.Run(() =>
            _context.contacts
                .Include(c => c.EmailList) // Подгружает списки email
                .Include(c => c.PhoneNumberList) // Подгружает списки телефонов
                .Where(predicate) // Применяет предикат для фильтрации
                .ToList()); // Конвертирование в список
    }*/

    // Метод для удаления всех контактов
   

public async Task<Contact> GetById(int id)
{
    var contact = await _context.contacts
        .Include(c => c.Phones) // Загружаем список номеров телефонов
        .Include(c => c.Emails) // Загружаем список email
        .FirstOrDefaultAsync(c => c.Id == id); // Используем FirstOrDefaultAsync для поиска по id

    if (contact == null) throw new NullReferenceException();

    return contact;
}
    public async Task Clear()
    {
        //IQueriable vs IEnumerable
        //_context.contacts.RemoveRange(_context.contacts);
        foreach (Contact contact in _context.contacts)
        {
            _context.contacts.Remove(contact);
        }
        await _context.SaveChangesAsync();
    }
    public int Count()
    {
        return _context.contacts.Count();
    }

} // Удаляем все контакты из контекста
  //IEnumerable работает со всем массивом данных, а IQueryable с отфильтрованным.
  //IEnumerable получает все данные на стороне сервера и загружает их в память а затем позволяет сделать фильтрацию по данным из памяти.
  //Когда делается запрос к базе данных, IQueryable выполняет запрос на серверной стороне и в запросе применяет фильтрацию.