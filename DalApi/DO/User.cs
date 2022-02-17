using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enum;

namespace DO
{
    /// <summary>
    /// This is a type that represents a user of the system -
    /// a company employee or a customer with an account in the system.
    /// </summary>
    public struct User
    {
        #region properties
        public string Password { get; set; }
        public int UserId { get; set; }
        //User access permission
        public Access Access { get; set; }
        public bool IsDeleted { get; set; }        
        #endregion
    }
}
