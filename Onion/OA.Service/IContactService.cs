using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
   public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(int id);
        void InsertContact(Contact Contact);
        void UpdateContact(Contact Contact);
        void DeleteContact(int id);
    }
}
