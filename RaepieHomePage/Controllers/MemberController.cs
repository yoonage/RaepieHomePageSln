using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RaepieHomePage.Models;
using System.Data.Entity;

namespace RaepieHomePage.Controllers
{
    public class MemberController : Controller
    {
        RaepieHomePageEntities db = new RaepieHomePageEntities();

        [HttpGet]
        public ActionResult Entry()
        {
            Members member = new Members();
            return View(member);
        }

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Entry(Members member)
        {
            member.EntryDate = DateTime.Now;
            try
            {
                db.Members.Add(member);
                db.SaveChanges();
                ViewBag.Result = "OK";
            }catch(Exception ex)
            {
                ViewBag.Result = "FAIL";
            }

            return View(member);
        }

        public ActionResult List()
        {
            List<Members> list = db.Members.OrderByDescending(o => o.EntryDate).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult Edit(string memberid)
        {
            Members member = db.Members.Where(c => c.MemberID == memberid).FirstOrDefault();
            return View(member);
        }

        [HttpPost]
        public ActionResult Edit(Members member)
        {
            Members dbMember = db.Members.Find(member.MemberID);
            try
            {
                dbMember.MemberName = member.MemberName;
                dbMember.MemberPWD = member.MemberPWD;
                dbMember.Email = member.Email;
                dbMember.Telephone = member.Telephone;

                db.Entry(dbMember).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Resullt = "OK";
            }
            catch(Exception ex)
            {
                ViewBag.Result = "FAlL";
            }
            return View(dbMember);
        }

        [HttpGet]
        public ActionResult Delete(string memberid)
        {
            Members dbMember = db.Members.Find(memberid);
            db.Members.Remove(dbMember);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        //open API
        public JsonResult IDCheck(string memberid)
        {
            string result = string.Empty;
            Members member = db.Members.Find(memberid);
            if (member == null)
            {
                result = "OK";
            }
            else
            {
                result = "FAIL";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}