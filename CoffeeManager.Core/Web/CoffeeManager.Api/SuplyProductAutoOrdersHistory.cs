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
    
    public partial class SuplyProductAutoOrdersHistory
    {
        public int Id { get; set; }
        public int SuplyProductId { get; set; }
        public int OrderHistoryId { get; set; }
        public int QuantityBefore { get; set; }
        public int OrderedQuantity { get; set; }
    
        public virtual AutoOrdersHistory AutoOrdersHistory { get; set; }
        public virtual SupliedProduct SupliedProduct { get; set; }
    }
}