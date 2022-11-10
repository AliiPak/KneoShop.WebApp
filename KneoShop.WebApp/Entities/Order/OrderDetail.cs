using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KenoShop.WebApp.Entities.Products;

namespace KenoShop.WebApp.Entities.Order
{
    [Table("OrderDetails", Schema = "Sales")]
    public class OrderDetail
    {
        #region Props

        [Key] public int? OrderDetailID { get; set; }
        public int? OrderID { get; set; }
        public int? ProductID { get; set; }
        public int? Price { get; set; }
        public int? Count { get; set; }

        #endregion

        #region Relation
        public Order Order { get; set; }

        public Product Product { get; set; }
        #endregion
    }
}