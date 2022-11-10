using KenoShop.WebApp.Entities.Products;
using KenoShop.WebApp.ViewModels.Paging;
using System.ComponentModel.DataAnnotations;

namespace KenoShop.WebApp.ViewModels.Products
{
    public class FilterProductViewModel : BasePaging<Product>
    {
        public FilterProductViewModel()
        {
            OrderBy = OrderProducts.NoOrder;
        }

        public string? Name { get; set; }

        public OrderProducts OrderBy { get; set; }
    }
    public enum OrderProducts
    {
        [Display(Name ="بدون ترتیب")]
        NoOrder,
        [Display(Name = "قیمت (صعودی)")]
        Price_ASC,
        [Display(Name = "قیمت (نزولی)")]
        Price_DESC,
        [Display(Name = "موجودی (صعودی)")]
        Inventory_ASC,
        [Display(Name = "موجودی (نزولی)")]
        Inventory_DESC,
        [Display(Name = "نام محصول (صعودی)")]
        ProductName_ASC,
        [Display(Name = "نام محصول (نزولی)")]
        ProductName_DESC
    }
}
