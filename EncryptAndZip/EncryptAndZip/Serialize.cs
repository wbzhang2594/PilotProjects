using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO.Compression;

namespace Motorola.CommonCPS.CommonContract
{
    public static class Serializer
    {
        private readonly static Encoding _Encoding = new UTF8Encoding();
        private readonly static IFormatter _Serializer;

        static Serializer()
        {
            var serializer = new NetDataContractSerializer();
            serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
            _Serializer = serializer;
        }



        public static string Serialize_ToXMLString(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                XmlSerializer xs = new XmlSerializer(obj.GetType());
                xs.Serialize(memoryStream, obj);

                return _Encoding.GetString(memoryStream.ToArray());
            }
        }

        public static object Deserialize_FromXMLString(string s, Type type)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            using (var memoryStream = new MemoryStream(_Encoding.GetBytes(s)))
            {
                XmlSerializer xs = new XmlSerializer(type);

                return xs.Deserialize(memoryStream);
            }
        }

        public static void SerializeToXmlFile(string xmlFilePath, Type type, object obj)
        {
            XmlSerializer xs = new XmlSerializer(type);
            if (!Directory.Exists(Path.GetDirectoryName(xmlFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(xmlFilePath));
            }

            using (Stream stream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                stream.SetLength(0);
                xs.Serialize(stream, obj);
                stream.Flush();
            }
        }

        public static object DeserializeFromXmlFile(string xmlFilePath, Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            using (Stream stream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
            {
                return xs.Deserialize(stream);
            }
        }

        public static string Serialize_DataContract_ToXML(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                _Serializer.Serialize(memoryStream, obj);

                return _Encoding.GetString(memoryStream.ToArray());
            }
        }
        public static void Serialize_DataContract_ToXMLFile(object obj, string xmlFilePath)
        {
            using (Stream stream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                stream.SetLength(0);
                _Serializer.Serialize(stream, obj);
                stream.Flush();
            }
        }

        public static object Deserialize_DataContract_FromXML(string s, Type type)
        {
            using (var memoryStream = new MemoryStream(_Encoding.GetBytes(s)))
            {
                return _Serializer.Deserialize(memoryStream);
            }
        }

        public static object Deserialize_DataContract_FromXMLFile(string xmlFilePath, Type type)
        {
            using (Stream stream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
            {
                return _Serializer.Deserialize(stream);
            }
        }

        public static string Serialize_ToXMLZipString(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var cmpStream = new DeflateStream(memoryStream, CompressionLevel.Fastest))
                {
                    XmlSerializer xs = new XmlSerializer(obj.GetType());
                    xs.Serialize(cmpStream, obj);
                }

                return System.Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static object Deserialize_FromXMLZipString(string s, Type type)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            var bytes = System.Convert.FromBase64String(s);
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var cmpStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
                {
                    XmlSerializer xs = new XmlSerializer(type);
                    return xs.Deserialize(cmpStream);
                }
            }
        }
    }
}
