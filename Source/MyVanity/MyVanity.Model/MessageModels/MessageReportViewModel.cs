using System;
using System.Security.Principal;

namespace MyVanity.Model.MessageModels
{
    public class MessageReportViewModel : ModelBase
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Body { get; set; }

        public bool IsRead { get; set; }

        public DateTime Date { get; set; }
    }
}
