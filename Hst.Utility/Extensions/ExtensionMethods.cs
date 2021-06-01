using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Hst.Utility.Extensions
{
    public static class ExtensionMethods
    {
        public static string ToSerialziedXML<T>(this T obj)
        {
            string serialzedXML = string.Empty;
            StringWriter stringWriter = new StringWriter();
            //XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("root"));
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces xmlnamespace = new XmlSerializerNamespaces();
            xmlnamespace.Add(string.Empty, string.Empty);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.Encoding = System.Text.Encoding.Default;
            XmlWriter writer = XmlWriter.Create(stringWriter, settings);
            serializer.Serialize(writer, obj, xmlnamespace);
            serialzedXML = stringWriter.ToString();
            settings = null;
            writer.Flush();
            writer.Dispose();
            return serialzedXML;
        }

        public static DateTime ToDateTime(this string dateTime)
        {
            DateTime dt;
            if (DateTime.TryParseExact(dateTime, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                //valid date
            }
            else
            {
                //invalid date
            }
            return dt;
        }

        public static DateTime ToDateTimeFromUrl(this string dateTime)
        {
            DateTime dt;
            if (DateTime.TryParseExact(dateTime, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                //valid date
            }
            else
            {
                //invalid date
            }
            return dt;
        }

        public static string ToDDMMYYYY(this DateTime dateTime)
        {
            string s = dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return s;
        }

        public static string ToDDMMYYYYForUrl(this DateTime dateTime)
        {
            string s = dateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            return s;
        }

        public static bool IsDDMMYYYY(this string dateTime)
        {
            DateTime dt;
            if (DateTime.TryParseExact(dateTime, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
