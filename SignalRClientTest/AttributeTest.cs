using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRClientTest
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    class AttributeTest:Attribute
    {
        public string Name { get; set; }

        public EntityData Entity { get; set; }
    }

    public class EntityData
    {
        public string Name { get; set; }

        public string Id { get; set; }
    }
}
