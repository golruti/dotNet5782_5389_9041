﻿using System;
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
                    Access = (DO.Enum.Access)tempUser.Access,
                    IsDeleted = false
                });
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// return a user from an array of users by userName and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetUser(int userName, string password, Access access)
        {
            DO.User tempUser = dal.GetUser(userName, password, (DO.Enum.Access)access);
            User user = new User()
            {
                UserId = tempUser.UserId,
                Password = tempUser.Password,
                Access = (Enums.Access)tempUser.Access,
                IsDeleted = tempUser.IsDeleted
            };

            return user;
        }



        public bool IsExistClient(int userId, string password)
        {
            return dal.ExistUser(userId, password, (DO.Enum.Access)Access.Client) && !dal.GetCustomer(userId).Equals(default(DO.Customer));
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
                Access = (DO.Enum.Access)tempUser.Access,
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
