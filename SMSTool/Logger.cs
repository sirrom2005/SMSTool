using System;
using System.Diagnostics;

namespace SMSTool
{
    class Logger
    {
        public static void Write(string text)
        {
            using (EventLog eventLog = new EventLog())
            {
                string now = DateTime.Now.ToString();
                //Console.WriteLine($"[{now}]\r\n{text}\r\n");
                eventLog.WriteEntry($"[{now}]\r\n{text}\r\n");
            }
        }
    }
}
