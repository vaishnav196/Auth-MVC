using AuthMVC.Models;
using AuthMVC.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AuthMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext db;

        public AccountController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public static string EncryptPassword(string password)
        {
            if ( string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] data=ASCIIEncoding.ASCII.GetBytes(password);
                string ep = Convert.ToBase64String(data);
                return ep ;
            }
        }

        public static string DecryptPassword(string password)
        {   
            if (string.IsNullOrEmpty(password))
            {
                return null; 
            }
            else
            {
                try
                {
                    byte[] data = Convert.FromBase64String(password);
                    string dp = Encoding.ASCII.GetString(data);
                    return dp;
                }
                catch (FormatException ex)
                {
                    
                    throw new ArgumentException("Invalid base64 string", nameof(password), ex);
                }
            }
        }


       

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User u)
        {   //db.users.Add(u);
            if (ModelState.IsValid)
            {
                if (db.users.Any(x => x.Email == u.Email))
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(u);
                }
                var us = new User()
                {
                    Name = u.Name,
                    Email = u.Email,
                    Password = EncryptPassword(u.Password)

                };
                db.Add(us);
                db.SaveChanges();
                TempData["msg"] = "User Added Successfully !!";
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

           
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginModel m)
        {
            if (ModelState.IsValid)
            {   var data= db.users.FirstOrDefault(t => t.Email.Equals(m.Email));
                if (data != null)
                {
                    bool d = data.Email.Equals(m.Email) && DecryptPassword(data.Password).Equals(m.Password);
                    if (d)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["InvalidPassword"] = "Invalid Passowrd";
                        return View();  
                    }
                }
                else
                {
                    TempData["InvalidEmail"] = "Invalid Email";
                    return View();
                }
            }
            else
            {
                return View();
            }
            return View();
        }
    }
}
