namespace KenoShop.WebApp.ViewModels.Inventory
{
    public class InventoryListViewModel
    {
        public int InventoryID { get; set; }

        public int? ProductID { get; set; }

        public int? ColorID { get; set; }

        public int? SizeID { get; set; }

        public int? InventoryAmount { get; set; }

        public bool? IsDeleted { get; set; }
    }
}