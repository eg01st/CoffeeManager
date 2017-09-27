namespace CoffeeManager.Models
{
    public class SupliedProduct : Entity
    {
        public decimal Price { get; set; }
        public decimal? Quatity { get; set; }
        public decimal? SalePrice { get; set; }

        public int? ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
        public bool InventoryEnabled { get; set; }

        public string ExpenseNumerationName { get; set; }
        public decimal ExpenseNumerationMultyplier { get; set; }

        public string InventoryNumerationName { get; set; }
        public decimal InventoryNumerationMultyplier { get; set; }
    }
}
