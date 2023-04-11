using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MemberController : Controller
    {
        private MyDbContext _MyDbContext;
        public MemberController(MyDbContext MyDbContext)
        {
            _MyDbContext = MyDbContext;
        }

        // GET: MemberController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MemberController/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsync(IFormCollection collection)
        {
           
            string? passwd = collection["Passwd"];
            string? repasswd = collection["rePasswd"];
            string account = collection["Account"];
            //if(passwd != repasswd)
            //{
            //    ViewBag.err = "密碼不相同";
            //    return View();
            //}
            bool a = await _MyDbContext.Members.AnyAsync(x=>x.Account==account);
            if (a)
            {
                ViewBag.err = "帳號已存在";
                return View();
            }

            _MyDbContext.Members.Add(new Member()
            {
                Account = collection["Account"],
                Passwd = collection["Passwd"],
                Email = collection["Email"],
                Name = collection["Name"],

            });


            _MyDbContext.SaveChanges();



            return RedirectToAction("Index", "Home");

        }
        public ActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
        public ActionResult login()
        {
            return View();
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(IFormCollection collection)
        {
            string? account = collection["Account"];
            string? passwd = collection["Passwd"];
            try
            {
                var a = _MyDbContext.Members.Where(x => x.Account == account && x.Passwd == passwd);
                if (a.Count() == 0)
                {
                    ViewBag.err = "帳密錯誤";
                    return View();
                }
                else
                {
                    string? name = a.First().Name;
                    HttpContext.Session.SetString("account", account);
                    HttpContext.Session.SetString("name", name);
                }

                return RedirectToAction("Index", "Product");
            }
            catch
            {
                return View();
            }
        }
        // GET: MemberController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
