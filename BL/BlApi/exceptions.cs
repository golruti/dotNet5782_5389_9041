using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace BO
{
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

    public class BLConfigException : Exception
    {
        public BLConfigException(string msg) : base(msg) { }
        public BLConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

}
