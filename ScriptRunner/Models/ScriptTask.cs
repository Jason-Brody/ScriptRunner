using ScriptRunner.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Models
{
    public class ScriptTask
    {
        public string Id { get; set; }

        public ScriptTaskInfo TaskData{ get; set; }
    }
}
