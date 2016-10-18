﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CoffeeRoomEntities : DbContext
    {
        public CoffeeRoomEntities()
            : base("name=CoffeeRoomEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CupSuply> CupSuplies { get; set; }
        public virtual DbSet<CupType> CupTypes { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<UsedCupsPerShift> UsedCupsPerShifts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UtilizedCup> UtilizedCups { get; set; }
        public virtual DbSet<Dept> Depts { get; set; }
        public virtual DbSet<UsedProductsPerShift> UsedProductsPerShifts { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<SupliedProduct> SupliedProducts { get; set; }
        public virtual DbSet<SuplyRequest> SuplyRequests { get; set; }
    }
}
