using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;


/// <summary>
/// Main loader classs
/// </summary>
namespace SMSTool
{
    public class SMSTimer
    {
        private List<ShortMessage> ShortMessageList;
        private Timer timer;
        private SMSTool tool;

        public SMSTimer() { tool = new SMSTool(); }

        public void Start() {
            Logger.Write($"Starting SMS Service in {Config.SERVICE_DELAY_START/1000} seconds on COM PORT >> {Config.COM_PORT}");
            timer = new Timer(SmsCallBack, null, Config.SERVICE_DELAY_START, Config.SERVICE_FREQUENCY);      
        }

        public void Stop() {
            Logger.Write($"Stopping SMS Service");
            tool.Close();
            if (timer != null)
            {
                timer.Dispose();
            }
        }

        private async void SmsCallBack(object state)
        {
            if (!tool.IsConnected()) { return; }
            ReadSms();
            if (ShortMessageList.Count > 0)
            {
                int count = await WebClient.PostDataAsync(JsonConvert.SerializeObject(ShortMessageList));                 
                if (count == ShortMessageList.Count)
                {
                    Logger.Write($"{count} Message added to database");
                    for (int i = 0; i<ShortMessageList.Count; i++)
                    {
                        if (!tool.DeleteSMS(ShortMessageList[i].Index).Contains("OK\r\n"))
                        {
                            Logger.Write($"Error Deleting message\r\n{ShortMessageList[i].ToString()}");
                        }
                    }
                }
            }
        }

        private void ReadSms()
        {
            ShortMessageList = new List<ShortMessage>();
            var SMS = tool.ReadSMS();

            if (SMS.Contains("OK\r\n"))
            {
                Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n");
                Match m = r.Match(SMS);
                while (m.Success)
                {
                    ShortMessageList.Add(new ShortMessage()
                    {
                        Index       = int.Parse(m.Groups[1].Value),
                        Status      = m.Groups[2].Value,
                        Sender      = m.Groups[3].Value,
                        Alphabet    = m.Groups[4].Value,
                        SmsDate     = m.Groups[5].Value,
                        Message     = m.Groups[6].Value
                    });
                    m = m.NextMatch();
                }
            }
        }
    }
}