using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;



namespace DO
{
    #region ThereIsAnObjectWithTheSameKeyInTheListException
    [Serializable]
    public class ThereIsAnObjectWithTheSameKeyInTheListException : Exception
    {
        public ThereIsAnObjectWithTheSameKeyInTheListException() : base() { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message) : base(message) { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message, Exception inner) : base(message, inner) { }
        protected ThereIsAnObjectWithTheSameKeyInTheListException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return "An element with the same key already exists in the list";
        }
    }
    #endregion

    #region DalConfigException
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
    #endregion


    //החבילה מקושרת לרחפן ולא יכולה להימחק
    #region TheParcelIsAssociatedAndCannotBeDeleted
    [Serializable]

    public class TheParcelIsAssociatedAndCannotBeDeleted : Exception
    {
        public TheParcelIsAssociatedAndCannotBeDeleted() : base() { }
        public TheParcelIsAssociatedAndCannotBeDeleted(string message) : base(message) { }
        public TheParcelIsAssociatedAndCannotBeDeleted(string message, Exception inner) : base(message, inner) { }
        protected TheParcelIsAssociatedAndCannotBeDeleted(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + "The parcel is associated with a drone and cannot be deleted";
        }
    }
    #endregion
}

