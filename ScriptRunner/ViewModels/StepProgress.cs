using ScriptRunner.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.ViewModels
{
    public class StepProgress : WPFNotify
    {
        private bool _isIndetermine;
        public bool IsIndeterMine
        {
            get { return _isIndetermine; }
            set { SetProperty(ref _isIndetermine, value); }
        }

        private int _current;

        public int Current
        {
            get { return _current; }
            set { SetProperty(ref _current, value); }
        }

        private int _total;

        public int Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }

        private string _msg;

        public string Msg
        {
            get { return _msg; }
            set { SetProperty(ref _msg, value); }
        }
    }
}
