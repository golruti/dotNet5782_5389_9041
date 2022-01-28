using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    internal partial class DalXML
    {
        /// <summary>
        /// AddDrone is a method in the DalXml class.
        /// the method adds a new drone
        /// </summary>
        public void AddDrone(Drone drone)
        {
            drone.IsDeleted = false;
            List<Drone> dronesXml;
            try
            {
                dronesXml = XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath);
            }
            catch (XMLFileLoadCreateException e) { throw e; }
            foreach (var item in dronesXml)
            {
                if (item.Id == drone.Id && item.IsDeleted == false)
                {
                    throw new Exception();
                }
            }
            dronesXml.Add(drone);
            try
            {
                XMLTools.SaveListToXmlSerializer<Drone>(dronesXml, dronesPath);
            }
            catch (XMLFileLoadCreateException e) { throw e; }
        }
        public void UpdateCharge(int droneId)
        {
            throw new NotImplementedException();
        }
        public Drone GetDrone(int requestedId)
        {
            return XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath).Find(drone => drone.Id == requestedId);
            //return DalFactory.GetDal("List").GetDrone(requestedId);
        }

        public IEnumerable<Drone> GetDrones()
        {
            return XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath);
        }
        public void DeleteDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRelease(int droneId)
        {
            throw new NotImplementedException();
        }

    }
}
