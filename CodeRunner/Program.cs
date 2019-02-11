using SMSTool;
using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace CodeRunner
{
    class Program
    {
        static private SerialPort _port;
        private static int TimeOut = 300;
        private static bool Connected;
        public static AutoResetEvent receiveNow;

        static void Main(string[] args)
        {
            SMSTimer timer = new SMSTimer();
     timer.Start();
     Console.Read();
     timer.Stop();

            /*receiveNow = new AutoResetEvent(false);

            _port = new SerialPort("COM2", 9600)
            {
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                ReadTimeout = TimeOut,
                WriteTimeout = TimeOut,
                DtrEnable = true,
                RtsEnable = true,
                NewLine = Environment.NewLine,
                Encoding = Encoding.GetEncoding("iso-8859-1"),
            };
            _port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            try
            {

            }
            catch (Exception ex)
            {
                _port.Close();
                Console.WriteLine($"SetUp >> Device not connected >> {ex.Message}");
                return;
            }
     
            Connected = true;
            var Storage = (true) ? "SM" : "ME";
            ExeCommand($"AT+CMGF=1{(char)(13)}");               //Set Text Mode to Text
            ExeCommand($"AT+CPMS=\"{Storage}\"{(char)(13)}");   //Set Text Storage
            Console.WriteLine($"Device is connected and ready");

            ExeCommand(@"AT" + (char)(13));
            ExeCommand(@"AT+CMGL=""ALL""" + (char)(13));
            Console.Read();*/
        }

        private static void ExeCommand(string cmd)
        {
            try
            {
                if (!_port.IsOpen) { _port.Open(); }
                _port.BaseStream.Flush();
                _port.DiscardOutBuffer();
                _port.DiscardInBuffer();
                _port.Write(cmd);
                //Thread.Sleep(300);

                receiveNow.Reset();
                //string rs = _port.ReadExisting();
                string rs = ReadResponse(_port, 300);
                Console.WriteLine($">>> {rs}");
            }
            catch (Exception ex)
            {
                _port.Close();
                Console.WriteLine($"ExeCommand >> Device not connected >> {ex.Message}");
            }
        }

        public static string ReadResponse(SerialPort port, int timeout)
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        if (buffer.Length > 0)
                            throw new ApplicationException("Response received is incomplete.");
                        else
                            throw new ApplicationException("No data received from phone.");
                    }
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer;
        }

        public static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                {
                    receiveNow.Set();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
