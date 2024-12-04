﻿using ContactBook.Core.Entity;

namespace ContactBook.DAL;

public interface IRepositoty
{
    Task<IEnumerable<Contact>> ReadAll();
    Task<IEnumerable<Contact>> FilterContacts(Func<Contact, bool> predicate);
    Task<bool> Create(string FirstName, string LastName, List<string> EmailList, List<string> PhoneNumberList);
    Task Clear();
    int Count();


}