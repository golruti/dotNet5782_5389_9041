using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;


namespace DAL
{
    internal partial class DalXML : Singleton.Singleton<DalXML>, IDal
    {
        private readonly string realtivePath = "../../XmlFiles";
        private string baseStationsPath => $"{realtivePath}/BaseStations.xml";
        private string dronesPath => $"{realtivePath}/Drones.xml";
        private string parcelsPath => $"{realtivePath}/Parcels.xml";
        private string customersPath => $"{realtivePath}/Customers.xml";
        private string droneChargesPath => $"{realtivePath}/DroneCharges.xml";
        private string ConfigPath => $"{realtivePath}/DroneCharges.xml";

        public double[] GetElectricityUse()
        {
            XElement electricity = XDocument.Load(ConfigPath).Root;

            return new double[]
            {
                double.Parse(electricity.Element("Free").Value),
                double.Parse(electricity.Element("CarriesLightWeight").Value),
                double.Parse(electricity.Element("CarriesMediumWeight").Value),
                double.Parse(electricity.Element("CarriesHeavyWeight").Value),
                double.Parse(electricity.Element("ChargingRate").Value),
            };
        }      
        
        private void AddItem(string path, object item)
        {
            XDocument document = XDocument.Load(path);
            document.Root.Add(item.Serialize());

            document.Save(path);
        }

        private T GetItem<T>(string path, int id) 
        {
            XDocument document = XDocument.Load(path);
            XElement item = document.Root
                                    .Elements()
                                    .FirstOrDefault(element => !bool.Parse(element.Element("IsDeleted").Value) 
                                                               && id == int.Parse(element.Element("Id").Value));
            return item == null? default: item.Deserialize<T>();
        }

        private IEnumerable<T> GetList<T>(string path)
        {
            XDocument document = XDocument.Load(path);
            return document.Root
                           .Elements()
                           .Where(element => !bool.Parse(element.Element("IsDeleted").Value))
                           .Select(element => element.Deserialize<T>());
        }

        private void UpdateItem(string path, int id, string propertyName, object propertyValue)
        {
            XDocument document = XDocument.Load(path);
            XElement root = document.Root;
            XElement item = document.Root
                                    .Elements()
                                    .FirstOrDefault(element => !bool.Parse(element.Element("IsDeleted").Value)
                                                               && id == int.Parse(element.Element("Id").Value));

            if (item == default(XElement))
            {
                throw new KeyNotFoundException($"Update item - Dal (object in path: {path}");
            }

            item.Element(propertyName).RemoveAttributes();
            item.SetElementValue(propertyName, propertyValue);

            document.Save(path);
        }
    }
}
