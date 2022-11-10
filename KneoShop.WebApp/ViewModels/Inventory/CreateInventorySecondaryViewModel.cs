using System.ComponentModel.DataAnnotations;
using KenoShop.WebApp.Entities.Products;

namespace KenoShop.WebApp.ViewModels.Inventory
{
    public class CreateInveotrySecondaryViewModel
    {
        public int InventoryID { get; set; }

        public int? ProductID { get; set; }

        public string? ProductName { get; set; }

        public int? ColorID { get; set; }

        public int? SizeID { get; set; }

        public int? InventoryAmount { get; set; }

        public bool? IsDeleted { get; set; }
        
        public string? SizeName { get; set; }

        public string? ColorName { get; set; }
    }
}