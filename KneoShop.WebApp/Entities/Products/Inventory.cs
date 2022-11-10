using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenoShop.WebApp.Entities.Products;

[Table("Inventory",Schema = "Sales")]
public class Inventory
{
    [Key]
    public int InventoryID { get; set; }
    
    public int? ProductID { get; set; }
    
    public int? ColorID { get; set; }
    
    public int? SizeID { get; set; }
    
    public int? InventoryAmount { get; set; }

    public bool? IsDeleted { get; set; }

    public string? SizeName { get; set; }

    public string? ColorName { get; set; }
}