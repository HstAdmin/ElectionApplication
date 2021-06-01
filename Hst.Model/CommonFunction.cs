using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Hst.Model
{
   public static class ExtensionMethod
    {
        public static string DataContractSerializeToString<T>(T t) where T: class
        {
            //var contractSerializer = new DataContractSerializer(typeof(T));
            //var stringBuilder = new StringBuilder();

            //using (var xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings {
            //    NewLineOnAttributes=true,
            //    Indent=true
            //}))
            //{
            //    contractSerializer.WriteObject(xmlWriter, t);
            //}
            //return stringBuilder.ToString();

            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(t.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, t);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public static T DataContractDeSerialize<T>( string xml) where T : class
        {
            if (string.IsNullOrEmpty(xml))
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
            else {
                var contarctSerializer = new DataContractSerializer(typeof(T));
                using (var xmlReader = XmlReader.Create(new StringReader(xml)))
                {
                    return (T)contarctSerializer.ReadObject(xmlReader);
                }
            }
        }

        //public static Object XMLToObject(this Object oObject, string XMLString)
        //{

        //    XmlSerializer oXmlSerializer = new XmlSerializer(oObject.GetType());
        //    oObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
        //    return oObject;
        //}

        //XmlSerializer serialiser = new XmlSerializer(typeof(List<Company>));

        //TextWriter Filestream = new StreamWriter(@"E:\Company.xml");

        //serialiser.Serialize(Filestream, objList);

        //    Filestream.Close();-

    }
}
