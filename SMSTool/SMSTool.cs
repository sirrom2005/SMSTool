using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace SMSTool
{
    public class SMSTool
    {
        private int TimeOut = 300;
        private SerialPort _port;
        public AutoResetEvent receiveNow;
        private bool Connected;

        public SMSTool(){ SetUp(); }

        public void SetUp()
        {
            receiveNow = new AutoResetEvent(false);
            try
            {
                _port = new SerialPort(Config.COM_PORT, 9600)
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
                _port.Open();
                Logger.Write($"Device is connected and ready");

                Connected = true;
                var Storage = (Config.USE_SIM) ? "SM" : "ME";
                ExeCommand($"AT+CMGF=1{(char)(13)}");               //Set Text Mode to Text
                ExeCommand($"AT+CPMS=\"{Storage}\"{(char)(13)}");   //Set Text Storage
            }
            catch (Exception ex) {
                Close();
                Logger.Write($"SetUp >> Device not connected >> {ex.Message}");
            }
        }

        public void DeviceInfo()
        {
            ExeCommand("AT+CGMM" + (char)(13));
            ExeCommand("AT+CGMI" + (char)(13));
            ExeCommand("AT+CGSN" + (char)(13));
            ExeCommand("AT+CGMR" + (char)(13));
            ExeCommand("AT+CIMI" + (char)(13));
        }

        public string ReadSMS() { return ExeCommand(@"AT+CMGL=""ALL""" + (char)(13)); }
        public string DeleteSMS(int index) { return ExeCommand($"AT+CMGD={index}" + (char)(13)); }

        public void SendSMS()
        {
            ExeCommand(@"AT+CMGS=""8765357123""" + (char)(13));
            ExeCommand(@"send to rohan again" + (char)(26));
        }

        private string ExeCommand(string cmd)
        {
            try {
                _port.BaseStream.Flush();
                _port.DiscardOutBuffer();
                _port.DiscardInBuffer();
                _port.Write(cmd);
                return ReadResponse();
            }
            catch(Exception ex) {
                Close();
                Logger.Write($"ExeCommand >> Device not connected >> {ex.Message}");
            }
           return string.Empty;
        }
        
        public string[] ListPorts() { return SerialPort.GetPortNames(); }

        public void Close() {
            Connected = false;
            if (_port!=null) {
                _port.Close();
            }
        }

        public bool IsConnected() { return Connected; }

        public string ReadResponse()
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(TimeOut, false))
                    {
                        string t = _port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        if (buffer.Length > 0)
                            Logger.Write($"Response received is incomplete. >> {buffer}");
                        else
                            Logger.Write($"No data received from phone.");
                        buffer = "\r\nERROR\r\n";
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

        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
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
