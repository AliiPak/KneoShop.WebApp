using System.Security.Claims;
using KenoShop.WebApp.ViewModels.Account;
using KenoShop.WebApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenoShop.WebApp.Entities.Account;

namespace KenoShop.WebApp.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {
        #region ctor
        private readonly KenoShopDbContext _context;
        public AccountController(KenoShopDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Account List

        public IActionResult Index(FilterAccountViewModel filterAccount)
        {
            var query = _context.Users
                         .Where(p => p.IsActive == true && p.IsDeleted == false)
                         .AsQueryable();

            //Filter
            if (filterAccount.Name != null)
            {
                query = query.Where(q => EF.Functions.Like(q.UserName, $"%{filterAccount.Name}%"));
            }

            //Sort
            switch (filterAccount.OrderBy)
            {
                case OrderAccounts.NoOrder:
                    break;
                case OrderAccounts.Code_ASC:
                    query = query.OrderBy(q => q.UserID);
                    break;
                case OrderAccounts.Code_DESC:
                    query = query.OrderByDescending(q => q.UserID);
                    break;
                case OrderAccounts.Name_ASC:
                    query = query.OrderBy(q => q.UserName);
                    break;
                case OrderAccounts.Name_DESC:
                    query = query.OrderByDescending(q => q.UserName);
                    break;
            }

            //Paging
            filterAccount.SetPaging(query);

            return View(filterAccount);
        }

        #endregion

        #region Create Account

        [HttpGet("create-account")]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost("create-user"), ValidateAntiForgeryToken]
        public IActionResult CreateAccount(CreateUserViewModel createUser)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = createUser.UserName,
                    Email = createUser.Email,
                    Password = createUser.Password,
                    IsAdmin = createUser.IsAdmin,
                    IsActive = true,
                    IsDeleted = false
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Account", new { area = "Admin" });
        }

        #endregion

        #region Edit Account

        [HttpGet("edit-account")]
        public IActionResult EditAccount(int Id)
        {
            var selectUser = _context.Users.SingleOrDefault(p => p.UserID == Id);

            if (selectUser == null)
            {
                return NotFound();
            }

            var editModel = new EditUserViewModel
            {
                UserName = selectUser.UserName,
                Password = selectUser.Password,
                Email = selectUser.Email,
                IsActive = selectUser.IsActive,
                IsAdmin= selectUser.IsAdmin
            };

            return View("EditAccount", editModel);
        }

        [HttpPost("edit-account"), ValidateAntiForgeryToken]
        public IActionResult EditAccount(EditUserViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                var selectUser = _context.Users.SingleOrDefault(p => p.UserID == editUser.UserID);

                if (selectUser == null) return NotFound();


                selectUser.UserName = editUser.UserName;
                selectUser.Email = editUser.Email;
                selectUser.IsAdmin = editUser.IsAdmin;
                selectUser.IsActive = editUser.IsActive;

                _context.Users.Update(selectUser);
                _context.SaveChanges();

                return RedirectToAction("Index", "Account", new { area = "Admin" });
            }

            return View(editUser);
        }

        #endregion

        #region remove Account

        public IActionResult RemoveAccount(int id)
        {
            var selectUser = _context.Users.SingleOrDefault(p => p.UserID == id);
            if (selectUser == null)
            {
                return Json(new
                {
                    status = "NotFound"
                });
            }

            selectUser.IsDeleted = true;
            _context.Users.Update(selectUser);
            _context.SaveChanges();

            return Json(new
            {
                status = "Success"
            });
        }

        #endregion

        #region My Account

        [HttpGet("myaccount")]
        public IActionResult MyAccount()
        {
            var userID = Convert.ToInt32(HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var myUser = _context.Users.SingleOrDefault(u => u.UserID == userID);
            MyUserViewModel myUserModel = new MyUserViewModel()
            {
                UserID = myUser.UserID,
                UserName = myUser.UserName,
                Email = myUser.UserName,
            };
            
            return View(myUserModel);
        }

        #endregion
    }
}
