using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenoShop.WebApp.Entities.BasicDefinitions
{
    [Table("Brands",Schema ="Sales")]
    public class Brand
    {
        #region Props

        [Key]
        public int BrandID { get; set; }

        public string BrandName { get; set; }

        #endregion
        
    }
}
