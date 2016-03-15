using ScriptRunner.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Models
{
    class Robot
    {
        public string Guid { get; set; }

        public int ProcessId { get; set; }

        public string ConnectionId { get; set; }

        public ScriptTaskInfo TaskData { get; set; }

        public int DataRow { get; set; }
    }
}
