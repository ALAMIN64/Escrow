using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IRepository<UserType> UserTypeRepository;
        public UserTypeService(IRepository<UserType> UserTypeRepository)
        {
            this.UserTypeRepository = UserTypeRepository;
        }

        public void DeleteUserType(int id)
        {
            UserType UserType = UserTypeRepository.Get(id);
            UserTypeRepository.Remove(UserType);

        }

        public UserType GetUserType(int id)
        {
            return UserTypeRepository.Get(id);
        }

        public IEnumerable<UserType> GetUserTypes()
        {
            return UserTypeRepository.GetAll();
        }

        public void InsertUserType(UserType UserType)
        {
            UserTypeRepository.Insert(UserType);
        }

        public void UpdateUserType(UserType UserType)
        {
            UserTypeRepository.Update(UserType);
        }
    }
}
