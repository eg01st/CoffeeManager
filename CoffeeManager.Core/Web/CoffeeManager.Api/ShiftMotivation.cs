//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeeManager.Api
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShiftMotivation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public int MotivationId { get; set; }
        public System.DateTime Date { get; set; }
        public decimal ShiftScore { get; set; }
        public decimal Moneycore { get; set; }
        public decimal OtherScore { get; set; }
    
        public virtual Motivation Motivation { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual User User { get; set; }
    }
}
