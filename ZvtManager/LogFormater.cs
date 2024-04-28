using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskStar.ZvtTest.ZvtManager
{
    internal class LogFormater
    {
        internal static string PrintParams(ParameterInfo[] paramNames, params object[] args)
        {
            string retValue = "";
            for (int i = 0; i < args.Length; i++)
            {
                retValue += $", {paramNames[i].Name} : {args[i]}";
            }
            return retValue;
        }

    }
}
