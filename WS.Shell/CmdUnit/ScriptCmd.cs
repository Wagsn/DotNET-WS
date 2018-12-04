using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    public class ScriptCmd : CmdUnitBase
    {
        public RunContext RunContext { get; set; }

        public ScriptCmd(ShellContext context) : base(context) { }

        public override int Excute(string arg)
        {

            return 0;
            //RunContext
        }

        public override void Init()
        {
            Name = "script";
            Desc = "脚本";
            Usage = "script print(\"hello world\")";
        }
    }
}
