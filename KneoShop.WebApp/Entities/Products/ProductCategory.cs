using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenoShop.WebApp.Entities.Products;

[Table(name: "ProductCategories", Schema = "Sales")]
public class ProductCategory
{
    #region Props

    [Key] public int ProductCategoryID { get; set; }
    public bool? IsActive { get; set; }
    public string ProductCategoryTitle { get; set; }

    #endregion

    #region Relations

    public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }

    #endregion
}