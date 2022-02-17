using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DO.Enum;

namespace DAL
{
    internal partial class DalXml
    {
        //--------------------------------------------Adding----------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddUser(User user)
        {
            if (!GetUser(user.UserId, user.Password, user.Access).Equals(default(User)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("An existing user in the database - DAL");
            if (GetCustomer(user.UserId).Equals(default(Customer)))
                throw new KeyNotFoundException("No matching customer found for UserId");
            user.IsDeleted = false;

            AddItem(usersPath, user);
        }


        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public User GetUser(int userId, string password, Access access)
        {
            return GetItem<User>(usersPath, userId, password, access);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool ExistUser(int userName, string password, Access access)
        {
            return GetUsers().FirstOrDefault(user => user.Access == access && user.UserId == userName && user.Password == password).Equals(default(User)) ? false : true;
        }

        //--------------------------------------------Show list--------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<User> GetUsers()
        {
            return GetList<User>(usersPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<User> GetUsers(Predicate<User> predicate)
        {
            return GetUsers().Where(item => predicate(item));
        }


        //--------------------------------------------Delete---------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteUser(User user)
        {
            if (GetUser(user.UserId, user.Password, user.Access).Equals(default(User)))
                throw new KeyNotFoundException("Delete user -DAL: There is no suitable user in data");
            UpdateItem(customersPath, user.UserId, user.Password, user.Access, nameof(User.IsDeleted), true);
        }
    }
}
