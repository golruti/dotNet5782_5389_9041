using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using static BO.Enums;

namespace BL
{
    partial class BL
    {

        public void AddUser(User tempUser)
        {
            try
            {
                dal.AddUser(new DO.User()
                {
                    Password = tempUser.Password,
                    UserId = tempUser.UserId,
                    Acces = (DO.Enum.Access)tempUser.Acces,
                    IsDeleted = false
                });
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }

        public bool IsExistClient(int userId, string password)
        {
            return dal.ExistUser(userId, password, (DO.Enum.Access)Access.Client);
        }

        public bool IsExistEmployee(int userId, string password)
        {
            return dal.ExistUser(userId, password, (DO.Enum.Access)Access.Employee);
        }


        public void DeleteUser(User tempUser)
        {
            dal.DeleteUser(new DO.User()
            {
                Password = tempUser.Password,
                UserId = tempUser.UserId,
                Acces = (DO.Enum.Access)tempUser.Acces,
                IsDeleted = false
            });
        }

        //private User MapUser(DO.User user)
        //{
        //    return new User()
        //    {
        //        Password = user.Password,
        //        UserId = user.UserId,
        //        Acces = (Access)user.Acces,
        //        IsDeleted = false
        //    };
        //}
    }
}
