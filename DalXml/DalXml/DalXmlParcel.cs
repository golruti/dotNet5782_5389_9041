using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DO.Enum;
using Enum = System.Enum;

namespace DAL
{
    internal partial class DalXML
    {
        /// <summary>
        /// CreateParcel is a method in the DalXml class.
        /// the method adds a new parcel
        /// </summary>
        /// <param name="parcel">the first Parcel value</param>
        public void AddParcel(Parcel parcel)
        {
            parcel.IsDeleted = false;
            List<Parcel> parcelsXml;
            try
            {
                parcelsXml = XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath);
            }
            catch (XMLFileLoadCreateException e) { throw e; }
            foreach (var item in parcelsXml)
            {
                if (item.Id == parcel.Id && item.IsDeleted == false)
                {
                    throw new Exception();
                }
            }
            parcel.Id = 100000000 + parcelsXml.Count;
            parcelsXml.Add(parcel);
            try
            {
                XMLTools.SaveListToXmlSerializer<Parcel>(parcelsXml, parcelsPath);
            }
            catch (XMLFileLoadCreateException e) { throw e; }
        }

        public Parcel GetParcel(Predicate<Parcel> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateParcelPickedUp(Parcel Parcel)
        {
            XElement parcels = XMLTools.LoadListFromXmlElement(parcelsPath);
            XElement parcel = (from p in parcels.Elements()
                               where Convert.ToInt32(p.Element("Id").Value) == Parcel.Id
                               select p).FirstOrDefault();
            parcel.Element("PickedUp").Value = DateTime.Now.ToString("O"); // datetime iso-8601 format

            XMLTools.SaveListToXmlElement(parcels, parcelsPath);
        }

        public void UpdateParcelDelivered(Parcel Parcel)
        {
            throw new NotImplementedException();
        }

        public void UpdateSupply(Parcel parcel)
        {

            XElement parcels = XMLTools.LoadListFromXmlElement(parcelsPath);
            XElement tempParcel = (from p in parcels.Elements()
                               where Convert.ToInt32(p.Element("Id").Value) == parcel.Id
                               select p).FirstOrDefault();
            tempParcel.Element("DroneId").Value = parcel.Droneld.ToString();

            XMLTools.SaveListToXmlElement(parcels, parcelsPath);
        }

        public void DeleteParcel(int id)
        {
            throw new NotImplementedException();
        }

        public void ParcelSchedule(int parcelId, int droneId)
        {
            XElement parcels = XMLTools.LoadListFromXmlElement(parcelsPath);
            XElement parcel = (from p in parcels.Elements()
                               where Convert.ToInt32(p.Element("Id").Value) == parcelId
                               select p).FirstOrDefault();
            parcel.Element("DroneId").Value = droneId.ToString();

            XMLTools.SaveListToXmlElement(parcels, parcelsPath);
        }

        public Parcel GetParcel(int requestedId)
        {
            return XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath).Find(parcel => parcel.Id == requestedId);
            //return GetNewParcel();
        }

        public Parcel GetNewParcel()
        {
            Parcel parcel = new();
            XElement xmlParcel = XElement.Load("http://ygoldsht.jct.ac.il/dronesproject/getnewparcel.php");

            parcel.Id = int.Parse(xmlParcel.Element("Id").Value);
            parcel.SenderId = int.Parse(xmlParcel.Element("SenderId").Value);
            parcel.TargetId = int.Parse(xmlParcel.Element("TargetId").Value);
            parcel.Priority = (Priorities)Enum.Parse(typeof(Priorities), xmlParcel.Element("Priority").Value);

            double weight = double.Parse(xmlParcel.Element("Weight").Value);
            if (weight < 5)
                parcel.Weight = WeightCategories.Light;
            else if (weight < 10)
                parcel.Weight = WeightCategories.Medium;
            else
            {
                parcel.Weight = WeightCategories.Heavy;
            }

            parcel.Requested = DateTime.Now;
            return parcel;
        }

        public IEnumerable<Parcel?> GetParcels(Func<Parcel?, bool> predicate = null) =>
           predicate == null ?
               XMLTools.LoadListFromXmlSerializer<Parcel?>(parcelsPath) :
               XMLTools.LoadListFromXmlSerializer<Parcel?>(parcelsPath).Where(predicate);


    }
}
