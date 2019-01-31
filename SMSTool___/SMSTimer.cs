using Newtonsoft.Json;
using System;
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

        public SMSTimer() {
            tool = new SMSTool();
        }

        public void Start() {
            timer = new Timer(SmsCallBack, null, 0, Config.SERVICE_FREQUENCY);
        }

        public void Stop() {
            tool.Close();
        }

        private async void SmsCallBack(object state)
        {
            ReadSms();

            Console.WriteLine(ShortMessageList.Count);
            if (ShortMessageList.Count>0) {
                string output = JsonConvert.SerializeObject(ShortMessageList);
                int count = await WebClient.PostDataAsync(output);
                if(count==ShortMessageList.Count) {
                    var smsList = ShortMessageList;
                   
                    foreach (var info in smsList)
                    {
                        //Console.WriteLine(info.ToString());
                        //tool.DeleteSMS(info.Index);
                        smsList.RemoveAll(x => x.Index == info.Index);
                        Console.WriteLine(info);
                    }
                }
            }
            Console.WriteLine(ShortMessageList.Count);
        }

        private void ReadSms()
        {
            string sms = tool.ReadSMS();
            if (sms.Contains("OK\r\n"))
            {
                ShortMessageList = new List<ShortMessage>();
                Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n");
                Match m = r.Match(sms);
                while (m.Success)
                {
                    ShortMessageList.Add(new ShortMessage()
                                            {
                                                Index   = int.Parse(m.Groups[1].Value),
                                                Status  = m.Groups[2].Value,
                                                Sender  = m.Groups[3].Value,
                                                Alphabet= m.Groups[4].Value,
                                                SmsDate = m.Groups[5].Value,
                                                Message = m.Groups[6].Value
                                            });                       
                    m = m.NextMatch();
                }
            }
        }
    }
}