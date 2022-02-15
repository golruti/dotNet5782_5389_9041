using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DO.Enum;

namespace DAL
{
    internal partial class DalXml
    {
        /// <summary>
        /// Add a user to the array of existing users
        /// </summary>
        /// <param name="user">struct of customer</param>
        public void AddUser(User user)
        {
            if (!GetUser(user.UserId, user.Password, user.Access).Equals(default(User)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("An existing user in the database - DAL");
            user.IsDeleted = false;

            AddItem(usersPath, user);
        }

        public bool ExistUser(int userName, string password, Access access)
        {
            return GetUsers().FirstOrDefault(user =>  user.Access == access).Equals(default(User)) ? false : true;
        }
        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// return a user from an array of users by userName and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private User GetUser(int userId, string password, Access access)
        {
            return GetItem<User>(usersPath, userId, password, access);
        }

        /// <summary>
        /// The function accepts a condition in the predicate and returns the user who satisfies the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>the user</returns>
        private User GetCUser(Predicate<User> predicate, Access access)
        {
            return GetUsers().FirstOrDefault(user => predicate(user) && user.Access == access);
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing users
        /// </summary>
        /// <returns>list of users</returns>
        public IEnumerable<User> GetUsers()
        {
            return GetList<User>(usersPath);
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of users that maintain the predicate</returns>
        private IEnumerable<User> GetUsers(Predicate<User> predicate)
        {
            return GetUsers().Where(item => predicate(item));
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific user
        /// </summary>
        /// <param name="id">user details</param>
        public void DeleteUser(User user)
        {
            if (GetUser(user.UserId, user.Password, user.Access).Equals(default(User)))
                throw new KeyNotFoundException("Delete user -DAL: There is no suitable user in data");
            UpdateItem(customersPath, user.UserId, user.Password, user.Access, nameof(User.IsDeleted), true);
        }
    }
}
