using System;
using System.ComponentModel.DataAnnotations;

namespace MyVanity.Model.MessageModels
{
    public class MessageEditModel : ModelBase
    {
        [Display(Name = "From: ")]
        public string FromUserName { get; set; }
        
        public int FromUserId { get; set; }

        [Display(Name = "To: ")]
        public string ToUserName { get; set; }

        public int ToUserId { get; set; }

        public string Body { get; set; }

        public string FromProfilePic { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Subject: ")]
        public string Subject { get; set; }

        public bool IsRead { get; set; }

        public bool IsReplying { get; set; }

        public int? RepliesTo { get; set; }
    }
}
