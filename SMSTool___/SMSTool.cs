using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace SMSTool
{
    public class SMSTool
    {
        private int TimeOut = 1000;
        private SerialPort _port;

        public SMSTool(){ SetUp(); }

        private void SetUp()
        {
            _port = new SerialPort(Config.COM_PORT, 9600)
            {
                DataBits    = 8,
                StopBits    = StopBits.One,
                Parity      = Parity.None,
                ReadTimeout = TimeOut,
                WriteTimeout= TimeOut,
                Handshake   = Handshake.RequestToSend,
                DtrEnable   = true,
                RtsEnable   = true,
                NewLine     = Environment.NewLine,
                Encoding    = Encoding.GetEncoding("iso-8859-1")
            };
            var Storage = (Config.USE_SIM) ? "SM" : "ME" ;
            ExeCommand($"AT+CMGF=1{(char)(13)}");               //Set Text Mode to Text
            ExeCommand($"AT+CPMS=\"{Storage}\"{(char)(13)}");   //Set Text Storage
        }

        public void DeviceInfo()
        {
            ExeCommand("AT+CGMM" + (char)(13));
            ExeCommand("AT+CGMI" + (char)(13));
            ExeCommand("AT+CGSN" + (char)(13));
            ExeCommand("AT+CGMR" + (char)(13));
            ExeCommand("AT+CIMI" + (char)(13));
        }

        public string ReadSMS()
        {
            return ExeCommand(@"AT+CMGL=""ALL""" + (char)(13));
        }

        public string DeleteSMS(int index)
        {
            return ExeCommand($"AT+CMGD=\"{index}\"" + (char)(13));
        }

        public void SendSMS()
        {
            ExeCommand(@"AT+CMGS=""8765357123""" + (char)(13));
            ExeCommand(@"send to rohan" + (char)(26));
        }

        private string ExeCommand(string cmd)
        {
            try {
                if (!_port.IsOpen) { _port.Open(); }
                _port.BaseStream.Flush();
                _port.DiscardOutBuffer();
                _port.DiscardInBuffer();
                _port.Write(cmd);
                Thread.Sleep(300);
                return _port.ReadExisting();
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        
        public string[] ListPorts()
        {
            return SerialPort.GetPortNames();
        }

        public void Close() {
            _port.Close();
        } 
    }
}
