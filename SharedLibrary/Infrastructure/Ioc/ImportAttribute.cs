using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Ioc
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ImportAttribute : Attribute
    {
        public ImportAttribute(string name = "")
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
