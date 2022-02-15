using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace BO
{
    //אין תחנת בסיס קרובה שהרחפן יכול להגיע אליה
    [Serializable]
    public class ThereIsNoNearbyBaseStationThatTheDroneCanReachException : Exception
    {
        public ThereIsNoNearbyBaseStationThatTheDroneCanReachException() : base() { }
        public ThereIsNoNearbyBaseStationThatTheDroneCanReachException(string message) : base(message) { }
        public ThereIsNoNearbyBaseStationThatTheDroneCanReachException(string message, Exception inner) : base(message, inner) { }
        protected ThereIsNoNearbyBaseStationThatTheDroneCanReachException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + "There is no nearby base station that the drone can reach exception";
        }
    }

    //קיים מישהו עם אותו מזהה
    [Serializable]
    public class ThereIsAnObjectWithTheSameKeyInTheListException : Exception
    {
        public ThereIsAnObjectWithTheSameKeyInTheListException() : base() { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message) : base(message) { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message, Exception inner) : base(message, inner) { }
        protected ThereIsAnObjectWithTheSameKeyInTheListException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + "An element with the same key already exists in the list";
        }
    }

    //החבילה מקושרת לרחפן ולא יכולה להימחק
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


    //אין חבילה לשלחיחה
    [Serializable]

    public class NoParcelFoundForConnectionToTheDroneException : Exception
    {
        public NoParcelFoundForConnectionToTheDroneException() : base() { }
        public NoParcelFoundForConnectionToTheDroneException(string message) : base(message) { }
        public NoParcelFoundForConnectionToTheDroneException(string message, Exception inner) : base(message, inner) { }
        protected NoParcelFoundForConnectionToTheDroneException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + "There is no parcel that needs to be sent - associate it with the drone";
        }
    }



    public class BLConfigException : Exception
    {
        public BLConfigException(string msg) : base(msg) { }
        public BLConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

}
