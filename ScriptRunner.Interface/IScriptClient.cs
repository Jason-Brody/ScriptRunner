﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Interface
{
    public interface IScriptClient
    {
        void Connect();

        void RunScript(ScriptTaskInfo info);

        void Complete();
    }
}
