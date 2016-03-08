using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRClientTest
{
    class JSONObjTest
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<Item> Items { get; set; }

        public DataTable Data { get; set; }
    }

    
    class JSONObjTest1
    {
        public string Id { get; set; }

        public string Name { get; set; }

        //public List<Item1> Items { get; set; }

        public DataTable Data { get; set; }

        public bool IsSelected { get; set; }
    }

    class Item
    {
        public string Name2 { get; set; }

        public int Id2 { get; set; }
    }

    class Item1
    {
        public string Name2 { get; set; }

        public int Id2 { get; set; }
    }
}
