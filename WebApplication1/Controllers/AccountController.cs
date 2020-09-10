using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.Owin.Security.DataProtection;

namespace WebApplication1.Controllers
{
   
        // GET: Account
        public class AccountController : Controller
        {
            private ApplicationUserManager UserManager
            {
                get
                {
                    return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
            }

            public ActionResult Register()
            {
                return View();
            }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var provider = new DpapiDataProtectionProvider("SampleAppName");

                model.UserName = model.Email;
               // var user = new UserModel { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(model);
                if (result.Succeeded)
                {
                    UserManager.UserTokenProvider = new DataProtectorTokenProvider<UserModel>(
                                provider.Create("SampleTokenName"));
                    // генерируем токен для подтверждения регистрации
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(model.Id);
                    // создаем ссылку для подтверждения
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = model.Id, code = code },
                               protocol: Request.Url.Scheme);
                    // отправка письма
                    await UserManager.SendEmailAsync(model.Id, "Подтверждение электронной почты",
                               "Для завершения регистрации перейдите по ссылке:: <a href=\""
                                                               + callbackUrl + "\">завершить регистрацию</a>");
                    return View("DisplayEmail");
                }
                //AddErrors(result);
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserModel user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    if (user.EmailConfirmed == true)
                    {
                        ModelState.AddModelError("", "Не подтвержден email.");
                    }

                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
    
}