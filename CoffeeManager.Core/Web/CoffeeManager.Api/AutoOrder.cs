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
    
    public partial class AutoOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AutoOrder()
        {
            this.SuplyProductOrderItems = new HashSet<SuplyProductOrderItem>();
            this.AutoOrdersHistories = new HashSet<AutoOrdersHistory>();
        }
    
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public System.TimeSpan Time { get; set; }
        public bool IsActive { get; set; }
        public int CoffeeRoomId { get; set; }
    
        public virtual CoffeeRoom CoffeeRoom { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuplyProductOrderItem> SuplyProductOrderItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutoOrdersHistory> AutoOrdersHistories { get; set; }
    }
}