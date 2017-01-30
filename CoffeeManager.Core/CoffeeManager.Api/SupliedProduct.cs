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
    
    public partial class SupliedProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupliedProduct()
        {
            this.ProductCalculations = new HashSet<ProductCalculation>();
            this.SuplyOrderItems = new HashSet<SuplyOrderItem>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> ExprenseTypeId { get; set; }
        public Nullable<int> CoffeeRoomNo { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public int Priority { get; set; }
    
        public virtual ExpenseType ExpenseType { get; set; }
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCalculation> ProductCalculations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuplyOrderItem> SuplyOrderItems { get; set; }
    }
}
