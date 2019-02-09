using SMSTool;
using System;

namespace CodeRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            SMSTimer timer = new SMSTimer();
            timer.Start();
            Console.Read();
            timer.Stop();
        }
    }
}
