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
    
    public partial class UserEarningsHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
        public bool IsDayShift { get; set; }
    
        public virtual Shift Shift { get; set; }
        public virtual User User { get; set; }
    }
}
