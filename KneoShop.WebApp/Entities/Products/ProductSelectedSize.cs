using KenoShop.WebApp.Entities.BasicDefinitions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenoShop.WebApp.Entities.Products
{
    [Table("ProductSelectedSizes",Schema ="Sales")]
    public class ProductSelectedSize
    {
        #region Props
        [Key] public int ProductSelectedSizeID { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        #endregion

        #region Relations
        public Product Product { get; set; }

        public Size Size { get; set; }
        #endregion
    }
}
