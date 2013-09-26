using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Ioc
{
    public class Mapping
    {
        public Type Interface { get; set; }
        public Type Class { get; set; }
        public object Instance { get; set; }
        public string Name { get; set; }
    }
}
