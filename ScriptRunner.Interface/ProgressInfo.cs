using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public class ProgressInfo
    {
        public ProgressInfo(int Current,int Total,string Msg)
        {
            this.Current = Current;
            this.Total = Total;
            this.Msg = Msg;
            this.IsProgressKnow = true;
        }

        public ProgressInfo(string Message)
        {
            this.Msg = Message;
            this.IsProgressKnow = false;
        }

        public ProgressInfo() { }

        public bool IsProgressKnow { get; set; } 

        public int Current { get; set; }

        public int Total { get; set; }

        public string Msg { get; set; }
    }
}
