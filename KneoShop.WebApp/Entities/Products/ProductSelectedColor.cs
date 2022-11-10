using KenoShop.WebApp.Entities.BasicDefinitions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenoShop.WebApp.Entities.Products
{
    [Table("ProductSelectedColors",Schema ="Sales")]
    public class ProductSelectedColor
    {

        #region Props
        [Key] public int ProductSelectedColorID { get; set; }
        public int ProductID { get; set; }
        public int ColorID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        #endregion

        #region Relations

        public Product Product { get; set; }
        public Color Color { get; set; }

        #endregion
    }
}
