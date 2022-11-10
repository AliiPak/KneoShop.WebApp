using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace KenoShop.WebApp.Entities.Products
{
    [Table("ProductGalleries",Schema ="Sales")]
    public class ProductGallery
    {
        #region properties

        [Key]
        public int ProductGalleryID { get; set; }

        public int ProductID { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر داشته باشد")]
        public string Image { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        #endregion
    }
}
