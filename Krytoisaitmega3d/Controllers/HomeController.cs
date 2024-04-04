using Krytoisaitmega3d.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Krytoisaitmega3d.Controllers
{
    public class HomeController : Controller
    {
        private Gun_Shop_DatabaseContext db;

        public HomeController(Gun_Shop_DatabaseContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Clients.ToListAsync());
        }

        public IActionResult autorization()
        {
            if (HttpContext.Session.Keys.Contains("AuthUser"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> autorization(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Client client = await db.Clients.FirstOrDefaultAsync(p => p.Login == model.Login && p.Password == model.Password);
                if (client != null)
                {
                    HttpContext.Session.SetString("AuthUser", model.Login);
                    await Authenticate(model.Login);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин или пароль");
            }
            return RedirectToAction("autorization", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("AuthUser");
            return RedirectToAction("autorization");
        }

        private async Task Authenticate(string UserName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, UserName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> katalog()
        {
            var Gun = db.Guns.Include(p => p.Pushka).Include(p => p.Pistol).Include(p => p.Machine);
            return View(await Gun.ToListAsync());
        }
        public IActionResult registracion()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("autorization");
            }
            else
                return View(client);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AddToCart()
        {
            int ID = Convert.ToInt32(Request.Query["ID"]);
            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart"))
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart"));
            cart.CartLines.Add(db.Guns.Find(ID));
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize<Cart>(cart));
            return Redirect("~/Home/Index");
        }
        public IActionResult RemoveFromCart()
        {
            int number = Convert.ToInt32(Request.Query["Number"]);
            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart"))
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart"));
            cart.CartLines.RemoveAt(number);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize<Cart>(cart));
            return Redirect("~/Home/Cart");
        }
        public IActionResult RemoveAllFromCart()
        {
            Cart cartModel = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart"))
                cartModel = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart"));
            for (int i = 0; i < cartModel.CartLines.Count; i++)
                cartModel.CartLines.RemoveAt(i);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize<Cart>(cartModel));
            return Redirect("~/Home/Cart");
        }
        public  IActionResult Cart()
        {
            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart"))
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart"));
            return View (cart);
        }

    }
}