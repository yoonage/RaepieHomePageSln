﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;

using RaepieHomePage.Models;
using System.Data.Entity;

namespace RaepieHomePage.Controllers
{
    public class BoardController : Controller
    {
        RaepieHomePageEntities db = new RaepieHomePageEntities();        

        [HttpGet]
        public ActionResult Create()
        {
            Articles article = new Articles();
            return View(article);
        }

        [HttpPost]
        public ActionResult Create(Articles article)
        {
            try
            {
                article.ViewCount = 0;
                article.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                article.RegistDate = DateTime.Now;
                article.RegistMemberID = "admin";
                article.ModifyDate = DateTime.Now;
                article.ModifyMemberID = "admin";

                db.Articles.Add(article);
                db.SaveChanges();

                if (Request.Files.Count > 0)
                {
                    var attachFile = Request.Files[0];

                    if (attachFile != null && attachFile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(attachFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/Upload/"), fileName);
                        attachFile.SaveAs(path);

                        ArticleFiles file = new ArticleFiles();
                        file.ArticleIDX = article.ArticleIDX;
                        file.FilePath = "/Upload/";
                        file.FileName = fileName;
                        file.FileFormat = Path.GetExtension(attachFile.FileName);
                        file.FileSize = attachFile.ContentLength;
                        file.UploadDate = DateTime.Now;
                        db.ArticleFiles.Add(file);

                        db.SaveChanges();
                    }
                    ViewBag.Result = "OK";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = "FAIL";                
            }

            return View(article);
        }

        // GET: Board
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ArticleList()
        {
            List<Articles> list = db.Articles.OrderByDescending(o => o.ModifyDate).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult Edit(int aidx)
        {
            ArticleEditViewModel vm = new ArticleEditViewModel();
            Articles article = db.Articles.Where(c => c.ArticleIDX == aidx).FirstOrDefault();
            List<ArticleFiles> files = db.ArticleFiles.Where(c => c.ArticleIDX == aidx).OrderBy(o => o.UploadDate).ToList();
            vm.Article = article;
            vm.Files = files;

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(ArticleEditViewModel vm)
        {
            ArticleEditViewModel dbVM = new ArticleEditViewModel();

            try
            {
                Articles dbArticle = db.Articles.Find(vm.Article.ArticleIDX);

                dbArticle.Title = vm.Article.Title;
                dbArticle.Category = vm.Article.Category;
                dbArticle.Contents = vm.Article.Contents;
                dbArticle.IPAddress = vm.Article.IPAddress;
                dbArticle.ModifyDate = DateTime.Now;
                dbArticle.ModifyMemberID = "admin";

                db.Entry(dbArticle).State = EntityState.Modified;
                db.SaveChanges();

                if (Request.Files.Count > 0)
                {
                    var attachFile = Request.Files[0];

                    if (attachFile != null && attachFile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(attachFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/Upload/"), fileName);
                        attachFile.SaveAs(path);

                        ArticleFiles file = new ArticleFiles();
                        file.ArticleIDX = vm.Article.ArticleIDX;
                        file.FilePath = "/Upload/";
                        file.FileName = fileName;
                        file.FileFormat = Path.GetExtension(attachFile.FileName);
                        file.FileSize = attachFile.ContentLength;
                        file.UploadDate = DateTime.Now;

                        db.ArticleFiles.Add(file);
                        db.SaveChanges();
                    }
                }

                Articles article = db.Articles.Where(c => c.ArticleIDX == vm.Article.ArticleIDX).FirstOrDefault();
                List<ArticleFiles> files = db.ArticleFiles.Where(c => c.ArticleIDX == vm.Article.ArticleIDX).OrderBy(o => o.UploadDate).ToList();

                dbVM.Article = article;
                dbVM.Files = files;
                ViewBag.Result = "OK";
            } catch(Exception ex)
            {
                dbVM = vm;
                ViewBag.Result = "FAIL";
            }
            return View(dbVM);
        }

        [HttpGet]
        public ActionResult ArticleDelete(int aidx)
        {
            Articles dbArticle = db.Articles.Where(c => c.ArticleIDX == aidx).FirstOrDefault();
            db.Articles.Remove(dbArticle);
            db.SaveChanges();

            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public ActionResult FileRemove(int fidx)
        {
            ArticleFiles file = db.ArticleFiles.Where(c => c.FileIDX == fidx).FirstOrDefault();
            int articleIDX = Convert.ToInt32(file.ArticleIDX);

            System.IO.File.Delete(Server.MapPath(file.FilePath + file.FileName));

            db.ArticleFiles.Remove(file);
            db.SaveChanges();

            //return Redirect("/Board/Edit?adix=" + articleIDX.ToString());
            return RedirectToAction("Edit", new { aidx = articleIDX.ToString() });
        }
    }
}