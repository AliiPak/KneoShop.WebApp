using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenoShop.WebApp.Entities.Products;

[Table(name:"ProductSelectedCategories",Schema = "Sales")]
public class ProductSelectedCategory
{
    #region Props

    [Key]
    public int ProductSelectedCategoryID { get; set; }
    public int ProductID { get; set; }
    public int ProductCategoryID { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }

    #endregion

    #region Relations

    public Product Product { get; set; }

    public ProductCategory ProductCategory { get; set; }
    #endregion
}