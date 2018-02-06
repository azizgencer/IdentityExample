using IdentityExample.DAL;
using IdentityExample.Identity;
using IdentityExample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public AccountController()
        {
            IdentityContext db = new IdentityContext();

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            _userManager = new UserManager<ApplicationUser>(userStore);

            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            _roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        #region Register İşlemleri

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Username
                };

                IdentityResult iResult = _userManager.Create(user, model.Password);

                if (iResult.Succeeded)
                {
                    // User seviyesinde role atanıyor.
                    _userManager.AddToRole(user.Id, "User");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUser", "Kullanıcı işleminde hata !");
                }
            }

            return View(model);
        }

        #endregion

        #region Login İşlemleri

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Find(model.Username, model.Password);

                if (user != null)
                {
                    IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;

                    ClaimsIdentity identity = _userManager.CreateIdentity(user, "ApplicationCookie");
                    AuthenticationProperties authProps = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    authManager.SignIn(authProps, identity);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUser", "Böyle bir kullanıcı bulunumadı !");
                }
            }

            return View(model);
        }

        #endregion

        #region Logout İşlemleri

        [Authorize]
        public ActionResult Logout()
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}