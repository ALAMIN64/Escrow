using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public interface IUserTypeService
    {
        IEnumerable<UserType> GetUserTypes();
        UserType GetUserType(int id);
        void InsertUserType(UserType UserType);
        void UpdateUserType(UserType UserType);
        void DeleteUserType(int id);
    }
}
