using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GIO.Utilities
{
    public class Logger
    {
        public static bool LogWarnings { get; set; }
        public static bool LogErrors { get; set; }
        public static bool LogInfos { get; set; }

        public static void LogError(string _message)
        {
            if (LogErrors)
            Debug.WriteLine($"*****ERROR***** --> {_message}");
        }

        public static void LogWarning(string _message)
        {
            if (LogWarnings)
            Debug.WriteLine($"*****WARNING***** --> {_message}");
        }

        public static void LogInfo(string _message)
        {
            if (LogInfos)
            Debug.WriteLine($"*****INFO***** --> {_message}");
        }
    }
}
