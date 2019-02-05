using System;
using System.Diagnostics;

namespace SMSTool
{
    class Logger
    {
        public static void Write(string text)
        {
            string now = DateTime.Now.ToString();
            //Console.WriteLine($"[{now}]\r\n{text}\r\n");
            using (EventLog eventLog = new EventLog())
            {
                eventLog.Source = "SMS Tool";
                eventLog.WriteEntry($"[{now}]\r\n{text}\r\n", EventLogEntryType.Information, 101);
            }
        }
    }
}


//eventcreate /l "Application" /t Information /so "SMS Tool" /id 101 /d "SMS Tool Log Service"
//cd C:\Windows\Microsoft.NET\Framework\v4.0.30319