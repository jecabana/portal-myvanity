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
    
    public partial class DocumentSubcategory : IEntity 
    {
        public DocumentSubcategory()
        {
            this.Documents = new HashSet<Document>();
        }
    
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    
        public virtual DocumentCategory Category { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
