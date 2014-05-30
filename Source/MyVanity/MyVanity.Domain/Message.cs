//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyVanity.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message : IEntity 
    {
        public Message()
        {
            this.Attachments = new HashSet<MessageAttachment>();
        }
    
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int ToId { get; set; }
        public int FromId { get; set; }
        public bool IsRead { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual User From { get; set; }
        public virtual User To { get; set; }
        public virtual ICollection<MessageAttachment> Attachments { get; set; }
        public virtual Message RepliesTo { get; set; }
    }
}
