using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameDemo.Models;

namespace GameDemo.Controllers
{
    public class HomeController : Controller
    {
        GameDBEntities db = new GameDBEntities();
        public ActionResult Index()
        {
            //查询，获取所有的用户信息，包含主表和从表的所有内容
            //db.Accounts.Include("Msg");
            //List<Msg> list1 = db.Msgs.Include("Account").OrderByDescending(t => t.MsgTime).ToList();
            ViewBag.list = db.Msgs.Include("Account").OrderByDescending(t=>t.MsgTime).ToList();
            return View();
        }



        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult news()
        {
            //1查询所有信息类别是新闻公告的数据 传递到前端页面
            List<Msg> list = db.Msgs.Include("Account").Where(t => t.MsgType == "新闻公告").ToList();
            return View(list);

        }



        public ActionResult heros()
        {
            List<Msg> list = db.Msgs.Include("Account").Where(t => t.MsgType == "英雄档案").ToList();
            return View(list);
        }


        public ActionResult videos()
        {
            List<Msg> list = db.Msgs.Include("Account").Where(t => t.MsgType == "视频图片").ToList();
            return View(list);
        }
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}