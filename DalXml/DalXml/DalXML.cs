using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;


namespace DAL
{
    internal partial class DalXML : IDal
    {
        private readonly string baseStationsPath = "BaseStations.xml";
        private readonly string dronesPath = "Drones.xml";
        private readonly string parcelsPath = "Parcels.xml";
        private readonly string customersPath = "Customers.xml";
        private readonly string droneChargesPath = "DroneCharges.xml";
        public static IDal Instance { get; } = new DalXML();

        private DalXML()
        {
            //XMLTools.SaveListToXmlSerializer(DalFactory.GetDal("List").GetDrones(),dronesPath); 
            //XMLTools.SaveListToXmlSerializer(DalFactory.GetDal("List").GetBaseStations(), baseStationsPath);
            //XMLTools.SaveListToXmlSerializer(DalFactory.GetDal("List").GetParcels(), parcelsPath);
            //XMLTools.SaveListToXmlSerializer(DalFactory.GetDal("List").GetCustomers(), customersPath);
        }     

        //public double[] BatteryUsages()
        //{
        //    //return DalFactory.GetDal().BatteryUsages();
        //}
        public double[] GetElectricityUse()
        {
            throw new NotImplementedException();
        }      
        
    }
}
