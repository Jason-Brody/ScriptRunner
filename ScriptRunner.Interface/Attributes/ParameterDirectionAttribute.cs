using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public class ParameterAttribute:Attribute
    {
        public Direction Direction { get; set; }
    }

    public enum Direction
    {
        Input = 0,
        Output=1
    }


}
