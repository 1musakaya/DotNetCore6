using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebApplicationCoreLogin.Models;
using WebApplicationCoreLogin.Models.ViewModel;

namespace WebApplicationCoreLogin.Controllers
{
    public class AccountController : Controller
    {

        private DatabaseContext db;
        private IConfiguration _configuration;
        public AccountController(DatabaseContext dbcontext,IConfiguration configuration)
        {
            db=dbcontext;
            _configuration=configuration;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model)
        {
            string sifre = _configuration.GetValue<string>("Appsettings:sifre");
            sifre = model.Password + sifre;
            string md5sifre = sifre.MD5();

            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(x => x.UserName.ToLower() == model.Username.ToLower() && x.Password==md5sifre);
                if (user!=null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim("Name", user.Name ?? string.Empty));
                    claims.Add(new Claim("Username", user.UserName));

                    ClaimsIdentity claimsIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı ya da şifre hatalı");
                }
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (db.Users.Any(x => x.UserName.ToLower() == model.Username.ToLower()))
                    {
                        ModelState.AddModelError(nameof(model.Username), "Bu kullanıcı adı bir başkasına ait");
                        return View(model);
                    }
                }

                string sifre= _configuration.GetValue<string>("Appsettings:sifre");
                sifre = model.Password + sifre;

                string md5sifre = sifre.MD5();

                User user=new ()
                {
                    UserName=model.Username,
                    Password=md5sifre,

                };
                db.Users.Add(user);
                if(db.SaveChanges()==0)
                {
                    ModelState.AddModelError("", "Kayıt Eklenemedi");
                }
                else
                {
                    return RedirectToAction("Login");
                }

            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Profil()
        {
            Guid id = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = db.Users.SingleOrDefault(x => x.Id == id);
            ViewData["adsoyad"] = user.Name;
            return View();
        }
        [HttpPost]
        public IActionResult AdSoyadKaydet(string adsoyad)
        {
            if (ModelState.IsValid)
            {
                Guid id = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user=db.Users.SingleOrDefault(x => x.Id == id); 
                
                user.Name=adsoyad;
                db.SaveChanges();
                return RedirectToAction("Profil");
            }
            return View("Profil");
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
