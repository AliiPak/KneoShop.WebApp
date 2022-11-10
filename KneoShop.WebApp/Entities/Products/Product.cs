using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KenoShop.WebApp.Entities.BasicDefinitions;

namespace KenoShop.WebApp.Entities.Products
{
    [Table("Products", Schema = "Sales")]

    public class Product 
    {
        #region Props

        [Key]
        public int ProductID { get; set; }
        public int? ProductCode { get; set; }
        public string ProductName { get; set; }
        public int? ProductSexTypeID { get; set; }
        public string? ProductSexTypeTitle { get; set; }
        public int? BrandID { get; set; }
        public string? InventoryLocationTitle { get; set; }
        public int? InventoryLocationID { get; set; }
        public bool? IsInActive { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? ProductPurchasedPrice { get; set; }
        public decimal? ProductLiraPrice { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductShortDescription { get; set; }
        public string? Image { get; set; }

        #endregion

        #region Relation

        public ICollection<ProductGallery> ProductGalleries { get; set; }

        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        
        public ICollection<ProductSelectedColor> ProductSelectedColors { get; set; }
        
        public ICollection<ProductSelectedSize> ProductSelectedSizes { get; set; }

        public Brand Brand { get; set; }
        
        #endregion
    }
}
