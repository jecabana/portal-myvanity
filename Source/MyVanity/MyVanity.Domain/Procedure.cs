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
    
    public partial class Procedure : IEntity 
    {
        public Procedure()
        {
            this.Users = new HashSet<UserProcedure>();
        }
    
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public Nullable<double> RegularPrice { get; set; }
        public Nullable<double> SalePrice { get; set; }
        public string PicPath { get; set; }
    
        public virtual ICollection<UserProcedure> Users { get; set; }
        public virtual ProcedureCategory Category { get; set; }
        public virtual ProcedureType Type { get; set; }
    }
}
