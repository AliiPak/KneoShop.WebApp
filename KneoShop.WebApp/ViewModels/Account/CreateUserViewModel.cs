namespace KenoShop.WebApp.ViewModels.Account
{
    public class CreateUserViewModel
    {
        public int? UserCode { get; set; }
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

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
