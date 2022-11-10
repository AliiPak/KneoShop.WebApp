namespace KenoShop.WebApp.ViewModels.Products
{
    public class ProductDetailViewModel
    {
        public int? ProductCode { get; set; }
        public int? ProductSexTypeID { get; set; }
        public bool? IsInActive { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? ProductPurchasedPrice { get; set; }
        public decimal? ProductLiraPrice { get; set; }
        public string ProductName { get; set; }
        public int? BrandID { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductShortDescription { get; set; }
    }
}
