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
    
    public partial class Shift
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shift()
        {
            this.Expenses = new HashSet<Expense>();
            this.Sales = new HashSet<Sale>();
            this.UsedCupsPerShifts = new HashSet<UsedCupsPerShift>();
            this.UtilizedCups = new HashSet<UtilizedCup>();
            this.Depts = new HashSet<Dept>();
        }
    
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> IsFinished { get; set; }
        public decimal StartAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<int> CoffeeRoomNo { get; set; }
        public decimal TotalExprenses { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expense> Expenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsedCupsPerShift> UsedCupsPerShifts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UtilizedCup> UtilizedCups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dept> Depts { get; set; }
    }
}
