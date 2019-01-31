using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SMSTool
{
    internal class ShortMessage
    {
        private string _Status;
        private string _SmsDate;

        public int Index { get; internal set; }
        public string Sender { get; internal set; }       
        public string Message { get; internal set; }

        public string Status
        {
            get
            {
                switch (_Status)
                {
                    case "REC UNREAD":
                    case "REC READ":
                        return "RECEIVED";
                    default:
                        return "";
                }
            }

            internal set { _Status = value; }
        }

        public string SmsDate {
            get
            {
                if (DateTime.TryParseExact(_SmsDate.Substring(0, 17), "yy/MM/dd,HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime OutDate))
                {
                    return OutDate.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return string.Empty;
            }

            internal set { _SmsDate = value; }
        }

        public string Alphabet { get; internal set; }

        public override string ToString()
        {
            return $"{Index} >> {Status} >> {Sender}>> {Alphabet} >> {SmsDate} >> {Message}";
        }
    }
}