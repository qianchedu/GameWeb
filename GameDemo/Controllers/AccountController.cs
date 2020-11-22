using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameDemo.Models;

namespace GameDemo.Controllers
{
    public class AccountController : Controller
    {
        GameDBEntities db = new GameDBEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string account, string pwd)
        {
            //1.根据传递过来的用户名和密码，到数据库中查询是否存在
            Account acc = db.Accounts.FirstOrDefault(t => t.AccountName == account && t.AccountPwd == pwd);
            if (acc!=null)
            {
                Session["login"] = acc;
                return RedirectToAction("index","home");
            }
            else
            {
                TempData["msg"] = "用户的名称或者密码错误！";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Account acc)
        {
            Account a = acc;
          
            if (acc!=null)
            {
                db.Accounts.Add(acc);
                db.SaveChanges();
                TempData["msg"] = "恭喜，注册成功，请登录！";
                return View("login");
            }
            else
            {
                TempData["msg"] = "注册失败，重新注册！";
                return View();
            }
        }

        public ActionResult Quite()
        {
            //1.退出功能的实现，只需要将session存入的值设置为NULL
            Session["login"] = null;

            return RedirectToAction("index","home");
        }
    }
}