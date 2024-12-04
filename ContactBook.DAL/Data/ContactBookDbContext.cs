using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContactBook.Core.Entity;
using System.Reflection.Metadata;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

namespace ContactBook.DAL.Data
{
    public class ContactBookDbContext : DbContext
    {
        public DbSet<Contact> contacts { get; set; } //то что видим в бд(создание таблицы)

        public ContactBookDbContext(DbContextOptions<ContactBookDbContext> options)//обьявление бд
            : base(options)
        {
        }

        public ContactBookDbContext() 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//создает модель (поля контакта)-проверка есть ли они
        {
            modelBuilder.Entity<Contact>().HasKey(c => c.Id);
            modelBuilder.Entity<PhoneNumber>().HasKey(p => p.Id);
            modelBuilder.Entity<Email>().HasKey(p => p.Id);
            modelBuilder.Entity<Contact>().HasMany(c => c.PhoneNumberList).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Contact>().HasMany(c => c.EmailList).WithOne().OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)//создает модель (поля контакта)-проверка есть ли они
        {
            modelBuilder.Entity<Contact>().HasKey(r => r.Id);
            modelBuilder.Entity<PhoneNumber>().HasKey(p => p.Id);
        }*/
    }
}
