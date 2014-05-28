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
    
    public partial class Agent : User 
    {
        public Agent()
        {
            this.Patients = new HashSet<Patient>();
            this.UserProcedures = new HashSet<UserProcedure>();
            this.PersonDetails = new PersonDetails();
        }
    
        public AgentType Type { get; set; }
        public string Email { get; set; }
        public string PicPath { get; set; }
        public string Description { get; set; }
    
        public PersonDetails PersonDetails { get; set; }
    
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<UserProcedure> UserProcedures { get; set; }
    }
}