using System.ComponentModel.DataAnnotations;

namespace KenoShop.WebApp.ViewModels.Account
{
    public class EditUserViewModel
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
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string? UserProvinceTitle { get; set; }
        public string? UserFatherName { get; set; }
        public string? UserCountryTitle { get; set; }
        public string? UserAddress { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? UserPostCode { get; set; }
        public string? UserCityTitle { get; set; }
        public DateTime? UserBirthday { get; set; }
        public int? UserCountryID { get; set; }
        public int? UserCityID { get; set; }
        public int? UserProvinceID { get; set; }
    }
}