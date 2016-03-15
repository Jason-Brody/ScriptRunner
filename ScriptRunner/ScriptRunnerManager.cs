using ScriptRunner.Models;
using ScriptRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner
{
    static class ScriptRunnerManager
    {
        static ScriptRunnerManager()
        {
            ScriptFolders = new ObservableCollection<ScriptFolder>();
            Scripts = new ObservableCollection<Script>();
            Robots = new List<Robot>();
        }

        public static ObservableCollection<Script> Scripts = null;

        public static ObservableCollection<ScriptFolder> ScriptFolders = null;

        public static List<Robot> Robots = null;

        public static Script CurrentScript { get; set; }

        public static string RobotCount { get; set; }
    }
}
