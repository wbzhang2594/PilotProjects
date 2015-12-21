using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Motorola.CommonCPS.CommonContract;

namespace Motorola.CommonCPS.Server.EntityModel.GenericModel
{
    public class MultipleFieldsXML : IEquatable<MultipleFieldsXML>
    {
        public Dictionary<string, object> ConvertToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (NameAndValue item in NameAndValueList)
            {
                result.Add(item.Name, item.Value);
            }

            return result;
        }
        public MultipleFieldsXML()
        {
            NameAndValueList = new List<NameAndValue>();
        }

        [XmlElementAttribute("Field")]
        public List<NameAndValue> NameAndValueList { get; set; }

        public object GetValue(string Name)
        {
            IEnumerable<NameAndValue> findResult = NameAndValueList.Where(
                item => item.Name == Name
                );

            Debug.Assert(findResult.Count() <= 1);

            if (findResult.Count() == 1)
            {
                return findResult.First().Value;
            }
            else
            {
                return null;
            }
        }



        public bool TrySetValue(string FieldName, object value)
        {
            bool Result = false;

            IEnumerable<NameAndValue> findResult = NameAndValueList.Where(
                item => item.Name == FieldName
                );

            if (findResult.Count() == 1)
            {
                try
                {
                    findResult.First().Value = value;
                    Result = true;
                }
                catch
                {
                    //do nothing, result = false
                }
            }


            return Result;
        }

        public string SerializeToXML()
        {
            string originalOutput = Serializer.Serialize_ToXMLString(this);
            return originalOutput;

            //string zippedOutput = Serializer.Serialize_ToXMLZipString(this);
            //return zippedOutput;
        }

        public static MultipleFieldsXML DeserializeFromXML(string xmlString)
        {
            MultipleFieldsXML result;

            if (xmlString == null)
            {
                return null;
            }

            if (xmlString.Substring(0, 5).Equals("<?xml"))
            {
                result = Serializer.Deserialize_FromXMLString(xmlString, typeof(MultipleFieldsXML)) as MultipleFieldsXML;
            }
            else
            {
                result = Serializer.Deserialize_FromXMLZipString(xmlString, typeof(MultipleFieldsXML)) as MultipleFieldsXML;
            }


            return result;
        }

        public bool Equals(MultipleFieldsXML other)
        {
            return Equals(other, null);
        }

        public bool Equals(MultipleFieldsXML other, List<string> IgnoreFields)
        {
            if (NameAndValueList.Count() != other.NameAndValueList.Count())
            {
#if DEBUG
                MultipleFieldsXML theMore, theLess;
                if (NameAndValueList.Count() < other.NameAndValueList.Count())
                {
                    theMore = other;
                    theLess = this;
                }
                else
                {
                    theMore = this; theLess = other;
                }

                foreach (var itemself in theMore.NameAndValueList)
                {
                    if (null == theLess.GetValue(itemself.Name))
                    {
                        Debug.Assert(false);
                    }
                }
#endif
                return false;
            }
            foreach (NameAndValue item in NameAndValueList)
            {
                if (IgnoreFields != null && IgnoreFields.Contains(item.Name))
                {
                    continue;
                }

                if (false == other.GetValue(item.Name).Equals(item.Value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
