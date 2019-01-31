using System;
using SMSTool;

namespace CodeRunner
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
