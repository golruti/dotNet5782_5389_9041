using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    static class XMLTools
    {

        /// <summary>
        /// Converts an object to XElement
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>An XElement representation of the object</returns>
        internal static XElement Serialize(this object item)
        {
            using var stream = new MemoryStream();
            using TextWriter streamWriter = new StreamWriter(stream);

            var xmlSerializer = new XmlSerializer(item.GetType());
            xmlSerializer.Serialize(streamWriter, item);

            return XElement.Parse(Encoding.ASCII.GetString(stream.ToArray()));
        }

        /// <summary>
        /// Converts <see cref="XElement"/> to regular object   
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="xElement">The <see cref="XElement"/></param>
        /// <returns>The converted object</returns>
        internal static T Deserialize<T>(this XElement xElement)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }


        private static readonly string dirPath = @"xml\";


        #region SaveLoadWithXElement
        public static void SaveListToXmlElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dirPath + filePath);
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        public static XElement LoadListFromXmlElement(string filePath)
        {
            try
            {
                if (File.Exists(dirPath + filePath))
                {
                    return XElement.Load(dirPath + filePath);
                }
                else
                {
                    XElement rootElem = new XElement(dirPath + filePath);
                    rootElem.Save(dirPath + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

        #region SaveLoadWithXMLSerializer
        public static void SaveListToXmlSerializer<T>(IEnumerable<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dirPath + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXmlSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dirPath + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dirPath + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion        
    }
}
