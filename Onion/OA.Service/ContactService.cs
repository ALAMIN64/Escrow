using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> ContactRepository;
        public ContactService(IRepository<Contact> ContactRepository)
        {
            this.ContactRepository = ContactRepository;
        }

        public void DeleteContact(int id)
        {
            Contact Contact = ContactRepository.Get(id);
            ContactRepository.Remove(Contact);

        }

        public Contact GetContact(int id)
        {
            return ContactRepository.Get(id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return ContactRepository.GetAll();
        }

        public void InsertContact(Contact Contact)
        {
            ContactRepository.Insert(Contact);
        }

        public void UpdateContact(Contact Contact)
        {
            ContactRepository.Update(Contact);
        }
    }
}
