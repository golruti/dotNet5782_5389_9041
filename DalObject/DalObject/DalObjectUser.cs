using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DO;
using static DO.Enum;

namespace DAL
{
    public partial class DalObject
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddUser(User user)
        {
            if (!GetUser(user.UserId, user.Password, user.Access).Equals(default(User)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("An existing user in the database - DAL");
            if (GetCustomer(user.UserId).Equals(default(Customer)))
                throw new KeyNotFoundException("No matching customer found for UserId");
            user.IsDeleted = false;

            DataSource.users.Add(user);
        }


        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public User GetUser(int userName, string password, Access access)
        {
            User user = DataSource.users.FirstOrDefault(user => user.UserId == userName && user.Password == password &&
            user.Access == access && !(user.IsDeleted));

            return user;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool ExistUser(int userName, string password, Access access)
        {
            return GetUsers().FirstOrDefault(user => user.Access == access).Equals(default(User)) ? false : true;
        }


        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<User> GetUsers()
        {
            return DataSource.users;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<User> GetUsers(Predicate<User> predicate)
        {
            return GetUsers().Where(item => predicate(item));
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteUser(User user)
        {
            User deletedUser = GetUser(user.UserId, user.Password, user.Access);
            if (deletedUser.Equals(default(User)))
                throw new KeyNotFoundException("Delete user -DAL-: There is no suitable user in data");
            else
            {
                DataSource.users.Remove(deletedUser);
                deletedUser.IsDeleted = true;
                DataSource.users.Add(deletedUser);
            }
        }
    }
}
