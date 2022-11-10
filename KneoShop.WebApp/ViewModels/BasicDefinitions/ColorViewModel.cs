using System.ComponentModel.DataAnnotations;

namespace KenoShop.WebApp.ViewModels.BasicDefinitions
{
    public class ColorViewModel
    {
        [Key] public int ColorID { get; set; }

        public string ColorName { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
