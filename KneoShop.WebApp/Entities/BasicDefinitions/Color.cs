using KenoShop.WebApp.Entities.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenoShop.WebApp.Entities.BasicDefinitions;

[Table("Colors",Schema = "Sales")]
public class Color
{
    #region Props
    [Key] public int ColorID { get; set; }

    public string ColorName { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }


    #endregion

    #region Relations

    public ICollection<ProductSelectedColor> ProductSelectedColors { get; set; }

    #endregion
}