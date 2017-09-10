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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
        public virtual DbSet<ProductCalculation> ProductCalculations { get; set; }
        public virtual DbSet<SuplyOrder> SuplyOrders { get; set; }
        public virtual DbSet<SuplyOrderItem> SuplyOrderItems { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<ExpenseSuplyProduct> ExpenseSuplyProducts { get; set; }
        public virtual DbSet<InventoryReport> InventoryReports { get; set; }
        public virtual DbSet<InventoryReportItem> InventoryReportItems { get; set; }
        public virtual DbSet<UserPenalty> UserPenalties { get; set; }
    
        public virtual ObjectResult<GetAllSales_Result> GetAllSales(Nullable<System.DateTime> from, Nullable<System.DateTime> to)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllSales_Result>("GetAllSales", fromParameter, toParameter);
        }
    
        public virtual ObjectResult<GetExpenses_Result> GetExpenses(Nullable<System.DateTime> from, Nullable<System.DateTime> to)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetExpenses_Result>("GetExpenses", fromParameter, toParameter);
        }
    
        public virtual ObjectResult<GetMetroExpenses_Result> GetMetroExpenses(Nullable<System.DateTime> from, Nullable<System.DateTime> to)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetMetroExpenses_Result>("GetMetroExpenses", fromParameter, toParameter);
        }
    
        public virtual ObjectResult<GetSales_Result> GetSales(Nullable<System.DateTime> from, Nullable<System.DateTime> to, Nullable<int> id)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSales_Result>("GetSales", fromParameter, toParameter, idParameter);
        }
    }
}
