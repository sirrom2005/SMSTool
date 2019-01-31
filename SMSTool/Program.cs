using System;
using System.IO.Ports;

namespace SMSTool
{
    class Program
    {
        static void Main(string[] args)
        {
            SMSTimer T = new SMSTimer();
            T.Start();
            Console.Read();
            T.Stop();
        }
    }
}
