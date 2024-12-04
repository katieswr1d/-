using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContactBook.Core.Entity;

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
            modelBuilder.Entity<Email>().HasKey(e => e.Id);
            modelBuilder.Entity<Contact>().HasMany(c => c.Phones).WithOne(p => p.Contact).IsRequired().HasForeignKey(p => p.ContactId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Contact>().HasMany(c => c.Emails).WithOne(p => p.Contact).IsRequired().HasForeignKey(p => p.ContactId).OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);


        }
    }
}
