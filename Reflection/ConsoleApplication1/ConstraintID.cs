using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class ConstraintIDAttribute : Attribute
    {
        public ConstraintIDAttribute(string ID)
        {
            this.ID = ID;
        }

        public string ID { get; set; }
    }
}
