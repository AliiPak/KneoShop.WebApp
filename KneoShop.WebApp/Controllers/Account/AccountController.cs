using KenoShop.WebApp.Context;
using KenoShop.WebApp.Entities.Account;
using KenoShop.WebApp.Senders;
using KenoShop.WebApp.Services.Interfaces;
using KenoShop.WebApp.ViewModels.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Security.Claims;

namespace KenoShop.WebApp.Controllers.Account
{
    public class AccountController : Controller
    {
        #region Ctor

        private readonly KenoShopDbContext _context;
        private readonly IViewRenderService _viewRenderService;

        public AccountController(KenoShopDbContext context, IViewRenderService viewRenderService)
        {
            _context = context;
            _viewRenderService = viewRenderService;
        }

        #endregion

        #region Register

        [HttpGet("register")]
        public IActionResult Register()
        {
            //var Accounts = _context.Users.ToList();

            return View(new RegisterViewModel());
        }
        [HttpPost("register"),ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel Register)
        {
            if (ModelState.IsValid)
            {
                //Check Email Is Exists Or Not
                var UserExists = _context.Users.Any(u => u.Email == Register.Email.ToLower().Trim());

                if (!UserExists)
                {
                    //Add New User To DataBase
                    var user = new User()
                    {
                        Email = Register.Email.ToLower().Trim(),
                        Password = Register.Password.Trim(),
                        ActiveCode = Guid.NewGuid().ToString("N")
                    };

                    _context.Users.Add(user);
                    _context.SaveChanges();

                    //Send Email Activation Code

                    string message = _viewRenderService.RenderToStringAsync("ActivateAccount", user);

                    EmailSender.SendEmail(user.Email, "فعالسازی حساب کاربری", message);

                    return RedirectToAction("Login");

                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده قبلا ثبت نام کرده است");
                }
            }
            return View();
        }

        #endregion

        #region Activate Account

        [HttpGet("activate-account/{emailActiveCode}")]
        public IActionResult ActiveAccount(string emailActiveCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == emailActiveCode);

            if (user != null)
            {
                user.IsActive = true;
                user.ActiveCode = Guid.NewGuid().ToString("N");
                _context.Users.Update(user);
                _context.SaveChanges();

                return View("Login");
            }
            else
            {
                return NotFound();
            }
        }

        #endregion

        #region Forgot Password

        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot-password"), ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var User = _context.Users.SingleOrDefault(u => u.Email == forgotPassword.Email.ToLower().Trim());

                if (User != null)
                {
                    string message = _viewRenderService.RenderToStringAsync("ForgotPassword", User);

                    EmailSender.SendEmail(forgotPassword.Email.Trim().ToLower(), "بازیابی رمز عبور", message);

                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                }
            }

            return View();
        }
        #endregion

        #region Reset Password

        [HttpGet("reset-password/{emailActiveCode}")]        
        public IActionResult ResetPassword(string emailActiveCode)
        {
            var User = _context.Users.SingleOrDefault(u => u.ActiveCode == emailActiveCode);

            if (User == null) return NotFound();

            return View();
        }
        [HttpPost("reset-password/{emailActiveCode}"),ValidateAntiForgeryToken]
        public IActionResult ResetPassword(string emailActiveCode,ResetPasswordViewModel resetPassword)
        {
            var User = _context.Users.SingleOrDefault(u => u.ActiveCode == emailActiveCode);

            if (User == null) return NotFound();

            User.Password = resetPassword.Password;
            User.ActiveCode = Guid.NewGuid().ToString("N");
            User.IsActive = true;
            _context.Users.Update(User);
            _context.SaveChanges();

            return View("Login");
        }
        #endregion

        #region Login

        [HttpGet("LogIn")]
        public IActionResult Login()
        {
            //var Accounts = _context.Users.ToList();

            return View();
        }
        [HttpPost("LogIn")]
        public IActionResult Login(LogInViewModel logInUser)
        {

            //Get user by email
            var User = _context.Users.SingleOrDefault(u => u.Email == logInUser.Email.ToLower().Trim());

            if (ModelState.IsValid)
            {
                //Check Exist Email
                if (User != null)
                {
                    //Check user password
                    if (User.Password == logInUser.Password)
                    {
                        //Check user is active or not
                        if (User.IsActive == true)
                        {
                            // Login : cookie ( or jwt => json web token )
                            // 1 - user information
                            List<Claim> claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, User.UserID.ToString()),
                                new Claim(ClaimTypes.Email, User.Email)
                            };

                            // 2 - set generation type
                            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            // 3 - generate current user
                            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                            // 4 - login current user
                            HttpContext.SignInAsync(principal);

                            return Redirect("/");
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                }

            }
            return View();
        }
        #endregion

        #region Logout

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}
