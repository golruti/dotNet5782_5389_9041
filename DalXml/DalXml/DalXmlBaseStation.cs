using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    internal partial class DalXML
    {
        /// <summary>
        /// CreateBaseStation is a method in the DalXml class.
        /// the method adds a new base station
        /// </summary>
        public void AddBaseStation(BaseStation baseStation)
        {

            XElement rootElem;
            try
            {
                rootElem = XMLTools.LoadListFromXmlElement(baseStationsPath);

            }
            catch (XMLFileLoadCreateException e)
            {
                throw e;
            }
            var exist = from b in rootElem.Elements()
                        where Int32.Parse(b.Element("id").Value) == baseStation.Id && bool.Parse(b.Element("IsDeleted").Value) == false
                        select b.Value.FirstOrDefault();
            if (exist != null)
            {
                throw new Exception();
            }
            rootElem.Element("ArrayOfBaseStation").Add(xmlBaseStation(baseStation));
            try
            {
                XMLTools.SaveListToXmlElement(rootElem, baseStationsPath);
            }
            catch (XMLFileLoadCreateException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// RequestBaseStation is a method in the DalXml class.
        /// the method allows base station view
        /// </summary>
        public BaseStation RequestBaseStation(int id)
        {
            try
            {
                XElement baseStationsXml = XMLTools.LoadListFromXmlElement(baseStationsPath);
                XElement baseStation = RequestXmlBaseStation(baseStationsXml, id);
                return (new BaseStation()
                {
                    Id = Int32.Parse(baseStation.Element("id").Value),
                    Name = baseStation.Element("Name").Value,
                    ChargeSlote = Int32.Parse(baseStation.Element("ChargeSlots").Value),
                    Longitude = Int32.Parse(baseStation.Element("Longitude").Value),
                    Latitude = Int32.Parse(baseStation.Element("Latitude").Value),
                    IsDeleted = bool.Parse(baseStation.Element("IsDeleted").Value)
                });
            }
            //catch (UnextantException e) { throw e; }
            catch (XMLFileLoadCreateException e) { throw e; }
        }

        /// <summary>
        /// ViewListBaseStations is a method in the DalXML class.
        /// the method displays a List of base stations
        /// </summary>
        public IEnumerable<BaseStation> ListBaseStations()
        {
            XElement baseStationsXML;
            try
            {
                baseStationsXML = XMLTools.LoadListFromXmlElement(baseStationsPath);
            }
            catch (XMLFileLoadCreateException e)
            {
                throw e;
            }
            List<BaseStation> baseStations = new();
            baseStations = ((List<BaseStation>)(from b in baseStationsXML.Elements()
                                                where bool.Parse(b.Element("IsDeleted").Value) == false
                                                select new BaseStation()
                                                {
                                                    Id = int.Parse(b.Element("Id").Value),
                                                    ChargeSlote = int.Parse(b.Element("ChargeSlots").Value),
                                                    Name = b.Element("Name").Value,
                                                    Latitude = double.Parse(b.Element("Latitude").Value),
                                                    Longitude = double.Parse(b.Element("Longitude").Value),
                                                })).ToList();
            return baseStations;
        }

       
         public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate)
        {
            XElement baseStationsXML;
            try
            {
                baseStationsXML = XMLTools.LoadListFromXmlElement(baseStationsPath);
            }
            catch (XMLFileLoadCreateException e)
            {
                throw e;
            }
            List<BaseStation> baseStations = new();
            baseStations = ((List<BaseStation>)(from b in baseStationsXML.Elements()
                                                where bool.Parse(b.Element("IsDeleted").Value) == false /*&& bool.Parse(predicate(b))*/
                                                select new BaseStation()
                                                {
                                                    Id = int.Parse(b.Element("Id").Value),
                                                    ChargeSlote = int.Parse(b.Element("ChargeSlots").Value),
                                                    Name = b.Element("Name").Value,
                                                    Latitude = double.Parse(b.Element("Latitude").Value),
                                                    Longitude = double.Parse(b.Element("Longitude").Value),
                                                })).ToList();
            return baseStations;
        }

        /// <summary>
        /// the func updates name or/and sum of charge slots of a Base-Station
        /// </summary>
        /// <param name="b">the first BaseStation object</param>
        public void UpdateBaseStation(BaseStation b)
        {
            XElement baseStationsXml;
            try
            {
                baseStationsXml = XMLTools.LoadListFromXmlElement(baseStationsPath);
            }
            catch(XMLFileLoadCreateException e) { throw e; }
            XElement baseStation;
            try
            {
                baseStation = RequestXmlBaseStation(baseStationsXml, b.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
            if (b.Name != "")
            {
                baseStation.Element("Name").Value = b.Name;
            }
            if (b.ChargeSlote > -1)
            {
                baseStation.Element("ChargeSlots").Value = ""+b.ChargeSlote;
            }
            try
            {
                XMLTools.SaveListToXmlElement(baseStationsXml, baseStationsPath);
            }
            catch(XMLFileLoadCreateException e) { throw e; }
        }

        /// <summary>
        /// check every item according the predicate
        /// </summary>
        /// <param name="predicate">first item - predicate</param>
        /// <returns>ienumerable of basestation</returns>
        public IEnumerable<BaseStation> RequestPartListBaseStations(Predicate<BaseStation> predicate)
        {
            IEnumerable<BaseStation> baseStations;
            try
            {
                baseStations = ListBaseStations();
            }
            catch(XMLFileLoadCreateException e) { throw e; }
            return baseStations.Where(baseStation => predicate(baseStation));
        }

        public IEnumerable<BaseStation> GetAvaBaseStations()
        {
            XElement baseStationsXML;
            try
            {
                baseStationsXML = XMLTools.LoadListFromXmlElement(baseStationsPath);
            }
            catch (XMLFileLoadCreateException e)
            {
                throw e;
            }
            List<BaseStation> baseStations = new();
            baseStations = ((List<BaseStation>)(from b in baseStationsXML.Elements()
                                                where bool.Parse(b.Element("IsDeleted").Value) == false && int.Parse(b.Element("AvailableChargingPorts").Value)>0
                                                select new BaseStation()
                                                {
                                                    Id = int.Parse(b.Element("Id").Value),
                                                    ChargeSlote = int.Parse(b.Element("ChargeSlots").Value),
                                                    Name = b.Element("Name").Value,
                                                    Latitude = double.Parse(b.Element("Latitude").Value),
                                                    Longitude = double.Parse(b.Element("Longitude").Value),
                                                })).ToList();
            return baseStations;
        }

        public IEnumerable<BaseStation> GetBaseStations()
        {
            XElement baseStationsXML = XMLTools.LoadListFromXmlElement(baseStationsPath);

            List<BaseStation> baseStations = new();
            foreach (var baseStationElement in baseStationsXML.Elements())
            {
                baseStations.Add(
                    new BaseStation()
                    {
                        Id = int.Parse(baseStationElement.Element("Id").Value),
                        AvailableChargingPorts = int.Parse(baseStationElement.Element("ChargingPorts").Value),
                        Name = baseStationElement.Element("Name").Value,
                        Latitude = double.Parse(baseStationElement.Element("Latitude").Value),
                        Longitude = double.Parse(baseStationElement.Element("Longitude").Value),
                    });
            }

            return baseStations;
        }
        /// <summary>
        /// delete base station - by delete prop
        /// </summary>
        /// <param name="id">first int value</param>
        public void DeleteBaseStation(int id)
        {
            XElement baseStationsXml;
            try
            {
                baseStationsXml = XMLTools.LoadListFromXmlElement(baseStationsPath);
            }
            catch (XMLFileLoadCreateException e) { throw e; }
            XElement baseStation;
            try
            {
                baseStation = RequestXmlBaseStation(baseStationsXml, id);
            }
            catch (Exception e)
            {
                throw e;
            }
            baseStation.Element("IsDeleted").Value = "true";
            try
            {
                XMLTools.SaveListToXmlElement(baseStationsXml, baseStationsPath);
            }
            catch (XMLFileLoadCreateException e) { throw e; }
        }

        #region helpFunctions
        
        private XElement xmlBaseStation(BaseStation baseStation)
        {
            return new XElement("BaseStation",
                        new XElement("Id", baseStation.Id),
                        new XElement("ChargeSlots", baseStation.ChargeSlote),
                        new XElement("Longitude", baseStation.Longitude),
                        new XElement("Latitude", baseStation.Latitude),
                        new XElement("IsDeleted", false)
                        );
        }
        public BaseStation GetBaseStation(int requestedId)
        {

            XElement baseStationsXML = XMLTools.LoadListFromXmlElement(baseStationsPath);


            BaseStation baseStation;
            baseStation = (from p in baseStationsXML.Elements()
                           where Convert.ToInt32(p.Element("Id").Value) == requestedId
                           select new BaseStation()
                           {
                               Id = Convert.ToInt32(p.Element("Id").Value),
                               AvailableChargingPorts = int.Parse(p.Element("ChargingPorts").Value),
                               Name = p.Element("Name").Value,
                               Latitude = double.Parse(p.Element("Latitude").Value),
                               Longitude = double.Parse(p.Element("Longitude").Value),
                           }).FirstOrDefault();
            return baseStation;
            //return DalFactory.GetDal("List").GetBaseStation(requestedId);
        }
        public XElement RequestXmlBaseStation(XElement root, int id)
        {
            try
            {
                return ((XElement)(from b in root.Elements()
                                   where Int32.Parse(b.Element("Id").Value) == id && bool.Parse(b.Element("IsDeleted").Value) == false
                                   select b).First());
            }
            catch (ArgumentNullException) { throw new Exception("base station"); }
        }

        #endregion
    }

    

    [Serializable]
    internal class XMLFileLoadCreateException : Exception
    {
        public XMLFileLoadCreateException()
        {
        }

        public XMLFileLoadCreateException(string message) : base(message)
        {
        }

        public XMLFileLoadCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}