using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameDemo.Models;
namespace GameDemo.Controllers
{
    public class MsgController : Controller
    {
        GameDBEntities db = new GameDBEntities();
        // GET: Msg
        public ActionResult Index()
        {
            //1.获取到当前登录用户的用户名，利用用户名查询该用户发布的所有数据内容
            string UserName = ((Account)Session["login"]).AccountName;
            if(UserName != null)
            {
                List<Msg> list = db.Msgs.Include("Account").Where(t=>t.Account.AccountName==UserName).ToList();
                return View(list);

            }
            else
            {
                return RedirectToAction("index","home");
            }
            
        }

        public ActionResult Delete(int Msg_id)
        {
            //1.获取传递过来的msgid的值
            //Request.QueryString["Msg_id"];
            int id = Msg_id;
            Msg msg = db.Msgs.SingleOrDefault(t=>t.MsgID==Msg_id);
            if(msg!=null)
            {
                db.Msgs.Remove(msg);
                db.SaveChanges();
                return RedirectToAction("index", "msg");
            }
            else
            {

                return RedirectToAction("index", "msg");
            }
        }


        public ActionResult Detail(int Msg_id)
        {
            //Msg msg = db.Msgs.SingleOrDefault(t => t.MsgID == Msg_id);
            Msg msg = db.Msgs.Include("Account").SingleOrDefault(t => t.MsgID == Msg_id);
            return View(msg);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int Msg_id)
        {
            //1.根据页面传递的msgid，进行数据的查询，并将数据传递到前端页面
            Msg msg = db.Msgs.Include("Account").SingleOrDefault(t=>t.MsgID==Msg_id);
            return View(msg);
        }


        [HttpPost]
        public ActionResult Eidt(Msg msg,HttpPostedFileBase PhotoFile)
        {
            //1.补全msg对象中属性值
            //Msg m = msg;
            if (PhotoFile!=null)
            {
                msg.MsgPhoto = "/img/" +PhotoFile.FileName;
                PhotoFile.SaveAs(Server.MapPath(msg.MsgPhoto));
            }

            msg.MsgTime = DateTime.Now;

            //2.将修改好的msg对象添加到数据库中
            db.Entry(msg).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("index");
        }


        public ActionResult Create()
        {

            return View();
        }


        public ActionResult Create(Msg msg,HttpPostedFileBase PhotoFile)
        {
            if(PhotoFile != null)
            {
                msg.MsgPhoto = "/img/" + PhotoFile.FileName;
                PhotoFile.SaveAs(Server.MapPath(msg.MsgPhoto));
            }
            msg.MsgTime = DateTime.Now;
            msg.MsgHit = 0;
            msg.AccountID = ((Account)Session["login"]).AccountID;
            db.Msgs.Add(msg);
            db.SaveChanges();
            return RedirectToAction("index");
        }

    }
}