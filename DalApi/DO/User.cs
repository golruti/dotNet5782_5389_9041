using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enum;

namespace DO
{
    public struct User
    {
        #region properties
        public string Password { get; set; }
        public int UserId { get; set; }
        public Access Acces { get; set; }
        public bool IsDeleted { get; set; }        
        #endregion
    }
}
