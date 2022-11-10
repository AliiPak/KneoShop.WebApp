using System.ComponentModel.DataAnnotations;

namespace KenoShop.WebApp.ViewModels.Account
{
    public class MyUserViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]        
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر داشته باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? IsDeleted { get; set; }

    }
}
