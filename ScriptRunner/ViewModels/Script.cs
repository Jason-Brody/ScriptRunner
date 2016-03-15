using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptRunner.Interface;
using System.Collections.ObjectModel;
using System.Data;
using ScriptRunner.Models;

namespace ScriptRunner.ViewModels
{
   
    public class Script:WPFNotify
    {
        public Script() { }

        public Script(ScriptMarshal script)
        {
            _stepsbyDatas = new Dictionary<int, List<Step>>();
            this.Name = script.Name;
            this.Description = script.Description;
            this.HelpLink = script.HelpLink;
            this.Steps = new List<Step>();
            this.Types = new List<InputData>();
            this.Location = script.Location;
            this.TargetClass = script.TargetClass;
            foreach(var s in script.Steps)
            {
                this.Steps.Add(new Step(s));
            }
            foreach(var t in script.Types)
            {
                this.Types.Add(new InputData(t));
            }
            this.Datas = new DataTable();
            foreach (var item in this.Types.OrderBy(s=>s.IsOutput))
            {
                DataColumn dc = new DataColumn(item.Name, Type.GetType(item.Type));
                if (item.IsOutput)
                    dc.ReadOnly = true;
                this.Datas.Columns.Add(dc);
            }
        }

        

        private Dictionary<int, List<Step>> _stepsbyDatas;
        public Dictionary<int,List<Step>> StepsByDatas { get { return _stepsbyDatas; } }

        public bool IsSingtonMode { get; set; } = true;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string HelpLink { get; set; }

        public string Location { get; set; }

        public string TargetClass { get; set; }

        public List<Step> Steps { get; set; }

        public List<InputData> Types { get; set; }

        

        private bool _isChoose;
        public bool IsChoose
        {
            get { return _isChoose; }
            set { SetProperty(ref _isChoose, value); }
        }

        private DataTable _datas;

        public DataTable Datas
        {
            get { return _datas; }
            set { SetProperty(ref _datas, value); }
        }

        public void SetData(IDictionary<int,System.Dynamic.ExpandoObject> data)
        {
            
            DataTable newTable = Datas.Copy();
            foreach(var item in data)
            {
                DataRow dr = newTable.Rows[item.Key];
                foreach(var subItem in item.Value)
                {
                    if(newTable.Columns[subItem.Key].ReadOnly)
                    {
                        newTable.Columns[subItem.Key].ReadOnly = false;
                        dr[subItem.Key] = subItem.Value;
                        newTable.Columns[subItem.Key].ReadOnly =true;
                    }
                    else
                    {
                        dr[subItem.Key] = subItem.Value;
                    }
                }
            }
            Datas = newTable;
        }

    }

    public class ScriptMarshal : MarshalByRefObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HelpLink { get; set; }

        public string Location { get; set; }

        public string TargetClass { get; set; }

        public List<StepMarshal> Steps { get; set; }

        public List<InputDataMarshal> Types { get; set; }
    }

}
