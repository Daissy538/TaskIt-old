using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItApi.Attributes
{
    /// <summary>
    /// Attribute that is used to name enums
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        public string Value { get; } 

        public StringValueAttribute(string value)
        {
            this.Value = value;
        }        
    }
}
