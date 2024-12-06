using ContactBook.Core.Services;
using ContactBook.DAL;
using Microsoft.EntityFrameworkCore; // Для работы с Entity Framework Core
using ContactBook.DAL.Data; // Пространство имен для вашего контекста базы данных
using ContactBook.DAL.Repositories; // Пространство имен для ваших репозиториев
using Microsoft.Extensions.DependencyInjection; // Для работы с зависимостями

var builder = WebApplication.CreateBuilder(args);

// Добавление контекста базы данных
builder.Services.AddDbContext<ContactBookDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); // 

// Добавление репозитория
builder.Services.AddScoped<IRepositoty, FileRepository>(); // Регистрация репозитория

builder.Services.AddScoped<IContactService, ContactService>();//зарегестрировали сервис


// Добавление служб для контроллеров
builder.Services.AddControllers();

// (Опционально) Добавление поддержки OpenAPI (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка промежуточного ПО
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Включение Swagger
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactBook API V1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();