using KenoShop.WebApp.Entities.BasicDefinitions;
using KenoShop.WebApp.Entities.Products;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KenoShop.WebApp.ViewModels.Products
{
    public class CreateProductViewModel
    {
        public CreateProductViewModel()
        {
            BrandNames = BrandProduct.Zara;
        }
        public int ProductID { get; set; }

        public int? ProductCode { get; set; }
        
        public int? ProductSexTypeID { get; set; }
        
        public string? ProductSexTypeTitle { get; set; }

        public bool? IsInActive { get; set; }

        public decimal? ProductPrice { get; set; }
        
        public decimal? ProductPurchasedPrice { get; set; }
        
        public decimal? ProductLiraPrice { get; set; }

        public string ProductName { get; set; }
        
        public int? BrandID { get; set; }

        public string? ProductDescription { get; set; }

        public string? ProductShortDescription { get; set; }

        [Display(Name = "تصویر")] public IFormFile? Image { get; set; }

        [Display(Name = "گالری تصاویر")] public List<IFormFile>? Galleries { get; set; }

        public string? ImageName { get; set; }

        public string? InventoryLocationTitle { get; set; }
        
        public int? InventoryLocationID { get; set; }
        public List<ProductCategory>? ProductCategories { get; set; }

        public List<int>? SelectedCategories { get; set; }

        public List<Size>? ProductSizes { get; set; }

        public List<int>? SelectedSizes { get; set; }

        public List<Color>? ProductColors { get; set; }

        public List<int>? SelectedColors { get; set; }
        
        public BrandProduct BrandNames { get; set; }
    }
    public enum BrandProduct
    {
        [Display(Name ="بدون برند")]
        NoBrand,
        [Display(Name ="Zara")]
        Zara,
        [Display(Name = "Pull and Bear")]
        PullandBear,
        [Display(Name ="Bershka")]
        Bershka,
        [Display(Name = "H&M")]
        HandM,
        [Display(Name = "LC Waikiki")]
        LC
    }



}

