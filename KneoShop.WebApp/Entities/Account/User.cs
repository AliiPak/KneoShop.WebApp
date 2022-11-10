using KenoShop.WebApp.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenoShop.WebApp.Entities.Account
{
    [Table("Users", Schema = "com")]
    public class User
    {
        [Key] public int UserID { get; set; }
        public int? UserCode { get; set; }
        public int? UserCountryID { get; set; }
        public int? UserCityID { get; set; }
        public int? UserProvinceID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsDeleted { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? ActiveCode { get; set; } = string.Empty;
        public string? UserFirstName { get; set; } = string.Empty;
        public string? UserLastName { get; set; } = string.Empty;
        public string? UserProvinceTitle { get; set; } = string.Empty;
        public string? UserFatherName { get; set; } = string.Empty;
        public string? UserCountryTitle { get; set; } = string.Empty;
        public string? UserAddress { get; set; } = string.Empty;
        public string? UserPhoneNumber { get; set; } = string.Empty;
        public string? UserPostCode { get; set; } = string.Empty;
        public string? UserCityTitle { get; set; } = string.Empty;
        public DateTime? UserBirthday { get; set; }
    }
}