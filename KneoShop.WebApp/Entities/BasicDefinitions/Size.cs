using KenoShop.WebApp.Entities.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenoShop.WebApp.Entities.BasicDefinitions;

[Table("Sizes", Schema = "Sales")]
public class Size
{
    #region Props

    [Key]
    public int SizeID { get; set; }

    public string SizeTitle { get; set; }

    public int TypeID { get; set; }
    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }


    #endregion

    #region Relations

    public ICollection<ProductSelectedSize> ProductSelectedSizes { get; set; }

    #endregion
}