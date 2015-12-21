using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Motorola.CommonCPS.Server.EntityModel.GenericModel
{
    public class NameAndValue
    {
        NameAndValue() { }
        public NameAndValue(string Name, object value)
            : this(Name, value, false)
        {
        }

        public NameAndValue(string Name, object value, bool valueShallLocalize)
        {
            this.Name = Name;
            this.Value = value;
            this.ValueShallLocalize = valueShallLocalize;
        }

        [XmlAttribute]
        public string Name { get; set; }



        public object Value { get; set; }

        [XmlAttribute]
        public bool ValueShallLocalize { get; set; }
    }
}
