namespace KenoShop.WebApp.ViewModels.Order
{
    public class OrderListViewModel
    {
        public int OrderID { get; set; }
        public int? UserID { get; set; }

        public string? UserName { get; set; }
        
        public DateTime? CreateDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? RefCode { get; set; }
        public string? Description { get; set; }
    }
}