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
    
    public partial class Sale
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int ShiftId { get; set; }
        public int Product { get; set; }
        public Nullable<bool> IsPoliceSale { get; set; }
        public System.DateTime Time { get; set; }
        public int CoffeeRoomNo { get; set; }
        public bool IsRejected { get; set; }
    
        public virtual Product Product1 { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
