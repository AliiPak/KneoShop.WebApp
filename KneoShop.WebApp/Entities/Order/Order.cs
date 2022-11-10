using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KenoShop.WebApp.Entities.Account;

namespace KenoShop.WebApp.Entities.Order
{
    [Table("Orders", Schema = "Sales")]
    public class Order
    {
        #region Props

        [Key] public int OrderID { get; set; }
        public int? UserID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? RefCode { get; set; }
        public string? Description { get; set; }

        #endregion

        #region Relation

        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}