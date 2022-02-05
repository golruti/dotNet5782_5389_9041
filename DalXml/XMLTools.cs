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
    }
}
