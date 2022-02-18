using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace BO
{
    /// <summary>
    /// There is no nearby base station that the drone can reach.
    /// </summary>
    #region ThereIsNoNearbyBaseStationThatTheDroneCanReachException
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
    #endregion


    /// <summary>
    /// When adding a new object - there is an object in the data with a similar ID.
    /// </summary>
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
            return Message + "An element with the same key already exists in the list";
        }
    }
    #endregion


    /// <summary>
    /// The parcel is linked to the drone and can not be deleted.
    /// </summary>
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


    /// <summary>
    /// No parcel suitable for the conditions of the drone has been found.
    /// </summary>
    #region NoParcelFoundForConnectionToTheDroneException
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
    #endregion

    /// <summary>
    /// No station available for charging
    /// </summary>
    #region NoStationAvailableForCharging
    [Serializable]
    public class NoStationAvailableForCharging : Exception
    {
        public NoStationAvailableForCharging() : base() { }
        public NoStationAvailableForCharging(string message) : base(message) { }
        public NoStationAvailableForCharging(string message, Exception inner) : base(message, inner) { }
        protected NoStationAvailableForCharging(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + "There is no parcel that needs to be sent - associate it with the drone";
        }
    }
    #endregion

    /// <summary>
    /// For the factory operation to the BL layer.
    /// </summary>
    #region BLConfigException
    public class BLConfigException : Exception
    {
        public BLConfigException(string msg) : base(msg) { }
        public BLConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
    #endregion
}
