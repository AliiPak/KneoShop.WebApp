using KenoShop.WebApp.Entities.Account;
using KenoShop.WebApp.ViewModels.Paging;
using System.ComponentModel.DataAnnotations;

namespace KenoShop.WebApp.ViewModels.Account
{
    public class FilterAccountViewModel : BasePaging<User>
    {
        public FilterAccountViewModel()
        {
            OrderBy = OrderAccounts.NoOrder;
        }

        public string? Name { get; set; }

        public OrderAccounts OrderBy { get; set; }
    }
    public enum OrderAccounts
    {
        [Display(Name = "بدون ترتیب")]
        NoOrder,
        [Display(Name = "کد کاربر (صعودی)")]
        Code_ASC,
        [Display(Name = "کد کاربر (نزولی)")]
        Code_DESC,
        [Display(Name = "نام کاربر (صعودی)")]
        Name_ASC,
        [Display(Name = "نام کاربر (نزولی)")]
        Name_DESC
        //[Display(Name = "ادمین (صعودی)")]
        //Admin_ASC,
        //[Display(Name = "ادمین (نزولی)")]
        //Admin_DESC
    }
}
