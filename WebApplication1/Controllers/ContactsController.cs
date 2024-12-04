using ContactBook.Core.Entity;
using ContactBook.DAL.Data;

namespace WebApplication1.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly ContactBookDbContext _context;

    public ContactsController(ContactBookDbContext context)
    {
        _context = context;
    }

    // Получить все контакты
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
    {
        var contacts = await _context.contacts
            .Include(c => c.Phones) // Загружаем список номеров телефонов
            .Include(c => c.Emails) // Загружаем список email
            .ToListAsync();

        return Ok(contacts);
    }

    // Добавить новый контакт
    [HttpPost]
    public async Task<ActionResult<Contact>> PostContact(Contact contact)
    {
        _context.contacts.Add(contact);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
    }

    // Получить контакт по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContact(int id)
    {
        var contact = await _context.contacts
            .Include(c => c.Phones) // Загружаем список номеров телефонов
            .Include(c => c.Emails) // Загружаем список email
            .FirstOrDefaultAsync(c => c.Id == id); // Используем FirstOrDefaultAsync для поиска по id

        if (contact == null)
        {
            return NotFound();
        }

        return Ok(contact);
    }


    // Поиск контактов по имени или номеру телефона
    [HttpGet("search")]
    
    public async Task<ActionResult<IEnumerable<Contact>>> SearchContacts(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty.");
        }

        var contacts = await _context.contacts
            .Include(c => c.Phones) // Загружаем связанные данные
            .Include(c => c.Emails) // Загружаем список email
            .Where(c => c.FirstName.Contains(query) || 
                    c.LastName.Contains(query) || 
                    c.Phones.Any(p => p.Value.Contains(query)) || 
                    c.Emails.Any(e => e.Value.Contains(query))) // Проверка email
            .ToListAsync();

        return Ok(contacts);
}


}
/*
    * Функция Include() в LINQ служит для загрузки связанных сущностей, предотвращая тем самым проблему N+1 запроса.
    * Она дополняет основной запрос данными о связях, выполняя по существу функцию SQL JOIN.
    * Когда вы используете Include, вы говорите Entity Framework, что хотите загрузить определенные связанные сущности вместе с основной сущностью.
      Например, если у вас есть сущность Contact, которая имеет связи с EmailList и PhoneNumberList, использование Include позволяет загрузить
      эти связанные данные в одном запросе к базе данных
.     Это снижает количество запросов и улучшает производительность.
    */

/*свагер это хуйня которая будет помогать отправлять запрос запросы api,
  ну то есть это по сути просто добавляет ui: нажимаешь кнопки вместо того чтобы их ручками писать в postman.
  как его поднять без свагера : в постмане сами пишем запросы

  Корс это хуйня браузера, которая позволяет нам пиздить, например при создании сайта,
  всякие картинки, стили, шрифты из других разных источников.
  Типо, есть наш сайт domenA.com и есть domenB.com, вот наш это доменА, 
  а тот, с которого мы пиздим это доменБ, и вот этот Корс даёт нам как раз возможность спиздить 
  что-то откуда-то. потому что изначально так делать нельзя, а благодаря этому корсу можно
*/
